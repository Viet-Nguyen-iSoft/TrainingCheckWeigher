using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TcpComm.TcpCommUC;
using ModbusTcpClient;
namespace TestPLC
{
  public partial class Form1 : Form
  {
    const string IP = "192.168.3.124";
    private const int startAddress = 1000;
    private const int numberOfWords = 50;

    private Block _curBlock_Slmp = Block.Block_start;
    private Block _curBlock_modbusTcp = Block.Block_start;
    public Form1()
    {
      InitializeComponent();
      this.Load += Form1_Load;
      this.FormClosing += Form1_FormClosing;
      //đăng ký nhận event
      slmpCommUC1.OnNotifyStatus += TcpCommUC1_OnNotifyStatus;
      slmpCommUC1.OnReadDeviceData += TcpCommUC1_OnReadDeviceData;
      //
      this.modbusTcp1.OnNotifyStatus += ModbusTcp1_OnNotifyStatus;
      this.modbusTcp1.OnReadDeviceData += ModbusTcp1_OnReadDeviceData; ;
      this.modbusTcp1.OnReadData += ModbusTcp1_OnReadData;
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.modbusTcp1.DeInit();
      this.slmpCommUC1.DeInit();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      this.slmpCommUC1.Init(IP, 5000, TcpComm.ETHERNET_PROTOCOL.SLMP_BINARY_CODES);

      //---- MobusTcp init
      this.modbusTcp1.SetStartAddressReadCyclic(startAddress, numberOfWords);
      this.modbusTcp1.StopCyclic();
      this.modbusTcp1.Init(1, 0, IP, "502", eSupportPlc.Fx5U);
     

    }

    private void btTestRead_Click(object sender, EventArgs e)
    {
      if (this.slmpCommUC1.IsEthConnected)
      {
        _curBlock_Slmp = Block.Block_start;
        int start_address = 1000 + ((int)(_curBlock_Slmp) * 50);
        this.slmpCommUC1.Read_DeviceMemory($"D{start_address}", 50, TcpComm.PROTOCOL_UNIT._x1_WORD);
      }
      //---------------
      if (this.modbusTcp1.IsEthConnected)
      {
        this._curBlock_modbusTcp = Block.Block_start;
        int start_address = 1000 + ((int)(_curBlock_Slmp) * numberOfWords);
        this.modbusTcp1.ReadHoldingRegister(start_address, numberOfWords);
      }
    }


    private enum Block
    {
      Block_start = 0,
      Block_1 = 1,
      Block_2,
      Block_end,
    }

    private void button1_Click(object sender, EventArgs e)
    {
      
    }
  }
}
