using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusTcpClient
{
  public partial class ModbusTcp
  {
    private bool _isStartReadCyclic = true;

   // private int 
    
    private SupportPLC _supportPLC = new SupportPLC();
    //
    public void StartReadCyclic()
    {
      _isStartReadCyclic = true;
    }
    public void StopReadCyclic()
    {
      _isStartReadCyclic = false;
    }

    private void ActiveReadCyclicByTimer()
    {
      if (ServerConnected_Server1 == true)
      {
        if (checkWriteBufferHaveData() == true)
        {
          try
          {
            plc_data plc_Data = GetWriteBufferTopItem();
            WriteSingleRegister(plc_Data.address, plc_Data.value);
          }
          catch
          {
          }
        }
        else
        {
          
          /* Check if we have some thing to write ?*/
          if (_isStartReadCyclic == true)
          {
            if (_eClientRequest_Server1 == eClientRequest.GET_CLIENT_DATA_1)
            {
              ReadHoldingRegister(this._start_memory_address_in_cyclic, (ushort)this._number_of_words_in_cyclic);
            }
            else if (_eClientRequest_Server1 == eClientRequest.GET_CLIENT_DATA_2)
            {
              ReadHoldingRegister(this._start_memory_address_in_cyclic, (ushort)this._number_of_words_in_cyclic);
            }
            else if (_eClientRequest_Server1 == eClientRequest.GET_CLIENT_DATA_3)
            {
              ReadHoldingRegister(this._start_memory_address_in_cyclic, (ushort)this._number_of_words_in_cyclic);
            }
            else if (_eClientRequest_Server1 == eClientRequest.GET_CLIENT_DATA_4)
            {
              ReadHoldingRegister(this._start_memory_address_in_cyclic, (ushort)this._number_of_words_in_cyclic);
            }
            else if (_eClientRequest_Server1 == eClientRequest.GET_CLIENT_DATA_5)
            {
              ReadHoldingRegister(this._start_memory_address_in_cyclic, (ushort)this._number_of_words_in_cyclic);
            }
            else if (_eClientRequest_Server1 == eClientRequest.GET_CLIENT_DATA_6)
            {

              ReadHoldingRegister(this._start_memory_address_in_cyclic, (ushort)this._number_of_words_in_cyclic);
            }
            else if (_eClientRequest_Server1 == eClientRequest.GET_CLIENT_DATA_7)
            {
              ReadHoldingRegister(this._start_memory_address_in_cyclic, (ushort)this._number_of_words_in_cyclic);
            }
            else if (_eClientRequest_Server1 == eClientRequest.GET_CLIENT_DATA_8)
            {
              ReadHoldingRegister(this._start_memory_address_in_cyclic, (ushort)this._number_of_words_in_cyclic);
            }
            else if (_eClientRequest_Server1 == eClientRequest.GET_CLIENT_DATA_9)
            {
              ReadHoldingRegister(this._start_memory_address_in_cyclic, (ushort)this._number_of_words_in_cyclic);
            }
            else if (_eClientRequest_Server1 == eClientRequest.GET_CLIENT_DATA_10)
            {
              ReadHoldingRegister(this._start_memory_address_in_cyclic, (ushort)this._number_of_words_in_cyclic);
            }
          }
        }/*if (checkWriteBufferHaveData() == true)*/
      }
    }

  }
}
