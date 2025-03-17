using iTaaS.Api.Aplicacao.DTOs;
using iTaaS.Api.Aplicacao.DTOs.Auxiliares;

namespace iTaaS.Api.Aplicacao.Interfaces.Servicos
{
    public interface ILogTipoServico
    {
        Resultado<string> ConverterDeDtoParaString(LogDto logDto);

        Resultado<string> ConverterDeDtoParaArquivo(string urlBase, LogDto logDto);

        Resultado<LogDto> ConverterDeStringParaDto(string logString);

        Resultado<string> ConverterDeStringParaArquivo(string logString, string caminho);

        Resultado<string> ConverterDeArquivoParaString(string caminho);

        Resultado<string> ConverterDeUrlParaArquivo(string url, string caminho);

        Resultado<LogDto> ConverterDeArquivoParaDto(string caminho);

        Resultado<LogDto> ConverterDeUrlParaDto(string url);

    }
}
