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
        private State state = State.GetInstance();
        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();
            updateGamePanel(state.GetState());
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
                case "g":
                        Goal goal = new Goal();
                        Control goalUI = goal.GetGoal();
                        this.Controls.Add(goalUI);
                        gamePanel.Controls.Add(goalUI);
                        gamePanel.SetRow(goalUI, row);
                        gamePanel.SetColumn(goalUI, col);
                        break;
                case "p":
                        Player player = new Player();
                        Control playerUI = player.GetPlayer();
                        this.Controls.Add(playerUI);
                        gamePanel.Controls.Add(playerUI);
                        gamePanel.SetRow(playerUI, row);
                        gamePanel.SetColumn(playerUI, col);
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
                checkIfGameIsOver();
            }
            else if (e.KeyCode == Keys.Left)
            {
                gamePanel.Controls.Clear();
                state.UpdateState(MovementTypes.LEFT);
                updateGamePanel(state.GetState());
                checkIfGameIsOver();
            }
            else if (e.KeyCode == Keys.Up)
            {
                gamePanel.Controls.Clear();
                state.UpdateState(MovementTypes.UP);
                updateGamePanel(state.GetState());
                checkIfGameIsOver();
            }
            else if(e.KeyCode == Keys.Down)
            {
                gamePanel.Controls.Clear();
                state.UpdateState(MovementTypes.DOWN);
                updateGamePanel(state.GetState());
                checkIfGameIsOver();
            }

        }

        public void checkIfGameIsOver()
        {
            if (state.IsItTargetState())
            {
                MessageBox.Show("Gratulálunk! Nyertél!");
                gamePanel.Controls.Clear();
                state.LoadDefaultState();
                updateGamePanel(state.GetState());
            }
        }
    }
}
