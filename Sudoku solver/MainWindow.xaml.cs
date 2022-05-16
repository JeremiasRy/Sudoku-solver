using System.Windows;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Windows.Input;

namespace Sudoku_solver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public GameController control;
        Regex values = new Regex("[0-9]");
        

        public MainWindow()
        {
            InitializeComponent();
            control = Resources["control"] as GameController;  
        }

        private void SolvePuzzle(object sender, RoutedEventArgs e)
        {
            control.CallSolvePuzzle(control.GameSquares);
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            control.ClearTable();
        }

        private void CheckValue(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.ToString() != Key.D1.ToString() && e.Key.ToString() != Key.D2.ToString() && e.Key.ToString() != Key.D3.ToString() && e.Key.ToString() != Key.D5.ToString() && e.Key.ToString() != Key.D6.ToString() && e.Key.ToString() != Key.D7.ToString() && e.Key.ToString() != Key.D8.ToString() && e.Key.ToString() != Key.D9.ToString()) 
            {
                e.Handled = true;
            }
        }
    }
}
