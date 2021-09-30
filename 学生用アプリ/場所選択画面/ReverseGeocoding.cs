//using Mapbox.Unity.Location;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Networking;
using NCMB;
using UnityEngine.SceneManagement;


public class ReverseGeocoding : MonoBehaviour
{
    const string ApiUrl = "https://aginfo.cgk.affrc.go.jp/ws/rgeocode.php";

    [SerializeField] public static string Prefecture { get; private set; }
    [SerializeField] public static string Municipalities { get; private set; }

    private select_locatin lonlat;


    NCMBUser currentUser;

    private int mode;

    void Start()
    {
        currentUser = NCMBUser.CurrentUser;
        mode = System.Convert.ToInt32(currentUser["mode"]);
    }

    public void GetAddr()
    {
        lonlat = GetComponent<select_locatin>();
        string requestUrl = ApiUrl;

        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
        keyValuePairs["lat"] = lonlat.Location.latitude.ToString();
        keyValuePairs["lon"] = lonlat.Location.longitude.ToString();
        keyValuePairs["ax"] = "10";
        keyValuePairs["ar"] = "10000";
        keyValuePairs["opt"] = "jpr";

        if (keyValuePairs.Count > 0)
        {
            string paramJoinedStr = string.Join("&", keyValuePairs.Select(pair => string.Format("{0}={1}", pair.Key, pair.Value)));
            requestUrl = string.Format("{0}?{1}", requestUrl, paramJoinedStr);
        }

        StartCoroutine(GetRequest(requestUrl));
    }


    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {
                Debug.Log("Received: " + webRequest.downloadHandler.text);

                XDocument xml = XDocument.Parse(webRequest.downloadHandler.text);
                XNamespace nsSys = "http://aginfo.cgk.affrc.go.jp/ts";

                XElement rgeocode = xml.Element(nsSys + "rgeocode");
                if (rgeocode != null)
                {
                    XElement result = rgeocode.Element(nsSys + "result");
                    if (result != null)
                    {
                        XElement prefecture = result.Element(nsSys + "prefecture");
                        if (prefecture != null)
                        {
                            XElement pcode = prefecture.Element(nsSys + "pcode");
                            if (pcode != null)
                            {
                                Debug.Log("pcode.Value : " + pcode.Value);
                            }
                            XElement pname = prefecture.Element(nsSys + "pname");
                            if (pname != null)
                            {
                                Debug.Log("pname.Value : " + pname.Value);
                                Prefecture = pname.Value;
                                Debug.Log(Prefecture);
                            }
                        }
                        XElement municipality = result.Element(nsSys + "municipality");
                        if (municipality != null)
                        {
                            XElement mcode = municipality.Element(nsSys + "mcode");
                            if (mcode != null)
                            {
                                Debug.Log("mcode.Value : " + mcode.Value);
                            }
                            XElement mname = municipality.Element(nsSys + "mname");
                            if (mname != null)
                            {
                                Debug.Log("pname.Value : " + mname.Value);
                                Municipalities = mname.Value;
                                Debug.Log(Municipalities);

                            }
                        }
                    }
                }
            }
        }

        if (mode == 1)
        {
            currentUser["Prefecture"] = Prefecture;
            currentUser["Municipalities"] = Municipalities;
        }
        else if (mode == 2)
        {
            currentUser["Pra_Prefecture"] = Prefecture;
            currentUser["Pra_Municipalities"] = Municipalities;
        }
        currentUser.SaveAsync((NCMBException e) =>
        {
            if (e != null)
            {
                Debug.Log("保存失敗");
            }

            else
            {
                Debug.Log("保存成功!");
            }
        });


        SceneManager.LoadScene("Ready");
        
      
    }
}
