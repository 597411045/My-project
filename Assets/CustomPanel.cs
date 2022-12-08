using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class CustomPanel
{
    public GameObject GameObjectPanel;
    public Animation animation;
    public List<AnimationState> list = new List<AnimationState>();
    public CustomPanelState state;

    private Image ItemImage;
    private Text ItemName;
    private Text ItemDescription;
    private Text ItemLevel;
    private Text ItemATK;
    private Text ItemLife;
    private Text ItemPower;

    private View_ItemInfo item;
    private View_PlayerInfo player;

    public View_ItemInfo Item
    {
        set
        {
            if (ItemImage != null) value.ProfileUI.Add(ItemImage);
            if (ItemName != null) value.NameUI.Add(ItemName);
            if (ItemDescription != null) value.DescriptionUI.Add(ItemDescription);
            if (ItemLevel != null) value.LevelUI.Add(ItemLevel);
            if (ItemATK != null) value.AtkUI.Add(ItemATK);
            if (ItemLife != null) value.LifeUI.Add(ItemLife);
            if (ItemPower != null) value.PowerUI.Add(ItemPower);
            value.c.module.Refresh();

            value.ProfileUI.Remove(ItemImage);
            value.NameUI.Remove(ItemName);
            value.DescriptionUI.Remove(ItemDescription);
            value.LevelUI.Remove(ItemLevel);
            value.AtkUI.Remove(ItemATK);
            value.LifeUI.Remove(ItemLife);
            value.PowerUI.Remove(ItemPower);
        }
    }

    public View_PlayerInfo Player
    {
        set
        {
            if (ItemName != null) value.NameUI.Add(ItemName);
            value.c.module.Refresh();
            value.NameUI.Remove(ItemName);
        }
    }

    public CustomPanel(GameObject go, CustomPanelState s = 0)
    {
        this.state = s;
        GameObjectPanel = go;
        animation = GameObjectPanel.GetComponent<Animation>();
        if (animation != null)
        {
            foreach (AnimationState state in animation)
            {
                list.Add(state);
            }
        }
        ItemImage = MyUtil.FindOneInChildren(GameObjectPanel.transform, "ItemProfileUI")?.gameObject.GetComponent<Image>();
        ItemName = MyUtil.FindOneInChildren(GameObjectPanel.transform, "ItemNameUI")?.gameObject.GetComponent<Text>();
        ItemDescription = MyUtil.FindOneInChildren(GameObjectPanel.transform, "ItemDescriptionUI")?.gameObject.GetComponent<Text>();
        ItemLevel = MyUtil.FindOneInChildren(GameObjectPanel.transform, "ItemLevelUI")?.gameObject.GetComponent<Text>();
        ItemATK = MyUtil.FindOneInChildren(GameObjectPanel.transform, "ItemAtkUI")?.gameObject.GetComponent<Text>();
        ItemLife = MyUtil.FindOneInChildren(GameObjectPanel.transform, "ItemLifeUI")?.gameObject.GetComponent<Text>();
        ItemPower = MyUtil.FindOneInChildren(GameObjectPanel.transform, "ItemPowerUI")?.gameObject.GetComponent<Text>();
    }
}

[Flags]
public enum CustomPanelState
{
    [Description("Close")]
    ifOpen = 1 << 0,
    [Description("Ignore Cinemachine Restriction")]
    ifIgnore = 1 << 2
}
