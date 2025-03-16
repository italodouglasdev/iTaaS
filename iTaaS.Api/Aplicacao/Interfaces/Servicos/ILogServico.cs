using iTaaS.Api.Aplicacao.DTOs;
using iTaaS.Api.Dominio.Enumeradores;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iTaaS.Api.Aplicacao.Interfaces.Servicos
{
    public interface ILogServico : IServicoBase<LogDto>
    {

        Task<Resultado<string>> ImportarPorUrl(string url, TipoRetornoTranformacao tipoRetornoTranformacao);

        Task<Resultado<string>> ImportarPorId(int id, TipoRetornoTranformacao tipoRetornoTranformacao);

        Task<Resultado<string>> VerPorNomeArquivo(string nomeArquivo);

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
        TipoFormatoExibicaoLog tipoFormatoExibicaoLog);

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
         TipoFormatoExibicaoLog tipoFormatoExibicaoLog);

    }
}
