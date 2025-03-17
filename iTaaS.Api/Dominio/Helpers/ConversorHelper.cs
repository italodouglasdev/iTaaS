using iTaaS.Api.Aplicacao.DTOs.Auxiliares;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace iTaaS.Api.Dominio.Helpers
{
    /// <summary>
    /// Classe auxiliar para conversão de tipos de dados.
    /// </summary>
    public class ConversorHelper
    {
        /// <summary>
        /// Converte uma lista de strings em uma única string, separando os elementos por quebras de linha.
        /// </summary>
        /// <param name="listaString">Lista de strings a ser convertida.</param>
        /// <returns>Resultado contendo a string concatenada ou inconsistências em caso de erro.</returns>
        public static Resultado<string> ConverterDeListaParaString(List<string> listaString)
        {
            var resultado = new Resultado<string>();

            try
            {
                resultado.Dados = string.Join(Environment.NewLine, listaString);
            }
            catch
            {
                resultado.AdicionarInconsistencia("SISTEMA_ARQUIVOS", "Não foi possível converter de lista para string!");
            }

            return resultado;
        }

        /// <summary>
        /// Converte uma string em uma lista de strings usando um delimitador especificado.
        /// </summary>
        /// <param name="textoString">Texto de entrada a ser dividido.</param>
        /// <param name="delimitador">Delimitador usado para separar os elementos.</param>
        /// <returns>Resultado contendo a lista de strings ou inconsistências se a entrada for inválida.</returns>
        public static Resultado<List<string>> ConverterStringEmListaPorDelimitador(string textoString, string delimitador)
        {
            var resultado = new Resultado<List<string>>();

            if (ValidadorHelper.StringEhNulaOuVazia(textoString) ||
                ValidadorHelper.StringEhNulaOuVazia(delimitador))
                return resultado;

            var partes = textoString.Split(delimitador);

            resultado.Dados = new List<string>();

            resultado.Dados.AddRange(partes);

            return resultado;
        }

        /// <summary>
        /// Converte uma string para um número inteiro. Retorna 0 se a conversão falhar.
        /// </summary>
        /// <param name="texto">Texto contendo o número inteiro.</param>
        /// <returns>Valor inteiro convertido ou 0 em caso de erro.</returns>
        public static int ConverterStringParaInt(string texto)
        {
            var valorInt = 0;

            if (ValidadorHelper.StringEhNulaOuVazia(texto))
                return valorInt;

            int.TryParse(texto, out valorInt);

            return valorInt;
        }

        /// <summary>
        /// Converte uma string para um número decimal. Retorna 0 caso a conversão falhe.
        /// </summary>
        /// <param name="texto">Texto contendo o número decimal.</param>
        /// <returns>Valor decimal convertido ou 0 em caso de erro.</returns>
        public static decimal ConverterStringParaDecimal(string texto)
        {
            var valorDecimal = 0M;

            if (ValidadorHelper.StringEhNulaOuVazia(texto))
                return valorDecimal;

            decimal.TryParse(texto, NumberStyles.Any, CultureInfo.InvariantCulture, out valorDecimal);

            return valorDecimal;
        }

        /// <summary>
        /// Converte um número decimal para inteiro, truncando as casas decimais.
        /// </summary>
        /// <param name="valor">Valor decimal a ser convertido.</param>
        /// <returns>Parte inteira do número ou 0 em caso de erro.</returns>
        public static int ConverterDecimalParaInt(decimal valor)
        {
            try
            {
                return (int)Math.Truncate(valor);
            }
            catch
            {

                return 0;
            }

        }

        /// <summary>
        /// Converte uma string para um objeto DateTime usando o formato "yyyyMMddHHmm".
        /// </summary>
        /// <param name="data">String contendo a data no formato esperado.</param>
        /// <returns>Objeto DateTime convertido ou um DateTime padrão (0001-01-01) em caso de falha.</returns>
        public static DateTime ConverterStringParaDataTime(string data)
        {
            string formato = "yyyyMMddHHmm";
            return DateTime.TryParseExact(data, formato, CultureInfo.InvariantCulture, DateTimeStyles.None, out var resultado)
                ? resultado
                : new DateTime();
        }

    }
}
