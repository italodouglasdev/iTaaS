using iTaaS.Api.Aplicacao.DTOs.Auxiliares;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace iTaaS.Api.Dominio.Helpers
{
    public class JsonHelper
    {

        public static Resultado<string> Serializar(object objeto)
        {
            var resultado = new Resultado<string>();

            try
            {
                resultado.Dados = JsonConvert.SerializeObject(objeto, Formatting.Indented);
            }
            catch
            {
                resultado.AdicionarInconsistencia("ERRRO_JSON", "Não foi possível serializar!");
            }

            return resultado;

        }

        public static Resultado<T> Serializar<T>(string texto)
        {
            var resultado = new Resultado<T>();

            try
            {
                resultado.Dados = JsonConvert.DeserializeObject<T>(texto);
            }
            catch
            {
                resultado.AdicionarInconsistencia("ERRRO_JSON", "Não foi possível deserializar!");
            }

            return resultado;

        }



    }
}
