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

    }
}
