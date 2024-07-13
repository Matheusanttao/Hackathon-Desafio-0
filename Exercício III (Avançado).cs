using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercício3
{
    public class GeradorDeSubconjuntos
    {
        public List<List<int>> ObterSubconjuntos(List<int> numeros, int? maxSize = null, int? minSize = null, bool distinctOnly = false, bool sortSubsets = false)
        {
            // Remover duplicados se distinctOnly for verdadeiro
            if (distinctOnly)
            {
                numeros = numeros.Distinct().ToList();
            }

            List<List<int>> subconjuntos = new List<List<int>> { new List<int>() };

            foreach (int numero in numeros)
            {
                int n = subconjuntos.Count;
                for (int i = 0; i < n; i++)
                {
                    List<int> novoSubconjunto = new List<int>(subconjuntos[i]) { numero };
                    subconjuntos.Add(novoSubconjunto);
                }
            }

            // Aplicar maxSize e minSize
            subconjuntos = subconjuntos.Where(s => (!maxSize.HasValue || s.Count <= maxSize) && (!minSize.HasValue || s.Count >= minSize)).ToList();

            // Ordenar os subconjuntos e seus elementos se sortSubsets for verdadeiro
            if (sortSubsets)
            {
                subconjuntos = subconjuntos.OrderBy(s => s.Count).ThenBy(s => string.Join(",", s)).ToList();
                foreach (var subconjunto in subconjuntos)
                {
                    subconjunto.Sort();
                }
            }

            return subconjuntos;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<int> numeros = ObterNumerosDoUsuario();
            if (numeros.Count > 0)
            {
                GeradorDeSubconjuntos gerador = new GeradorDeSubconjuntos();

                // Solicitar parâmetros opcionais do usuário
                Console.Write("Digite o tamanho máximo dos subconjuntos (ou pressione Enter para ignorar): ");
                int? maxSize = TryParseNullableInt(Console.ReadLine());

                Console.Write("Digite o tamanho mínimo dos subconjuntos (ou pressione Enter para ignorar): ");
                int? minSize = TryParseNullableInt(Console.ReadLine());

                Console.Write("Deseja subconjuntos sem elementos duplicados? (true/false, ou pressione Enter para ignorar): ");
                bool distinctOnly = TryParseNullableBool(Console.ReadLine()) ?? false;

                Console.Write("Deseja ordenar os subconjuntos e seus elementos? (true/false, ou pressione Enter para ignorar): ");
                bool sortSubsets = TryParseNullableBool(Console.ReadLine()) ?? false;

                List<List<int>> subconjuntos = gerador.ObterSubconjuntos(numeros, maxSize, minSize, distinctOnly, sortSubsets);

                Console.WriteLine("Subconjuntos:");
                foreach (var subconjunto in subconjuntos)
                {
                    Console.WriteLine("[" + string.Join(", ", subconjunto) + "]");
                }

                Console.WriteLine("Pressione qualquer tecla para sair...");
                Console.ReadKey();
            }
        }

        static List<int> ObterNumerosDoUsuario()
        {
            while (true)
            {
                try
                {
                    Console.Write("Digite os números separados por espaço: ");
                    string entrada = Console.ReadLine();
                    string[] partes = entrada.Split(' ');
                    List<int> numeros = new List<int>();

                    foreach (string parte in partes)
                    {
                        if (int.TryParse(parte, out int numero))
                        {
                            numeros.Add(numero);
                        }
                        else
                        {
                            Console.WriteLine($"Entrada inválida: '{parte}' não é um número inteiro. Tente novamente.");
                            numeros.Clear();
                            break;
                        }
                    }

                    if (numeros.Count > 0)
                    {
                        return numeros;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ocorreu um erro: " + ex.Message);
                    Console.WriteLine("Tente novamente.");
                }
            }
        }

        static int? TryParseNullableInt(string input)
        {
            if (int.TryParse(input, out int result))
            {
                return result;
            }
            return null;
        }

        static bool? TryParseNullableBool(string input)
        {
            if (bool.TryParse(input, out bool result))
            {
                return result;
            }
            return null;
        }
    }
}
