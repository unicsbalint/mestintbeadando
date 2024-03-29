﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MestIntSocoboInWinform.Structures
{
    class Wall
    {
        public Control GetWall()
        {
            PictureBox wall = new System.Windows.Forms.PictureBox();
            wall.Size = new System.Drawing.Size(100, 100);
            if (Form1.renderTextures)
            {
                wall.ImageLocation = @"textures\wall.png";
            }
            wall.BackColor = Color.Gray;
            wall.Dock = DockStyle.Fill;
            wall.Margin = Padding.Empty;
            return wall;
        }
    }
}
