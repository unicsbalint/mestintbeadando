using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MestIntSocoboInWinform.Structures
{
    class Obstacle
    {
        public Control GetObstacle()
        {
            PictureBox obstacle = new System.Windows.Forms.PictureBox();
            obstacle.Size = new System.Drawing.Size(100, 100);
            if (Form1.renderTextures)
            {
                obstacle.ImageLocation = @"textures\obstacle.png";
            }
            obstacle.Dock = DockStyle.Fill;
            obstacle.Margin = Padding.Empty;
            obstacle.BackColor = Color.Gold;
            return obstacle;
        }
    }
}
