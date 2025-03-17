namespace iTaaS.Api.Aplicacao.DTOs.Auxiliares
{
    /// <summary>
    /// Classe auxiliar responsável por armazenar as inconsitências
    /// </summary>
    public class Inconsistencia
    {
        public string Codigo { get; set; }
        public string Mensagem { get; set; }

        public Inconsistencia(string codigo, string mensagem)
        {
            Codigo = codigo;
            Mensagem = mensagem;
        }
    }
}
