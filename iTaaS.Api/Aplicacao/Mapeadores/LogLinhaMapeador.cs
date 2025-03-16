using iTaaS.Api.Aplicacao.DTOs;
using iTaaS.Api.Aplicacao.Interfaces.Mapeadores;
using iTaaS.Api.Dominio.Entidades;
using System.Collections.Generic;

namespace iTaaS.Api.Aplicacao.Mapeadores
{
    public class LogLinhaMapeador : ILogLinhaMapeador
    {

        public LogLinhaDto ConverterParaDto(LogLinhaEntidade entity)
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

        public LogLinhaEntidade ConverterParaEntity(LogLinhaDto dto)
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

        public List<LogLinhaDto> ConverterListaParaDto(List<LogLinhaEntidade> listaEntities)
        {
            if (listaEntities == null)
                return null;

            var listaDtos = new List<LogLinhaDto>();

            foreach (var entity in listaEntities)
                listaDtos.Add(ConverterParaDto(entity));

            return listaDtos;

        }

        public List<LogLinhaEntidade> ConverterListaParaEntity(List<LogLinhaDto> listaDtos)
        {
            if (listaDtos == null)
                return null;

            var listaEntities = new List<LogLinhaEntidade>();

            foreach (var dto in listaDtos)
                listaEntities.Add(ConverterParaEntity(dto));

            return listaEntities;
        }
    }

}
