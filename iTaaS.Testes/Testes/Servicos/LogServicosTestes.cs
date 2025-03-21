﻿using iTaaS.Api.Aplicacao.Mapeadores;
using iTaaS.Api.Aplicacao.Servicos;
using iTaaS.Api.Dominio.Enumeradores;
using iTaaS.Api.Infraestrutura.BancoDeDados;
using iTaaS.Api.Infraestrutura.Repositorios;
using iTaaS.Testes.Mocks.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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
        public async Task ObterPorId()
        {
            using (var context = new EntityContext(this.DbContextOptions))
            {
                this.PoupularBancoDadosVirtual(context);

                var repositorioMock = new LogRepositorio(context);
                var mapperLogLinha = new LogLinhaMapeador();
                var mapper = new LogMapeador(mapperLogLinha);
                var httpContext = new HttpContextoServicoMock();
                var servico = new LogServico(repositorioMock, mapper, httpContext);

                var resultado = await servico.ObterPorId(1);

                Assert.True(resultado.Sucesso);
            }

        }

        [Fact]
        public async Task ObterLista()
        {
            using (var context = new EntityContext(this.DbContextOptions))
            {
                this.PoupularBancoDadosVirtual(context);

                var repositorioMock = new LogRepositorio(context);
                var mapperLogLinha = new LogLinhaMapeador();
                var mapper = new LogMapeador(mapperLogLinha);
                var httpContext = new HttpContextoServicoMock();
                var servico = new LogServico(repositorioMock, mapper, httpContext);

                var resultado = await servico.ObterLista();

                Assert.True(resultado.Sucesso);
            }
        }

        [Fact]
        public async Task Criar()
        {
            using (var context = new EntityContext(this.DbContextOptions))
            {
                var repositorioMock = new LogRepositorio(context);
                var mapperLogLinha = new LogLinhaMapeador();
                var mapper = new LogMapeador(mapperLogLinha);
                var httpContext = new HttpContextoServicoMock();
                var servico = new LogServico(repositorioMock, mapper, httpContext);

                var logEntidade = LogRepositorioMock.PopularLogEntidade(6);

                var log = mapper.MapearDeEntidadeParaDto(logEntidade);

                var resultado = await servico.Criar(log);

                Assert.True(resultado.Sucesso);
            }

        }

        [Fact]
        public async Task Deletar()
        {

            using (var context = new EntityContext(this.DbContextOptions))
            {
                this.PoupularBancoDadosVirtual(context);

                var repositorioMock = new LogRepositorio(context);
                var mapperLogLinha = new LogLinhaMapeador();
                var mapper = new LogMapeador(mapperLogLinha);
                var httpContext = new HttpContextoServicoMock();
                var servico = new LogServico(repositorioMock, mapper, httpContext);

                var resultado = await servico.Deletar(1);

                Assert.True(resultado.Sucesso);
            }

        }

        [Fact]
        public async Task ImportarPorUrl()
        {
            using (var context = new EntityContext(this.DbContextOptions))
            {
                var repositorioMock = new LogRepositorio(context);
                var mapperLogLinha = new LogLinhaMapeador();
                var mapper = new LogMapeador(mapperLogLinha);
                var httpContext = new HttpContextoServicoMock();
                var servico = new LogServico(repositorioMock, mapper, httpContext);

                var resultado = await servico.ImportarPorUrl("https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-01.txt", Api.Dominio.Enumeradores.TipoRetornoLog.RETORNAR_ARQUIVO);

                Assert.True(resultado.Sucesso);
            }
        }

        [Fact]
        public async Task ImportarPorId()
        {
            using (var context = new EntityContext(this.DbContextOptions))
            {
                this.PoupularBancoDadosVirtual(context);

                var repositorioMock = new LogRepositorio(context);
                var mapperLogLinha = new LogLinhaMapeador();
                var mapper = new LogMapeador(mapperLogLinha);
                var httpContext = new HttpContextoServicoMock();
                var servico = new LogServico(repositorioMock, mapper, httpContext);

                var resultado = await servico.ImportarPorId(1, Api.Dominio.Enumeradores.TipoRetornoLog.RETORNAR_ARQUIVO);

                Assert.True(resultado.Sucesso);
            }
        }

        [Fact]
        public async Task BuscarSalvos()
        {
            using (var context = new EntityContext(this.DbContextOptions))
            {
                this.PoupularBancoDadosVirtual(context);

                var repositorioMock = new LogRepositorio(context);
                var mapperLogLinha = new LogLinhaMapeador();
                var mapper = new LogMapeador(mapperLogLinha);
                var httpContext = new HttpContextoServicoMock();
                var servico = new LogServico(repositorioMock, mapper, httpContext);

                var resultado = await servico.BuscarSalvos(
                    dataHoraRecebimentoInicio: DateTime.Now.AddDays(-1).ToString("yyyyMMddHHmm"),
                    dataHoraRecebimentoFim: DateTime.Now.AddDays(1).ToString("yyyyMMddHHmm"),
                    metodoHttp: "",
                    codigoStatus: 0,
                    caminhoUrl: "",
                    tempoRespostaInicial: 0,
                    tempoRespostaFinal: 0,
                    tamanhoRespostaInicial: 0,
                    tamanhoRespostaFinal: 0,
                    cashStatus: "",
                    TipoRetornoLog.RETORNAR_ARQUIVO
                );

                Assert.True(resultado.Sucesso);
            }
        }

        [Fact]
        public async Task BuscarTransformados()
        {
            using (var context = new EntityContext(this.DbContextOptions))
            {
                this.PoupularBancoDadosVirtual(context);

                var repositorioMock = new LogRepositorio(context);
                var mapperLogLinha = new LogLinhaMapeador();
                var mapper = new LogMapeador(mapperLogLinha);
                var httpContext = new HttpContextoServicoMock();
                var servico = new LogServico(repositorioMock, mapper, httpContext);

                var resultado = await servico.BuscarTransformados(
                    dataHoraRecebimentoInicio: DateTime.Now.AddDays(-1).ToString("yyyyMMddHHmm"),
                    dataHoraRecebimentoFim: DateTime.Now.AddDays(1).ToString("yyyyMMddHHmm"),
                    metodoHttp: "",
                    codigoStatus: 0,
                    caminhoUrl: "",
                    tempoRespostaInicial: 0,
                    tempoRespostaFinal: 0,
                    tamanhoRespostaInicial: 0,
                    tamanhoRespostaFinal: 0,
                    cashStatus: "",
                    TipoRetornoLog.RETORNAR_ARQUIVO
                );

                Assert.True(resultado.Sucesso);
            }
        }

        [Fact]
        public async Task BuscarPorIdentificador()
        {
            using (var context = new EntityContext(this.DbContextOptions))
            {
                this.PoupularBancoDadosVirtual(context);

                var repositorioMock = new LogRepositorio(context);
                var mapperLogLinha = new LogLinhaMapeador();
                var mapper = new LogMapeador(mapperLogLinha);
                var httpContext = new HttpContextoServicoMock();
                var servico = new LogServico(repositorioMock, mapper, httpContext);             

                var resultado = await servico.BuscarPorIdentificador(1, TipoRetornoLog.RETORNAR_ARQUIVO);

                Assert.True(resultado.Sucesso);
            }

        }

        [Fact]
        public async Task BuscarTransformadoPorIdentificador()
        {
            using (var context = new EntityContext(this.DbContextOptions))
            {
                this.PoupularBancoDadosVirtual(context);

                var repositorioMock = new LogRepositorio(context);
                var mapperLogLinha = new LogLinhaMapeador();
                var mapper = new LogMapeador(mapperLogLinha);
                var httpContext = new HttpContextoServicoMock();
                var servico = new LogServico(repositorioMock, mapper, httpContext);

                var resultado = await servico.BuscarTransformadoPorIdentificador(1, TipoRetornoLog.RETORNAR_ARQUIVO);

                Assert.True(resultado.Sucesso);
            }

        }

    }
}
