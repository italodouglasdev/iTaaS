using System.Collections.Generic;

namespace iTaaS.Api.Aplicacao.Interfaces.Mapeadores
{
    public interface IMapeadorBase<TEntity, TDto>
    {
        TDto MapearDeEntidadeParaDto(TEntity entity);
        TEntity MapearDeDtoParaEntidade(TDto dto);


        List<TDto> MapearListaDeEntitadesParaDtos(List<TEntity> listaEntities);
        List<TEntity> MapearListaDeDtosParaEntitades(List<TDto> listaDtos);

    }
}
