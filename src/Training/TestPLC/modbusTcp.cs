using ModbusTcpClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestPLC
{
  public partial class Form1 : Form
  {
    private void ModbusTcp1_OnNotifyStatus(object ent, ModbusTcp.STATUS status, string IPAddress)
    {
      
    }
    private void ModbusTcp1_OnReadDeviceData(object ent, byte[] data, int length, string IPAddress)
    {
      
    }
    private void ModbusTcp1_OnReadData(object ent, ModbusTcp.plc_data[] plcdatas, string IPAddress, int station_id)
    {
      int start_address = 1000 + ((int)(_curBlock_modbusTcp) * 50);
      //------1. Process data

      //---2. Next cycle, next boxk
      if (_curBlock_modbusTcp == Block.Block_start)
      {
        _curBlock_modbusTcp = Block.Block_1;
      }
      else if (_curBlock_modbusTcp == Block.Block_1)
      {
        _curBlock_modbusTcp = Block.Block_2;
      }
      else if (_curBlock_modbusTcp == Block.Block_2)
      {
        _curBlock_modbusTcp = Block.Block_end;
      }
      else if (_curBlock_modbusTcp == Block.Block_end)
      {
        _curBlock_modbusTcp = Block.Block_start;
      }
      //send
      this.modbusTcp1.ReadHoldingRegister(start_address, numberOfWords);
    }

  }
}
