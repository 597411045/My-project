using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Button_InVillage : MonoBehaviour
{
    public static Button_InVillage Instance;

    List<Transform> Button_Close;
    List<Transform> Button_Menu;
    List<Button> Button_Task;
    Button Rename;
    Button Profile;
    Button OK;
    Button Cancel;
    Button AddCoin;
    Button SkillUpgrade;
    Transform Canvas;

    private void Awake()
    {
        Instance = this;
        Canvas = GameObject.Find("Canvas").transform;
        BuildButtonClose();
        BuildButtonMenu();
        BuildButtonOther();
    }

    private void BuildButtonClose()
    {
        Button_Close = new List<Transform>();
        MyUtil.FindAllInChildren(Button_Close, Canvas, "Button_Close");
        foreach (var i in Button_Close)
        {
            i.gameObject.GetComponent<Button>().onClick.AddListener(() =>
            {

                OnPanelClose(i.gameObject.name.Split('_')[2]);
            });
        }
    }

    private void BuildButtonMenu()
    {
        Button_Menu = new List<Transform>();
        MyUtil.FindAllInChildren(Button_Menu, Canvas, "Button_Menu");
        foreach (var i in Button_Menu)
        {
            i.gameObject.GetComponent<Button>().onClick.AddListener(() =>
            {

                OnPanelOpen(i.gameObject.name.Split('_')[2]);
            });
        }
    }

    private void BuildButtonOther()
    {
        Rename = MyUtil.FindOneInChildren(Canvas, "Button_Rename").GetComponent<Button>();
        Rename.onClick.AddListener(() => { OnRenameClick(Rename); });

        Profile = MyUtil.FindOneInChildren(Canvas, "Button_Profile").GetComponent<Button>();
        Profile.onClick.AddListener(OnProfileClick);

        OK = MyUtil.FindOneInChildren(Canvas, "Button_OK").GetComponent<Button>();
        OK.onClick.AddListener(OnOKInPanelNotification);

        Cancel = MyUtil.FindOneInChildren(Canvas, "Button_Cancel").GetComponent<Button>();
        Cancel.onClick.AddListener(OnCancelInPanelNotification);

        AddCoin = MyUtil.FindOneInChildren(Canvas, "Button_AddCoin").GetComponent<Button>();
        AddCoin.onClick.AddListener(OnAddCoin);

        SkillUpgrade = MyUtil.FindOneInChildren(Canvas, "Button_SkillUpgrade").GetComponent<Button>();
        SkillUpgrade.onClick.AddListener(OnSkillUpgrade);
    }

    public void OnProfileClick()
    {
        PanelManagerInVillage.Instance.ChangePanel(null, NameMap.PanelCharacterInfo);
    }

    public void OnRenameClick(Button b)
    {
        PanelManagerInVillage.Instance.NotificationText.text = "Input New Name";
        PanelManagerInVillage.Instance.ChangePanel(null, NameMap.PanelNotification, () =>
        {
            RectTransform PanelRect = PanelManagerInVillage.Instance.Panels[NameMap.PanelNotification].GameObjectPanel.GetComponent<RectTransform>();
            RectTransform AlignedObjectRect = GameObject.Find("Rename").GetComponent<RectTransform>();
            PanelRect.position = AlignedObjectRect.position;
            PanelRect.anchoredPosition += new Vector2(PanelRect.rect.width / 2 + AlignedObjectRect.rect.width / 2, -PanelRect.rect.height / 2 + AlignedObjectRect.rect.height / 2);
        });
    }

    public void OnOKInPanelNotification()
    {
        GameManagerInVillage.PlayerControl.module.Name = PanelManagerInVillage.Instance.NotificationInput.text;
        PanelManagerInVillage.Instance.ChangePanel(NameMap.PanelNotification, null);
    }

    public void OnCancelInPanelNotification()
    {
        PanelManagerInVillage.Instance.NotificationInput.GetComponentInParent<InputField>().text = "";
        PanelManagerInVillage.Instance.ChangePanel(NameMap.PanelNotification, null);
    }
    public void OnPanelClose(string s)
    {
        PanelManagerInVillage.Instance.ChangePanel(s, null);
    }
    public void OnPanelOpen(string names)
    {
        foreach (string s in names.Split(','))
        {
            PanelManagerInVillage.Instance.ChangePanel(null, s);
        }
    }
    public void OnTaskClick(string names)
    {
        foreach (string s in names.Split(','))
        {
            PanelManagerInVillage.Instance.ChangePanel(null, s);
        }
    }

    public void OnAddCoin()
    {
        GameManagerInVillage.PlayerControl.module.Coin += 50;
    }

    public void OnSkillUpgrade()
    {
        SkillDragScript.SelectedSkill.Upgrade();
    }

    public void OnQuestButtonClick(Module_QuestInfo m)
    {
        if (m.Status == QuestStatus.New)
        {
            GameManagerInVillage.PlayerControl.module.Quests.Add(m.Id, m);

            m.Status = QuestStatus.Doing;
            m.Refresh();

            GameManagerInVillage.PlayerControl.PlayerQuest.TryAcceptQuestFrom(m, NPCScript.NPCQuestUI);
        }

    }

}


