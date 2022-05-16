﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Sudoku_solver
{
    public class GameController : INotifyPropertyChanged
    {
        public Dictionary<int, string> GameSquares { get; set; } = new Dictionary<int, string>();
        public string Message { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void UpdateGameStatus(int pos, string value)
        {
            GameSquares[pos] = value;
            OnPropertyChanged("GameSquares");
        }

        public void ChangeMessage(string message)
        {
            Message = message;
            OnPropertyChanged("Message");
        }
        public bool CheckIfImpossible()
        {
            return Gameboard.GameSquares.Where(x => x.HasValue).Select(x => x.IsCorrect).Contains(false);
        }

        public void CallSolvePuzzle(Dictionary<int, string> userGame)
        {
            foreach (var key in userGame.Keys)
            {
                if (int.TryParse(userGame[key], out int resultValue))
                {
                    Gameboard.GameSquares.Find(x => x.Position == key).Value = resultValue;
                }
            }

            if (CheckIfImpossible())
            {
                ChangeMessage("That's impossible to solve...");
                foreach (var square in Gameboard.GameSquares)
                {
                    square.Value = null;
                }
                return;
            }

            SolveMachine.SolvePuzzle(out int BranchesUsed);

            ChangeMessage($"Branches used to solve the puzzle: {BranchesUsed}");
            GameSquares.Clear();
            foreach (var square in Gameboard.GameSquares)
            {
                GameSquares[square.Position] = square.Value.ToString();
            }
            OnPropertyChanged("GameSquares");
            OnPropertyChanged("Message");
        }

        public void ClearTable()
        {
            ChangeMessage("Cleared");
            OnPropertyChanged("Message");
            foreach (var square in Gameboard.GameSquares)
            {
                square.Value = null;
                UpdateGameStatus(square.Position, square.Value.ToString());
            }
            OnPropertyChanged("GameSquares");
        }

        public GameController()
        {
            foreach (var square in Gameboard.GameSquares)
            {
                GameSquares.Add(square.Position, square.Value.ToString());
            }

        }
    }
}
