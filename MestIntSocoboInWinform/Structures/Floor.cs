using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MestIntSocoboInWinform.Structures
{
    class Floor
    {
        public Control GetFloor()
        {
            
            PictureBox floor = new System.Windows.Forms.PictureBox();
            floor.Size = new System.Drawing.Size(100, 100);
            if (Form1.renderTextures)
            {
                floor.ImageLocation = @"textures\floor.png";
            }
            floor.BackColor = Color.White;
            floor.Dock = DockStyle.Fill;
            floor.Margin = Padding.Empty;
            return floor;
        }
    }
}
