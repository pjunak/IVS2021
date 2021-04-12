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
            string HelpFilePath = "./Napoveda/Napoveda_ver_1_0.chm";

            if (!File.Exists(HelpFilePath))
            {
                HelpFilePath = "../../Napoveda/Napoveda_ver_1_0.chm";
            }
            
            InputTextBox.SelectionStart = InputTextBox.CaretIndex;
            InputTextBox.SelectionLength = 0;
            InputTextBox.Focus();
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
            if (InputTextBox.SelectionLength == InputTextBox.Text.Length && InputTextBox.Text.Length != 0)
            {   // Smazání celého textu
                InputTextBox.Clear();
            }
            if (InputTextBox.Text.Length != 0 && InputTextBox.SelectionLength != 0)
            {   // Při výběru většího úseku smaže celý úsek
                InputTextBox.Text = InputTextBox.Text.Remove(InputTextBox.SelectionStart, InputTextBox.SelectionLength);
                InputTextBox.Focus();
                InputTextBox.SelectionStart = TextPosition - InputTextBox.SelectionStart;
                InputTextBox.SelectionLength = 0;
            }
            else if (InputTextBox.Text.Length != 0 && InputTextBox.CaretIndex != 0)
            {   // Smazání jednoho znaku od pozice kurzoru
                if (TextPosition == InputTextBox.Text.Length && TextPosition >= 2)
                {   // Smazání na konci řetězce
                    int CaretPosition;
                    if (InputTextBox.Text[TextPosition - 2] == 's')
                    {
                        CaretPosition = TextPosition - 2;
                    }
                    else
                    {
                        CaretPosition = TextPosition - 1;
                    }
                    InputTextBox.Text = InputTextBox.Text.Substring(0, InputTextBox.Text.Length - 1);
                    InputTextBox.CaretIndex = CaretPosition;
                }
                else
                {   // Smazání uprostřed řetězce
                    int CaretPosition;
                    if (TextPosition >= 2 && InputTextBox.Text[TextPosition - 2] == 's')
                    {
                        InputTextBox.Focus();
                        CaretPosition = TextPosition - 2;
                    }
                    else
                    {
                        InputTextBox.Focus();
                        CaretPosition = TextPosition - 1;
                    }
                    string BeforeDel = InputTextBox.Text.Substring(0, TextPosition - 1);
                    string AfterDel = InputTextBox.Text.Substring(TextPosition, (InputTextBox.Text.Length - TextPosition));
                    InputTextBox.Text = BeforeDel + AfterDel;
                    InputTextBox.CaretIndex = CaretPosition;
                }
                
                //InputTextBox.SelectionStart = TextPosition - 1;
                InputTextBox.SelectionLength = 0;
                InputTextBox.Focus();
            }
            else
            {
                // Nastavení kurzoru, nesmaže se nic
                InputTextBox.Focus();
                InputTextBox.SelectionStart = TextPosition;
                InputTextBox.SelectionLength = 0;
            }
        }



        private void ButtonClickDigitsOperators(object sender, RoutedEventArgs e)
        {
            Button BtnObject = (System.Windows.Controls.Button)e.Source;
            Viewbox ViewboxObject = (Viewbox)BtnObject.Content;
            TextBlock TextBlockObject = (TextBlock)ViewboxObject.Child;
            string content = TextBlockObject.Text;
            if (InputTextBox.SelectionLength == InputTextBox.Text.Length && InputTextBox.Text.Length != 0)
            {
                InputTextBox.Clear();
            }
            int TextPosition = InputTextBox.CaretIndex;

            if ((sender as Button).Name == "OperatorMul")
            {   // vloží operátor násobení
                InputTextBox.Text = InputTextBox.Text.Insert(InputTextBox.CaretIndex, "*");
                TextPosition++;
            }
            else if ((sender as Button).Name == "OperatorDiv")
            {   // vloží operátor násobení
                InputTextBox.Text = InputTextBox.Text.Insert(InputTextBox.CaretIndex, "/");
                TextPosition++;
            }
            else
            {   // vloží obsah tlačítka (číslice, +, -)
                InputTextBox.Text = InputTextBox.Text.Insert(InputTextBox.CaretIndex, content);
                TextPosition += content.Length;
            }

            InputTextBox.CaretIndex = TextPosition;
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
                case "PiOperand":
                    InputTextBox.Text = InputTextBox.Text.Insert(InputTextBox.CaretIndex, "π");
                    InputTextBox.Focus();
                    InputTextBox.SelectionStart = TextPosition + 1;
                    InputTextBox.SelectionLength = 0;
                    break;
                case "OBrackOperator":
                    InputTextBox.Text = InputTextBox.Text.Insert(InputTextBox.CaretIndex, "(");
                    InputTextBox.Focus();
                    InputTextBox.SelectionStart = TextPosition + 1;
                    InputTextBox.SelectionLength = 0;
                    break;
                case "CBrackOperator":
                    InputTextBox.Text = InputTextBox.Text.Insert(InputTextBox.CaretIndex, ")");
                    InputTextBox.Focus();
                    InputTextBox.SelectionStart = TextPosition + 1;
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
            char[] InputChars = { ',', '.', 's', 'f', 'q', '(', ')', '+', '-', '*', '/', '!', '^', 'p', 'π', 'v', 'b' };
            char[] CharsToTranslate = { 's', 'f', 'q', 'm', 'p', 'v', 'b' };
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
                        case 'p':
                            FuncName = "PiOperand";
                            break;
                        case 'v':
                            FuncName = "OBrackOperator";
                            break;
                        case 'b':
                            FuncName = "CBrackOperator";
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
                InputTextBox.Focus();
                //InputTextBox.SelectionStart = TextPosition + 1;
                //InputTextBox.SelectionLength = 0;
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

        private string LastValue { get; set; }

        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
            var vm = (MainWindowClass)DataContext;
			if (vm.Final)
			{
				InputTextBox.CaretIndex = InputTextBox.Text.Length;
				InputTextBox.Focus();
			}
			if (vm.CheckAfterFinal)
			{
				vm.CheckAfterFinal = false;
				if (LastValue.Length <= InputTextBox.Text.Length && InputTextBox.CaretIndex == InputTextBox.Text.Length && char.IsDigit(InputTextBox.Text[InputTextBox.Text.Length - 1]))
				{
					vm.Input = InputTextBox.Text[InputTextBox.Text.Length - 1].ToString();
					InputTextBox.CaretIndex = InputTextBox.Text.Length;
					InputTextBox.Focus();
                }
			}
            LastValue = InputTextBox.Text;
        }

		private void InputTextBox_LostFocus(object sender, RoutedEventArgs e)
		{
			SolidColorBrush blackBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFABADB3"));
			Okraj.Stroke = blackBrush;
		}

		private void InputTextBox_GotFocus(object sender, RoutedEventArgs e)
		{
			SolidColorBrush blackBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF0078D7"));
			Okraj.Stroke = blackBrush;
		}

		private void Window_Activated(object sender, EventArgs e)
		{
			InputTextBox_GotFocus(new object(), new RoutedEventArgs());
		}

		private void Window_Deactivated(object sender, EventArgs e)
		{
			InputTextBox_LostFocus(new object(), new RoutedEventArgs());
		}
	}
}