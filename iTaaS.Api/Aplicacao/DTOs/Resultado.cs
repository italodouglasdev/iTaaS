using Microsoft.AspNetCore.Http.Connections;
using System.Collections.Generic;

namespace iTaaS.Api.Aplicacao.DTOs
{
    public class Resultado<T>
    {
        public Resultado()
        {           
            Inconsistencias = new List<Inconsistencia>();
        }

        public T Dados { get; set; }
        public List<Inconsistencia> Inconsistencias { get; set; }

        public bool Sucesso => Inconsistencias.Count == 0;

        public void AdicionarInconsistencia(string codigo, string mensagem)
        {
            Inconsistencias.Add(new Inconsistencia(codigo, mensagem));
        }
    }
}
