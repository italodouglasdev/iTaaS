using iTaaS.Api.Aplicacao.Interfaces.Servicos;
using iTaaS.Api.Aplicacao.Servicos;
using iTaaS.Api.Dominio.Enumeradores;

namespace iTaaS.Api.Dominio.Fabricas
{
    public class LogTipoFormatoFabrica
    {       
        public static ILogTipoFormatoServico ObtenhaTipoFormato(TipoFormatoLog tipoFormato)
        {
            if (tipoFormato == TipoFormatoLog.AGORA)
                return new LogTipoFormatoAgoraServico();

            return new LogTipoFormatoMinhaCdnServico();
        }
    }
}
