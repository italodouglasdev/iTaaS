namespace iTaaS.Api.Aplicacao.DTOs.Auxiliares
{

    /// <summary>
    /// Classe auxiliar responsável por agrupar os tipos de Log para retorno da API
    /// </summary>
    public class LogJson
    {
        public int Id { get; set; }
        public string LogMinhaCdn { get; set; }
        public string LogAgora { get; set; }

    }
}
