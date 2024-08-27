namespace TestPLC
{
  partial class Form1
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
      this.panel_Header = new System.Windows.Forms.Panel();
      this.modbusTcp1 = new ModbusTcpClient.ModbusTcp();
      this.btTestRead = new System.Windows.Forms.Button();
      this.panel_status = new System.Windows.Forms.Panel();
      this.slmpCommUC1 = new TcpComm.TcpCommUC();
      this.panel_Footer = new System.Windows.Forms.Panel();
      this.panel_body = new System.Windows.Forms.Panel();
      this.btWriteResetCounter = new System.Windows.Forms.Button();
      this.panel_Header.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel_Header
      // 
      this.panel_Header.BackColor = System.Drawing.SystemColors.ActiveCaption;
      this.panel_Header.Controls.Add(this.btWriteResetCounter);
      this.panel_Header.Controls.Add(this.modbusTcp1);
      this.panel_Header.Controls.Add(this.btTestRead);
      this.panel_Header.Controls.Add(this.panel_status);
      this.panel_Header.Controls.Add(this.slmpCommUC1);
      this.panel_Header.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel_Header.Location = new System.Drawing.Point(0, 0);
      this.panel_Header.Name = "panel_Header";
      this.panel_Header.Size = new System.Drawing.Size(1134, 123);
      this.panel_Header.TabIndex = 0;
      // 
      // modbusTcp1
      // 
      this.modbusTcp1.Location = new System.Drawing.Point(129, 12);
      this.modbusTcp1.Name = "modbusTcp1";
      this.modbusTcp1.Size = new System.Drawing.Size(263, 38);
      this.modbusTcp1.TabIndex = 3;
      // 
      // btTestRead
      // 
      this.btTestRead.Location = new System.Drawing.Point(12, 56);
      this.btTestRead.Name = "btTestRead";
      this.btTestRead.Size = new System.Drawing.Size(95, 41);
      this.btTestRead.TabIndex = 2;
      this.btTestRead.Text = "Test Read data";
      this.btTestRead.UseVisualStyleBackColor = true;
      this.btTestRead.Click += new System.EventHandler(this.btTestRead_Click);
      // 
      // panel_status
      // 
      this.panel_status.BackColor = System.Drawing.Color.Green;
      this.panel_status.Location = new System.Drawing.Point(23, 34);
      this.panel_status.Name = "panel_status";
      this.panel_status.Size = new System.Drawing.Size(31, 16);
      this.panel_status.TabIndex = 1;
      // 
      // slmpCommUC1
      // 
      this.slmpCommUC1.Index = 1;
      this.slmpCommUC1.IPAddress = "192.168.3.124";
      this.slmpCommUC1.Location = new System.Drawing.Point(24, 4);
      this.slmpCommUC1.Name = "slmpCommUC1";
      this.slmpCommUC1.Port = ((ushort)(2000));
      this.slmpCommUC1.Size = new System.Drawing.Size(32, 32);
      this.slmpCommUC1.TabIndex = 0;
      // 
      // panel_Footer
      // 
      this.panel_Footer.BackColor = System.Drawing.SystemColors.ControlDarkDark;
      this.panel_Footer.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel_Footer.Location = new System.Drawing.Point(0, 724);
      this.panel_Footer.Name = "panel_Footer";
      this.panel_Footer.Size = new System.Drawing.Size(1134, 57);
      this.panel_Footer.TabIndex = 1;
      // 
      // panel_body
      // 
      this.panel_body.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel_body.Location = new System.Drawing.Point(0, 123);
      this.panel_body.Name = "panel_body";
      this.panel_body.Size = new System.Drawing.Size(1134, 601);
      this.panel_body.TabIndex = 2;
      // 
      // btWriteResetCounter
      // 
      this.btWriteResetCounter.Location = new System.Drawing.Point(129, 56);
      this.btWriteResetCounter.Name = "btWriteResetCounter";
      this.btWriteResetCounter.Size = new System.Drawing.Size(168, 41);
      this.btWriteResetCounter.TabIndex = 4;
      this.btWriteResetCounter.Text = "Test Write reset counter";
      this.btWriteResetCounter.UseVisualStyleBackColor = true;
      this.btWriteResetCounter.Click += new System.EventHandler(this.btWriteResetCounter_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1134, 781);
      this.Controls.Add(this.panel_body);
      this.Controls.Add(this.panel_Footer);
      this.Controls.Add(this.panel_Header);
      this.Name = "Form1";
      this.Text = "Form1";
      this.panel_Header.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel_Header;
    private System.Windows.Forms.Panel panel_Footer;
    private System.Windows.Forms.Panel panel_body;
    private System.Windows.Forms.Panel panel_status;
    private TcpComm.TcpCommUC slmpCommUC1;
    private System.Windows.Forms.Button btTestRead;
    private ModbusTcpClient.ModbusTcp modbusTcp1;
    private System.Windows.Forms.Button btWriteResetCounter;
  }
}

