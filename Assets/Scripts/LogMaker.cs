using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LogMaker : MonoBehaviour
{

    private string logFilePath;

    private float timeTaken = 0.0f;

    private StreamWriter logFile;

    // Start is called before the first frame update
    void Start()
    {
        logFilePath = Application.persistentDataPath + "/testLog " + System.DateTime.Now.Date + ".txt";

        if (File.Exists(logFilePath))
        {
            logFile = new StreamWriter(logFilePath);
        }
        else
        {
            File.Create(logFilePath);
            logFile = new StreamWriter(logFilePath);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeTaken += Time.deltaTime;
    }

    //save time to file
    void saveFile()
    {
        logFile.WriteLine(System.DateTime.Now.TimeOfDay + " Time: " + timeTaken);
        logFile.WriteLine("");
        logFile.Close();
    }
}
