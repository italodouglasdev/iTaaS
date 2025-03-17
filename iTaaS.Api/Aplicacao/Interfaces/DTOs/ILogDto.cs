using iTaaS.Api.Aplicacao.DTOs;
using iTaaS.Api.Aplicacao.Interfaces.Entidades;
using System;
using System.Collections.Generic;

namespace iTaaS.Api.Aplicacao.Interfaces.DTOs
{
    public interface ILogDto : IDtoBase
    {
      
        string Hash { get; set; }
        DateTime DataHoraRecebimento { get; set; }
        string UrlOrigem { get; set; }
        ICollection<LogLinhaDto> Linhas { get; set; }


        string ObtenhaNomeArquivo(string sufixo = null);

    }
}
