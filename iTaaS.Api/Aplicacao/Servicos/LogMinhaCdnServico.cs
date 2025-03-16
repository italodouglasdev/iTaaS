using iTaaS.Api.Aplicacao.DTOs;
using iTaaS.Api.Aplicacao.Interfaces.Servicos;
using iTaaS.Api.Dominio.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace iTaaS.Api.Aplicacao.Servicos
{
    public class LogMinhaCdnServico : IConverterLogServico
    {

        private const string SUFIXO_TIPO_LOG_MINHA_CDN = "MINHA_CDN";

        private const string DEMILITADOR_PIPE = "|";
        private const string DEMILITADOR_ESPACO = " ";

        private const int QUANTIDADE_CAMPOS = 5;

        private const int INDEX_TAMANHO_RESPOSTA = 0;
        private const int INDEX_CODIGO_STATUS = 1;
        private const int INDEX_CACHE_STATUS = 2;
        private const int INDEX_METODO_HTTP_E_CAMINHO_URL = 3;

        private const int INDEX_METODO_HTTP = 0;
        private const int INDEX_CAMINHO_URL = 1;
        private const int INDEX_TEMPO_RESPOSTA = 4;



        public Resultado<LogDto> ConverterDeArquivoParaDto(string caminho)
        {
            throw new NotImplementedException();
        }

        public Resultado<string> ConverterDeArquivoParaString(string caminho)
        {
            throw new NotImplementedException();
        }

        public Resultado<string> ConverterDeDtoParaArquivo(LogDto logDto)
        {
            var resultadoArquivo = new Resultado<string>();

            var resultadoDtoString = ConverterDeDtoParaString(logDto);
            if (!resultadoDtoString.Sucesso)
            {
                resultadoArquivo.Inconsistencias = resultadoDtoString.Inconsistencias;
                return resultadoArquivo;
            }

            var consultaArvoreDiretorios = UtilitarioHelper.CriarArvoreDiretorios(logDto.DataHoraRecebimento);
            if (!consultaArvoreDiretorios.Sucesso)
            {
                resultadoArquivo.Inconsistencias = consultaArvoreDiretorios.Inconsistencias;
                return resultadoArquivo;
            }


            var nomeArquivo = logDto.ObtenhaNomeArquivo(SUFIXO_TIPO_LOG_MINHA_CDN);
            var caminhoArquivo = $"{consultaArvoreDiretorios.Dados}\\{nomeArquivo}";
            var consultaArquivoTxt = UtilitarioHelper.CriarArquivoTxt(caminhoArquivo, resultadoDtoString.Dados);
            if (!consultaArquivoTxt.Sucesso)
            {
                resultadoArquivo.Inconsistencias = consultaArquivoTxt.Inconsistencias;
                return resultadoArquivo;
            }

            //Ajustar
            resultadoArquivo.Dados = $"https://localhost:44339/api/Log/Ver/{nomeArquivo}";

            return resultadoArquivo;


            //var resultadoArquivo = new Resultado<string>();

            //var resultadoDtoString = ConverterDeDtoParaString(logDto);
            //if (!resultadoDtoString.Sucesso)
            //{
            //    resultadoArquivo.Inconsistencias = resultadoDtoString.Inconsistencias;
            //    return resultadoArquivo;
            //}

            //var consultaArvoreDiretorios = UtilitarioHelper.CriarArvoreDiretorios(logDto.DataHoraRecebimento);
            //if (!consultaArvoreDiretorios.Sucesso)
            //{
            //    resultadoArquivo.Inconsistencias = consultaArvoreDiretorios.Inconsistencias;
            //    return resultadoArquivo;
            //}

            //var consultaCriarArquivoTxt = UtilitarioHelper.CriarArquivoTxt(consultaArvoreDiretorios.Dados, resultadoDtoString.Dados);
            //if (!consultaCriarArquivoTxt.Sucesso)
            //{
            //    resultadoArquivo.Inconsistencias = consultaCriarArquivoTxt.Inconsistencias;
            //    return resultadoArquivo;
            //}

            //return resultadoArquivo;
        }

        public Resultado<string> ConverterDeDtoParaString(LogDto logDto)
        {
            var resultado = new Resultado<string>();

            var strinBuilder = new StringBuilder();

            foreach (var dtoLogLinha in logDto.Linhas)
                strinBuilder.AppendLine($"{dtoLogLinha.TamahoResposta}|{dtoLogLinha.CodigoStatus}|{dtoLogLinha.CacheStatus}|\"{dtoLogLinha.MetodoHttp} {dtoLogLinha.CaminhoUrl} HTTP/1.1\"|{dtoLogLinha.TempoResposta}");

            resultado.Dados = strinBuilder.ToString();

            return resultado;
        }

        public Resultado<string> ConverterDeStringParaArquivo(string logString, string caminho)
        {
            throw new NotImplementedException();
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

                    var resultadoConversaoCampos = UtilitarioHelper.ConverterStringEmListaPorDelimitador(linha, DEMILITADOR_PIPE);
                    if (!resultadoConversaoCampos.Sucesso)
                    {
                        resultadoDto.AdicionarInconsistencia("FORMATO_INVALIDO", $"O formato da linha {contadorLinhas} é inválido!");
                        resultadoDto.Inconsistencias = resultadoConversaoCampos.Inconsistencias;
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

                    logLinha.TamahoResposta = UtilitarioHelper.ConverterStringParaInt(listaCamposLinha[INDEX_TAMANHO_RESPOSTA]);
                    logLinha.CodigoStatus = UtilitarioHelper.ConverterStringParaInt(listaCamposLinha[INDEX_CODIGO_STATUS]);
                    logLinha.CacheStatus = listaCamposLinha[INDEX_CACHE_STATUS];



                    var campoComMetodoHttpUrl = listaCamposLinha[INDEX_METODO_HTTP_E_CAMINHO_URL];
                    var resultadoConversaoCamposHttp = UtilitarioHelper.ConverterStringEmListaPorDelimitador(campoComMetodoHttpUrl, DEMILITADOR_ESPACO);
                    if (!resultadoConversaoCamposHttp.Sucesso)
                    {
                        resultadoDto.AdicionarInconsistencia("FORMATO_INVALIDO", $"O formato da linha {contadorLinhas} é inválido!");
                        resultadoDto.Inconsistencias = resultadoConversaoCamposHttp.Inconsistencias;
                        return resultadoDto;
                    }


                    var listaCamposMetodoHttp = resultadoConversaoCamposHttp.Dados;
                    logLinha.MetodoHttp = listaCamposMetodoHttp[INDEX_METODO_HTTP].Replace("\"", "");
                    logLinha.CaminhoUrl = listaCamposMetodoHttp[INDEX_CAMINHO_URL];
                    logLinha.TempoResposta = UtilitarioHelper.ConverterStringParaDecimal(listaCamposLinha[INDEX_TEMPO_RESPOSTA]);

                    resultadoDto.Dados.Linhas.Add(logLinha);
                }
            }

            return resultadoDto;
        }

        public Resultado<string> ConverterDeUrlParaArquivo(string url, string caminho)
        {
            throw new NotImplementedException();
        }

        public Resultado<LogDto> ConverterDeUrlParaDto(string url)
        {
            var resultadoDto = new Resultado<LogDto>();

            var resultadoDeUrlParaString = UtilitarioHelper.ObtenhaStringDeUrl(url);
            if (!resultadoDeUrlParaString.Sucesso)
            {
                resultadoDto.Inconsistencias = resultadoDeUrlParaString.Inconsistencias;
                return resultadoDto;
            }

            resultadoDto = ConverterDeStringParaDto(resultadoDeUrlParaString.Dados);

            return resultadoDto;
        }
    }
}
