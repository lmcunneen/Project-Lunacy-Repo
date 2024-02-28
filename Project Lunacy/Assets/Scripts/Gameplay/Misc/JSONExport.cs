using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONExport : JourneyScript
{
    private DataPoints data = new();

    void Start()
    {
        
    }

    public void UpdateData(uint stepCount, uint vitalityValue, uint willpowerValue, uint sanityValue)
    {
        data.amountOfStepsLasted = stepCount;

        data.vitalityData.Add(vitalityValue);
        data.willpowerData.Add(willpowerValue);
        data.sanityData.Add(sanityValue);
    }

    public void CommitToFile()
    {
        string json = JsonUtility.ToJson(data);

        string pathName = Application.persistentDataPath + 
            "/DataExports/characterData" + DateTime.Now.DayOfYear + '-' + DateTime.Now.Hour + '-' + 
               DateTime.Now.Minute + '-' + DateTime.Now.Second + ".json";

        if (!File.Exists(pathName))
        {
            Directory.CreateDirectory($@"{Application.persistentDataPath}\DataExports\");
        }

        FileStream fs = new FileStream(pathName, FileMode.OpenOrCreate, FileAccess.ReadWrite);

        using (StreamWriter w = new StreamWriter(fs))
        {
            w.Write(json);
        }

        fs.Dispose();
    }
}

[Serializable]
public class DataPoints
{
    public uint amountOfStepsLasted = 0;

    public List<uint> vitalityData = new();
    public List<uint> willpowerData = new();
    public List<uint> sanityData = new();
}
