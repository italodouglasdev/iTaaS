using iTaaS.Api.Aplicacao.DTOs;
using System.Collections.Generic;
using System;

namespace iTaaS.Api.Aplicacao.Interfaces.DTOs
{
    public interface ILogDto
    {
        int Id { get; set; }
        string Hash { get; set; }
        DateTime DataHoraRecebimento { get; set; }
        string UrlOrigem { get; set; }
        ICollection<LogLinhaDto> Linhas { get; set; }

        string ObtenhaNomeArquivo(string sufixo = null);
    }
}
