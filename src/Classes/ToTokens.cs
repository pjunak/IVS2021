using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace Calculator.Classes
{
    public class ToTokens
    {
        public List<Token> toTokens(string str)
        {
            if (str[0] == '-' || str[0] == '+')
            {// Ošetření implicitní nuly na začátku vstupního výrazu
                str.Insert(0, "0");
            }
            int i = 0;
            while (i < (str.Length - 1))
            {// Ošetření implicitní nuly za všemi otevíracími závorkami
                if (str[i] == '(')
                {
                    if (!Char.IsDigit(str[i + 1]))
                    {
                        str.Insert(i + 1, "0");
                    }
                }
                i++;
            }
            List<Token> tokens = new List<Token>();
            string number = string.Empty;

            foreach ( char ch in str)
            {// Převod na tokeny
                if (Char.IsDigit(ch))
                {
                    number += ch;
                }
                else if (ch == '.' || ch == ',')
                {
                    number += '.';
                }
                else
                {
                    if ( !string.IsNullOrEmpty(number))
                    {
                        Token token;
                        token.type = TokenType.operand;
                        token.operand = Convert.ToDouble(number);
                        tokens.Add(token);
                    }
                    Token token;
                    if (ch == '+' || ch == '-')
                    {
                        token.type = TokenType.operand;
                        token.operand = Convert.ToDouble(number);
                        tokens.Add(token);
                    }

                    /////// WIP
                }
            }

            return tokens;
        }
    }
}
