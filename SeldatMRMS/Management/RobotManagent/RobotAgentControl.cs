using SeldatMRMS.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeldatMRMS.Management.RobotManagent
{
    public class RobotAgentControl:RosSocket
    {
        public struct ParamsRosSocket
        {
            public int publication_RobotInfo;
            public int publication_RobotParams;
            public int publication_ServerRobotCtrl;
            public int publication_CtrlRobotHardware;
            public int publication_DriveRobot;
            public int publication_BatteryRegister;
            public int publication_EmergencyRobot;
            public int publication_CtrlRobotDriving;
            public int publication_gotoPalletArea;
            public int publication_serverRobotGotToLineDockingArea;
            public int publication_serverRobotGotToPalletDockingArea;
            public int publication_serverRobotGotToLinePutAwayArea;
            public int publication_serverRobotGotToPalletPutAwayArea;
            public int publication_responsedStandPosInsideReadyArea;
            public int publication_serverRobotGotToCheckInDockingArea;
            public int publication_serverRobotGotToCheckInPutAwayArea;
            public int publication_serverRobotGotToFrontReadyArea;
            public int publication_serverResetErrorDetectLine;
            public int publication_checkAliveTimeOut;
            public int publication_serverRobotGotToChargeArea;
            public int publication_serverRequestRobotDetectLine;
            public int publication_linedetectionctrl;
        }
        ParamsRosSocket paramsRosSocket;
        public RobotAgentControl()
        {

        }
        public void createRosTerms()
        {
            /*int subscription_robotInfo = this.Subscribe("/amcl_pose", "geometry_msgs/PoseWithCovarianceStamped", robotInfoHandler);
            paramsRosSocket.publication_RobotInfo = this.Advertise("/serverRobotInfoCallBack", "std_msgs/String");
            int subscription_robotParams = this.Subscribe("/robotParams", "std_msgs/String", robotParamsHandler);
            paramsRosSocket.publication_RobotParams = this.Advertise("/serverRobotParamsCallBack", "std_msgs/String");
            int subscription_ctrlRobotHardwareStatus = this.Subscribe("/ctrlRobotHardwareStatusResponse", "std_msgs/String", ctrlRobotHardwareStatusHandler);
            paramsRosSocket.publication_CtrlRobotHardware = this.Advertise("/serverCtrlRobotHardwareCallBack", "std_msgs/String");
            int subscription_DriveRobotStatus = this.Subscribe("/driveRobotStatusResponse", "std_msgs/String", driveRobotStatusHandler);
            paramsRosSocket.publication_DriveRobot = this.Advertise("/serverDriveRobotCallBack", "std_msgs/String");
            int subscription_batteryRegister = this.Subscribe("/batteryRegisterResponse", "std_msgs/String", batteryRegisterHandler);
            paramsRosSocket.publication_BatteryRegister = this.Advertise("/serverBatteryRegisterCallBack", "std_msgs/String");
            int subscription_emergencyRobot = this.Subscribe("/emergencyRobotResponse", "std_msgs/String", emergencyRobotHandler);
            paramsRosSocket.publication_EmergencyRobot = this.Advertise("/serverEmergencyRobotCallBack", "std_msgs/String");

            int subscription_batteryvol = this.Subscribe("/battery_vol", "std_msgs/Float32", batteryVolHandler);
            //
            paramsRosSocket.publication_gotoPalletArea = this.Advertise("servergotoPalletAreaCallBack", "std_msgs/String");
            // Directly publish to control robot
            paramsRosSocket.publication_CtrlRobotDriving = this.Advertise("/ctrlRobotDriving", "std_msgs/Int32");

            // publish docking area location
            paramsRosSocket.publication_serverRobotGotToLineDockingArea = this.Advertise("/serverRobotGotToLineDockingArea", "std_msgs/String");
            paramsRosSocket.publication_serverRobotGotToPalletDockingArea = this.Advertise("/serverRobotGotToPalletDockingArea", "std_msgs/String");

            paramsRosSocket.publication_serverRobotGotToCheckInDockingArea = this.Advertise("/serverRobotGotToCheckInDockingArea", "std_msgs/String");
            // publish putaway area location
            paramsRosSocket.publication_serverRobotGotToLinePutAwayArea = this.Advertise("/serverRobotGotToLinePutAwayArea", "std_msgs/String");
            paramsRosSocket.publication_serverRobotGotToPalletPutAwayArea = this.Advertise("/serverRobotGotToPalletPutAwayArea", "std_msgs/String");

            paramsRosSocket.publication_serverRobotGotToCheckInPutAwayArea = this.Advertise("/serverRobotGotToCheckInPutAwayArea", "std_msgs/String");

            paramsRosSocket.publication_serverRobotGotToFrontReadyArea = this.Advertise("/serverRobotGotToFrontReadyArea", "std_msgs/String");
            paramsRosSocket.publication_serverRobotGotToChargeArea = this.Advertise("/serverRobotGotToChargeArea", "std_msgs/String");
            int subscription_requestPutAwayArea = this.Subscribe("/requestPutAwayArea", "std_msgs/String", requestPutAwayAreaHandler);
            // subcribe Zone Checker
            int subscription_zoneChecker = this.Subscribe("/zoneChecker", "std_msgs/String", zoneCheckerHandler);
            int subscription_finishedStates = this.Subscribe("/finishedStates", "std_msgs/Int32", finishedStatesHandler);
            // int subscription_test = this.Subscribe("/pallet_status_4", "std_msgs/String", testHandler);
            int subscription_robotStatus = this.Subscribe("/robotStatus", "std_msgs/Int32", robotStatusHandler);
            int subscription_readyAreaPos = this.Subscribe("/requestReadyAreaPos", "std_msgs/Int32", readyAreaPosHandler);
            paramsRosSocket.publication_responsedStandPosInsideReadyArea = this.Advertise("/responsedStandPosInsideReadyArea", "std_msgs/String");
            paramsRosSocket.publication_serverResetErrorDetectLine = this.Advertise("/serverResetErrorDetectLine", "std_msgs/Int32");
            paramsRosSocket.publication_checkAliveTimeOut = this.Advertise("/checkAliveTimeOut", "std_msgs/String");
            paramsRosSocket.publication_serverRequestRobotDetectLine = this.Advertise("/serverRequestRobotDetectLine", "std_msgs/String");
            paramsRosSocket.publication_linedetectionctrl = this.Advertise("/linedetectionctrl", "std_msgs/Int32");
            */   
        }
    }
}
