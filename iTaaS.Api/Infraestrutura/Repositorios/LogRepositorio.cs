using iTaaS.Api.Aplicacao.DTOs;
using iTaaS.Api.Aplicacao.Interfaces.Repositorios;
using iTaaS.Api.Dominio.Entidades;
using iTaaS.Api.Dominio.Helpers;
using iTaaS.Api.Infraestrutura.BancoDeDados;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iTaaS.Api.Infraestrutura.Repositorios
{
    public class LogRepositorio : ILogRepositorio
    {
        private readonly Context context;

        public LogRepositorio(Context context)
        {
            this.context = context;
        }

        public async Task<Resultado<LogEntidade>> ObterPorId(int id)
        {
            var resultado = new Resultado<LogEntidade>();

            var log = await context.Logs
                .Include(l => l.Linhas)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (log == null)
                resultado.AdicionarInconsistencia("NAO_ENCONTRADO", $"Não foi possível localizar o Log Id {id}!");

            resultado.Dados = log;

            return resultado;
        }

        public async Task<Resultado<List<LogEntidade>>> ObterLista()
        {
            var resultado = new Resultado<List<LogEntidade>>();

            var logs = await context.Logs.ToListAsync();

            if (logs.Count == 0)
                resultado.AdicionarInconsistencia("SEM_REGISTROS", "Nenhum log foi localizado!");

            return resultado;
        }


        public async Task<Resultado<List<LogEntidade>>> ObterLogsFiltrados(
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

            var query = context.Logs.Include(l => l.Linhas).AsQueryable();


            //Filtros de Data

            if (!UtilitarioHelper.StringEhNulaOuVazia(dataHoraRecebimentoInicio))
            {
                var dataInicial = UtilitarioHelper.ConverterStringParaDataTime(dataHoraRecebimentoInicio);
                query = query.Where(l => l.DataHoraRecebimento >= dataInicial);
            }
            if (!UtilitarioHelper.StringEhNulaOuVazia(dataHoraRecebimentoFim))
            {
                var dataFinal = UtilitarioHelper.ConverterStringParaDataTime(dataHoraRecebimentoFim);
                query = query.Where(l => l.DataHoraRecebimento <= dataFinal);
            }
         

            // Filtros nos LogsLinhas
            if (!UtilitarioHelper.StringEhNulaOuVazia(metodoHttp))
                query = query.Where(l => l.Linhas.Any(ll => ll.MetodoHttp == metodoHttp));

            if (codigoStatus > 0)
                query = query.Where(l => l.Linhas.Any(ll => ll.CodigoStatus == codigoStatus));

            if (!UtilitarioHelper.StringEhNulaOuVazia(caminhoUrl))
                query = query.Where(l => l.Linhas.Any(ll => ll.CaminhoUrl.Contains(caminhoUrl)));

            if (tempoRespostaInicial > 0)
                query = query.Where(l => l.Linhas.Any(ll => ll.TempoResposta >= tempoRespostaInicial));

            if (tempoRespostaFinal > 0)
                query = query.Where(l => l.Linhas.Any(ll => ll.TempoResposta <= tempoRespostaFinal));

            if (tamanhoRespostaInicial > 0)
                query = query.Where(l => l.Linhas.Any(ll => ll.TamahoResposta >= tamanhoRespostaInicial));

            if (tamanhoRespostaFinal > 0)
                query = query.Where(l => l.Linhas.Any(ll => ll.TamahoResposta <= tamanhoRespostaFinal));

            if (!UtilitarioHelper.StringEhNulaOuVazia(cashStatus))
                query = query.Where(l => l.Linhas.Any(ll => ll.CacheStatus == cashStatus));


            var logs = await query.ToListAsync();

            if (!logs.Any())
                resultado.AdicionarInconsistencia("SEM_REGISTROS", "Nenhum log foi encontrado com os filtros informados.");

            resultado.Dados = logs;

            return resultado;
        }


        public async Task<Resultado<LogEntidade>> Criar(LogEntidade entity)
        {
            var resultado = new Resultado<LogEntidade>();

            if (entity == null)
            {
                resultado.AdicionarInconsistencia("ENTIDADE_NULA", "O Log informado não pode ser vazio ou nulo!");
                return resultado;
            }

            try
            {
                await context.Logs.AddAsync(entity);
                await context.SaveChangesAsync();
                resultado.Dados = entity;
            }
            catch (DbUpdateException ex)
            {
                resultado.AdicionarInconsistencia("ERRO_BANCO", "Não foi possível Criar o Log.");
            }
            catch (Exception ex)
            {
                resultado.AdicionarInconsistencia("ERRO_DESCONHECIDO", "Não foi possível Criar o Log.");
            }

            return resultado;
        }

        public async Task<Resultado<LogEntidade>> Atualizar(LogEntidade entity)
        {
            var resultado = new Resultado<LogEntidade>();

            if (entity == null)
            {
                resultado.AdicionarInconsistencia("ENTIDADE_NULA", "O Log informado não pode ser vazio ou nulo!");
                return resultado;
            }

            try
            {
                context.Logs.Update(entity);
                await context.SaveChangesAsync();
                resultado.Dados = entity;
            }
            catch (DbUpdateException ex)
            {
                resultado.AdicionarInconsistencia("ERRO_BANCO", "Erro ao atualizar no banco de dados!");
            }
            catch (Exception ex)
            {
                resultado.AdicionarInconsistencia("ERRO_DESCONHECIDO", "Ocorreu um erro inesperado!");
            }

            return resultado;
        }

        public async Task<Resultado<LogEntidade>> Deletar(int id)
        {
            var resultado = new Resultado<LogEntidade>();

            var resultadoBusca = await ObterPorId(id);
            if (!resultadoBusca.Sucesso)
                return resultadoBusca;


            try
            {
                var entity = resultadoBusca.Dados;
                context.Logs.Remove(entity);
                await context.SaveChangesAsync();
                resultado.Dados = entity;
            }
            catch (DbUpdateException ex)
            {
                resultado.AdicionarInconsistencia("ERRO_BANCO", "Erro ao excluir do banco de dados!");
            }
            catch (Exception ex)
            {
                resultado.AdicionarInconsistencia("ERRO_DESCONHECIDO", "Ocorreu um erro inesperado!");
            }

            return resultado;
        }

    }
}
