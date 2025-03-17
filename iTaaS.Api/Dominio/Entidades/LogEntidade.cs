using iTaaS.Api.Aplicacao.Interfaces.Entidades;
using System;
using System.Collections.Generic;

namespace iTaaS.Api.Dominio.Entidades
{
    /// <summary>
    /// Classe responsável por representar o cabeçalho do Log e armazenar as linhas que compõem o corpo.
    /// </summary>
    public class LogEntidade : ILog
    {
        /// <summary>
        /// Identificador do Log.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Hash do Log.
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// Versao do Log.
        /// </summary>
        public string Versao { get; set; }

        /// <summary>
        /// Data e Hora do recebimento do Log.
        /// </summary>
        public DateTime DataHoraRecebimento { get; set; }

        /// <summary>
        /// Url Original do Log.
        /// </summary>
        public string UrlOrigem { get; set; }

        /// <summary>
        /// Lista de Linhas que compoem o corpo o Log.
        /// </summary>
        public List<LogLinhaEntidade> Linhas { get; set; } = new List<LogLinhaEntidade>();

    }

}
