namespace Tool
{
	partial class MainForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.groupWindow = new System.Windows.Forms.GroupBox();
			this.chkHideBorder = new System.Windows.Forms.CheckBox();
			this.panelOpacity = new System.Windows.Forms.Panel();
			this.txtOpacity = new System.Windows.Forms.TextBox();
			this.trackOpacity = new System.Windows.Forms.TrackBar();
			this.label5 = new System.Windows.Forms.Label();
			this.numHeight = new Tool.WheelNumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.numWidth = new Tool.WheelNumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.groupUrl = new System.Windows.Forms.GroupBox();
			this.txtUrl = new System.Windows.Forms.TextBox();
			this.panelButtons = new System.Windows.Forms.Panel();
			this.btnCircle = new System.Windows.Forms.Button();
			this.btnOpen = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.groupWindow.SuspendLayout();
			this.panelOpacity.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackOpacity)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numHeight)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numWidth)).BeginInit();
			this.groupUrl.SuspendLayout();
			this.panelButtons.SuspendLayout();
			this.SuspendLayout();
			// (removed browser settings group)
			// 
			// groupWindow
			// 
			this.groupWindow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.groupWindow.Controls.Clear();
			this.groupWindow.Controls.Add(this.panelOpacity);
			this.groupWindow.Controls.Add(this.chkHideBorder);
			this.groupWindow.Controls.Add(this.numHeight);
			this.groupWindow.Controls.Add(this.label4);
			this.groupWindow.Controls.Add(this.numWidth);
			this.groupWindow.Controls.Add(this.label3);
			this.groupWindow.Location = new System.Drawing.Point(12, 12);
			this.groupWindow.Name = "groupWindow";
			this.groupWindow.Padding = new System.Windows.Forms.Padding(10, 14, 10, 10);
			this.groupWindow.Size = new System.Drawing.Size(376, 130);
			this.groupWindow.TabIndex = 1;
			this.groupWindow.TabStop = false;
			this.groupWindow.Text = "窗口设置";
			// 
			// panelOpacity
			// 
			this.panelOpacity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.panelOpacity.Controls.Add(this.txtOpacity);
			this.panelOpacity.Controls.Add(this.trackOpacity);
			this.panelOpacity.Controls.Add(this.label5);
			this.panelOpacity.Location = new System.Drawing.Point(16, 84);
			this.panelOpacity.Name = "panelOpacity";
			this.panelOpacity.Size = new System.Drawing.Size(342, 26);
			this.panelOpacity.TabIndex = 4;
			// 
			// chkHideBorder
			// 
			this.chkHideBorder.AutoSize = true;
			this.chkHideBorder.Location = new System.Drawing.Point(30, 24);
			this.chkHideBorder.Name = "chkHideBorder";
			this.chkHideBorder.Size = new System.Drawing.Size(99, 21);
			this.chkHideBorder.TabIndex = 0;
			this.chkHideBorder.Text = "隐藏窗口边框";
			this.chkHideBorder.UseVisualStyleBackColor = true;
			// 
			// txtOpacity
			// 
			this.txtOpacity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtOpacity.Location = new System.Drawing.Point(280, 3);
			this.txtOpacity.Name = "txtOpacity";
			this.txtOpacity.Size = new System.Drawing.Size(50, 23);
			this.txtOpacity.TabIndex = 2;
			this.txtOpacity.Text = "0.95";
			this.txtOpacity.TextChanged += new System.EventHandler(this.txtOpacity_TextChanged);
			// 
			// trackOpacity
			// 
			this.trackOpacity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.trackOpacity.Location = new System.Drawing.Point(88, 3);
			this.trackOpacity.Maximum = 100;
			this.trackOpacity.Minimum = 10;
			this.trackOpacity.Name = "trackOpacity";
			this.trackOpacity.Size = new System.Drawing.Size(186, 45);
			this.trackOpacity.TabIndex = 1;
			this.trackOpacity.TickFrequency = 5;
			this.trackOpacity.Value = 95;
			this.trackOpacity.ValueChanged += new System.EventHandler(this.trackOpacity_ValueChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(14, 7);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(44, 17);
			this.label5.TabIndex = 0;
			this.label5.Text = "透明度";
			// 
			// numHeight
			// 
			this.numHeight.Location = new System.Drawing.Point(262, 54);
			this.numHeight.Maximum = new decimal(new int[] {
			10000,
			0,
			0,
			0});
			this.numHeight.Minimum = new decimal(new int[] {
			200,
			0,
			0,
			0});
			this.numHeight.Increment = new decimal(new int[] {1,0,0,0});
			this.numHeight.Name = "numHeight";
			this.numHeight.Size = new System.Drawing.Size(90, 23);
			this.numHeight.TabIndex = 3;
			this.numHeight.Value = new decimal(new int[] {
			800,
			0,
			0,
			0});
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(232, 57);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(20, 17);
			this.label4.TabIndex = 2;
			this.label4.Text = "高";
			// 
			// numWidth
			// 
			this.numWidth.Location = new System.Drawing.Point(90, 54);
			this.numWidth.Maximum = new decimal(new int[] {
			10000,
			0,
			0,
			0});
			this.numWidth.Minimum = new decimal(new int[] {
			200,
			0,
			0,
			0});
			this.numWidth.Increment = new decimal(new int[] {1,0,0,0});
			this.numWidth.Name = "numWidth";
			this.numWidth.Size = new System.Drawing.Size(90, 23);
			this.numWidth.TabIndex = 1;
			this.numWidth.Value = new decimal(new int[] {
			1200,
			0,
			0,
			0});
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(30, 54);
			this.label3.Name = "label3";
			this.label3.TabIndex = 0;
			this.label3.Text = "宽";
			// 
			// groupUrl
			// 
			this.groupUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.groupUrl.Controls.Add(this.txtUrl);
			this.groupUrl.Location = new System.Drawing.Point(12, 146);
			this.groupUrl.Name = "groupUrl";
			this.groupUrl.Padding = new System.Windows.Forms.Padding(10, 8, 10, 10);
			this.groupUrl.Size = new System.Drawing.Size(376, 54);
			this.groupUrl.TabIndex = 2;
			this.groupUrl.TabStop = false;
			this.groupUrl.Text = "目标网站";
			// 
			// txtUrl
			// 
			this.txtUrl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtUrl.Location = new System.Drawing.Point(10, 24);
			this.txtUrl.Name = "txtUrl";
			this.txtUrl.PlaceholderText = "输入要打开的网址，例如 https://www.example.com";
			this.txtUrl.Size = new System.Drawing.Size(356, 23);
			this.txtUrl.TabIndex = 0;
			// 
			// panelButtons
			// 
			this.panelButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.panelButtons.Location = new System.Drawing.Point(12, 206);
			this.panelButtons.Name = "panelButtons";
			this.panelButtons.Size = new System.Drawing.Size(376, 36);
			this.panelButtons.TabIndex = 3;
			// 
			// btnCircle
			// 
			this.btnCircle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCircle.Location = new System.Drawing.Point(10, 6);
			this.btnCircle.Name = "btnCircle";
			this.btnCircle.Size = new System.Drawing.Size(84, 28);
			this.btnCircle.TabIndex = 0;
			this.btnCircle.Text = "圆圈展示";
			this.btnCircle.UseVisualStyleBackColor = true;
			this.btnCircle.Click += new System.EventHandler(this.btnCircle_Click);
			// 
			// btnOpen
			// 
			this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOpen.Location = new System.Drawing.Point(196, 6);
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(84, 28);
			this.btnOpen.TabIndex = 1;
			this.btnOpen.Text = "打开浏览器";
			this.btnOpen.UseVisualStyleBackColor = true;
			this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.Location = new System.Drawing.Point(292, 6);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(84, 28);
			this.btnClose.TabIndex = 2;
			this.btnClose.Text = "关闭浏览器";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnSave
			// 
			this.btnSave = new System.Windows.Forms.Button();
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.Location = new System.Drawing.Point(100, 6);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(84, 28);
			this.btnSave.TabIndex = 0;
			this.btnSave.Text = "保存设置";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// add buttons to panel after init
			this.panelButtons.Controls.Add(this.btnCircle);
			this.panelButtons.Controls.Add(this.btnSave);
			this.panelButtons.Controls.Add(this.btnOpen);
			this.panelButtons.Controls.Add(this.btnClose);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(400, 260);
			this.Controls.Add(this.panelButtons);
			this.Controls.Add(this.groupUrl);
			this.Controls.Add(this.groupWindow);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = true;
			this.MaximumSize = new System.Drawing.Size(416, 300);
			this.MinimumSize = new System.Drawing.Size(416, 300);
			this.Name = "MainForm";
			this.Text = "浏览器置顶工具";
			this.TopMost = true;
			this.groupWindow.ResumeLayout(false);
			this.groupWindow.PerformLayout();
			this.panelOpacity.ResumeLayout(false);
			this.panelOpacity.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackOpacity)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numHeight)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numWidth)).EndInit();
			this.groupUrl.ResumeLayout(false);
			this.groupUrl.PerformLayout();
			this.panelButtons.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private GroupBox groupWindow;
		private CheckBox chkHideBorder;
		private Panel panelOpacity;
		private TextBox txtOpacity;
		private TrackBar trackOpacity;
		private Label label5;
		private WheelNumericUpDown numHeight;
		private Label label4;
		private WheelNumericUpDown numWidth;
		private Label label3;
		private GroupBox groupUrl;
		private TextBox txtUrl;
		private Panel panelButtons;
		private Button btnCircle;
		private Button btnOpen;
		private Button btnClose;
		private Button btnSave;
	}
}
