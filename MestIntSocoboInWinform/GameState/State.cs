using MestIntSocoboInWinform.PlayerMovement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MestIntSocoboInWinform.GameState
{
    class State
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
                { "w","p","f","f","w","w","w","w","w" },
                { "w","f","o","o","w","w","w","w","w" },
                { "w","f","o","f","w","w","w","g","w" },
                { "w","w","w","f","w","w","w","g","w" },
                { "w","w","w","f","f","f","f","g","w" },
                { "w","w","f","f","f","w","f","f","w" },
                { "w","w","f","f","f","w","w","w","w" },
                { "w","w","w","w","w","w","w","w","w" }
        };

        private string[,] currentState;

        public static State GetInstance()
        {
            if (state == null)
            {
                state = new State();
                state.currentState = startingState;
            }
            return state;
        }

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

        public bool IsItTargetState()
        {
            return currentState[3, 7] == "o" && currentState[4, 7] == "o" && currentState[5, 7] == "o";
        }

        public void LoadDefaultState()
        {
            state.currentState = startingState;
        }



    }
}
