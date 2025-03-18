using iTaaS.Api.Dominio.Helpers;
using System;
using Xunit;

namespace iTaaS.Testes.Testes.Helpers
{
    public class ValidadorHelperTestes
    {       
        [Fact]
        public void StringEhNulaOuVazia()
        {           
            string texto = null;         
            var resultado = ValidadorHelper.StringEhNulaOuVazia(texto);          
            Assert.True(resultado);
        }  

        [Fact]
        public void DataEhValida()
        {           
            var data = new DateTime(1999, 1, 1);
            var resultado = ValidadorHelper.DataEhValida(data);         
            Assert.False(resultado);
        }

      
    }
}
