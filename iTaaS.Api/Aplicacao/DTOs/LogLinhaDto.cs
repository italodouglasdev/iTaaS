using iTaaS.Api.Aplicacao.Interfaces.DTOs;

namespace iTaaS.Api.Aplicacao.DTOs
{
    /// <summary>
    /// Classe responsável por representar uma linha do corpo do Log.
    /// </summary>
    public class LogLinhaDto : ILogLinhaDto
    {
        /// <summary>
        /// Identificador da linha do Log.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identificador do cabeçalho do Log.
        /// </summary>
        public int LogId { get; set; }

        /// <summary>
        /// Provedor do Log.
        /// </summary>
        public string Provedor { get; set; }

        /// <summary>
        /// Método HTTP.
        /// </summary>
        public string MetodoHttp { get; set; }

        /// <summary>
        /// Código de Status.
        /// </summary>
        public int CodigoStatus { get; set; }

        /// <summary>
        /// Caminho da url.
        /// </summary>
        public string CaminhoUrl { get; set; }

        /// <summary>
        /// Tempo da Resposta.
        /// </summary>
        public decimal TempoResposta { get; set; }

        /// <summary>
        /// Tamanho da Resposta.
        /// </summary>
        public int TamahoResposta { get; set; }

        /// <summary>
        /// Satatus do Cache.
        /// </summary>
        public string CacheStatus { get; set; }
    }
}
