using System;
using System.Collections.Generic;
using UnityEngine;

public class OperationGenerator : MonoBehaviour
{
    String expressao;
    String operacao;
    int fase;
    List<float> opcoes = new List<float>();
    float resultado;

    private SceneLoadCounter sc = new SceneLoadCounter();

    // Start is called before the first frame update
    public void GerarOperacoes()
    {
        fase = sc.getCount()+1;
        operacao = "+";

        int num1 = UnityEngine.Random.Range(1, 100*fase);

        int num2 = UnityEngine.Random.Range(1, 100*fase);

        int op = 0;

        if (fase <= 4)
        {
            op = UnityEngine.Random.Range(0, 2);
        }
        else if (fase <= 8)
        {
            op = UnityEngine.Random.Range(0, 3);
        }
        else
        {
            op = UnityEngine.Random.Range(0, 4);
        }

        float result = 0;

        switch (op)
        {
            case 0:
                result = num1 + num2;
                operacao = "+";
                break;
            case 1:
                result = num1 - num2;
                operacao = "-";
                break;
            case 2:
                result = num1 * num2;
                operacao = "*";
                break;
            case 3:
                result = num1 / num2;
                operacao = "/";
                break;

        }
        expressao = num1.ToString() + operacao + num2.ToString();
        for(int i = 0; i < 4; i++)
        {
            if (i == 0)
                opcoes.Add(result);
            else if(i%2 == 0)
                opcoes.Add(UnityEngine.Random.Range(result-4, result-1));
            else
                opcoes.Add(UnityEngine.Random.Range(result+1, result+4));
        }
        resultado = result;          
    }

    public String getExpressao()
    {
        return expressao;
    }

    public String getOpcao()
    {
        String aux;
        int i = UnityEngine.Random.Range(0, opcoes.ToArray().Length);
        Console.WriteLine(i);
        if(operacao != "/")
        {
            aux = opcoes[i].ToString("N0");
        }
        else
            aux = opcoes[i].ToString();
        opcoes.RemoveAt(i);
        return aux;
    }
    public string getResposta()
    {
        if (operacao != "/")
        {
            return resultado.ToString("N0");
        }
        else
            return resultado.ToString();
    }
}
