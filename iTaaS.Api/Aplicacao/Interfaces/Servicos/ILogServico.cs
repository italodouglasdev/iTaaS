using iTaaS.Api.Aplicacao.DTOs;
using iTaaS.Api.Aplicacao.DTOs.Auxiliares;
using iTaaS.Api.Dominio.Enumeradores;
using System.Threading.Tasks;

namespace iTaaS.Api.Aplicacao.Interfaces.Servicos
{
    public interface ILogServico : IServicoBase<LogDto>
    {
        Task<Resultado<string>> ImportarPorUrl(string url, TipoRetornoLog tipoLogRetorno);

        Task<Resultado<string>> ImportarPorId(int id, TipoRetornoLog tipoLogRetorno);    

        Task<Resultado<string>> ObterLogsFiltrados(
        string dataHoraRecebimentoInicio,
        string dataHoraRecebimentoFim,
        string metodoHttp,
        int codigoStatus,
        string caminhoUrl,
        decimal tempoRespostaInicial,
        decimal tempoRespostaFinal,
        int tamanhoRespostaInicial,
        int tamanhoRespostaFinal,
        string cashStatus,
        TipoRetornoLog tipoRetornoLog);

        Task<Resultado<string>> ObterLogsTransformados(
         string dataHoraRecebimentoInicio,
         string dataHoraRecebimentoFim,
         string metodoHttp,
         int codigoStatus,
         string caminhoUrl,
         decimal tempoRespostaInicial,
         decimal tempoRespostaFinal,
         int tamanhoRespostaInicial,
         int tamanhoRespostaFinal,
         string cashStatus,
         TipoRetornoLog tipoRetornoLog);

        Task<Resultado<string>> ObtenhaPorIdentificador(
        int id,
        TipoRetornoLog tipoRetornoLog);

        Task<Resultado<string>> ObtenhaTransformadoPorIdentificador(
        int id,
        TipoRetornoLog tipoRetornoLog);
    }
}
