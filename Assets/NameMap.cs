using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

static class NameMap
{
    public static string PanelLogin = "PanelLogin";
    public static string PanelRegister = "PanelRegister";
    public static string PanelStart = "PanelStart";
    public static string PanelServerList = "PanelServerList";
    public static string PanelCharacter = "PanelCharacter";
    public static string PanelSelectCharacter = "PanelSelectCharacter";
    public static string PanelCharacterInfo = "PanelCharacterInfo";
    public static string PanelNotification = "PanelNotification";
    public static string PanelEquipmnemt = "PanelEquipmnemt";
    public static string PanelInventory = "PanelInventory";
    public static string PanelItemDetail = "PanelItemDetail";
    public static string PanelEquipDetail = "PanelEquipDetail";
    public static string PanelMenu = "PanelMenu";
    public static string PanelQuest = "PanelQuest";
    public static string PanelSkill = "PanelSkill";

    public static string PanelFade = "PanelFade";
    public static string PanelShow = "PanelShow";
    public static string PanelLeftSlideIn = "PanelLeftSlideIn";
    public static string PanelLeftSlideOut = "PanelLeftSlideOut";
    public static string PanelCharacterInfoOut = "PanelCharacterInfoOut";
    public static string PanelCharacterInfoIn = "PanelCharacterInfoIn";

    public static string InputUserName = "InputUserName";
    public static string InputPassword = "InputPassword";

    public static string AniTurnLeft = "TurnLeft";
    public static string AniTurnRight = "TurnRight";
}

public class CustomPanel
{
    public GameObject GameObjectPanel;
    public Animation animation;
    public List<AnimationState> list = new List<AnimationState>();

    private Image ItemImage;
    private Text ItemName;
    private Text ItemDescription;
    private Text ItemLevel;
    private Text ItemATK;
    private Text ItemLife;
    private Text ItemPower;

    private ItemInfo item;

    public ItemInfo Item
    {
        get => item;
        set
        {
            item = value;
            if (ItemImage != null) ItemImage.sprite = Resources.Load<Sprite>(item.Profile);
            if (ItemName != null) ItemName.text = item.Name;
            if (ItemDescription != null) ItemDescription.text = item.Description;
            if (ItemLevel != null) ItemLevel.text = item.Level.ToString();
            if (ItemATK != null) ItemATK.text = item.Atk.ToString();
            if (ItemLife != null) ItemLife.text = item.Life.ToString();
            if (ItemPower != null) ItemPower.text = item.Power.ToString();
        }
    }

    public CustomPanel(GameObject go)
    {
        GameObjectPanel = go;
        animation = GameObjectPanel.GetComponent<Animation>();
        if (animation != null)
        {
            foreach (AnimationState state in animation)
            {
                list.Add(state);
            }
        }
        ItemImage = MyUtil.FindTransformInChildren(GameObjectPanel.transform, "ItemImage")?.gameObject.GetComponent<Image>();
        ItemName = MyUtil.FindTransformInChildren(GameObjectPanel.transform, "ItemName")?.gameObject.GetComponent<Text>();
        ItemDescription = MyUtil.FindTransformInChildren(GameObjectPanel.transform, "ItemDescription")?.gameObject.GetComponent<Text>();
        ItemLevel = MyUtil.FindTransformInChildren(GameObjectPanel.transform, "ItemLevel")?.gameObject.GetComponent<Text>();
        ItemATK = MyUtil.FindTransformInChildren(GameObjectPanel.transform, "ItemATK")?.gameObject.GetComponent<Text>();
        ItemLife = MyUtil.FindTransformInChildren(GameObjectPanel.transform, "ItemLife")?.gameObject.GetComponent<Text>();
        ItemPower = MyUtil.FindTransformInChildren(GameObjectPanel.transform, "ItemPower")?.gameObject.GetComponent<Text>();
    }
}

public class CustomModel
{
    public GameObject Model;
    public GameObject PositionInPanelSelectCharacter;

    public CustomModel(GameObject a)
    {
        Model = a;
        PositionInPanelSelectCharacter = Model.transform.parent.gameObject;
    }

    public void ChangeLayer(int l)
    {
        MyUtil.ChangeLayerIncludeChildren(Model.transform, l);
    }
}