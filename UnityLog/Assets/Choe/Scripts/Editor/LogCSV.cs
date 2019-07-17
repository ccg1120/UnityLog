
namespace Choe
{
    public class LogCSV
    {
        //file Pattern
        public static string Log_CSVTitle = "Index, Time, Type, Message, Tracking";
        
        public static string CSVTypeString(LogMessage message)
        {
            return message.Index + ", " + message.TimeInfo + ", " + message.Type.ToString() + ", " + message.Message + ", " + message.Trace;
        }
    }
}

