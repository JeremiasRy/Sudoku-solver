using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Sudoku_solver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Regex inputCheck = new Regex("^D[0-9]");
        public GameController control;

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

        private void CheckValue(object sender, KeyEventArgs e) 
        {
            if (!inputCheck.IsMatch(e.Key.ToString()))
            {
                e.Handled = true;
            }
        }
    }
}
