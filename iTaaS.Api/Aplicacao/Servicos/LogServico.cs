using iTaaS.Api.Aplicacao.DTOs;
using iTaaS.Api.Aplicacao.Interfaces.Mapeadores;
using iTaaS.Api.Aplicacao.Interfaces.Repositorios;
using iTaaS.Api.Aplicacao.Interfaces.Servicos;
using iTaaS.Api.Dominio.Enumeradores;
using iTaaS.Api.Dominio.Fabricas;
using iTaaS.Api.Dominio.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iTaaS.Api.Aplicacao.Servicos
{
    public class LogServico : ILogServico
    {

        const string SEPARADOR_UNDERLINE = "_";
        const int INDEX_ID_LOG = 0;

        private readonly ILogRepositorio LogRepository;
        private readonly ILogMapeador LogMapper;

        public LogServico(ILogRepositorio logRepository, ILogMapeador logMapper)
        {
            LogRepository = logRepository;
            LogMapper = logMapper;
        }

        public async Task<Resultado<LogDto>> ObterPorId(int id)
        {
            var resultadoService = new Resultado<LogDto>();

            var resultadoRepository = await LogRepository.ObterPorId(id);
            if (!resultadoRepository.Sucesso)
            {
                resultadoService.Inconsistencias = resultadoRepository.Inconsistencias;
                return resultadoService;
            }

            resultadoService.Dados = LogMapper.ConverterParaDto(resultadoRepository.Dados);

            return resultadoService;
        }

        public async Task<Resultado<List<LogDto>>> ObterLista()
        {
            var resultadoService = new Resultado<List<LogDto>>();

            var resultadoRepository = await LogRepository.ObterLista();
            if (!resultadoRepository.Sucesso)
            {
                resultadoService.Inconsistencias = resultadoRepository.Inconsistencias;
                return resultadoService;
            }

            resultadoService.Dados = LogMapper.ConverterListaParaDto(resultadoRepository.Dados);

            return resultadoService;
        }

        public async Task<Resultado<LogDto>> Criar(LogDto logDto)
        {
            var resultadoService = new Resultado<LogDto>();

            var logEntity = LogMapper.ConverterParaEntity(logDto);

            logEntity.Hash = Guid.NewGuid().ToString();

            var resultadoRepository = await LogRepository.Criar(logEntity);
            if (!resultadoRepository.Sucesso)
            {
                resultadoService.Inconsistencias = resultadoRepository.Inconsistencias;
                return resultadoService;
            }

            resultadoService.Dados = LogMapper.ConverterParaDto(logEntity);

            return resultadoService;
        }

        public async Task<Resultado<LogDto>> Atualizar(LogDto logDto)
        {
            var resultadoService = new Resultado<LogDto>();

            var entity = LogMapper.ConverterParaEntity(logDto);

            var resultadoRepository = await LogRepository.Atualizar(entity);
            if (!resultadoRepository.Sucesso)
            {
                resultadoService.Inconsistencias = resultadoRepository.Inconsistencias;
                return resultadoService;
            }

            resultadoService.Dados = LogMapper.ConverterParaDto(entity);

            return resultadoService;
        }

        public async Task<Resultado<LogDto>> Deletar(int id)
        {
            var resultadoService = new Resultado<LogDto>();

            var resultadoRepository = await LogRepository.Deletar(id);
            if (!resultadoRepository.Sucesso)
            {
                resultadoService.Inconsistencias = resultadoRepository.Inconsistencias;
                return resultadoService;
            }

            return resultadoService;
        }


        //public async Task<Resultado<LogDto>> CriarLogComLinhas(LogDto logDto)
        //{
        //    var resultadoCriarLogComLinhas = new Resultado<LogDto>();

        //    var resultadoCriarLog = await Criar(logDto);

        //    //foreach (var logLinha in resultadoCriarLog.Dados.Linhas)
        //    //{
        //    //    var entityLogLinha = this.LogMapper.ConverterParaEntity(logLinha);

        //    //    LogLinhaRepository.Criar(logLinhs);
        //    //}

        //    return resultadoCriarLogComLinhas;
        //}


        public async Task<Resultado<string>> ImportarPorUrl(string url, TipoRetornoTranformacao tipoRetornoTranformacao)
        {
            var resultado = new Resultado<string>();

            var conversorLog = ConverterLogFabrica.ObterConversor(TipoFormatoLog.MINHA_CDN);
            var resultadoConversaoUrlDto = conversorLog.ConverterDeUrlParaDto(url);
            if (!resultadoConversaoUrlDto.Sucesso)
            {
                resultado.Inconsistencias = resultadoConversaoUrlDto.Inconsistencias;
                return resultado;
            }

            var logDto = resultadoConversaoUrlDto.Dados;
            var resultadoCriar = await Criar(logDto);
            if (!resultadoCriar.Sucesso)
            {
                resultado.Inconsistencias = resultadoCriar.Inconsistencias;
                return resultado;
            }

            var resultadoConversaoDtoArquivo = new Resultado<string>();
            if (tipoRetornoTranformacao == TipoRetornoTranformacao.RETORNAR_PATCH)
            {
                conversorLog = ConverterLogFabrica.ObterConversor(TipoFormatoLog.AGORA);
                resultadoConversaoDtoArquivo = conversorLog.ConverterDeDtoParaArquivo(resultadoCriar.Dados);
            }
            else if (tipoRetornoTranformacao == TipoRetornoTranformacao.RETORNAR_ARQUIVO)
            {
                conversorLog = ConverterLogFabrica.ObterConversor(TipoFormatoLog.AGORA);
                resultadoConversaoDtoArquivo = conversorLog.ConverterDeDtoParaString(resultadoCriar.Dados);
            }

            if (!resultadoConversaoDtoArquivo.Sucesso)
            {
                resultado.Inconsistencias = resultadoConversaoDtoArquivo.Inconsistencias;
                return resultado;
            }

            resultado.Dados = resultadoConversaoDtoArquivo.Dados;

            return resultado;

        }

        public async Task<Resultado<string>> ImportarPorId(int id, TipoRetornoTranformacao tipoRetornoTranformacao)
        {
            var resultado = new Resultado<string>();

            var conversorLog = ConverterLogFabrica.ObterConversor(TipoFormatoLog.AGORA);

            var resultadoObtenhaPorId = await ObterPorId(id);
            if (!resultadoObtenhaPorId.Sucesso)
            {
                resultado.Inconsistencias = resultadoObtenhaPorId.Inconsistencias;
                return resultado;
            }

            var resultadoConversaoDtoArquivo = new Resultado<string>();
            if (tipoRetornoTranformacao == TipoRetornoTranformacao.RETORNAR_PATCH)
            {
                resultadoConversaoDtoArquivo = conversorLog.ConverterDeDtoParaArquivo(resultadoObtenhaPorId.Dados);
            }
            else if (tipoRetornoTranformacao == TipoRetornoTranformacao.RETORNAR_ARQUIVO)
            {
                resultadoConversaoDtoArquivo = conversorLog.ConverterDeDtoParaString(resultadoObtenhaPorId.Dados);
            }

            if (!resultadoConversaoDtoArquivo.Sucesso)
            {
                resultado.Inconsistencias = resultadoConversaoDtoArquivo.Inconsistencias;
                return resultado;
            }

            resultado.Dados = resultadoConversaoDtoArquivo.Dados;

            return resultado;

        }

        public async Task<Resultado<string>> VerPorNomeArquivo(string nomeArquivo)
        {
            var resultado = new Resultado<string>();

            var resultadoCamposNome = UtilitarioHelper.ConverterStringEmListaPorDelimitador(nomeArquivo, SEPARADOR_UNDERLINE);
            if (!resultadoCamposNome.Sucesso)
            {
                resultado.AdicionarInconsistencia("ERRO_FORMATO", "O nome do arquivo está em um formato inválido!");
                return resultado;
            }

            var listaCamposNomeArquivo = resultadoCamposNome.Dados;
            var id = UtilitarioHelper.ConverterStringParaInt(listaCamposNomeArquivo[INDEX_ID_LOG]);

            var resultadoObtenhaPorId = await ObterPorId(id);
            if (!resultadoObtenhaPorId.Sucesso)
            {
                resultado.Inconsistencias = resultadoObtenhaPorId.Inconsistencias;
                return resultado;
            }

            var conversorLog = ConverterLogFabrica.ObterConversor(TipoFormatoLog.AGORA);


            var caminhoArquivo = $"{UtilitarioHelper.ObtenhaCaminhoDiretorioPorDataHora(resultadoObtenhaPorId.Dados.DataHoraRecebimento)}\\{nomeArquivo.Replace(".txt", "")}.txt";
            var resultadoArquivoString = conversorLog.ConverterDeArquivoParaString(caminhoArquivo);
            if (!resultadoArquivoString.Sucesso)
                resultado.Inconsistencias = resultadoArquivoString.Inconsistencias;


            resultado.Dados = resultadoArquivoString.Dados;

            return resultado;

        }

    }

}
