﻿namespace iTaaS.Api.Dominio.Enumeradores
{
    /// <summary>
    /// Define os diferentes tipos de retorno para os logs do sistema.
    /// </summary>
    public enum TipoRetornoLog
    {
        /// <summary>
        /// Retorna o arquivo do arquivo de log
        /// </summary>
        RETORNAR_ARQUIVO = 0,

        /// <summary>
        /// Retorna o link para o arquivo de log
        /// </summary>
        RETORNAR_PATCH = 1,

        /// <summary>
        /// Retorna o link para o arquivo Json de log
        /// </summary>
        RETORNAR_JSON = 2
    }
}
