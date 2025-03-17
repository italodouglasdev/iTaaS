using iTaaS.Api.Aplicacao.DTOs.Auxiliares;
using iTaaS.Api.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iTaaS.Api.Aplicacao.Interfaces.Repositorios
{
    public interface ILogRepositorio : IRepositorioBase<LogEntidade>
    {
        Task<Resultado<List<LogEntidade>>> ObterLogsFiltrados(
             string dataHoraRecebimentoInicio,
             string dataHoraRecebimentoFim,
             string metodoHttp,
             int codigoStatus,
             string caminhoUrl,
             decimal tempoRespostaInicial,
             decimal tempoRespostaFinal,
             int tamanhoRespostaInicial,
             int tamanhoRespostaFinal,
             string cashStatus);

    }
}
