using iTaaS.Api.Aplicacao.DTOs.Auxiliares;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iTaaS.Api.Aplicacao.Interfaces.Repositorios
{
    public interface IRepositorioBase<T> where T : class
    {
        Task<Resultado<T>> Criar(T entity);
        Task<Resultado<T>> Atualizar(T entity);
        Task<Resultado<T>> ObterPorId(int id);
        Task<Resultado<T>> Deletar(int id);
        Task<Resultado<List<T>>> ObterLista();
    }
}
