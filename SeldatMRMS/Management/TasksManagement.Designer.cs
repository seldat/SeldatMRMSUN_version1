namespace SeldatMRMS.Management
{
	partial class TasksManagement
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.timerInterruptAllRobotAgentStatus = new System.Windows.Forms.Timer(this.components);
            this.timerInterruptAllChargerStatus = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timerInterruptAllRobotAgentStatus
            // 
            this.timerInterruptAllRobotAgentStatus.Interval = 2000;
            // 
            // timerInterruptAllChargerStatus
            // 
            this.timerInterruptAllChargerStatus.Interval = 10000;
            // 
            // TasksManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 156);
            this.Name = "TasksManagement";
            this.Text = "TasksManagement";
            this.Load += new System.EventHandler(this.TasksManagement_Load);
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Timer timerInterruptAllRobotAgentStatus;
		private System.Windows.Forms.Timer timerInterruptAllChargerStatus;
	}
}