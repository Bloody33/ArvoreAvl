using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        ArvoreAVL arvore = new ArvoreAVL();

        try
        {
            string[] linhas = File.ReadAllLines("entrada.txt");

            foreach (string linha in linhas)
            {
                string[] partes = linha.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (partes.Length == 0) continue;

                string comando = partes[0];

                switch (comando)
                {
                    case "I":
                        if (partes.Length >= 2 && int.TryParse(partes[1], out int valorInserir))
                        {
                            arvore.Inserir(valorInserir);
                        }
                        else
                        {
                            Console.WriteLine("Comando de inserção inválido.");
                        }
                        break;

                    case "R":
                        if (partes.Length >= 2 && int.TryParse(partes[1], out int valorRemover))
                        {
                            arvore.Remover(valorRemover);
                        }
                        else
                        {
                            Console.WriteLine("Comando de remoção inválido.");
                        }
                        break;

                    case "B":
                        if (partes.Length >= 2 && int.TryParse(partes[1], out int valorBuscar))
                        {
                            Console.WriteLine(arvore.Buscar(valorBuscar) ? "Valor encontrado" : "Valor não encontrado");
                        }
                        else
                        {
                            Console.WriteLine("Comando de busca inválido.");
                        }
                        break;

                    case "P":
                        Console.WriteLine("Árvore em pré-ordem: " + arvore.PreOrdem());
                        break;

                    case "F":
                        Console.WriteLine("Fatores de balanceamento:");
                        Console.WriteLine(arvore.FatoresBalanceamento());
                        break;

                    case "H":
                        Console.WriteLine($"Altura da árvore: {arvore.Altura()}");
                        break;

                    default:
                        Console.WriteLine($"Comando desconhecido: {comando}");
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }
}
