using System.Collections.Generic;

namespace iTaaS.Api.Aplicacao.DTOs.Auxiliares
{
    /// <summary>
    /// Classe auxiliar responsável por carregar os objetos e inconsistências entre as camadas do projeto.
    /// </summary>
    /// <typeparam name="T">Retorna um Resultado do tipo T informado.</typeparam>
    public class Resultado<T>
    {
        public Resultado()
        {
            this.Inconsistencias = new List<Inconsistencia>();
        }

        public T Dados { get; set; }
        public List<Inconsistencia> Inconsistencias { get; set; }

        public bool Sucesso => Inconsistencias.Count == 0;

        /// <summary>
        /// Adiciona uma nova inconsistência ao objeto Resultado
        /// </summary>
        /// <param name="codigo">Código da Inconsistência</param>
        /// <param name="mensagem">Descrição da Inconsistência</param>
        public void AdicionarInconsistencia(string codigo, string mensagem)
        {
            this.Inconsistencias.Add(new Inconsistencia(codigo, mensagem));
        }

        /// <summary>
        /// Adiciona uma nova inconsistência ou múltiplas inconsistências ao objeto Resultado.
        /// </summary>
        /// <param name="inconsistencias">Lista de inconsistências a serem adicionadas.</param>
        public void AdicionarInconsistencias(IEnumerable<Inconsistencia> inconsistencias)
        {
            this.Inconsistencias.AddRange(inconsistencias);
        }
    }
}
