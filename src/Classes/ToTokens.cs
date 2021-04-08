using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows; //TODO only for disgnostics, remove later

namespace Calculator.Classes
{
    /**
     * @class ToTokens
     * 
     * @brief Třída pro převod vstupního stringu na seznam tokenů pro další práci s matematickou knihovnou
     */
    public class ToTokens
    {
		public ToTokens() { }
        /** 
         * @brief Převede vstup kalkulačky ze stringu do tokenů
         * @param str Vstupní řetězec z formulářového okna
         * @return Vrátí List datového typu Token
         */
        public List<Token> toTokens(string str)
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
                        token.operand = Convert.ToDouble(number, new CultureInfo("en-US"));
                        tokens.Add(token);
                        number = string.Empty;
                        token = new Token();
                    }
                    if (ch == 'π')
                    {
                        token.type = TokenType.operand;
                        token.operand = Math.PI;
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
                        token.type = TokenType.powerFactorSin;
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
                    else if (Char.IsWhiteSpace(ch))
                    {
                        MessageBox.Show("Problem in toToken"); // TODO, nějakej pěknej chybovej message
                    }
                    else
                    { // TODO Dočasné, sem by se neměly dostat žádné krom předpokládaných znaků. -> vyhodit Error
                        MessageBox.Show("Problem in toToken"); // TODO, nějakej pěknej chybovej message
                        token.type = TokenType.other;
                        token.operation = ch;
                        tokens.Add(token);
                        token = new Token();
                    }
                }
            }
            if (!string.IsNullOrEmpty(number))
            {
                token.type = TokenType.operand;
                token.operand = Convert.ToDouble(number, new CultureInfo("en-US"));
                tokens.Add(token);
                if (tokens.Count == 1 && (number == "5318008" || number == "58008"))
                {
                    MessageBox.Show("Easter egg discovered"); // TODO, easter egg message
                }
                token = new Token();
            }

            return tokens;
        }
    }
}
