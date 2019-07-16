
namespace Choe
{
    public class LogCSV
    {
        public static string Log_CSVTitle = "Index, Time, Type, Message, Tracking";
        
       
        public string CSVTypeString(LogMessage message)
        {
            return message.Index + ", " + message.TimeInfo + ", " + message.Type.ToString() + ", " + message.Message + ", " + message.Trace;
        }
    }
}

