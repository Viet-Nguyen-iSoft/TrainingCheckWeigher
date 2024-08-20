using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusTcpClient
{
  public partial class ModbusTcp
  {
    public void ReadHoldingRegister(int startAddress, ushort numInputs)
    {
      int register_address = _supportPLC.GetModbusAddress(startAddress);
      this._save_memory_address = startAddress; //save it
      //
      byte[] data = CreateReadHeader(this._id, this._unit, register_address, numInputs, fctReadHoldingRegister);
      SendMessageToServer(data);
    }

    

  }
}
