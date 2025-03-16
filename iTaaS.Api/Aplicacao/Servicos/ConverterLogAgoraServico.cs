using iTaaS.Api.Aplicacao.DTOs;
using iTaaS.Api.Aplicacao.Interfaces.Servicos;
using iTaaS.Api.Dominio.Helpers;
using System;
using System.Text;

namespace iTaaS.Api.Aplicacao.Servicos
{
    public class ConverterLogAgoraServico : IConverterLogServico
    {

        private const string PROVEDOR = "MINHA CDN";

        private const string SUFIXO_TIPO_LOG_AGORA = "LOG_AGORA";


        public Resultado<LogDto> ConverterDeArquivoParaDto(string caminho)
        {
            throw new NotImplementedException();
        }

        public Resultado<string> ConverterDeArquivoParaString(string caminho)
        {
            var resultadoString = new Resultado<string>();


            var consultaLerArquivo = UtilitarioHelper.LerArquivoTxt(caminho);
            if (!consultaLerArquivo.Sucesso)
                resultadoString.Inconsistencias = consultaLerArquivo.Inconsistencias;


            var consultaListaParaString = UtilitarioHelper.ConverterDeListaParaString(consultaLerArquivo.Dados);
            if (!consultaListaParaString.Sucesso)
                resultadoString.Inconsistencias = consultaListaParaString.Inconsistencias;

            resultadoString.Dados = consultaListaParaString.Dados;

            return resultadoString;
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


            var nomeArquivo = logDto.ObtenhaNomeArquivo(SUFIXO_TIPO_LOG_AGORA);
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
                strinBuilder.AppendLine($"\"{PROVEDOR}\" {dtoLogLinha.MetodoHttp} {dtoLogLinha.CodigoStatus} {dtoLogLinha.CaminhoUrl} {UtilitarioHelper.ConverterDecimalParaInt(dtoLogLinha.TempoResposta)} {dtoLogLinha.TamahoResposta} {dtoLogLinha.CacheStatus}");

            resultado.Dados = strinBuilder.ToString();

            return resultado;
        }

        public Resultado<string> ConverterDeStringParaArquivo(string logString, string caminho)
        {
            throw new NotImplementedException();
        }

        public Resultado<LogDto> ConverterDeStringParaDto(string logString)
        {
            throw new NotImplementedException();
        }

        public Resultado<string> ConverterDeUrlParaArquivo(string url, string caminho)
        {
            throw new NotImplementedException();
        }

        public Resultado<LogDto> ConverterDeUrlParaDto(string url)
        {
            throw new NotImplementedException();
        }

    }
}
