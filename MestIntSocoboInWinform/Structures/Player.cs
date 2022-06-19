using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MestIntSocoboInWinform.Structures
{
    class Player
    {
        public Control GetPlayer()
        {
            PictureBox player = new System.Windows.Forms.PictureBox();
            player.Size = new System.Drawing.Size(100, 100);
            player.ImageLocation = @"textures\player.png";
            player.Dock = DockStyle.Fill;
            player.Margin = Padding.Empty;
            return player;
        }
    }
}
