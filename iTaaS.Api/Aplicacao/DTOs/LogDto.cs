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


        public string ObtenhaNomeArquivo(string sufixo)
        {
            return $"{Id.ToString("D10")}_{sufixo}.txt";
        }

    }
}
