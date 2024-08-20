using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusTcpClient
{
  public partial class ModbusTcp
  {
    private DateTime _start_datetime = DateTime.Now;
    private Queue<plc_data> _write_datas = new Queue<plc_data>();

    public void WriteSingleRegister(string memory_type, int address, int value)
    {
      int register_address = address;// _supportPLC.GetModbusAddress(address);
      WriteSingleRegister(register_address, value);
    }


    public void WriteSingleRegister(int startAddress, int value)
    {
      byte[] values = new byte[2];
      values[0] = Convert.ToByte((value << 0x08) & 0xFF);
      values[1] = Convert.ToByte((value & 0xFF));
      //
      byte[] data;
      int register_address = _supportPLC.GetModbusAddress(startAddress);
      this._save_memory_address = startAddress; //save it
      //
      data = CreateWriteHeader(this._id, this._unit, (ushort)register_address, 1, 1, fctWriteSingleRegister);
      data[10] = values[0];
      data[11] = values[1];
      //

      SendMessageToServer(data);
    }

    
    private void AddToListWriteData(int address, int value)
    {
      _write_datas.Enqueue(new plc_data(address, value));
    }
    private plc_data GetWriteBufferTopItem()
    {
      plc_data rp = _write_datas.Dequeue();
      return rp;
    }
    private bool checkWriteBufferHaveData()
    {
      return (_write_datas.Count > 0);
    }

    public void Write(int start_address_as_int, int value)
    {
      _start_datetime = DateTime.Now;
      AddToListWriteData(start_address_as_int, value);
    }

    
  }
}
