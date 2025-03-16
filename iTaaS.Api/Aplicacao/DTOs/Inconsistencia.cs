namespace iTaaS.Api.Aplicacao.DTOs
{
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
