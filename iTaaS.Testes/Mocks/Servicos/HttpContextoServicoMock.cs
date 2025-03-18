using iTaaS.Api.Aplicacao.Interfaces.Servicos;

namespace iTaaS.Api.Aplicacao.Servicos
{
    public class HttpContextoServicoMock : IHttpContextoServico
    {
        public string ObtenhaUrlBase()
        {
            var caminhoBase = $"LocalHost//";

            return caminhoBase;
        }
    }
}
