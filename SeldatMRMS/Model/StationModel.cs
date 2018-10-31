using Newtonsoft.Json.Linq;
using SeldatMRMS.Communication;
using SeldatMRMS.Management;
using SeldatMRMS.Management.OrderManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace SeldatMRMS.Model
{
    public partial class StationModel : Form
    {
        public class Camera
        {
            public Camera()
            {
                area = "-1";
                id = "-1";
                no_lines = -1;
                no_pallet_per_lines = -1;
                lines_detail = new SortedDictionary<string, LineDetail>(); // <lineID>,<Line>
                valid = false;
            }
            public string area;
            public string id;
            public string ip;
            public string port;
            public int no_lines;
            public int no_pallet_per_lines;
            public SortedDictionary<string, LineDetail> lines_detail; // <lineID>,<Line>
            public bool valid;
        }
        public class LineDetail
        {
            public LineDetail()
            {
                valid = true;
                palletWarning = "none";
                pallets_detail = new SortedDictionary<string, string>();
            }
            public bool valid;
            public string palletWarning;
            public SortedDictionary<string, string> pallets_detail;// <palletID>,<yes/no>
        }
        public struct Properties
        {
            public int port;
            public int id;
            public double X, Y;
            //public string type; //PUTAWAY, DOCKING, MIXED
            public string area; //Default = 0
            public string label; //PAW0
            public string NameKey; //PAW012301540306 --- PAW0
            public string stationNameID; //PAW012301540306
            public string typeName; //PUTAWAY, DOCKING, MIXED
            public double costValue;
            public string addressIP;
            public bool isConnected;
            public double lengthValue;
            public double Xcenter, Ycenter;
            public double X_model, Y_model;
            public List<bool> pallets_status;
            public Camera camera;
        }
        public enum FLAGPOINTDEFINED
        {
            FLAGPOINT_SET_STARTPOINT,
            FLAGPOINT_SET_ENDPOINT
        }
        public RosSocket rosSocket;
        public const int TYPE_STATION_CHARGE = 100;
        public const int TYPE_STATION_PICKUP = 101;
        public const int TYPE_STATION_PICKDOWN = 102;
        public Properties properties;
        public TextBlock plabel = new TextBlock();
        public CheckInPoint checkInPoint;
        public CheckOutPoint checkOutPoint;
        public LineInfo lineInfo;
        private List<PathModel> connectedPath_head;
        private List<FLAGPOINTDEFINED> flag_headtail;
        BitmapImage bmp;
        Image img;
        public StationModel() { }
        public StationModel(MainWindow content, string type)
        {
            InitializeComponent();
            img = new Image();
            properties.camera = new Camera();
            properties.area = "0";
            properties.typeName = type;
            properties.isConnected = false;
            connectedPath_head = new List<PathModel>();
            flag_headtail = new List<FLAGPOINTDEFINED>();
            string m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            switch (properties.typeName)
            {
                case "PUTAWAY":
                    {
                        bmp = new BitmapImage(new Uri(m_exePath + "//Resources//phat_putaway.png"));
                        break;
                    }
                case "DOCKING":
                    {
                        bmp = new BitmapImage(new Uri(m_exePath + "//Resources//phat_docking.png"));
                        break;
                    }
                case "MIXED":
                    {
                        bmp = new BitmapImage(new Uri(m_exePath + "//Resources//phat_mixed.png"));
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            img.Source = bmp;
            plabel.Width = 50;
            SetLabelBackgroundColor(Colors.Red);
            RegistrationAgent.mainWindowPointer.map.Children.Add(plabel);
            lineInfo = new LineInfo();
            rosSocket = new RosSocket();
            timerCheckConnection.Start();
        }
        private void Station_Load(object sender, EventArgs e)
        {
            dgvProperties_setting();
            Load_SetNameID(properties.stationNameID);
            Load_SetLabel(properties.label);
            Load_SetCameraID(properties.camera.id);
            Load_SetCameraIP(properties.camera.ip);
            Load_SetCameraPort(properties.camera.port);
            Load_SetArea(properties.camera.area);
            Text = properties.typeName;
        }
        void dgvProperties_setting()
        {
            dGV_properties.Rows.Clear();
            dGV_properties.Rows.Add("Name", properties.stationNameID);
            dGV_properties.Rows.Add("Label", properties.label);
            dGV_properties.Rows.Add("Camera");
            dGV_properties.Rows[2].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            dGV_properties.Rows.Add("ID", "0");
            dGV_properties.Rows.Add("IP", "192.168.1.");
            dGV_properties.Rows.Add("Port", "9090");
            //dGV_properties.Rows.Add("Area", "0");
        }
        public void Load_SetNameID(string nameID)
        {
            dGV_properties.Rows[0].Cells[1].Value = nameID;
        }
        public string Update_GetNameID()
        {
            return (dGV_properties.Rows[0].Cells[1].Value != null) ? (string)dGV_properties.Rows[0].Cells[1].Value : "";
        }
        public void Load_SetLabel(string lb)
        {
            dGV_properties.Rows[1].Cells[1].Value = lb;
        }
        public string Update_GetLabel()
        {
            return (dGV_properties.Rows[1].Cells[1].Value != null) ? (string)dGV_properties.Rows[1].Cells[1].Value : "";
        }
        public void Load_SetCameraID(string id)
        {
            dGV_properties.Rows[3].Cells[1].Value = id;
        }
        public string Update_GetCameraID()
        {
            return (dGV_properties.Rows[3].Cells[1].Value != null) ? (string)dGV_properties.Rows[3].Cells[1].Value : "";
        }
        public void Load_SetCameraIP(string ip)
        {
            dGV_properties.Rows[4].Cells[1].Value = ip;
        }
        public string Update_GetCameraIP()
        {
            return (dGV_properties.Rows[4].Cells[1].Value != null)? (string)dGV_properties.Rows[4].Cells[1].Value:"";
        }
        public void Load_SetCameraPort(string port)
        {
            dGV_properties.Rows[5].Cells[1].Value = port;
        }
        public string Update_GetCameraPort()
        {
            return (dGV_properties.Rows[5].Cells[1].Value != null) ? (string)dGV_properties.Rows[5].Cells[1].Value : "";
        }
        public void Load_SetArea(string area)
        {
            dGV_properties.Rows[6].Cells[1].Value = area;
        }
        public string Update_GetArea()
        {
            return (dGV_properties.Rows[6].Cells[1].Value != null) ? (string)dGV_properties.Rows[6].Cells[1].Value : "";
        }
        public void SetPosition(double x, double y)
        {

            properties.X = x;
            properties.Y = y;
            img.Width = 50;
            img.Height = 50;
            img.SetValue(Canvas.LeftProperty, x);
            img.SetValue(Canvas.TopProperty, y);
            TranslateTransform ptrans = new TranslateTransform();
            ptrans.X = -25;
            ptrans.Y = -25;
            img.RenderTransform = ptrans;
            SetLabel(properties.label);
            RegistrationAgent.mainWindowPointer.map.Children.Add(img);
            properties.NameKey = properties.stationNameID + " --- " + properties.label;
        }
        public void SetNewPosition(double x, double y)
        {
            properties.X = x;
            properties.Y = y;
            img.SetValue(Canvas.LeftProperty, x);
            img.SetValue(Canvas.TopProperty, y);
            TranslateTransform ptrans = new TranslateTransform();
            ptrans.X = -25;
            ptrans.Y = -25;
            img.RenderTransform = ptrans;
            plabel.SetValue(Canvas.LeftProperty, properties.X - 15);
            plabel.SetValue(Canvas.TopProperty, properties.Y - 40);
            SetLabel(properties.label);
        }
        public void SetKeyAndLabel(string text)
        {
            properties.label = text;
            properties.NameKey = properties.stationNameID + " --- " + properties.label;
        }
        public void SetLabel(string label)
        {
            properties.label = label;
            plabel.Text = label;
            plabel.Foreground = new SolidColorBrush(Colors.White);
            plabel.SetValue(Canvas.LeftProperty, properties.X);
            plabel.SetValue(Canvas.TopProperty, properties.Y - 40);
            plabel.TextAlignment = System.Windows.TextAlignment.Center;
            TranslateTransform ptrans = new TranslateTransform();
            ptrans.X = -plabel.Width / 2;
            ptrans.Y = -10;
            plabel.RenderTransform = ptrans;
            properties.NameKey = properties.stationNameID + " --- " + properties.label;
        }
        public void SetLabelBackgroundColor(Color color)
        {
            // green is connected
            // red is disdconnected
            plabel.Background = new SolidColorBrush(color);
        }
        public void Getpath(PathModel pathmodel, FLAGPOINTDEFINED fp)
        {
            if (pathmodel != null)
            {
                connectedPath_head.Add(pathmodel);
                flag_headtail.Add(fp);
            }
        }
        public void SetLabelTextColor(Color pcl)
        {
            plabel.Foreground = new SolidColorBrush(pcl);
        }
        public void UpdatePathfromNewPosition()
        {
            if (connectedPath_head.Count > 0)
            {
                for (int index = 0; index < connectedPath_head.Count; index++)
                {
                    if (connectedPath_head[index] != null)
                    {
                        if (flag_headtail[index] == FLAGPOINTDEFINED.FLAGPOINT_SET_STARTPOINT)
                        {
                            if (connectedPath_head[index].properties.PathType == PathModel.PATH_TYPE_DIRECT)
                            {
                                connectedPath_head[index].drawdirectpath(new System.Windows.Point(properties.X, properties.Y), connectedPath_head[index].endpos);
                            }
                            else if (connectedPath_head[index].properties.PathType == PathModel.PATH_TYPE_BEZIERSEGMENT)
                            {
                                connectedPath_head[index].drawbezierpath(new System.Windows.Point(properties.X, properties.Y), connectedPath_head[index].middlepos, connectedPath_head[index].endpos);
                            }
                            else if (connectedPath_head[index].properties.PathType == PathModel.PATH_TYPE_LINK)
                            {
                                connectedPath_head[index].drawdirectpath(new System.Windows.Point(properties.X, properties.Y), connectedPath_head[index].endpos);
                            }
                        }
                        if (flag_headtail[index] == FLAGPOINTDEFINED.FLAGPOINT_SET_ENDPOINT)
                        {
                            if (connectedPath_head[index].properties.PathType == PathModel.PATH_TYPE_DIRECT)
                            {
                                connectedPath_head[index].drawdirectpath(connectedPath_head[index].startpos, new System.Windows.Point(properties.X, properties.Y));
                            }
                            else if (connectedPath_head[index].properties.PathType == PathModel.PATH_TYPE_BEZIERSEGMENT)
                            {
                                connectedPath_head[index].drawbezierpath(connectedPath_head[index].startpos, connectedPath_head[index].middlepos, new System.Windows.Point(properties.X, properties.Y));
                            }
                            else if (connectedPath_head[index].properties.PathType == PathModel.PATH_TYPE_LINK)
                            {
                                connectedPath_head[index].drawdirectpath(connectedPath_head[index].startpos, new System.Windows.Point(properties.X, properties.Y));
                            }
                        }
                    }

                }
            }

        }
        public bool FindName(string name)
        {
            if (name.Equals(properties.stationNameID))
                return true;
            else
                return false;
        }
        public void SetNameID(string nameID)
        {
            properties.stationNameID = nameID;
            img.Name = nameID;
        }
        public void removeobject()
        {
            if (img != null)
            {
                RegistrationAgent.mainWindowPointer.map.Children.Remove(img);
                RegistrationAgent.mainWindowPointer.map.Children.Remove(plabel);

                if (connectedPath_head.Count > 0)
                {
                    for (int i = 0; i < connectedPath_head.Count; i++)
                    {

                        connectedPath_head[i].remove();
                    }
                }
            }

        }
        public void remove()
        {
            if (img != null)
            {
                RegistrationAgent.mainWindowPointer.map.Children.Remove(img);
                RegistrationAgent.mainWindowPointer.map.Children.Remove(plabel);
            }
        }
        public JObject createJsonstring()
        {
            dynamic product = new JObject();
            product.Name = properties.stationNameID;
            product.Label = properties.label;
            product.Type = properties.typeName;
            product.posX = properties.X;
            product.posY = properties.Y;
            product.stationID = properties.camera.id;
            product.stationIP = properties.camera.ip;
            product.stationPort = properties.camera.port;
            product.stationArea = properties.camera.area;
            product.lineInfo = lineInfo.save(properties.NameKey);
            return product;
        }
        public void Load_StationProperties(object _stuff)
        {
            dynamic stuff = _stuff as JObject;
            properties.stationNameID = stuff.Name;
            properties.label = stuff.Label;
            properties.typeName = stuff.Type;
            properties.X = stuff.posX;
            properties.Y = stuff.posY;
            properties.camera.id = stuff.stationID;
            properties.camera.ip = stuff.stationIP;
            properties.camera.port = stuff.stationPort;
            properties.camera.area = stuff.stationArea;
        }
        public bool CreateCameraAgentD()
        {
            if (RegistrationAgent.areaList.ContainsKey(properties.area) == false)
            {
                RegistrationAgent.areaList.Add(properties.area, new Area(properties.area));
                return RegistrationAgent.areaList[properties.area].AddDocking(this);
            }
            else
            {
                return RegistrationAgent.areaList[properties.area].AddDocking(this);
            }
        }
        public bool CreateCameraAgentP()
        {
            if (RegistrationAgent.areaList.ContainsKey(properties.area) == false)
            {
                RegistrationAgent.areaList.Add(properties.area, new Area(properties.area));
                return RegistrationAgent.areaList[properties.area].AddPutAway(this);
            }
            else
            {
                return RegistrationAgent.areaList[properties.area].AddPutAway(this);
            }
        }
        public bool CreateCameraAgentM()
        {
            if (RegistrationAgent.areaList.ContainsKey(properties.area) == false)
            {
                RegistrationAgent.areaList.Add(properties.area, new Area(properties.area));
                return RegistrationAgent.areaList[properties.area].AddMixed(this);
            }
            else
            {
                return RegistrationAgent.areaList[properties.area].AddMixed(this);
            }
        }
        private void connectToAgent_Click(object sender, EventArgs e)
        {
            /*if (ConnectToAgent())
            {
                setColorLabel(Colors.LimeGreen);
                properties.isConnected = true;
            }
            properties.isConnected = false;*/
        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Hide();
        }
        private void btn_update_Click(object sender, EventArgs e)
        {
            properties.stationNameID = Update_GetNameID();
            properties.label = Update_GetLabel();
            properties.camera.id = Update_GetCameraID();
            properties.camera.ip = Update_GetCameraIP();
            properties.camera.port = Update_GetCameraPort();
            properties.camera.area = Update_GetArea();
            
        }
        private void btn_lineInfo_Click(object sender, EventArgs e)
        {
            lineInfo.ShowDialog();
        }
        private void timerCheckConnection_Tick(object sender, EventArgs e)
        {
            if (rosSocket != null)
            {
                if(rosSocket.webSocket != null)
                {
                    if(rosSocket.webSocket.ReadyState.ToString().Equals("Closed"))
                    {
                        rosSocket.isConnected = false;
                        SetLabelBackgroundColor(Colors.Red);
                        timerCheckConnection.Stop();
                        timerConnectRosSocket.Start();
                        
                    }
                    else if(rosSocket.webSocket.ReadyState.ToString().Equals("Open"))
                    {
                        SetLabelBackgroundColor(Colors.LimeGreen);
                        rosSocket.isConnected = true;
                    }
                    Console.WriteLine(rosSocket.webSocket.ReadyState);
                }
                else
                {
                    rosSocket.isConnected = false;
                    SetLabelBackgroundColor(Colors.Red);
                    timerCheckConnection.Stop();
                    timerConnectRosSocket.Start();
                    
                }
            }
            
        }
        private void timerConnectRosSocket_Tick(object sender, EventArgs e)
        {
                rosSocket.seturl("ws://" + properties.camera.ip + ":" + properties.camera.port);
                if (rosSocket.isConnected)
                {
                    int subscription_id = rosSocket.Subscribe("/pallet_status_" + properties.camera.id, "std_msgs/string", subscriptionHandler);
                    timerConnectRosSocket.Stop();
                    timerCheckConnection.Start();
                }
                else
                {
                    rosSocket.connect();
                }
        }
        private void StationModel_FormClosed(object sender, FormClosedEventArgs e)
        {
            Hide();
        }
        private void subscriptionHandler(Communication.Message message)
        {
            StandardString standardstring = (StandardString)message;
            dynamic data = JObject.Parse(standardstring.data);
            int no_lines = Convert.ToInt32(data.NOL);
            int no_pallets_per_line = Convert.ToInt32(data.NOPPL);
            properties.camera.no_lines = no_lines;
            properties.camera.no_pallet_per_lines = no_pallets_per_line;
            properties.camera.ip = data.CAMIP;
            properties.camera.port = data.CAMPORT;
            properties.camera.id = Convert.ToInt32(data.CAMID);
            List<bool> palletStatusArray = new List<bool>(no_lines * no_pallets_per_line);

            if (properties.camera.lines_detail == null)
            {
                properties.camera.lines_detail = new SortedDictionary<string, LineDetail>();
            }
            for (int lineIndex = 0; lineIndex < no_lines; lineIndex++)
            {
                string line = "L" + lineIndex;
                LineDetail lineDetail = new LineDetail();
                //lineDetail.pallets_detail = new SortedDictionary<string, string>();
                bool first = true;
                for (int palletIndex = 0; palletIndex < no_pallets_per_line; palletIndex++)
                {
                    string pallet = "PL" + palletIndex;
                    string[] palletStatus = ((string)data[line][pallet]).Split('-');
                    lineDetail.pallets_detail.Add(palletIndex.ToString(), palletStatus[0]);
                    palletStatusArray.Add((palletStatus[0] == "yes") ? true : false);
                    if (!((palletStatus[1] == ("available") || palletStatus[1] == ("detected")) &&
                        (palletStatus[2] == "stable")))
                    {
                        if (first == true)
                        {
                            lineDetail.valid = false;
                            lineDetail.palletWarning = palletIndex.ToString();
                            first = false;
                        }
                    }
                }
                if (!properties.camera.lines_detail.ContainsKey(lineIndex.ToString()))
                {
                    //NEW
                    properties.camera.lines_detail.Add(lineIndex.ToString(), lineDetail);
                    properties.pallets_status = palletStatusArray;
                    properties.camera.valid = false;
                }
                else
                {
                    //UPDATE
                    properties.camera.lines_detail[lineIndex.ToString()].pallets_detail = lineDetail.pallets_detail;
                    properties.camera.lines_detail[lineIndex.ToString()].valid = lineDetail.valid;
                    if (lineDetail.palletWarning != "none")
                    {
                        properties.camera.lines_detail[lineIndex.ToString()].palletWarning = lineDetail.palletWarning;
                    }
                    properties.pallets_status = palletStatusArray;
                    properties.camera.valid = false;
                }
            }
            properties.camera.valid = true;
            if (RegistrationAgent.areaList.Count != 0)
            {
                RegistrationAgent.areaList[properties.camera.area].ProcessStation(properties.typeName, properties.stationNameID);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(lineInfo.jsonLine(0, properties.type, properties.stationNameID, 0));
        }

        private void button2_Click(object sender, EventArgs e)
        {
        //    MessageBox.Show(lineInfo.jsonPallet(0, properties.type, properties.stationNameID, 0,1));
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void StationModel_Load(object sender, EventArgs e)
        {

        }
    }
}
