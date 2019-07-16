using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;


public class UnityLog : Editor {

    private static string m_LogSaveFolder = "Logs";
    private static string m_LogSavePath = string.Empty;
	
    public UnityLog()
    { 

        Debug.Log("UnityLog ");
    }

    [InitializeOnLoadMethod]
    public static void UnityLogInit()
    {
        Debug.Log("UnityLogInit");
        Check_SaveLogFolder();
    }

    private static void Check_SaveLogFolder()
    {
        m_LogSavePath = Path.Combine(Directory.GetCurrentDirectory(), m_LogSaveFolder);

        DirectoryInfo info = new DirectoryInfo(m_LogSavePath);
        if(!info.Exists)
        {
            Debug.LogError("Create file path !!");
            info.Create();
        }
    }



}
