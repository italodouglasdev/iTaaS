using iTaaS.Api.Dominio.Entidades;
using System;
using System.Collections.Generic;

namespace iTaaS.Api.Aplicacao.Interfaces.Entidades
{
    public interface ILog : IEntidadesBase
    {
        string Hash { get; set; }

        DateTime DataHoraRecebimento { get; set; }

        string UrlOrigem { get; set; }

        List<LogLinhaEntidade> Linhas { get; set; }

    }
}
