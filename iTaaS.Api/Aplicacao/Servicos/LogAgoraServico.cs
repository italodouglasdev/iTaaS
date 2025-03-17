using iTaaS.Api.Aplicacao.DTOs;
using iTaaS.Api.Aplicacao.DTOs.Auxiliares;
using iTaaS.Api.Aplicacao.Interfaces.Servicos;
using iTaaS.Api.Dominio.Helpers;
using System;
using System.Text;

namespace iTaaS.Api.Aplicacao.Servicos
{
    public class LogAgoraServico : ILogTipoFormatoServico
    {

        private const string END_POINT_BUSCAR_TRANSFORMADO_ID = "Api/Log/BuscarTransformadoId";

        private const string PROVEDOR = "MINHA CDN";

        private const string SUFIXO_TIPO_LOG_AGORA = "LOG_AGORA";


        public Resultado<string> ConverterDeArquivoParaString(string caminho)
        {
            var resultadoString = new Resultado<string>();

            var consultaLerArquivo = SistemaArquivosHelper.LerArquivoTxt(caminho);
            if (!consultaLerArquivo.Sucesso)
            {
                resultadoString.Inconsistencias = consultaLerArquivo.Inconsistencias;
                return resultadoString;
            }

            var consultaListaParaString = ConversorHelper.ConverterDeListaParaString(consultaLerArquivo.Dados);
            if (!consultaListaParaString.Sucesso)
                resultadoString.Inconsistencias = consultaListaParaString.Inconsistencias;

            resultadoString.Dados = consultaListaParaString.Dados;

            return resultadoString;
        }

        public Resultado<string> ConverterDeDtoParaArquivo(string urlBase, LogDto logDto)
        {
            var resultadoArquivo = new Resultado<string>();

            var resultadoDtoString = ConverterDeDtoParaString(logDto);
            if (!resultadoDtoString.Sucesso)
            {
                resultadoArquivo.Inconsistencias = resultadoDtoString.Inconsistencias;
                return resultadoArquivo;
            }

            var consultaArvoreDiretorios = SistemaArquivosHelper.CriarArvoreDiretorios(logDto.DataHoraRecebimento);
            if (!consultaArvoreDiretorios.Sucesso)
            {
                resultadoArquivo.Inconsistencias = consultaArvoreDiretorios.Inconsistencias;
                return resultadoArquivo;
            }

            var caminhoArquivo = $"{consultaArvoreDiretorios.Dados}\\{logDto.ObtenhaNomeArquivo(SUFIXO_TIPO_LOG_AGORA)}";
            var consultaArquivoString = ConverterDeArquivoParaString(caminhoArquivo);
            if (!consultaArquivoString.Sucesso)
            {
                var consultaArquivoTxt = SistemaArquivosHelper.CriarArquivoTxt(caminhoArquivo, resultadoDtoString.Dados);
                if (!consultaArquivoTxt.Sucesso)
                {
                    resultadoArquivo.Inconsistencias = consultaArquivoTxt.Inconsistencias;
                    return resultadoArquivo;
                }
            }

            resultadoArquivo.Dados = $"{urlBase}/{END_POINT_BUSCAR_TRANSFORMADO_ID}/{logDto.Id}";

            return resultadoArquivo;

        }

        public Resultado<string> ConverterDeDtoParaString(LogDto logDto)
        {
            var resultado = new Resultado<string>();

            var strinBuilder = new StringBuilder();

            strinBuilder.AppendLine($"#Version: 1.0");
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
                    resultado.Inconsistencias = consultaArquivoTxt.Inconsistencias;
                    return resultado;
                }

                consultaArquivoString = ConverterDeArquivoParaString(caminhoArquivo);
            }

            if (!consultaArquivoString.Sucesso)
            {
                resultado.Inconsistencias = consultaArquivoString.Inconsistencias;
                return resultado;
            }

            resultado.Dados = consultaArquivoString.Dados;

            return resultado;
        }

        public Resultado<LogDto> ConverterDeStringParaDto(string logString)
        {
            throw new NotImplementedException();
        }

        public Resultado<LogDto> ConverterDeUrlParaDto(string url)
        {
            throw new NotImplementedException();
        }

    }
}
