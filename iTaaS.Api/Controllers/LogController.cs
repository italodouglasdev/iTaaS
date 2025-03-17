using iTaaS.Api.Aplicacao.DTOs;
using iTaaS.Api.Aplicacao.Interfaces.Servicos;
using iTaaS.Api.Dominio.Enumeradores;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Threading.Tasks;

namespace iTaaS.Api.Controllers
{
    /// <summary>
    /// EndPoints responsáveis por realizar a gestão dos Logs.
    /// </summary>
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
        /// Importa e transforma um log a partir de uma URL.
        /// </summary>
        /// <param name="url">URL do log a ser importado.</param>
        /// <param name="TipoRetornoLog">Tipo do retorno do log ([0] ARQUIVO | [1] PATCH | [2] JSON).</param>
        /// <returns>Retorna o log transformado como conteúdo de texto plano ou erros de inconsistência.</returns>
        [HttpPost("TranformarLogUrl")]
        public async Task<ActionResult<LogDto>> TranformarLogUrl(string Url, TipoRetornoLog TipoLogRetorno)
        {
            var resultadoServico = await LogService.ImportarPorUrl(Url, TipoLogRetorno);

            if (resultadoServico.Sucesso)
            {

                return Content(resultadoServico.Dados, "text/plain", Encoding.UTF8);

            }
            else
            {
                return BadRequest(resultadoServico.Inconsistencias);
            }

        }


        /// <summary>
        /// Importa e transforma um log a partir de um identificador.
        /// </summary>
        /// <param name="Id">Identificador do log.</param>
        /// <param name="TipoRetornoLog">Tipo do retorno do log ([0] ARQUIVO | [1] PATCH | [2] JSON).</param>
        /// <returns>Retorna o log transformado como conteúdo de texto plano ou erros de inconsistência.</returns>
        [HttpPost("TranformarLogId")]
        public async Task<ActionResult<LogDto>> TranformarLogId(int Id, TipoRetornoLog TipoLogRetorno)
        {
            var resultadoServico = await LogService.ImportarPorId(Id, TipoLogRetorno);

            if (resultadoServico.Sucesso)
            {
                return Content(resultadoServico.Dados, "text/plain", Encoding.UTF8);

            }
            else
            {
                return BadRequest(resultadoServico.Inconsistencias);
            }

        }       


        /// <summary>
        /// Busca logs salvos com base em diversos filtros.
        /// </summary>
        /// <param name="DataHoraRecebimentoInicio">Data e hora inicial do recebimento do log.</param>
        /// <param name="DataHoraRecebimentoFim">Data e hora final do recebimento do log.</param>
        /// <param name="MetodoHttp">Método HTTP utilizado na requisição (GET, POST, etc.).</param>
        /// <param name="CodigoStatus">Código de status HTTP retornado.</param>
        /// <param name="CaminhoUrl">URL acessada na requisição.</param>
        /// <param name="TempoRespostaInicial">Tempo mínimo de resposta da requisição (em milissegundos).</param>
        /// <param name="TempoRespostaFinal">Tempo máximo de resposta da requisição (em milissegundos).</param>
        /// <param name="TamanhoRespostaInicial">Tamanho mínimo da resposta em bytes.</param>
        /// <param name="TamanhoRespostaFinal">Tamanho máximo da resposta em bytes.</param>
        /// <param name="CashStatus">Status do cache utilizado na requisição.</param>
        /// <param name="TipoRetornoLog">Tipo do retorno do log ([0] ARQUIVO | [1] PATCH | [2] JSON).</param>
        /// <returns>Retorna a lista de logs filtrados ou erros de inconsistência.</returns>
        [HttpGet("BuscarSalvos")]
        public async Task<ActionResult<LogDto>> BuscarSalvos(string DataHoraRecebimentoInicio, string DataHoraRecebimentoFim, string MetodoHttp, int CodigoStatus, string CaminhoUrl, decimal TempoRespostaInicial, decimal TempoRespostaFinal, int TamanhoRespostaInicial, int TamanhoRespostaFinal, string CashStatus, TipoRetornoLog TipoRetornoLog)
        {
            var resultadoServico = await LogService.ObterLogsFiltrados(
                DataHoraRecebimentoInicio,
                DataHoraRecebimentoFim,
                MetodoHttp,
                CodigoStatus,
                CaminhoUrl,
                TempoRespostaInicial,
                TempoRespostaFinal,
                TamanhoRespostaInicial,
                TamanhoRespostaFinal,
                CashStatus,
                TipoRetornoLog);

            if (resultadoServico.Sucesso)
            {
                return Content(string.Join(Environment.NewLine, resultadoServico.Dados), "text/plain", Encoding.UTF8);
            }
            else
            {
                return BadRequest(resultadoServico.Inconsistencias);
            }

        }


        /// <summary>
        /// Busca logs salvos com base em diversos filtros.
        /// </summary>
        /// <param name="DataHoraRecebimentoInicio">Data e hora inicial do recebimento do log.</param>
        /// <param name="DataHoraRecebimentoFim">Data e hora final do recebimento do log.</param>
        /// <param name="MetodoHttp">Método HTTP utilizado na requisição (GET, POST, etc.).</param>
        /// <param name="CodigoStatus">Código de status HTTP retornado.</param>
        /// <param name="CaminhoUrl">URL acessada na requisição.</param>
        /// <param name="TempoRespostaInicial">Tempo mínimo de resposta da requisição (em milissegundos).</param>
        /// <param name="TempoRespostaFinal">Tempo máximo de resposta da requisição (em milissegundos).</param>
        /// <param name="TamanhoRespostaInicial">Tamanho mínimo da resposta em bytes.</param>
        /// <param name="TamanhoRespostaFinal">Tamanho máximo da resposta em bytes.</param>
        /// <param name="CashStatus">Status do cache utilizado na requisição.</param>
        /// <param name="TipoRetornoLog">Tipo do retorno do log ([0] ARQUIVO | [1] PATCH | [2] JSON).</param>
        /// <returns>Retorna a lista de logs filtrados ou erros de inconsistência.</returns>
        [HttpGet("BuscarTransformados")]
        public async Task<ActionResult<LogDto>> BuscarTransformados(string DataHoraRecebimentoInicio, string DataHoraRecebimentoFim, string MetodoHttp, int CodigoStatus, string CaminhoUrl, decimal TempoRespostaInicial, decimal TempoRespostaFinal, int TamanhoRespostaInicial, int TamanhoRespostaFinal, string CashStatus, TipoRetornoLog TipoRetornoLog)
        {
            var resultadoServico = await LogService.ObterLogsTransformados(
                DataHoraRecebimentoInicio,
                DataHoraRecebimentoFim,
                MetodoHttp,
                CodigoStatus,
                CaminhoUrl,
                TempoRespostaInicial,
                TempoRespostaFinal,
                TamanhoRespostaInicial,
                TamanhoRespostaFinal,
                CashStatus,
                TipoRetornoLog);

            if (resultadoServico.Sucesso)
            {
                return Content(string.Join(Environment.NewLine, resultadoServico.Dados), "text/plain", Encoding.UTF8);
            }
            else
            {
                return BadRequest(resultadoServico.Inconsistencias);
            }

        }


        /// <summary>
        /// Busca um log salvo a partir do identificador.
        /// </summary>
        /// <param name="Id">Identificador do log.</param>
        /// <param name="tipoRetornoLog">Tipo de retorno esperado.</param>
        /// <returns>Retorna o log salvo ou erros de inconsistência.</returns>
        [HttpGet("BuscarSalvoId/{identificador}")]
        public async Task<ActionResult<LogDto>> BuscarSalvoId([FromRoute] int identificador, [FromQuery] TipoRetornoLog tipoRetornoLog)
        {
            var resultadoServico = await LogService.ObtenhaPorIdentificador(identificador, tipoRetornoLog);

            if (resultadoServico.Sucesso)
            {
                return Content(string.Join(Environment.NewLine, resultadoServico.Dados), "text/plain", Encoding.UTF8);
            }
            else
            {
                return BadRequest(resultadoServico.Inconsistencias);
            }

        }


        /// <summary>
        /// Busca um log transformado a partir do identificador.
        /// </summary>
        /// <param name="Id">Identificador do log.</param>
        /// <param name="tipoRetornoLog">Tipo de retorno esperado.</param>
        /// <returns>Retorna o log transformado ou erros de inconsistência.</returns>
        [HttpGet("BuscarTransformadoId/{identificador}")]
        public async Task<ActionResult<LogDto>> BuscarTransformadoId(int identificador, TipoRetornoLog tipoRetornoLog)
        {
            var resultadoServico = await LogService.ObtenhaTransformadoPorIdentificador(identificador, tipoRetornoLog);

            if (resultadoServico.Sucesso)
            {
                return Content(string.Join(Environment.NewLine, resultadoServico.Dados), "text/plain", Encoding.UTF8);
            }
            else
            {
                return BadRequest(resultadoServico.Inconsistencias);
            }

        }


        /// <summary>
        /// Cria um novo log no sistema.
        /// </summary>
        /// <param name="logDto">Objeto contendo as informações do log a ser criado.</param>
        /// <returns>Retorna um status de sucesso ou erro caso os dados sejam inválidos.</returns>
        [HttpPost("Criar")]
        public async Task<ActionResult<LogDto>> Criar([FromBody] LogDto logDto)
        {       
            var resultadoServico = await this.LogService.Criar(logDto);

            if (resultadoServico.Sucesso)
            {
                return Ok(resultadoServico.Dados);
            }
            else
            {
                return BadRequest(resultadoServico.Inconsistencias);
            }

           
        }


        /// <summary>
        /// Atualiza um log existente no sistema.
        /// </summary>
        /// <param name="logDto">Objeto contendo os dados atualizados do log.</param>
        /// <returns>Retorna o log atualizado ou um erro caso os dados sejam inválidos.</returns>
        [HttpPut("Salvar")]
        public async Task<ActionResult<LogDto>> Atualizar([FromBody] LogDto logDto)
        {         
            var resultadoServico = await this.LogService.Atualizar(logDto);

            if (resultadoServico.Sucesso)
            {
                return Ok(resultadoServico.Dados);
            }
            else
            {
                return BadRequest(resultadoServico.Inconsistencias);
            }
        }


        /// <summary>
        /// Deleta um log do sistema com base no identificador fornecido.
        /// </summary>
        /// <param name="id">Identificador único do log a ser deletado.</param>
        /// <returns>Retorna o log deletado ou um erro caso não seja encontrado.</returns>
        [HttpDelete("Deletar/{id}")]
        public async Task<ActionResult<LogDto>> Deletar(int id)
        {
            var resultadoServico = await this.LogService.Deletar(id);

            if (resultadoServico.Sucesso)
            {
                return Ok(resultadoServico.Dados);
            }
            else
            {
                return BadRequest(resultadoServico.Inconsistencias);
            }

        }
    }
}
