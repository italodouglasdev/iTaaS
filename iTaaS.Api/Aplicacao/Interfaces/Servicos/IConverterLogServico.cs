using iTaaS.Api.Aplicacao.DTOs;

namespace iTaaS.Api.Aplicacao.Interfaces.Servicos
{
    public interface IConverterLogServico
    {
        Resultado<string> ConverterDeDtoParaString(LogDto logDto);

        Resultado<string> ConverterDeDtoParaArquivo(LogDto logDto);

        Resultado<LogDto> ConverterDeStringParaDto(string logString);

        Resultado<string> ConverterDeStringParaArquivo(string logString, string caminho);

        Resultado<string> ConverterDeArquivoParaString(string caminho);

        Resultado<string> ConverterDeUrlParaArquivo(string url, string caminho);

        Resultado<LogDto> ConverterDeArquivoParaDto(string caminho);

        Resultado<LogDto> ConverterDeUrlParaDto(string url);

    }
}
