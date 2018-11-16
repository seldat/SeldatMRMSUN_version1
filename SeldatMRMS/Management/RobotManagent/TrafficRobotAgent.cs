using SeldatMRMS.Management.RobotManagent;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SeldatMRMS.Management
{
    class TrafficRobotAgent:RobotAgentService
    {
        List<RobotUnity> RobotUnitylist;
        public TrafficRobotAgent() : base() { }
        public void RegisteRobotInAvailable(List<RobotUnity> RobotUnitylist)
        {
            this.RobotUnitylist = RobotUnitylist;
        }
        public void CheckIntersection()
        {
            
        }
        public bool CheckSafeDistance() // KIểm tra khoản cách an toàn/ nếu đang trong vùng close với robot khác thì giảm tốc độ, chuyển sang chế độ dò risk area
        {
            return false;
        }
    }
}
