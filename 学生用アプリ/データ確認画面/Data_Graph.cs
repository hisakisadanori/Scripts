using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Data_Graph : MonoBehaviour
{
    public string filename = "savedata.json";

    GlowData glowData;
    GlowSave glowSave;

    private RectTransform graph;
    private RectTransform view;
    private float[] precipitation = new float[0];        // 降水量
    private int[] gaveWaterAmount = new int[0];       // 与えた水の量
    private int[] gaveFertilizerAmount = new int[0]; // 与えた肥料の量
    private float offsetX = 50f;
    private Font font;
    private enum placing
    {
        Left,Right,Center
    }
    //private const float OFFSET_X = 10;

    // Start is called before the first frame update
    void Start()
    {
        glowSave = this.GetComponent<GlowSave>();

        font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        //lineRenderers = new List<LineRenderer>();
        graph = gameObject.GetComponent<RectTransform>();
        view = graph.Find("View").GetComponent<RectTransform>();
        //Debug.Log(graph.sizeDelta.x.ToString() + ", " + graph.sizeDelta.y.ToString());


#if UNITY_EDITOR
        if (System.IO.File.Exists(Application.dataPath + "/" + filename) == true)
        {
            glowData = glowSave.Load(filename);
            DataUpdate();
        }
#else
        if (System.IO.File.Exists(Application.persistentDataPath + "/" + filename) == true)
        {
            glowData = glowSave.Load(filename);
            DataUpdate();
        }
#endif

        Render();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // データの更新
    void DataUpdate()
    {
        // NOTE: apiなどからデータを取得
        precipitation = glowData.pre.ToArray();
        gaveWaterAmount = glowData.water.ToArray();
        gaveFertilizerAmount = glowData.fert.ToArray();

        /* For Test
        precipitation = new float[]{ 700, 500, 600, 800, 300, 200, 400, 100 };
        gaveWaterAmount = new int[] { 100, 400, 200, 300, 800, 600, 500, 700 };
        gaveFertilizerAmount = new int[] { 200, 0, 600, 200, 400, 0, 200, 100 };*/

        foreach(var a in precipitation)
        {
            Debug.Log(a);
        }
        foreach (var a in gaveWaterAmount)
        {
            Debug.Log(a);
        }
        foreach (var a in gaveFertilizerAmount)
        {
            Debug.Log(a);
        }

        if (gaveWaterAmount.Length != 0)
        {
            offsetX = 415 / gaveWaterAmount.Length;
        }
    }

    // 描画計算
    void Render()
    {
        //CreateDot(new Vector2(0, 0), Color.black);
        //CreateLine(new Vector2[] { new Vector2(0, 0), new Vector2(100, 100), new Vector2(200, 50) }, Color.blue);
        CreateReferenceLine(1000, Color.black, 200);
        CreateBarGraph(gaveWaterAmount, 1000, Color.green, placing.Left, 200, "ml");
        CreateBarGraph(gaveFertilizerAmount, 1000, Color.red, placing.Right, 200, "kg");
        CreateLineGraph(precipitation, 1000, Color.blue);
        //CreateText(new Vector2(0, 0), placing.Right, "test", 10, Color.black);
    }

    private void CreateDot(Vector2 position, Color color)
    {
        GameObject dot = new GameObject("dot", typeof(Image));
        dot.GetComponent<Image>().color = color;
        dot.transform.SetParent(view, false);
        RectTransform dotRT = dot.GetComponent<RectTransform>();
        dotRT.anchoredPosition = position;
        dotRT.sizeDelta = new Vector2(10f, 10f);
        dotRT.anchorMin = Vector2.zero;
        dotRT.anchorMax = Vector2.zero;
    }

    private void CreateLine(Vector2[] positions, Color color, float widthMultiplier = 1.0f, int sortingOrder = 0)
    {
        // 新規オブジェクトを生成し、LineRendererコンポーネントを追加
        GameObject objLineRenderer = new GameObject("LineRenderer"/* + lineRenderers.Count.ToString()*/);
        objLineRenderer.AddComponent<RectTransform>();
        objLineRenderer.transform.parent = view.transform;
        RectTransform rtLineRenderer = objLineRenderer.GetComponent<RectTransform>();
        rtLineRenderer.pivot = Vector2.zero;
        rtLineRenderer.localPosition = Vector3.zero;
        rtLineRenderer.localScale = Vector3.one;
        LineRenderer lineRenderer = objLineRenderer.AddComponent<LineRenderer>();

        //lineRenderers.Add(lineRenderer); // test
        lineRenderer.useWorldSpace = false;
        lineRenderer.sortingOrder = sortingOrder;
        lineRenderer.material = new Material(Shader.Find("UI/Default"));
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.widthMultiplier = widthMultiplier;

        // 2次元座標を3次元座標に変換
        Vector3[] _positions = new Vector3[positions.Length];
        for(int i = 0; i < positions.Length; i++) {
            _positions[i] = positions[i];
            _positions[i].z = -0.1f;
        }

        // 線を引く場所を指定する
        Debug.Log(_positions.Length);
        lineRenderer.positionCount = _positions.Length;
        lineRenderer.SetPositions(_positions);
    }

    // 折れ線グラフ描画
    private void CreateLineGraph(float[] values, float maxValue, Color color)
    {
        float coefficientY = view.sizeDelta.y / maxValue;
        Vector2[] positions = new Vector2[values.Length];
        for(int i = 0; i < values.Length; i++) {
            positions[i] = new Vector2(offsetX * i + offsetX / 2, values[i] * coefficientY);
            CreateDot(positions[i], color);
        }
        CreateLine(positions, color, 1.0f, 3);
    }

    // テキスト描画
    private void CreateText(Vector2 position, string str, int fontSize, Color color, Vector2 pivot)
    {
        GameObject objText = new GameObject("text", typeof(Text));
        objText.transform.SetParent(graph, false);
        Text text = objText.GetComponent<Text>();
        text.color = color;
        text.text = str;
        text.font = font;
        text.fontSize = fontSize;
        RectTransform dotRT = objText.GetComponent<RectTransform>();
        dotRT.sizeDelta = new Vector2(text.preferredWidth, text.preferredHeight);
        dotRT.anchorMin = new Vector2(0.5f,0.5f);
        dotRT.anchorMax = new Vector2(0.5f,0.5f);
        dotRT.pivot = pivot;
        dotRT.anchoredPosition = position;
    }
    private void CreateText(Vector2 position, string str, int fontSize, Color color)
    {
        CreateText(position, str, fontSize, color, new Vector2(0.5f, 0.5f));
    }

    private void CreateBar(Vector2 centerPosition, float height, Color color)
    {
        GameObject bar = new GameObject("bar", typeof(Image));
        bar.GetComponent<Image>().color = color;
        bar.transform.SetParent(view, false);
        RectTransform barRT = bar.GetComponent<RectTransform>();
        barRT.anchoredPosition = centerPosition;
        barRT.sizeDelta = new Vector2(10f, height);
        barRT.anchorMin = Vector2.zero;
        barRT.anchorMax = Vector2.zero;
    }

    // 棒グラフ描画
    private void CreateBarGraph(int[] values, float maxValue, Color color, placing place, int referenceValue, string referenceUnit)
    {
        float coefficientY = view.sizeDelta.y / maxValue;

        // 基準線、基準値
        // //基準線が先
        int referenceNum = (int)(maxValue / referenceValue) + 1;
        float textX;
        Vector2 textPivot;
        switch (place) {
            case placing.Left: textX = view.localPosition.x - 5; textPivot = new Vector2(1, 0.5f); break;
            case placing.Right: textX = view.localPosition.x + view.sizeDelta.x + 5; textPivot = new Vector2(0, 0.5f); break;
            default: textX = 0; textPivot = new Vector2(0.5f, 0.5f); break;
        }

        for (int i = 0; i < referenceNum; i++) {
            float posY = view.localPosition.y + referenceValue * coefficientY * i;
            // 基準線を描画 NOTE: 0とreferenceNumの時は描画しない
            /*
            if (i != 0 && i != referenceNum) {
                CreateLine(new Vector2[] { new Vector2(view.localPosition.x, posY), new Vector2(view.localPosition.x + view.sizeDelta.x, posY) }, Color.black, 0.5f);
            }
            */
            // 基準値を描画
            CreateText(new Vector2(textX, posY), (referenceValue * i).ToString() + referenceUnit, 10, Color.black, textPivot);

        }

        // 棒を描画
        float shiftX;
        switch (place) {
            case placing.Left: shiftX = -5f; break;
            case placing.Right: shiftX = 5f; break;
            default: shiftX = 0; break;
        }
        Vector2[] positions = new Vector2[values.Length];
        for (int i = 0; i < values.Length; i++) {
            float height = values[i] * coefficientY;
            positions[i] = new Vector2(offsetX * i + offsetX / 2 + shiftX, height / 2);
            CreateBar(positions[i], height, color);
        }
    }

    private void CreateReferenceLine(float maxValue, Color color, int referenceValue)
    {
        float coefficientY = view.sizeDelta.y / maxValue;
        int referenceNum = (int)(maxValue / referenceValue) + 1;
        for (int i = 0; i < referenceNum; i++) {
            
            // 基準線を描画 NOTE: 0とreferenceNumの時は描画しない
            if (i != 0 && i != referenceNum -1) {
                //float posY = referenceValue * coefficientY * i;
                //CreateLine(new Vector2[] { new Vector2(view.localPosition.x, posY), new Vector2(view.localPosition.x + view.sizeDelta.x, posY) }, Color.black, 0.5f);
                //CreateLine(new Vector2[] { new Vector2(0, posY), new Vector2(view.sizeDelta.x, posY) }, Color.black, 0.5f, 1);

                float posY = /*view.localPosition.y + */referenceValue * coefficientY * i;
                GameObject dot = new GameObject("ReferenceLine", typeof(Image));
                dot.GetComponent<Image>().color = color;
                dot.transform.SetParent(view, false);
                RectTransform dotRT = dot.GetComponent<RectTransform>();
                dotRT.anchoredPosition = new Vector2(0, posY);
                dotRT.sizeDelta = new Vector2(view.sizeDelta.x, 0.5f);
                dotRT.anchorMin = new Vector2(0.5f, 0);
                dotRT.anchorMax = new Vector2(0.5f, 0);
            }
        }
    }
}
