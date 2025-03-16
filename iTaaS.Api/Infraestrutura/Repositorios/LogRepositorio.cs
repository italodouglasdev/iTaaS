using iTaaS.Api.Aplicacao.DTOs;
using iTaaS.Api.Aplicacao.Interfaces.Repositorios;
using iTaaS.Api.Dominio.Entidades;
using iTaaS.Api.Infraestrutura.BancoDeDados;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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



        //public async Task<Resultado<LogEntity>> CriarLogComLinhas(LogEntity entity)
        //{
        //    var resultado = new Resultado<LogEntity>();

        //    var repositorio = new LogRepository(context);

        //    entity.Hash = Guid.NewGuid().ToString();

        //    var resultadoLogCriar = repositorio.Criar(entity);



        //    return resultado;
        //}

    }
}
