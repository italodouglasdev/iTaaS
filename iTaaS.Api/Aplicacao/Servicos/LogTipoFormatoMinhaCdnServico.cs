using iTaaS.Api.Aplicacao.DTOs;
using iTaaS.Api.Aplicacao.DTOs.Auxiliares;
using iTaaS.Api.Aplicacao.Interfaces.Servicos;
using iTaaS.Api.Dominio.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace iTaaS.Api.Aplicacao.Servicos
{
    public class LogTipoFormatoMinhaCdnServico : ILogTipoFormatoServico
    {
        private const string SUFIXO_TIPO_LOG_MINHA_CDN = "MINHA_CDN";

        private const string END_POINT_BUSCAR_SALVO_ID = "Api/Log/BuscarSalvoId";

        private const string DEMILITADOR_PIPE = "|";
        private const string DEMILITADOR_ESPACO = " ";

        private const int QUANTIDADE_CAMPOS = 5;

        private const int INDEX_TAMANHO_RESPOSTA = 0;
        private const int INDEX_CODIGO_STATUS = 1;
        private const int INDEX_CACHE_STATUS = 2;
        private const int INDEX_METODO_HTTP_E_CAMINHO_URL = 3;

        private const int INDEX_METODO_HTTP = 0;
        private const int INDEX_CAMINHO_URL = 1;
        private const int INDEX_VERSAO = 2;
        private const int INDEX_TEMPO_RESPOSTA = 4;



        public Resultado<string> ConverterDeArquivoParaString(string caminho)
        {
            var resultadoString = new Resultado<string>();

            var consultaLerArquivo = SistemaArquivosHelper.LerArquivoTxt(caminho);
            if (!consultaLerArquivo.Sucesso)
            {
                resultadoString.AdicionarInconsistencias(consultaLerArquivo.Inconsistencias);
                return resultadoString;
            }

            var consultaListaParaString = ConversorHelper.ConverterDeListaParaString(consultaLerArquivo.Dados);
            if (!consultaListaParaString.Sucesso)
                resultadoString.AdicionarInconsistencias(consultaListaParaString.Inconsistencias);

            resultadoString.Dados = consultaListaParaString.Dados;

            return resultadoString;
        }

        public Resultado<string> ConverterDeDtoParaArquivo(string urlBase, LogDto logDto)
        {
            var resultadoArquivo = new Resultado<string>();

            var resultadoDtoString = ConverterDeDtoParaString(logDto);
            if (!resultadoDtoString.Sucesso)
            {
                resultadoArquivo.AdicionarInconsistencias(resultadoDtoString.Inconsistencias);
                return resultadoArquivo;
            }

            var consultaArvoreDiretorios = SistemaArquivosHelper.CriarArvoreDiretorios(logDto.DataHoraRecebimento);
            if (!consultaArvoreDiretorios.Sucesso)
            {
                resultadoArquivo.AdicionarInconsistencias(consultaArvoreDiretorios.Inconsistencias);
                return resultadoArquivo;
            }

            var caminhoArquivo = $"{consultaArvoreDiretorios.Dados}\\{logDto.ObtenhaNomeArquivo(SUFIXO_TIPO_LOG_MINHA_CDN)}";
            var consultaArquivoString = ConverterDeArquivoParaString(caminhoArquivo);
            if (!consultaArquivoString.Sucesso)
            {
                var consultaArquivoTxt = SistemaArquivosHelper.CriarArquivoTxt(caminhoArquivo, resultadoDtoString.Dados);
                if (!consultaArquivoTxt.Sucesso)
                {
                    resultadoArquivo.AdicionarInconsistencias(consultaArquivoTxt.Inconsistencias);
                    return resultadoArquivo;
                }
            }

            resultadoArquivo.Dados = $"{urlBase}/{END_POINT_BUSCAR_SALVO_ID}/{logDto.Id}";

            return resultadoArquivo;

        }

        public Resultado<string> ConverterDeDtoParaString(LogDto logDto)
        {
            var resultado = new Resultado<string>();

            var strinBuilder = new StringBuilder();

            foreach (var dtoLogLinha in logDto.Linhas)
                strinBuilder.AppendLine($"{dtoLogLinha.TamahoResposta}|{dtoLogLinha.CodigoStatus}|{dtoLogLinha.CacheStatus}|\"{dtoLogLinha.MetodoHttp} {dtoLogLinha.CaminhoUrl} HTTP/{logDto.Versao}\"|{dtoLogLinha.TempoResposta.ToString("F1").Replace(",",".")}");

            var consultaArvoreDiretorios = SistemaArquivosHelper.CriarArvoreDiretorios(logDto.DataHoraRecebimento);
            if (!consultaArvoreDiretorios.Sucesso)
            {
                resultado.AdicionarInconsistencias(consultaArvoreDiretorios.Inconsistencias);
                return resultado;
            }

            var caminhoArquivo = $"{consultaArvoreDiretorios.Dados}\\{logDto.ObtenhaNomeArquivo(SUFIXO_TIPO_LOG_MINHA_CDN)}";
            var consultaArquivoString = ConverterDeArquivoParaString(caminhoArquivo);
            if (!consultaArquivoString.Sucesso)
            {
                var consultaArquivoTxt = SistemaArquivosHelper.CriarArquivoTxt(caminhoArquivo, strinBuilder.ToString());
                if (!consultaArquivoTxt.Sucesso)
                {
                    resultado.AdicionarInconsistencias(consultaArquivoTxt.Inconsistencias);
                    return resultado;
                }

                consultaArquivoString = ConverterDeArquivoParaString(caminhoArquivo);
            }

            if (!consultaArquivoString.Sucesso)
            {
                resultado.AdicionarInconsistencias(consultaArquivoString.Inconsistencias);
                return resultado;
            }

            resultado.Dados = consultaArquivoString.Dados;

            return resultado;
        }

        public Resultado<LogDto> ConverterDeStringParaDto(string logString)
        {
            var resultadoDto = new Resultado<LogDto>();

            resultadoDto.Dados = new LogDto();

            resultadoDto.Dados.DataHoraRecebimento = DateTime.Now;

            var listaLinhas = new List<LogLinhaDto>();

            using (var reader = new StringReader(logString))
            {
                var contadorLinhas = 0;
                string linha;

                while ((linha = reader.ReadLine()) != null)
                {
                    var logLinha = new LogLinhaDto();

                    contadorLinhas++;

                    var resultadoConversaoCampos = ConversorHelper.ConverterStringEmListaPorDelimitador(linha, DEMILITADOR_PIPE);
                    if (!resultadoConversaoCampos.Sucesso)
                    {
                        resultadoDto.AdicionarInconsistencia("FORMATO_INVALIDO", $"O formato da linha {contadorLinhas} é inválido!");
                        resultadoDto.AdicionarInconsistencias(resultadoConversaoCampos.Inconsistencias);
                        return resultadoDto;
                    }

                    var listaCamposLinha = resultadoConversaoCampos.Dados;

                    if (listaCamposLinha.Count < QUANTIDADE_CAMPOS)
                    {
                        resultadoDto.AdicionarInconsistencia("FORMATO_INVALIDO", $"O formato da linha {contadorLinhas} é inválido!");
                        return resultadoDto;
                    }

                    logLinha.Id = 0;
                    logLinha.LogId = 0;

                    logLinha.TamahoResposta = ConversorHelper.ConverterStringParaInt(listaCamposLinha[INDEX_TAMANHO_RESPOSTA]);
                    logLinha.CodigoStatus = ConversorHelper.ConverterStringParaInt(listaCamposLinha[INDEX_CODIGO_STATUS]);
                    logLinha.CacheStatus = listaCamposLinha[INDEX_CACHE_STATUS];



                    var campoComMetodoHttpUrl = listaCamposLinha[INDEX_METODO_HTTP_E_CAMINHO_URL];
                    var resultadoConversaoCamposHttp = ConversorHelper.ConverterStringEmListaPorDelimitador(campoComMetodoHttpUrl, DEMILITADOR_ESPACO);
                    if (!resultadoConversaoCamposHttp.Sucesso)
                    {
                        resultadoDto.AdicionarInconsistencia("FORMATO_INVALIDO", $"O formato da linha {contadorLinhas} é inválido!");
                        resultadoDto.AdicionarInconsistencias(resultadoConversaoCamposHttp.Inconsistencias);
                        return resultadoDto;
                    }


                    var listaCamposMetodoHttp = resultadoConversaoCamposHttp.Dados;
                    logLinha.MetodoHttp = listaCamposMetodoHttp[INDEX_METODO_HTTP].Replace("\"", "");
                    logLinha.CaminhoUrl = listaCamposMetodoHttp[INDEX_CAMINHO_URL];
                    logLinha.TempoResposta = ConversorHelper.ConverterStringParaDecimal(listaCamposLinha[INDEX_TEMPO_RESPOSTA]);

                    resultadoDto.Dados.Versao = listaCamposMetodoHttp[INDEX_VERSAO].Replace("\"", "").Replace("HTTP/", "");

                    resultadoDto.Dados.Linhas.Add(logLinha);
                }
            }

            return resultadoDto;
        }

        public Resultado<LogDto> ConverterDeUrlParaDto(string url)
        {
            var resultadoDto = new Resultado<LogDto>();

            var resultadoDeUrlParaString = SistemaArquivosHelper.ObtenhaStringDeUrl(url);
            if (!resultadoDeUrlParaString.Sucesso)
            {
                resultadoDto.AdicionarInconsistencias(resultadoDeUrlParaString.Inconsistencias);
                return resultadoDto;
            }

            resultadoDto = ConverterDeStringParaDto(resultadoDeUrlParaString.Dados);

            return resultadoDto;
        }
    }
}
