using iTaaS.Api.Aplicacao.DTOs;
using iTaaS.Api.Dominio.Entidades;
using System.Collections.Generic;

namespace iTaaS.Testes.Mocks.Repositorios
{
    public class LogLinhaRepositorioMock
    {

        public static List<LogLinhaEntidade> PopularLogsLinhas()
        {
            return new List<LogLinhaEntidade>
            {
                new LogLinhaEntidade { Id = 1, LogId = 1, MetodoHttp = "GET", CodigoStatus = 200, CaminhoUrl = "/robots.txt", TempoResposta = 100.20M, TamahoResposta = 312, CacheStatus = "HIT" },
                new LogLinhaEntidade { Id = 2, LogId = 1, MetodoHttp = "POST", CodigoStatus = 200, CaminhoUrl = "/myImages", TempoResposta = 319.40M, TamahoResposta = 101, CacheStatus = "MISS" },
                new LogLinhaEntidade { Id = 3, LogId = 1, MetodoHttp = "GET", CodigoStatus = 404, CaminhoUrl = "/not-found", TempoResposta = 142.90M, TamahoResposta = 199, CacheStatus = "MISS" },
                new LogLinhaEntidade { Id = 4, LogId = 1, MetodoHttp = "GET", CodigoStatus = 200, CaminhoUrl = "/robots.txt", TempoResposta = 245.10M, TamahoResposta = 312, CacheStatus = "INVALIDATE" },
                new LogLinhaEntidade { Id = 5, LogId = 2, MetodoHttp = "GET", CodigoStatus = 200, CaminhoUrl = "/robots.txt", TempoResposta = 100.20M, TamahoResposta = 312, CacheStatus = "HIT" },
                new LogLinhaEntidade { Id = 6, LogId = 2, MetodoHttp = "POST", CodigoStatus = 200, CaminhoUrl = "/myImages", TempoResposta = 319.40M, TamahoResposta = 101, CacheStatus = "MISS" },
                new LogLinhaEntidade { Id = 7, LogId = 2, MetodoHttp = "GET", CodigoStatus = 404, CaminhoUrl = "/not-found", TempoResposta = 142.90M, TamahoResposta = 199, CacheStatus = "MISS" },
                new LogLinhaEntidade { Id = 8, LogId = 2, MetodoHttp = "GET", CodigoStatus = 200, CaminhoUrl = "/robots.txt", TempoResposta = 245.10M, TamahoResposta = 312, CacheStatus = "INVALIDATE" },
                new LogLinhaEntidade { Id = 9, LogId = 3, MetodoHttp = "GET", CodigoStatus = 200, CaminhoUrl = "/robots.txt", TempoResposta = 100.20M, TamahoResposta = 312, CacheStatus = "HIT" },
                new LogLinhaEntidade { Id = 10, LogId = 3, MetodoHttp = "POST", CodigoStatus = 200, CaminhoUrl = "/myImages", TempoResposta = 319.40M, TamahoResposta = 101, CacheStatus = "MISS" },
                new LogLinhaEntidade { Id = 11, LogId = 3, MetodoHttp = "GET", CodigoStatus = 404, CaminhoUrl = "/not-found", TempoResposta = 142.90M, TamahoResposta = 199, CacheStatus = "MISS" },
                new LogLinhaEntidade { Id = 12, LogId = 3, MetodoHttp = "GET", CodigoStatus = 200, CaminhoUrl = "/robots.txt", TempoResposta = 245.10M, TamahoResposta = 312, CacheStatus = "INVALIDATE" },
                new LogLinhaEntidade { Id = 13, LogId = 4, MetodoHttp = "GET", CodigoStatus = 200, CaminhoUrl = "/robots.txt", TempoResposta = 100.20M, TamahoResposta = 312, CacheStatus = "HIT" },
                new LogLinhaEntidade { Id = 14, LogId = 4, MetodoHttp = "POST", CodigoStatus = 200, CaminhoUrl = "/myImages", TempoResposta = 319.40M, TamahoResposta = 101, CacheStatus = "MISS" },
                new LogLinhaEntidade { Id = 15, LogId = 4, MetodoHttp = "GET", CodigoStatus = 404, CaminhoUrl = "/not-found", TempoResposta = 142.90M, TamahoResposta = 199, CacheStatus = "MISS" },
                new LogLinhaEntidade { Id = 16, LogId = 4, MetodoHttp = "GET", CodigoStatus = 200, CaminhoUrl = "/robots.txt", TempoResposta = 245.10M, TamahoResposta = 312, CacheStatus = "INVALIDATE" }
            };
        }


        public static LogLinhaEntidade PopularLogLinhaEntidade(int Id)
        {
            return new LogLinhaEntidade { Id = Id, LogId = 1, MetodoHttp = "GET", CodigoStatus = 200, CaminhoUrl = "/robots.txt", TempoResposta = 100.20M, TamahoResposta = 312, CacheStatus = "HIT" };

        }


    }
}
