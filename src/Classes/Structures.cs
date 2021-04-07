using System;
using System.Collections.Generic;

namespace Calculator.Classes
{
    public enum TokenType
    {
        operand = 0,
        plusMinus = 1,
        mulDiv = 2,
        powerFactorSin = 3,
        brackets = -1,
        other = 42
    }
    public struct Token
    {
        public TokenType type;
        public double operand;
        public char operation;
    }
}