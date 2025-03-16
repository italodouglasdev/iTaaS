using System.Collections.Generic;

namespace iTaaS.Api.Aplicacao.Interfaces.Mapeadores
{
    public interface IMapeadorBase<TEntity, TDto>
    {
        TDto ConverterParaDto(TEntity entity);
        TEntity ConverterParaEntity(TDto dto);


        List<TDto> ConverterListaParaDto(List<TEntity> listaEntities);
        List<TEntity> ConverterListaParaEntity(List<TDto> listaDtos);

    }
}
