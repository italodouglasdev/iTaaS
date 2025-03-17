using iTaaS.Api.Aplicacao.DTOs.Auxiliares;
using iTaaS.Api.Aplicacao.Interfaces.DTOs;
using iTaaS.Api.Dominio.Helpers;

namespace iTaaS.Api.Aplicacao.Validadores.DTOs
{
    public class LogDtoLinhaValidador
    {
        public static Resultado<string> ValidarCriar(ILogLinhaDto dto)
        {
            var resultado = new Resultado<string>();

            resultado = ValidadorGeral(dto);
            if (!resultado.Sucesso)
            {
                resultado.AdicionarInconsistencia("FORMATO_LOG", "A Linha informada é inválida!");
                return resultado;
            }           

            return resultado;
        }

        public static Resultado<string> ValidarAtualizar(ILogLinhaDto dto)
        {
            var resultado = new Resultado<string>();

            resultado = ValidadorGeral(dto);
            if (!resultado.Sucesso)
            {
                resultado.AdicionarInconsistencia("FORMATO_LOG", "O Log deve ser informado!");
                return resultado;
            }

            return resultado;
        }

        private static Resultado<string> ValidadorGeral(ILogLinhaDto dto)
        {
            var resultado = new Resultado<string>();

            if (dto == null)
            {
                resultado.AdicionarInconsistencia("FORMATO_LOG", "O Log deve ser informado!");
                return resultado;
            }          

            if (ValidadorHelper.StringEhNulaOuVazia(dto.MetodoHttp))
            {
                resultado.AdicionarInconsistencia("FORMATO_LOG", "O campo Método HTTP deve ser informado!");
                return resultado;
            }

            if (ValidadorHelper.StringEhNulaOuVazia(dto.CaminhoUrl))
            {
                resultado.AdicionarInconsistencia("FORMATO_LOG", "O campo Caminho da URL deve ser informado!");
                return resultado;
            }

            if (dto.CodigoStatus == 0)
            {
                resultado.AdicionarInconsistencia("FORMATO_LOG", "O campo Código de Status deve ser informado!");
                return resultado;
            }

            if (dto.TempoResposta <= 0)
            {
                resultado.AdicionarInconsistencia("FORMATO_LOG", "O campo Tempo de Resposta deve ser informado!");
                return resultado;
            }

            if (dto.TamahoResposta <= 0)
            {
                resultado.AdicionarInconsistencia("FORMATO_LOG", "O campo Tamanho da Resposta deve ser informado!");
                return resultado;
            }

            if (ValidadorHelper.StringEhNulaOuVazia(dto.CacheStatus))
            {
                resultado.AdicionarInconsistencia("FORMATO_LOG", "O campo Status do Cache deve ser informado!");
                return resultado;
            }

            return resultado;
        }

    }
}
