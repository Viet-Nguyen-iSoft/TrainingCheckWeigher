using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusTcpClient
{
  public partial class ModbusTcp
  {    
    private delegate void OnCommunicationsWithServerDelegateServer(byte[] tcpAsyClBuffer, int length);
    private void OnCommunicationsWithServer(byte[] tcpAsyClBuffer, int length)
    {
      if (InvokeRequired)
      {
        this.Invoke(new OnCommunicationsWithServerDelegateServer(OnCommunicationsWithServer), new object[] { tcpAsyClBuffer, length });
        return;
      }
      //Console.WriteLine($"Receive data: {length}");

      
      //
      if (length > 8)
      {
        byte[] tmp_bytes = new byte[length];
        Array.Copy(tcpAsyClBuffer, tmp_bytes, length);
        //
        ushort id = SwapUInt16(BitConverter.ToUInt16(tmp_bytes, 0));
        byte unit = tmp_bytes[6];
        byte function = tmp_bytes[7];
        // byte[] data;
        //int[] ModbusRegisterWords = CovertToWord(tcpAsyClBuffer, );
        if ((function >= fctWriteSingleCoil) && (function != fctReadWriteMultipleRegister))
        {
          int error_code = (int)function - 0x80;

          if (error_code == 1) OnException(this.IPAddress, 1, "Illegal Function- Function code received in the query is not recognized or allowed by server");
          if (error_code == 1) OnException(this.IPAddress, 2, "Illegal Data Address-Data address of some or all the required entities are not allowed or do not exist in server");
          if (error_code == 1) OnException(this.IPAddress, 3, "Illegal Data Value-Value is not accepted by server");
          if (error_code == 1) OnException(this.IPAddress, 4, "Server Device Failure-Unrecoverable error occurred while server was attempting to perform requested action");
          if (error_code == 1) OnException(this.IPAddress, 5, "Acknowledge-Server has accepted request and is processing it, but a long duration of time is required.This response is returned to prevent a timeout error from occurring in the client.client can next issue a Poll Program Complete message to determine whether processing is completed");
          if (error_code == 1) OnException(this.IPAddress, 6, "Server Device Busy-Server is engaged in processing a long-duration command.client should retry later");
          if (error_code == 1) OnException(this.IPAddress, 7, "Negative Acknowledge-Server cannot perform the programming functions. Client should request diagnostic or error information from server");
          if (error_code == 1) OnException(this.IPAddress, 8, "Memory Parity Error-Server detected a parity error in memory.Client can retry the request, but service may be required on the server device");
          if (error_code == 1) OnException(this.IPAddress, 10, "Gateway Path Unavailable Specialized for Modbus gateways. Indicates a misconfigured gateway");
          if (error_code == 1) OnException(this.IPAddress, 11, "Gateway Target Device Failed to Respond Specialized for Modbus gateways. Sent when server fails to respond");
        }
        else
        {
          List<plc_data> list_plc_data = new List<plc_data>();
          int data_length = length - 8 - 1;
          if (data_length > 0)
          {
            byte[] data_bytes = new byte[data_length];
            Array.Copy(tmp_bytes, 9, data_bytes, 0, data_length);
            //
            if (OnReadDeviceData != null)
            {
              OnReadDeviceData(this, data_bytes, data_length, this.IPAddress);
            }


            //
            int idx = 0;
            for (int i = 0; (i < data_length - 1); i += 2)
            {
              int value = data_bytes[i] << 8 | data_bytes[i + 1];
              //
              list_plc_data.Add(new plc_data(this._save_memory_address + idx++, value));
            }
            if (OnReadData != null)
            {
              OnReadData(this, list_plc_data.ToArray(), this.IPAddress, this._station_id);
            }
            //
          }
        }
      }
    }

    private int[] CovertToWord(byte[] data_from_slave, int Length)
    {
      // int nlength = Length / 2;
      List<int> list_word = new List<int>();
      for (int i = 0; i < Length; i += 2)
      {
        list_word.Add((data_from_slave[i] << 8) | data_from_slave[i + 1]);
      }
      return list_word.ToArray();
    }


    private delegate void DisplayStatusDelegate(bool bConnected);
    private void DisplayStatus(bool bConnected)
    {
      if (InvokeRequired)
      {
        this.Invoke(new DisplayStatusDelegate(DisplayStatus), new object[] { bConnected });
        return;
      }

      this.lblIpAdress.Text = this.IPAddress;
      this.lblIpAdress.BackColor = bConnected ? Color.Green : Color.Red;
      this.lblIpAdress.ForeColor = bConnected ? Color.White : Color.Black;
      //
      this.lblControllerStatus.Text = bConnected ? "Connected": "Disconnected";
      STATUS status = bConnected ? STATUS.INIT_OK : STATUS.INIT_FAILED;

      IsEthConnectDevice = bConnected ? true : false;
      OnNotifyStatus_Server1(this, status);
    }

    private delegate void OnNotifyDelegate_Server1(object sender, STATUS status);
    private void OnNotifyStatus_Server1(object sender, STATUS status)
    {
      if (InvokeRequired)
      {
        this.Invoke(new OnNotifyDelegate_Server1(OnNotifyStatus_Server1), new object[] { sender, status });
        return;
      }
      if (OnNotifyStatus != null)
      {
        OnNotifyStatus(this, status, this.IPAddress);
      }
    }


    private delegate void OnExceptionDelegate(string IPAddess, int error_code, string description);
    private void OnException(string IPAddess, int error_code, string description)
    {
      if (InvokeRequired)
      {
        this.Invoke(new OnExceptionDelegate(OnException), new object[] { IPAddess, error_code, description });
        return;
      }
      if (OnNotifyException != null)
      {
        OnNotifyException(IPAddess, error_code, description);
      }
    }




  }
}
