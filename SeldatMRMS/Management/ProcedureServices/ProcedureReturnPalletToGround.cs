﻿using DoorControllerService;
using SeldatMRMS.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeldatMRMS
{
    class ProcedureReturnPalletToGround : ProcedureControlServices
    {
        public ProcedureReturnPalletToGround(RobotAgent robot, DoorService doorservice) : base(robot, doorservice) { }
    }
}
