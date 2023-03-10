using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication41.Models;


namespace WebApplication41.Containers {
    public interface Checker
    {
        public Status CheckValidBoar(IEnumerable<string> gameBoard);
        

        public string WhichPlayersTurn(IEnumerable<string> gameBoard);

        public List<int> WinningScenario(IEnumerable<string> gameBoard);
       
        public string Winner(IEnumerable<string> gameBoard);


  
        public IEnumerable<string> PutRandomField(IEnumerable<string> gameBoard,  string playerSymbol);
       

    }

    public class BoardChecker: Checker {
    
        public static readonly string X = "X";

        public static readonly string O = "O";
       
        private static readonly string EMPTY = "*";

        public static readonly string INCONCLUSIVE = "inconclusive";
       
        private static readonly string TIE = "tie";

        private static readonly string[] Symbols = { X, O, EMPTY };

     
        private static readonly List<List<int>> winningIndexes = new List<List<int>> {
          
            new List<int> { 0, 1, 2 },
            new List<int> { 3, 4, 5 },
            new List<int> { 6, 7, 8 },
         
            new List<int> { 0, 3, 6 },
            new List<int> { 1, 4, 7 },
            new List<int> { 2, 5, 8 },
            
            new List<int> { 0, 4, 8 },
            new List<int> { 2, 4, 6 },
        };

        public  static readonly List<string> clearfields = new List<string> 

             { "*", "*", "*" ,
            "*", "*", "*"  ,
          "*", "*", "*"   

           
        };


       
       

       
        public Status CheckValidBoar (IEnumerable<string> gameBoard) {
            if (gameBoard.Count () != 9) {
                return new Status {
                    Message = "У поля только 9 элементов" , Code = 400 };
            }
            if (!gameBoard.All (space => Symbols.Contains (space))) {
                return new Status
                {
                    Message = "Элементы поля должны содержать следующие символы \"X\", \"O\", or \"*\".",
                    Code = 400
                };
                
            }
            return new Status
            {
                Message = "",
                Code = 200
            };

        }

        
        public string WhichPlayersTurn (IEnumerable<string> gameBoard) {
           
            return gameBoard.Count (x => x == O) >=
                gameBoard.Count (x => x == X) ?
                X :
                O;
        }

       
        private IEnumerable<string> GetScenarioState (IEnumerable<string> gameBoard , IEnumerable<int> scenario) {
            return scenario.Select (i => gameBoard.ElementAt (i));
        }

       
        public List<int> WinningScenario (IEnumerable<string> gameBoard) {
            foreach (List<int> winScenario in winningIndexes) {
                IEnumerable<string> scenario = this.GetScenarioState(gameBoard, winScenario);

               
                string first = scenario.First ();
                if (first != EMPTY) {
                    Boolean hasWinner = scenario.Skip (1).All (s => s == first);
                    if (hasWinner) {
                        return winScenario;
                    }
                }
            }
            return null;
        }

     
        public string Winner (IEnumerable<string> gameBoard)
        {
            List<int> winScenario = this.WinningScenario(gameBoard);
            if (winScenario != null) {
                return this.GetScenarioState(gameBoard, winScenario).First();
            }
            return gameBoard
                .All (s => s != EMPTY) ?
                TIE :
                INCONCLUSIVE;
        }

        
        public IEnumerable<string> PutRandomField (IEnumerable<string> gameBoard, string playerSymbol) {

            IEnumerable<int> indexes = gameBoard.Select ((i, a) => a);
            IEnumerable<int>  notselected = indexes.Where(i => gameBoard.ElementAt(i) != EMPTY);
            Random rnd = new Random();
            int move = notselected.ElementAt(rnd.Next(0, notselected.Count()));

            return  gameBoard.Select ((s, i) =>
                    i == move ?
                    playerSymbol :
                    s
                );
        }

       
        
    }
}
