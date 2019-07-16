using UnityEditor;
using UnityEngine;
using System.IO;
using System;

public class UnityLogFileUtility : Editor {

    private static string m_LogSaveFolder = "Logs";
    private static string m_LogFileNameBase = "Log_";
    private static string m_LogFileType = ".csv";
    private static string m_LogSavePath = string.Empty;

    private static FileStream m_FileStream;
    private static StreamWriter m_LogWriter;


    [InitializeOnLoadMethod]
    public static void UnityLogInit()
    {
        Check_SaveLogFolder();
    }
    private static void Check_SaveLogFolder()
    {
        m_LogSavePath = Path.Combine(Directory.GetCurrentDirectory(), m_LogSaveFolder);

        DirectoryInfo info = new DirectoryInfo(m_LogSavePath);
        if (!info.Exists)
        {
            Debug.LogError("Create file path !!");
            info.Create();
        }   
    }

    public static void CreateLogFile()
    {
        try
        {
            string filename = LogFileName();
            string filepath = Path.Combine(m_LogSavePath, filename);
            Debug.Log("Create File Path = " + filepath);
            m_FileStream = File.Create(filepath);
            m_LogWriter = new StreamWriter(m_FileStream);
        }
        catch
        {
            if(m_FileStream != null)
            {
                m_FileStream.Close();
            }
            if (m_LogWriter != null)
            {
                m_LogWriter.Close();
            }
        }
    }


    private static string LogFileName()
    {
        string name = m_LogFileNameBase + DateTime.Now.ToString("MMdd_hh_mm_ss") + m_LogFileType;
        return name;
    }

}
