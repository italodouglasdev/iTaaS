using iTaaS.Api.Aplicacao.DTOs;
using iTaaS.Api.Aplicacao.DTOs.Auxiliares;
using iTaaS.Api.Aplicacao.Interfaces.Mapeadores;
using iTaaS.Api.Aplicacao.Interfaces.Repositorios;
using iTaaS.Api.Aplicacao.Interfaces.Servicos;
using iTaaS.Api.Aplicacao.Validadores.DTOs;
using iTaaS.Api.Dominio.Enumeradores;
using iTaaS.Api.Dominio.Fabricas;
using iTaaS.Api.Dominio.Helpers;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iTaaS.Api.Aplicacao.Servicos
{
    /// <summary>
    /// Serviço responsável pela manipulação de logs.
    /// </summary>
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


        /// <summary>
        /// Obtém um log pelo seu identificador único.
        /// </summary>
        /// <param name="id">Identificador único do log.</param>
        /// <returns>Resultado contendo o DTO do log, se encontrado, ou as inconsistências em caso de erro.</returns>
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


        /// <summary>
        /// Obtém uma lista de todos os logs armazenados.
        /// </summary>
        /// <returns>Resultado contendo uma lista de DTOs de logs.</returns>
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


        /// <summary>
        /// Cria um novo log e o armazena.
        /// </summary>
        /// <param name="logDto">Objeto DTO contendo os dados do log a ser criado.</param>
        /// <returns>Resultado com o log recém-criado ou inconsistências, caso falhe.</returns>
        public async Task<Resultado<LogDto>> Criar(LogDto logDto)
        {
            var resultadoService = new Resultado<LogDto>();

            var resultadoValidadorLogDto = LogDtoValidador.ValidarCriar(logDto);
            if (!resultadoValidadorLogDto.Sucesso)
            {
                resultadoService.AdicionarInconsistencias(resultadoValidadorLogDto.Inconsistencias);
                return resultadoService;
            }

            var logEntity = this.LogMapper.MapearDeDtoParaEntidade(logDto);

            var resultadoRepository = await this.LogRepository.Criar(logEntity);
            if (!resultadoRepository.Sucesso)
            {
                resultadoService.AdicionarInconsistencias(resultadoRepository.Inconsistencias);
                return resultadoService;
            }

            resultadoService.Dados = this.LogMapper.MapearDeEntidadeParaDto(logEntity);

            return resultadoService;
        }


        /// <summary>
        /// Atualiza um log existente com os dados fornecidos.
        /// </summary>
        /// <param name="logDto">Objeto DTO com os dados atualizados do log.</param>
        /// <returns>Resultado com o log atualizado ou inconsistências, caso falhe.</returns>
        public async Task<Resultado<LogDto>> Atualizar(LogDto logDto)
        {
            var resultadoService = new Resultado<LogDto>();

            var resultadoValidadorLogDto = LogDtoValidador.ValidarAtualizar(logDto);
            if (!resultadoValidadorLogDto.Sucesso)
            {
                resultadoService.AdicionarInconsistencias(resultadoValidadorLogDto.Inconsistencias);
                return resultadoService;
            }

            var entity = this.LogMapper.MapearDeDtoParaEntidade(logDto);

            var resultadoRepository = await this.LogRepository.Atualizar(entity);
            if (!resultadoRepository.Sucesso)
            {
                resultadoService.AdicionarInconsistencias(resultadoRepository.Inconsistencias);
                return resultadoService;
            }

            resultadoService.Dados = this.LogMapper.MapearDeEntidadeParaDto(entity);

            return resultadoService;
        }


        /// <summary>
        /// Deleta um log com base no identificador fornecido.
        /// </summary>
        /// <param name="id">Identificador único do log a ser deletado.</param>
        /// <returns>Resultado indicando se a operação foi bem-sucedida ou se houve inconsistências.</returns>
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


        /// <summary>
        /// Importa um log a partir de uma URL e cria um novo log no sistema.
        /// </summary>
        /// <param name="url">URL de onde os dados do log serão importados.</param>
        /// <param name="tipoRetornoLog">Tipo de retorno desejado (JSON, arquivo, etc.).</param>
        /// <returns>Resultado com a conversão do log importado ou inconsistências, caso falhe.</returns>
        public async Task<Resultado<string>> ImportarPorUrl(string url, TipoRetornoLog tipoRetornoLog)
        {
            var resultado = new Resultado<string>();

            var logTipoFormatoMinhaCdn = LogTipoFormatoFabrica.ObtenhaTipoFormato(TipoFormatoLog.MINHA_CDN);
            var logTipoFormatoAgora = LogTipoFormatoFabrica.ObtenhaTipoFormato(TipoFormatoLog.AGORA);

            var resultadoConversaoUrlDto = logTipoFormatoMinhaCdn.ConverterDeUrlParaDto(url);
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


            if (tipoRetornoLog == TipoRetornoLog.RETORNAR_PATCH)
            {
                var resultadoConversaoDtoArquivo = logTipoFormatoAgora.ConverterDeDtoParaArquivo(this.HttpContextoServico.ObtenhaUrlBase(), resultadoCriar.Dados);

                if (!resultadoConversaoDtoArquivo.Sucesso)
                {
                    resultado.AdicionarInconsistencias(resultadoConversaoDtoArquivo.Inconsistencias);
                    return resultado;
                }

                resultado.Dados = resultadoConversaoDtoArquivo.Dados;

            }
            else if (tipoRetornoLog == TipoRetornoLog.RETORNAR_JSON)
            {
                var resultadoConversaoJson = JsonHelper.Serializar(resultadoCriar.Dados);
                if (!resultadoConversaoJson.Sucesso)
                {
                    resultado.AdicionarInconsistencias(resultadoConversaoJson.Inconsistencias);
                    return resultado;
                }

                resultado.Dados = resultadoConversaoJson.Dados;
            }
            else
            {
                var resultadoConversaoDtoString = logTipoFormatoAgora.ConverterDeDtoParaString(resultadoCriar.Dados);
                if (!resultadoConversaoDtoString.Sucesso)
                {
                    resultado.AdicionarInconsistencias(resultadoConversaoDtoString.Inconsistencias);
                    return resultado;
                }

                resultado.Dados = resultadoConversaoDtoString.Dados;
            }


            return resultado;

        }


        /// <summary>
        /// Importa um log a partir de seu identificador e retorna no formato especificado.
        /// </summary>
        /// <param name="id">Identificador único do log a ser importado.</param>
        /// <param name="tipoLogRetorno">Tipo de retorno desejado (JSON, arquivo, etc.).</param>
        /// <returns>Resultado com o log importado ou inconsistências, caso falhe.</returns>
        public async Task<Resultado<string>> ImportarPorId(int id, TipoRetornoLog tipoRetornoLog)
        {
            var resultado = new Resultado<string>();

            var logTipoFormatoAgora = LogTipoFormatoFabrica.ObtenhaTipoFormato(TipoFormatoLog.AGORA);

            var resultadoObtenhaPorId = await ObterPorId(id);
            if (!resultadoObtenhaPorId.Sucesso)
            {
                resultado.AdicionarInconsistencias(resultadoObtenhaPorId.Inconsistencias);
                return resultado;
            }

            if (tipoRetornoLog == TipoRetornoLog.RETORNAR_PATCH)
            {
                var resultadoConversaoDtoArquivo = logTipoFormatoAgora.ConverterDeDtoParaArquivo(this.HttpContextoServico.ObtenhaUrlBase(), resultadoObtenhaPorId.Dados);

                if (!resultadoConversaoDtoArquivo.Sucesso)
                {
                    resultado.AdicionarInconsistencias(resultadoConversaoDtoArquivo.Inconsistencias);
                    return resultado;
                }

                resultado.Dados = resultadoConversaoDtoArquivo.Dados;

            }
            else if (tipoRetornoLog == TipoRetornoLog.RETORNAR_JSON)
            {
                var resultadoConversaoJson = JsonHelper.Serializar(resultadoObtenhaPorId.Dados);
                if (!resultadoConversaoJson.Sucesso)
                {
                    resultado.AdicionarInconsistencias(resultadoConversaoJson.Inconsistencias);
                    return resultado;
                }

                resultado.Dados = resultadoConversaoJson.Dados;
            }
            else
            {
                var resultadoConversaoDtoString = logTipoFormatoAgora.ConverterDeDtoParaString(resultadoObtenhaPorId.Dados);
                if (!resultadoConversaoDtoString.Sucesso)
                {
                    resultado.AdicionarInconsistencias(resultadoConversaoDtoString.Inconsistencias);
                    return resultado;
                }

                resultado.Dados = resultadoConversaoDtoString.Dados;
            }




            return resultado;

        }


        /// <summary>
        /// Obtém logs filtrados com base em diversos parâmetros de filtro e retorna o resultado no formato especificado.
        /// </summary>
        /// <param name="dataHoraRecebimentoInicio">Data e hora de início do recebimento dos logs.</param>
        /// <param name="dataHoraRecebimentoFim">Data e hora de fim do recebimento dos logs.</param>
        /// <param name="metodoHttp">Método HTTP utilizado na requisição.</param>
        /// <param name="codigoStatus">Código de status HTTP.</param>
        /// <param name="caminhoUrl">Caminho da URL requisitada.</param>
        /// <param name="tempoRespostaInicial">Tempo de resposta inicial (em segundos).</param>
        /// <param name="tempoRespostaFinal">Tempo de resposta final (em segundos).</param>
        /// <param name="tamanhoRespostaInicial">Tamanho inicial da resposta (em bytes).</param>
        /// <param name="tamanhoRespostaFinal">Tamanho final da resposta (em bytes).</param>
        /// <param name="cashStatus">Status do cache da resposta.</param>
        /// <param name="tipoRetornoLog">Tipo de retorno do log (JSON, PATCH, ou STRING).</param>
        /// <returns>Retorna um objeto de resultado com os logs filtrados.</returns>
        public async Task<Resultado<string>> BuscarSalvos(
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
            var logTipoFormatoMinhaCdn = LogTipoFormatoFabrica.ObtenhaTipoFormato(TipoFormatoLog.MINHA_CDN);


            if (tipoRetornoLog == TipoRetornoLog.RETORNAR_PATCH)
            {
                foreach (var logEntidade in resultadoRepository.Dados)
                {
                    var logDto = LogMapper.MapearDeEntidadeParaDto(logEntidade);

                    var resultadoConversaoDtoArquivo = logTipoFormatoMinhaCdn.ConverterDeDtoParaArquivo(this.HttpContextoServico.ObtenhaUrlBase(), logDto);
                    if (!resultadoConversaoDtoArquivo.Sucesso)
                    {
                        resultadoService.AdicionarInconsistencias(resultadoConversaoDtoArquivo.Inconsistencias);
                        return resultadoService;
                    }

                    strinBuilderLogs.AppendLine(resultadoConversaoDtoArquivo.Dados);
                }
            }
            else if (tipoRetornoLog == TipoRetornoLog.RETORNAR_JSON)
            {
                var listaLogsDto = LogMapper.MapearListaDeEntitadesParaDtos(resultadoRepository.Dados);

                var resultadoJson = JsonHelper.Serializar(listaLogsDto);
                if (!resultadoJson.Sucesso)
                {
                    resultadoService.AdicionarInconsistencias(resultadoJson.Inconsistencias);
                    return resultadoService;
                }

                strinBuilderLogs.Append(resultadoJson.Dados);
            }
            else
            {
                foreach (var logEntidade in resultadoRepository.Dados)
                {
                    var logDto = LogMapper.MapearDeEntidadeParaDto(logEntidade);

                    var resultadoConversaoDtoString = logTipoFormatoMinhaCdn.ConverterDeDtoParaString(logDto);
                    if (!resultadoConversaoDtoString.Sucesso)
                    {
                        resultadoService.AdicionarInconsistencias(resultadoConversaoDtoString.Inconsistencias);
                        return resultadoService;
                    }

                    strinBuilderLogs.AppendLine(resultadoConversaoDtoString.Dados);
                    strinBuilderLogs.AppendLine();
                }

            }

            resultadoService.Dados = strinBuilderLogs.ToString();

            return resultadoService;
        }


        /// <summary>
        /// Obtém logs transformados com base em diversos parâmetros de filtro e retorna o resultado no formato especificado.
        /// </summary>
        /// <param name="dataHoraRecebimentoInicio">Data e hora de início do recebimento dos logs.</param>
        /// <param name="dataHoraRecebimentoFim">Data e hora de fim do recebimento dos logs.</param>
        /// <param name="metodoHttp">Método HTTP utilizado na requisição.</param>
        /// <param name="codigoStatus">Código de status HTTP.</param>
        /// <param name="caminhoUrl">Caminho da URL requisitada.</param>
        /// <param name="tempoRespostaInicial">Tempo de resposta inicial (em segundos).</param>
        /// <param name="tempoRespostaFinal">Tempo de resposta final (em segundos).</param>
        /// <param name="tamanhoRespostaInicial">Tamanho inicial da resposta (em bytes).</param>
        /// <param name="tamanhoRespostaFinal">Tamanho final da resposta (em bytes).</param>
        /// <param name="cashStatus">Status do cache da resposta.</param>
        /// <param name="tipoRetornoLog">Tipo de retorno do log (JSON, PATCH, ou STRING).</param>
        /// <returns>Retorna um objeto de resultado com os logs transformados.</returns>
        public async Task<Resultado<string>> BuscarTransformados(
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
            var logTipoFormatoMinhaCdn = LogTipoFormatoFabrica.ObtenhaTipoFormato(TipoFormatoLog.MINHA_CDN);
            var logTipoFormatoAgora = LogTipoFormatoFabrica.ObtenhaTipoFormato(TipoFormatoLog.AGORA);


            if (tipoRetornoLog == TipoRetornoLog.RETORNAR_PATCH)
            {
                foreach (var logEntidade in resultadoRepository.Dados)
                {
                    var logDto = LogMapper.MapearDeEntidadeParaDto(logEntidade);
                    var resultadoConversaoDtoArquivoMinhaCdn = logTipoFormatoMinhaCdn.ConverterDeDtoParaArquivo(this.HttpContextoServico.ObtenhaUrlBase(), logDto);
                    strinBuilderLogs.AppendLine(resultadoConversaoDtoArquivoMinhaCdn.Dados);

                    var resultadoConversaoDtoArquivoAgora = logTipoFormatoAgora.ConverterDeDtoParaArquivo(this.HttpContextoServico.ObtenhaUrlBase(), logDto);
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

                    var resultadoConversaoDtoArquivoMinhaCdn = logTipoFormatoMinhaCdn.ConverterDeDtoParaString(logDto);
                    logJson.LogMinhaCdn = resultadoConversaoDtoArquivoMinhaCdn.Dados;

                    var resultadoConversaoDtoArquivoAgora = logTipoFormatoAgora.ConverterDeDtoParaString(logDto);
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

                    var resultadoConversaoDtoStringMinhaCdn = logTipoFormatoMinhaCdn.ConverterDeDtoParaString(logDto);
                    strinBuilderLogs.AppendLine(resultadoConversaoDtoStringMinhaCdn.Dados);
                    strinBuilderLogs.AppendLine();

                    var resultadoConversaoDtoArquivoAgora = logTipoFormatoAgora.ConverterDeDtoParaString(logDto);
                    strinBuilderLogs.AppendLine(resultadoConversaoDtoArquivoAgora.Dados);
                    strinBuilderLogs.AppendLine();
                    strinBuilderLogs.AppendLine("---");
                    strinBuilderLogs.AppendLine();
                }

            }

            resultadoService.Dados = strinBuilderLogs.ToString();

            return resultadoService;
        }


        /// <summary>
        /// Obtém o log por identificador e retorna o resultado no formato especificado.
        /// </summary>
        /// <param name="id">Identificador do log.</param>
        /// <param name="tipoRetornoLog">Tipo de retorno do log (JSON, PATCH, ou STRING).</param>
        /// <returns>Retorna um objeto de resultado com o log encontrado.</returns>
        public async Task<Resultado<string>> BuscarPorIdentificador(int id, TipoRetornoLog tipoRetornoLog)
        {
            var resultadorObtenhaPorIdentificador = new Resultado<string>();

            var resultadoObtenha = await ObterPorId(id);
            if (!resultadoObtenha.Sucesso)
            {
                resultadorObtenhaPorIdentificador.AdicionarInconsistencias(resultadoObtenha.Inconsistencias);
                return resultadorObtenhaPorIdentificador;
            }

            var logDto = resultadoObtenha.Dados;

            var logTipoFormatoMinhaCdn = LogTipoFormatoFabrica.ObtenhaTipoFormato(TipoFormatoLog.MINHA_CDN);
                        
            if (tipoRetornoLog == TipoRetornoLog.RETORNAR_PATCH)
            {
                var resultadoConversaoDtoArquivo = logTipoFormatoMinhaCdn.ConverterDeDtoParaArquivo(this.HttpContextoServico.ObtenhaUrlBase(), logDto);

                if (!resultadoConversaoDtoArquivo.Sucesso)
                {
                    resultadorObtenhaPorIdentificador.AdicionarInconsistencias(resultadoConversaoDtoArquivo.Inconsistencias);
                    return resultadorObtenhaPorIdentificador;
                }

                resultadorObtenhaPorIdentificador.Dados = resultadoConversaoDtoArquivo.Dados;

            }
            else if (tipoRetornoLog == TipoRetornoLog.RETORNAR_JSON)
            {
                var resultadoConversaoJson = JsonHelper.Serializar(logDto);
                if (!resultadoConversaoJson.Sucesso)
                {
                    resultadorObtenhaPorIdentificador.AdicionarInconsistencias(resultadoConversaoJson.Inconsistencias);
                    return resultadorObtenhaPorIdentificador;
                }

                resultadorObtenhaPorIdentificador.Dados = resultadoConversaoJson.Dados;
            }
            else
            {
                var resultadoConversaoDtoString = logTipoFormatoMinhaCdn.ConverterDeDtoParaString(logDto);
                if (!resultadoConversaoDtoString.Sucesso)
                {
                    resultadorObtenhaPorIdentificador.AdicionarInconsistencias(resultadoConversaoDtoString.Inconsistencias);
                    return resultadorObtenhaPorIdentificador;
                }

                resultadorObtenhaPorIdentificador.Dados = resultadoConversaoDtoString.Dados;
            }


            return resultadorObtenhaPorIdentificador;
        }


        /// <summary>
        /// Obtém o log transformado por identificador e retorna o resultado no formato especificado.
        /// </summary>
        /// <param name="id">Identificador do log.</param>
        /// <param name="tipoRetornoLog">Tipo de retorno do log (JSON, PATCH, ou STRING).</param>
        /// <returns>Retorna um objeto de resultado com o log transformado.</returns>
        public async Task<Resultado<string>> BuscarTransformadoPorIdentificador(int id, TipoRetornoLog tipoRetornoLog)
        {
            var resultadorObtenhaPorIdentificador = new Resultado<string>();

            var resultadoObtenha = await ObterPorId(id);
            if (!resultadoObtenha.Sucesso)
            {
                resultadorObtenhaPorIdentificador.AdicionarInconsistencias(resultadoObtenha.Inconsistencias);
                return resultadorObtenhaPorIdentificador;
            }

            var logDto = resultadoObtenha.Dados;

            var logTipoFormatoAgora = LogTipoFormatoFabrica.ObtenhaTipoFormato(TipoFormatoLog.AGORA);            

            if (tipoRetornoLog == TipoRetornoLog.RETORNAR_PATCH)
            {
                var resultadoConversaoDtoArquivo = logTipoFormatoAgora.ConverterDeDtoParaArquivo(this.HttpContextoServico.ObtenhaUrlBase(), logDto);

                if (!resultadoConversaoDtoArquivo.Sucesso)
                {
                    resultadorObtenhaPorIdentificador.AdicionarInconsistencias(resultadoConversaoDtoArquivo.Inconsistencias);
                    return resultadorObtenhaPorIdentificador;
                }

                resultadorObtenhaPorIdentificador.Dados = resultadoConversaoDtoArquivo.Dados;

            }
            else if (tipoRetornoLog == TipoRetornoLog.RETORNAR_JSON)
            {
                var resultadoConversaoJson = JsonHelper.Serializar(logDto);
                if (!resultadoConversaoJson.Sucesso)
                {
                    resultadorObtenhaPorIdentificador.AdicionarInconsistencias(resultadoConversaoJson.Inconsistencias);
                    return resultadorObtenhaPorIdentificador;
                }

                resultadorObtenhaPorIdentificador.Dados = resultadoConversaoJson.Dados;
            }
            else
            {
                var resultadoConversaoDtoString = logTipoFormatoAgora.ConverterDeDtoParaString(logDto);
                if (!resultadoConversaoDtoString.Sucesso)
                {
                    resultadorObtenhaPorIdentificador.AdicionarInconsistencias(resultadoConversaoDtoString.Inconsistencias);
                    return resultadorObtenhaPorIdentificador;
                }

                resultadorObtenhaPorIdentificador.Dados = resultadoConversaoDtoString.Dados;
            }


            return resultadorObtenhaPorIdentificador;
        }
    }

}
