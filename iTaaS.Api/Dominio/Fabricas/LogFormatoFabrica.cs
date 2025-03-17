using iTaaS.Api.Aplicacao.Interfaces.Servicos;
using iTaaS.Api.Aplicacao.Servicos;
using iTaaS.Api.Dominio.Enumeradores;

namespace iTaaS.Api.Dominio.Fabricas
{
    public class LogFormatoFabrica
    {
        public static IConverterLogServico ObterConversor(TipoFormatoLog tipoFormato)
        {
            if (tipoFormato == TipoFormatoLog.AGORA)
                return new LogAgoraServico();

            return new LogMinhaCdnServico();
        }
    }
}
