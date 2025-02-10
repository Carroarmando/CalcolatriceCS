using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcolatriceCS
{
    class Program
    {
        static void Main(string[] args)
        {
            string expression = "1+(1+(2+3))+(3+4)";
            GetResult(expression);
        }
        static int GetResult(string expression)
        {
            {
                int r;
                if (int.TryParse(expression, out r))
                    return r;
            }//se è un numero lo ritorna

            {
                while (expression.Contains('('))
                {
                    int start = expression.LastIndexOf('(');
                    int end = expression.IndexOf(')', start);

                    string s = expression.Substring(start + 1, end - start - 1);
                    int result = GetResult(s);

                    expression = expression.Substring(0, start) + result.ToString() + expression.Substring(end + 1);
                }

                int i = (expression[0] == '-') ? 1 : 0;
                while (i < expression.Length && char.IsDigit(expression[i]))
                {
                    i++;
                }
            }//rimuovi parentesi

            while (expression.Contains('*') || expression.Contains('/'))
            {
                // Trova l'indice del primo operatore con la precedenza più alta (moltiplicazione o divisione)
                int index = (expression.IndexOf('*') != -1 ? expression.IndexOf('*') : int.MaxValue) <
                            (expression.IndexOf('/') != -1 ? expression.IndexOf('/') : int.MaxValue)
                            ? expression.IndexOf('*')
                            : expression.IndexOf('/');

                string left = "", right = "";

                // Trova il numero a sinistra dell'operatore
                int leftIndex = index - 1;
                while (leftIndex >= 0 && char.IsDigit(expression[leftIndex]))
                {
                    leftIndex--;
                }
                left = expression.Substring(leftIndex + 1, index - leftIndex - 1);

                // Trova il numero a destra dell'operatore
                int rightIndex = index + 1;
                while (rightIndex < expression.Length && char.IsDigit(expression[rightIndex]))
                {
                    rightIndex++;
                }
                right = expression.Substring(index + 1, rightIndex - index - 1);

                // Calcola il risultato dell'operazione
                int num1 = int.Parse(left);
                int num2 = int.Parse(right);
                int result = 0;

                if (expression[index] == '*')
                    result = num1 * num2;
                else if (expression[index] == '/')
                    result = num1 / num2;

                // Sostituisci l'operazione con il risultato nella stringa
                expression = expression.Substring(0, leftIndex + 1) + result.ToString() + expression.Substring(rightIndex);
            }

            while (expression.Contains('+') || expression.Contains('-'))
            {
                // Trova l'indice del primo operatore con la precedenza più alta (somma o sottrazione)
                int index = (expression.IndexOf('+') != -1 ? expression.IndexOf('+') : int.MaxValue) <
                            (expression.IndexOf('-') != -1 ? expression.IndexOf('-') : int.MaxValue)
                            ? expression.IndexOf('+')
                            : expression.IndexOf('-');

                string left = "", right = "";

                // Trova il numero a sinistra dell'operatore
                int leftIndex = index - 1;
                while (leftIndex >= 0 && char.IsDigit(expression[leftIndex]))
                {
                    leftIndex--;
                }
                left = expression.Substring(leftIndex + 1, index - leftIndex - 1);

                // Trova il numero a destra dell'operatore
                int rightIndex = index + 1;
                while (rightIndex < expression.Length && char.IsDigit(expression[rightIndex]))
                {
                    rightIndex++;
                }
                right = expression.Substring(index + 1, rightIndex - index - 1);

                // Calcola il risultato dell'operazione
                int num1 = int.Parse(left);
                int num2 = int.Parse(right);
                int result = 0;

                if (expression[index] == '+')
                    result = num1 + num2;
                else if (expression[index] == '-')
                    result = num1 - num2;

                // Sostituisci l'operazione con il risultato nella stringa
                expression = expression.Substring(0, leftIndex + 1) + result.ToString() + expression.Substring(rightIndex);
            }

        }
        static int GetType(char c)
        {
            char[] num = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            char[] op = { '+', '-', '*', '/' };
            if (num.Contains(c))
                return 1;
            else if (op.Contains(c))
                return 2;
            return -1;
        }

        #region Operators
        static double Add(string factor1, string factor2)
        {
            return GetResult(factor1) + GetResult(factor2);
        }
        static double Mul(string factor1, string factor2)
        {
            return GetResult(factor1) * GetResult(factor2);
        }
        static double Sub(string factor1, string factor2)
        {
            return GetResult(factor1) - GetResult(factor2);
        }
        static double Div(string factor1, string factor2)
        {
            return GetResult(factor1) / GetResult(factor2);
        }
        #endregion

    }
}
