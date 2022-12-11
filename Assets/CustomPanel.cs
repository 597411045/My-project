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

    public virtual void SetPlayer(View_PlayerInfo value) { }
    public virtual void SetItem(View_ItemInfo value) { }

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

public class PanelForPlayer : CustomPanel
{
    private Text ItemName;
    private Image ItemImage;
    private Text ItemDescription;
    private Text DialogicChoice1;
    private Text DialogicChoice2;
    private Text DialogicChoice3;
    private Button DialogC1BG;
    private Button DialogC2BG;
    private Button DialogC3BG;

    public override void SetPlayer(View_PlayerInfo value)
    {
        {
            if (ItemName != null) value.NameUI.Add(ItemName);
            if (ItemImage != null) value.ProfileUI.Add(ItemImage);
            if (ItemDescription != null) value.DialogueUI = ItemDescription;
            if (DialogicChoice1 != null) value.DialogicChoice1 = DialogicChoice1;
            DynamicButton(DialogicChoice1, DialogC1BG, value.c.module, 0);
            if (DialogicChoice2 != null) value.DialogicChoice2 = DialogicChoice2;
            DynamicButton(DialogicChoice1, DialogC2BG, value.c.module, 1);
            if (DialogicChoice3 != null) value.DialogicChoice3 = DialogicChoice3;
            DynamicButton(DialogicChoice1, DialogC3BG, value.c.module, 2);
            value.c.module.Refresh();
            value.NameUI.Remove(ItemName);
            value.ProfileUI.Remove(ItemImage);
            value.DialogueUI = null;
            value.DialogicChoice1 = null;
            value.DialogicChoice2 = null;
            value.DialogicChoice3 = null;
        }
    }

    public PanelForPlayer(GameObject go, CustomPanelState s = 0) : base(go, s)
    {
        ItemImage = MyUtil.FindOneInChildren(GameObjectPanel.transform, "ItemProfileUI")?.gameObject.GetComponent<Image>();
        ItemName = MyUtil.FindOneInChildren(GameObjectPanel.transform, "ItemNameUI")?.gameObject.GetComponent<Text>();
        ItemDescription = MyUtil.FindOneInChildren(GameObjectPanel.transform, "ItemDescriptionUI")?.gameObject.GetComponent<Text>();
        DialogicChoice1 = MyUtil.FindOneInChildren(GameObjectPanel.transform, "DialogicChoice1")?.gameObject.GetComponent<Text>();
        DialogicChoice2 = MyUtil.FindOneInChildren(GameObjectPanel.transform, "DialogicChoice2")?.gameObject.GetComponent<Text>();
        DialogicChoice3 = MyUtil.FindOneInChildren(GameObjectPanel.transform, "DialogicChoice3")?.gameObject.GetComponent<Text>();
        DialogC1BG = MyUtil.FindOneInChildren(GameObjectPanel.transform, "DialogC1BG")?.gameObject.GetComponent<Button>();
        DialogC2BG = MyUtil.FindOneInChildren(GameObjectPanel.transform, "DialogC2BG")?.gameObject.GetComponent<Button>();
        DialogC3BG = MyUtil.FindOneInChildren(GameObjectPanel.transform, "DialogC3BG")?.gameObject.GetComponent<Button>();
    }

    private void DynamicButton(Text DialogicChoice1, Button DialogC1BG, Module_PlayerInfo p, int i)
    {
        if (DialogicChoice1 != null)
        {
            DialogC1BG.onClick.RemoveAllListeners();
            DialogC1BG.onClick.AddListener(() =>
            {
                if (p.dialogList[p.dialogueIndex][i]._choiceAction != null)
                {
                    p.dialogList[p.dialogueIndex][i]._choiceAction(p.dialogueIndex,i);
                    //p.dialogList[p.dialogueIndex][i]._choiceAction = null;
                }
            });
            if (p.dialogList[p.dialogueIndex][i]._nextid == 0)
            {
                DialogC1BG.gameObject.SetActive(false);
            }
            else
            {
                DialogC1BG.gameObject.SetActive(true);
            }
        }
    }

}

public class PanelForItem : CustomPanel
{
    private Image ItemImage;
    private Text ItemName;
    private Text ItemDescription;
    private Text ItemLevel;
    private Text ItemATK;
    private Text ItemLife;
    private Text ItemPower;

    public override void SetItem(View_ItemInfo value)
    {
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


    public PanelForItem(GameObject go, CustomPanelState s = 0) : base(go, s)
    {
        ItemImage = MyUtil.FindOneInChildren(GameObjectPanel.transform, "ItemProfileUI")?.gameObject.GetComponent<Image>();
        ItemName = MyUtil.FindOneInChildren(GameObjectPanel.transform, "ItemNameUI")?.gameObject.GetComponent<Text>();
        ItemDescription = MyUtil.FindOneInChildren(GameObjectPanel.transform, "ItemDescriptionUI")?.gameObject.GetComponent<Text>();
        ItemLevel = MyUtil.FindOneInChildren(GameObjectPanel.transform, "ItemLevelUI")?.gameObject.GetComponent<Text>();
        ItemATK = MyUtil.FindOneInChildren(GameObjectPanel.transform, "ItemAtkUI")?.gameObject.GetComponent<Text>();
        ItemLife = MyUtil.FindOneInChildren(GameObjectPanel.transform, "ItemLifeUI")?.gameObject.GetComponent<Text>();
        ItemPower = MyUtil.FindOneInChildren(GameObjectPanel.transform, "ItemPowerUI")?.gameObject.GetComponent<Text>();
    }
}
