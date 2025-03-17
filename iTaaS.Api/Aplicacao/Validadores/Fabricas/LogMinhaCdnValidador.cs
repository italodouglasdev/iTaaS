using iTaaS.Api.Aplicacao.DTOs.Auxiliares;
using System.Text.RegularExpressions;

namespace iTaaS.Api.Aplicacao.Validadores.Fabricas
{
    public class LogMinhaCdnValidador
    {

        public static Resultado<bool> ValidarStringLinha(string regex, string stringLinha)
        {
            var resultado = new Resultado<bool>();

            if (string.IsNullOrEmpty(stringLinha))
            {
                resultado.AdicionarInconsistencia("FORMATO_INVALIDO", "Os dados da linha devem ser informados!");
                return resultado;
            }

            if (!Regex.IsMatch(stringLinha, regex))
                resultado.AdicionarInconsistencia("FORMATO_INVALIDO", "O formato dos dados da linha é inválido!");

            return resultado;
        }

    }


}
