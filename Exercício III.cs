using System;
using System.Collections.Generic;

namespace Exercício3
{
    public class GeradorDeSubconjuntos
    {
        public List<List<int>> ObterSubconjuntos(List<int> numeros)
        {
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
                List<List<int>> subconjuntos = gerador.ObterSubconjuntos(numeros);

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
    }
}
