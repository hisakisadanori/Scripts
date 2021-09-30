using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GlowData
{
    public List<string> day;
    public List<int> water;
    public List<int> fert;
    public List<float> pre;
    public List<float> temp;

    public bool Once = true;

}
public class GlowSave : MonoBehaviour
{
    public void Save(GlowData data, string filename)
    {
        StreamWriter writer;
        string jsonData = JsonUtility.ToJson(data);
#if UNITY_EDITOR
        writer = new StreamWriter(Application.dataPath + "/" + filename, false);
#else
        writer = new StreamWriter(Application.persistentDataPath + "/" + filename, false);
#endif

        writer.Write(jsonData);
        writer.Flush();
        writer.Close();
    }

    public GlowData Load(string filename)
    {
        string data = "";
        StreamReader reader;
#if UNITY_EDITOR
        reader = new StreamReader(Application.dataPath + "/" + filename);
#else
        reader = new StreamReader(Application.persistentDataPath + "/" + filename);
#endif

        data = reader.ReadToEnd();
        reader.Close();

        return JsonUtility.FromJson<GlowData>(data);
    }

    public void ResetData(string filename)
    {
        // 初期状態のデータをセーブ
        GlowData data = new GlowData();
        Save(data,filename);
    }
}
