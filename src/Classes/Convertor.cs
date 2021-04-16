/*
Calculator, FITness StudIO 21
Copyright (C)

Convertor.cs: Processing tool for MainWindow.xaml
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
using System.Globalization;
using System.Windows.Data;

namespace Calculator.Classes
{
	/**
	* @class Convertor
	* Tato třída dědí z IValueConvertor, je u ní nutné implementovat rozhraní Convert, ConvertBack.
	* Jelikož nepotřebujeme ConvertBack, tak zustává neimplementován.
	* @brief Třída pro úpravu fontu v hlavním textboxu.
	*/
	class Convertor : IValueConverter
	{
		/** 
         * @brief Když se má hodnota, co je nabindovaná, uložit do proměnné v xml, je zavolána tato metoda, která dynamický mění font.
         * @param Value Hodnota, která sa posílá do konvertoru.
         * @param targetType Typ value, nepotřebujeme.
         * @param parameter Parameter pro konvertor pro rozlišení objektu.
         * @param culture Informace o kultuře, nepotřebujeme.
         * @return Vrátí novou hodnotu fontu, číslo double.
         */
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if(parameter.ToString() == "ExpressionFont")
            {
				double Vyska = System.Convert.ToDouble(value);
				return Math.Abs(Vyska / 1.8);
			}
			else if (parameter.ToString() == "ResultFont")
			{
				double Vyska = System.Convert.ToDouble(value);
				return Math.Abs(Vyska / 1.6);
			}
			else if(parameter.ToString() == "InputFont")
			{
                double Vyska = System.Convert.ToDouble(value);
				return Math.Abs(Vyska / 3);
            }
			else
            {
				System.Windows.MessageBox.Show("Interní chyba konvertoru.");
				return 0;
            }
		}

		/** 
         * @brief Tato metoda musí byt implemetovaná, ale jelikož my ji nepoužíváme, zůstává v základním stavu při vygenerovaní ve Visual Studiu.
         */
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
