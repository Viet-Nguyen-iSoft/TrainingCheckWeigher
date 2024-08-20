using ModbusTcpClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GlacialComponents.Controls;

namespace ModbusTcpClientTester
{
  public partial class Form1 : Form
  {
    private const string IP = "192.168.3.124"; //PLC của Dũng
    private const int startAddress = 1000; //D1000
    private const int numberOfWords = 50;

    public Form1()
    {
      InitializeComponent();
      //
      this.Load += Form1_Load;
      this.FormClosing += Form1_FormClosing;
      this.modbusTcp1.OnNotifyStatus += ModbusTcp1_OnNotifyStatus;
      this.modbusTcp1.OnReadDeviceData += ModbusTcp1_OnReadDeviceData;
      this.modbusTcp1.OnReadData += ModbusTcp1_OnReadData;
      //
      this.btStartCyclic.Click += new System.EventHandler(this.btStartCyclic_Click);
      this.btStopCyclic.Click += new System.EventHandler(this.btStopCyclic_Click);
      this.btWriteSingleCoil.Click += new System.EventHandler(this.button1_Click);
    }

   
    private void ModbusTcp1_OnReadData(object ent, ModbusTcp.plc_data[] plcdatas, string IPAddress, int station_id)
    {
      //foreach(ModbusTcp.plc_data plc_data in plcdatas)
      //{
      //  Console.WriteLine($"D[{plc_data.address}]:{plc_data.value}");
      //}
      int idx_2000 = plcdatas.ToList().FindIndex(x => x.address == 2000);
      int idx_2001 = plcdatas.ToList().FindIndex(x => x.address == 2001);

      ModbusTcp.plc_data plcdata_2000 = plcdatas.ToList().Find(x => x.address == 2000);
      ModbusTcp.plc_data plcdata_2001 = plcdatas.ToList().Find(x => x.address == 2001);
      if ((idx_2000 >= 0) && (idx_2001 >= 0))
      {
        int value = (plcdata_2001.value << 16) | (plcdata_2000.value);
        byte[] bytes = BitConverter.GetBytes(value);
        float aa = BitConverter.ToSingle(bytes, 0);
        int mm = 0;
      }

      for (int i = 0; i < numberOfWords; i++)
      {
        int address = startAddress + i;
        int idx = plcdatas.ToList().FindIndex(x => x.address == address);
        if (idx >= 0)
        {
          ModbusTcp.plc_data plc_data = plcdatas[idx];
          if (idx < numberOfWords / 2)
          {
            this.glacialList1.Items[idx].SubItems[1].Text = plc_data.value.ToString();
          }
          else
          {
            this.glacialList2.Items[idx - (numberOfWords / 2)].SubItems[1].Text = plc_data.value.ToString();
          }
        }
      }
    }

    private void ModbusTcp1_OnReadDeviceData(object ent, byte[] data, int length, string IPAddress)
    {
      //string str = String.Format("{0}", BitConverter.ToString(data).Replace("-", " "));
      //Console.WriteLine($"Receive: {str}");
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.modbusTcp1.DeInit();
    }

    private void ModbusTcp1_OnNotifyStatus(object ent, ModbusTcpClient.ModbusTcp.STATUS status, string IPAddress)
    {
      
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      this.modbusTcp1.SetStartAddressReadCyclic(startAddress, numberOfWords);
      this.modbusTcp1.Init(1, 0, IP, "502", eSupportPlc.Fx5U);
      //
      for(int i = 0; i < numberOfWords; i++)
      {
        GLItem gLItem = new GLItem();
        gLItem.SubItems[0].Text = $"D{(startAddress + i).ToString().PadLeft(4, '0')}";
        gLItem.SubItems[1].Text = "";
        //
        if (i < numberOfWords / 2)
        {
          this.glacialList1.Items.Add(gLItem);
        }
        else
        {
          this.glacialList2.Items.Add(gLItem);
        }
      }
    }

    private void btStartCyclic_Click(object sender, EventArgs e)
    {
      
      this.modbusTcp1.StartCyclic();
    }

    private void btStopCyclic_Click(object sender, EventArgs e)
    {
      this.modbusTcp1.StopCyclic();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      this.modbusTcp1.Write(1, 10);
    }

    //

  }
}
