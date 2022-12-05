using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerInVillage : MonoBehaviour
{
    public static PlayerInfo Player;
    public static GameManagerInVillage Instance;

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
    }

    private void Start()
    {
        PanelManagerInVillage.Instance.HPTimer.text = "00:00:10";
        PanelManagerInVillage.Instance.MPTimer.text = "00:00:10";
    }

    bool ifAutoIncreased = true;
    private void Update()
    {
        if (ifAutoIncreased)
        {
            ifAutoIncreased = false;
            StartCoroutine(HPAutoIncrease());
        }

        for (int i = 0; i < timers.Count; i++)
        {
            timers[i].doAction();
        }
        if (LaterAction != null)
        {
            LaterAction();
        }
    }

    IEnumerator HPAutoIncrease()
    {
        yield return new CustomCR();
        if (Player.Hp < Player.Life) Player.Hp++;
        if (Player.Mp < 100) Player.Mp++;
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

    public List<CustomTimer> timers = new List<CustomTimer>();
    public Action LaterAction;
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

public class CustomTimer
{
    List<Text> t;
    float f;
    float deadline;
    AnimationCurve c;

    public CustomTimer(List<Text> t, float f, float deadline, AnimationCurve c)
    {
        this.t = t;
        this.f = f;
        this.deadline = deadline;
        this.c = c;
    }

    public void doAction()
    {
        f += Time.deltaTime;
        if (f >= deadline)
        {
            GameManagerInVillage.Instance.LaterAction += () => { GameManagerInVillage.Instance.timers.Remove(this); };
            return;
        }
        foreach (var item in t)
        {
            item.text = c.Evaluate(f).ToString();
        }
    }
}