/*
Calculator, FITness StudIO 21
Copyright (C)

MainWindow.xaml.cs: Handler for UI interaction.
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
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;
using Calculator.Classes;
using System.Timers;

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
        /**
         * Časovač pro tlačítko mazání.
         */
        private Timer DeleteTimer;

        /**
         * Přepínač smazání celého výrazu (držení tlačítka na 500ms) nebo smazání jednoho znaku.
         */
        private bool TimeUP;

        public MainWindow()
        {
            InitializeComponent();
            TimeUP = false;
            DeleteTimer = new Timer();
            DeleteTimer.AutoReset = false;
            DeleteTimer.Interval = 500;
            DeleteTimer.Elapsed += OnTimedEvent;
        }

        /**
         * Přepnutí přepínače \c TimeUp při dovršení času.
         */
        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            TimeUP = true;
            this.Dispatcher.Invoke(() =>
            {
                InputTextBox.Clear();
                InputTextBox.Focus();
            });
        }

        /**
         * Funkce je volána při zmáčknutí libovolné klávesy v celém okně aplikace a ošetří je (F1 = nápověda, Tab = nepovolený).
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
         * Otevře okno nápovědy pomocí funkce převzaté z Windows Forms (nápověda ve formátu CHM).
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

        /**
         * Spuštění časovače \c DeleteTimer.
         */
        private void ButtonDownDelete(object sender, RoutedEventArgs e)
        {
            TimeUP = false;
            DeleteTimer.Stop();
            DeleteTimer.Enabled = true;
        }

        /**
         * Vynulování časovače \c DeleteTimer. Vyhodnocení, co se smaže ze vstupu.
         */
        private void ButtonUpDelete(object sender, RoutedEventArgs e)
        {
            DeleteTimer.Enabled = false;
            DeleteTimer.Stop();
            if(!TimeUP)
            {
                ButtonClickDelete(sender, e);
            }
            else
            {
                return;
            }
        }

        /**
         * Ošetří mazání při stisku tlačítka Delete v grafickém rozhraní.
         */
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


        /**
         * Vloží stisknutý operátor nebo číslo do vstupního pole kalkulačky.
         */
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

            int SelectionLength = InputTextBox.SelectionLength;
            if (SelectionLength > 0)
            {
                if (InputTextBox.SelectionStart != 0 && InputTextBox.Text[InputTextBox.SelectionStart - 1] == 's')
                {   // je přepisována pravá závorka sinu, musí se smazat i s
                    TextPosition--;
                    SelectionLength++;
                }
                InputTextBox.Text = InputTextBox.Text.Remove(TextPosition, SelectionLength);
            }

            if ((sender as Button).Name == "OperatorMul")
            {   // vloží operátor násobení
                InputTextBox.Text = InputTextBox.Text.Insert(TextPosition, "*");
                TextPosition++;
            }
            else if ((sender as Button).Name == "OperatorDiv")
            {   // vloží operátor násobení
                InputTextBox.Text = InputTextBox.Text.Insert(TextPosition, "/");
                TextPosition++;
            }
            else
            {   // vloží obsah tlačítka (číslice, +, -)

                //Jestli je v textox výsledek, tak po stlačení čísla myší, se obsah přemaže daným číslem
                var vm = (MainWindowClass)DataContext;
                if (vm.CheckAfterFinal && (char.IsDigit(content[0]) || (content[0] == 'π')))
                {
                    InputTextBox.Text = content;
                    TextPosition = 1;
                }
                else
                {
                    InputTextBox.Text = InputTextBox.Text.Insert(TextPosition, content);
                    TextPosition += content.Length;
                }
            }

            InputTextBox.CaretIndex = TextPosition;
            InputTextBox.Focus();
        }

        /**
         * Vloží do vstupního pole stisknutou funkci pomocí tlačíka v GUI.
         */
        private void ButtonClickFunctions(object sender, RoutedEventArgs e)
        {
            int TextPosition = InputTextBox.CaretIndex;
            int SelectionLength = InputTextBox.SelectionLength;
            if (SelectionLength > 0)
            {
                if (InputTextBox.SelectionStart != 0 && InputTextBox.Text[InputTextBox.SelectionStart - 1] == 's')
                {   // je přepisována pravá závorka sinu, musí se smazat i s
                    TextPosition--;
                    SelectionLength++;
                }
                InputTextBox.Text = InputTextBox.Text.Remove(TextPosition, SelectionLength);
            }
            string FuncName = (sender as Button).Name;
            TranslateShortKeys(FuncName, TextPosition);
        }

        /**
         * Přeloží funkci z výčtu funkcí (enum) na konkrétní funkci a vloží ji do vstupu.
         */
        private void TranslateShortKeys (string FuncName, int TextPosition)
        {
            switch (FuncName)
            {
                case "FuncPower":
                    InputTextBox.Text = InputTextBox.Text.Insert(TextPosition, "^");
                    InputTextBox.Focus();
                    InputTextBox.SelectionStart = TextPosition + 1;
                    InputTextBox.SelectionLength = 0;
                    break;
                case "FuncSqrt":
                    InputTextBox.Text = InputTextBox.Text.Insert(TextPosition, "^(1/");
                    InputTextBox.Focus();
                    InputTextBox.SelectionStart = TextPosition + 4;
                    InputTextBox.SelectionLength = 0;
                    break;
                case "FuncFactorial":
                    InputTextBox.Text = InputTextBox.Text.Insert(TextPosition, "!");
                    InputTextBox.Focus();
                    InputTextBox.SelectionStart = TextPosition + 1;
                    InputTextBox.SelectionLength = 0;
                    break;
                case "FuncSinus":
                    InputTextBox.Text = InputTextBox.Text.Insert(TextPosition, "s(");
                    InputTextBox.Focus();
                    InputTextBox.SelectionStart = TextPosition + 2;
                    InputTextBox.SelectionLength = 0;
                    break;
                case "PiOperand":
                    InputTextBox.Text = InputTextBox.Text.Insert(TextPosition, "π");
                    InputTextBox.Focus();
                    InputTextBox.SelectionStart = TextPosition + 1;
                    InputTextBox.CaretIndex = TextPosition + 1;
                    InputTextBox.SelectionLength = 0;
                    break;
                case "OBrackOperator":
                    InputTextBox.Text = InputTextBox.Text.Insert(TextPosition, "(");
                    InputTextBox.Focus();
                    InputTextBox.SelectionStart = TextPosition + 1;
                    InputTextBox.SelectionLength = 0;
                    break;
                case "CBrackOperator":
                    InputTextBox.Text = InputTextBox.Text.Insert(TextPosition, ")");
                    InputTextBox.Focus();
                    InputTextBox.SelectionStart = TextPosition + 1;
                    InputTextBox.SelectionLength = 0;
                    break;
                default:
                    MessageBox.Show("Neexistující funkce!");
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
                    int SelectionLength = InputTextBox.SelectionLength;
                    if (SelectionLength > 0)
                    {
                        if (InputTextBox.SelectionStart != 0 && InputTextBox.Text[InputTextBox.SelectionStart - 1] == 's')
                        {   // je přepisována pravá závorka sinu, musí se smazat i s
                            TextPosition--;
                            SelectionLength++;
                        }
                        InputTextBox.Text = InputTextBox.Text.Remove(TextPosition, SelectionLength);
                    }

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
                    var vm = (MainWindowClass)DataContext;
                    if (FuncName == "PiOperand" && vm.CheckAfterFinal)
                    {
                        InputTextBox.Text = "π";
                        InputTextBox.CaretIndex = 1;
                    }
                    else
                    {
                        TranslateShortKeys(FuncName, TextPosition);
                    }
                    e.Handled = true;
                }

                if (!Char.IsDigit(c) && !InputChars.Contains(Char.ToLower(c)))
                {
                    e.Handled = true;
                }
                InputTextBox.Focus();
            }
        }

        private string LastValue { get; set; }

        /**
         * Po stisknutí rovná se a následném doplnění čísla či operátoru řeší metoda, jestli se pokračuje ve výpočtu, nebo se vstup vymaže.
         */
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
				if (LastValue.Length <= InputTextBox.Text.Length && InputTextBox.CaretIndex == InputTextBox.Text.Length && (char.IsDigit(InputTextBox.Text[InputTextBox.Text.Length - 1]) || InputTextBox.Text[InputTextBox.Text.Length - 1] == 'π'))
				{
					vm.Input = InputTextBox.Text[InputTextBox.Text.Length - 1].ToString();
					InputTextBox.CaretIndex = InputTextBox.Text.Length;
					InputTextBox.Focus();
                }
			}
            LastValue = InputTextBox.Text;
        }

        /**
         * Přebarví okraj textboxu při ztrátě focusu z tohoto textboxu.
         */
		private void InputTextBox_LostFocus(object sender, RoutedEventArgs e)
		{
			SolidColorBrush blackBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFABADB3"));
			Okraj.Stroke = blackBrush;
        }

        /**
         * Přebarví okraj textboxu při zisku focusu na tento textbox.
         */
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

        private void SetFocus(object sender, EventArgs e)
        {
            InputTextBox.CaretIndex = InputTextBox.Text.Length;
            InputTextBox.Focus();
        }

        /**
         * Navazuje bindingové funkce pro procházení historie při scrollování kolečkem myši nahoru/dolů.
         */
        private void InputTextBox_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                var vm = (MainWindowClass)DataContext;
                vm.BackInHistoryMethod();
            }
            else
            {
                var vm = (MainWindowClass)DataContext;
                vm.ForwardInHistoryMethod();
            }
        }
    }
}