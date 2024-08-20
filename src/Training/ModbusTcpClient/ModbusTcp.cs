#define SUPPORT_BINARY
#define USE_WRITEN_VAR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using CommonClassLibs;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace ModbusTcpClient
{
  public partial class ModbusTcp : UserControl
  {
    public enum STATUS
    {
      INIT_OK = 0x01,
      INIT_FAILED = 0x02,
      //
      READ_DATA_OK = 0x06,
      READ_DATA_FAILED = 0x07,
      //
      WRITE_DATA_OK = 0x08,
      WRITE_DATA_OK_FAILED,

      WRONG_FORMAT
    }   

    public delegate void NotifyStatus(object ent, STATUS status, string IPAddress);
    public event NotifyStatus OnNotifyStatus;

    public delegate void NotifyException(string IPAddess, int error_code, string description);
    public event NotifyException OnNotifyException;


    public delegate void ReadDeviceData(object ent, byte[] data, int length, string IPAddress);
    public event ReadDeviceData OnReadDeviceData;

    public delegate void ReadData(object ent, plc_data[] plcdatas, string IPAddress, int station_id);
    public event ReadData OnReadData;
    //plc_data[] plcdatas, string Comport


    #region Variables
    private int _station_id = 0;
    private ushort _id = 1;
    private byte _unit = 0;
    private string IPAddress = "192.168.0.230";
    private string Port = "502";
    //
    private int _save_memory_address = 0;
    private ushort MAX_WORDs = 50;
    //
    private eSupportPlc eSupportPlc = eSupportPlc.DeltaDVP12SE;

    //for cyclic
    private int _start_memory_address_in_cyclic = 0;
    private int _number_of_words_in_cyclic = 1;


    /// <summary> 
    /// Flag to save connect status: TRUE: OK; FALSE: FAIL.
    /// </summary> 
    private bool IsEthConnectDevice = false;
    #endregion

    public bool IsEthConnected
    {
      get { return IsEthConnectDevice; }
    }
    public ModbusTcp()
    {
      InitializeComponent();
      //this.Load += SLMPProtocol_Load;
      this.backgroundWorker_Server1 = new System.ComponentModel.BackgroundWorker();
      this.backgroundWorker_Server1.WorkerReportsProgress = true;
      this.backgroundWorker_Server1.WorkerSupportsCancellation = true;
      this.backgroundWorker_Server1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerDoWork_Server1);
      this.backgroundWorker_Server1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged_Server1);
      this.backgroundWorker_Server1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted_Server1);
      
      //
      this.timer_restart_tcp_comm_Server1 = new System.Windows.Forms.Timer();
      this.timer_restart_tcp_comm_Server1.Interval = 200;
      this.timer_restart_tcp_comm_Server1.Enabled = false;
      this.timer_restart_tcp_comm_Server1.Tick += new System.EventHandler(this.timer_restart_tcp_comm_Tick_Server1);
      

    }
    /// <summary>
    /// Min is 100ms
    /// </summary>
    /// <param name="value"></param>
    public void SetTimerCycicInterval(int value)
    {
      if (value > 100)
      {
        this.timer_restart_tcp_comm_Server1.Interval = value;
      }
    }

    public void Init(ushort id, byte unit, string IPAddress, string port, eSupportPlc _eSupportPlc = eSupportPlc.GenericDevice, int station_id = 0)
    {
      this._station_id = station_id;
      this._id = id;
      this._unit = unit;

      this.IPAddress = IPAddress;
      this.Port = port;
      this.eSupportPlc = _eSupportPlc;
      //
      _eClientRequest_Server1 = eClientRequest.CONNECT_CLIENT;
      this.timer_restart_tcp_comm_Server1.Enabled = true;
      //
      if (_eSupportPlc == eSupportPlc.DeltaDVP12SE)
      {
        _supportPLC = new DeltaDVP12SE();
      }
    }



    public void DeInit()
    {
      this.timer_restart_tcp_comm_Server1.Enabled = false;
      if (this.backgroundWorker_Server1.IsBusy == true)
      {
        this.backgroundWorker_Server1.CancelAsync();
      }

      StartToDisconnect_Server1();
      AppIsExiting_Server1 = true;
    }



    public bool GetConnectStatus()
    {
      return ((client_to_Server1 == null)?false: client_to_Server1.Connected);
    }

    public void SetStartAddressReadCyclic(int startAddress, int length)
    {
      this._start_memory_address_in_cyclic = startAddress;
      this._number_of_words_in_cyclic = length;
    }
    public void StartCyclic()
    {
      this._isStartReadCyclic = true;
    }

    public void StopCyclic()
    {
      this._isStartReadCyclic = false;
    }



  }
}
