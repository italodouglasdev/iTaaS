using iTaaS.Api.Aplicacao.DTOs;
using iTaaS.Api.Aplicacao.Interfaces.Mapeadores;
using iTaaS.Api.Aplicacao.Interfaces.Repositorios;
using iTaaS.Api.Aplicacao.Interfaces.Servicos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iTaaS.Api.Aplicacao.Servicos
{
    public class LogLinhaServico : ILogLinhaServico
    {

        private readonly ILogLinhaRepositorio LogLinhaRepository;
        private readonly ILogLinhaMapeador LogLinhaMapper;

        public LogLinhaServico(ILogLinhaRepositorio logLinhaRepository, ILogLinhaMapeador logLinhaMapper)
        {
            LogLinhaRepository = logLinhaRepository;
            LogLinhaMapper = logLinhaMapper;
        }


        public async Task<Resultado<LogLinhaDto>> ObterPorId(int id)
        {
            var resultadoService = new Resultado<LogLinhaDto>();

            var resultadoRepository = await LogLinhaRepository.ObterPorId(id);
            if (!resultadoRepository.Sucesso)
            {
                resultadoService.Inconsistencias = resultadoRepository.Inconsistencias;
                return resultadoService;
            }

            resultadoService.Dados = LogLinhaMapper.ConverterParaDto(resultadoRepository.Dados);

            return resultadoService;
        }

        public async Task<Resultado<List<LogLinhaDto>>> ObterLista()
        {
            var resultadoService = new Resultado<List<LogLinhaDto>>();

            var resultadoRepository = await LogLinhaRepository.ObterLista();
            if (!resultadoRepository.Sucesso)
            {
                resultadoService.Inconsistencias = resultadoRepository.Inconsistencias;
                return resultadoService;
            }

            resultadoService.Dados = LogLinhaMapper.ConverterListaParaDto(resultadoRepository.Dados);

            return resultadoService;
        }

        public async Task<Resultado<LogLinhaDto>> Criar(LogLinhaDto dto)
        {
            var resultadoService = new Resultado<LogLinhaDto>();

            var logLinhaEntity = LogLinhaMapper.ConverterParaEntity(dto);

            var resultadoRepository = await LogLinhaRepository.Criar(logLinhaEntity);
            if (!resultadoRepository.Sucesso)
            {
                resultadoService.Inconsistencias = resultadoRepository.Inconsistencias;
                return resultadoService;
            }
            resultadoService.Dados = LogLinhaMapper.ConverterParaDto(logLinhaEntity);

            return resultadoService;

        }

        public async Task<Resultado<LogLinhaDto>> Atualizar(LogLinhaDto dto)
        {
            var resultadoService = new Resultado<LogLinhaDto>();

            var logLinhaEntity = LogLinhaMapper.ConverterParaEntity(dto);

            var resultadoRepository = await LogLinhaRepository.Atualizar(logLinhaEntity);
            if (!resultadoRepository.Sucesso)
            {
                resultadoService.Inconsistencias = resultadoRepository.Inconsistencias;
                return resultadoService;
            }
            resultadoService.Dados = LogLinhaMapper.ConverterParaDto(logLinhaEntity);

            return resultadoService;
        }

        public async Task<Resultado<LogLinhaDto>> Deletar(int id)
        {
            var resultadoService = new Resultado<LogLinhaDto>();

            var resultadoRepository = await LogLinhaRepository.Deletar(id);
            if (!resultadoRepository.Sucesso)
            {
                resultadoService.Inconsistencias = resultadoRepository.Inconsistencias;
                return resultadoService;
            }

            return resultadoService;
        }

    }
}
