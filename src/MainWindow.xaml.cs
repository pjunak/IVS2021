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
using Calculator.Classes;
using System.Diagnostics; //TODO only for disgnostics, remove later

/**
 * @mainpage notitle
 *
 * @section zadani Zadání
 *
 * Vytvořte kalkulačku se základními matematickými operacemi (+,-,*,/), faktoriálem,
 * umocňováním s přirozenými exponenty (exponenty jsou přirozená čísla),
 * obecnou odmocninou a jednou další funkcí.<br>
 * 
 * Oficiální zadání naleznete na stránkách předmětu IVS v sekci
 * <a href="http://ivs.fit.vutbr.cz/projekt-2_tymova_spoluprace2020-21.html" target="_blank">Projekt 2: Týmová spolupráce</a>
 * 
 * @section team Vypracování
 *
 * @subsection authors Členové týmu:
 * - Dalibor Čásek
 * - Patrik Haas
 * - Kristián Heřman
 * - Petr Junák
 * @subsection additional_info Formální náležitosti projektu:
 * Projekt byl vypracován v jazyce C#, .NET Framework v 4.7.2.<br>
 * Dokumentace je psána v českém jazyce. Názvy funkcí, proměnných a další jsou v anglickém jazyce.<br>
 * Projekt byl celou dobu vypracovávan na společném repozitáři GitHub na tomto odkaze: <a href="https://github.com/pjunak/IVS2021" target="_blank">https://github.com/pjunak/IVS2021</a><br>
 * Společně s kódem jsme také vedli svoji vlastní <a href="https://github.com/pjunak/IVS2021/wiki" target="_blank">Wiki</a>, kde jsme ukládali nejdůležitější dohody ze schůzek a smluvené konvence progamu.<br>
 * Pokračujte dále na <a href="namespaces.html">Seznam prostorů jmen</a> nebo <a href="annotated.html">Seznam tříd</a>.
 */

namespace Calculator
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

        /**
         * Funkce je volána při zmáčknutí libovolné klávesy v celém okně aplikace a ošetří je (F1 = nápověda, Tab = nepovolený)
         */
        private void WindowKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                OpenHelp(sender, e);
            }
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
            }
        }

        /**
         * Otevře okno nápovědy pomocí funkce z Windows Forms
         */
        private void OpenHelp(object sender, RoutedEventArgs e)
        {
            /**
             * @todo odkomentovat správnou cestu k nápovědě pro spuštění
             */
            //string HelpFilePath = "./Napoveda/Napoveda_ver_1_0.chm";
            string HelpFilePath = "../../Napoveda/Napoveda_ver_1_0.chm";
            System.Windows.Forms.Help.ShowHelp(null, HelpFilePath);
        }

        /*private void Tokens_Test(object sender, RoutedEventArgs e)
        {
            TODO test toTokens a toPostfix
            //string inputstr = InputTextBox.Text;

            MessageBox.Show("TODO, pouze test funkčnosti toToken a toPostfix");

            string str = "10^20/(50*1)+s(20.5)!-π-5318008";


			List<Token> tokens = new List<Token>();
			tokens = ToTokens.toTokens(str);
            string message = "";
            foreach(Token token in tokens)
            {
                if(token.type == TokenType.operand)
                {
                    message += token.operand.ToString();
                }
                else
                {
                    message += token.operation;
                }
            }
            Trace.WriteLine(message);
            MessageBox.Show(message);
            
			List <Token> result = new List<Token>();
			result = ToPostfixClass.toPostfix(tokens);

            message = "";
            foreach (Token token in result)
            {
                if (token.type == TokenType.operand)
                {
                    message += token.operand.ToString();
                }
                else
                {
                    message += token.operation;
                }
            }
            Trace.WriteLine(message);
            MessageBox.Show(message);
            // Konec testů toTokens a toPostfix
        }*/

        private void ButtonClickDelete(object sender, RoutedEventArgs e)
        {
            int TextPosition = InputTextBox.CaretIndex;
            if (InputTextBox.Text.Length != 0 && InputTextBox.CaretIndex != 0)
            {
                InputTextBox.Text = InputTextBox.Text.Substring(0, InputTextBox.Text.Length - 1);
                InputTextBox.Focus();
                InputTextBox.SelectionStart = TextPosition - 1;
                InputTextBox.SelectionLength = 0;
            }
            else
            {
                InputTextBox.Focus();
                InputTextBox.SelectionStart = TextPosition;
                InputTextBox.SelectionLength = 0;
            }
        }



        private void ButtonClickDigitsOperators(object sender, RoutedEventArgs e)
        {
            string content = (sender as System.Windows.Controls.Button).Content.ToString();
            if (InputTextBox.SelectionLength == InputTextBox.Text.Length)
            {
                // TODO
                //InputTextBox.Clear();
            }
            //vloží obsah tlačítka do textboxu a posune kurzor
            int TextPosition = InputTextBox.CaretIndex;
            InputTextBox.Text = InputTextBox.Text.Insert(InputTextBox.CaretIndex, content);
            InputTextBox.Focus();
            InputTextBox.SelectionStart = TextPosition + content.Length;
            InputTextBox.SelectionLength = 0;
            

            /*
            // pokud sinus, doplní závorku otevírací
            if (content == "sin(x)")
            {
                InputTextBox.Text = InputTextBox.Text.Insert(InputTextBox.CaretIndex, "(");
                InputTextBox.SelectionStart = InputTextBox.Text.Length;
                InputTextBox.SelectionLength = 0;
            }
            */
        }

        private void ButtonClickFunctions(object sender, RoutedEventArgs e)
        {
            int TextPosition = InputTextBox.CaretIndex;
            string FuncName = (sender as Button).Name;
            TranslateShortKeys(FuncName, TextPosition);
        }

        private void TranslateShortKeys (string FuncName, int TextPosition)
        {
            switch (FuncName)
            {
                case "FuncPower":
                    InputTextBox.Text = InputTextBox.Text.Insert(InputTextBox.CaretIndex, "^");
                    InputTextBox.Focus();
                    InputTextBox.SelectionStart = TextPosition + 1;
                    InputTextBox.SelectionLength = 0;
                    break;
                case "FuncSqrt":
                    InputTextBox.Text = InputTextBox.Text.Insert(InputTextBox.CaretIndex, "^(1/");
                    InputTextBox.Focus();
                    InputTextBox.SelectionStart = TextPosition + 4;
                    InputTextBox.SelectionLength = 0;
                    break;
                case "FuncFactorial":
                    InputTextBox.Text = InputTextBox.Text.Insert(InputTextBox.CaretIndex, "!");
                    InputTextBox.Focus();
                    InputTextBox.SelectionStart = TextPosition + 1;
                    InputTextBox.SelectionLength = 0;
                    break;
                case "FuncSinus":
                    InputTextBox.Text = InputTextBox.Text.Insert(InputTextBox.CaretIndex, "s(");
                    InputTextBox.Focus();
                    InputTextBox.SelectionStart = TextPosition + 2;
                    InputTextBox.SelectionLength = 0;
                    break;
                default:
                    MessageBox.Show("Chybna funkce!");
                    break;
            }
        }

        /**
         * Funkce ignoruje mezery a nevloží je do textboxu.
         */
        private void InputTextBoxPreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;
        }

        /**
         * Funkce ignoruje jiné než povolené znaky a nevloží je do textboxu,
         * stejně tak společně s vlastností textboxu zařídí, že se všechna velká písmena převedou na malá.
         */
        private void InputTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int TextPosition = InputTextBox.CaretIndex;
            char[] InputChars = { ',', '.', 's', 'f', 'q', '(', ')', '+', '-', '*', '/', '!', '^', 'p', 'π' };
            char[] CharsToTranslate = { 's', 'f', 'q', 'm' };
            foreach (char c in e.Text)
            {

                if (CharsToTranslate.Contains(Char.ToLower(c)))
                {
                    string FuncName;
                    switch(Char.ToLower(c))
                    {
                        case 's':
                            FuncName = "FuncSinus";
                            break;
                        case 'f':
                            FuncName = "FuncFactorial";
                            break;
                        case 'q':
                            FuncName = "FuncSqrt";
                            break;
                        case 'm':
                            FuncName = "FuncPower";
                            break;
                        default:
                            FuncName = "chyba";
                            break;
                    }
                    TranslateShortKeys(FuncName, TextPosition);
                    e.Handled = true;
                }

                if (!Char.IsDigit(c) && !InputChars.Contains(Char.ToLower(c)))
                {
                    e.Handled = true;
                }
            }
        }

        /**
         * Funkce zakazuje Vkládání textu (např. pomocí Ctrl+V)
         */
        private void InputTextBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }

        /**
         * Funkce namapuje klávesové zkratky na správné znaky při vkládání do textového pole
         * např. po zadání p se přepíše na PI
         */
        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int TextPosition = InputTextBox.CaretIndex;
            TextBox box = (TextBox)sender;

            box.Text = box.Text.Replace("p", "π");


            InputTextBox.Focus();
            InputTextBox.SelectionStart = TextPosition;
            InputTextBox.SelectionLength = 0;
        }
    }
}