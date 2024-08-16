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

namespace TestPLC
{
  public partial class Form1 : Form
  {
    private Block _curBlock = Block.Block_start;
    public Form1()
    {
      InitializeComponent();
      this.Load += Form1_Load;
      //đăng ký nhận event
      tcpCommUC1.OnNotifyStatus += TcpCommUC1_OnNotifyStatus;
      tcpCommUC1.OnReadDeviceData += TcpCommUC1_OnReadDeviceData;
    }

    private void TcpCommUC1_OnReadDeviceData(object ent, int index, List<TcpComm.FX_DATA> list_data, bool IsCorrectChecksum)
    {
      int start_address = 1000 + ((int)(_curBlock) * 50);
      //------1. Process data

      //---2. Next cycle, next boxk
      if (_curBlock == Block.Block_start)
      {        
        _curBlock = Block.Block_1;
      }
      else if (_curBlock == Block.Block_1)
      {
       _curBlock = Block.Block_2;
      }
      else if (_curBlock == Block.Block_2)
      {
        _curBlock = Block.Block_end;
      }
      else if (_curBlock == Block.Block_end)
      {
        _curBlock = Block.Block_start;
      }
      //send
      this.tcpCommUC1.Read_DeviceMemory($"D{start_address}", 50, TcpComm.PROTOCOL_UNIT._x1_WORD);
    }

    private void TcpCommUC1_OnReadData(object ent, byte[] data_from_plc, bool IsMessageIdOK, bool IsCorrectCRC)
    {
      throw new NotImplementedException();
    }

    private void TcpCommUC1_OnNotifyStatus(object ent, TcpComm.STATUS status)
    {
      if ((status == TcpComm.STATUS.OK) ||
         (status == TcpComm.STATUS.INIT_OK) ||
        (status == TcpComm.STATUS.READ_DATA_OK) ||
        (status == TcpComm.STATUS.WRITE_DATA_OK))
      {
        this.panel_status.BackColor = Color.Green;
      }
      else
      {
        this.panel_status.BackColor = Color.Red;
      }
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      this.tcpCommUC1.Init("192.168.3.124", 5000, TcpComm.ETHERNET_PROTOCOL.SLMP_BINARY_CODES);
    }

    private void btTestRead_Click(object sender, EventArgs e)
    {
      if (this.tcpCommUC1.IsEthConnected)
      {
        _curBlock = Block.Block_start;
        int start_address = 1000 + ((int)(_curBlock) * 50);
        this.tcpCommUC1.Read_DeviceMemory($"D{start_address}", 50, TcpComm.PROTOCOL_UNIT._x1_WORD);
      }
    }


    private enum Block
    {
      Block_start = 0,
      Block_1 = 1,
      Block_2,
      Block_end,
    }
  }
}
