using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MestIntSocoboInWinform.Structures
{
    class Goal
    {
        public Control GetGoal()
        {
            PictureBox goal = new System.Windows.Forms.PictureBox();
            goal.Size = new System.Drawing.Size(100, 100);
            goal.ImageLocation = @"textures\floor.jpg";
            goal.Dock = DockStyle.Fill;
            goal.Margin = Padding.Empty;
            return goal;
        }
    }
}
