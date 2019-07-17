using System;
using UnityEngine;

namespace Choe
{
    public class LogMessage
    {
        public uint Index;
        public string TimeInfo = string.Empty;
        public LogType Type = LogType.Log;
        public string Message = string.Empty;
        public string Trace = string.Empty;

        
        public LogMessage(uint index, string mes, string trace, LogType type)
        {
            Index = index;
            TimeInfo = DateTime.Now.ToString() + " ms : " + DateTime.Now.Millisecond.ToString();
            Message = mes;
            Trace = trace.Replace("\n", ", ");
            Type = type;
        }
    }

}

