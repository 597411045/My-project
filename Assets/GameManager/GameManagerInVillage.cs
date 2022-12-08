using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerInVillage : MonoBehaviour
{
    public static Control_PlayerInfo PlayerControl;
    public static GameObject PlayerObject;

    public static GameManagerInVillage Instance;

    Image Loader;
    Text Progress;
    AsyncOperation ao;

    float FrameTimer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        //Loader = GameObject.Find("Loader").GetComponent<Image>();
        //Progress = GameObject.Find("Progress").GetComponent<Text>();

        PlayerObject = GameObject.Find("Player");
        Module_PlayerInfo m = new Module_PlayerInfo(PlayerType.Player);
        View_PlayerInfo v = new View_PlayerInfo(PlayerType.Player);
        PlayerControl = new Control_PlayerInfo(m, v, PlayerType.Player);

        SkillInfo s = new SkillInfo();
    }

    private void Start()
    {
        PanelManagerInVillage.Instance.HPTimer.text = "00:00:10";
        PanelManagerInVillage.Instance.MPTimer.text = "00:00:10";

        //ao = SceneManager.LoadSceneAsync("Dungeon");
        //ao.allowSceneActivation = false;
    }

    bool ifAutoIncreased = true;
    private void Update()
    {
        //FrameTimer += Time.deltaTime;
        if (ifAutoIncreased)
        {
            ifAutoIncreased = false;
            StartCoroutine(HPAutoIncrease());
        }

        //if (FrameTimer > 0.02)
        //{
        for (int i = 0; i < timers.Count; i++)
        {
            timers[i].action();
        }
        if (LaterAction != null)
        {
            LaterAction();
        }
        //    FrameTimer = 0;
        //}
        //if (true)
        //{
        //    Progress.text = Mathf.Ceil(ao.progress) * 100 + "%";
        //    Loader.fillAmount = Mathf.Ceil(ao.progress);
        //}
    }

    IEnumerator HPAutoIncrease()
    {
        yield return new CustomCR();
        if (PlayerControl.module.Hp < PlayerControl.module.Life) PlayerControl.module.Hp++;
        if (PlayerControl.module.Mp < 100) PlayerControl.module.Mp++;
        PanelManagerInVillage.Instance.HPTimer.text = "00:00:10";
        PanelManagerInVillage.Instance.MPTimer.text = "00:00:10";
        ifAutoIncreased = true;
    }

    public GameObject CustomInstantiate(GameObject go, Transform parent)
    {
        GameObject g = Instantiate(go, parent);
        return g;
    }

    //Dictionary<int, float> timers = new Dictionary<int, float>();
    //Dictionary<int, Action> actions = new Dictionary<int, Action>();

    public static List<BaseUpdateAction> timers = new List<BaseUpdateAction>();
    public static Action LaterAction;
    //public void RegisterProxy(List<Text> t, AnimationCurve c, float deadline)
    //{
    //    timers.Add(new CustomTimer(t, 0, deadline, c));
    //}

}

class CustomCR : CustomYieldInstruction
{
    float timerTotal;
    float timerSeconds;
    public override bool keepWaiting
    {
        get
        {
            timerTotal += Time.deltaTime;
            timerSeconds += Time.deltaTime;
            if (timerTotal > 10)
            {
                return false;
            }
            else if (timerSeconds > 1)
            {
                timerSeconds = 0;
                PanelManagerInVillage.Instance.HPTimer.text = DateTime.Parse(PanelManagerInVillage.Instance.HPTimer.text).AddSeconds(-1).ToString("HH:mm:ss");
                PanelManagerInVillage.Instance.MPTimer.text = DateTime.Parse(PanelManagerInVillage.Instance.MPTimer.text).AddSeconds(-1).ToString("HH:mm:ss");
                return true;
            }
            else
            {

                return true;
            }
        }
    }

    public CustomCR()
    {
        timerTotal = 0;
        timerSeconds = 0;
    }
}





