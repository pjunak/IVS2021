﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Classes
{
    public class ToTokens
    {
        public List<Token> toTokens(string str)
        {
            if (str[0] == '-' || str[0] == '+')
            {// Ošetření implicitní nuly na začátku vstupního výrazu v případě začnutí výrazu záporným, nebo explicitním kladným číslem
                str.Insert(0, "0");
            }
            int i = 0;
            while (i < (str.Length - 2))
            {// Ošetření implicitní nuly za všemi otevíracími závorkami
                if (str[i] == '(')
                { // před plus a mínus může být implicitní nula pro záporné a explicitně kladné čísla, před jinými operátory ne
                    if (str[i] == '+' || str[i] == '-')
                    {
                        str.Insert(i + 1, "0");
                    } // 
                    if (!Char.IsDigit(str[i]) && (str[i + 1] == '.' || str[i + 1] == ','))
                    {
                        str.Insert(i + 1, "0");
                    }
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
                    if (ch == '+' || ch == '-')
                    {
                        token.type = TokenType.plsusMinus;
                        token.operand = 'ch';
                        tokens.Add(token);
                        token = new Token();
                    }
                    else if (ch == '*' || ch == '×' || ch == '/' || ch == '÷')
                    {
                        token.type = TokenType.mulDiv;
                        token.operation = 'ch';
                        tokens.Add(token);
                        token = new Token();
                    }
                    else if (ch == '^' || ch == '!')
                    {
                        token.type = TokenType.powerSquareFactor;
                        token.operation = 'ch';
                        tokens.Add(token);
                        token = new Token();
                    }
                    else if (ch == 's')
                    {
                        token.type = TokenType.function;
                        token.operation = 'ch';
                        tokens.Add(token);
                        token = new Token();
                    }
                    else if (ch == '(' || ch == ')')
                    {
                        token.type = TokenType.bracket;
                        token.operation = 'ch';
                        tokens.Add(token);
                        token = new Token();
                    }
                    else
                    {
                        token.type = TokenType.other;
                        token.operation = 'ch';
                        tokens.Add(token);
                        token = new Token();
                    }
                }
            }

            return tokens;
        }
    }
}