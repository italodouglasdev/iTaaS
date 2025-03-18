using iTaaS.Api.Dominio.Entidades;
using System;
using System.Collections.Generic;

namespace iTaaS.Testes.Mocks.Repositorios
{
    public class LogRepositorioMock
    {
        public static List<LogEntidade> PopularLogs()
        {
            return new List<LogEntidade>
            {
            new LogEntidade { Id = 1, DataHoraRecebimento = DateTime.Now.AddDays(-1), Hash = "cb29e621-32c9-4978-aa42-28d471654141", Versao = "1.1", UrlOrigem = "https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-01.txt" },
            new LogEntidade { Id = 2, DataHoraRecebimento = DateTime.Now.AddDays(-2), Hash = "ef200832-919a-4256-b34b-c4b8d17018e0", Versao = "1.1", UrlOrigem = "https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-02.txt" },
            new LogEntidade { Id = 3, DataHoraRecebimento = DateTime.Now.AddDays(-3), Hash = "833dcbd8-ca91-4c85-9e6a-38ca6eb80c28", Versao = "1.1", UrlOrigem = "https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-03.txt" },
            new LogEntidade { Id = 4, DataHoraRecebimento = DateTime.Now.AddDays(-4), Hash = "a1e6dd0e-4477-4499-ac9f-42b8c0087828", Versao = "1.1", UrlOrigem = "https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-04.txt" },
            new LogEntidade { Id = 5, DataHoraRecebimento = DateTime.Now.AddDays(-5), Hash = "8f72be96-9bc3-4e4c-af60-ac796f0c0249", Versao = "1.1", UrlOrigem = "https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-05.txt" }
            };
        }

        public static LogEntidade PopularLogEntidade(int idLog)
        {
            var log = new LogEntidade
            {
                Id = idLog,
                DataHoraRecebimento = DateTime.Now.AddMinutes(-10),
                Hash = "cb29e621-32c9-4978-aa42-28d471654142",
                Versao = "1.1",
                UrlOrigem = "https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-01.txt",
                Linhas = new List<LogLinhaEntidade>
                {
                    new LogLinhaEntidade { MetodoHttp = "GET", CodigoStatus = 200, CaminhoUrl = "/robots.txt", TempoResposta = 100.20M, TamahoResposta = 312, CacheStatus = "HIT" },
                    new LogLinhaEntidade { MetodoHttp = "POST", CodigoStatus = 200, CaminhoUrl = "/myImages", TempoResposta = 319.40M, TamahoResposta = 101, CacheStatus = "MISS" },
                    new LogLinhaEntidade { MetodoHttp = "GET", CodigoStatus = 404, CaminhoUrl = "/not-found", TempoResposta = 142.90M, TamahoResposta = 199, CacheStatus = "MISS" },
                    new LogLinhaEntidade { MetodoHttp = "GET", CodigoStatus = 200, CaminhoUrl = "/robots.txt", TempoResposta = 245.10M, TamahoResposta = 312, CacheStatus = "INVALIDATE" }
                }
            };
          
            foreach (var linha in log.Linhas)
            {
                linha.LogId = log.Id;
            }

            return log;
        }

      
    }
}
