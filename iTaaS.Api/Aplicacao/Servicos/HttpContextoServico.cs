using iTaaS.Api.Aplicacao.Interfaces.Servicos;
using Microsoft.AspNetCore.Http;

namespace iTaaS.Api.Aplicacao.Servicos
{
    public class HttpContextoServico : IHttpContextoServico
    {
        private readonly IHttpContextAccessor HttpContexto;

        public HttpContextoServico(IHttpContextAccessor HttpContexto)
        {
            this.HttpContexto = HttpContexto;
        }

        public string ObtenhaUrlBase()
        {         
            var caminhoBase = this.HttpContexto.HttpContext.Request.Scheme + "://" +
                              this.HttpContexto.HttpContext.Request.Host.Value;

            return caminhoBase;
        }
    }
}
