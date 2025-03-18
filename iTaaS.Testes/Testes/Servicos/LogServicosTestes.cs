using iTaaS.Api.Aplicacao.DTOs;
using iTaaS.Api.Aplicacao.DTOs.Auxiliares;
using iTaaS.Api.Aplicacao.Interfaces.Entidades;
using iTaaS.Api.Aplicacao.Interfaces.Mapeadores;
using iTaaS.Api.Aplicacao.Interfaces.Repositorios;
using iTaaS.Api.Aplicacao.Interfaces.Servicos;
using iTaaS.Api.Aplicacao.Mapeadores;
using iTaaS.Api.Aplicacao.Servicos;
using iTaaS.Api.Dominio.Entidades;
using iTaaS.Api.Infraestrutura.BancoDeDados;
using iTaaS.Api.Infraestrutura.Repositorios;
using iTaaS.Testes.Mocks.Repositorios;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace iTaaS.Testes.Testes.Servicos
{
    public class LogServicoTestes
    {
        private readonly DbContextOptions<EntityContext> DbContextOptions;

        public LogServicoTestes()
        {
            this.DbContextOptions = new DbContextOptionsBuilder<EntityContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;


        }

        private void PoupularBancoDadosVirtual(EntityContext context)
        {
            context.Logs.AddRange(LogRepositorioMock.PopularLogs());
            context.LogsLinhas.AddRange(LogLinhaRepositorioMock.PopularLogsLinhas());
            context.SaveChanges();
        }


        [Fact]
        public async Task ObterPorId_DeveRetornarLogQuandoExistir()
        {
            using (var context = new EntityContext(this.DbContextOptions))
            {
                this.PoupularBancoDadosVirtual(context);

                var repositorio = new LogRepositorio(context);
                var mapper = new LogMapeador(new LogLinhaMapeador());

                var servico = new LogServico(repositorio, mapper, null);

                var resultado = await servico.ObterPorId(1);

                Assert.True(resultado.Sucesso);
            }           
        
        }

    }
}
