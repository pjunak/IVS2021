using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Classes
{
    /**
     * @class ToTokens
     * 
     * @brief Třída pro převod vstupního stringu na seznam tokenů pro další práci s matematickou knihovnou
     */
    public class ToTokens
    {
        /** 
         * @brief Převede vstup kalkulačky ze stringu do tokenů
         * @param str Vstupní řetězec z formulářového okna
         * @return Vrátí List datového typu Token
         */
        static public List<Token> toTokens(string str)
        {
            if (str[0] == '-' || str[0] == '+')
            {// Ošetření implicitní nuly na začátku vstupního výrazu v případě začnutí výrazu záporným, nebo explicitním kladným číslem
                str.Insert(0, "0");
            }
            int i = 0;
            while (i < (str.Length - 2))
            {// Ošetření implicitní uvnitř stringu
                if (str[i] == '(')
                { 
                    if (str[i] == '+' || str[i] == '-')
                    { // před plus a mínus může být implicitní nula pro záporné a explicitně kladné čísla
                        str.Insert(i + 1, "0");
                    } 
                }
                else if (!Char.IsDigit(str[i]) && (str[i + 1] == '.' || str[i + 1] == ','))
                {// před desetinnou tečkou či čárkou může bůt implicitní nula
                    str.Insert(i + 1, "0");
                }
                i++;
            }

            List<Token> tokens = new List<Token>();
            string number = string.Empty;
            Token token = new Token();

            foreach (char ch in str)
            {// Převod na tokeny
                if (Char.IsDigit(ch))
                {
                    number += ch;
                }
                else if (ch == '.' || ch == ',')
                {
                    number += '.';
                }
                else // TODO dodělat převod pi jakmile se dohodne zápis pi
                {
                    if (!string.IsNullOrEmpty(number))
                    { // Přidání aktuaálního stringu číslic jako číslo do tokenů
                        token.type = TokenType.operand;
                        token.operand = Convert.ToDouble(number);
                        tokens.Add(token);
                        number = string.Empty;
                        token = new Token();
                    }
                    if (ch == 'p')
                    {
                        token.type = TokenType.operand;
                        token.operand = 3.1415926535897931;
                        tokens.Add(token);
                        token = new Token();
                    }
                    else if (ch == '+' || ch == '-')
                    {
                        token.type = TokenType.plusMinus;
                        token.operation = ch;
                        tokens.Add(token);
                        token = new Token();
                    }
                    else if (ch == '*' || ch == '×' || ch == '/' || ch == '÷')
                    {
                        token.type = TokenType.mulDiv;
                        token.operation = ch;
                        tokens.Add(token);
                        token = new Token();
                    }
                    else if (ch == '^' || ch == '!' || ch == 's')
                    {
                        token.type = TokenType.powerSquareFactorFunc;
                        token.operation = ch;
                        tokens.Add(token);
                        token = new Token();
                    }
                    else if (ch == 'x') // TODO obsolete
                    {
                        token.type = TokenType.function;
                        token.operation = ch;
                        tokens.Add(token);
                        token = new Token();
                    }
                    else if (ch == '(' || ch == ')')
                    {
                        token.type = TokenType.brackets;
                        token.operation = ch;
                        tokens.Add(token);
                        token = new Token();
                    }
                    else
                    {
                        token.type = TokenType.other;
                        token.operation = ch;
                        tokens.Add(token);
                        token = new Token();
                    }
                }
            }

            return tokens;
        }
    }
}
