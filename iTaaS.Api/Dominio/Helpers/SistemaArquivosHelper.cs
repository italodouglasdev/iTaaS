using iTaaS.Api.Aplicacao.DTOs.Auxiliares;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace iTaaS.Api.Dominio.Helpers
{
    /// <summary>
    /// Classe auxiliar para manipulação de arquivos e diretórios.
    /// </summary>
    public class SistemaArquivosHelper
    {

        /// <summary>
        /// Verifica se um diretório existe no caminho especificado.
        /// </summary>
        /// <param name="caminhoDiretorio">Caminho do diretório a ser verificado.</param>
        /// <returns>Retorna <c>true</c> se o diretório existir, caso contrário, <c>false</c>.</returns>
        public static bool DiretorioExiste(string caminhoDiretorio)
        {
            return Directory.Exists(caminhoDiretorio);
        }

        /// <summary>
        /// Cria um diretório no caminho especificado.
        /// </summary>
        /// <param name="caminhoDiretorio">Caminho do diretório a ser criado.</param>
        /// <returns>Retorna <c>true</c> se o diretório foi criado com sucesso, caso contrário, <c>false</c>.</returns>
        public static bool DiretorioCriar(string caminhoDiretorio)
        {
            Directory.CreateDirectory(caminhoDiretorio);

            return DiretorioExiste(caminhoDiretorio);
        }

        /// <summary>
        /// Deleta um diretório no caminho especificado.
        /// </summary>
        /// <param name="caminhoDiretorio">Caminho do diretório a ser deletado.</param>
        /// <returns>Retorna <c>true</c> se o diretório foi deletado com sucesso, caso contrário, <c>false</c>.</returns>
        public static bool DiretorioDeletar(string caminhoDiretorio)
        {
            Directory.Delete(caminhoDiretorio);

            return DiretorioExiste(caminhoDiretorio);
        }

        /// <summary>
        /// Obtém o diretório base onde os arquivos de log serão armazenados.
        /// </summary>
        /// <returns>Retorna o caminho base para os diretórios de log.</returns>
        public static string ObtenhaDiretorioBase()
        {
            return $"C:\\iTaaS_Logs";
        }

        /// <summary>
        /// Obtém o caminho do diretório a ser criado baseado na data e hora fornecidas.
        /// </summary>
        /// <param name="dataHora">Data e hora usada para construir o caminho.</param>
        /// <returns>Retorna o caminho completo do diretório baseado na data fornecida.</returns>
        public static string ObtenhaCaminhoDiretorioPorDataHora(DateTime dataHora)
        {
            return $"{ObtenhaDiretorioBase()}\\{dataHora.ToString("yyyy")}\\{dataHora.ToString("MM")}\\{dataHora.ToString("dd")}";
        }

        /// <summary>
        /// Cria a árvore de diretórios de acordo com a data e hora fornecidas e retorna um resultado com o caminho criado.
        /// </summary>
        /// <param name="dataHora">Data e hora usada para criar a árvore de diretórios.</param>
        /// <returns>Retorna um objeto Resultado contendo o caminho do diretório e possíveis inconsistências.</returns>
        public static Resultado<string> CriarArvoreDiretorios(DateTime dataHora)
        {
            var resultado = new Resultado<string>();

            var caminho = $"{ObtenhaCaminhoDiretorioPorDataHora(dataHora)}";

            if (!DiretorioCriar(caminho))
                resultado.AdicionarInconsistencia("SISTEMA_ARQUIVOS", "Não foi possível criar o diretório para salvar o log!");

            resultado.Dados = caminho;

            return resultado;
        }

        /// <summary>
        /// Verifica se um arquivo existe no caminho especificado.
        /// </summary>
        /// <param name="caminhoArquivo">Caminho do arquivo a ser verificado.</param>
        /// <returns>Retorna um objeto Resultado contendo um booleano indicando se o arquivo existe e possíveis inconsistências.</returns>
        public static Resultado<bool> ArquivoExiste(string caminhoArquivo)
        {
            var resultado = new Resultado<bool>();

            if (!File.Exists(caminhoArquivo))
            {
                resultado.AdicionarInconsistencia("SISTEMA_ARQUIVOS", "Arquivo não localizado!");
            }

            return resultado;

        }

        /// <summary>
        /// Lê o conteúdo de um arquivo texto e retorna uma lista de strings.
        /// </summary>
        /// <param name="caminhoArquivo">Caminho do arquivo a ser lido.</param>
        /// <returns>Retorna um objeto Resultado contendo uma lista de strings com o conteúdo do arquivo ou inconsistências.</returns>
        public static Resultado<List<string>> LerArquivoTxt(string caminhoArquivo)
        {
            var resultado = new Resultado<List<string>>();
            resultado.Dados = new List<string>();

            try
            {
                var resultadoArquivoExiste = ArquivoExiste(caminhoArquivo);
                if (!resultadoArquivoExiste.Sucesso)
                {
                    resultado.AdicionarInconsistencias(resultadoArquivoExiste.Inconsistencias);
                    return resultado;
                }

                resultado.Dados = new List<string>(File.ReadAllLines(caminhoArquivo));

            }
            catch (Exception ex)
            {
                resultado.AdicionarInconsistencia("SISTEMA_ARQUIVOS", "Erro ao ler o arquivo!");
            }

            return resultado;
        }

        /// <summary>
        /// Cria ou substitui um arquivo de texto no caminho especificado com o texto fornecido.
        /// </summary>
        /// <param name="caminhoArquivo">Caminho do arquivo a ser criado ou substituído.</param>
        /// <param name="texto">Texto a ser escrito no arquivo.</param>
        /// <returns>Retorna um objeto Resultado com o caminho do arquivo criado ou substituído e possíveis inconsistências.</returns>
        public static Resultado<string> CriarArquivoTxt(string caminhoArquivo, string texto)
        {
            var resultado = new Resultado<string>();

            resultado.Dados = caminhoArquivo;

            var diretorioDoArquivo = ObtenhaCaminhoDiretorioPorCaminhoArquivo(caminhoArquivo);
            var diretorioDoArquivoExiste = DiretorioExiste(diretorioDoArquivo);
            if (!diretorioDoArquivoExiste)
            {
                diretorioDoArquivoExiste = DiretorioCriar(diretorioDoArquivo);

                if (!diretorioDoArquivoExiste)
                {
                    resultado.AdicionarInconsistencia("SISTEMS_ARQUIVOS", "Não foi possível criar o arquivo!");
                    return resultado;
                }
            }


            try
            {
                File.WriteAllText(caminhoArquivo, texto);
            }
            catch (Exception ex)
            {
                resultado.AdicionarInconsistencia("SISTEMS_ARQUIVOS", "Não foi possível criar o arquivo!");
            }

            return resultado;
        }

        /// <summary>
        /// Obtém uma string de uma URL fornecida.
        /// </summary>
        /// <param name="url">URL a ser acessada para obter o conteúdo.</param>
        /// <returns>Retorna um objeto Resultado contendo o conteúdo obtido da URL ou possíveis inconsistências.</returns>
        public static Resultado<string> ObtenhaStringDeUrl(string url)
        {
            var resultado = new Resultado<string>();

            try
            {
                var stringBuilderLog = new StringBuilder();

                using (HttpClient client = new HttpClient())
                {
                    var conteudo = client.GetStringAsync(url).Result;

                    using (StringReader reader = new StringReader(conteudo))
                    {
                        string linha;
                        while ((linha = reader.ReadLine()) != null)
                        {
                            stringBuilderLog.AppendLine(linha);
                        }
                    }
                }

                resultado.Dados = stringBuilderLog.ToString();
            }
            catch (AggregateException ex) when (ex.InnerException is HttpRequestException httpEx)
            {
                resultado.AdicionarInconsistencia("REDE", "Erro na requisição HTTP!");
            }
            catch (Exception ex)
            {
                resultado.AdicionarInconsistencia("REDE", "Erro na requisição HTTP!");
            }

            return resultado;
        }

        public static string ObtenhaCaminhoDiretorioPorCaminhoArquivo(string caminhoArquivo)
        {
            return Path.GetDirectoryName(caminhoArquivo);

        }


    }


}
