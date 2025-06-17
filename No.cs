public class No
{
    public int Valor { get; set; }
    public No Esquerda { get; set; }
    public No Direita { get; set; }
    public int Altura { get; set; }

    public No(int valor)
    {
        Valor = valor;
        Altura = 1;
    }
}