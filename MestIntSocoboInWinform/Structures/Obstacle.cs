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
            PictureBox wall = new System.Windows.Forms.PictureBox();
            wall.Size = new System.Drawing.Size(100, 100);
            wall.ImageLocation = @"textures\obstacle.jpg";
            wall.Dock = DockStyle.Fill;
            wall.Margin = Padding.Empty;
            return wall;
        }
    }
}
