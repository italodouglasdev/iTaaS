using iTaaS.Api.Aplicacao.DTOs;
using iTaaS.Api.Aplicacao.DTOs.Auxiliares;
using iTaaS.Api.Aplicacao.Interfaces.Mapeadores;
using iTaaS.Api.Aplicacao.Interfaces.Repositorios;
using iTaaS.Api.Aplicacao.Interfaces.Servicos;
using iTaaS.Api.Dominio.Enumeradores;
using iTaaS.Api.Dominio.Fabricas;
using iTaaS.Api.Dominio.Helpers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Remotion.Linq.Clauses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iTaaS.Api.Aplicacao.Servicos
{
    public class LogServico : ILogServico
    {

        private readonly ILogRepositorio LogRepository;
        private readonly ILogMapeador LogMapper;
        private readonly IHttpContextoServico HttpContextoServico;


        public LogServico(ILogRepositorio LogRepository, ILogMapeador LogMapper, IHttpContextoServico HttpContextoServico)
        {
            this.LogRepository = LogRepository;
            this.LogMapper = LogMapper;
            this.HttpContextoServico = HttpContextoServico;
        }

        public async Task<Resultado<LogDto>> ObterPorId(int id)
        {
            var resultadoService = new Resultado<LogDto>();

            var resultadoRepository = await LogRepository.ObterPorId(id);
            if (!resultadoRepository.Sucesso)
            {
                resultadoService.AdicionarInconsistencias(resultadoRepository.Inconsistencias);
                return resultadoService;
            }

            resultadoService.Dados = LogMapper.MapearDeEntidadeParaDto(resultadoRepository.Dados);

            return resultadoService;
        }

        public async Task<Resultado<List<LogDto>>> ObterLista()
        {
            var resultadoService = new Resultado<List<LogDto>>();

            var resultadoRepository = await LogRepository.ObterLista();
            if (!resultadoRepository.Sucesso)
            {
                resultadoService.AdicionarInconsistencias(resultadoRepository.Inconsistencias);
                return resultadoService;
            }

            resultadoService.Dados = LogMapper.MapearListaDeEntitadesParaDtos(resultadoRepository.Dados);

            return resultadoService;
        }

        public async Task<Resultado<LogDto>> Criar(LogDto logDto)
        {
            var resultadoService = new Resultado<LogDto>();

            var logEntity = LogMapper.MapearDeDtoParaEntidade(logDto);

            logEntity.Hash = Guid.NewGuid().ToString();

            var resultadoRepository = await LogRepository.Criar(logEntity);
            if (!resultadoRepository.Sucesso)
            {
                resultadoService.AdicionarInconsistencias(resultadoRepository.Inconsistencias);
                return resultadoService;
            }

            resultadoService.Dados = LogMapper.MapearDeEntidadeParaDto(logEntity);

            return resultadoService;
        }

        public async Task<Resultado<LogDto>> Atualizar(LogDto logDto)
        {
            var resultadoService = new Resultado<LogDto>();

            var entity = LogMapper.MapearDeDtoParaEntidade(logDto);

            var resultadoRepository = await LogRepository.Atualizar(entity);
            if (!resultadoRepository.Sucesso)
            {
                resultadoService.AdicionarInconsistencias(resultadoRepository.Inconsistencias);
                return resultadoService;
            }

            resultadoService.Dados = LogMapper.MapearDeEntidadeParaDto(entity);

            return resultadoService;
        }

        public async Task<Resultado<LogDto>> Deletar(int id)
        {
            var resultadoService = new Resultado<LogDto>();

            var resultadoRepository = await LogRepository.Deletar(id);
            if (!resultadoRepository.Sucesso)
            {
                resultadoService.AdicionarInconsistencias(resultadoRepository.Inconsistencias);
                return resultadoService;
            }

            return resultadoService;
        }


        public async Task<Resultado<string>> ImportarPorUrl(string url, TipoRetornoLog tipoLogRetorno)
        {
            var resultado = new Resultado<string>();

            var logTipoFormatoFabrica = LogTipoFormatoFabrica.ObtenhaTipoFormato(TipoFormatoLog.MINHA_CDN);
            var resultadoConversaoUrlDto = logTipoFormatoFabrica.ConverterDeUrlParaDto(url);
            if (!resultadoConversaoUrlDto.Sucesso)
            {
                resultado.AdicionarInconsistencias(resultadoConversaoUrlDto.Inconsistencias);
                return resultado;
            }

            var logDto = resultadoConversaoUrlDto.Dados;
            logDto.UrlOrigem = url;

            var resultadoCriar = await Criar(logDto);
            if (!resultadoCriar.Sucesso)
            {
                resultado.AdicionarInconsistencias(resultadoCriar.Inconsistencias);
                return resultado;
            }

            var resultadoConversaoDtoArquivo = new Resultado<string>();
            if (tipoLogRetorno == TipoRetornoLog.RETORNAR_PATCH)
            {
                logTipoFormatoFabrica = LogTipoFormatoFabrica.ObtenhaTipoFormato(TipoFormatoLog.AGORA);
                resultadoConversaoDtoArquivo = logTipoFormatoFabrica.ConverterDeDtoParaArquivo(this.HttpContextoServico.ObtenhaUrlBase(), resultadoCriar.Dados);
            }
            else if (tipoLogRetorno == TipoRetornoLog.RETORNAR_ARQUIVO)
            {
                logTipoFormatoFabrica = LogTipoFormatoFabrica.ObtenhaTipoFormato(TipoFormatoLog.AGORA);
                resultadoConversaoDtoArquivo = logTipoFormatoFabrica.ConverterDeDtoParaString(resultadoCriar.Dados);
            }
            else
            {
                resultadoConversaoDtoArquivo.Dados = JsonConvert.SerializeObject(logDto, Formatting.Indented);
            }

            if (!resultadoConversaoDtoArquivo.Sucesso)
            {
                resultado.AdicionarInconsistencias(resultadoConversaoDtoArquivo.Inconsistencias);
                return resultado;
            }

            resultado.Dados = resultadoConversaoDtoArquivo.Dados;

            return resultado;

        }

        public async Task<Resultado<string>> ImportarPorId(int id, TipoRetornoLog tipoLogRetorno)
        {
            var resultado = new Resultado<string>();

            var logTipoFormatoFabrica = LogTipoFormatoFabrica.ObtenhaTipoFormato(TipoFormatoLog.AGORA);

            var resultadoObtenhaPorId = await ObterPorId(id);
            if (!resultadoObtenhaPorId.Sucesso)
            {
                resultado.AdicionarInconsistencias(resultadoObtenhaPorId.Inconsistencias);
                return resultado;
            }

            var resultadoConversaoDtoArquivo = new Resultado<string>();
            if (tipoLogRetorno == TipoRetornoLog.RETORNAR_PATCH)
            {
                resultadoConversaoDtoArquivo = logTipoFormatoFabrica.ConverterDeDtoParaArquivo(this.HttpContextoServico.ObtenhaUrlBase(), resultadoObtenhaPorId.Dados);
            }
            else if (tipoLogRetorno == TipoRetornoLog.RETORNAR_ARQUIVO)
            {               
                resultadoConversaoDtoArquivo = logTipoFormatoFabrica.ConverterDeDtoParaString(resultadoObtenhaPorId.Dados);
            }
            else
            {
                resultadoConversaoDtoArquivo.Dados = JsonConvert.SerializeObject(resultadoObtenhaPorId.Dados, Formatting.Indented);
            }

            if (!resultadoConversaoDtoArquivo.Sucesso)
            {
                resultado.AdicionarInconsistencias(resultadoConversaoDtoArquivo.Inconsistencias);
                return resultado;
            }

            resultado.Dados = resultadoConversaoDtoArquivo.Dados;

            return resultado;

        }


        public async Task<Resultado<string>> ObterLogsFiltrados(
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
         TipoRetornoLog tipoRetornoLog)
        {
            var resultadoService = new Resultado<string>();

            var resultadoRepository = await LogRepository.ObterLogsFiltrados(
                dataHoraRecebimentoInicio,
                dataHoraRecebimentoFim,
                metodoHttp,
                codigoStatus,
                caminhoUrl,
                tempoRespostaInicial,
                tempoRespostaFinal,
                tamanhoRespostaInicial,
                tamanhoRespostaFinal,
                cashStatus);

            if (!resultadoRepository.Sucesso)
            {
                resultadoService.AdicionarInconsistencias(resultadoRepository.Inconsistencias);
                return resultadoService;
            }

            var strinBuilderLogs = new StringBuilder();
            var logTipoFormatoFabrica = LogTipoFormatoFabrica.ObtenhaTipoFormato(TipoFormatoLog.MINHA_CDN);


            if (tipoRetornoLog == TipoRetornoLog.RETORNAR_PATCH)
            {
                foreach (var logEntidade in resultadoRepository.Dados)
                {
                    var logDto = LogMapper.MapearDeEntidadeParaDto(logEntidade);
                    var resultadoConversaoDtoArquivo = logTipoFormatoFabrica.ConverterDeDtoParaArquivo(this.HttpContextoServico.ObtenhaUrlBase(), logDto);
                    strinBuilderLogs.AppendLine(resultadoConversaoDtoArquivo.Dados);
                }
            }
            else if (tipoRetornoLog == TipoRetornoLog.RETORNAR_JSON)
            {
                var listaLogsDto = LogMapper.MapearListaDeEntitadesParaDtos(resultadoRepository.Dados);
                strinBuilderLogs.Append(JsonConvert.SerializeObject(listaLogsDto, Formatting.Indented));
            }
            else
            {
                foreach (var logEntidade in resultadoRepository.Dados)
                {
                    var logDto = LogMapper.MapearDeEntidadeParaDto(logEntidade);
                    var resultadoConversaoDtoString = logTipoFormatoFabrica.ConverterDeDtoParaString(logDto);

                    strinBuilderLogs.AppendLine(resultadoConversaoDtoString.Dados);
                    strinBuilderLogs.AppendLine();
                }

            }

            resultadoService.Dados = strinBuilderLogs.ToString();

            return resultadoService;
        }


        public async Task<Resultado<string>> ObterLogsTransformados(
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
         TipoRetornoLog tipoRetornoLog)
        {
            var resultadoService = new Resultado<string>();

            var resultadoRepository = await LogRepository.ObterLogsFiltrados(
                dataHoraRecebimentoInicio,
                dataHoraRecebimentoFim,
                metodoHttp,
                codigoStatus,
                caminhoUrl,
                tempoRespostaInicial,
                tempoRespostaFinal,
                tamanhoRespostaInicial,
                tamanhoRespostaFinal,
                cashStatus);

            if (!resultadoRepository.Sucesso)
            {
                resultadoService.AdicionarInconsistencias(resultadoRepository.Inconsistencias);
                return resultadoService;
            }

            var strinBuilderLogs = new StringBuilder();
            var logTipoFormatoMinhaCdnFabrica = LogTipoFormatoFabrica.ObtenhaTipoFormato(TipoFormatoLog.MINHA_CDN);
            var logTipoFormatoAgoraFabrica = LogTipoFormatoFabrica.ObtenhaTipoFormato(TipoFormatoLog.AGORA);


            if (tipoRetornoLog == TipoRetornoLog.RETORNAR_PATCH)
            {
                foreach (var logEntidade in resultadoRepository.Dados)
                {
                    var logDto = LogMapper.MapearDeEntidadeParaDto(logEntidade);
                    var resultadoConversaoDtoArquivoMinhaCdn = logTipoFormatoMinhaCdnFabrica.ConverterDeDtoParaArquivo(this.HttpContextoServico.ObtenhaUrlBase(), logDto);
                    strinBuilderLogs.AppendLine(resultadoConversaoDtoArquivoMinhaCdn.Dados);

                    var resultadoConversaoDtoArquivoAgora = logTipoFormatoAgoraFabrica.ConverterDeDtoParaArquivo(this.HttpContextoServico.ObtenhaUrlBase(), logDto);
                    strinBuilderLogs.AppendLine(resultadoConversaoDtoArquivoAgora.Dados);

                    strinBuilderLogs.AppendLine();
                }
            }
            else if (tipoRetornoLog == TipoRetornoLog.RETORNAR_JSON)
            {
                var listaLogsJson = new List<LogJson>();

                foreach (var logEntidade in resultadoRepository.Dados)
                {
                    var logDto = LogMapper.MapearDeEntidadeParaDto(logEntidade);

                    var logJson = new LogJson();
                    logJson.Id = logDto.Id;

                    var resultadoConversaoDtoArquivoMinhaCdn = logTipoFormatoMinhaCdnFabrica.ConverterDeDtoParaString(logDto);
                    logJson.LogMinhaCdn = resultadoConversaoDtoArquivoMinhaCdn.Dados;

                    var resultadoConversaoDtoArquivoAgora = logTipoFormatoAgoraFabrica.ConverterDeDtoParaString(logDto);
                    logJson.LogAgora = resultadoConversaoDtoArquivoAgora.Dados;

                    listaLogsJson.Add(logJson);
                }

                strinBuilderLogs.Append(JsonConvert.SerializeObject(listaLogsJson, Formatting.Indented));
            }
            else
            {
                foreach (var logEntidade in resultadoRepository.Dados)
                {
                    var logDto = LogMapper.MapearDeEntidadeParaDto(logEntidade);

                    var resultadoConversaoDtoStringMinhaCdn = logTipoFormatoMinhaCdnFabrica.ConverterDeDtoParaString(logDto);
                    strinBuilderLogs.AppendLine(resultadoConversaoDtoStringMinhaCdn.Dados);
                    strinBuilderLogs.AppendLine();

                    var resultadoConversaoDtoArquivoAgora = logTipoFormatoAgoraFabrica.ConverterDeDtoParaString(logDto);
                    strinBuilderLogs.AppendLine(resultadoConversaoDtoArquivoAgora.Dados);
                    strinBuilderLogs.AppendLine();
                    strinBuilderLogs.AppendLine("---");
                    strinBuilderLogs.AppendLine();
                }

            }

            resultadoService.Dados = strinBuilderLogs.ToString();

            return resultadoService;
        }


        public async Task<Resultado<string>> ObtenhaPorIdentificador(int id, TipoRetornoLog tipoRetornoLog)
        {
            var resultadorObtenhaPorIdentificador = new Resultado<string>();

            var resultadoObtenha = await ObterPorId(id);
            if (!resultadoObtenha.Sucesso)
            {
                resultadorObtenhaPorIdentificador.AdicionarInconsistencias(resultadoObtenha.Inconsistencias);
                return resultadorObtenhaPorIdentificador;
            }

            var logDto = resultadoObtenha.Dados;

            var logTipoFormatoFabricaMinhaCdn = LogTipoFormatoFabrica.ObtenhaTipoFormato(TipoFormatoLog.MINHA_CDN);


            if (tipoRetornoLog == TipoRetornoLog.RETORNAR_PATCH)
            {
                var resultadoConversaoDtoArquivoMinhaCdn = logTipoFormatoFabricaMinhaCdn.ConverterDeDtoParaArquivo(this.HttpContextoServico.ObtenhaUrlBase(), logDto);
                resultadorObtenhaPorIdentificador.Dados = resultadoConversaoDtoArquivoMinhaCdn.Dados;
            }
            else if (tipoRetornoLog == TipoRetornoLog.RETORNAR_JSON)
            {
                resultadorObtenhaPorIdentificador.Dados = JsonConvert.SerializeObject(logDto, Formatting.Indented);
            }
            else
            {
                var resultadoConversaoDtoStringMinhaCdn = logTipoFormatoFabricaMinhaCdn.ConverterDeDtoParaString(logDto);
                resultadorObtenhaPorIdentificador.Dados = resultadoConversaoDtoStringMinhaCdn.Dados;
            }


            return resultadorObtenhaPorIdentificador;
        }

        public async Task<Resultado<string>> ObtenhaTransformadoPorIdentificador(int id, TipoRetornoLog tipoRetornoLog)
        {
            var resultadorObtenhaPorIdentificador = new Resultado<string>();

            var resultadoObtenha = await ObterPorId(id);
            if (!resultadoObtenha.Sucesso)
            {
                resultadorObtenhaPorIdentificador.AdicionarInconsistencias(resultadoObtenha.Inconsistencias);
                return resultadorObtenhaPorIdentificador;
            }

            var logDto = resultadoObtenha.Dados;

            var logTipoFormatoFabricaAgora = LogTipoFormatoFabrica.ObtenhaTipoFormato(TipoFormatoLog.AGORA);
            if (tipoRetornoLog == TipoRetornoLog.RETORNAR_PATCH)
            {
                var resultadoConversaoDtoArquivoMinhaCdn = logTipoFormatoFabricaAgora.ConverterDeDtoParaArquivo(this.HttpContextoServico.ObtenhaUrlBase(), logDto);
                resultadorObtenhaPorIdentificador.Dados = resultadoConversaoDtoArquivoMinhaCdn.Dados;
            }
            else if (tipoRetornoLog == TipoRetornoLog.RETORNAR_JSON)
            {
                resultadorObtenhaPorIdentificador.Dados = JsonConvert.SerializeObject(logDto, Formatting.Indented);
            }
            else
            {
                var resultadoConversaoDtoStringMinhaCdn = logTipoFormatoFabricaAgora.ConverterDeDtoParaString(logDto);
                resultadorObtenhaPorIdentificador.Dados = resultadoConversaoDtoStringMinhaCdn.Dados;
            }


            return resultadorObtenhaPorIdentificador;
        }
    }

}
