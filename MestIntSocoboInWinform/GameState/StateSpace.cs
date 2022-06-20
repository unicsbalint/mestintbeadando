﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MestIntSocoboInWinform.GameState
{
    interface StateSpace
    { 
        bool IsItTargetState();
        void UpdateState(PlayerMovement.MovementTypes mvType);
    }
}
