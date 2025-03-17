using iTaaS.Api.Aplicacao.Interfaces.Entidades;

namespace iTaaS.Api.Aplicacao.Interfaces.DTOs
{
    public interface ILogLinhaDto : IDtoBase
    {
        int LogId { get; set; }
        string Provedor { get; set; }
        string MetodoHttp { get; set; }
        int CodigoStatus { get; set; }
        string CaminhoUrl { get; set; }
        decimal TempoResposta { get; set; }
        int TamahoResposta { get; set; }
        string CacheStatus { get; set; }
    }
}
