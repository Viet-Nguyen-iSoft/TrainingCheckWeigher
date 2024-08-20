using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestPLC
{
  public partial class Form1 : Form
  {
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

    private void TcpCommUC1_OnReadDeviceData(object ent, int index, List<TcpComm.FX_DATA> list_data, bool IsCorrectChecksum)
    {
      int start_address = 1000 + ((int)(_curBlock_Slmp) * 50);
      //------1. Process data

      //---2. Next cycle, next boxk
      if (_curBlock_Slmp == Block.Block_start)
      {
        _curBlock_Slmp = Block.Block_1;
      }
      else if (_curBlock_Slmp == Block.Block_1)
      {
        _curBlock_Slmp = Block.Block_2;
      }
      else if (_curBlock_Slmp == Block.Block_2)
      {
        _curBlock_Slmp = Block.Block_end;
      }
      else if (_curBlock_Slmp == Block.Block_end)
      {
        _curBlock_Slmp = Block.Block_start;
      }
      //send
      this.slmpCommUC1.Read_DeviceMemory($"D{start_address}", 50, TcpComm.PROTOCOL_UNIT._x1_WORD);
    }

  }
}
