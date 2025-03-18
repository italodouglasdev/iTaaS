using iTaaS.Api.Dominio.Helpers;
using System;
using System.Collections.Generic;
using Xunit;

namespace iTaaS.Testes.Testes.Helpers
{
    public class ConversorHelperTestes
    {
        [Fact]
        public void ConverterDeListaParaString()
        {
            var lista = new List<string> { "Linha1", "Linha2", "Linha3" };
            var resultado = ConversorHelper.ConverterDeListaParaString(lista);
            Assert.True(resultado.Sucesso);

        }

        [Fact]
        public void ConverterStringEmListaPorDelimitador()
        {
            var texto = "Linha1,Linha2,Linha3";
            var delimitador = ",";
            var resultado = ConversorHelper.ConverterStringEmListaPorDelimitador(texto, delimitador);
            Assert.True(resultado.Sucesso);
        }

        [Fact]
        public void ConverterStringParaInt()
        {
            var texto = "123";
            var resultado = ConversorHelper.ConverterStringParaInt(texto);
            Assert.Equal(123, resultado);
        }

        [Fact]
        public void ConverterStringParaDecimal()
        {
            var texto = "123.45";
            var resultado = ConversorHelper.ConverterStringParaDecimal(texto);
            Assert.Equal(123.45M, resultado);
        }

        [Fact]
        public void ConverterDecimalParaInt()
        {
            var valor = 123.99M;
            var resultado = ConversorHelper.ConverterDecimalParaInt(valor);
            Assert.Equal(123, resultado);
        }

        [Fact]
        public void ConverterStringParaDataTime()
        {
            var data = "202310151230";
            var resultado = ConversorHelper.ConverterStringParaDataTime(data);
            Assert.Equal(new DateTime(2023, 10, 15, 12, 30, 0), resultado);
        }
    }
}
