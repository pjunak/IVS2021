using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace Calculator.Classes
{
    public class ToTokens
    {
        public List toTokens(string str)
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
            foreach ( char ch in str)
            {// Převod na tokeny
                if (Char.IsDigit(ch))
                {
                    ///
                }
                switch (ch)
                {
                   ///
                }
            }

            return tokens;
        }
    }
}
