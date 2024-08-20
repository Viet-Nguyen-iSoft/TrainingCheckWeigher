
namespace ModbusTcpClientTester
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
      GlacialComponents.Controls.GLColumn glColumn3 = new GlacialComponents.Controls.GLColumn();
      GlacialComponents.Controls.GLColumn glColumn4 = new GlacialComponents.Controls.GLColumn();
      GlacialComponents.Controls.GLColumn glColumn1 = new GlacialComponents.Controls.GLColumn();
      GlacialComponents.Controls.GLColumn glColumn2 = new GlacialComponents.Controls.GLColumn();
      this.panel1 = new System.Windows.Forms.Panel();
      this.btStartCyclic = new System.Windows.Forms.Button();
      this.btStopCyclic = new System.Windows.Forms.Button();
      this.btWriteSingleCoil = new System.Windows.Forms.Button();
      this.modbusTcp1 = new ModbusTcpClient.ModbusTcp();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.glacialList1 = new GlacialComponents.Controls.GlacialList();
      this.glacialList2 = new GlacialComponents.Controls.GlacialList();
      this.panel1.SuspendLayout();
      this.tableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.btWriteSingleCoil);
      this.panel1.Controls.Add(this.btStopCyclic);
      this.panel1.Controls.Add(this.btStartCyclic);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 41);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(775, 36);
      this.panel1.TabIndex = 4;
      // 
      // btStartCyclic
      // 
      this.btStartCyclic.Dock = System.Windows.Forms.DockStyle.Left;
      this.btStartCyclic.Location = new System.Drawing.Point(0, 0);
      this.btStartCyclic.Name = "btStartCyclic";
      this.btStartCyclic.Size = new System.Drawing.Size(103, 36);
      this.btStartCyclic.TabIndex = 2;
      this.btStartCyclic.Text = "Start Cyclic";
      this.btStartCyclic.UseVisualStyleBackColor = true;
      // 
      // btStopCyclic
      // 
      this.btStopCyclic.Dock = System.Windows.Forms.DockStyle.Left;
      this.btStopCyclic.Location = new System.Drawing.Point(103, 0);
      this.btStopCyclic.Name = "btStopCyclic";
      this.btStopCyclic.Size = new System.Drawing.Size(103, 36);
      this.btStopCyclic.TabIndex = 3;
      this.btStopCyclic.Text = "Stop Cyclic";
      this.btStopCyclic.UseVisualStyleBackColor = true;
      // 
      // btWriteSingleCoil
      // 
      this.btWriteSingleCoil.Dock = System.Windows.Forms.DockStyle.Left;
      this.btWriteSingleCoil.Location = new System.Drawing.Point(206, 0);
      this.btWriteSingleCoil.Name = "btWriteSingleCoil";
      this.btWriteSingleCoil.Size = new System.Drawing.Size(103, 36);
      this.btWriteSingleCoil.TabIndex = 4;
      this.btWriteSingleCoil.Text = "WriteSingleCoil";
      this.btWriteSingleCoil.UseVisualStyleBackColor = true;
      // 
      // modbusTcp1
      // 
      this.modbusTcp1.Dock = System.Windows.Forms.DockStyle.Top;
      this.modbusTcp1.Location = new System.Drawing.Point(0, 0);
      this.modbusTcp1.Name = "modbusTcp1";
      this.modbusTcp1.Size = new System.Drawing.Size(775, 41);
      this.modbusTcp1.TabIndex = 0;
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 3;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47F));
      this.tableLayoutPanel1.Controls.Add(this.glacialList2, 2, 0);
      this.tableLayoutPanel1.Controls.Add(this.glacialList1, 0, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 77);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 1;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(775, 529);
      this.tableLayoutPanel1.TabIndex = 5;
      // 
      // glacialList1
      // 
      this.glacialList1.ActivatedEmbeddedData = null;
      this.glacialList1.AllowColumnResize = true;
      this.glacialList1.AllowMultiselect = false;
      this.glacialList1.AlternateBackground = System.Drawing.Color.WhiteSmoke;
      this.glacialList1.AlternatingColors = false;
      this.glacialList1.AutoHeight = true;
      this.glacialList1.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.glacialList1.BackgroundStretchToFit = true;
      glColumn3.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
      glColumn3.CheckBoxes = false;
      glColumn3.CheckBoxesReadOnly = false;
      glColumn3.DisplayProgressBar = false;
      glColumn3.EnableContextMenu = true;
      glColumn3.GLActivatedEmbeddePreviousTypes = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
      glColumn3.ImageIndex = -1;
      glColumn3.Name = "Column1";
      glColumn3.NumericSort = false;
      glColumn3.Text = "D";
      glColumn3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
      glColumn3.Width = 100;
      glColumn4.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
      glColumn4.CheckBoxes = false;
      glColumn4.CheckBoxesReadOnly = false;
      glColumn4.DisplayProgressBar = false;
      glColumn4.EnableContextMenu = true;
      glColumn4.GLActivatedEmbeddePreviousTypes = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
      glColumn4.ImageIndex = -1;
      glColumn4.Name = "Column2";
      glColumn4.NumericSort = false;
      glColumn4.Text = "Value";
      glColumn4.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
      glColumn4.Width = 100;
      this.glacialList1.Columns.AddRange(new GlacialComponents.Controls.GLColumn[] {
            glColumn3,
            glColumn4});
      this.glacialList1.ControlStyle = GlacialComponents.Controls.GLControlStyles.Normal;
      this.glacialList1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.glacialList1.FullRowSelect = false;
      this.glacialList1.GridColor = System.Drawing.Color.LightGray;
      this.glacialList1.GridLines = GlacialComponents.Controls.GLGridLines.gridBoth;
      this.glacialList1.GridLineStyle = GlacialComponents.Controls.GLGridLineStyles.gridSolid;
      this.glacialList1.GridTypes = GlacialComponents.Controls.GLGridTypes.gridOnExists;
      this.glacialList1.HeaderHeight = 22;
      this.glacialList1.HeaderVisible = true;
      this.glacialList1.HeaderWordWrap = false;
      this.glacialList1.HighLight_SelectedSubItem = false;
      this.glacialList1.HighLightSelectedSubItemColor = System.Drawing.Color.Red;
      this.glacialList1.HotColumnTracking = true;
      this.glacialList1.HotItemTracking = true;
      this.glacialList1.HotTrackingColor = System.Drawing.Color.LightGray;
      this.glacialList1.HoverEvents = false;
      this.glacialList1.HoverTime = 1;
      this.glacialList1.ImageList = null;
      this.glacialList1.ItemHeight = 17;
      this.glacialList1.ItemWordWrap = false;
      this.glacialList1.Location = new System.Drawing.Point(3, 3);
      this.glacialList1.Name = "glacialList1";
      this.glacialList1.Selectable = true;
      this.glacialList1.SelectedTextColor = System.Drawing.Color.White;
      this.glacialList1.SelectionColor = System.Drawing.Color.DarkBlue;
      this.glacialList1.ShowBorder = true;
      this.glacialList1.ShowFocusRect = false;
      this.glacialList1.Size = new System.Drawing.Size(358, 523);
      this.glacialList1.SortType = GlacialComponents.Controls.SortTypes.InsertionSort;
      this.glacialList1.SuperFlatHeaderColor = System.Drawing.Color.White;
      this.glacialList1.TabIndex = 0;
      this.glacialList1.Text = "glacialList1";
      // 
      // glacialList2
      // 
      this.glacialList2.ActivatedEmbeddedData = null;
      this.glacialList2.AllowColumnResize = true;
      this.glacialList2.AllowMultiselect = false;
      this.glacialList2.AlternateBackground = System.Drawing.Color.WhiteSmoke;
      this.glacialList2.AlternatingColors = false;
      this.glacialList2.AutoHeight = true;
      this.glacialList2.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.glacialList2.BackgroundStretchToFit = true;
      glColumn1.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
      glColumn1.CheckBoxes = false;
      glColumn1.CheckBoxesReadOnly = false;
      glColumn1.DisplayProgressBar = false;
      glColumn1.EnableContextMenu = true;
      glColumn1.GLActivatedEmbeddePreviousTypes = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
      glColumn1.ImageIndex = -1;
      glColumn1.Name = "Column1";
      glColumn1.NumericSort = false;
      glColumn1.Text = "D";
      glColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
      glColumn1.Width = 100;
      glColumn2.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
      glColumn2.CheckBoxes = false;
      glColumn2.CheckBoxesReadOnly = false;
      glColumn2.DisplayProgressBar = false;
      glColumn2.EnableContextMenu = true;
      glColumn2.GLActivatedEmbeddePreviousTypes = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
      glColumn2.ImageIndex = -1;
      glColumn2.Name = "Column2";
      glColumn2.NumericSort = false;
      glColumn2.Text = "Value";
      glColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
      glColumn2.Width = 100;
      this.glacialList2.Columns.AddRange(new GlacialComponents.Controls.GLColumn[] {
            glColumn1,
            glColumn2});
      this.glacialList2.ControlStyle = GlacialComponents.Controls.GLControlStyles.Normal;
      this.glacialList2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.glacialList2.FullRowSelect = false;
      this.glacialList2.GridColor = System.Drawing.Color.LightGray;
      this.glacialList2.GridLines = GlacialComponents.Controls.GLGridLines.gridBoth;
      this.glacialList2.GridLineStyle = GlacialComponents.Controls.GLGridLineStyles.gridSolid;
      this.glacialList2.GridTypes = GlacialComponents.Controls.GLGridTypes.gridOnExists;
      this.glacialList2.HeaderHeight = 22;
      this.glacialList2.HeaderVisible = true;
      this.glacialList2.HeaderWordWrap = false;
      this.glacialList2.HighLight_SelectedSubItem = false;
      this.glacialList2.HighLightSelectedSubItemColor = System.Drawing.Color.Red;
      this.glacialList2.HotColumnTracking = true;
      this.glacialList2.HotItemTracking = true;
      this.glacialList2.HotTrackingColor = System.Drawing.Color.LightGray;
      this.glacialList2.HoverEvents = false;
      this.glacialList2.HoverTime = 1;
      this.glacialList2.ImageList = null;
      this.glacialList2.ItemHeight = 17;
      this.glacialList2.ItemWordWrap = false;
      this.glacialList2.Location = new System.Drawing.Point(413, 3);
      this.glacialList2.Name = "glacialList2";
      this.glacialList2.Selectable = true;
      this.glacialList2.SelectedTextColor = System.Drawing.Color.White;
      this.glacialList2.SelectionColor = System.Drawing.Color.DarkBlue;
      this.glacialList2.ShowBorder = true;
      this.glacialList2.ShowFocusRect = false;
      this.glacialList2.Size = new System.Drawing.Size(359, 523);
      this.glacialList2.SortType = GlacialComponents.Controls.SortTypes.InsertionSort;
      this.glacialList2.SuperFlatHeaderColor = System.Drawing.Color.White;
      this.glacialList2.TabIndex = 2;
      this.glacialList2.Text = "glacialList3";
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(775, 606);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.modbusTcp1);
      this.Name = "Form1";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Form1";
      this.panel1.ResumeLayout(false);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private ModbusTcpClient.ModbusTcp modbusTcp1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button btWriteSingleCoil;
    private System.Windows.Forms.Button btStopCyclic;
    private System.Windows.Forms.Button btStartCyclic;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private GlacialComponents.Controls.GlacialList glacialList1;
    private GlacialComponents.Controls.GlacialList glacialList2;
  }
}

