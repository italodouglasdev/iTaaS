using iTaaS.Api.Aplicacao.DTOs;
using iTaaS.Api.Aplicacao.Interfaces.Servicos;
using iTaaS.Api.Dominio.Enumeradores;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iTaaS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogServico LogService;

        public LogController(ILogServico logService)
        {
            this.LogService = logService;
        }


        /// <summary>
        /// Realiza a transformação de um log disponível em uma url
        /// </summary>
        /// <param name="url">Url em que o log está localizado</param>
        /// <param name="tipoLogRetorno"></param>
        /// <returns></returns>
        [HttpPost("TranformarLogUrl")]
        public async Task<ActionResult<LogDto>> TranformarLogUrl(string url, TipoRetornoLog tipoLogRetorno)
        {
            var resultado = await LogService.ImportarPorUrl(url, tipoLogRetorno);

            if (resultado.Sucesso)
            {

                return Content(resultado.Dados, "text/plain", Encoding.UTF8);

            }
            else
            {
                return BadRequest(resultado.Inconsistencias);
            }

        }


        
        [HttpPost("TranformarLogId")]
        public async Task<ActionResult<LogDto>> TranformarLogId(int Id, TipoRetornoLog tipoLogRetorno)
        {
            var resultado = await LogService.ImportarPorId(Id, tipoLogRetorno);

            if (resultado.Sucesso)
            {
                return Content(resultado.Dados, "text/plain", Encoding.UTF8);

            }
            else
            {
                return BadRequest(resultado.Inconsistencias);
            }

        }

        [HttpGet("Ver/{nomeArquivo}")]
        public async Task<ActionResult<LogDto>> Ver(string nomeArquivo)
        {
            var resultado = await LogService.VerPorNomeArquivo(nomeArquivo);

            if (resultado.Sucesso)
            {
                return Content(string.Join(Environment.NewLine, resultado.Dados), "text/plain", Encoding.UTF8);
            }
            else
            {
                return BadRequest(resultado.Inconsistencias);
            }

        }

        [HttpGet("BuscarSalvos")]
        public async Task<ActionResult<LogDto>> BuscarSalvos(string DataHoraRecebimentoInicio, string DataHoraReceimentoFim, string MetodoHttp, int CodigoStatus, string CaminhoUrl, decimal TempoRespostaInicial, decimal TempoRespostaFinal, int TamanhoRespostaInicial, int TamanhoRespostaFinal, string CashStatus, TipoRetornoLog tipoRetornoLog)
        {
            var resultado = await LogService.ObterLogsFiltrados(
                DataHoraRecebimentoInicio,
                DataHoraReceimentoFim,
                MetodoHttp,
                CodigoStatus,
                CaminhoUrl,
                TempoRespostaInicial,
                TempoRespostaFinal,
                TamanhoRespostaInicial,
                TamanhoRespostaFinal,
                CashStatus,
                tipoRetornoLog);

            if (resultado.Sucesso)
            {
                return Content(string.Join(Environment.NewLine, resultado.Dados), "text/plain", Encoding.UTF8);
            }
            else
            {
                return BadRequest(resultado.Inconsistencias);
            }

        }

        [HttpGet("BuscarTransformados")]
        public async Task<ActionResult<LogDto>> BuscarTransformados(string DataHoraRecebimentoInicio, string DataHoraReceimentoFim, string MetodoHttp, int CodigoStatus, string CaminhoUrl, decimal TempoRespostaInicial, decimal TempoRespostaFinal, int TamanhoRespostaInicial, int TamanhoRespostaFinal, string CashStatus, TipoRetornoLog tipoRetornoLog)
        {
            var resultado = await LogService.ObterLogsTransformados(
                DataHoraRecebimentoInicio,
                DataHoraReceimentoFim,
                MetodoHttp,
                CodigoStatus,
                CaminhoUrl,
                TempoRespostaInicial,
                TempoRespostaFinal,
                TamanhoRespostaInicial,
                TamanhoRespostaFinal,
                CashStatus,
                tipoRetornoLog);

            if (resultado.Sucesso)
            {
                return Content(string.Join(Environment.NewLine, resultado.Dados), "text/plain", Encoding.UTF8);
            }
            else
            {
                return BadRequest(resultado.Inconsistencias);
            }

        }

        [HttpGet("BuscarSalvoId/{Id}")]
        public async Task<ActionResult<LogDto>> BuscarSalvoId(int Id, TipoRetornoLog tipoRetornoLog)
        {
            var resultado = await LogService.ObtenhaPorIdentificador(
                Id,
                tipoRetornoLog);

            if (resultado.Sucesso)
            {
                return Content(string.Join(Environment.NewLine, resultado.Dados), "text/plain", Encoding.UTF8);
            }
            else
            {
                return BadRequest(resultado.Inconsistencias);
            }

        }

        [HttpGet("BuscarTransformadoId/{Id}")]
        public async Task<ActionResult<LogDto>> BuscarTransformadoId(int Id, TipoRetornoLog tipoRetornoLog)
        {
            var resultado = await LogService.ObtenhaTransformadoPorIdentificador(
                Id,
                tipoRetornoLog);

            if (resultado.Sucesso)
            {
                return Content(string.Join(Environment.NewLine, resultado.Dados), "text/plain", Encoding.UTF8);
            }
            else
            {
                return BadRequest(resultado.Inconsistencias);
            }

        }



             

        [HttpPost("Criar")]
        public async Task<ActionResult<LogDto>> Criar([FromBody] LogDto logDto)
        {
            if (logDto == null)
            {
                return BadRequest("Dados inválidos.");
            }

            var logCriado = await this.LogService.Criar(logDto);

            return Ok();
        }

        [HttpPut("Salvar")]
        public async Task<ActionResult<LogDto>> Atualizar([FromBody] LogDto logDto)
        {
            if (logDto == null)
            {
                return BadRequest("Dados inválidos.");
            }

            var logAtualizado = await this.LogService.Atualizar(logDto);
            return Ok(logAtualizado);
        }

        [HttpDelete("Deletar/{id}")]
        public async Task<ActionResult<LogDto>> Deletar(int id)
        {
            var logDeletado = await this.LogService.Deletar(id);
            return Ok(logDeletado);
        }
    }
}
