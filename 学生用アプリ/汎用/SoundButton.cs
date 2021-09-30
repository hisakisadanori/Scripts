using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundButton : MonoBehaviour
{
    static public SoundButton instance;
    public bool DontDestroyEnabled = true;
    bool firstload;

    [SerializeField] AudioClip BGM;
    [SerializeField] AudioClip ButtonSound;
    [SerializeField] AudioClip DiaryWSound;
    [SerializeField] AudioClip DiaryRSound;
    [SerializeField] AudioClip BackSound;
    

    AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {

            Destroy(gameObject);
        }
    }
    void Start()
    {
        GameObject[] buttons;

        SceneManager.sceneLoaded += OnSceneLoaded;

        //Componentを取得
        audioSource = GetComponent<AudioSource>();

        buttons = GameObject.FindGameObjectsWithTag("Button");
        foreach (var buttonobject in buttons)
        {
            Button button = buttonobject.GetComponent<Button>();
            button.onClick.AddListener(SoundOnButton);
        }

        if (DontDestroyEnabled)
        {
            // Sceneを遷移してもオブジェクトが消えないようにする
            DontDestroyOnLoad(this);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject[] buttons;
        GameObject[] backs;
        GameObject[] DiaryWs;
        GameObject[] DiaryRs;

        buttons = GameObject.FindGameObjectsWithTag("Button");
        foreach (var buttonobject in buttons)
        {
            Button button = buttonobject.GetComponent<Button>();
            button.onClick.AddListener(SoundOnButton);
        }

        backs = GameObject.FindGameObjectsWithTag("Back");
        foreach (var buttonobject in backs)
        {
            Button button = buttonobject.GetComponent<Button>();
            button.onClick.AddListener(SoundOnBack);
        }

        DiaryWs = GameObject.FindGameObjectsWithTag("DiaryW");
        foreach (var buttonobject in DiaryWs)
        {
            Button button = buttonobject.GetComponent<Button>();
            button.onClick.AddListener(SoundOnDiaryW);
        }

        DiaryRs = GameObject.FindGameObjectsWithTag("DiaryR");
        foreach (var buttonobject in DiaryRs)
        {
            Button button = buttonobject.GetComponent<Button>();
            button.onClick.AddListener(SoundOnDiaryR);
        }

        Exceptionhandling(scene);

        if (audioSource.clip == null)
        {
            audioSource.clip = BGM;
            audioSource.Play();
        }
        if (scene.name == "hisaki_scene" || scene.name == "practice_scene")
        {
            audioSource.clip = null;
        }
        

    }

    void Exceptionhandling(Scene scene)
    {
        if (scene.name == "OKADA")
        {
            GameObject parent = GameObject.FindGameObjectWithTag("ParentofButton");

            GameObject child1 = parent.transform.Find("check/Button").gameObject;
            GameObject child2 = parent.transform.Find("check/back").gameObject;

            Button childbutton1 = child1.GetComponent<Button>();
            childbutton1.onClick.AddListener(SoundOnButton);
            Button childbutton2 = child2.GetComponent<Button>();
            childbutton2.onClick.AddListener(SoundOnBack);
        }

        if (scene.name == "hisaki_scene" || scene.name == "practice_scene")
        {
            GameObject parent = GameObject.FindGameObjectWithTag("ParentofButton");

            GameObject child1 = parent.transform.Find("Canvas/check/Button").gameObject;
            GameObject child2 = parent.transform.Find("Canvas/check/back").gameObject;

            Button childbutton1 = child1.GetComponent<Button>();
            childbutton1.onClick.AddListener(SoundOnButton);
            Button childbutton2 = child2.GetComponent<Button>();
            childbutton2.onClick.AddListener(SoundOnBack);
        }

    }

    public void SoundOnButton()
    {
        audioSource.PlayOneShot(ButtonSound);
    }

    public void SoundOnDiaryW()
    {
        audioSource.PlayOneShot(DiaryWSound);
    }

    public void SoundOnDiaryR()
    {
        audioSource.PlayOneShot(DiaryRSound);
    }

    public void SoundOnBack()
    {
        audioSource.PlayOneShot(BackSound);
    }
}
