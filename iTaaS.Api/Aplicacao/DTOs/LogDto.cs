using iTaaS.Api.Dominio.Helpers;
using System;
using System.Collections.Generic;

namespace iTaaS.Api.Aplicacao.DTOs
{
    public class LogDto
    {
        public LogDto()
        {
            Linhas = new List<LogLinhaDto>();
        }

        public int Id { get; set; }
        public string Hash { get; set; }
        public DateTime DataHoraRecebimento { get; set; }
        public ICollection<LogLinhaDto> Linhas { get; set; }


        public string ObtenhaNomeArquivo(string sufixo = null)
        {
            var nomeArquivo = $"{Id.ToString("D10")}";

            if (!UtilitarioHelper.StringEhNulaOuVazia(sufixo))
                return $"{nomeArquivo}_{sufixo}.txt";

            return nomeArquivo;
        }

    }
}
