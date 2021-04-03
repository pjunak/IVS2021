using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

/**
 * @mainpage notitle
 *
 * @section intro_sec Úvodní sekce
 *
 * This is the introduction.
 *
 * @section install_sec Instalace, odinstalace
 *
 * @subsection step1 Step 1: Opening the box
 */

namespace Kalkulacka
{
    /**
     * @brief Třída hlavního okna Kalkulačky, řeší stisky myši a nápovědu.
     */
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                string filePath = "../../Help/Napoveda_ver_1_0.chm";
                System.Windows.Forms.Help.ShowHelp(null, filePath);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string content = (sender as System.Windows.Controls.Button).Content.ToString();

            if (content == "?")
            {
                string filePath = "../../Help/Napoveda_ver_1_0.chm";
                System.Windows.Forms.Help.ShowHelp(null, filePath);
            }
            else if (content == "← Del")
            {
                if (InputTextBox.Text.Length != 0)
                { 
                    InputTextBox.Text = InputTextBox.Text.Substring(0, InputTextBox.Text.Length - 1);
                }
            }
            else
            {   //vloží obsah tlačítka do textboxu a posune kurzor
                InputTextBox.Text = InputTextBox.Text.Insert(InputTextBox.CaretIndex, content);
                InputTextBox.SelectionStart = InputTextBox.Text.Length;
                InputTextBox.SelectionLength = 0;
            }

            // pokud sinus, doplní závorku otevírací
            if (content == "sin(x)")
            {
                InputTextBox.Text = InputTextBox.Text.Insert(InputTextBox.CaretIndex, "(");
                InputTextBox.SelectionStart = InputTextBox.Text.Length;
                InputTextBox.SelectionLength = 0;
            }    
        }

        private enum Row
        {
            DotComma = 0,
            Pi = 1,
            PlusMinusMulDivPow = 2,
            OBracketSin = 3,
            Digit = 4,
            CBracketFact = 5,
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
            EndFlag = 10
        }

        //true == Zakázaný následující znak.
        //false == Povolený následující znak.
        bool[,] IncorrectFollow = new bool[6, 10]
        {
            {false, true, true, true, true, true, true, true, true, true},
            {true, true, true, false, false, false, false, true, true, false},
            {false, false, false, true, true, true, true, false, false, true},
            {false, false, false, false, true, true, true, false, false, true},
            {false, false, true, false, false, false, false, true, true, false},
            {true, true, true, false, false, false, false, true, true, false}
        };

        bool[] CannotEnd = new bool[] {true, true, true, true, false, false};

        /** 
         * Funkce ověří syntaktickou sprvánost vstupního řetězce.
         * 
         * @param Input Vstupní řetězec ke kontrole.
         * @param FinalChecking Určuje, zda se jedná o průběžnou (\c FALSE) nebo finální (\c TRUE) konrolu před výpočtem výsledku.
         *
         */
        private void SyntaxCheck(string Input, bool FinalChecking)
        {
            int InputLen = Input.Length;
            Row RSymbol;
            Column CSymbol;

            if (FinalChecking)
            {
                IdentifyChar(Input[InputLen-1], Input[InputLen-1], out RSymbol, out CSymbol);
                //CSymbol je nepoužívaná hodnota.
                if(CannotEnd[(int)RSymbol])
                {
                    /**
                     * @todo Ošetřit chybu při nesprávném ukončujícím znaku.
                     */
                }
            }

            for (int i = 0; i < InputLen - 1; i++)
            {
                IdentifyChar(Input[i], Input[i + 1], out RSymbol, out CSymbol);
                if(IncorrectFollow[(int)RSymbol, (int)CSymbol])
                {
                    /**
                     * @todo Ošetřit chybu při nesprávném následujícím znaku.
                     */
                }
            }
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
                case '(':
                    RSymbol = Row.OBracketSin;
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
                    MessageBox.Show("Error: Vyskytl se nepovolený znak na vstupu!\nKonec.");
                    Application.Current.Shutdown();
                    break;
            }

            switch(Next)
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
                    MessageBox.Show("Error: Vyskytl se nepovolený znak na vstupu!\nKonec.");
                    Application.Current.Shutdown();
                    break;
            }
            return;
        }
    }
}
