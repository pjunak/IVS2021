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
    }
}
