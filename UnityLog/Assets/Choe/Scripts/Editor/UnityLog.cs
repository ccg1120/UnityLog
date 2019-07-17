using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace Choe
{
    public class UnityLog  {

        private static string m_EditorLogStateKey = "m_LogOn";

        private static bool m_LogOn = false;

        private static bool m_LogStart = false;
        

        private static uint m_LogCount = 0;

        #region LogTestMenu
        //private static bool m_TestLogFuc = false;
        //[MenuItem("Test/Log %&q")]
        //public static void TestLog()
        //{
        //    Debug.Log("Test Log");
        //}

        //[MenuItem("Test/ErrorLog %&w")]
        //public static void UnityErrorLog()
        //{
        //    Debug.LogError("Unity LogError");

        //}


        //[MenuItem("Test/WarningLog %&e")]
        //public static void UnityWarningLog()
        //{
        //    Debug.LogWarning("Unity Warning");
        //}
        //[MenuItem("Test/AllLog %&a")]
        //public static void UnityAllLog()
        //{
        //    TestLog();
        //    UnityErrorLog();
        //    UnityWarningLog();
        //}

        //[MenuItem("Test/LoopLog")]
        //public static void UnityLoop()
        //{
        //    m_TestLogFuc = !m_TestLogFuc;
        //    if (m_TestLogFuc)
        //    {
        //        EditorApplication.update += UpdateTest;
        //    }
        //    else
        //    {
        //        EditorApplication.update -= UpdateTest;
        //    }

        //}

        //public UnityLog()
        //{ 
        //    Debug.Log("Unity Log ");
        //}
        //public static void UpdateTest()
        //{
        //    UnityAllLog();
        //}

        #endregion

        [MenuItem("Log/Open Folder %&o")]
        public static void OpenFolder()
        {
            UnityLogFileUtility.OpenLogFolder();
        }


        [MenuItem("Log/Write")]
        public static void ChangeLogState()
        {
            bool value = EditorPrefs.GetBool(m_EditorLogStateKey);

            if (value)
            {
                EditorPrefs.SetBool(m_EditorLogStateKey, false);               
                Debug.Log("Log Write Off !!");
            }
            else
            {
                EditorPrefs.SetBool(m_EditorLogStateKey, true);
                Debug.LogError("Log Write On !!");
            }
        }

        [RuntimeInitializeOnLoadMethod]
        public static void PlayLogEvent()
        {
            m_LogOn = EditorPrefs.GetBool(m_EditorLogStateKey);
            
            if (m_LogOn)
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
                    AddLogEvent();
                    break;
                case PlayModeStateChange.ExitingPlayMode:
                    RemoveLogEvent();
                    break;
            }
        }

        private static void LogEvent(string mes, string trace, LogType type)
        {
            uint num = m_LogCount;

            LogMessage temp = new LogMessage(num, mes, trace, type);
           
            UnityLogFileUtility.WriteMessage(LogCSV.CSVTypeString(temp));
            m_LogCount++;
        }

        private static void AddLogEvent()
        {
            if(UnityLogFileUtility.CreateLogFile())
            {
                m_LogCount = 0;
                Application.logMessageReceived += LogEvent;
                m_LogStart = true;
            }
            
        }

        private static void RemoveLogEvent()
        {
            if(m_LogStart)
            {
                Application.logMessageReceived -= LogEvent;
                UnityLogFileUtility.CloseLogFile();
                m_LogCount = 0;
                m_LogStart = false;                
            }
        }
    }
}