using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercício2Avançado
{
    public class ParInteiros
    {
        public int Primeiro { get; set; }
        public int Segundo { get; set; }

        public ParInteiros(int primeiro, int segundo)
        {
            if (primeiro < segundo)
            {
                Primeiro = primeiro;
                Segundo = segundo;
            }
            else
            {
                Primeiro = segundo;
                Segundo = primeiro;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (ParInteiros)obj;
            return (Primeiro == other.Primeiro && Segundo == other.Segundo) ||
                   (Primeiro == other.Segundo && Segundo == other.Primeiro);
        }

        public override int GetHashCode()
        {
            return Primeiro.GetHashCode() ^ Segundo.GetHashCode();
        }
    }

    class Program
    {
        public static List<ParInteiros> EncontrarParesMenorDiferenca(int[] nums, bool allow_duplicates = true, bool sorted_pairs = false, bool unique_pairs = false)
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

            if (!allow_duplicates)
            {
                paresMenorDiferenca = paresMenorDiferenca.Where(p => p.Primeiro != p.Segundo).ToList();
            }

            if (unique_pairs)
            {
                paresMenorDiferenca = paresMenorDiferenca.Distinct().ToList();
            }

            if (sorted_pairs)
            {
                paresMenorDiferenca = paresMenorDiferenca.OrderBy(p => p.Primeiro).ThenBy(p => p.Segundo).ToList();
            }

            return paresMenorDiferenca;
        }

        static void Main(string[] args)
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

                    bool allow_duplicates = GetBooleanInput("Permitir duplicatas nos pares? (true/false): ");
                    bool sorted_pairs = GetBooleanInput("Ordenar os pares no resultado? (true/false): ");
                    bool unique_pairs = GetBooleanInput("Retornar apenas pares únicos? (true/false): ");

                    List<ParInteiros> paresMenorDiferenca = EncontrarParesMenorDiferenca(nums, allow_duplicates, sorted_pairs, unique_pairs);

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

        private static bool GetBooleanInput(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine().Trim().ToLower();

                if (input == "true")
                {
                    return true;
                }
                else if (input == "false")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Entrada inválida. Por favor, insira 'true' ou 'false'.");
                }
            }
        }
    }
}
