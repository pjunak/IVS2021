using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Calculator.Classes;

namespace Calculator.Classes
{
    /**
     * @class SyntaxClass
     * 
     * @brief Třída pro syntaktickou kontrolu výrazu zadáveného do kalkulkačky.
     */
    public class SyntaxClass
    {
        public SyntaxClass() { }

        /**
         * Index řádku do tabulky \c IncorrectFollow.
         */
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

        /**
         * Index sloupce do tabulky \c IncorrectFollow.
         */
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

        /**
          Tabulka zakázané a povolené následovnosti znaků.\n
          \c true == zakázaný následující znak.\n
          \c false == povolený následující znak.\n
          \n
          Řádky:    ,. | π | +-/*^ | ( | s | číslice | )! | ostatní znaky\n
          Sloupce: číslice | ,. | π | +- | /* | ^ |  ! | s | ( | ) | ostatní znaky
         */
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

        /**
          Příznak validního ukončujícího znaku.\n
          \c true == zakázaný ukončující znak.\n
          \c false == povolený ukončující znak. 
         */
        readonly private bool[] CannotEnd = new bool[8] { true, false, true, true, true, false, false, true };

        /**
          Příznak validního počátečního znaku.\n
          \c true == zakázaný počáteční znak.\n
          \c false == povolený počáteční znak. 
         */
        readonly private bool[] CannotBegin = new bool[11] { false, true, false, false, true, true, true, false, false, true, true};

        /** 
         * Funkce ověří syntaktickou správnost vstupního řetězce, v případě chybějících pravých závorek je doplní.
         * Smaže samostatný znak funkce sinus (s) bez otevírací závorky.
         * 
         * @param Input Vstupní řetězec ke kontrole.
         * @param FinalChecking Určuje, zda se jedná o průběžnou (\c false) nebo finální (\c true) konrolu před výpočtem výsledku.
         * @param MainWindowRef Reference na třídu MainWindowClass.
         *
         * @return V případě validního řetězce vrátí řetezec ze vstupu, případně doplněný o pravé závorky. V případě nevalidního řetězce vrací \c null
         */
        public string SyntaxCheck(string Input, bool FinalChecking, MainWindowClass MainWindowRef)
        {
            if(Input == "" || Input == null)
            {
                return Input += "";
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

            //Mazání samotného "s".
            for(int i = 0; i < InputLen; i++)
            {
                if (Input[i] == 's')
                {
                    if(i == InputLen - 1)
                    {
                        Input = Input.Remove(i, 1);
                        MainWindowRef.CorrectedValue = MainWindowRef.CorrectedValue.Remove(i, 1);
                        InputLen--;
                        break;
                    }
                    else if(Input[i + 1] != '(')
                    {
                        Input = Input.Remove(i, 1);
                        MainWindowRef.CorrectedValue = MainWindowRef.CorrectedValue.Remove(i, 1);
                        InputLen--;
                        i--;
                    }
                }
            }

            //Kontrola korektní posloupnosti znaků.
            for (int i = 0; i < InputLen - 1; i++)
            {
                IdentifyChar(Input[i], Input[i + 1], out RSymbol, out CSymbol);
                if (IncorrectFollow[(int)RSymbol, (int)CSymbol])
                {
                    return null;
                }
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
                for (int i = OpenBracketCount - ClosedBracketCount; i > 0; i--)
                {
                    Input += ")";
                }
            }

            return Input;
        }

        /** 
         * Funkce namapuje vstupní znaky \p Actual a \p Next na předdefinové indexy \p RSymbol a \p CSymbol do tabulky \c IncorrectFollow.
         * 
         * @param Actual Aktuální znak vstupního řetězce
         * @param Next Následující znak vstupního řetězce
         * @param RSymbol Index aktuálního znaku
         * @param CSymbol Index následujícího znaku
         */
        private void IdentifyChar(char Actual, char Next, out Row RSymbol, out Column CSymbol)
        {
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
