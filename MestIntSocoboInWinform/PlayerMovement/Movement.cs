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

        public static void MoveLeft(ref string[,] state)
        {
            for (int i = 0; i < state.GetLength(0); i++) // sor
            {
                for (int j = 0; j < state.GetLength(1); j++) // oszlop
                {
                    if (state[i, j] == "p" && hm.CanPlayerMove(state[i, j + -1]))
                    {
                        if (hm.isThereObstacleInWay(state[i, j - 1]))
                        {
                            Console.WriteLine("obstacle van az útban");
                            // Ha w marad akkor fal van ott, vagy kimentünk volna a pályáról.
                            string wallCheck = "w";
                            try { wallCheck = state[i, j - 2]; } catch (Exception) { }
                            if (hm.CanObstacleBePushed(wallCheck))
                            {
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
                            Console.WriteLine("obstacle van az útban");
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
                            Console.WriteLine("Nincsen izém");
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


    }
}
