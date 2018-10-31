using SeldatMRMS.Model;
using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Windows.Media;

namespace SeldatMRMS.Management
{
    public class LoadModel
    {
        Interface pif;
        private string contents;
        public LoadModel(Interface pif)
        {
            this.pif = pif;
        }
        public void LoadFileModel()
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader reader = new StreamReader(theDialog.FileName);
                while (!reader.EndOfStream)
                {
                    contents = reader.ReadToEnd();
                }
                reader.Close();
            }
            theDialog.Dispose();

        }
        public void ParseInfo()
        {
            DialogResult pwarming = MessageBox.Show("Do you want to load new model?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (pwarming == DialogResult.Yes)
            {
                //code for Yes
            }
            else if (pwarming == DialogResult.No)
            {
                return;
            }

            pif.ClearAll();
            JObject results = JObject.Parse(contents);
            foreach (var result in results["haftpoints"])
            {
                string Name = (string )result["Name"];
                string label = (string )result["Label"];
                double Width = (double)result["Width"];
                double Height = (double)result["Width"];
                int posx = (int)result["posX"];
                int posy = (int)result["posY"];
                loadpoint(Name, label, new Point(posx, posy), Width, Height);

            }
            foreach (var result in results["stations"])
            {
                string Name = (string )result["Name"];
                string label = (string )result["Label"];
                string _type = (string )result["Type"];
                int posx = (int)result["posX"];
                int posy = (int)result["posY"];
                string stationID = (string )result["stationID"];
                string stationIP = (string )result["stationIP"];
                string stationPORT = (string )result["stationPort"];
                string stationAREA = (string )result["stationArea"];
                StationModel pstation = new StationModel(pif.content, _type);
                pstation.SetNameID(Name);
                pstation.SetPosition(posx, posy);
                pstation.SetKeyAndLabel(label);
                pstation.SetLabel(label);
                pstation.Load_StationProperties(result);
                pstation.lineInfo.loadparams(result["lineInfo"]);
                if (_type.Equals("DOCKING"))
                {
                    pstation.CreateCameraAgentD();
                }
                else if (_type.Equals("PUTAWAY"))
                {
                    pstation.CreateCameraAgentP();
                }
                else if (_type.Equals("MIXED"))
                {
                    pstation.CreateCameraAgentM();
                }
                RegistrationAgent.stationRegistrationList.Add(pstation);
                pif.updateTreeviewStations(pstation.properties.NameKey);
            }
            foreach (var result in results["checkins"])
            {
                string Name = (string)result["Name"];
                string label = (string)result["Label"];
                string _type = (string)result["Type"];
                int posx = (int)result["posX"];
                int posy = (int)result["posY"];
                CheckinModel pcheckin = new CheckinModel(pif.content, _type);
                pcheckin.setName(Name);
                pcheckin.setCheckin(posx, posy);
                pcheckin.setText(label);
                pcheckin.lineInfo.loadparams(result["lineInfo"]);
                //pstation.SetCamParam(stationID, stationIP, stationPORT, stationAREA);
                RegistrationAgent.checkinRegistrationList.Add(pcheckin);
                pif.updateTreeviewStations(pcheckin.props.NameKey);
            }
            foreach (var result in results["checkouts"])
            {
                string Name = (string)result["Name"];
                string label = (string)result["Label"];
                string _type = (string)result["Type"];
                int posx = (int)result["posX"];
                int posy = (int)result["posY"];
                CheckoutModel pcheckout = new CheckoutModel(pif.content, _type);
                pcheckout.setName(Name);
                pcheckout.setCheckout(posx, posy);
                pcheckout.setText(label);
                pcheckout.lineInfo.loadparams(result["lineInfo"]);
                //pstation.SetCamParam(stationID, stationIP, stationPORT, stationAREA);
                RegistrationAgent.checkoutRegistrationList.Add(pcheckout);
                pif.updateTreeviewStations(pcheckout.props.NameKey);
            }
            foreach (var result in results["robotconfig"])
            {
                string Name = (string )result["NameObj"];
                string IpAddress = (string )result["IpAddress"];
                string Hostname = (string )result["Hostname"];
                double CriticalEnergyAt = (double)result["CriticalEnergyAt"];
                double GoodEnergyAt = (double)result["GoodEnergyAt"];
                int Port = (int)result["Port"];
                double px = (double)result["InitialPosX"];
                double py = (double)result["InitialPosY"];
                double initialheadingangle = (double)result["InitialPosY"];
                loadrobotconfig(IpAddress, Hostname, Name, Port, CriticalEnergyAt, GoodEnergyAt, new System.Windows.Point(px, py), initialheadingangle);

            }
            foreach (var result in results["paths"])
            {
                string Name = (string )result["Name"];
                int typepath = (int)result["Type"];

                dynamic startPoint = result["startPoint"];
                string Name_sp = startPoint.Name;
                double posx_sp = startPoint.X;
                double posy_sp = startPoint.Y;

                double posx_mp = 0;
                double posy_mp = 0;
                if (typepath == PathModel.PATH_TYPE_BEZIERSEGMENT)
                {
                    dynamic middlePoint = result["middlepoint"];
                    string Name_mp = middlePoint.Name;
                    posx_mp = middlePoint.X;
                    posy_mp = middlePoint.Y;
                }
                dynamic endPoint = result["endPoint"];
                string Name_ep = endPoint.Name;
                double posx_ep = endPoint.X;
                double posy_ep = endPoint.Y;
                if (typepath == PathModel.PATH_TYPE_BEZIERSEGMENT)
                {
                    loadpath_bezier(Name, Name_sp, Name_ep, new System.Windows.Point(posx_sp, posy_sp), new System.Windows.Point(posx_mp, posy_mp), new System.Windows.Point(posx_ep, posy_ep));
                }
                if (typepath == PathModel.PATH_TYPE_DIRECT)
                {
                    loadpath_direct(Name, Name_sp, Name_ep, new System.Windows.Point(posx_sp, posy_sp), new System.Windows.Point(posx_ep, posy_ep));
                }

            }



        }
        public void loadpoint(string  Name, string label, Point pos, double w, double h)
        {
            HalfPoint temp = new HalfPoint(pif.content.map);
            temp.sethalfpoint(w, h, pos.X, pos.Y, Colors.Red);
            temp.setTextlabel(label);
            temp.setName(Name);
            pif.phalfpoint_manager.Add(temp);
            pif.updateTreeviewPoints(temp.properties.NameID);
        }

        public void loadrobotconfig(string  ipAddr, string host, string NameObj, int port, double CriticalEnergyAt, double GoodEnergyAt, System.Windows.Point initialPos, double initialheadingangle)
        {
            RobotAgent temp = new RobotAgent();
            temp.IpAddress = ipAddr;
            temp.Hostname = host;
            temp.NameID = NameObj;
            temp.Port = port;
            temp.InitialPostion = initialPos;
            temp.InitialHeadingAngle = initialheadingangle;
            RegistrationAgent.robotAgentRegisteredList.Add(temp.NameID, temp);
            pif.updateTreeviewRobotConfig(temp.NameID);
        }
        public void loadstation(string  Name, string label, string Type, Point pos, string stationID, string stationIP, string stationPORT, string stationAREA)
        {
            StationModel pstation = new StationModel(pif.content, Type);
            pstation.SetPosition(pos.X, pos.Y);
            pstation.SetKeyAndLabel(label);
            pstation.SetLabel(label);
            pstation.SetNameID(Name);
            //pstation.SetCamParam(stationID, stationIP, stationPORT, stationAREA);
            RegistrationAgent.stationRegistrationList.Add(pstation);
            pif.updateTreeviewStations(pstation.properties.stationNameID);
        }
        public void loadpath_bezier(string  Name, string Name_sp, string Name_ep, System.Windows.Point possp, System.Windows.Point posmp, System.Windows.Point posep)
        {
            PathModel ptempPath = new PathModel(pif.content.map, PathModel.PATH_TYPE_BEZIERSEGMENT);
            ptempPath.setName(Name);
            RegistrationAgent.pathRegistrationList.Add(ptempPath);
            HalfPoint halfpoint_sp = pif.findobjecthalfpoint(Name_sp);
            StationModel pstation_sp = pif.findobjectstation(Name_sp);
            if (halfpoint_sp != null)
            {
                pif.SetObjectPath_StartPoint(halfpoint_sp);
            }
            else if (pstation_sp != null)
            {
                pif.SetObjectPath_StartPoint(pstation_sp);
            }
            HalfPoint halfpoint_ep = pif.findobjecthalfpoint(Name_ep);
            StationModel pstation_ep = pif.findobjectstation(Name_ep);
            if (halfpoint_ep != null)
            {
                pif.SetObjectPath_EndPoint(halfpoint_ep);
            }
            else if (pstation_ep != null)
            {
                pif.SetObjectPath_EndPoint(pstation_ep);
            }


            ptempPath.drawbezierpath(possp, posmp, posep);
            ptempPath.ondraw();
            pif.refreshpath();
        }

        public void loadpath_direct(string  Name, string Name_sp, string Name_ep, System.Windows.Point possp, System.Windows.Point posep)
        {
            PathModel ptempPath = new PathModel(pif.content.map, PathModel.PATH_TYPE_DIRECT);
            ptempPath.setName(Name);
            RegistrationAgent.pathRegistrationList.Add(ptempPath);
            HalfPoint halfpoint_sp = pif.findobjecthalfpoint(Name_sp);
            StationModel pstation_sp = pif.findobjectstation(Name_sp);
            if (halfpoint_sp != null)
            {
                pif.SetObjectPath_StartPoint(halfpoint_sp);
            }
            else if (pstation_sp != null)
            {
                pif.SetObjectPath_StartPoint(pstation_sp);
            }
            HalfPoint halfpoint_ep = pif.findobjecthalfpoint(Name_ep);
            StationModel pstation_ep = pif.findobjectstation(Name_ep);
            if (halfpoint_ep != null)
            {
                pif.SetObjectPath_EndPoint(halfpoint_ep);
            }
            else if (pstation_ep != null)
            {
                pif.SetObjectPath_EndPoint(pstation_ep);
            }


            ptempPath.drawdirectpath(possp, posep);
            ptempPath.ondraw();
            pif.refreshpath();
        }
    }
}
