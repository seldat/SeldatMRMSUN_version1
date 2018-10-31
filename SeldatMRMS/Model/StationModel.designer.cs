namespace SeldatMRMS.Model
{
    partial class StationModel
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
            this.SuspendLayout();
            // 
            // StationModel
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "StationModel";
            this.Load += new System.EventHandler(this.StationModel_Load);
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.Timer timerCheckConnection;
        private System.Windows.Forms.Label txt_console;
        private System.Windows.Forms.Timer timerConnectRosSocket;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Button btn_Connect;
        public System.Windows.Forms.Button btn_lineInfo;
        public System.Windows.Forms.Button btn_update;
        public System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.DataGridView dGV_properties;
        private System.Windows.Forms.DataGridViewTextBoxColumn txt_properties;
        private System.Windows.Forms.DataGridViewTextBoxColumn txt_value;
    }
}