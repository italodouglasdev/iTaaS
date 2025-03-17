using iTaaS.Api.Aplicacao.DTOs;
using iTaaS.Api.Aplicacao.DTOs.Auxiliares;
using iTaaS.Api.Aplicacao.Interfaces.Mapeadores;
using iTaaS.Api.Aplicacao.Interfaces.Repositorios;
using iTaaS.Api.Aplicacao.Interfaces.Servicos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iTaaS.Api.Aplicacao.Servicos
{
    /// <summary>
    /// Serviço responsável pela manipulação de registros de log de linha.
    /// </summary>
    public class LogLinhaServico : ILogLinhaServico
    {

        private readonly ILogLinhaRepositorio LogLinhaRepository;
        private readonly ILogLinhaMapeador LogLinhaMapper;

        public LogLinhaServico(ILogLinhaRepositorio logLinhaRepository, ILogLinhaMapeador logLinhaMapper)
        {
            LogLinhaRepository = logLinhaRepository;
            LogLinhaMapper = logLinhaMapper;
        }


        /// <summary>
        /// Obtém um registro de log de linha pelo seu ID.
        /// </summary>
        /// <param name="id">ID do log de linha a ser obtido.</param>
        /// <returns>Resultado contendo o DTO do log de linha ou inconsistências.</returns>
        public async Task<Resultado<LogLinhaDto>> ObterPorId(int id)
        {
            var resultadoService = new Resultado<LogLinhaDto>();

            var resultadoRepository = await LogLinhaRepository.ObterPorId(id);
            if (!resultadoRepository.Sucesso)
            {
                resultadoService.AdicionarInconsistencias(resultadoRepository.Inconsistencias);
                return resultadoService;
            }

            resultadoService.Dados = LogLinhaMapper.MapearDeEntidadeParaDto(resultadoRepository.Dados);

            return resultadoService;
        }


        /// <summary>
        /// Obtém uma lista de registros de log de linha.
        /// </summary>
        /// <returns>Resultado contendo a lista de DTOs dos logs de linha ou inconsistências.</returns>
        public async Task<Resultado<List<LogLinhaDto>>> ObterLista()
        {
            var resultadoService = new Resultado<List<LogLinhaDto>>();

            var resultadoRepository = await LogLinhaRepository.ObterLista();
            if (!resultadoRepository.Sucesso)
            {
                resultadoService.AdicionarInconsistencias(resultadoRepository.Inconsistencias);
                return resultadoService;
            }

            resultadoService.Dados = LogLinhaMapper.MapearListaDeEntitadesParaDtos(resultadoRepository.Dados);

            return resultadoService;
        }


        /// <summary>
        /// Cria um novo registro de log de linha.
        /// </summary>
        /// <param name="dto">DTO do log de linha a ser criado.</param>
        /// <returns>Resultado contendo o DTO do log de linha criado ou inconsistências.</returns>
        public async Task<Resultado<LogLinhaDto>> Criar(LogLinhaDto dto)
        {
            var resultadoService = new Resultado<LogLinhaDto>();

            var logLinhaEntity = LogLinhaMapper.MapearDeDtoParaEntidade(dto);

            var resultadoRepository = await LogLinhaRepository.Criar(logLinhaEntity);
            if (!resultadoRepository.Sucesso)
            {
                resultadoService.AdicionarInconsistencias(resultadoRepository.Inconsistencias);
                return resultadoService;
            }
            resultadoService.Dados = LogLinhaMapper.MapearDeEntidadeParaDto(logLinhaEntity);

            return resultadoService;

        }


        /// <summary>
        /// Atualiza um registro de log de linha existente.
        /// </summary>
        /// <param name="dto">DTO do log de linha com as alterações.</param>
        /// <returns>Resultado contendo o DTO atualizado do log de linha ou inconsistências.</returns>
        public async Task<Resultado<LogLinhaDto>> Atualizar(LogLinhaDto dto)
        {
            var resultadoService = new Resultado<LogLinhaDto>();

            var logLinhaEntity = LogLinhaMapper.MapearDeDtoParaEntidade(dto);

            var resultadoRepository = await LogLinhaRepository.Atualizar(logLinhaEntity);
            if (!resultadoRepository.Sucesso)
            {
                resultadoService.AdicionarInconsistencias(resultadoRepository.Inconsistencias);
                return resultadoService;
            }
            resultadoService.Dados = LogLinhaMapper.MapearDeEntidadeParaDto(logLinhaEntity);

            return resultadoService;
        }

        /// <summary>
        /// Deleta um registro de log de linha pelo seu ID.
        /// </summary>
        /// <param name="id">ID do log de linha a ser deletado.</param>
        /// <returns>Resultado indicando o sucesso ou falha da operação.</returns>
        public async Task<Resultado<LogLinhaDto>> Deletar(int id)
        {
            var resultadoService = new Resultado<LogLinhaDto>();

            var resultadoRepository = await LogLinhaRepository.Deletar(id);
            if (!resultadoRepository.Sucesso)
            {
                resultadoService.AdicionarInconsistencias(resultadoRepository.Inconsistencias);
                return resultadoService;
            }

            return resultadoService;
        }

    }
}
