using System;

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


        /// <summary>
        /// Verifica se a data é válida
        /// </summary>
        /// <param name="data">Data a ser verificada.</param>
        /// <returns>Retorna <c>true</c> se a data form válida, caso contrário, retorna <c>false</c>.</returns>
        public static bool DataEhValida(DateTime data)
        {
            if (data.Year < 2000)
                return false;

            return false;
        }
    }
}
