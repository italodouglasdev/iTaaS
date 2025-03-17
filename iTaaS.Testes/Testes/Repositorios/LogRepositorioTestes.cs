using iTaaS.Api.Dominio.Entidades;
using iTaaS.Api.Infraestrutura.BancoDeDados;
using iTaaS.Api.Infraestrutura.Repositorios;
using iTaaS.Testes.Mocks.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace iTaaS.Testes.Testes.Repositorios
{
    public class LogRepositorioTestes
    {
        private readonly DbContextOptions<EntityContext> DbContextOptions;

        public LogRepositorioTestes()
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
        public async Task ObterPorId()
        {
            using (var context = new EntityContext(this.DbContextOptions))
            {
                this.PoupularBancoDadosVirtual(context);

                var idLog = 1;

                var repositorio = new LogRepositorio(context);

                var resultado = await repositorio.ObterPorId(idLog);

                Assert.True(resultado.Sucesso);
            }

        }

        [Fact]
        public async Task Criar()
        {
            using (var context = new EntityContext(this.DbContextOptions))
            {               
                var log = LogRepositorioMock.PopularLogEntidade(6);

                var repositorio = new LogRepositorio(context);
                var resultado = await repositorio.Criar(log);

                Assert.True(resultado.Sucesso);
            }
        }

        [Fact]
        public async Task Atualizar()
        {
            using (var context = new EntityContext(this.DbContextOptions))
            {
                this.PoupularBancoDadosVirtual(context);

                var log = context.Logs.FirstOrDefault();
                log.DataHoraRecebimento = DateTime.Now;
                await context.SaveChangesAsync();

                var repositorio = new LogRepositorio(context);
                var resultado = await repositorio.Atualizar(log);

                Assert.True(resultado.Sucesso);
            }
        }

        [Fact]
        public async Task Deletar()
        {
            using (var context = new EntityContext(this.DbContextOptions))
            {
                this.PoupularBancoDadosVirtual(context);

                var repositorio = new LogRepositorio(context);

                var resultado = await repositorio.Deletar(1);

                Assert.True(resultado.Sucesso);

            }
        }



        [Fact]
        public async Task ObterLista()
        {
            using (var context = new EntityContext(this.DbContextOptions))
            {
                this.PoupularBancoDadosVirtual(context);

                var repositorio = new LogRepositorio(context);

                var resultado = await repositorio.ObterLista();         

                Assert.True(resultado.Sucesso);
            }
        }

        [Fact]
        public async Task ObterLogsFiltrados()
        {
            using (var context = new EntityContext(this.DbContextOptions))
            {
                this.PoupularBancoDadosVirtual(context);

                var repositorio = new LogRepositorio(context);               

                var resultado = await repositorio.ObterLogsFiltrados(
                    dataHoraRecebimentoInicio: DateTime.Now.AddDays(-1).ToString("yyyyMMddHHmm"),
                    dataHoraRecebimentoFim: DateTime.Now.AddDays(1).ToString("yyyyMMddHHmm"),
                    metodoHttp: "",
                    codigoStatus: 0,
                    caminhoUrl: "",
                    tempoRespostaInicial: 0,
                    tempoRespostaFinal: 0,
                    tamanhoRespostaInicial: 0,
                    tamanhoRespostaFinal: 0,
                    cashStatus: ""
                );

                Assert.True(resultado.Sucesso);
            }
        }
    }
}
