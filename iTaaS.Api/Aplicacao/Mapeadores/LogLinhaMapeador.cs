using iTaaS.Api.Aplicacao.DTOs;
using iTaaS.Api.Aplicacao.Interfaces.Mapeadores;
using iTaaS.Api.Dominio.Entidades;
using System.Collections.Generic;

namespace iTaaS.Api.Aplicacao.Mapeadores
{
    /// <summary>
    /// Classe responsável por realizar a conversão/transferência de dados entre as entidades e os DTOs.
    /// </summary>
    public class LogLinhaMapeador : ILogLinhaMapeador
    {

        /// <summary>
        /// Converte uma entidade do tipo <see cref="LogLinhaEntidade"/> em um DTO do tipo <see cref="LogLinhaDto"/>.
        /// </summary>
        /// <param name="entity">Objeto da entidade <see cref="LogLinhaEntidade"/> a ser convertido.</param>
        /// <returns>Um objeto <see cref="LogLinhaDto"/> correspondente à entidade fornecida ou <c>null</c> se a entidade for <c>null</c>.</returns>
        public LogLinhaDto MapearDeEntidadeParaDto(LogLinhaEntidade entity)
        {
            if (entity == null)
                return null;

            return new LogLinhaDto
            {
                Id = entity.Id,
                LogId = entity.LogId,         
                MetodoHttp = entity.MetodoHttp,
                CodigoStatus = entity.CodigoStatus,
                CaminhoUrl = entity.CaminhoUrl,
                TempoResposta = entity.TempoResposta,
                TamahoResposta = entity.TamahoResposta,
                CacheStatus = entity.CacheStatus
            };
        }

        /// <summary>
        /// Converte um DTO do tipo <see cref="LogLinhaDto"/> em uma entidade do tipo <see cref="LogLinhaEntidade"/>.
        /// </summary>
        /// <param name="dto">Objeto do DTO <see cref="LogLinhaDto"/> a ser convertido.</param>
        /// <returns>Um objeto <see cref="LogLinhaEntidade"/> correspondente ao DTO fornecido ou <c>null</c> se o DTO for <c>null</c>.</returns>

        public LogLinhaEntidade MapearDeDtoParaEntidade(LogLinhaDto dto)
        {
            if (dto == null)
                return null;

            return new LogLinhaEntidade
            {
                Id = dto.Id,
                LogId = dto.LogId,           
                MetodoHttp = dto.MetodoHttp,
                CodigoStatus = dto.CodigoStatus,
                CaminhoUrl = dto.CaminhoUrl,
                TempoResposta = dto.TempoResposta,
                TamahoResposta = dto.TamahoResposta,
                CacheStatus = dto.CacheStatus
            };
        }

        /// <summary>
        /// Converte uma lista de entidades do tipo <see cref="LogLinhaEntidade"/> em uma lista de DTOs do tipo <see cref="LogLinhaDto"/>.
        /// </summary>
        /// <param name="listaEntities">Lista de entidades <see cref="LogLinhaEntidade"/> a ser convertida.</param>
        /// <returns>Uma lista de <see cref="LogLinhaDto"/> correspondente ou <c>null</c> se a lista fornecida for <c>null</c>.</returns>

        public List<LogLinhaDto> MapearListaDeEntitadesParaDtos(List<LogLinhaEntidade> listaEntities)
        {
            if (listaEntities == null)
                return null;

            var listaDtos = new List<LogLinhaDto>();

            foreach (var entity in listaEntities)
                listaDtos.Add(MapearDeEntidadeParaDto(entity));

            return listaDtos;

        }

        /// <summary>
        /// Converte uma lista de DTOs do tipo <see cref="LogLinhaDto"/> em uma lista de entidades do tipo <see cref="LogLinhaEntidade"/>.
        /// </summary>
        /// <param name="listaDtos">Lista de DTOs <see cref="LogLinhaDto"/> a ser convertida.</param>
        /// <returns>Uma lista de <see cref="LogLinhaEntidade"/> correspondente ou <c>null</c> se a lista fornecida for <c>null</c>.</returns>

        public List<LogLinhaEntidade> MapearListaDeDtosParaEntitades(List<LogLinhaDto> listaDtos)
        {
            if (listaDtos == null)
                return null;

            var listaEntities = new List<LogLinhaEntidade>();

            foreach (var dto in listaDtos)
                listaEntities.Add(MapearDeDtoParaEntidade(dto));

            return listaEntities;
        }
    }

}
