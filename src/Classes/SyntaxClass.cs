using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Calculator.Classes
{
    public class SyntaxClass
    {
        public SyntaxClass() { }

        private enum Row
        {
            DotComma = 0,
            Pi = 1,
            PlusMinusMulDivPow = 2,
            OBracket = 3,
            Sin = 4,
            Digit = 5,
            CBracketFact = 6,
            Other = 7
        }

        private enum Column
        {
            Digit = 0,
            DotComma = 1,
            Pi = 2,
            PlusMinus = 3,
            MulDiv = 4,
            Pow = 5,
            Fact = 6,
            Sin = 7,
            OBracket = 8,
            CBracket = 9,
            Other = 10
        }

        //true == Zakázaný následující znak.
        //false == Povolený následující znak.
        readonly private bool[,] IncorrectFollow = new bool[8, 11]
        {
            {false, true, true, true, true, true, true, true, true, true, true},
            {true, true, true, false, false, false, false, true, true, false, true},
            {false, false, false, true, true, true, true, false, false, true, true},
            {false, false, false, false, true, true, true, false, false, true, true},
            {true, true, true, true, true, true, true, true, false, true, true},
            {false, false, true, false, false, false, false, true, true, false, true},
            {true, true, true, false, false, false, false, true, true, false, true},
            {true, true, true, true, true, true, true, true, true, true, true},
        };

        //řádky v tabulce
        readonly private bool[] CannotEnd = new bool[8] { true, false, true, true, true, false, false, true };

        //sloupce v tabulce
        readonly private bool[] CannotBegin = new bool[11] { false, true, false, false, true, true, true, false, false, true, true};

        /** 
         * Funkce ověří syntaktickou správnost vstupního řetězce.
         * 
         * @param Input Vstupní řetězec ke kontrole.
         * @param FinalChecking Určuje, zda se jedná o průběžnou (\c FALSE) nebo finální (\c TRUE) konrolu před výpočtem výsledku.
         *
         */
        public string SyntaxCheck(string Input, bool FinalChecking)
        {
            if(Input == "" || Input == null)
            {
                return Input += "0";
            }

            int InputLen = Input.Length;
            Row RSymbol;
            Column CSymbol;
            IdentifyChar(Input[InputLen - 1], Input[0], out RSymbol, out CSymbol);

            //Kontrola, jestli výraz začíná nepovolenými znaky.
            if(CannotBegin[(int)CSymbol])
            {
                return null;
            }

            //Kontrola, jestli výraz končí povolenými znaky.
            if (FinalChecking && CannotEnd[(int)RSymbol])
            {
                return null;
            }

            //Kontrola počtu závorek.
            int OpenBracketCount = 0;
            int ClosedBracketCount = 0;
            for (int i = 0; i < InputLen; i++)
            {
                if (Input[i] == '(')
                {
                    OpenBracketCount++;
                }
                if (Input[i] == ')')
                {
                    ClosedBracketCount++;
                }
            }
            if (ClosedBracketCount > OpenBracketCount)
            {
                return null;
            }
            else if (OpenBracketCount > ClosedBracketCount && FinalChecking)
            {
                for(int i = OpenBracketCount - ClosedBracketCount; i > 0; i--)
                {
                    Input += ")";
                }
            }

            //Kontrola korektní posloupnosti zanků a závorkou za sinusem.
            for (int i = 0; i < InputLen - 1; i++)
            {
                IdentifyChar(Input[i], Input[i + 1], out RSymbol, out CSymbol);
                if (IncorrectFollow[(int)RSymbol, (int)CSymbol])
                {
                    return null;
                }
            }
            
            //V pořádku.
            return Input;
        }

        /** 
         * Funkce namapuje vstupní znaky \p Actual a \p Next na předdefinové symboly \p RSymbol a \p CSymbol.
         * 
         * @param Actual Aktuální znak vstupního řetězce
         * @param Next Následující znak vstupního řetězce
         * @param RSymbol Symbol aktuálního znaku (index řádku)
         * @param CSymbol Symbol následujícího znaku (index sloupce)
         */
        private void IdentifyChar(char Actual, char Next, out Row RSymbol, out Column CSymbol)
        {
            RSymbol = 0;
            CSymbol = 0;
            switch (Actual)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    RSymbol = Row.Digit;
                    break;
                case ')':
                case '!':
                    RSymbol = Row.CBracketFact;
                    break;
                case 's':
                    RSymbol = Row.Sin;
                    break;
                case '(':
                    RSymbol = Row.OBracket;
                    break;
                case '+':
                case '-':
                case '*':
                case '/':
                case '^':
                    RSymbol = Row.PlusMinusMulDivPow;
                    break;
                case 'π':
                    RSymbol = Row.Pi;
                    break;
                case '.':
                case ',':
                    RSymbol = Row.DotComma;
                    break;
                default:
                    RSymbol = Row.Other;
                    break;
            }

            switch (Next)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    CSymbol = Column.Digit;
                    break;
                case '.':
                case ',':
                    CSymbol = Column.DotComma;
                    break;
                case 'π':
                    CSymbol = Column.Pi;
                    break;
                case '+':
                case '-':
                    CSymbol = Column.PlusMinus;
                    break;
                case '*':
                case '/':
                    CSymbol = Column.MulDiv;
                    break;
                case '^':
                    CSymbol = Column.Pow;
                    break;
                case '!':
                    CSymbol = Column.Fact;
                    break;
                case 's':
                    CSymbol = Column.Sin;
                    break;
                case '(':
                    CSymbol = Column.OBracket;
                    break;
                case ')':
                    CSymbol = Column.CBracket;
                    break;
                default:
                    CSymbol = Column.Other;
                    break;
            }
            return;
        }
    }
}
