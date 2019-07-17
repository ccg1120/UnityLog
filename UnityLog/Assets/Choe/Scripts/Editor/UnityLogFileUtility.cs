using UnityEditor;
using UnityEngine;
using System.IO;
using System;

namespace Choe
{
    public class UnityLogFileUtility  {

        private static string m_LogSaveFolder = "Logs";
        private static string m_LogFileNameBase = "Log_";
        private static string m_LogFileType = ".csv";
        private static string m_LogSaveFolderPath = string.Empty;

        private static FileStream m_FileStream;
        private static StreamWriter m_LogWriter;

        private static bool m_CreateFileState = false;

        [InitializeOnLoadMethod]
        public static void UnityLogInit()
        {
            Check_SaveLogFolder();
        }

        
        public static void OpenLogFolder()
        {
            Application.OpenURL(m_LogSaveFolderPath);
            //EditorUtility.RevealInFinder(m_LogSaveFolderPath);    // 로그 폴더까지 안들어가짐
        }

        public static bool CreateLogFile()
        {
            try
            {
                string filename = LogFileName();
                string filepath = Path.Combine(m_LogSaveFolderPath, filename);
                Debug.Log("Create File Path = " + filepath);
                m_FileStream = File.Create(filepath);
                m_LogWriter = new StreamWriter(m_FileStream);
                m_LogWriter.WriteLine(LogCSV.Log_CSVTitle);
                m_CreateFileState = true;
            }
            catch
            {
                if (m_LogWriter != null)
                {
                    m_LogWriter.Close();
                }
                if (m_FileStream != null)
                {
                    m_FileStream.Close();
                }
            }
            finally
            {
                Debug.Log("CreateLogFile final");
            }
            return m_CreateFileState;
        }
        public static void WriteMessage(string log)
        {
            if(m_CreateFileState)
            {
                //Debug.Log(log);
                m_LogWriter.WriteLine(log);
            }
        }
        public static bool CloseLogFile()
        {
            bool result = false;
            if(m_CreateFileState)
            {
                m_CreateFileState = false;

                m_LogWriter.Close();
                m_FileStream.Close();
                Debug.Log("Close File");
                result = true;
            }
            return result;
        }


        private static void Check_SaveLogFolder()
        {
            m_LogSaveFolderPath = Path.Combine(Directory.GetCurrentDirectory(), m_LogSaveFolder);

            DirectoryInfo info = new DirectoryInfo(m_LogSaveFolderPath);
            if (!info.Exists)
            {
                Debug.LogError("Create file path !!");
                info.Create();
            }   
        }
        private static string LogFileName()
        {
            string name = m_LogFileNameBase + DateTime.Now.ToString("MMdd_hh_mm_ss") + m_LogFileType;
            return name;
        }

    }
}