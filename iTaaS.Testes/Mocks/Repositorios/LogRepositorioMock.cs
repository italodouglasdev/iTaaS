using iTaaS.Api.Aplicacao.DTOs.Auxiliares;
using iTaaS.Api.Aplicacao.Interfaces.Repositorios;
using iTaaS.Api.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iTaaS.Testes.Mocks.Repositorios
{
    public class LogRepositorioMock : ILogRepositorio
    {
        private readonly List<LogEntidade> _logs;

        public LogRepositorioMock()
        {
            _logs = new List<LogEntidade>();
        }

        public Task<Resultado<LogEntidade>> ObterPorId(int id)
        {
            var resultado = new Resultado<LogEntidade>();

            var log = _logs.FirstOrDefault(l => l.Id == id);

            if (log == null)
                resultado.AdicionarInconsistencia("NAO_ENCONTRADO", $"Não foi possível localizar o Log Id {id}!");
            else
                resultado.Dados = log;

            return Task.FromResult(resultado);
        }

        public Task<Resultado<List<LogEntidade>>> ObterLista()
        {
            var resultado = new Resultado<List<LogEntidade>>
            {
                Dados = _logs
            };

            if (!_logs.Any())
                resultado.AdicionarInconsistencia("SEM_REGISTROS", "Nenhum log foi localizado!");

            return Task.FromResult(resultado);
        }

        public Task<Resultado<LogEntidade>> Criar(LogEntidade entity)
        {
            var resultado = new Resultado<LogEntidade>();

            if (entity == null)
            {
                resultado.AdicionarInconsistencia("ENTIDADE_NULA", "O Log informado não pode ser vazio ou nulo!");
                return Task.FromResult(resultado);
            }

            entity.Id = _logs.Count + 1;
            _logs.Add(entity);
            resultado.Dados = entity;

            return Task.FromResult(resultado);
        }

        public Task<Resultado<LogEntidade>> Atualizar(LogEntidade entity)
        {
            var resultado = new Resultado<LogEntidade>();

            if (entity == null)
            {
                resultado.AdicionarInconsistencia("ENTIDADE_NULA", "O Log informado não pode ser vazio ou nulo!");
                return Task.FromResult(resultado);
            }

            var index = _logs.FindIndex(l => l.Id == entity.Id);
            if (index == -1)
            {
                resultado.AdicionarInconsistencia("NAO_ENCONTRADO", "Log não encontrado para atualização!");
            }
            else
            {
                _logs[index] = entity;
                resultado.Dados = entity;
            }

            return Task.FromResult(resultado);
        }

        public Task<Resultado<LogEntidade>> Deletar(int id)
        {
            var resultado = new Resultado<LogEntidade>();
            var log = _logs.FirstOrDefault(l => l.Id == id);

            if (log == null)
            {
                resultado.AdicionarInconsistencia("NAO_ENCONTRADO", "Log não encontrado para exclusão!");
            }
            else
            {
                _logs.Remove(log);
                resultado.Dados = log;
            }

            return Task.FromResult(resultado);
        }

        public Task<Resultado<List<LogEntidade>>> ObterLogsFiltrados(
            string dataHoraRecebimentoInicio,
            string dataHoraRecebimentoFim,
            string metodoHttp,
            int codigoStatus,
            string caminhoUrl,
            decimal tempoRespostaInicial,
            decimal tempoRespostaFinal,
            int tamanhoRespostaInicial,
            int tamanhoRespostaFinal,
            string cashStatus)
        {
            var resultado = new Resultado<List<LogEntidade>>();
            var query = _logs.AsQueryable();

            if (!string.IsNullOrEmpty(dataHoraRecebimentoInicio))
            {
                var dataInicial = DateTime.Parse(dataHoraRecebimentoInicio);
                query = query.Where(l => l.DataHoraRecebimento >= dataInicial);
            }
            if (!string.IsNullOrEmpty(dataHoraRecebimentoFim))
            {
                var dataFinal = DateTime.Parse(dataHoraRecebimentoFim);
                query = query.Where(l => l.DataHoraRecebimento <= dataFinal);
            }
            if (!string.IsNullOrEmpty(metodoHttp))
                query = query.Where(l => l.Linhas.Any(ll => ll.MetodoHttp == metodoHttp));
            if (codigoStatus > 0)
                query = query.Where(l => l.Linhas.Any(ll => ll.CodigoStatus == codigoStatus));
            if (!string.IsNullOrEmpty(caminhoUrl))
                query = query.Where(l => l.Linhas.Any(ll => ll.CaminhoUrl.Contains(caminhoUrl)));
            if (tempoRespostaInicial > 0)
                query = query.Where(l => l.Linhas.Any(ll => ll.TempoResposta >= tempoRespostaInicial));
            if (tempoRespostaFinal > 0)
                query = query.Where(l => l.Linhas.Any(ll => ll.TempoResposta <= tempoRespostaFinal));
            if (tamanhoRespostaInicial > 0)
                query = query.Where(l => l.Linhas.Any(ll => ll.TamahoResposta >= tamanhoRespostaInicial));
            if (tamanhoRespostaFinal > 0)
                query = query.Where(l => l.Linhas.Any(ll => ll.TamahoResposta <= tamanhoRespostaFinal));
            if (!string.IsNullOrEmpty(cashStatus))
                query = query.Where(l => l.Linhas.Any(ll => ll.CacheStatus == cashStatus));

            var logsFiltrados = query.ToList();
            if (!logsFiltrados.Any())
                resultado.AdicionarInconsistencia("SEM_REGISTROS", "Nenhum log foi encontrado com os filtros informados.");

            resultado.Dados = logsFiltrados;
            return Task.FromResult(resultado);
        }
    }
}
