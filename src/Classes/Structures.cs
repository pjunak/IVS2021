/*
Calculator, FITness StudIO 21
Copyright (C)

Structures.cs: Date structures for ToTokens.cs
Full project can be found here: https://github.com/pjunak/IVS2021/

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program. If not, see https://www.gnu.org/licenses/.
Also add information on how to contact you by electronic and paper mail. 
 */

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