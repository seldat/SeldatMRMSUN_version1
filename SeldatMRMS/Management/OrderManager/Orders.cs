
using SeldatMRMS.Management.RobotManagent;
using SeldatMRMS.Management.TrafficManager;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeldatMRMS.Management.OrderManager
{
    public class Orders
    {
        public delegate void RequestToReadyArea(string msg);
        public RequestToReadyArea requestToReadyArea;
        public void addReadyAreaDepends(ReadyArea readyArea)
        {
            //readyArea.onRequestANewOrder += RequestOrder;
            //readyArea.onFinishAnOrder += OrderFinished;
        }


        public string[] RequestDockingOrderLine(int area, string robotID)
        {
            RegistrationAgent.mainWindowPointer.LogConsole("In RequestDockingOrderLine", "logOrder");
            string[] data = null;
            try
            {
                string areaID = area.ToString();
                string listLineDockingKey = RegistrationAgent.areaList[areaID].GetDockingLine();
                if (listLineDockingKey != "none")
                {
                    string stationNameID = listLineDockingKey.Split('-')[0];
                    int lposdk = Int32.Parse(listLineDockingKey.Split('-')[1]);
                    if (RegistrationAgent.areaList[areaID].DOCKING_LINE_LIST.ContainsKey(listLineDockingKey))
                    {
                        data = new string[3];
                        data[0] = RegistrationAgent.areaList["0"].dockingStations["Docking-" + stationNameID].lineInfo.jsonLine(area, "DOCKING", stationNameID, lposdk);
                        //data[0] = DataTranformation.jsonDockingLine(area, stationNameID, lposdk);
                        data[1] = stationNameID.ToString();
                        data[2] = lposdk.ToString();
                        RegistrationAgent.mainWindowPointer.LogConsole("GET DOCKING:" + stationNameID + "-" + lposdk, "logOrder");
                        return data;
                    }
                    RegistrationAgent.mainWindowPointer.LogConsole("No-listlinedocking-" + listLineDockingKey, "logOrder");
                }
                RegistrationAgent.mainWindowPointer.LogConsole("In RequestDockingOrderLine-no list line docking key-" + listLineDockingKey, "logOrder");
            }
            catch
            {
                RegistrationAgent.mainWindowPointer.LogConsole("Error when RequestDockingOrderLine: Area:" + area + "-Robot:" + robotID, "logOrder");
            }
            RegistrationAgent.mainWindowPointer.LogConsole("Last RequestDockingOrderLine", "logOrder");
            return data;
        }

        public string[] RequestDockingOrderPallet(int area, string robotID, string stationNameID, string line)
        {
            RegistrationAgent.mainWindowPointer.LogConsole("In RequestDockingOrderPallet", "logOrder");
            string[] data = null;
            try
            {
                string areaID = area.ToString();
                List<int> palletnumsdk = new List<int>();
                if (RegistrationAgent.areaList[areaID].DOCKING_LINE_LIST.ContainsKey(stationNameID + "-" + line) &&
                   RegistrationAgent.areaList[areaID].DOCKING_LINE_LIST[stationNameID + "-" + line].GetPallet(palletnumsdk))
                {
                    data = new string[4];
                    data[0] = RegistrationAgent.areaList["0"].dockingStations["Docking-" + stationNameID].lineInfo.jsonPallet(area, "DOCKING", stationNameID, int.Parse(line), palletnumsdk.First());
                    //data[0] = DataTranformation.jsonDockingPallet(area, Int32.Parse(stationNameID), Int32.Parse(line), palletnumsdk.First());
                    data[1] = stationNameID.ToString();
                    data[2] = line.ToString();
                    data[3] = palletnumsdk.First().ToString();
                    RegistrationAgent.mainWindowPointer.LogConsole("DOCKING :" + data[1] + "-" + data[2] + "-" + data[3], "logOrder");
                    return data;
                }
                RegistrationAgent.mainWindowPointer.LogConsole("Error when RequestDockingOrderPallet1: Area:" + area + "-Robot:" + robotID + "-agentID:" + stationNameID + "-line:" + line, "logOrder");
            }
            catch
            {
                RegistrationAgent.mainWindowPointer.LogConsole("Error when RequestDockingOrderPallet2: Area:" + area + "-Robot:" + robotID + "-agentID:" + stationNameID + "-line:" + line, "logOrder");
            }
            return data;
        }

        public string[] RequestPutAwayOrderLine(int area, string robotID)
        {
            RegistrationAgent.mainWindowPointer.LogConsole("In RequestPutAwayOrderLine", "logOrder");
            string[] data = null;
            try
            {
                string areaID = area.ToString();
                string listLinePutAwayKey = RegistrationAgent.areaList[areaID].GetPutAwayLine();
                if (listLinePutAwayKey != "none")
                {
                    string stationNameID = listLinePutAwayKey.Split('-')[0];
                    int lpospw = Int32.Parse(listLinePutAwayKey.Split('-')[1]);
                    if (RegistrationAgent.areaList[areaID].PUTAWAY_LINE_LIST.ContainsKey(listLinePutAwayKey))
                    {
                        data = new string[3];
                        data[0] = RegistrationAgent.areaList["0"].putAwayStations["PutAway-" + stationNameID].lineInfo.jsonLine(area, "PUTAWAY", stationNameID, lpospw);
                        //data[0] = DataTranformation.jsonPutAwayLine(area, stationNameID, lpospw);
                        data[1] = stationNameID.ToString();
                        data[2] = lpospw.ToString();
                        RegistrationAgent.mainWindowPointer.LogConsole("PUT PUTAWAY:" + stationNameID + "-" + lpospw, "logOrder");
                        return data;
                    }
                    RegistrationAgent.mainWindowPointer.LogConsole("No-listlineputaway-" + listLinePutAwayKey, "logOrder");
                }
                RegistrationAgent.mainWindowPointer.LogConsole("In RequestPutAwayOrderLine-no list line docking key-" + listLinePutAwayKey, "logOrder");
            }
            catch
            {
                RegistrationAgent.mainWindowPointer.LogConsole("Error when RequestPutAwayOrderLine: Area:" + area + "-Robot:" + robotID, "logOrder");
            }
            RegistrationAgent.mainWindowPointer.LogConsole("Last RequestPutAwayOrderLine", "logOrder");
            return data;
        }


        public string[] RequestPutAwayOrderPallet(int area, string robotID, string stationNameID, string line)
        {
            RegistrationAgent.mainWindowPointer.LogConsole("In RequestPutAwayOrderPallet", "logOrder");
            string[] data = null;
            try
            {
                string areaID = area.ToString();
                List<int> palletnumspw = new List<int>();
                if (RegistrationAgent.areaList[areaID].PUTAWAY_LINE_LIST.ContainsKey(stationNameID + "-" + line) &&
                   RegistrationAgent.areaList[areaID].PUTAWAY_LINE_LIST[stationNameID + "-" + line].GetPallet(palletnumspw))
                {
                    data = new string[4];
                    data[0] = RegistrationAgent.areaList["0"].putAwayStations["PutAway-" + stationNameID].lineInfo.jsonPallet(area, "PUTAWAY", stationNameID, int.Parse(line), palletnumspw.First());
                    //data[0] = DataTranformation.jsonPutAwayPallet(area, Convert.ToInt32(stationNameID), Convert.ToInt32(line), palletnumspw.First());
                    data[1] = stationNameID.ToString();
                    data[2] = line.ToString();
                    data[3] = palletnumspw.First().ToString();
                    RegistrationAgent.mainWindowPointer.LogConsole("PUT PUTAWAY:" + data[1] + "-" + data[2] + "-" + data[3], "logOrder");
                    return data;
                }
                RegistrationAgent.mainWindowPointer.LogConsole("Error when RequestPutAwayOrderPallet1: Area:" + area + "-Robot:" + robotID + "-agentID:" + stationNameID + "-line:" + line, "logOrder");
            }
            catch
            {
                RegistrationAgent.mainWindowPointer.LogConsole("Error when RequestPutAwayOrderPallet2: Area:" + area + "-Robot:" + robotID + "-agentID:" + stationNameID + "-line:" + line, "logOrder");
            }
            return data;
        }

        public bool ReleaseDockingOrder(string areaID, string stationNameID, string lineOrdered, string palletIndex)
        {
            Console.WriteLine("Call Docking Remove");
            if (RegistrationAgent.areaList.ContainsKey(areaID))
            {
                if (RegistrationAgent.areaList[areaID].DOCKING_LINE_LIST.ContainsKey(stationNameID + "-" + lineOrdered))
                {
                    RegistrationAgent.areaList[areaID].DOCKING_LINE_LIST.Remove(stationNameID + "-" + lineOrdered);
                    string messLog = "Removed Docking Order: " + stationNameID + "-" + lineOrdered + "-" + palletIndex;
                    RegistrationAgent.mainWindowPointer.LogConsole(messLog, "logOrder");
                    LogWriter.LogWrite("Area\\Area" + 0, messLog, "Docking-" + stationNameID + "-" + lineOrdered);
                    return true;
                }
                RegistrationAgent.mainWindowPointer.LogConsole("There is no Docking agent: " + stationNameID, "logOrder");
                return false;
            }
            RegistrationAgent.mainWindowPointer.LogConsole("There is no area: " + areaID, "logOrder");
            return false;
        }

        public bool ReleasePutAwayOrder(string areaID, string stationNameID, string lineOrdered, string palletIndex)
        {
            Console.WriteLine("Call PutAway Remove");
            if (RegistrationAgent.areaList.ContainsKey(areaID))
            {
                if (RegistrationAgent.areaList[areaID].PUTAWAY_LINE_LIST.ContainsKey(stationNameID + "-" + lineOrdered))
                {
                    RegistrationAgent.areaList[areaID].PUTAWAY_LINE_LIST.Remove(stationNameID + "-" + lineOrdered);
                    string messLog = "Removed Put-Away Order: " + stationNameID + "-" + lineOrdered + "-" + palletIndex;
                    RegistrationAgent.mainWindowPointer.LogConsole(messLog, "logOrder");
                    LogWriter.LogWrite("Area\\Area" + 0, messLog, "PutAway-" + stationNameID + "-" + lineOrdered);
                    return true;
                }
                RegistrationAgent.mainWindowPointer.LogConsole("There is no Put-Away agent: " + stationNameID, "logOrder");
                return false;
            }
            RegistrationAgent.mainWindowPointer.LogConsole("There is no area: " + areaID, "logOrder");
            return false;
        }

    }
}
