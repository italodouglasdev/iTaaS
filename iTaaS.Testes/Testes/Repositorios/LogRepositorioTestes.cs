using iTaaS.Api.Aplicacao.DTOs.Auxiliares;
using iTaaS.Api.Aplicacao.Interfaces.Repositorios;
using iTaaS.Api.Dominio.Entidades;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace iTaaS.Testes.Testes.Repositorios
{
    public class LogRepositorioTestes
    {
        private readonly Mock<ILogRepositorio> LogRepositorioMock;

        private List<LogEntidade> ListaLogs;

        public LogRepositorioTestes()
        {
            this.LogRepositorioMock = new Mock<ILogRepositorio>();

            this.ObtenhaDados();
        }

        private void ObtenhaDados()
        {
            this.ListaLogs = new List<LogEntidade>
            {
                new LogEntidade { Id = 1, DataHoraRecebimento = DateTime.Now.AddMinutes(-10), Hash = "cb29e621-32c9-4978-aa42-28d471654141", Versao = "1.1", UrlOrigem = "https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-01.txt", Linhas = new List<LogLinhaEntidade>
                {
                    new LogLinhaEntidade { Id = 1, LogId = 1, MetodoHttp = "GET", CodigoStatus = 200, CaminhoUrl = "/robots.txt", TempoResposta = 100.20M, TamahoResposta = 312, CacheStatus = "HIT" },
                    new LogLinhaEntidade { Id = 2, LogId = 1, MetodoHttp = "POST", CodigoStatus = 200, CaminhoUrl = "/myImages", TempoResposta = 319.40M, TamahoResposta = 101, CacheStatus = "MISS" },
                    new LogLinhaEntidade { Id = 3, LogId = 1, MetodoHttp = "GET", CodigoStatus = 404, CaminhoUrl = "/not-found", TempoResposta = 142.90M, TamahoResposta = 199, CacheStatus = "MISS" },
                    new LogLinhaEntidade { Id = 4, LogId = 1, MetodoHttp = "GET", CodigoStatus = 200, CaminhoUrl = "/robots.txt", TempoResposta = 245.10M, TamahoResposta = 312, CacheStatus = "INVALIDATE" }
                }},
                new LogEntidade { Id = 2, DataHoraRecebimento = DateTime.Now.AddMinutes(-20), Hash = "ef200832-919a-4256-b34b-c4b8d17018e0", Versao = "1.1", UrlOrigem = "https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-02.txt", Linhas = new List<LogLinhaEntidade>
                {
                    new LogLinhaEntidade { Id = 5, LogId = 2, MetodoHttp = "GET", CodigoStatus = 200, CaminhoUrl = "/robots.txt", TempoResposta = 100.20M, TamahoResposta = 312, CacheStatus = "HIT" },
                    new LogLinhaEntidade { Id = 6, LogId = 2, MetodoHttp = "POST", CodigoStatus = 200, CaminhoUrl = "/myImages", TempoResposta = 319.40M, TamahoResposta = 101, CacheStatus = "MISS" },
                    new LogLinhaEntidade { Id = 7, LogId = 2, MetodoHttp = "GET", CodigoStatus = 404, CaminhoUrl = "/not-found", TempoResposta = 142.90M, TamahoResposta = 199, CacheStatus = "MISS" },
                    new LogLinhaEntidade { Id = 8, LogId = 2, MetodoHttp = "GET", CodigoStatus = 200, CaminhoUrl = "/robots.txt", TempoResposta = 245.10M, TamahoResposta = 312, CacheStatus = "INVALIDATE" }
                }},
                new LogEntidade { Id = 3, DataHoraRecebimento = DateTime.Now.AddMinutes(-30), Hash = "833dcbd8-ca91-4c85-9e6a-38ca6eb80c28", Versao = "1.1", UrlOrigem = "https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-03.txt", Linhas = new List<LogLinhaEntidade>
                {
                    new LogLinhaEntidade { Id = 9, LogId = 3, MetodoHttp = "GET", CodigoStatus = 200, CaminhoUrl = "/robots.txt", TempoResposta = 100.20M, TamahoResposta = 312, CacheStatus = "HIT" },
                    new LogLinhaEntidade { Id = 10, LogId = 3, MetodoHttp = "POST", CodigoStatus = 200, CaminhoUrl = "/myImages", TempoResposta = 319.40M, TamahoResposta = 101, CacheStatus = "MISS" },
                    new LogLinhaEntidade { Id = 11, LogId = 3, MetodoHttp = "GET", CodigoStatus = 404, CaminhoUrl = "/not-found", TempoResposta = 142.90M, TamahoResposta = 199, CacheStatus = "MISS" },
                    new LogLinhaEntidade { Id = 12, LogId = 3, MetodoHttp = "GET", CodigoStatus = 200, CaminhoUrl = "/robots.txt", TempoResposta = 245.10M, TamahoResposta = 312, CacheStatus = "INVALIDATE" }
                }},
                new LogEntidade { Id = 4, DataHoraRecebimento = DateTime.Now.AddMinutes(-40), Hash = "a1e6dd0e-4477-4499-ac9f-42b8c0087828", Versao = "1.1", UrlOrigem = "https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-04.txt", Linhas = new List<LogLinhaEntidade>
                {
                    new LogLinhaEntidade { Id = 13, LogId = 4, MetodoHttp = "GET", CodigoStatus = 200, CaminhoUrl = "/robots.txt", TempoResposta = 100.20M, TamahoResposta = 312, CacheStatus = "HIT" },
                    new LogLinhaEntidade { Id = 14, LogId = 4, MetodoHttp = "POST", CodigoStatus = 200, CaminhoUrl = "/myImages", TempoResposta = 319.40M, TamahoResposta = 101, CacheStatus = "MISS" },
                    new LogLinhaEntidade { Id = 15, LogId = 4, MetodoHttp = "GET", CodigoStatus = 404, CaminhoUrl = "/not-found", TempoResposta = 142.90M, TamahoResposta = 199, CacheStatus = "MISS" },
                    new LogLinhaEntidade { Id = 16, LogId = 4, MetodoHttp = "GET", CodigoStatus = 200, CaminhoUrl = "/robots.txt", TempoResposta = 245.10M, TamahoResposta = 312, CacheStatus = "INVALIDATE" }
                }},
                new LogEntidade { Id = 5, DataHoraRecebimento = DateTime.Now.AddMinutes(-50), Hash = "8f72be96-9bc3-4e4c-af60-ac796f0c0249", Versao = "1.1", UrlOrigem = "https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-05.txt", Linhas = new List<LogLinhaEntidade>
                {
                    new LogLinhaEntidade { Id = 17, LogId = 5, MetodoHttp = "GET", CodigoStatus = 200, CaminhoUrl = "/robots.txt", TempoResposta = 100.20M, TamahoResposta = 312, CacheStatus = "HIT" },
                    new LogLinhaEntidade { Id = 18, LogId = 5, MetodoHttp = "POST", CodigoStatus = 200, CaminhoUrl = "/myImages", TempoResposta = 319.40M, TamahoResposta = 101, CacheStatus = "MISS" },
                    new LogLinhaEntidade { Id = 19, LogId = 5, MetodoHttp = "GET", CodigoStatus = 404, CaminhoUrl = "/not-found", TempoResposta = 142.90M, TamahoResposta = 199, CacheStatus = "MISS" },
                    new LogLinhaEntidade { Id = 20, LogId = 5, MetodoHttp = "GET", CodigoStatus = 200, CaminhoUrl = "/robots.txt", TempoResposta = 245.10M, TamahoResposta = 312, CacheStatus = "INVALIDATE" }
                }}

            };
         
        }


        [Fact]
        public async Task ObterPorId_DeveRetornarLog_QuandoIdExistente()
        {
            int id = 1;

            this.LogRepositorioMock.Setup(repo => repo.ObterPorId(id)).ReturnsAsync(new Resultado<LogEntidade> { Dados = this.ListaLogs[0] });

            var resultado = await this.LogRepositorioMock.Object.ObterPorId(id);

            Assert.NotNull(resultado.Dados);

            Assert.Equal(id, resultado.Dados.Id);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarInconsistencia_QuandoIdNaoExistente()
        {
            int id = 99;

            var resultado = new Resultado<LogEntidade>();

            resultado.AdicionarInconsistencia("NAO_ENCONTRADO", "Log não encontrado");

            var resultadoEsperado = resultado;
           
            this.LogRepositorioMock.Setup(repo => repo.ObterPorId(id)).ReturnsAsync(resultadoEsperado);
         
            resultado = await this.LogRepositorioMock.Object.ObterPorId(id);
         
            Assert.Null(resultado.Dados);

            Assert.False(resultado.Sucesso);
        }

        [Fact]
        public async Task ObterLista_DeveRetornarLogs_QuandoExistiremLogs()
        {
            this.LogRepositorioMock.Setup(repo => repo.ObterLista()).ReturnsAsync(new Resultado<List<LogEntidade>> { Dados = this.ListaLogs });

            var resultado = await this.LogRepositorioMock.Object.ObterLista();

            Assert.NotEmpty(resultado.Dados);

            Assert.Equal(5, resultado.Dados.Count);
        }

        [Fact]
        public async Task Criar_DeveRetornarLogCriado()
        {
            var novoLog = new LogEntidade { Id = 3, DataHoraRecebimento = DateTime.Now };

            this.LogRepositorioMock.Setup(repo => repo.Criar(novoLog)).ReturnsAsync(new Resultado<LogEntidade> { Dados = novoLog });

            var resultado = await this.LogRepositorioMock.Object.Criar(novoLog);

            Assert.NotNull(resultado.Dados);

            Assert.Equal(3, resultado.Dados.Id);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarLogAtualizado()
        {
            var logAtualizado = this.ListaLogs[0];

            logAtualizado.DataHoraRecebimento = DateTime.Now.AddHours(1);

            this.LogRepositorioMock.Setup(repo => repo.Atualizar(logAtualizado)).ReturnsAsync(new Resultado<LogEntidade> { Dados = logAtualizado });

            var resultado = await this.LogRepositorioMock.Object.Atualizar(logAtualizado);

            Assert.NotNull(resultado.Dados);

            Assert.Equal(logAtualizado.DataHoraRecebimento, resultado.Dados.DataHoraRecebimento);
        }

        [Fact]
        public async Task Deletar_DeveRetornarLogDeletado()
        {
            int id = 1;

            this.LogRepositorioMock.Setup(repo => repo.Deletar(id)).ReturnsAsync(new Resultado<LogEntidade> { Dados = this.ListaLogs[0] });

            var resultado = await this.LogRepositorioMock.Object.Deletar(id);

            Assert.NotNull(resultado.Dados);

            Assert.Equal(id, resultado.Dados.Id);
        }
    }
}
