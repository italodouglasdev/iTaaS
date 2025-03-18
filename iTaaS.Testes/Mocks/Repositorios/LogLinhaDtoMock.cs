using iTaaS.Api.Aplicacao.DTOs;
using System.Collections.Generic;

namespace iTaaS.Testes.Mocks.Repositorios
{
    public class LogLinhaDtoMock
    {
        public static List<LogLinhaDto> PopularLogsLinhas()
        {
            return new List<LogLinhaDto>
            {
                new LogLinhaDto { Id = 1, LogId = 1, MetodoHttp = "GET", CodigoStatus = 200, CaminhoUrl = "/robots.txt", TempoResposta = 100.20M, TamahoResposta = 312, CacheStatus = "HIT" },
                new LogLinhaDto { Id = 2, LogId = 1, MetodoHttp = "POST", CodigoStatus = 200, CaminhoUrl = "/myImages", TempoResposta = 319.40M, TamahoResposta = 101, CacheStatus = "MISS" },
                new LogLinhaDto { Id = 3, LogId = 1, MetodoHttp = "GET", CodigoStatus = 404, CaminhoUrl = "/not-found", TempoResposta = 142.90M, TamahoResposta = 199, CacheStatus = "MISS" },
                new LogLinhaDto { Id = 4, LogId = 1, MetodoHttp = "GET", CodigoStatus = 200, CaminhoUrl = "/robots.txt", TempoResposta = 245.10M, TamahoResposta = 312, CacheStatus = "INVALIDATE" },
                new LogLinhaDto { Id = 5, LogId = 2, MetodoHttp = "GET", CodigoStatus = 200, CaminhoUrl = "/robots.txt", TempoResposta = 100.20M, TamahoResposta = 312, CacheStatus = "HIT" },
                new LogLinhaDto { Id = 6, LogId = 2, MetodoHttp = "POST", CodigoStatus = 200, CaminhoUrl = "/myImages", TempoResposta = 319.40M, TamahoResposta = 101, CacheStatus = "MISS" },
                new LogLinhaDto { Id = 7, LogId = 2, MetodoHttp = "GET", CodigoStatus = 404, CaminhoUrl = "/not-found", TempoResposta = 142.90M, TamahoResposta = 199, CacheStatus = "MISS" },
                new LogLinhaDto { Id = 8, LogId = 2, MetodoHttp = "GET", CodigoStatus = 200, CaminhoUrl = "/robots.txt", TempoResposta = 245.10M, TamahoResposta = 312, CacheStatus = "INVALIDATE" },
                new LogLinhaDto { Id = 9, LogId = 3, MetodoHttp = "GET", CodigoStatus = 200, CaminhoUrl = "/robots.txt", TempoResposta = 100.20M, TamahoResposta = 312, CacheStatus = "HIT" },
                new LogLinhaDto { Id = 10, LogId = 3, MetodoHttp = "POST", CodigoStatus = 200, CaminhoUrl = "/myImages", TempoResposta = 319.40M, TamahoResposta = 101, CacheStatus = "MISS" },
                new LogLinhaDto { Id = 11, LogId = 3, MetodoHttp = "GET", CodigoStatus = 404, CaminhoUrl = "/not-found", TempoResposta = 142.90M, TamahoResposta = 199, CacheStatus = "MISS" },
                new LogLinhaDto { Id = 12, LogId = 3, MetodoHttp = "GET", CodigoStatus = 200, CaminhoUrl = "/robots.txt", TempoResposta = 245.10M, TamahoResposta = 312, CacheStatus = "INVALIDATE" },
                new LogLinhaDto { Id = 13, LogId = 4, MetodoHttp = "GET", CodigoStatus = 200, CaminhoUrl = "/robots.txt", TempoResposta = 100.20M, TamahoResposta = 312, CacheStatus = "HIT" },
                new LogLinhaDto { Id = 14, LogId = 4, MetodoHttp = "POST", CodigoStatus = 200, CaminhoUrl = "/myImages", TempoResposta = 319.40M, TamahoResposta = 101, CacheStatus = "MISS" },
                new LogLinhaDto { Id = 15, LogId = 4, MetodoHttp = "GET", CodigoStatus = 404, CaminhoUrl = "/not-found", TempoResposta = 142.90M, TamahoResposta = 199, CacheStatus = "MISS" },
                new LogLinhaDto { Id = 16, LogId = 4, MetodoHttp = "GET", CodigoStatus = 200, CaminhoUrl = "/robots.txt", TempoResposta = 245.10M, TamahoResposta = 312, CacheStatus = "INVALIDATE" }
            };
        }
    }
}
