using iTaaS.Api.Aplicacao.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iTaaS.Api.Aplicacao.Interfaces.Servicos
{
    public interface IServicoBase<T> where T : class
    {

        Task<Resultado<T>> Criar(T entity);
        Task<Resultado<T>> Atualizar(T entity);
        Task<Resultado<T>> Deletar(int id);
        Task<Resultado<List<T>>> ObterLista();
        Task<Resultado<T>> ObterPorId(int id);

    }
}
