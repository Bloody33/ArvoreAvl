using System;

public class ArvoreAVL
{
    private No raiz;

    private int Altura(No no) => no?.Altura ?? 0;

    private int FatorBalanceamento(No no) => no == null ? 0 : Altura(no.Esquerda) - Altura(no.Direita);

    private void AtualizarAltura(No no)
    {
        if (no != null)
            no.Altura = 1 + Math.Max(Altura(no.Esquerda), Altura(no.Direita));
    }

    private No RotacaoDireita(No y)
    {
        var x = y.Esquerda;
        var T2 = x.Direita;

        x.Direita = y;
        y.Esquerda = T2;

        AtualizarAltura(y);
        AtualizarAltura(x);

        return x;
    }

    private No RotacaoEsquerda(No x)
    {
        var y = x.Direita;
        var T2 = y.Esquerda;

        y.Esquerda = x;
        x.Direita = T2;

        AtualizarAltura(x);
        AtualizarAltura(y);

        return y;
    }

    public void Inserir(int valor)
    {
        raiz = Inserir(raiz, valor);
    }

    private No Inserir(No no, int valor)
    {
        if (no == null)
            return new No(valor);

        if (valor < no.Valor)
            no.Esquerda = Inserir(no.Esquerda, valor);
        else if (valor > no.Valor)
            no.Direita = Inserir(no.Direita, valor);
        else
            return no;

        AtualizarAltura(no);

        int balance = FatorBalanceamento(no);

        if (balance > 1 && valor < no.Esquerda.Valor)
            return RotacaoDireita(no);

        if (balance < -1 && valor > no.Direita.Valor)
            return RotacaoEsquerda(no);

        if (balance > 1 && valor > no.Esquerda.Valor)
        {
            no.Esquerda = RotacaoEsquerda(no.Esquerda);
            return RotacaoDireita(no);
        }

        if (balance < -1 && valor < no.Direita.Valor)
        {
            no.Direita = RotacaoDireita(no.Direita);
            return RotacaoEsquerda(no);
        }

        return no;
    }

    public void Remover(int valor)
    {
        raiz = Remover(raiz, valor);
    }

    private No Remover(No no, int valor)
    {
        if (no == null) return no;

        if (valor < no.Valor)
            no.Esquerda = Remover(no.Esquerda, valor);
        else if (valor > no.Valor)
            no.Direita = Remover(no.Direita, valor);
        else
        {
            if (no.Esquerda == null || no.Direita == null)
            {
                No temp = no.Esquerda ?? no.Direita;

                if (temp == null)
                {
                    temp = no;
                    no = null;
                }
                else
                {
                    no = temp;
                }
            }
            else
            {
                No temp = NoMinimo(no.Direita);
                no.Valor = temp.Valor;
                no.Direita = Remover(no.Direita, temp.Valor);
            }
        }

        if (no == null) return no;

        AtualizarAltura(no);

        int balance = FatorBalanceamento(no);

        if (balance > 1 && FatorBalanceamento(no.Esquerda) >= 0)
            return RotacaoDireita(no);

        if (balance > 1 && FatorBalanceamento(no.Esquerda) < 0)
        {
            no.Esquerda = RotacaoEsquerda(no.Esquerda);
            return RotacaoDireita(no);
        }

        if (balance < -1 && FatorBalanceamento(no.Direita) <= 0)
            return RotacaoEsquerda(no);

        if (balance < -1 && FatorBalanceamento(no.Direita) > 0)
        {
            no.Direita = RotacaoDireita(no.Direita);
            return RotacaoEsquerda(no);
        }

        return no;
    }

    private No NoMinimo(No no)
    {
        No atual = no;
        while (atual.Esquerda != null)
            atual = atual.Esquerda;
        return atual;
    }

    public bool Buscar(int valor)
    {
        return Buscar(raiz, valor);
    }

    private bool Buscar(No no, int valor)
    {
        if (no == null) return false;

        if (valor < no.Valor)
            return Buscar(no.Esquerda, valor);
        else if (valor > no.Valor)
            return Buscar(no.Direita, valor);
        else
            return true;
    }

    public string PreOrdem()
    {
        return PreOrdem(raiz).Trim();
    }

    private string PreOrdem(No no)
    {
        if (no == null) return "";
        return no.Valor + " " + PreOrdem(no.Esquerda) + PreOrdem(no.Direita);
    }

    public string FatoresBalanceamento()
    {
        var result = new System.Text.StringBuilder();
        FatoresBalanceamento(raiz, result);
        return result.ToString().Trim();
    }

    private void FatoresBalanceamento(No no, System.Text.StringBuilder sb)
    {
        if (no != null)
        {
            sb.AppendLine($"Nó {no.Valor}: Fator de balanceamento {FatorBalanceamento(no)}");
            FatoresBalanceamento(no.Esquerda, sb);
            FatoresBalanceamento(no.Direita, sb);
        }
    }

    public int Altura() => Altura(raiz);
}