namespace iTaaS.Api.Aplicacao.DTOs
{
    public class LogLinhaDto
    {
        public int Id { get; set; }
        public int LogId { get; set; }
        public string Provedor { get; set; }
        public string MetodoHttp { get; set; }
        public int CodigoStatus { get; set; }
        public string CaminhoUrl { get; set; }
        public decimal TempoResposta { get; set; }
        public int TamahoResposta { get; set; }
        public string CacheStatus { get; set; }
    }
}
