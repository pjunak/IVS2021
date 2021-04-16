/*
Calculator, FITness StudIO 21
Copyright (C)

Structures.cs: Data structures.
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

namespace Calculator.Classes
{
    /**
    * Určení typu Tokenu.
    */
    public enum TokenType
    {
        operand = 0,
        plusMinus = 1,
        mulDiv = 2,
        powerFactorSin = 3,
        brackets = -1,
        other = 42
    }
    /**
     * @struct Token
     * @brief Struktura popisující jeden lexém vstupu kalkulačky.
     * @var Token::type
     * Člen type obsahuje typ lexému, například operace, operand atp.
     * @var Token::operand
     * Člen operand obsahuje hodnotu operandu.
     * @var Token::operation
     * Člen operation obsahuje jednoznakový \c char název operace.
     */
    public struct Token
    {
        public TokenType type;
        public double operand;
        public char operation;
    }
}