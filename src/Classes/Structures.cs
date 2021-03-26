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
        other
    }
    public struct Token
    {
        public TokenType type;
        public double operand;
        public char operation;
    }
}