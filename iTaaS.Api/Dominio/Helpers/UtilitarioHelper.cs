using iTaaS.Api.Aplicacao.DTOs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace iTaaS.Api.Dominio.Helpers
{
    public class UtilitarioHelper
    {

        public static bool DiretorioExiste(string caminhoDiretorio)
        {
            return Directory.Exists(caminhoDiretorio);
        }

        public static bool DiretorioCriar(string caminhoDiretorio)
        {
            Directory.CreateDirectory(caminhoDiretorio);

            return DiretorioExiste(caminhoDiretorio);
        }

        public static bool DiretorioDeletar(string caminhoDiretorio)
        {
            Directory.Delete(caminhoDiretorio);

            return DiretorioExiste(caminhoDiretorio);
        }


        public static string ObtenhaDiretorioBase()
        {
            return $"C:\\iTaaS_Logs";
        }

        public static string ObtenhaCaminhoDiretorioPorDataHora(DateTime dataHora)
        {
            return $"{ObtenhaDiretorioBase()}\\{dataHora.ToString("yyyy")}\\{dataHora.ToString("MM")}\\{dataHora.ToString("dd")}";
        }

        public static Resultado<string> CriarArvoreDiretorios(DateTime dataHora)
        {
            var resultado = new Resultado<string>();

            var caminho = $"{ObtenhaCaminhoDiretorioPorDataHora(dataHora)}";

            if (!DiretorioCriar(caminho))
                resultado.AdicionarInconsistencia("SISTEMA_ARQUIVOS", "Não foi possível criar o diretório para salvar o log!");

            resultado.Dados = caminho;

            return resultado;
        }

        public static Resultado<bool> ArquivoExiste(string caminhoArquivo)
        {
            var resultado = new Resultado<bool>();

            if (!File.Exists(caminhoArquivo))
            {
                resultado.AdicionarInconsistencia("SISTEMA_ARQUIVOS", "Arquivo não localizado!");
            }

            return resultado;

        }


        

        public static Resultado<List<string>> LerArquivoTxt(string caminhoArquivo)
        {
            var resultado = new Resultado<List<string>>();
            resultado.Dados = new List<string>();

            try
            {
                var resultadoArquivoExiste = ArquivoExiste(caminhoArquivo);
                if (!resultadoArquivoExiste.Sucesso)
                {
                    resultado.Inconsistencias = resultadoArquivoExiste.Inconsistencias;
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
        public static Resultado<string> CriarArquivoTxt(string caminhoArquivo, string texto)
        {
            var resultado = new Resultado<string>();

            resultado.Dados = caminhoArquivo;

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


        public static Resultado<string> ConverterDeListaParaString(List<string> listaString)
        {
            var resultado = new Resultado<string>();

            try
            {
                resultado.Dados = string.Join(Environment.NewLine, listaString);
            }
            catch
            {
                resultado.AdicionarInconsistencia("SISTEMA_ARQUIVOS", "Não foi possível converter de lista para string!");
            }

            return resultado;
        }

        public static Resultado<string> ObtenhaStringDeUrl(string url)
        {
            var resultado = new Resultado<string>();

            try
            {
                var stringBuilderLog = new StringBuilder();

                using (HttpClient client = new HttpClient())
                {
                    var conteudo = client.GetStringAsync(url).Result; // Executa de forma síncrona

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
                Console.WriteLine($"Erro na requisição HTTP: {httpEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
            }

            return resultado;
        }
        public static Resultado<List<string>> ConverterStringEmListaPorDelimitador(string textoString, string delimitador)
        {
            var resultado = new Resultado<List<string>>();

            if (StringEhNulaOuVazia(textoString) ||
                StringEhNulaOuVazia(delimitador))
                return resultado;

            var partes = textoString.Split(delimitador);

            resultado.Dados = new List<string>();

            resultado.Dados.AddRange(partes);

            return resultado;
        }


        public static bool StringEhNulaOuVazia(string texto)
        {
            return string.IsNullOrEmpty(texto) ? true : false;
        }
        public static int ConverterStringParaInt(string texto)
        {
            var valorInt = 0;

            if (StringEhNulaOuVazia(texto))
                return valorInt;

            int.TryParse(texto, out valorInt);

            return valorInt;
        }
        public static decimal ConverterStringParaDecimal(string texto)
        {
            var valorDecimal = 0M;

            if (StringEhNulaOuVazia(texto))
                return valorDecimal;

            decimal.TryParse(texto, NumberStyles.Any, CultureInfo.InvariantCulture, out valorDecimal);

            return valorDecimal;
        }
        public static int ConverterDecimalParaInt(decimal valor)
        {
            try
            {
                return (int)Math.Truncate(valor);
            }
            catch
            {

                return 0;
            }

        }

        public static DateTime ConverterStringParaDataTime(string data)
        {
            string formato = "yyyyMMddHHmm";
            return DateTime.TryParseExact(data, formato, CultureInfo.InvariantCulture, DateTimeStyles.None, out var resultado)
                ? resultado
                : new DateTime();
        }


        //public static async Task<Resultado<string>> ObtenhaStringDeUrl(string url)
        //{
        //    var resultado = new Resultado<string>();

        //    var stringBuilderLog = new StringBuilder();

        //    using (HttpClient client = new HttpClient())
        //    {
        //        var conteudo = await client.GetStringAsync(url);

        //        using (StringReader reader = new StringReader(conteudo))
        //        {
        //            string linha;
        //            while ((linha = reader.ReadLine()) != null)
        //            {
        //                stringBuilderLog.AppendLine(linha);
        //            }
        //        }
        //    }

        //    resultado.Dados = stringBuilderLog.ToString();

        //    return resultado;
        //}


    }


}
