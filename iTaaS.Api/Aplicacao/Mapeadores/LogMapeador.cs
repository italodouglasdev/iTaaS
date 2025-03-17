using iTaaS.Api.Aplicacao.DTOs;
using iTaaS.Api.Aplicacao.Interfaces.Mapeadores;
using iTaaS.Api.Dominio.Entidades;
using System.Collections.Generic;
using System.Linq;

namespace iTaaS.Api.Aplicacao.Mapeadores
{
    /// <summary>
    /// Classe responsável por realizar a conversão/transferência de dados entre as entidades e os DTOs.
    /// </summary>
    public class LogMapeador : ILogMapeador
    {

        private readonly ILogLinhaMapeador LogLinhaMapper;

        public LogMapeador(ILogLinhaMapeador logLinhaMapper)
        {
            this.LogLinhaMapper = logLinhaMapper;
        }



        /// <summary>
        /// Converte uma entidade do tipo <see cref="LogEntidade"/> em um DTO do tipo <see cref="LogDto"/>.
        /// </summary>
        /// <param name="entity">Objeto da entidade <see cref="LogEntidade"/> a ser convertido.</param>
        /// <returns>Um objeto <see cref="LogDto"/> correspondente à entidade fornecida ou <c>null</c> se a entidade for <c>null</c>.</returns>

        public LogDto MapearDeEntidadeParaDto(LogEntidade entity)
        {
            if (entity == null)
                return null;

            return new LogDto
            {
                Id = entity.Id,
                Hash = entity.Hash,
                Versao = entity.Versao,
                UrlOrigem = entity.UrlOrigem,
                DataHoraRecebimento = entity.DataHoraRecebimento,
                Linhas = LogLinhaMapper.MapearListaDeEntitadesParaDtos(entity.Linhas.ToList())
            };
        }

        /// <summary>
        /// Converte um DTO do tipo <see cref="LogDto"/> em uma entidade do tipo <see cref="LogEntidade"/>.
        /// </summary>
        /// <param name="dto">Objeto do DTO <see cref="LogDto"/> a ser convertido.</param>
        /// <returns>Um objeto <see cref="LogEntidade"/> correspondente ao DTO fornecido ou <c>null</c> se o DTO for <c>null</c>.</returns>

        public LogEntidade MapearDeDtoParaEntidade(LogDto dto)
        {
            if (dto == null)
                return null;

            return new LogEntidade
            {
                Id = dto.Id,
                Hash = dto.Hash,
                Versao = dto.Versao,
                UrlOrigem = dto.UrlOrigem,
                DataHoraRecebimento = dto.DataHoraRecebimento,
                Linhas = LogLinhaMapper.MapearListaDeDtosParaEntitades(dto.Linhas.ToList())
            };
        }

        /// <summary>
        /// Converte uma lista de entidades do tipo <see cref="LogEntidade"/> em uma lista de DTOs do tipo <see cref="LogDto"/>.
        /// </summary>
        /// <param name="listaEntities">Lista de entidades <see cref="LogEntidade"/> a ser convertida.</param>
        /// <returns>Uma lista de <see cref="LogDto"/> correspondente ou <c>null</c> se a lista fornecida for <c>null</c>.</returns>

        public List<LogDto> MapearListaDeEntitadesParaDtos(List<LogEntidade> listaEntities)
        {
            if (listaEntities == null)
                return null;

            var listaDtos = new List<LogDto>();

            foreach (var entity in listaEntities)
                listaDtos.Add(MapearDeEntidadeParaDto(entity));

            return listaDtos;

        }

        /// <summary>
        /// Converte uma lista de DTOs do tipo <see cref="LogDto"/> em uma lista de entidades do tipo <see cref="LogEntidade"/>.
        /// </summary>
        /// <param name="listaDtos">Lista de DTOs <see cref="LogDto"/> a ser convertida.</param>
        /// <returns>Uma lista de <see cref="LogEntidade"/> correspondente ou <c>null</c> se a lista fornecida for <c>null</c>.</returns>

        public List<LogEntidade> MapearListaDeDtosParaEntitades(List<LogDto> listaDtos)
        {
            if (listaDtos == null)
                return null;

            var listaEntities = new List<LogEntidade>();

            foreach (var dto in listaDtos)
                listaEntities.Add(MapearDeDtoParaEntidade(dto));

            return listaEntities;
        }

    }
}
