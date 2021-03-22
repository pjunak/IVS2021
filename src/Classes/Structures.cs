using System;
using System.Collections.Generic;

namespace Calculator.Classes
{
    public class Structures
    {
        public enum typTokenu
        {
            operand,
            plusMinus,
            mulDiv,
            powerSquare,
            other
        }
        public struct Token
        {
            public typTokenu typ;
            public float operand;
            public char operation;
        }
    }
}