namespace iTaaS.Api.Aplicacao.Interfaces.DTOs
{
    public interface ILogLinhaDto
    {
        int Id { get; set; }
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
