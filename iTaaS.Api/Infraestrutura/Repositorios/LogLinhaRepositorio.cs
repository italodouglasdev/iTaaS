using iTaaS.Api.Aplicacao.DTOs;
using iTaaS.Api.Aplicacao.Interfaces.Repositorios;
using iTaaS.Api.Dominio.Entidades;
using iTaaS.Api.Infraestrutura.BancoDeDados;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iTaaS.Api.Infraestrutura.Repositorios
{
    public class LogLinhaRepositorio : ILogLinhaRepositorio
    {
        private readonly Context context;

        public LogLinhaRepositorio(Context context)
        {
            this.context = context;
        }

        public async Task<Resultado<LogLinhaEntidade>> ObterPorId(int id)
        {
            var resultado = new Resultado<LogLinhaEntidade>();

            var entity = await context.LogsLinhas.FindAsync(id);

            if (entity == null)            
                resultado.AdicionarInconsistencia("NAO_ENCONTRADO", "Registro não encontrado.");        

            resultado.Dados = entity;

            return resultado;
        }

        public async Task<Resultado<List<LogLinhaEntidade>>> ObterLista()
        {
            var resultado = new Resultado<List<LogLinhaEntidade>>();

            var logsLinhas = await context.LogsLinhas.ToListAsync();

            if (logsLinhas.Count == 0)
                resultado.AdicionarInconsistencia("LISTA_VAZIA", "Nenhum registro encontrado.");

            resultado.Dados = logsLinhas;

            return resultado;
        }

        public async Task<Resultado<LogLinhaEntidade>> Criar(LogLinhaEntidade entity)
        {
            var resultado = new Resultado<LogLinhaEntidade>();

            if (entity == null)
            {
                resultado.AdicionarInconsistencia("ENTIDADE_NULA", "A entidade informada é inválida.");
                return resultado;
            }

            try
            {
                await context.LogsLinhas.AddAsync(entity);
                await context.SaveChangesAsync();
                resultado.Dados = entity;
            }
            catch (DbUpdateException ex)
            {
                resultado.AdicionarInconsistencia("ERRO_BANCO", "Erro ao salvar no banco de dados: " + ex.Message);
            }
            catch (Exception ex)
            {
                resultado.AdicionarInconsistencia("ERRO_DESCONHECIDO", "Ocorreu um erro inesperado: " + ex.Message);
            }

            return resultado;
        }

        public async Task<Resultado<LogLinhaEntidade>> Atualizar(LogLinhaEntidade entity)
        {
            var resultado = new Resultado<LogLinhaEntidade>();

            if (entity == null)
            {
                resultado.AdicionarInconsistencia("ENTIDADE_NULA", "A entidade informada é inválida.");
                return resultado;
            }

            try
            {
                context.LogsLinhas.Update(entity);
                await context.SaveChangesAsync();
                resultado.Dados = entity;
            }
            catch (DbUpdateException ex)
            {
                resultado.AdicionarInconsistencia("ERRO_BANCO", "Erro ao atualizar no banco de dados: " + ex.Message);
            }
            catch (Exception ex)
            {
                resultado.AdicionarInconsistencia("ERRO_DESCONHECIDO", "Ocorreu um erro inesperado: " + ex.Message);
            }

            return resultado;
        }

        public async Task<Resultado<LogLinhaEntidade>> Deletar(int id)
        {
            var resultado = new Resultado<LogLinhaEntidade>();

            var resultadoBusca = await ObterPorId(id);
            if (!resultadoBusca.Sucesso)
            {
                resultado.AdicionarInconsistencia("NAO_ENCONTRADO", "Registro não encontrado.");
                return resultado;
            }

            try
            {
                var entity = resultadoBusca.Dados;
                context.LogsLinhas.Remove(entity);
                await context.SaveChangesAsync();
                resultado.Dados = entity;
            }
            catch (DbUpdateException ex)
            {
                resultado.AdicionarInconsistencia("ERRO_BANCO", "Erro ao excluir do banco de dados: " + ex.Message);
            }
            catch (Exception ex)
            {
                resultado.AdicionarInconsistencia("ERRO_DESCONHECIDO", "Ocorreu um erro inesperado: " + ex.Message);
            }

            return resultado;
        }
    }

}
