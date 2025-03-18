using iTaaS.Api.Infraestrutura.BancoDeDados;
using iTaaS.Api.Infraestrutura.Repositorios;
using iTaaS.Testes.Mocks.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace iTaaS.Testes.Testes.Repositorios
{
    public class LogLinhaRepositorioTestes
    {
        private readonly DbContextOptions<EntityContext> DbContextOptions;

        public LogLinhaRepositorioTestes()
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

                var repositorio = new LogLinhaRepositorio(context);

                var resultado = await repositorio.ObterPorId(idLog);

                Assert.True(resultado.Sucesso);
            }

        }

        [Fact]
        public async Task Criar()
        {
            using (var context = new EntityContext(this.DbContextOptions))
            {
                var log = LogLinhaRepositorioMock.PopularLogLinhaEntidade(1);

                var repositorio = new LogLinhaRepositorio(context);
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

                var log = context.LogsLinhas.FirstOrDefault();
                log.CaminhoUrl = $"https://teste.com";
                await context.SaveChangesAsync();

                var repositorio = new LogLinhaRepositorio(context);
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

                var repositorio = new LogLinhaRepositorio(context);

                var resultado = await repositorio.Deletar(1);

                Assert.True(resultado.Sucesso);

            }
        }

    }
}
