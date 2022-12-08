using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public static PanelManager Instance;
    public Dictionary<string, CustomPanel> aniPanels = new Dictionary<string, CustomPanel>();
    public List<CustomModel> Models = new List<CustomModel>();

    public TextMeshProUGUI usernameInPanelStart;
    public TextMeshProUGUI characterNameInPanelCharacter;
    public TextMeshProUGUI servernameInPanelStart;
    public static GameObject ChosenServerUnit;
    AssetBundle ab;

    GameObject RolePositionInPanelCharacter;
    GameObject Wheel;

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

        aniPanels.Add(NameMap.PanelLogin, new CustomPanel(this.transform.Find(NameMap.PanelLogin).gameObject,CustomPanelState.ifOpen));
        aniPanels.Add(NameMap.PanelRegister, new CustomPanel(this.transform.Find(NameMap.PanelRegister).gameObject, CustomPanelState.ifOpen));
        aniPanels.Add(NameMap.PanelStart, new CustomPanel(this.transform.Find(NameMap.PanelStart).gameObject, CustomPanelState.ifOpen));
        aniPanels.Add(NameMap.PanelServerList, new CustomPanel(this.transform.Find(NameMap.PanelServerList).gameObject, CustomPanelState.ifOpen));
        aniPanels.Add(NameMap.PanelCharacter, new CustomPanel(this.transform.Find(NameMap.PanelCharacter).gameObject, CustomPanelState.ifOpen));
        aniPanels.Add(NameMap.PanelSelectCharacter, new CustomPanel(this.transform.Find(NameMap.PanelSelectCharacter).gameObject, CustomPanelState.ifOpen));

        usernameInPanelStart = MyUtil.FindOneInChildren(GameObject.Find("PanelStart").transform, "UserName").gameObject.GetComponentInChildren<TextMeshProUGUI>();
        servernameInPanelStart = GameObject.Find("ServerName").GetComponentInChildren<TextMeshProUGUI>();
        characterNameInPanelCharacter = MyUtil.FindOneInChildren(GameObject.Find("PanelCharacter").transform, "CharacterName").gameObject.GetComponentInChildren<TextMeshProUGUI>();
        characterNameInPanelCharacter.text = Module_PlayerInfo.CurrentCharacterName;
        ServerListContent = GameObject.Find("Content");
        ChosenServerUnit = GameObject.Find("ChosenServerUnit");

        string path = @"\..\AssetBundles\StandaloneWindows\001-startmenu";
        ab = AssetBundle.LoadFromFile(Application.dataPath + path);

        RolePositionInPanelCharacter = MyUtil.FindOneInChildren(GameObject.Find(NameMap.PanelCharacter).transform, "RolePosition").gameObject;
        Wheel = MyUtil.FindOneInChildren(GameObject.Find(NameMap.PanelSelectCharacter).transform, "Wheel").gameObject;
        for (int i = 0; i < Wheel.transform.childCount; i++)
        {
            Models.Add(new CustomModel(Wheel.transform.GetChild(i).GetChild(0).gameObject));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        MoveRoleFromPanelSelectCharacter();

        aniPanels[NameMap.PanelRegister].GameObjectPanel.SetActive(false);
        aniPanels[NameMap.PanelStart].GameObjectPanel.SetActive(false);
        aniPanels[NameMap.PanelServerList].GameObjectPanel.SetActive(false);
        aniPanels[NameMap.PanelCharacter].GameObjectPanel.SetActive(false);
        aniPanels[NameMap.PanelSelectCharacter].GameObjectPanel.SetActive(false);
    }

    public void SetPanel(string panelName, bool b)
    {
        if (b)
        {

            aniPanels[panelName].animation.Play(aniPanels[panelName].list[1].name);

        }
        else
        {

            aniPanels[panelName].animation.Play(aniPanels[panelName].list[0].name);
        }
    }
    public void ChangePanel(string from, string to)
    {
        //PanelManager.Instance.SetPanel(from, false);
        //StartCoroutine(PanelManager.Instance.CR_WaitOneSecond(() =>
        //{
        //    PanelManager.Instance.aniPanels[to].animation.gameObject.SetActive(true);
        //    PanelManager.Instance.SetPanel(to, true);

        //    PanelManager.Instance.aniPanels[from].animation.gameObject.SetActive(false);
        //}));
        if (from != null)
        {
            PanelManager.Instance.SetPanel(from, false);

            StartCoroutine(PanelManager.Instance.CR_WaitOneSecond(() =>
            {
                if (to != null)
                {
                    PanelManager.Instance.aniPanels[to].GameObjectPanel.SetActive(true);
                    PanelManager.Instance.SetPanel(to, true);
                }
                PanelManager.Instance.aniPanels[from].GameObjectPanel.SetActive(false);
            }));
        }
        else
        {
            PanelManager.Instance.aniPanels[to].GameObjectPanel.SetActive(true);
            PanelManager.Instance.SetPanel(to, true);
        }
    }


    public IEnumerator CR_WaitOneSecond(Action a)
    {
        yield return new WaitForSeconds(1);
        a();
    }

    GameObject ServerListContent;
    public void InitPanelServerList()
    {
        int count = 15;
        ServerListContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 60 * (count + count % 2) / 2);

        if (ServerListContent.transform.childCount > 0)
        {
            for (int i = 0; i < ServerListContent.transform.childCount; i++)
            {
                Destroy(ServerListContent.transform.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < count; i++)
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("ServerUnit"), ServerListContent.transform);

            go.name = i.ToString() + "Zone";

            ServerUnitScript su = go.GetComponent<ServerUnitScript>();

            su.ip = "127.0.0." + i.ToString();
            su.text = go.GetComponentInChildren<TextMeshProUGUI>();
            su.ServerName = i.ToString() + "Zone";
            su.popularity = UnityEngine.Random.Range(0, 100);
            if (su.popularity < 50)
            {
                go.GetComponent<Image>().sprite = ab.LoadAsset<Sprite>("btn_流畅1");
                SpriteState s = new SpriteState();
                s.highlightedSprite = ab.LoadAsset<Sprite>("btn_流畅2");
                s.pressedSprite = ab.LoadAsset<Sprite>("btn_流畅3");
                s.disabledSprite = ab.LoadAsset<Sprite>("btn_流畅4");
                go.GetComponent<Button>().spriteState = s;
            }
            if (i == 0)
            {
                ChosenServerUnit.GetComponent<Image>().sprite = go.GetComponent<Image>().sprite;
                ChosenServerUnit.GetComponentInChildren<TextMeshProUGUI>().text = go.GetComponentInChildren<TextMeshProUGUI>().text;
            }
        }
    }

    public void MoveRoleFromPanelSelectCharacter()
    {
        Models[Module_PlayerInfo.CharacterId].Model.transform.SetParent(RolePositionInPanelCharacter.transform);
        Models[Module_PlayerInfo.CharacterId].Model.transform.localPosition = Vector3.zero;
        Models[Module_PlayerInfo.CharacterId].ChangeLayer(5);
    }

    public void MoveRoleFromPanelCharacter()
    {
        Models[Module_PlayerInfo.CharacterId].Model.transform.SetParent(Models[Module_PlayerInfo.CharacterId].PositionInPanelSelectCharacter.transform);
        Models[Module_PlayerInfo.CharacterId].Model.transform.localPosition = Vector3.zero;
        Models[Module_PlayerInfo.CharacterId].ChangeLayer(0);
    }
}

