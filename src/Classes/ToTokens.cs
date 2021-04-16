/*
Calculator, FITness StudIO 21
Copyright (C)

ToTokens.cs: Decomposing the user input to tokens.
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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Calculator.Classes
{
    /**
     * @class ToTokens
     * 
     * @brief Třída pro převod vstupního stringu na seznam tokenů pro další práci s matematickou knihovnou
     */
    public class ToTokens
    {
        public ToTokens() { }
        /** 
         * @brief Převede vstup kalkulačky ze stringu do tokenů
         * @param str Vstupní řetězec z formulářového okna
         * @return Vrátí List datového typu Token
         */
        public List<Token> toTokens(string str)
        {
            if (str.Length > 0 && (str[0] == '-' || str[0] == '+'))
            {// Ošetření implicitní nuly na začátku vstupního výrazu v případě začnutí výrazu záporným, nebo explicitním kladným číslem
                str = str.Insert(0, "0");
            }
            int i = 0;
            while (i < (str.Length - 2))
            {// Ošetření implicitní uvnitř stringu
                if (str[i] == '(' && (str[i + 1] == '+' || str[i + 1] == '-'))
                {// před plus a mínus může být implicitní nula pro záporné a explicitně kladné čísla
                    str = str.Insert(i + 1, "0");
                }
                else if (!Char.IsDigit(str[i]) && (str[i + 1] == '.' || str[i + 1] == ','))
                {// před desetinnou tečkou či čárkou může bůt implicitní nula
                    str = str.Insert(i + 1, "0");
                }
                i++;
            }

            List<Token> tokens = new List<Token>();
            string number = string.Empty;
            Token token = new Token();

            foreach (char ch in str)
            {// Převod na tokeny
                if (Char.IsDigit(ch))
                {
                    number += ch;
                }
                else if (ch == '.' || ch == ',')
                {
                    if (!number.Contains('.'))
                    {
                        number += '.';
                    }
                    else
                    {
                        return null;
                    }

                }
                else
                {
                    if (!string.IsNullOrEmpty(number))
                    { // Přidání aktuaálního stringu číslic jako číslo do tokenů
                        token.type = TokenType.operand;
                        token.operand = Convert.ToDouble(number, new CultureInfo("en-US"));
                        tokens.Add(token);
                        number = string.Empty;
                        token = new Token();
                    }
                    if (ch == 'π')
                    {
                        token.type = TokenType.operand;
                        token.operand = Math.PI;
                        tokens.Add(token);
                        token = new Token();
                    }
                    else if (ch == '+' || ch == '-')
                    {
                        token.type = TokenType.plusMinus;
                        token.operation = ch;
                        tokens.Add(token);
                        token = new Token();
                    }
                    else if (ch == '*' || ch == '×' || ch == '/' || ch == '÷')
                    {
                        token.type = TokenType.mulDiv;
                        token.operation = ch;
                        tokens.Add(token);
                        token = new Token();
                    }
                    else if (ch == '^' || ch == '!' || ch == 's')
                    {
                        token.type = TokenType.powerFactorSin;
                        token.operation = ch;
                        tokens.Add(token);
                        token = new Token();
                    }
                    else if (ch == '(' || ch == ')')
                    {
                        token.type = TokenType.brackets;
                        token.operation = ch;
                        tokens.Add(token);
                        token = new Token();
                    }
                    else if (Char.IsWhiteSpace(ch))
                    {
                        return null; // Neočekávaná situace, whitespace na vstupu
                    }
                    else
                    {
                        return null; // Neočekávaná situace, neočekávaný znak na vstupu
                    }
                }
            }
            if (!string.IsNullOrEmpty(number))
            {
                token.type = TokenType.operand;
                token.operand = Convert.ToDouble(number, new CultureInfo("en-US"));
                tokens.Add(token);
                if (tokens.Count == 1 && (number == "5318008" || number == "58008" || number == "42"))
                { // Easter Egg catch

                    // Přehraje zvukové upozornění na easter egg
                    string soundsrc = "EasterEgg/EasterEgg.wav";
                    if (!File.Exists(soundsrc))
                    {
                        soundsrc = "../../EasterEgg/EasterEgg.wav";
                    }
                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(@Path.GetFullPath(soundsrc));
                    player.Play();

                    Random rnd = new Random();
                    int randInt = rnd.Next(1,6);

                    int h = 0;
                    int w = 0;
                    string imgsrc = "";

                    // Vybere náhodně jeden z easter eggů
                    if (randInt == 1)
                    {
                        h = 692;
                        w = 431;
                        imgsrc = "EasterEgg/FuchsandFourier.png";
                        if (!File.Exists(imgsrc))
                        {
                            imgsrc = "../../EasterEgg/FuchsandFourier.png";
                        }
                    }
                    else if (randInt == 2)
                    {
                        h = 565;
                        w = 485;
                        imgsrc = "EasterEgg/Body.png";
                        if (!File.Exists(imgsrc))
                        {
                            imgsrc = "../../EasterEgg/Body.png";
                        }
                    }
                    else if (randInt == 3)
                    {
                        h = 530;
                        w = 742;
                        imgsrc = "EasterEgg/IDontWantToPlayWithFitKitAnymore.png";
                        if (!File.Exists(imgsrc))
                        {
                            imgsrc = "../../EasterEgg/IDontWantToPlayWithFitKitAnymore.png";
                        }
                    }
                    else if (randInt == 4)
                    {
                        h = 736;
                        w = 350;
                        imgsrc = "EasterEgg/Howelementary.png";
                        if (!File.Exists(imgsrc))
                        {
                            imgsrc = "../../EasterEgg/Howelementary.png";
                        }
                    }
                    else if (randInt == 5)
                    {
                        h = 515;
                        w = 364;
                        imgsrc = "EasterEgg/Basen.png";
                        if (!File.Exists(imgsrc))
                        {
                            imgsrc = "../../EasterEgg/Basen.png";
                        }
                    }

                    // Zobrazí okno seaster eggem
                    System.Windows.Controls.Grid grid = new System.Windows.Controls.Grid();
                    Window wnd = new Window() { Height = h, Width = w, Content = grid };

                    System.Windows.Controls.Image img = new System.Windows.Controls.Image();

                    img.Source = new BitmapImage(new Uri(imgsrc, UriKind.Relative));

                    System.Windows.Controls.Grid.SetRow(img, 0);
                    grid.Children.Add(img);
                    wnd.ShowDialog();
                }
                token = new Token();
            }

            return tokens;
        }
    }
}
