using iTaaS.Api.Aplicacao.Interfaces.Entidades;
using System.ComponentModel.DataAnnotations.Schema;

namespace iTaaS.Api.Dominio.Entidades
{
    public class LogLinhaEntidade : ILogLinha
    {
        public int Id { get; set; }
        public int LogId { get; set; }      
        public string MetodoHttp { get; set; }
        public int CodigoStatus { get; set; }
        public string CaminhoUrl { get; set; }
        public decimal TempoResposta { get; set; }
        public int TamahoResposta { get; set; }
        public string CacheStatus { get; set; }

        [ForeignKey("LogId")]
        public LogEntidade Log { get; set; }
    }
}
