using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercício1
{
    class Program
    {
        public class GeradorDeAsteriscos
        {
            public List<string> GerarLista(int n)
            {
                List<string> resultado = new List<string>();

                for (int i = 1; i <= n; i++)
                {
                    resultado.Add(new String('*', i));
                }

                return resultado;
            }
        }
        static void Main(string[] args)
        {
            GeradorDeAsteriscos gerador = new GeradorDeAsteriscos();
            int n = 0;
            bool entradaValida = false;

            while (!entradaValida)
            {
                try
                {
                    
                    Console.Write("Digite um número inteiro positivo para gerar a lista de asteriscos: ");
                    n = int.Parse(Console.ReadLine());

                    if (n <= 0)
                    {
                        throw new ArgumentException("O número deve ser maior que zero.");
                    }

                    entradaValida = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Formato inválido. Por favor, digite um número inteiro.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
               
            }
            List<string> resultado = gerador.GerarLista(n);

            foreach (string item in resultado)
            {
                Console.WriteLine(item);
            }

            Console.Write("Precione qualquer tecla para sair...");
            Console.ReadKey();
        }
    }
}
