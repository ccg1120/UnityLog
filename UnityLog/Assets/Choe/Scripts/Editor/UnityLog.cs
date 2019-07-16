using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace Choe
{
    public class UnityLog : Editor {

       
        private static List<LogMessage> LogList = new List<LogMessage>();
        private static bool LogOn = true;


        #region LogTestMenu
        [MenuItem("Test/Log %&q")]
        public static void TestLog()
        {
            Debug.Log("Test Log");
        }

        [MenuItem("Test/ErrorLog %&w")]
        public static void UnityErrorLog()
        {
            Debug.LogError("Unity LogError");
        }


        [MenuItem("Test/WarningLog %&e")]
        public static void UnityWarningLog()
        {
            Debug.LogWarning("Unity Warning");
        }
        [MenuItem("Test/AllLog %&a")]
        public static void UnityAllLog()
        {
            TestLog();
            UnityErrorLog();
            UnityWarningLog();
        }

        public UnityLog()
        { 
            Debug.Log("Unity Log ");
        }
        #endregion

        [RuntimeInitializeOnLoadMethod]
        public static void PlayLogEvent()
        {
            Debug.Log("RuntimeInitializeOnLoadMethod");
            Debug.Log("LogOn : " + LogOn);
            if(LogOn)
            {
                EditorModeChangeEvent();
            }
        }

     

        private static void EditorModeChangeEvent()
        {
            EditorApplication.playModeStateChanged += (mode)=> PlayModeEvent(mode);
        }
        private static void PlayModeEvent(PlayModeStateChange mode)
        {
            switch (mode)
            {
                case PlayModeStateChange.EnteredEditMode:
                    break;
                case PlayModeStateChange.ExitingEditMode:
                    break;
                case PlayModeStateChange.EnteredPlayMode:
                    ClearLogList();
                    EditorApplication.update += UpdataLoop;
                    AddLogEvent();
                    break;
                case PlayModeStateChange.ExitingPlayMode:
                    EditorApplication.update -= UpdataLoop;
                    RemoveLogEvent();
                    SaveLogFile();
                    break;
            }
        }

        private static void UpdataLoop()
        {
            if (EditorApplication.isPlaying)
            {
                Debug.Log("is playing ");
            }
            else
            {
                Debug.Log("is Not  playing ");
            }
            
        }
        private static void LogEvent(string mes, string trace, LogType type)
        {
            uint num = (uint)LogList.Count;
            LogMessage temp = new LogMessage(num, mes, trace, type);
            LogList.Add(temp);
            Debug.Log("LogEvent " + mes);
        }

        private static void AddLogEvent()
        {
            Application.logMessageReceived += LogEvent;
        }
        private static void RemoveLogEvent()
        {
            Application.logMessageReceived -= LogEvent;
        }


        private static void ClearLogList()
        {
            LogList.Clear();
        }
            
        private static void SaveLogFile()
        {

        }


    }
}