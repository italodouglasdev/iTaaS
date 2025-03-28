﻿using iTaaS.Api.Aplicacao.DTOs;
using iTaaS.Api.Aplicacao.DTOs.Auxiliares;

namespace iTaaS.Api.Aplicacao.Interfaces.Servicos
{
    public interface ILogTipoFormatoServico
    {
        Resultado<string> ConverterDeDtoParaString(LogDto logDto);

        Resultado<string> ConverterDeDtoParaArquivo(string urlBase, LogDto logDto);

        Resultado<LogDto> ConverterDeStringParaDto(string logString);    

        Resultado<string> ConverterDeArquivoParaString(string caminho);    

        Resultado<LogDto> ConverterDeUrlParaDto(string url);

    }
}
