using System;
using System.Collections.Generic;

namespace Exercicio2
{
    public class ParInteiros
    {
        public int Primeiro { get; set; }
        public int Segundo { get; set; }

        public ParInteiros(int primeiro, int segundo)
        {
            Primeiro = primeiro;
            Segundo = segundo;
        }
    }

    public class Program
    {
        public static List<ParInteiros> EncontrarParesMenorDiferenca(int[] nums)
        {
            if (nums.Length < 2)
            {
                throw new ArgumentException("O array deve ter pelo menos dois elementos para encontrar pares.");
            }

            Array.Sort(nums);

            int menorDiferenca = int.MaxValue;
            List<ParInteiros> paresMenorDiferenca = new List<ParInteiros>();

            for (int i = 1; i < nums.Length; i++)
            {
                int diferenca = nums[i] - nums[i - 1];

                if (diferenca < menorDiferenca)
                {
                    menorDiferenca = diferenca;
                    paresMenorDiferenca.Clear();
                    paresMenorDiferenca.Add(new ParInteiros(nums[i - 1], nums[i]));
                }
                else if (diferenca == menorDiferenca)
                {
                    paresMenorDiferenca.Add(new ParInteiros(nums[i - 1], nums[i]));
                }
            }

            return paresMenorDiferenca;
        }

        public static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Console.Write("Digite os números inteiros separados por espaços: ");
                    string input = Console.ReadLine();
                    string[] numerosString = input.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    int[] nums = Array.ConvertAll(numerosString, int.Parse);

                    if (nums.Length < 2)
                    {
                        throw new ArgumentException("Por favor, insira pelo menos dois números inteiros.");
                    }

                    List<ParInteiros> paresMenorDiferenca = EncontrarParesMenorDiferenca(nums);

                    Console.WriteLine("Pares com menor diferença absoluta:");
                    foreach (var par in paresMenorDiferenca)
                    {
                        Console.WriteLine($"({par.Primeiro}, {par.Segundo})");
                    }

                    Console.WriteLine("Pressione qualquer tecla para sair...");
                    Console.ReadKey();
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Formato inválido. Certifique-se de inserir números inteiros separados por espaços.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ocorreu um erro: {ex.Message}");
                }
            }
        }
    }
}
