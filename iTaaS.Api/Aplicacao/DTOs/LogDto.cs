using iTaaS.Api.Aplicacao.Interfaces.DTOs;
using iTaaS.Api.Dominio.Helpers;
using System;
using System.Collections.Generic;

namespace iTaaS.Api.Aplicacao.DTOs
{
    /// <summary>
    /// Classe responsável por representar o cabeçalho do Log e armazenar as linhas que compõem o corpo.
    /// </summary>
    public class LogDto : ILogDto
    {
        public LogDto()
        {
            this.Linhas = new List<LogLinhaDto>();
        }

        /// <summary>
        /// Identificador do Log.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Hash do Log.
        /// </summary>
        public string Hash { get; set; }

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
        public ICollection<LogLinhaDto> Linhas { get; set; }


        /// <summary>
        /// Obtém o nome do agrupando ou não o sufixo.       
        /// </summary>
        /// <param name="sufixo">Sufixo com o nome do tipo de Log.</param>
        /// <returns>0000000001[_SUFIXO].txt</returns>
        public string ObtenhaNomeArquivo(string sufixo = null)
        {
            var nomeArquivo = $"{Id.ToString("D10")}";

            if (!ValidadorHelper.StringEhNulaOuVazia(sufixo))
                return $"{nomeArquivo}_{sufixo}.txt";

            return nomeArquivo;
        }

    }
}
