using iTaaS.Api.Aplicacao.DTOs;
using iTaaS.Api.Aplicacao.Interfaces.Servicos;
using iTaaS.Api.Dominio.Enumeradores;
using Microsoft.AspNetCore.Mvc;
using System;
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
        /// Teste descrição Swagger
        /// </summary>
        /// <param name="url">Url contendo o arquivo de log para transformação.</param>
        /// <param name="tipoRetornoTranformacao">Tipo de retorno da tranformação. [0] Log em texto, [1] Url do log transformado</param>
        /// <returns></returns>
        [HttpPost("TranformarLogUrl")]
        public async Task<ActionResult<LogDto>> TranformarLogUrl(string url, TipoRetornoTranformacao tipoRetornoTranformacao)
        {
            var resultado = await LogService.ImportarPorUrl(url, tipoRetornoTranformacao);

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
        public async Task<ActionResult<LogDto>> TranformarLogId(int Id, TipoRetornoTranformacao tipoRetornoTranformacao)
        {
            var resultado = await LogService.ImportarPorId(Id, tipoRetornoTranformacao);

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
        public async Task<ActionResult<LogDto>> BuscarSalvos(string DataHoraRecebimentoInicio, string DataHoraReceimentoFim, string MetodoHttp, int CodigoStatus, string CaminhoUrl, decimal TempoRespostaInicial, decimal TempoRespostaFinal, int TamanhoRespostaInicial, int TamanhoRespostaFinal, string CashStatus, TipoFormatoExibicaoLog tipoFormatoExibicaoLog)
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
                tipoFormatoExibicaoLog);

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
        public async Task<ActionResult<LogDto>> BuscarTransformados(string DataHoraRecebimentoInicio, string DataHoraReceimentoFim, string MetodoHttp, int CodigoStatus, string CaminhoUrl, decimal TempoRespostaInicial, decimal TempoRespostaFinal, int TamanhoRespostaInicial, int TamanhoRespostaFinal, string CashStatus, TipoFormatoExibicaoLog tipoFormatoExibicaoLog)
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
                tipoFormatoExibicaoLog);

            if (resultado.Sucesso)
            {
                return Content(string.Join(Environment.NewLine, resultado.Dados), "text/plain", Encoding.UTF8);
            }
            else
            {
                return BadRequest(resultado.Inconsistencias);
            }

        }




        //[HttpGet("{id}")]
        //public async Task<ActionResult<LogDto>> ObterPorId(int id)
        //{
        //    var log = await this.LogService.ObterPorId(id);

        //    if (log == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(log);
        //}

        //[HttpGet]
        //public async Task<ActionResult<List<LogDto>>> ObterLista()
        //{
        //    var logs = await this.LogService.ObterLista();
        //    return Ok(logs);
        //}

        //[HttpPost]
        //public async Task<ActionResult<LogDto>> Criar([FromBody] LogDto logDto)
        //{
        //    if (logDto == null)
        //    {
        //        return BadRequest("Dados inválidos.");
        //    }

        //    var logCriado = await this.LogService.Criar(logDto);

        //    return Ok();
        //}

        //[HttpPut]
        //public async Task<ActionResult<LogDto>> Atualizar([FromBody] LogDto logDto)
        //{
        //    if (logDto == null)
        //    {
        //        return BadRequest("Dados inválidos.");
        //    }

        //    var logAtualizado = await this.LogService.Atualizar(logDto);
        //    return Ok(logAtualizado);
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult<LogDto>> Deletar(int id)
        //{
        //    var logDeletado = await this.LogService.Deletar(id);
        //    return Ok(logDeletado);
        //}
    }
}
