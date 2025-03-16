using iTaaS.Api.Aplicacao.Interfaces.Entidades;
using System;
using System.Collections.Generic;

namespace iTaaS.Api.Dominio.Entidades
{
    public class LogEntidade : ILog
    {
        public int Id { get; set; }
        public string Hash { get; set; }
        public DateTime DataHoraRecebimento { get; set; }
        public ICollection<LogLinhaEntidade> Linhas { get; set; } = new List<LogLinhaEntidade>();
    }

}
