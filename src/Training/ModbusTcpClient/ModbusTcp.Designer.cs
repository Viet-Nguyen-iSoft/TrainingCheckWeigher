
namespace ModbusTcpClient
{
  partial class ModbusTcp
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.lblError = new System.Windows.Forms.Label();
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.itemConnect = new System.Windows.Forms.ToolStripMenuItem();
      this.itemDisconnect = new System.Windows.Forms.ToolStripMenuItem();
      this.itemReconnect = new System.Windows.Forms.ToolStripMenuItem();
      this.itemStartAutoRead = new System.Windows.Forms.ToolStripMenuItem();
      this.itemStopAutoRead = new System.Windows.Forms.ToolStripMenuItem();
      this.itemReset = new System.Windows.Forms.ToolStripMenuItem();
      this.lblControllerStatus = new System.Windows.Forms.Label();
      this.lblIpAdress = new System.Windows.Forms.Label();
      this.tableLayoutPanel1.SuspendLayout();
      this.contextMenuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
      this.tableLayoutPanel1.ColumnCount = 3;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.Controls.Add(this.lblIpAdress, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.lblError, 2, 0);
      this.tableLayoutPanel1.Controls.Add(this.lblControllerStatus, 1, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 1;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(263, 26);
      this.tableLayoutPanel1.TabIndex = 15;
      // 
      // lblError
      // 
      this.lblError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this.lblError.AutoSize = true;
      this.lblError.BackColor = System.Drawing.Color.Transparent;
      this.lblError.Font = new System.Drawing.Font("Century Gothic", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblError.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
      this.lblError.Location = new System.Drawing.Point(174, 6);
      this.lblError.Margin = new System.Windows.Forms.Padding(0);
      this.lblError.Name = "lblError";
      this.lblError.Size = new System.Drawing.Size(89, 13);
      this.lblError.TabIndex = 4;
      this.lblError.Text = "Error";
      this.lblError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemConnect,
            this.itemDisconnect,
            this.itemReconnect,
            this.itemStartAutoRead,
            this.itemStopAutoRead,
            this.itemReset});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(134, 136);
      this.contextMenuStrip1.Text = "Hello";
      // 
      // itemConnect
      // 
      this.itemConnect.Name = "itemConnect";
      this.itemConnect.Size = new System.Drawing.Size(133, 22);
      this.itemConnect.Text = "Connect";
      // 
      // itemDisconnect
      // 
      this.itemDisconnect.Name = "itemDisconnect";
      this.itemDisconnect.Size = new System.Drawing.Size(133, 22);
      this.itemDisconnect.Text = "Disconnect";
      // 
      // itemReconnect
      // 
      this.itemReconnect.Name = "itemReconnect";
      this.itemReconnect.Size = new System.Drawing.Size(133, 22);
      this.itemReconnect.Text = "Reconnect";
      // 
      // itemStartAutoRead
      // 
      this.itemStartAutoRead.Name = "itemStartAutoRead";
      this.itemStartAutoRead.Size = new System.Drawing.Size(133, 22);
      this.itemStartAutoRead.Text = "Start Auto";
      // 
      // itemStopAutoRead
      // 
      this.itemStopAutoRead.Name = "itemStopAutoRead";
      this.itemStopAutoRead.Size = new System.Drawing.Size(133, 22);
      this.itemStopAutoRead.Text = "Stop Auto";
      // 
      // itemReset
      // 
      this.itemReset.Name = "itemReset";
      this.itemReset.Size = new System.Drawing.Size(133, 22);
      this.itemReset.Text = "Reset";
      // 
      // lblControllerStatus
      // 
      this.lblControllerStatus.BackColor = System.Drawing.Color.Transparent;
      this.lblControllerStatus.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lblControllerStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblControllerStatus.Location = new System.Drawing.Point(87, 0);
      this.lblControllerStatus.Margin = new System.Windows.Forms.Padding(0);
      this.lblControllerStatus.Name = "lblControllerStatus";
      this.lblControllerStatus.Size = new System.Drawing.Size(87, 26);
      this.lblControllerStatus.TabIndex = 12;
      this.lblControllerStatus.Text = "Offline";
      this.lblControllerStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lblIpAdress
      // 
      this.lblIpAdress.BackColor = System.Drawing.Color.Transparent;
      this.lblIpAdress.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lblIpAdress.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblIpAdress.Location = new System.Drawing.Point(0, 0);
      this.lblIpAdress.Margin = new System.Windows.Forms.Padding(0);
      this.lblIpAdress.Name = "lblIpAdress";
      this.lblIpAdress.Size = new System.Drawing.Size(87, 26);
      this.lblIpAdress.TabIndex = 13;
      this.lblIpAdress.Text = "IPAddress";
      this.lblIpAdress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // ModbusTcp
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "ModbusTcp";
      this.Size = new System.Drawing.Size(263, 26);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.contextMenuStrip1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Label lblError;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem itemConnect;
    private System.Windows.Forms.ToolStripMenuItem itemDisconnect;
    private System.Windows.Forms.ToolStripMenuItem itemReconnect;
    private System.Windows.Forms.ToolStripMenuItem itemStartAutoRead;
    private System.Windows.Forms.ToolStripMenuItem itemStopAutoRead;
    private System.Windows.Forms.ToolStripMenuItem itemReset;
    private System.Windows.Forms.Label lblControllerStatus;
    private System.Windows.Forms.Label lblIpAdress;
  }
}
