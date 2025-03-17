namespace iTaaS.Api.Dominio.Helpers
{
    /// <summary>
    /// Classe auxiliar responsável por realizar validações diversas.
    /// </summary>
    public class ValidadorHelper
    {
        /// <summary>
        /// Verifica se uma string é nula ou vazia.
        /// </summary>
        /// <param name="texto">Texto a ser verificado.</param>
        /// <returns>Retorna <c>true</c> se a string for nula ou vazia, caso contrário, retorna <c>false</c>.</returns>     
        public static bool StringEhNulaOuVazia(string texto)
        {
            return string.IsNullOrEmpty(texto) ? true : false;
        }
    }
}
