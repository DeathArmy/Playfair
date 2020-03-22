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

namespace Aplication
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            encryptOrDecrypt.Click += EncryptOrDecryptAction;
        }

        private void EncryptOrDecryptAction(object sender, RoutedEventArgs e)
        {
            Playfair playfair;
            short whatToDo = ((bool)decryptionCheckBox.IsChecked ? (short)1 : (short)0);
            if (key.Text == string.Empty) playfair = new Playfair(input.Text.ToUpper(), whatToDo);
            else playfair = new Playfair(key.Text, input.Text.ToUpper(), whatToDo);
            output.Text = new string (playfair.outputText);
        }
    }
}
