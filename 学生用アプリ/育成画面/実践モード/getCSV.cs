using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;
using UnityEngine;
using UnityEngine.Networking;
using NCMB;

public class getCSV : MonoBehaviour
{
    // CSVファイルを置いているディレクトリのURL
    private readonly string ServerURL = "https://www.data.jma.go.jp/obd/stats/data/mdrr/pre_rct/alltable/";



    public IEnumerator GetScore(int days, string Prefecture, string Municipalities, Action<List<float>> callback)
    {
        List<float> Precipitation_amount = new List<float>();

        for (int i = 1; i <= days; i++)
        {
            string fileName = "pre24h0" + i.ToString();

            // 1. ダウンロード
            yield return DownloadCSV(fileName);

            // 2. 読み込み
            List<string[]> recordList = ReadCSV(fileName);


            //3. 検索・取り出し
            Municipalities = Municipalities.TrimEnd('市', '町', '村', '区');
            Debug.Log(Municipalities);
            Debug.Log(Prefecture);

            for (int j = 0; j < recordList.Count; j++)
            {
                if (recordList[j][1].Contains(Prefecture))
                {
                    if (recordList[j][2].Contains(Municipalities))
                    {
                        Precipitation_amount.Add(System.Convert.ToSingle(recordList[j][11]));
                    }
                }
            }
        }

        callback(Precipitation_amount);
    }

       

    private IEnumerator DownloadCSV(string fileName)
    {
        // ダウンロード
        
        UnityWebRequest csvDownload = UnityWebRequest.Get(ServerURL + fileName + ".csv");
        yield return csvDownload.SendWebRequest();

        // 失敗時
        if (!string.IsNullOrEmpty(csvDownload.error))
        {
            Debug.Log("取得失敗", gameObject);
        }

        // 成功時
        else
        {
            // ファイル作成
            string filePath = Path.Combine(Application.persistentDataPath, fileName);
            File.WriteAllBytes(filePath, csvDownload.downloadHandler.data);
        }
    }

    private List<string[]> ReadCSV(string fileName)
    {
        // ファイルパス
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        // 読み込み
        string csvText = File.ReadAllText(filePath, Encoding.GetEncoding("shift_jis"));
        StringReader stringReader = new StringReader(csvText);
        List<string[]> recordList = new List<string[]>();

        while (stringReader.Peek() > -1)
        {
            // 1行を取り出す
            string record = stringReader.ReadLine();

            // カンマ区切りの値を配列に格納
            string[] fields = record.Split(',');

            // リストに追加
            recordList.Add(fields);
        }

        // ファイル削除
        File.Delete(filePath);

        // StringReaderを閉じる
        stringReader.Close();

        // リストで出力
        return recordList;
    }

}

