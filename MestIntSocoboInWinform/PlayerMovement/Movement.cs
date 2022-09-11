using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MestIntSocoboInWinform.PlayerMovement
{
    class Movement
    {
        private static HorizontalMovement hm = new HorizontalMovement();
        private static VerticalMovement vm = new VerticalMovement();
        // Alap ötletem:
        // a p object tud mozogni? nem megy neki a falnak? movement.CanPlayerMove
        // van előtte akadály? ha nincs akkor lépek
        // ha van előtte lehet tolni? CanObstacleBePushed, ha igen lehet mozgatni.
        public static void MoveLeft(ref string[,] state)
        {
            for (int i = 0; i < state.GetLength(0); i++) // sor
            {
                for (int j = 0; j < state.GetLength(1); j++) // oszlop
                {
                    // Megnézzük tud-e mozogni a plyer balra (i, j-1)
                    if (state[i, j] == "p" && hm.CanPlayerMove(state[i, j-1]))
                    {
                        if (hm.isThereObstacleInWay(state[i, j - 1]))
                        {
                            // Ha w marad akkor fal van ott, vagy kimentünk volna a pályáról.
                            string wallCheck = "w";
                            try { wallCheck = state[i, j - 2]; } catch (Exception) { }
                            if (hm.CanObstacleBePushed(wallCheck))
                            {
                                // 
                                state[i, j] = state[i, j - 2];
                                state[i, j - 1] = "p";
                                state[i, j - 2] = "o";
                                return;
                            }
                        }
                        else
                        {
                            state[i, j] = state[i, j - 1];
                            state[i, j - 1] = "p";
                            return;
                        }

                    }
                }
            }
        }

        public static void MoveRight(ref string[,] state)
        {

            for (int i = 0; i < state.GetLength(0); i++) // sor
            {
                for (int j = 0; j < state.GetLength(1); j++) // oszlop
                {
                    if (state[i,j] == "p" && hm.CanPlayerMove(state[i,j+1])) 
                    {
                        if (hm.isThereObstacleInWay(state[i,j+1]))
                        {
                            // Ha w marad akkor fal van ott, vagy kimentünk volna a pályáról.
                            string wallCheck = "w";
                            try { wallCheck = state[i, j + 2];  } catch (Exception) { }
                            if (hm.CanObstacleBePushed(wallCheck))
                            {
                                state[i, j] = state[i, j + 2];
                                state[i, j + 1] = "p";
                                state[i, j + 2] = "o";
                                return;
                            }
                        }
                        else
                        {
                            state[i, j] = state[i, j + 1];
                            state[i, j + 1] = "p";
                            return;
                        }
                        
                    }
                }
            }
            
        }

        // a CanPlayerMove az előfeltételünk, IsThereObstacleInWay, CanObstacleBePushed további speciális feltételek.
        public static void MoveUp(ref string[,] state)
        {

            for (int i = 0; i < state.GetLength(0); i++) // sor
            {
                for (int j = 0; j < state.GetLength(1); j++) // oszlop
                {
                    if (state[i, j] == "p" && hm.CanPlayerMove(state[i - 1, j]))
                    {
                        if (hm.isThereObstacleInWay(state[i - 1, j]))
                        {
                            // Ha w marad akkor fal van ott, vagy kimentünk volna a pályáról.
                            string wallCheck = "w";
                            try { wallCheck = state[i - 2, j]; } catch (Exception) { }
                            if (hm.CanObstacleBePushed(wallCheck))
                            {
                                state[i, j] = state[i - 2, j];
                                state[i - 1, j] = "p";
                                state[i - 2, j] = "o";
                            }
                            break;
                        }
                        else
                        {
                            state[i, j] = state[i - 1, j];
                            state[i - 1, j] = "p";
                            break;
                        }

                    }
                }
            }

        }

        public static void MoveDown(ref string[,] state)
        {

            for (int i = 0; i < state.GetLength(0); i++) // sor
            {
                for (int j = 0; j < state.GetLength(1); j++) // oszlop
                {
                    if (state[i, j] == "p" && hm.CanPlayerMove(state[i + 1, j]))
                    {
                        if (hm.isThereObstacleInWay(state[i + 1, j]))
                        {
                            // Ha w marad akkor fal van ott, vagy kimentünk volna a pályáról.
                            string wallCheck = "w";
                            try { wallCheck = state[i + 2, j]; } catch (Exception) { }
                            if (hm.CanObstacleBePushed(wallCheck))
                            {
                                state[i, j] = state[i + 2, j];
                                state[i + 1, j] = "p";
                                state[i + 2, j] = "o";
                            }
                            return;
                        }
                        else
                        {
                            state[i, j] = state[i + 1, j];
                            state[i + 1, j] = "p";
                            return;
                        }
                    }
                }
            }

        }


        // Letároljuk az összes következő lépés állapotát.
        public static List<string[,]> GetNextMoves(string[,] state)
        {
            List<string[,]> nextMoves = new List<string[,]>();

            string[,] rightMove = new string[state.GetLength(0), state.GetLength(1)];
            for (int i = 0; i < state.GetLength(0); i++)
            {
                for (int j = 0; j < state.GetLength(1); j++)
                {
                    rightMove[i, j] = state[i, j];
                }
            }
            Movement.MoveRight(ref rightMove);
            nextMoves.Add(rightMove);


            string[,] leftMove = new string[state.GetLength(0), state.GetLength(1)];
            for (int i = 0; i < state.GetLength(0); i++)
            {
                for (int j = 0; j < state.GetLength(1); j++)
                {
                    leftMove[i, j] = state[i, j];
                }
            }
            Movement.MoveLeft(ref leftMove);
            nextMoves.Add(leftMove);

            string[,] upMove = new string[state.GetLength(0), state.GetLength(1)];
            for (int i = 0; i < state.GetLength(0); i++)
            {
                for (int j = 0; j < state.GetLength(1); j++)
                {
                    upMove[i, j] = state[i, j];
                }
            }
            Movement.MoveUp(ref upMove);
            nextMoves.Add(upMove);

            string[,] downMove = new string[state.GetLength(0), state.GetLength(1)];
            for (int i = 0; i < state.GetLength(0); i++)
            {
                for (int j = 0; j < state.GetLength(1); j++)
                {
                    downMove[i, j] = state[i, j];
                }
            }
            Movement.MoveDown(ref downMove);
            nextMoves.Add(downMove);

            return nextMoves;
        }


    }
}
