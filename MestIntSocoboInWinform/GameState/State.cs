using MestIntSocoboInWinform.PlayerMovement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MestIntSocoboInWinform.GameState
{
    class State : StateSpace
    {
        private State() { }
        private static State state;

        // wall = w
        // floor = f
        // player = p
        // obstacle = o
        // g = goal
       
        private static readonly string[,] startingState =  {
                { "w","w","w","w","w","w","w","w","w" },
                { "w","p","f","f","f","f","f","f","w" },
                { "w","f","o","o","f","f","f","f","w" },
                { "w","f","o","f","f","f","f","f","w" },
                { "w","f","f","f","g","f","f","f","w" },
                { "w","f","f","f","f","f","f","f","w" },
                { "w","f","f","g","f","g","f","f","w" },
                { "w","f","f","f","f","f","f","f","w" },
                { "w","w","w","w","w","w","w","w","w" }
        };

        public string[,] currentState;
        public Stack<string[,]> stack = new Stack<string[,]>();

        public static State GetInstance()
        {
            if (state == null)
            {
                state = new State();
                // Kezdőállapot beállítása a "konstruktorban"
                state.currentState = startingState;
                state.updateNextMoves();
            }
            return state;
        }

        public List<string[,]> nextMoves = new List<string[,]>();
        public void updateNextMoves()
        {
            nextMoves.Clear();
            nextMoves = Movement.GetNextMoves(currentState);
        }


        // Szuper operátor
        public void UpdateState(MovementTypes mt)
        {
            switch (mt)
            {
                case MovementTypes.LEFT:
                    Movement.MoveLeft(ref currentState);
                    break;
                case MovementTypes.RIGHT:
                    Movement.MoveRight(ref currentState);
                    break;
                case MovementTypes.UP:
                    Movement.MoveUp(ref currentState);
                    break;
                case MovementTypes.DOWN:
                    Movement.MoveDown(ref currentState);
                    break;
                default:
                    break;
            }
        }

        public string[,] GetState()
        {
            return this.currentState;
        }

        private static List<Coordinates> GetWinningCoordinates()
        {
            List<Coordinates> winningCoordinates = new List<Coordinates>();
            for (int i = 0; i < startingState.GetLength(0); i++)
            {
                for (int j = 0; j < startingState.GetLength(1); j++)
                {
                    if (startingState[i, j] == "g")
                    {
                        Coordinates xy = new Coordinates();
                        xy.x = i;
                        xy.y = j;
                        winningCoordinates.Add(xy);
                    }

                }
            }
            return winningCoordinates;
        }

        List<Coordinates> winningCoordinates = GetWinningCoordinates();
        public bool IsItTargetState()
        {
            return currentState[winningCoordinates[0].x, winningCoordinates[0].y] == "o" && 
                   currentState[winningCoordinates[1].x, winningCoordinates[1].y] == "o" 
                   && currentState[winningCoordinates[2].x, winningCoordinates[2].y] == "o";
        }

        public void LoadDefaultState()
        {
            state.currentState = startingState;
        }

        public bool IsStepBackNeeded()
        {
            bool result = false;
            for (int i = 0; i < state.currentState.GetLength(0); i++) // sor
            {
                for (int j = 0; j < state.currentState.GetLength(1); j++) // oszlop
                {
                    // i+1,j-1
                    //i - 1 ,j + 1

                    if (state.currentState[i,j] == "o" &&
                        ObjectStucked(state.currentState[i+1,j], state.currentState[i-1,j],
                        state.currentState[i,j-1], state.currentState[i,j+1]))
                    {
                        result = true;
                        return result;
                    }
                }
            }
            return result;
        }

        public bool ObjectStucked(string bottomCol, string topCol, string leftCol, string rightCol)
        {
            return bottomCol == "w" || topCol == "w" || leftCol == "w" || rightCol == "w";
        }



    }
}
