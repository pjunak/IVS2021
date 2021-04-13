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
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Calculator.Classes
{
	/**
	* @class Convertor
	* Táto třída dedí z IValueConverto, je u ní nutné implementovat rozhraní Convert, ConvertBack.
	* Jelikož, nám není na potřebí ConvertBack, tak zústava neimplementován
	* @brief Třída pro úpravu fontu v hlavním textboxu
	*/
	class Convertor : IValueConverter
	{
		/** 
         * @brief Když se má hodnota co je nabindgovaná uložit do promnené v xml, je zavolána táto metoda, která dynamický mnení font.
         * @param Value hodnota, která sa posíla do konvertoru.
         * @param targetType Typ value, neni nám za potřebí
         * @param parameter Parameter pro konvertor
         * @param culture informace o kultúře, nepotřebujeme
         * @return Vrátu novou hodnotu fontu, číslo double.
         */
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (parameter.ToString() == "MainWindow")
			{
				var Number = System.Convert.ToDouble(value);
				if (Number < 337) return 337;
				else return Number;
			}
			else
			{
				var Vyska = System.Convert.ToDouble(value);
				return Math.Abs(Vyska / 3);
			}
		}

		/** 
         * @brief Táto metodá musí byt implemetovaná, ale jelikoš mi ju nepoužívamé, zústava v základním stavu, při vygenerovaní ve Visual Studiu
         */
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
