using iTaaS.Api.Aplicacao.DTOs;
using iTaaS.Api.Aplicacao.DTOs.Auxiliares;
using iTaaS.Api.Aplicacao.Interfaces.Servicos;
using iTaaS.Api.Dominio.Helpers;
using System;
using System.Text;

namespace iTaaS.Api.Aplicacao.Servicos
{

    /// <summary>
    /// Serviço responsável pela conversão de logs no formato "LOG_AGORA".
    /// </summary>
    public class LogTipoFormatoAgoraServico : ILogTipoFormatoServico
    {
        private const string END_POINT_BUSCAR_TRANSFORMADO_ID = "Api/Log/BuscarTransformadoId";

        private const string PROVEDOR = "MINHA CDN";

        private const string SUFIXO_TIPO_LOG_AGORA = "LOG_AGORA";


        /// <summary>
        /// Converte um arquivo de log em uma string.
        /// </summary>
        /// <param name="caminho">Caminho do arquivo de log.</param>
        /// <returns>Resultado contendo a dto <see cref="string"/> encontrada ou uma inconsistência.</returns>
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


        /// <summary>
        /// Converte um DTO de log para um arquivo e retorna a URL do arquivo gerado.
        /// </summary>
        /// <param name="urlBase">URL base para a API de log.</param>
        /// <param name="logDto">Objeto DTO do log.</param>
        /// <returns>Resultado contendo a dto <see cref="string"/> encontrada ou uma inconsistência.</returns>
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

            var caminhoArquivo = $"{consultaArvoreDiretorios.Dados}\\{logDto.ObtenhaNomeArquivo(SUFIXO_TIPO_LOG_AGORA)}";
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

            resultadoArquivo.Dados = $"{urlBase}/{END_POINT_BUSCAR_TRANSFORMADO_ID}/{logDto.Id}";

            return resultadoArquivo;

        }


        /// <summary>
        /// Converte um objeto LogDto para uma string formatada de log.
        /// </summary>
        /// <param name="logDto">Objeto DTO do log.</param>
        /// <returns>Resultado contendo a dto <see cref="string"/> encontrada ou uma inconsistência.</returns>
        public Resultado<string> ConverterDeDtoParaString(LogDto logDto)
        {
            var resultado = new Resultado<string>();

            var strinBuilder = new StringBuilder();

            strinBuilder.AppendLine($"#Version: {logDto.Versao}");
            strinBuilder.AppendLine($"#Date: {logDto.DataHoraRecebimento.ToString("dd/MM/yyyy HH:mm:ss")}");
            strinBuilder.AppendLine($"#Fields: provider http-method status-code uri-path time-taken response-size cache-status");
            strinBuilder.AppendLine($"");

            foreach (var dtoLogLinha in logDto.Linhas)
                strinBuilder.AppendLine($"\"{PROVEDOR}\" {dtoLogLinha.MetodoHttp} {dtoLogLinha.CodigoStatus} {dtoLogLinha.CaminhoUrl} {ConversorHelper.ConverterDecimalParaInt(dtoLogLinha.TempoResposta)} {dtoLogLinha.TamahoResposta} {dtoLogLinha.CacheStatus}");


            var caminhoArquivo = $"{SistemaArquivosHelper.ObtenhaCaminhoDiretorioPorDataHora(logDto.DataHoraRecebimento)}\\{logDto.ObtenhaNomeArquivo(SUFIXO_TIPO_LOG_AGORA)}";
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


        /// <summary>
        /// Converte uma string de log em um objeto LogDto.
        /// </summary>
        /// <param name="logString">String contendo o log.</param>
        /// <returns>Resultado contendo a dto <see cref="LogDto"/> encontrada ou uma inconsistência.</returns>
        public Resultado<LogDto> ConverterDeStringParaDto(string logString)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Obtém um log a partir de uma URL e converte para um objeto LogDto.
        /// </summary>
        /// <param name="url">URL do log.</param>
        /// <returns>Resultado contendo a dto <see cref="LogDto"/> encontrada ou uma inconsistência.</returns>
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
