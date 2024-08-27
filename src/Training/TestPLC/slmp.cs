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
      //Kiểm tra buffers write có gì cần Write hay ko?
      bool is_found_write_data = false;
      int id_found = (-1);
      WriteRequest writeRequest = null;
      for (int i = 0; (i < list_WriteRequests.Count) && (is_found_write_data == false); i++)
      {
        if (list_WriteRequests[i].eWriteRequest != eWriteRequest.Donothing)
        {
          id_found = list_WriteRequests[i].Id;
          writeRequest = list_WriteRequests[i];
          is_found_write_data = true;
        }
      }/*for (int i = 0; i < list_WriteRequests.Count; i++)*/

      // is_found_write_data = list_WriteRequests.Exists(x => x.eWriteRequest != eWriteRequest.Donothing);

      if (is_found_write_data == true)
      {
        //start write
        if (writeRequest != null)
        {
          if (writeRequest.eWriteRequest == eWriteRequest.ResetCounter)
          {
            int[] decimal_values = new int[1]{0};
            int length = 1;
            this.slmpCommUC1.Write_DeviceMemory($"D100", decimal_values, length);
          }
          else if (writeRequest.eWriteRequest == eWriteRequest.AssignLoss)
          {
            int[] decimal_values = new int[2] { 12,34 };
            int length = 2;
            this.slmpCommUC1.Write_DeviceMemory($"D200", decimal_values, length);
          }
          else if (writeRequest.eWriteRequest == eWriteRequest.Do_action_1)
          {
          }
          else if (writeRequest.eWriteRequest == eWriteRequest.Do_action_2)
          {
          }
        }/*if (writeRequest != null)*/



        //-- Write done
        //----> Clear write request
        bool is_exit = false;
        for (int i = 0; (i < list_WriteRequests.Count) && (is_exit == false); i++)
        {
          if (list_WriteRequests[i].Id == id_found)
          {
            list_WriteRequests[i].eWriteRequest = eWriteRequest.Donothing;
            is_exit = true;
          }
        }/*for (int i = 0; i < list_WriteRequests.Count; i++)*/


        //Active next cycle
        TcpCommUC1_OnReadDeviceData(this, 0, new List<TcpComm.FX_DATA>(), false);
      }
      else
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
        //read data
        this.slmpCommUC1.Read_DeviceMemory($"D{start_address}", 50, TcpComm.PROTOCOL_UNIT._x1_WORD);
      }
    }

  }
}
