using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
namespace ModbusTcpClient
{
  public partial class ModbusTcp
  {// ------------------------------------------------------------------------
    // Constants for access
    private const byte fctReadCoil = 1;
    private const byte fctReadDiscreteInputs = 2;
    private const byte fctReadHoldingRegister = 3;
    private const byte fctReadInputRegister = 4;
    private const byte fctWriteSingleCoil = 5;
    private const byte fctWriteSingleRegister = 6;
    private const byte fctWriteMultipleCoils = 15;
    private const byte fctWriteMultipleRegister = 16;
    private const byte fctReadWriteMultipleRegister = 23;

    /// <summary>Constant for exception illegal function.</summary>
    public const byte excIllegalFunction = 1;
    /// <summary>Constant for exception illegal data address.</summary>
    public const byte excIllegalDataAdr = 2;
    /// <summary>Constant for exception illegal data value.</summary>
    public const byte excIllegalDataVal = 3;
    /// <summary>Constant for exception slave device failure.</summary>
    public const byte excSlaveDeviceFailure = 4;
    /// <summary>Constant for exception acknowledge.</summary>
    public const byte excAck = 5;
    /// <summary>Constant for exception slave is busy/booting up.</summary>
    public const byte excSlaveIsBusy = 6;
    /// <summary>Constant for exception gate path unavailable.</summary>
    public const byte excGatePathUnavailable = 10;
    /// <summary>Constant for exception not connected.</summary>
    public const byte excExceptionNotConnected = 253;
    /// <summary>Constant for exception connection lost.</summary>
    public const byte excExceptionConnectionLost = 254;
    /// <summary>Constant for exception response timeout.</summary>
    public const byte excExceptionTimeout = 255;
    /// <summary>Constant for exception wrong offset.</summary>
    private const byte excExceptionOffset = 128;
    /// <summary>Constant for exception send failt.</summary>
    private const byte excSendFailt = 100;



    private byte[] CreateReadHeader(ushort id, byte unit, int startAddress, ushort length, byte function)
    {
      byte[] data = new byte[12];

      byte[] _id = BitConverter.GetBytes((short)id);
      data[0] = _id[1];			    // Slave id high byte
      data[1] = _id[0];				// Slave id low byte
      data[5] = 6;					// Message size
      data[6] = unit;					// Slave address
      data[7] = function;				// Function code
      byte[] _adr = BitConverter.GetBytes((short)System.Net.IPAddress.HostToNetworkOrder((short)startAddress));
      data[8] = _adr[0];				// Start address
      data[9] = _adr[1];				// Start address
      byte[] _length = BitConverter.GetBytes((short)System.Net.IPAddress.HostToNetworkOrder((short)length));
      data[10] = _length[0];			// Number of data to read
      data[11] = _length[1];			// Number of data to read
      return data;
    }


    private byte[] CreateWriteHeader(ushort id, byte unit, ushort startAddress, ushort numData, ushort numBytes, byte function)
    {
      byte[] data = new byte[numBytes + 11];

      byte[] _id = BitConverter.GetBytes((short)id);
      data[0] = _id[1];				// Slave id high byte
      data[1] = _id[0];				// Slave id low byte
      data[2] = 0; //checksum
      data[3] = 0; //checksum
      byte[] _size = BitConverter.GetBytes((short)System.Net.IPAddress.HostToNetworkOrder((short)(5 + numBytes)));
      data[4] = _size[0];				// Complete message size in bytes
      data[5] = _size[1];				// Complete message size in bytes
      data[6] = unit;					// Slave address
      data[7] = function;				// Function code
      byte[] _adr = BitConverter.GetBytes((short)System.Net.IPAddress.HostToNetworkOrder((short)startAddress));
      data[8] = _adr[0];				// Start address
      data[9] = _adr[1];				// Start address
      if (function >= fctWriteMultipleCoils)
      {
        byte[] _cnt = BitConverter.GetBytes((short)System.Net.IPAddress.HostToNetworkOrder((short)numData));
        data[10] = _cnt[0];			// Number of bytes
        data[11] = _cnt[1];			// Number of bytes
        data[12] = (byte)(numBytes - 2);
      }
      return data;
    }




    internal static UInt16 SwapUInt16(UInt16 inValue)
    {
      return (UInt16)(((inValue & 0xff00) >> 8) |
               ((inValue & 0x00ff) << 8));
    }

    public enum plc_data_type
    {
      WORD,
      DWORD,
    }

    

    public struct plc_data
    {
      public int address;
      public int value;
      public plc_data_type _plc_data_type;
      public object Tag;
      public plc_data(int address, plc_data_type plc_data_type)
      {
        this.address = address;
        this.value = -1;
        this._plc_data_type = plc_data_type;
        this.Tag = null;
      }

      public plc_data(int address, plc_data_type plc_data_type, object tag)
      {
        this.address = address;
        this.value = -1;
        this._plc_data_type = plc_data_type;
        this.Tag = tag;
      }

      public plc_data(int address, int value)
      {
        this.address = address;
        this.value = value;
        this._plc_data_type =  plc_data_type.WORD;
        this.Tag = null;
      }
    }


    
  }
}
