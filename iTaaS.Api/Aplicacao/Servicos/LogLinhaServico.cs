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
        /// Obtém um log pelo seu identificador único.
        /// </summary>
        /// <param name="id">Identificador único do log.</param>
        /// <returns>Resultado contendo a dto <see cref="LogLinhaDto"/> encontrada ou uma inconsistência.</returns>
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
        /// Obtém uma lista de todos os logs armazenados.
        /// </summary>
        /// <returns>Resultado contendo uma lista dtos <see cref="LogLinhaDto"/> encontrada ou uma inconsistência.</returns>
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
        /// Cria um novo log.
        /// </summary>
        /// <param name="dtoLogLinha">Objeto DTO contendo os dados do log a ser criado.</param>
        /// <returns>Resultado contendo a dto <see cref="LogDto"/> encontrada ou uma inconsistência.</returns>

        public async Task<Resultado<LogLinhaDto>> Criar(LogLinhaDto dtoLogLinha)
        {
            var resultadoService = new Resultado<LogLinhaDto>();

            var logLinhaEntity = LogLinhaMapper.MapearDeDtoParaEntidade(dtoLogLinha);

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
        /// Atualiza um log existente com os dados fornecidos.
        /// </summary>
        /// <param name="dtoLogLinha">Objeto DTO com os dados atualizados do log.</param>
        /// <returns>Resultado contendo a dto <see cref="Atualizar"/> encontrada ou uma inconsistência.</returns>
        public async Task<Resultado<LogLinhaDto>> Atualizar(LogLinhaDto dtoLogLinha)
        {
            var resultadoService = new Resultado<LogLinhaDto>();

            var logLinhaEntity = LogLinhaMapper.MapearDeDtoParaEntidade(dtoLogLinha);

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
        /// Deleta um log com base no identificador fornecido.
        /// </summary>
        /// <param name="id">Identificador único do log a ser deletado.</param>
        /// <returns>Resultado contendo a dto <see cref="LogLinhaDto"/> encontrada ou uma inconsistência.</returns>
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
