using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelManagerInVillage : MonoBehaviour
{
    public AnimationCurve curve;

    public static PanelManagerInVillage Instance;
    public Dictionary<string, CustomPanel> Panels = new Dictionary<string, CustomPanel>();
    public SlotManager PlayerInventory;
    public SlotManager PlayerEquipmnent;
    public SlotManager PlayerQuest;
    public SlotManager PlayerSkill;

    public Text HPTimer;
    public Text MPTimer;
    public Text NotificationText;
    public Text NotificationInput;

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

        HPTimer = GameObject.Find("HPTimerValue").GetComponent<Text>();
        MPTimer = GameObject.Find("MPTimerValue").GetComponent<Text>();

        NotificationText = GameObject.Find("PanelNotification/Text").GetComponent<Text>();
        NotificationInput = GameObject.Find("PanelNotification/InputField/Text").GetComponent<Text>();


        Panels.Add(NameMap.PanelCharacterInfo, new CustomPanel(this.transform.Find(NameMap.PanelCharacterInfo).gameObject));
        Panels.Add(NameMap.PanelNotification, new CustomPanel(this.transform.Find(NameMap.PanelNotification).gameObject));
        Panels.Add(NameMap.PanelEquipmnemt, new CustomPanel(this.transform.Find(NameMap.PanelEquipmnemt).gameObject));
        Panels.Add(NameMap.PanelInventory, new CustomPanel(this.transform.Find(NameMap.PanelInventory).gameObject));
        Panels.Add(NameMap.PanelItemDetail, new CustomPanel(this.transform.Find(NameMap.PanelItemDetail).gameObject));
        Panels.Add(NameMap.PanelEquipDetail, new CustomPanel(this.transform.Find(NameMap.PanelEquipDetail).gameObject));
        Panels.Add(NameMap.PanelMenu, new CustomPanel(this.transform.Find(NameMap.PanelMenu).gameObject));
        Panels.Add(NameMap.PanelQuest, new CustomPanel(this.transform.Find(NameMap.PanelQuest).gameObject));
        Panels.Add(NameMap.PanelSkill, new CustomPanel(this.transform.Find(NameMap.PanelSkill).gameObject));

        PlayerInventory = new SlotManager(MyUtil.FindOneInChildren(GameObject.Find("PanelInventory").transform, "Slots").gameObject, SlotType.Inventory);
        PlayerEquipmnent = new SlotManager(MyUtil.FindOneInChildren(GameObject.Find("PanelEquipmnemt").transform, "Equips").gameObject, SlotType.Equipment);
        PlayerQuest = new SlotManager(MyUtil.FindOneInChildren(GameObject.Find("PanelQuest").transform, "Slots").gameObject, SlotType.Quest);
        PlayerSkill = new SlotManager(MyUtil.FindOneInChildren(GameObject.Find("PanelSkill").transform, "Slots").gameObject, SlotType.Skill);
    }

    void Start()
    {
        Module_PlayerInfo m = new Module_PlayerInfo();
        View_PlayerInfo v = new View_PlayerInfo();
        Control_PlayerInfo c = new Control_PlayerInfo(m, v);

        GameManagerInVillage.Player = c;
        QuestInfo q = new QuestInfo();
        SkillInfo s = new SkillInfo();

        Panels[NameMap.PanelCharacterInfo].GameObjectPanel.SetActive(false);
        Panels[NameMap.PanelNotification].GameObjectPanel.SetActive(false);
        Panels[NameMap.PanelEquipmnemt].GameObjectPanel.SetActive(false);
        Panels[NameMap.PanelInventory].GameObjectPanel.SetActive(false);
        Panels[NameMap.PanelItemDetail].GameObjectPanel.SetActive(false);
        Panels[NameMap.PanelEquipDetail].GameObjectPanel.SetActive(false);
        Panels[NameMap.PanelMenu].GameObjectPanel.SetActive(false);
        Panels[NameMap.PanelQuest].GameObjectPanel.SetActive(false);
        Panels[NameMap.PanelSkill].GameObjectPanel.SetActive(false);
    }

    private void SetPanel(string panelName, PanelAction a)
    {
        if (Panels[panelName].animation == null) return;
        switch (a)
        {
            case PanelAction.Disable:
                Panels[panelName].animation.Play(Panels[panelName].list[0].name);
                break;
            case PanelAction.Enable:
                Panels[panelName].animation.Play(Panels[panelName].list[1].name);
                break;
            default:
                break;
        }
    }
    public void ChangePanel(string from, string to, Action a = null)
    {
        if (from != null)
        {
            SetPanel(from, PanelAction.Disable);

            StartCoroutine(CR_WaitOneSecond(() =>
            {
                if (a != null) a();
                Panels[from].GameObjectPanel.SetActive(false);
                if (to != null)
                {
                    Panels[to].GameObjectPanel.SetActive(true);
                    SetPanel(to, PanelAction.Enable);
                }
            }, Panels[from].animation == null ? 0 : Panels[from].list[0].length
            ));
        }
        else
        {
            if (a != null) a();
            Panels[to].GameObjectPanel.SetActive(true);
            SetPanel(to, PanelAction.Enable);
        }
    }

    public IEnumerator CR_WaitOneSecond(Action a, float seconds)
    {
        if (seconds == 0)
        {
            a();
        }
        yield return new WaitForSeconds(seconds);
        if (seconds != 0)
        {
            a();
        }
    }
}

public enum PanelAction
{
    Disable,
    Enable
}




