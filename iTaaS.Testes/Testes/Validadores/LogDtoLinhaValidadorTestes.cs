﻿using iTaaS.Api.Aplicacao.Validadores.DTOs;
using iTaaS.Testes.Mocks.Repositorios;
using Xunit;

namespace iTaaS.Testes.Testes.Validadores
{
    public class LogDtoLinhaValidadorTestes
    {
        [Fact]
        public void ValidarCriar()
        {
            var dtoLinha = LogLinhaDtoMock.PopularLogLinhaDto(1);
            var resultado = LogDtoLinhaValidador.ValidarCriar(dtoLinha);
            Assert.True(resultado.Sucesso);
        }

        [Fact]
        public void ValidarAtualizar()
        {
            var dto = LogDtoMock.PopularLogDto(1);
            var resultado = LogDtoValidador.ValidarAtualizar(dto);
            Assert.True(resultado.Sucesso);
        }
    }
}
