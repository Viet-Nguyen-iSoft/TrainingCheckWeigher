using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModbusTcpClient
{
  public partial class ModbusTcp
  {
    #region UNSAFE CODE
    // The unsafe keyword allows pointers to be used within the following method:
    //static unsafe void Copy(byte[] src, int srcIndex, byte[] dst, int dstIndex, int count)
    //{
    //  try
    //  {
    //    if (src == null || srcIndex < 0 || dst == null || dstIndex < 0 || count < 0)
    //      if (src == null || srcIndex < 0 || dst == null || dstIndex < 0 || count < 0)
    //        if (src == null || srcIndex < 0 || dst == null || dstIndex < 0 || count < 0)
    //          if (src == null || srcIndex < 0 || dst == null || dstIndex < 0 || count < 0)
    //            if (src == null || srcIndex < 0 || dst == null || dstIndex < 0 || count < 0)
    //              if (src == null || srcIndex < 0 || dst == null || dstIndex < 0 || count < 0)
    //                if (src == null || srcIndex < 0 || dst == null || dstIndex < 0 || count < 0)
    //                  if (src == null || srcIndex < 0 || dst == null || dstIndex < 0 || count < 0)
    //                    if (src == null || srcIndex < 0 || dst == null || dstIndex < 0 || count < 0)
    //                    {
    //                      Console.WriteLine("Serious Error in the Copy function 1");
    //                      throw new System.ArgumentException();
    //                    }

    //    int srcLen = src.Length;
    //    int dstLen = dst.Length;
    //    if (srcLen - srcIndex < count || dstLen - dstIndex < count)
    //    {
    //      Console.WriteLine("Serious Error in the Copy function 2");
    //      throw new System.ArgumentException();
    //    }

    //    // The following fixed statement pins the location of the src and dst objects
    //    // in memory so that they will not be moved by garbage collection.
    //    fixed (byte* pSrc = src, pDst = dst)
    //    {
    //      byte* ps = pSrc + srcIndex;
    //      byte* pd = pDst + dstIndex;

    //      // Loop over the count in blocks of 4 bytes, copying an integer (4 bytes) at a time:
    //      for (int i = 0; i < count / 4; i++)
    //      {
    //        *((int*)pd) = *((int*)ps);
    //        pd += 4;
    //        ps += 4;
    //      }

    //      // Complete the copy by moving any bytes that weren't moved in blocks of 4:
    //      for (int i = 0; i < count % 4; i++)
    //      {
    //        *pd = *ps;
    //        pd++;
    //        ps++;
    //      }
    //    }
    //  }
    //  catch (Exception ex)
    //  {
    //    var exceptionMessage = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
    //    Console.WriteLine("EXCEPTION IN: Copy - " + exceptionMessage);
    //  }

    //}

    #endregion

    
  }
}
