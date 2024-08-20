#define TEST_COMM
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CommonClassLibs;

namespace ModbusTcpClient
{
  public partial class ModbusTcp
  {
    private enum eClientRequest
    {
      DO_NOTHING,
      CONNECT_CLIENT,
      GET_CLIENT_DATA_1,
      GET_CLIENT_DATA_2,
      GET_CLIENT_DATA_3,
      GET_CLIENT_DATA_4,
      GET_CLIENT_DATA_5,
      GET_CLIENT_DATA_6,
      GET_CLIENT_DATA_7,
      GET_CLIENT_DATA_8,
      GET_CLIENT_DATA_9,
      GET_CLIENT_DATA_10,
      DISCONNECT_CLIENT
    }

    //
    private System.Windows.Forms.Timer timer_restart_tcp_comm_Server1;
    private System.Windows.Forms.Timer GeneralTimer_Server1 = null;

    private System.ComponentModel.BackgroundWorker backgroundWorker_Server1;
    private eClientRequest _eClientRequest_Server1 = eClientRequest.CONNECT_CLIENT;

    //private int _lineId = 0;
   



    /*******************************************************/
    private Client client_to_Server1 = null;//Client Socket class

    private MotherOfRawPackets HostServerRawPackets_Server1 = null;

    private AutoResetEvent autoEventHost_Server1 = null;//mutex
    private AutoResetEvent autoEvent2_Server1;//mutex

    private bool IsEnable_DataProcessHostServerThread = true; //chang this value here


    private Thread DataProcessHostServerThread = null;


    //private Thread FullPacketDataProcessThread_Server1 = null;
    //private Queue<FullPacket> FullHostServerPacketList_Server1 = null;
    /*******************************************************/

    private bool AppIsExiting_Server1 = false;
    private bool ServerConnected_Server1 = false;
    private int MyHostServerID_Server1 = 0;
    private long ServerTime_Server1 = DateTime.Now.Ticks;
    private int nCountLossData = 0;
    private bool ImDisconnecting = false;

   // private List<Error> list_error = new List<Error>();

    private int _tcpComIndex = 0;
    public void SetIndex(int index)
    {
      _tcpComIndex = index;
    }

    


    private void StartToConnect()
    {
      ServerConnected_Server1 = true;//Set this before initializing the connection loops
      InitializeServerConnection_Server1();
      if (ConnectToHost_Server1())
      {
        //
        Console.WriteLine(String.Format("StartToConnect: server1 Connected {0}", this.IPAddress));
        //
        
        ServerConnected_Server1 = true;
        DisplayStatus(true);
        _eClientRequest_Server1 = eClientRequest.GET_CLIENT_DATA_1;
      }
      else
      {
        ServerConnected_Server1 = false;
        DisplayStatus(false);
        Console.WriteLine(String.Format("Can't connect to server1 {0}", this.IPAddress));
      }
    }

    private void StartToDisconnect_Server1()
    {
      //TellServerImDisconnecting_Server1();
      DoServerDisconnect();
      ServerConnected_Server1 = false;
    }

    //private byte[] ConvertToByteArray(string message)
    //{
    //  byte[] byData = new byte[21] { 80, 0, 0, 255, 255, 3, 0, 12, 0, 16, 0, 1, 4, 0, 0, 127, 0, 0, 168, 50, 0 };
    //  return byData;

    //}



    private void backgroundWorkerDoWork_Server1(object sender, DoWorkEventArgs e)    
    {

      if (_eClientRequest_Server1 == eClientRequest.CONNECT_CLIENT)
      {
        StartToConnect();
      }
      ActiveReadCyclicByTimer();
    }

    private void timer_restart_tcp_comm_Tick_Server1(object sender, EventArgs e)
    {
      this.timer_restart_tcp_comm_Server1.Enabled = false;
      if (_eClientRequest_Server1 == eClientRequest.GET_CLIENT_DATA_1)
      {
        _eClientRequest_Server1 = eClientRequest.GET_CLIENT_DATA_2;
      }
      else if (_eClientRequest_Server1 == eClientRequest.GET_CLIENT_DATA_2)
      {
        _eClientRequest_Server1 = eClientRequest.GET_CLIENT_DATA_3;
      }
      else if (_eClientRequest_Server1 == eClientRequest.GET_CLIENT_DATA_3)
      {
        _eClientRequest_Server1 = eClientRequest.GET_CLIENT_DATA_4;
      }
      else if (_eClientRequest_Server1 == eClientRequest.GET_CLIENT_DATA_4)
      {
        _eClientRequest_Server1 = eClientRequest.GET_CLIENT_DATA_5;
      }
      else if (_eClientRequest_Server1 == eClientRequest.GET_CLIENT_DATA_5)
      {
        _eClientRequest_Server1 = eClientRequest.GET_CLIENT_DATA_6;
      }
      else if (_eClientRequest_Server1 == eClientRequest.GET_CLIENT_DATA_6)
      {
        _eClientRequest_Server1 = eClientRequest.GET_CLIENT_DATA_7;
      }
      else if (_eClientRequest_Server1 == eClientRequest.GET_CLIENT_DATA_7)
      {
        _eClientRequest_Server1 = eClientRequest.GET_CLIENT_DATA_8;
      }
      else if (_eClientRequest_Server1 == eClientRequest.GET_CLIENT_DATA_8)
      {
        _eClientRequest_Server1 = eClientRequest.GET_CLIENT_DATA_9;
      }
      else if (_eClientRequest_Server1 == eClientRequest.GET_CLIENT_DATA_9)
      {
        _eClientRequest_Server1 = eClientRequest.GET_CLIENT_DATA_10;
      }
      else if (_eClientRequest_Server1 == eClientRequest.GET_CLIENT_DATA_10)
      {
        _eClientRequest_Server1 = eClientRequest.GET_CLIENT_DATA_1;
        //_eClientRequest_Server1 = eClientRequest.DISCONNECT_CLIENT;//eClientRequest.GET_CLIENT_DATA_1;

      }
      //-------------------------------------------------------------
      if (ServerConnected_Server1 == false)
      {
        _eClientRequest_Server1 = eClientRequest.CONNECT_CLIENT;
      }
      //----------------------------------------------
      if ((this.backgroundWorker_Server1.IsBusy == false) && (ImDisconnecting == false))
      {
        this.backgroundWorker_Server1.RunWorkerAsync();
      }
    }

    private void backgroundWorker_ProgressChanged_Server1(object sender, ProgressChangedEventArgs e)
    {

    }

    private void backgroundWorker_RunWorkerCompleted_Server1(object sender, RunWorkerCompletedEventArgs e)
    {
      if (_eClientRequest_Server1 == eClientRequest.CONNECT_CLIENT)
      {
        _eClientRequest_Server1 = eClientRequest.GET_CLIENT_DATA_1;
      }

      this.timer_restart_tcp_comm_Server1.Enabled = true;
    }

    



    private bool ConnectToHost_Server1()
    {
      try
      {
        //pictureBox1.Image = imageListStatusLights.Images["PURPLE"];
        if (client_to_Server1 == null)
        {
          client_to_Server1 = new Client();
          client_to_Server1.OnDisconnected += OnDisconnect_Server1;
          client_to_Server1.OnReceiveData += OnDataReceive_Server1;
          client_to_Server1.OnDisconnectedAndNeedRetries += OnDisconnectedAndNeedRetries;
        }
        else
        {
          //if we get here then we already have a client object so see if we are already connected
          if (client_to_Server1.Connected)
            return true;
        }

        string szIPstr = GetSHubAddress_Server1();
        if (szIPstr.Length == 0)
        {
          //pictureBox1.Image = imageListStatusLights.Images["RED"];
          return false;
        }

        int port = 0;
        if (!Int32.TryParse(Port, out port))
          port = 9999;

        IPAddress ipAdd = System.Net.IPAddress.Parse(szIPstr);
        client_to_Server1.Connect(ipAdd, port);//(int)GeneralSettings.HostPort);
        if (client_to_Server1 != null)
        {
          if (client_to_Server1.Connected)
            return true;
          else
            return false;
        }

      }
      catch (Exception ex)
      {
        var exceptionMessage = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
        // Console.WriteLine($"EXCEPTION IN: ConnectToHostServer - {exceptionMessage}");
      }
      return false;
    }

    public void DoServerDisconnect()
    {
      int Line = 0;
      //-------------------------------------------------------      
      if (ImDisconnecting)
        return;

      ImDisconnecting = true;

      // Console.WriteLine("\nIN DoServerDisconnect\n");


      //---------------------------------------------
      try
      {
        if (InvokeRequired)
        {
          this.Invoke(new MethodInvoker(DoServerDisconnect));
          return;
        }

        //pictureBox1.Image = imageListStatusLights.Images["PURPLE"];

        int i = 0;
        Line = 1;


        if (client_to_Server1 != null)
        {
          //TellServerImDisconnecting_Server1();
          Thread.Sleep(75);// this is needed!


        }

        Line = 4;

        ServerConnected_Server1 = false;

        DestroyGeneralTimer_Server1();

        Line = 5;


        /***************************************************/
        try
        {
          //bust out of the data loops
          if (autoEventHost_Server1 != null)
          {
            autoEventHost_Server1.Set();

            i = 0;
            while (DataProcessHostServerThread.IsAlive)
            {
              Thread.Sleep(1);
              if (i++ > 200)
              {
                DataProcessHostServerThread.Abort();
                //Debug.WriteLine("\nHAD TO ABORT PACKET THREAD\n");
                break;
              }
            }

            //


            autoEventHost_Server1.Close();
            //autoEventHost_Server1.
            autoEventHost_Server1 = null;
          }
        }
        catch (Exception ex)
        {
          // Console.WriteLine($"DoServerDisconnectA: {ex.Message}");
        }

        Line = 8;
        if (autoEvent2_Server1 != null)
        {
          autoEvent2_Server1.Set();

          autoEvent2_Server1.Close();
          // autoEvent2_Server1.Dispose();
          autoEvent2_Server1 = null;
        }
        /***************************************************/

        Line = 9;
        //Debug.WriteLine("AppIsExiting = " + AppIsExiting.ToString());
        if (client_to_Server1 != null)
        {
          if (client_to_Server1.OnReceiveData != null)
            client_to_Server1.OnReceiveData -= OnDataReceive_Server1;
          if (client_to_Server1.OnDisconnected != null)
            client_to_Server1.OnDisconnected -= OnDisconnect_Server1;
          if (client_to_Server1.OnDisconnectedAndNeedRetries != null)//OnDisconnectedAndNeedRetries
          {
            client_to_Server1.OnDisconnectedAndNeedRetries -= OnDisconnectedAndNeedRetries;
          }

          client_to_Server1.Disconnect();
          client_to_Server1 = null;
        }

        Line = 10;

        try
        {
          Line = 13;
          //buttonConnect.Text = "Connect";
          //labelStatusInfo.Text = "NOT Connected";
          Line = 14;
          //labelStatusInfo.ForeColor = System.Drawing.Color.Red;
        }
        catch { }
        Line = 15;

        //buttonConnectToServer.Enabled = true;
        //pictureBox1.Image = imageListStatusLights.Images["RED"];

        //-------------------------------------------------------------------

      }
      catch (Exception ex)
      {
        // Console.WriteLine($"DoServerDisconnectB: {ex.Message}");
      }
      finally
      {
        ImDisconnecting = false;
      }



      return;
    }

    private void InitializeServerConnection_Server1()
    {
      try
      {
        /**** Packet processor mutex, loop and other support variables *************************/
        autoEventHost_Server1 = new AutoResetEvent(false);//the data mutex
        autoEvent2_Server1 = new AutoResetEvent(false);//the FullPacket data mutex
        //FullPacketDataProcessThread_Server1 = new Thread(new ThreadStart(ProcessRecievedServerData_Server1));
        DataProcessHostServerThread = new Thread(new ThreadStart(NormalizeServerRawPackets_Server1));


        if (HostServerRawPackets_Server1 == null)
        {
          HostServerRawPackets_Server1 = new MotherOfRawPackets(0);
        }
        else
        {
          HostServerRawPackets_Server1.ClearList();
        }
        //


        //if (FullHostServerPacketList_Server1 == null)
        //{
        //  FullHostServerPacketList_Server1 = new Queue<FullPacket>();
        //}
        //else
        //{
        //  lock (FullHostServerPacketList_Server1)
        //    FullHostServerPacketList_Server1.Clear();
        //}
        /***************************************************************************************/


        //FullPacketDataProcessThread_Server1.Start();

        if (IsEnable_DataProcessHostServerThread == true)
        {
          DataProcessHostServerThread.Start();
        }


        //labelStatusInfo.Text = "Connecting...";
        //labelStatusInfo.ForeColor = System.Drawing.Color.Navy;
      }
      catch (Exception ex)
      {
        string exceptionMessage = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
        // Console.WriteLine($"EXCEPTION IN: InitializeServerConnection - {exceptionMessage}");
      }
    }

    #region Callbacks from the TCPIP client layer


    private void OnDisconnectedAndNeedRetries()
    {
      OnNotifyStatus_Server1(this, STATUS.INIT_FAILED);
    }



    private void OnDataReceive_Server1(byte[] message, int messageSize)
    {
      if (AppIsExiting_Server1 || (ImDisconnecting == true))
        return;
     

      if (messageSize == 0)
      {
        if (nCountLossData++ >= 30)
        {
          nCountLossData = 0;
          //
          StartToDisconnect_Server1();

          OnProcessDataWith_Server1("");
        }
      }
      //
      if ((IsEnable_DataProcessHostServerThread == true) && (messageSize > 0))
      {
        HostServerRawPackets_Server1.AddToList(message, messageSize);
      }
      //
      if (autoEventHost_Server1 != null)
        autoEventHost_Server1.Set();//Fire in the hole
    }

    private delegate void OnProcessDataDelegate_Server1(string str);
    private void OnProcessDataWith_Server1(string str)
    {
      if (InvokeRequired)
      {
        this.Invoke(new OnProcessDataDelegate_Server1(OnProcessDataWith_Server1), new object[] { str });
        return;
      }
      // Console.WriteLine("=== Test ==== ");
      _eClientRequest_Server1 = eClientRequest.CONNECT_CLIENT;
      this.timer_restart_tcp_comm_Server1.Enabled = true;
      //
      // Console.WriteLine(String.Format("=== timer_restart_tcp_comm is {0} ==== ", this.timer_restart_tcp_comm_Server1.Enabled));

    }
    /// <summary>
    /// Server disconnected
    /// </summary>
    private void OnDisconnect_Server1()
    {
      //Debug.WriteLine("Something Disconnected!! - OnDisconnect()");

      //this.restart_client_connect.Enabled = true;

      DoServerDisconnect();

      //Thread.Sleep(75);
      //_eClientRequest = eClientRequest.CONNECT_CLIENT;
      //this.timer_restart_tcp_comm.Enabled = true;
      OnProcessDataWith_Server1("");
      //
      _eClientRequest_Server1 = eClientRequest.CONNECT_CLIENT;
      ServerConnected_Server1 = false;

      //
      OnNotifyStatus_Server1(this, STATUS.INIT_FAILED);
    }
    #endregion

    internal void SendMessageToServer(byte[] byData)
    {
      //TimeSpan ts = client.LastDataFromServer
      try
      {
        if ((client_to_Server1 != null) && (ServerConnected_Server1 == true))
        {
          if (client_to_Server1.Connected)
          {
            //Console.WriteLine($"StartToSendData: {byData.Length}" );
            //Console.WriteLine($"{BitConverter.ToString(byData).Replace("-", " ")}");
            client_to_Server1.SendMessage(byData);
          }
          ServerConnected_Server1 = client_to_Server1.Connected;
        }
      }
      catch
      {
      }
    }

    #region Packet factory Processing from server

    private void NormalizeServerRawPackets_Server1()
    {
      try
      {
        // Console.WriteLine($"NormalizeServerRawPackets Server1 ThreadID = {Thread.CurrentThread.ManagedThreadId}");

        while (ServerConnected_Server1)
        {
          //ods.DebugOut("Before AutoEvent");
          //autoEventHost_Server1.WaitOne(10000);//wait at mutex until signal
          autoEventHost_Server1.WaitOne(10000);//wait at mutex until signal
          //ods.DebugOut("After AutoEvent");

          if (AppIsExiting_Server1 || this.IsDisposed || (ImDisconnecting == true))
            break;

          /**********************************************/

          if (HostServerRawPackets_Server1.GetItemCount == 0)
            continue;

          //byte[] packetplayground = new byte[45056];//good for 10 full packets(40960) + 1 remainder(4096)
          byte[] packetplayground = new byte[11264];//good for 10 full packets(10240) + 1 remainder(1024)
          RawPackets rp;

          int actualPackets = 0;

          while (true)
          {
            if (HostServerRawPackets_Server1.GetItemCount == 0)
            {
              break;
            }

            int holdLen = 0;

            //if (HostServerRawPackets_Server1.bytesRemaining > 0)
            //{
            //  Copy(HostServerRawPackets_Server1.Remainder, 0, packetplayground, 0, HostServerRawPackets_Server1.bytesRemaining);
            //}

            //string message = ConvertToString(HostServerRawPackets.Remainder, HostServerRawPackets.bytesRemaining);
            //Console.WriteLine(String.Format("NormalizeServerRawPackets: {0}", message));


            holdLen = HostServerRawPackets_Server1.bytesRemaining;

            for (int i = 0; i < 10; i++)//only go through a max of 10 times so there will be room for any remainder
            {
              try
              {
                if (HostServerRawPackets_Server1.GetItemCount == 0)
                {
                  break;
                }
                rp = HostServerRawPackets_Server1.GetTopItem;

                Array.Copy(rp.dataChunk, 0, packetplayground, holdLen, rp.iChunkLen);

                holdLen = rp.iChunkLen;

                if (HostServerRawPackets_Server1.GetItemCount == 0)//make sure there is more in the list befor continuing
                  break;
              }
              catch
              {

              }
            }
            //
            OnCommunicationsWithServer(packetplayground, holdLen);
            actualPackets = 0;

          }//end of while(true)
        }

        // Console.WriteLine("Exiting the packet normalizer");
      }
      catch (Exception ex)
      {
        string exceptionMessage = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
        // Console.WriteLine($"EXCEPTION IN: NormalizeServerRawPackets - {exceptionMessage}");
      }
    }



    //private void ProcessRecievedServerData_Server1()
    //{
    //  try
    //  {
    //    // Console.WriteLine($"ProcessRecievedHostServerData ThreadID = {Thread.CurrentThread.ManagedThreadId}");
    //    while (ServerConnected_Server1)
    //    {
    //      //ods.DebugOut("Before AutoEvent2");
    //      autoEvent2_Server1.WaitOne(10000);//wait at mutex until signal
    //      //autoEvent2.WaitOne();
    //      //ods.DebugOut("After AutoEvent2");
    //      if (AppIsExiting_Server1 || !ServerConnected_Server1 || this.IsDisposed)
    //        break;

    //      while (FullHostServerPacketList_Server1.Count > 0)
    //      {
    //        try
    //        {
    //          FullPacket fp;
    //          lock (FullHostServerPacketList_Server1)
    //            fp = FullHostServerPacketList_Server1.Dequeue();

    //          UInt16 type = (ushort)(fp.ThePacket[1] << 8 | fp.ThePacket[0]);



    //          switch (type)//Interrogate the first 2 Bytes to see what the packet TYPE is
    //          {
    //            case (Byte)PACKETTYPES.TYPE_RequestCredentials:
    //              {
    //                ReplyToHostCredentialRequest_Server1(fp.ThePacket);
    //                //(new Thread(() => ReplyToHostCredentialRequest(fp.ThePacket))).Start();//
    //              }
    //              break;
    //            case (Byte)PACKETTYPES.TYPE_Ping:
    //              {
    //                ReplyToHostPing_Server1(fp.ThePacket);
    //                // Console.WriteLine($"Received Ping: {GeneralFunction.GetDateTimeFormatted}");
    //              }
    //              break;
    //            case (Byte)PACKETTYPES.TYPE_HostExiting:
    //              HostCommunicationsHasQuit_Server1(true);
    //              break;
    //            case (Byte)PACKETTYPES.TYPE_Registered:
    //              {
    //                SetConnectionsStatus_Server1();
    //              }
    //              break;
    //            case (Byte)PACKETTYPES.TYPE_MessageReceived:
    //              //pictureBox1.Image = imageListStatusLights.Images["GREEN"];
    //              break;
    //          }

    //          if (client_to_Server1 != null)
    //            client_to_Server1.LastDataFromServer = DateTime.Now;
    //        }
    //        catch (Exception ex)
    //        {
    //          string exceptionMessage = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
    //          // Console.WriteLine($"EXCEPTION IN: ProcessRecievedServerData A - {exceptionMessage}");
    //        }
    //      }//end while
    //    }//end while serverconnected

    //    //ods.DebugOut("Exiting the ProcessRecievedHostServerData() thread");
    //  }
    //  catch (Exception ex)
    //  {
    //    string exceptionMessage = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
    //    // Console.WriteLine($"EXCEPTION IN: ProcessRecievedServerData B - {exceptionMessage}");
    //  }
    //}
    
    
    #endregion

    private void SetConnectionsStatus_Server1()
    {
      Int32 loc = 1;
      try
      {
        if (InvokeRequired)
        {
          loc = 5;
          this.Invoke(new MethodInvoker(SetConnectionsStatus_Server1));
          return;
        }
        loc = 10;
        //pictureBox1.Image = imageListStatusLights.Images["GREEN"];
      }
      catch (Exception ex)
      {
        string exceptionMessage = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
        // Console.WriteLine($"EXCEPTION IN: SetConnectionsStatus - {exceptionMessage}");
      }
    }

    #region Packets
    private void ReplyToHostPing_Server1(byte[] message)
    {
      try
      {
        PACKET_DATA IncomingData = new PACKET_DATA();
        IncomingData = (PACKET_DATA)PACKET_FUNCTIONS.ByteArrayToStructure(message, typeof(PACKET_DATA));

        /****************************************************************************************/
        //calculate how long that ping took to get here
        TimeSpan ts = (new DateTime(IncomingData.DataLong1)) - (new DateTime(ServerTime_Server1));
        // Console.WriteLine($"{GeneralFunction.GetDateTimeFormatted}: {string.Format("Ping From Server to client: {0:0.##}ms", ts.TotalMilliseconds)}");
        /****************************************************************************************/

        ServerTime_Server1 = IncomingData.DataLong1;// Server computer's current time!

        PACKET_DATA xdata = new PACKET_DATA();

        xdata.Packet_Type = (UInt16)PACKETTYPES.TYPE_PingResponse;
        xdata.Data_Type = 0;
        xdata.Packet_Size = 16;
        xdata.maskTo = 0;
        xdata.idTo = 0;
        xdata.idFrom = 0;

        xdata.DataLong1 = IncomingData.DataLong1;

        byte[] byData = PACKET_FUNCTIONS.StructureToByteArray(xdata);

        //SendMessageTo_Server1(byData);

        CheckThisComputersTimeAgainstServerTime_Server1();
      }
      catch (Exception ex)
      {
        string exceptionMessage = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
        // Console.WriteLine($"EXCEPTION IN: ReplyToHostPing - {exceptionMessage}");
      }
    }

    private void CheckThisComputersTimeAgainstServerTime_Server1()
    {
      Int64 timeDiff = DateTime.UtcNow.Ticks - ServerTime_Server1;
      TimeSpan ts = TimeSpan.FromTicks(Math.Abs(timeDiff));
      // Console.WriteLine($"Server diff in secs: {ts.TotalSeconds}");

      if (ts.TotalMinutes > 15)
      {
        string msg = string.Format("Computer Time Discrepancy!! " +
            "The time on this computer differs greatly " +
            "compared to the time on the Realtrac Server " +
            "computer. Check this PC's time.");

        // Console.WriteLine(msg);
      }
    }

    public void ReplyToHostCredentialRequest_Server1(byte[] message)
    {
      if (client_to_Server1 == null)
        return;

      // Console.WriteLine($"ReplyToHostCredentialRequest ThreadID = {Thread.CurrentThread.ManagedThreadId}");
      Int32 Loc = 0;
      try
      {
        //We will assume to tell the host this is just an update of the
        //credentials we first sent during the application start. This
        //will be true if the 'message' argument is null, otherwise we
        //will change the packet type below to the 'TYPE_MyCredentials'.
        UInt16 PaketType = (UInt16)PACKETTYPES.TYPE_CredentialsUpdate;

        if (message != null)
        {
          int myOldServerID = 0;
          //The host server has past my ID.
          PACKET_DATA IncomingData = new PACKET_DATA();
          IncomingData = (PACKET_DATA)PACKET_FUNCTIONS.ByteArrayToStructure(message, typeof(PACKET_DATA));
          Loc = 10;
          if (MyHostServerID_Server1 > 0)
            myOldServerID = MyHostServerID_Server1;
          Loc = 20;
          MyHostServerID_Server1 = (int)IncomingData.idTo;//Hang onto this value
          Loc = 25;

          //Console.WriteLine($"My Host Server ID is {MyHostServerID}");

          //string MyAddressAsSeenByTheHost = new string(IncomingData.szStringDataA).TrimEnd('\0');//My computer address
          //string text = String.Format("My Address As Seen By The Server: {0}, and my ID given by the server is: {1}", MyAddressAsSeenByTheHost, MyHostServerID);
          //SetSomeLabelInfoFromThread(text);

          ServerTime_Server1 = IncomingData.DataLong1;

          PaketType = (UInt16)PACKETTYPES.TYPE_MyCredentials;
        }


      }
      catch (Exception ex)
      {
        string exceptionMessage = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
        // Console.WriteLine($"EXCEPTION at location {Loc}, IN: ReplyToHostCredentialRequest - {exceptionMessage}");
      }
    }



    //private delegate void StartToDiconnectDelegate_Server1(string info);
    //private void SetStartToDiconnecFromThread_Server1(string info)
    //{
    //  if (InvokeRequired)
    //  {
    //    this.Invoke(new StartToDiconnectDelegate_Server1(SetStartToDiconnecFromThread_Server1), new object[] { info });
    //    return;
    //  }

    //  StartToDisconnect_Server1();
    //}



    //private delegate void SetSomeLabelInfoDelegate(string info);
    //private void SetSomeLabelInfoFromThread(string info)
    //{
    //  if (InvokeRequired)
    //  {
    //    this.Invoke(new SetSomeLabelInfoDelegate(SetSomeLabelInfoFromThread), new object[] { info });
    //    return;
    //  }

    //  //labelConnectionStuff.Text = info + " end";
    //}

    private delegate void HostCommunicationsHasQuitDelegate_Server1(bool FromHost);
    private void HostCommunicationsHasQuit_Server1(bool FromHost)
    {
      if (InvokeRequired)
      {
        this.Invoke(new HostCommunicationsHasQuitDelegate_Server1(HostCommunicationsHasQuit_Server1), new object[] { FromHost });
        return;
      }

      if (client_to_Server1 != null)
      {
        int c = 100;
        do
        {
          c--;
          Application.DoEvents();
          Thread.Sleep(10);
        }
        while (c > 0);

        DoServerDisconnect();

        if (FromHost)
        {
          //labelStatusInfo.Text = "The Server has exited";
        }
        else
        {
        }


      }
    }

    //private void TellServerImDisconnecting_Server1()
    //{
    //  try
    //  {
    //    //PACKET_DATA xdata = new PACKET_DATA();

    //    //xdata.Packet_Type = (UInt16)PACKETTYPES.TYPE_Close;
    //    //xdata.Data_Type = 0;
    //    //xdata.Packet_Size = 16;
    //    //xdata.maskTo = 0;
    //    //xdata.idTo = 0;
    //    //xdata.idFrom = 0;

    //    //byte[] byData = PACKET_FUNCTIONS.StructureToByteArray(xdata);

    //    //SendMessageTo_Server1(byData);
    //  }
    //  catch (Exception ex)
    //  {
    //    string exceptionMessage = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
    //    // Console.WriteLine($"EXCEPTION IN: TellServerImDisconnecting - {exceptionMessage}");
    //  }
    //}
    #endregion

    #region General Timer
    /// <summary>
    /// This will watch the TCPIP communication, after 5 minutes of no communications with the 
    /// Server we will assume the connections has been severed
    /// </summary>


    private void GeneralTimer_Tick_Server1(object sender, EventArgs e)
    {
      if (client_to_Server1 != null)
      {
        TimeSpan ts = DateTime.Now - client_to_Server1.LastDataFromServer;

        //If we dont hear from the server for more than 5 minutes then there is a problem so disconnect
        if (ts.TotalMinutes > 5)
        {
          DestroyGeneralTimer_Server1();
          HostCommunicationsHasQuit_Server1(false);
        }
      }

      // Add 5 seconds worth of Ticks to the server time
      ServerTime_Server1 += (TimeSpan.TicksPerSecond * 5);
      //Console.WriteLine("SERVER TIME: " + (new DateTime(GeneralFunction.ServerTime)).ToLocalTime().TimeOfDay.ToString());
    }

    private void DestroyGeneralTimer_Server1()
    {
      if (GeneralTimer_Server1 != null)
      {
        if (GeneralTimer_Server1.Enabled == true)
          GeneralTimer_Server1.Enabled = false;

        try
        {
          GeneralTimer_Server1.Tick -= GeneralTimer_Tick_Server1;
        }
        catch (Exception)
        {
          //just incase there was no event to remove
        }
        GeneralTimer_Server1.Dispose();
        GeneralTimer_Server1 = null;
      }
    }
    #endregion//General Timer section

    private string GetSHubAddress_Server1()//translates a named IP to an address
    {
      string SHubServer = IPAddress;//textBoxServer.Text; //GeneralSettings.HostIP.Trim();

      if (SHubServer.Length < 1)
        return string.Empty;

      try
      {
        string[] qaudNums = SHubServer.Split('.');

        // See if its not a straightup IP address.. 
        //if not then we have to resolve the computer name
        if (qaudNums.Length != 4)
        {
          //Must be a name so see if we can resolve it
          IPHostEntry hostEntry = Dns.GetHostEntry(SHubServer);

          foreach (IPAddress a in hostEntry.AddressList)
          {
            if (a.AddressFamily == AddressFamily.InterNetwork)//use IP 4 for now
            {
              SHubServer = a.ToString();
              break;
            }
          }
          //SHubServer = hostEntry.AddressList[0].ToString();
        }
      }
      catch (SocketException se)
      {
        // Console.WriteLine($"Exception: {se.Message}");
        //statusStrip1.Items[1].Text = se.Message + " for " + Properties.Settings.Default.HostIP;
        SHubServer = string.Empty;
      }

      return SHubServer;
    }





    private int counter = 0;
    

    //private List<FX_DATA> Filter_CorrectData(List<FX_DATA> list_FX_DATA)
    //{
    //  List<FX_DATA> list_correctData = new List<FX_DATA>();
    //  list_correctData = list_FX_DATA.FindAll(x => x.fx_device != FX_DEVICE.ERROR_DATA);
    //  //for (int i = 0; i < list_FX_DATA.Count; i++)
    //  //{
    //  //  if (list_FX_DATA[i].fx_device != FX_DEVICE.ERROR_DATA)
    //  //  {
    //  //    list_correctData.Add(list_FX_DATA[i]);
    //  //  }
    //  //}
    //  return list_correctData;
    //}
    //private Error FindErrorByCode(int error_code)
    //{
    //  Error error = null;
    //  bool IsExitLoop = false;
    //  error = list_error.FindLast(x => x._error_code == error_code);
    //  //for (int i = 0; (i < list_error.Count) && (IsExitLoop == false); i++)
    //  //{
    //  //  if (list_error[i]._error_code == error_code)
    //  //  {
    //  //    error = list_error[i];
    //  //    IsExitLoop = true;
    //  //  }
    //  //}
    //  return error;
    //}

    private int bool_to_int(bool bool_value)
    {
      int ret = (bool_value == true)?1:0;
      return ret;
    }

    //private char convertDeviceToChar(FX_DEVICE device)
    //{
    //  char data = ' ';
    //  if (device == FX_DEVICE.X)
    //  {
    //    data = 'X';
    //  }
    //  else if (device == FX_DEVICE.Y)
    //  {
    //    data = 'Y';
    //  }
    //  else if (device == FX_DEVICE.M)
    //  {
    //    data = 'M';
    //  }
    //  else if (device == FX_DEVICE.D)
    //  {
    //    data = 'D';
    //  }
    //  return data;
    //}


    //private List<FX_DATA> ProcessDataFromPLC_Binary(byte[] data_from_plc)
    //{
    //  bool IsSubHeader_OK = false;
    //  bool IsNetwork_Number_OK = false;
    //  bool IsRequest_destination_station_number_OK = false;
    //  bool IsRequest_destination_module_IO_number_OK = false;
    //  bool IsRequest_destination_multi_drop_station_number_OK = false;
    //  bool IsEnCode_OK = false;
    //  int EndCode = 0xFF;
    //  int byte_idx = 0;
    //  int Response_data_length = 0;
    //  List<FX_DATA> list_data = new List<FX_DATA>();
    //  Error error_des = null;
    //  try
    //  {
    //    /* Get & check hearder */
    //    if (data_from_plc.Length >= 2)
    //    {
    //      IsSubHeader_OK = ((data_from_plc[byte_idx++] == 0xD0) && //0
    //                        (data_from_plc[byte_idx++] == 0x00)); //1


    //    }
    //    /* Get & check sub_header */
    //    if ((data_from_plc.Length >= 3) && (IsSubHeader_OK == true))
    //    {
    //      IsNetwork_Number_OK = ((data_from_plc[byte_idx++] == 0x00)); //2
    //    }
    //    /* Get & check Network_Number */
    //    if ((data_from_plc.Length >= 4) && (IsNetwork_Number_OK == true))
    //    {
    //      IsRequest_destination_station_number_OK = ((data_from_plc[byte_idx++] == 0xFF)); //3
    //    }
    //    /* Get & check destination_station_number */
    //    if ((data_from_plc.Length >= 6) && (IsRequest_destination_station_number_OK == true))
    //    {
    //      IsRequest_destination_module_IO_number_OK = ((data_from_plc[byte_idx++] == 0xFF) && //4
    //                        (data_from_plc[byte_idx++] == 0x03)); //5
    //    }
    //    if ((data_from_plc.Length >= 7) && (IsRequest_destination_module_IO_number_OK == true))
    //    {
    //      IsRequest_destination_multi_drop_station_number_OK = ((data_from_plc[byte_idx++] == 0x00)); //6
    //    }
    //    if ((data_from_plc.Length >= 9) && (IsRequest_destination_multi_drop_station_number_OK == true))
    //    {
    //      byte data_length_LSB = data_from_plc[byte_idx++];//7
    //      byte data_length_MSB = data_from_plc[byte_idx++];//8
    //      Response_data_length = (data_length_MSB << 8) | data_length_LSB;
    //    }

    //    if (Response_data_length > 0)
    //    {
    //      //int uu = 0;
    //      if (data_from_plc.Length >= 11)
    //      {
    //        byte EndCode_LSB = data_from_plc[byte_idx++];//9
    //        byte EndCode_MSB = data_from_plc[byte_idx++];//10
    //        EndCode = (EndCode_MSB << 8) | EndCode_LSB;
    //        Response_data_length = Response_data_length - 2;
    //      }
    //    }
    //    IsEnCode_OK = (EndCode == 0);
    //    if (IsEnCode_OK == false)
    //    {
    //      /* page 29 -- Error information
    //       * The request destination network number, request destination station number, request destination module I/O number, and
    //        request destination multi-drop station number of the station which responded with errors are stored.
    //       */
    //      error_des = FindErrorByCode(EndCode);
    //    }
    //    else /*if (IsEnCode_OK == false)*/
    //    {
    //      if (Response_data_length > 0)
    //      {
    //        if (current_data.fx_command == FX_COMMAND.BR) /* request is READ_BITS */
    //        {
    //          if (current_data.protocol_unit == PROTOCOL_UNIT._x1_BIT)
    //          {
    //            int nCount = 0;
    //            for (int i = 0; i < Response_data_length; i++)
    //            {
    //              int byte_data_from_plc = data_from_plc[byte_idx + i];
    //              int value_1 = (byte_data_from_plc >> 4) & 0x0F;
    //              int value_2 = (byte_data_from_plc) & 0x0F;

    //              for (int j = 0; j < 2; j++)
    //              {
    //                nCount++;
    //                if (nCount <= current_data.max_device)
    //                {
    //                  FX_DATA fx_data = new FX_DATA();
    //                  //save to current_data
    //                  fx_data.fx_command = current_data.fx_command;
    //                  fx_data.fx_device = current_data.fx_device;
    //                  fx_data.address = current_data.address + ((2 * i) + j);
    //                  if (j == 0)
    //                  {
    //                    fx_data.value = value_1;
    //                  }
    //                  else if (j == 1)
    //                  {
    //                    fx_data.value = value_2;
    //                  }
    //                  fx_data.device_as_string = String.Format("{0}{1}", convertDeviceToChar(fx_data.fx_device), convertAddressToString(fx_data.address));
    //                  //add to list 
    //                  list_data.Add(fx_data);
    //                }/*if (nCount <= current_data.max_device)*/
    //              }/*for (int j = 0; j < 2; j++)*/
    //            }/*for (int i = 0; i < Response_data_length; i++)*/

    //          }
    //          else /* (current_data.protocol_unit == PROTOCOL_UNIT._x16_BITS) */
    //          {
    //            for (int i = 0; i < Response_data_length; i++)
    //            {
    //              int byte_data_from_plc = data_from_plc[byte_idx + i];
    //              int value_1 = (byte_data_from_plc >> 4) & 0x0F;
    //              int value_2 = (byte_data_from_plc) & 0x0F;

    //              for (int j = 1; j >= 0; j--)
    //              {
    //                int value_from_plc = 0;
    //                if (j == 1)
    //                {
    //                  value_from_plc = value_1;
    //                }
    //                else if (j == 0)
    //                {
    //                  value_from_plc = value_2;
    //                }

    //                for (int data_bit_idx = 3; data_bit_idx >= 0; data_bit_idx--)
    //                {

    //                  FX_DATA fx_data = new FX_DATA();
    //                  //save to current_data
    //                  fx_data.fx_command = current_data.fx_command;
    //                  fx_data.fx_device = current_data.fx_device;

    //                  fx_data.address = current_data.address + (8 * i) + (4 * j) + data_bit_idx;
    //                  if (data_bit_idx == 3)
    //                  {
    //                    fx_data.value = bool_to_int((value_from_plc & 0x08) == (0x08));
    //                  }
    //                  else if (data_bit_idx == 2)
    //                  {
    //                    fx_data.value = bool_to_int((value_from_plc & 0x04) == (0x04));
    //                  }
    //                  else if (data_bit_idx == 1)
    //                  {
    //                    fx_data.value = bool_to_int((value_from_plc & 0x02) == (0x02));
    //                  }
    //                  else /*(data_bit_idx == 0)*/
    //                  {
    //                    fx_data.value = bool_to_int((value_from_plc & 0x01) == (0x01));
    //                  }

    //                  fx_data.device_as_string = String.Format("{0}{1}", convertDeviceToChar(fx_data.fx_device), convertAddressToString(fx_data.address));
    //                  //add to list 
    //                  list_data.Add(fx_data);
    //                }
    //              }/*for (int j = 0; j < 2; j++)*/
    //            }/*for (int i = 0; i < Response_data_length; i++)*/
    //          }
    //        }
    //        else if (current_data.fx_command == FX_COMMAND.WR) /* request is READ_WORD */
    //        {
    //          //FX_DATA fx_data_Dequeue = WritenData_Queue.Dequeue(); //Khoa_test

    //          for (int i = 0; i < Response_data_length; i += 2)
    //          {
    //            int value_LSB = data_from_plc[byte_idx + i];
    //            int value_MSB = data_from_plc[byte_idx + (i + 1)];
    //            int value_from_plc = (value_MSB << 8) | (value_LSB);
    //            //
    //            int data_word_idx = (i / 2);
    //            //
    //            FX_DATA fx_data = new FX_DATA();
    //            //save to current_data
    //            fx_data.fx_command = current_data.fx_command;
    //            fx_data.fx_device = current_data.fx_device;
    //            fx_data.address = current_data.address + data_word_idx;

    //            //fx_data = fx_data_Dequeue; //Khoa_test
    //            //fx_data.address += data_word_idx;

    //            fx_data.value = value_from_plc;
    //            fx_data.device_as_string = String.Format("{0}{1}", convertDeviceToChar(fx_data.fx_device), convertAddressToString(fx_data.address));
    //            if (current_data.address == 1400)
    //            {
    //              if (fx_data.address == 1426)
    //              {
    //                if (value_from_plc > 0)
    //                {
    //                  int mm = 0;
    //                }
    //                else
    //                {
    //                  int mm = 0;
    //                }
    //              }
    //            }
    //            //add to list 
    //            list_data.Add(fx_data);
    //          }
    //        }
    //      }
    //      else /*if (Response_data_length > 0)*/
    //      {
    //        if (current_data.fx_command == FX_COMMAND.BW)
    //        {
    //          FX_DATA fx_data = new FX_DATA();
    //          //save to current_data
    //          fx_data.fx_command = current_data.fx_command;
    //          fx_data.fx_device = FX_DEVICE.ACK;
    //          fx_data.address = current_data.address;

    //          fx_data.value = 0x7F;
    //          fx_data.device_as_string = String.Format("{0}{1}", convertDeviceToChar(current_data.fx_device), convertAddressToString(fx_data.address));
    //          //add to list 
    //          list_data.Add(fx_data);
    //        }
    //        OnNotifyStatus_Server1(this, STATUS.WRITE_DATA_OK);
    //      }
    //    }
    //  }
    //  catch (Exception error)
    //  {
    //    string errr = "";//error.Message();
    //  }

    //  /* ever thing done */
    //  if ((IsSubHeader_OK == true) &&
    //      (IsNetwork_Number_OK == true) &&
    //      (IsRequest_destination_station_number_OK == true) &&
    //      (IsRequest_destination_module_IO_number_OK == true) &&
    //      (IsRequest_destination_multi_drop_station_number_OK == true) &&
    //      (IsEnCode_OK == true))
    //  {
    //    /* do nothing */
    //  }
    //  else /* something error ==> feedback error */
    //  {
    //    FX_DATA fx_data = new FX_DATA();
    //    //save to current_data
    //    fx_data.fx_command = current_data.fx_command;
    //    fx_data.fx_device = FX_DEVICE.ERROR_DATA;
    //    fx_data.address = (-1);
    //    fx_data.value = (-1);
    //    fx_data.device_as_string = "";//convert_SLMP_ErrorCode(EndCode);
    //    //add to list
    //    list_data.Add(fx_data);
    //  }
    //  return list_data;
    //}

  }

}
