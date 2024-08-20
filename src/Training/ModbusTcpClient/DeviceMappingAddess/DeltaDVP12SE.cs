using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusTcpClient
{
  public class DeltaDVP12SE: SupportPLC
  {

    public override int GetModbusAddress(int addess)
    {
      return (4096 + addess);
    }
    //public 
  }

  public class SupportPLC
  {
    public virtual int GetModbusAddress(int addess)
    {
      return addess;
    }
  }

  public enum Device
  {
    D,
    T,
    S
  }

  public enum eSupportPlc
  {
    DeltaDVP12SE,
    Fx5U,
    GenericDevice
  }
}
