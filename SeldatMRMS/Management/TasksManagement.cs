using SeldatMRMS.Communication;
using SeldatMRMS.Management.OrderManager;
using SeldatMRMS.Management.RobotManagent;
using SeldatMRMS.Management.TrafficManager;
using SeldatMRMS.RobotView;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Media.Media3D;

namespace SeldatMRMS.Management
{
    public partial class TasksManagement : Form
    {
        public Orders orders;
        public ReadyArea readyArea;
        public TasksManagement()
        {
            InitializeComponent();
            timerInterruptAllRobotAgentStatus.Start();

            createLogFolder();
            //	probot3dmap = new RobotView3D();
            //	probot3dmap.ShowDialog();
            //RegistrationAgent.robotview3dPointer.loadAWareHouseMap();
            //RegistrationAgent.robotview3dPointer.ShowDialog();
            RegistrationAgent.robotview3dPointer.loadAWareHouseMap();
            orders = new Orders();
            readyArea = new ReadyArea("Ready", "0");
            readyArea.updateLandMarkPoint(new Point3D(9.54, -6.05, 0));
            orders.addReadyAreaDepends(readyArea);

        }
        private void createLogFolder()
        {
            string subPath = Properties.Resources.NAME_LOGFOLDER; // your code goes here
            String m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + subPath;
            bool exists = System.IO.Directory.Exists(m_exePath);
            if (!exists)
                System.IO.Directory.CreateDirectory(m_exePath);

        }
        public void addANewRobotAgent(RobotAgent _pr)
        {

            _pr.setRobotView3D(RegistrationAgent.robotview3dPointer);
            _pr.requestParams();
        }
        public void UpdateRobotAgentProperties(RobotAgent _pr)
        {

        }
        public void updateRobotInfoToTaskManager(RobotAgent.RobotInfo robotInfo)
        {
            UpdateAtDataGridViewRobotManage(robotInfo);
        }

        public void deleteObjectRobotAgent(int pos)
        {

        }
        public void updateNewDataGridViewRobotManage(RobotAgent.RobotInfo robotInfo)
        {
            /*try
			{
				this.Invoke((MethodInvoker)delegate
				{
					DataGridViewRow row = (DataGridViewRow)dataGridViewRobotManager.Rows[0].Clone();
					row.Cells[0].Value = (String)robotInfo.robotName;
					row.Cells[1].Value = robotInfo.alive;
					row.Cells[2].Value = (String)robotInfo.statusconnection;
					row.Cells[2].Value = Convert.ToString(robotInfo.batteryPercentage);
					dataGridViewRobotManager.Rows.Add(row);
				});
			}
			catch { }*/

        }
        public void UpdateAtDataGridViewRobotManage(RobotAgent.RobotInfo robotInfo)
        {
            /*try
			{
				this.Invoke((MethodInvoker)delegate
				{
					
					dataGridViewRobotManager[0, robotInfo.posInGridView].Value = (String)robotInfo.robotName;
					dataGridViewRobotManager[1, robotInfo.posInGridView].Value = robotInfo.alive;
					dataGridViewRobotManager[2, robotInfo.posInGridView].Value = (String)robotInfo.statusconnection;
					dataGridViewRobotManager[3, robotInfo.posInGridView].Value = Convert.ToString(robotInfo.batteryPercentage);
				});
			}
			catch { }*/
        }

        private void TasksManagement_Load(object sender, EventArgs e)
        {

        }
    }
}
