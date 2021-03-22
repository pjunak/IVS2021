using System;
using System.Collections.Generic;

namespace Calculator.Classes
{
    public class Structures
    {
        public enum tokenType
        {
            operand,
            plusMinus,
            mulDiv,
            powerSquare,
            other
        }
        public struct Token
        {
            public tokenType type;
            public float operand;
            public char operation;
        }
    }
}