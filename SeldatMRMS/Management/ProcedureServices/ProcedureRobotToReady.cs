using DoorControllerService;
using SeldatMRMS.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeldatMRMS
{
    class ProcedureRobotToReady : ProcedureControlServices
    {
        public ProcedureRobotToReady(RobotAgent robot) : base(robot, null) { }
    }
}
