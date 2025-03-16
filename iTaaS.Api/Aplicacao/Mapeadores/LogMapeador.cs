using iTaaS.Api.Aplicacao.DTOs;
using iTaaS.Api.Aplicacao.Interfaces.Mapeadores;
using iTaaS.Api.Dominio.Entidades;
using System.Collections.Generic;
using System.Linq;

namespace iTaaS.Api.Aplicacao.Mapeadores
{
    public class LogMapeador : ILogMapeador
    {
        private readonly ILogLinhaMapeador LogLinhaMapper;

        public LogMapeador(ILogLinhaMapeador logLinhaMapper)
        {
            LogLinhaMapper = logLinhaMapper;
        }

        public LogDto ConverterParaDto(LogEntidade entity)
        {
            if (entity == null)
                return null;

            return new LogDto
            {
                Id = entity.Id,
                Hash = entity.Hash,
                UrlOrigem = entity.UrlOrigem,
                DataHoraRecebimento = entity.DataHoraRecebimento,
                Linhas = LogLinhaMapper.ConverterListaParaDto(entity.Linhas.ToList())
            };
        }

        public LogEntidade ConverterParaEntity(LogDto dto)
        {
            if (dto == null)
                return null;

            return new LogEntidade
            {
                Id = dto.Id,
                Hash = dto.Hash,
                UrlOrigem = dto.UrlOrigem,
                DataHoraRecebimento = dto.DataHoraRecebimento,
                Linhas = LogLinhaMapper.ConverterListaParaEntity(dto.Linhas.ToList())
            };
        }

        public List<LogDto> ConverterListaParaDto(List<LogEntidade> listaEntities)
        {
            if (listaEntities == null)
                return null;

            var listaDtos = new List<LogDto>();

            foreach (var entity in listaEntities)
                listaDtos.Add(ConverterParaDto(entity));

            return listaDtos;

        }

        public List<LogEntidade> ConverterListaParaEntity(List<LogDto> listaDtos)
        {
            if (listaDtos == null)
                return null;

            var listaEntities = new List<LogEntidade>();

            foreach (var dto in listaDtos)
                listaEntities.Add(ConverterParaEntity(dto));

            return listaEntities;
        }

    }
}
