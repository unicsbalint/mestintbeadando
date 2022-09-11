using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MestIntSocoboInWinform.GameState;
using MestIntSocoboInWinform.Structures;
using MestIntSocoboInWinform.PlayerMovement;

namespace MestIntSocoboInWinform
{
    public partial class Form1 : Form
    {
        public static bool renderTextures = true;
        private State state = State.GetInstance();
        public Form1()
        {
            InitializeComponent();
            // UI beállítása
            updateGamePanel(state.GetState());
            // A következő lépési lehetőségek betöltése az AI számára
            state.updateNextMoves();
            // Stacknek megadni a kezdőállapotot
            state.stack.Push(state.currentState);

            SetWinningCoordinatesText();
        }

        // Ez a Double Buffering miatt van, de ez sem működik jól, ugyan úgy villog a kép.
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED       
                return handleParam;
            }
        }

        private void updateGamePanel(string[,] state)
        {
            for (int i = 0; i < state.GetLength(0); i++)
            {
                for (int j = 0; j < state.GetLength(1); j++)
                {
                    RenderUIElements(i, j, state[i, j]);
                }
            }
        }

        private void RenderUIElements(int row, int col, string structure)
        {

            switch (structure)
            {
                case "w":
                        Wall wall = new Wall();
                        Control wallUI = wall.GetWall();
                        this.Controls.Add(wallUI);
                        gamePanel.Controls.Add(wallUI);
                        gamePanel.SetRow(wallUI, row);
                        gamePanel.SetColumn(wallUI, col);
                        break;
                case "f":
                        Floor floor = new Floor();
                        Control floorUI = floor.GetFloor();
                        this.Controls.Add(floorUI);
                        gamePanel.Controls.Add(floorUI);
                        gamePanel.SetRow(floorUI, row);
                        gamePanel.SetColumn(floorUI, col);
                        break;
                case "o":
                        Obstacle obstacle = new Obstacle();
                        Control obstacleUI = obstacle.GetObstacle();
                        this.Controls.Add(obstacleUI);
                        gamePanel.Controls.Add(obstacleUI);
                        gamePanel.SetRow(obstacleUI, row);
                        gamePanel.SetColumn(obstacleUI, col);
                        break;
                case "p":
                        Player player = new Player();
                        Control playerUI = player.GetPlayer();
                        this.Controls.Add(playerUI);
                        gamePanel.Controls.Add(playerUI);
                        gamePanel.SetRow(playerUI, row);
                        gamePanel.SetColumn(playerUI, col);
                        break;
                case "g":
                        Goal goal = new Goal();
                        Control goalUI = goal.GetGoal();
                        this.Controls.Add(goalUI);
                        gamePanel.Controls.Add(goalUI);
                        gamePanel.SetRow(goalUI, row);
                        gamePanel.SetColumn(goalUI, col);
                        break;
                default:
                    break;
            }
            
        }

        // A form1-en történő gomblenyomásokat érzékeli, ez a user inputunk.
        private void Form1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                gamePanel.Controls.Clear();
                state.UpdateState(MovementTypes.RIGHT);
                updateGamePanel(state.GetState());
                GameOver();
            }
            else if (e.KeyCode == Keys.Left)
            {
                gamePanel.Controls.Clear();
                state.UpdateState(MovementTypes.LEFT);
                updateGamePanel(state.GetState());
                GameOver();
            }
            else if (e.KeyCode == Keys.Up)
            {
                gamePanel.Controls.Clear();
                state.UpdateState(MovementTypes.UP);
                updateGamePanel(state.GetState());
                GameOver();
            }
            else if(e.KeyCode == Keys.Down)
            {
                gamePanel.Controls.Clear();
                state.UpdateState(MovementTypes.DOWN);
                updateGamePanel(state.GetState());
                GameOver();
            }
            else if(e.KeyCode == Keys.Space)
            {
                Backtrack();
            }
            else if(e.KeyCode == Keys.Q)
            {
                gamePanel.Controls.Clear();
                state.LoadDefaultState();
                updateGamePanel(state.currentState);
            }
            else if(e.KeyCode == Keys.T)
            {
                Form1.renderTextures = !Form1.renderTextures;
                gamePanel.Controls.Clear();
                updateGamePanel(state.GetState());
            }

        }

        public void GameOver()
        {
            if (state.IsItTargetState())
            {
                MessageBox.Show("Gratulálunk! Nyertél!");
                gamePanel.Controls.Clear();
                updateGamePanel(state.GetState());
            }
        }

        string[,] noMoreOperatorCanBeUsed = new string[0, 0];
        // Mélység határos. no-go backtrack
        public void Backtrack()
        {
            int depthLimit = 250;
            List<string[,]> noGo = new List<string[,]>();

            // Ha van olyan állapota az aktuális játéktérnek ami már visszalépést követelne, az állapot nem megoldható.
            if(state.IsStepBackNeeded())
            {
                MessageBox.Show("Nem megoldható feladat.");
                return;
            }
            while (state.stack.Count > 0 && !state.IsItTargetState())
            {
                // "mentem" az állapotot.
                state.stack.Push(state.currentState);
                
                // Ellépek az egyik irányba.
                state.currentState = state.GetNextMove(noGo);

                // [0,0]-t ad vissza, akkor az adott csúcsból nem tudunk már sehova lépni, vissza kell menni.
                if (state.currentState == noMoreOperatorCanBeUsed) state.stack.Pop();

                // Amennyiben elértem a cél állapotot GameOver
                if (state.IsItTargetState())
                {
                    GameOver();
                    break;
                }

                // Rosszul léptem?
                if (state.IsStepBackNeeded())           
                {
                    // Ha vissza kellett lépnem akkor az egy no-go állapot
                    noGo.Add(state.currentState);
                    // Ha rosszul léptem, visszalépek az előző állapotba.
                    state.currentState = state.stack.Peek();
                    //És a stacket is vissza állítom
                    state.stack.Pop();
                }

                // Ha a stack túl mélyre megy, a feladatot az AI valószínű nem tudja megoldani.
                if (state.stack.Count >= depthLimit)
                {
                    state.stack.Pop();
                }
            }
        }

      

        public void SetWinningCoordinatesText()
        {
            string coordinates = "Mozgasd a diplomákat a következő koordinátákra:";
            for (int i = 0; i < state.currentState.GetLength(0); i++)
            {
                for (int j = 0; j < state.currentState.GetLength(0); j++)
                {
                    if (state.currentState[i,j] == "g")
                    {
                        coordinates += string.Format("({0},{1})", i, j);
                    }
                }
            }
            goalCoordinates.Text = coordinates;
        }

    }
}
