using System;
using System.Collections.Generic;

namespace Calculator.Classes
{
    public enum TokenType
    {
        operand,
        plusMinus,
        mulDiv,
        powerSquare,
        function,
        brackets,
        other
    }
    public struct Token
    {
        public TokenType type;
        public double operand;
        public char operation;
    }
}