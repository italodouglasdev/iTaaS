using iTaaS.Api.Dominio.Entidades;

namespace iTaaS.Api.Aplicacao.Interfaces.Entidades
{
    public interface ILogLinha : IEntidadesBase
    {
     
        int LogId { get; set; }

        string MetodoHttp { get; set; }

        int CodigoStatus { get; set; }

        string CaminhoUrl { get; set; }

        decimal TempoResposta { get; set; }

        int TamahoResposta { get; set; }

        string CacheStatus { get; set; }

             
        LogEntidade Log { get; set; }
    }
}
