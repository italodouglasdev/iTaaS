using iTaaS.Api.Dominio.Entidades;
using iTaaS.Api.Dominio.Helpers;
using iTaaS.Testes.Mocks.Repositorios;
using System;
using Xunit;

namespace iTaaS.Testes.Testes.Helpers
{
    public class JsonHelperTestes
    {
     
        [Fact]
        public void Serializar()
        {            
            var objeto = new { Nome = "Teste", Valor = 123 };           
            var resultado = JsonHelper.Serializar(objeto);          
            Assert.True(resultado.Sucesso);         
        }              

        [Fact]
        public void Deserializar()
        {
            var LogEntidade = LogRepositorioMock.PopularLogEntidade(1);
            var resultadoJson = JsonHelper.Serializar(LogEntidade);
            var resultado = JsonHelper.Deserializar<LogEntidade>(resultadoJson.Dados);     
            Assert.True(resultado.Sucesso);       
        }

       
    }
}
