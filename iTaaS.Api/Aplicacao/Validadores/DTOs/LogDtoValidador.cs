using iTaaS.Api.Aplicacao.DTOs.Auxiliares;
using iTaaS.Api.Aplicacao.Interfaces.DTOs;
using iTaaS.Api.Dominio.Helpers;

namespace iTaaS.Api.Aplicacao.Validadores.DTOs
{
    public class LogDtoValidador
    {
        public static Resultado<string> ValidarCriar(ILogDto dto)
        {
            var resultado = new Resultado<string>();

            resultado = ValidadorGeral(dto);
            if (!resultado.Sucesso)
            {
                resultado.AdicionarInconsistencia("FORMATO_LOG", "O Log deve ser informado!");
                return resultado;
            }

            foreach (var dtoLinha in dto.Linhas)
            {
                var resultadoValidacoLogDtoLinha = LogDtoLinhaValidador.ValidarCriar(dtoLinha);
                if (!resultadoValidacoLogDtoLinha.Sucesso)
                {
                    resultado.AdicionarInconsistencias(resultadoValidacoLogDtoLinha.Inconsistencias);
                    return resultado;
                }
            }

            return resultado;
        }

        public static Resultado<string> ValidarAtualizar(ILogDto dto)
        {
            var resultado = new Resultado<string>();

            resultado = ValidadorGeral(dto);
            if (!resultado.Sucesso)
            {
                resultado.AdicionarInconsistencia("FORMATO_LOG", "O Log deve ser informado!");
                return resultado;
            }

            foreach (var dtoLinha in dto.Linhas)
            {
                var resultadoValidacoLogDtoLinha = LogDtoLinhaValidador.ValidarAtualizar(dtoLinha);
                if (!resultadoValidacoLogDtoLinha.Sucesso)
                {
                    resultado.AdicionarInconsistencias(resultadoValidacoLogDtoLinha.Inconsistencias);
                    return resultado;
                }
            }

            return resultado;
        }

        private static Resultado<string> ValidadorGeral(ILogDto dto)
        {
            var resultado = new Resultado<string>();

            if (dto == null)
            {
                resultado.AdicionarInconsistencia("FORMATO_LOG", "O Log deve ser informado!");
                return resultado;
            }

            if (ValidadorHelper.StringEhNulaOuVazia(dto.Versao))
            {
                resultado.AdicionarInconsistencia("FORMATO_LOG", "O campo Versão deve ser informado!");
                return resultado;
            }

            if (dto.Linhas == null ||
                dto.Linhas.Count == 0)
            {
                resultado.AdicionarInconsistencia("FORMATO_LOG", "A linhas do Log devem ser informadas!");
                return resultado;
            }




            return resultado;
        }

    }
}
