using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MestIntSocoboInWinform.PlayerMovement
{
    public abstract class AbsMovement
    {
        public abstract bool CanPlayerMove(string moveTo);
        public abstract bool isThereObstacleInWay(string moveTo);
        public abstract bool CanObstacleBePushed(string wallCheck);
    }
}
