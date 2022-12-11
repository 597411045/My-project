using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class View_QuestInfo : MonoBehaviour
{
    //Transform DragGO;
    //Transform DragGOParent;
    //Transform lastParent;
    Transform Canvas;
    public SlotType slotType;
    public Control_QuestInfo c;

    public List<Text> IdUI = new List<Text>();
    public List<Text> NameUI = new List<Text>();
    public List<Image> ProfileUI = new List<Image>();
    public List<Image> ItemProfileUI = new List<Image>();
    public List<Text> ItemIdUI = new List<Text>();
    public List<Text> ItemCountUI = new List<Text>();
    public List<Text> DescriptionUI = new List<Text>();
    public List<Text> StatusUI = new List<Text>();


    public void IdUIAction(object sender, int arg)
    {
        foreach (var i in IdUI) { i.text = arg.ToString(); }
    }
    public void NameUIAction(object sender, string arg)
    {
        foreach (var i in NameUI) { i.text = arg.ToString(); }
    }
    public void ProfileUIAction(object sender, string arg)
    {
        foreach (var i in ProfileUI) { i.sprite = Resources.Load<Sprite>(arg); }
    }
    public void RewardItemIdUIAction(object sender, int arg)
    {
        Module_ItemInfo item = new Module_ItemInfo(c.module.RewardItemId);
        ItemProfileUIAction(sender,item.Profile);
        ItemIdUIAction(sender,item.Id);
        ItemCountUIAction(sender,item.Count);
    }
    public void ItemProfileUIAction(object sender, string arg)
    {
        foreach (var i in ItemProfileUI) { i.sprite = Resources.Load<Sprite>(arg); }
    }
    public void ItemIdUIAction(object sender, int arg)
    {
        foreach (var i in ItemIdUI) { i.text = arg.ToString(); }
    }
    public void ItemCountUIAction(object sender, int arg)
    {
        foreach (var i in ItemCountUI) { i.text = arg.ToString(); }
    }
    public void DescriptionUIAction(object sender, string arg)
    {
        foreach (var i in DescriptionUI) { i.text = arg.ToString(); }
    }
    public void StatusUIAction(object sender, QuestStatus arg)
    {
        foreach (var i in StatusUI) { i.text = arg.ToString(); }
    }
    

    private void Awake()
    {
        BuildUIElements();
    }

    public void BuildUIElements()
    {
        Canvas = GameObject.Find("Canvas").transform;
        IdUI.Add(MyUtil.FindOneInChildren(this.transform, "QuestIdUI").GetComponent<Text>());
        NameUI.Add(MyUtil.FindOneInChildren(this.transform, "QuestNameUI").GetComponent<Text>());
        ProfileUI.Add(MyUtil.FindOneInChildren(this.transform, "QuestProfileUI").GetComponent<Image>());
        ItemProfileUI.Add(MyUtil.FindOneInChildren(this.transform, "Item").GetComponent<Image>());
        ItemIdUI.Add(MyUtil.FindOneInChildren(this.transform, "Id").GetComponent<Text>());
        ItemCountUI.Add(MyUtil.FindOneInChildren(this.transform, "Num").GetComponent<Text>());
        DescriptionUI.Add(MyUtil.FindOneInChildren(this.transform, "QuestDescriptionUI").GetComponent<Text>());
        StatusUI.Add(MyUtil.FindOneInChildren(this.transform, "QuestStatusUI").GetComponent<Text>());
    }

    private void OnDestroy()
    {
        c.UnlinkMV();
    }

}
