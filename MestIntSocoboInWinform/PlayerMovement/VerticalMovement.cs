using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MestIntSocoboInWinform.PlayerMovement
{
    class VerticalMovement : AbsMovement
    {
        public override bool CanPlayerMove(string moveTo)
        {
            if (moveTo != "w") return true;
            return false;
        }
        public override bool isThereObstacleInWay(string moveTo)
        {
            return moveTo == "o";
        }
        public override bool CanObstacleBePushed(string wallCheck)
        {
            return wallCheck != "w" && wallCheck != "o";
        }
    }
}
