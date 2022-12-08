using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager
{
    public GameObject SlotGrouper;
    public SlotType slotType;

    public List<Slot> SlotList;
    private Slot avaliableSlot;

    public SlotManager(GameObject slotsGO, SlotType slotType)
    {
        SlotGrouper = slotsGO;

        SlotList = new List<Slot>();
        for (int i = 0; i < SlotGrouper.transform.childCount; i++)
        {
            if (slotType == SlotType.Inventory)
            {
                SlotList.Add(new Slot() { SlotObject = SlotGrouper.transform.GetChild(i).gameObject, itemType = ItemType.All });
            }
            if (slotType == SlotType.Equipment)
            {
                SlotList.Add(new Slot() { SlotObject = SlotGrouper.transform.GetChild(i).gameObject, itemType = (ItemType)i });
            }
            if (slotType == SlotType.Quest)
            {
                SlotList.Add(new Slot() { SlotObject = SlotGrouper.transform.GetChild(i).gameObject });
            }
        }
        this.slotType = slotType;
    }

    public bool TryGetObjectFrom(Module_ItemInfo i, SlotManager fromSlotManager)
    {
        if (GetAvaliableSlot(out avaliableSlot, i.Type))
        {

            i.c.view.gameObject.transform.SetParent(avaliableSlot.SlotObject.transform);
            i.c.view.gameObject.transform.localPosition = Vector3.zero;
            i.c.view.slotType = this.slotType;

            //fromSlotManager.RemoveRecord(i);
            return true;
        }
        return false;
    }

    public void TryAddItem(Module_ItemInfo i)
    {
        if (GetAvaliableSlot(out avaliableSlot, i.Type))
        {
            GameObject go = GameManagerInVillage.Instance.CustomInstantiate(Resources.Load<GameObject>("Item"), avaliableSlot.SlotObject.transform);
            View_ItemInfo v = go.AddComponent<View_ItemInfo>();
            Control_ItemInfo c = new Control_ItemInfo(i, v);
            v.slotType = this.slotType;
            c.module.Refresh();
            SlotList[SlotList.IndexOf(avaliableSlot)].ic = c;
        }
    }

    public void TryAddQuest(Module_QuestInfo i)
    {
        GameObject go = GameManagerInVillage.Instance.CustomInstantiate(Resources.Load<GameObject>("Quest"), SlotGrouper.transform);
        View_QuestInfo v = go.AddComponent<View_QuestInfo>();
        Control_QuestInfo c = new Control_QuestInfo(i, v);
        v.slotType = this.slotType;
        c.module.Refresh();
        //SlotList[SlotList.IndexOf(avaliableSlot)].qc = c;

        //MyUtil.FindOneInChildren(go.transform, "QuestTypeImage").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(i.Profile);
        //MyUtil.FindOneInChildren(go.transform, "QuestImage").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(i.Profile);
        //MyUtil.FindOneInChildren(go.transform, "QuestName").gameObject.GetComponent<Text>().text = i.Name;
        //MyUtil.FindOneInChildren(go.transform, "QuestDescription").gameObject.GetComponent<Text>().text = i.Description;
        //i.StatusUI = MyUtil.FindOneInChildren(go.transform, "StatusValue").gameObject.GetComponent<Text>();
        //i.SetStatus(Module_QuestInfo.Accept);
        //i.NPCPosition = GameObject.Find("NPC_Example").transform.position;
        //MyUtil.FindOneInChildren(go.transform, "Status").GetComponent<Button>().onClick.AddListener(() =>
        //{
        //    i.SetStatus(Module_QuestInfo.Abandon);
        //    GameObject.Find("Ch36_nonPBR").GetComponent<PlayMoveScript>().isAutoNav = true;
        //    GameObject.Find("Ch36_nonPBR").GetComponent<PlayMoveScript>().navMeshAgent.SetDestination(i.NPCPosition);
        //});
    }

    public void TryAddSkill(SkillInfo i)
    {
        GameObject go = GameManagerInVillage.Instance.CustomInstantiate(Resources.Load<GameObject>("Skill"), SlotGrouper.transform);
        go.GetComponent<SkillDragScript>().profile = go.GetComponentInChildren<Image>();
        go.GetComponent<SkillDragScript>().name = MyUtil.FindOneInChildren(PanelManagerInVillage.Instance.Panels[NameMap.PanelSkill].GameObjectPanel.transform, "SkillNameUI").GetComponent<Text>();
        go.GetComponent<SkillDragScript>().description = MyUtil.FindOneInChildren(PanelManagerInVillage.Instance.Panels[NameMap.PanelSkill].GameObjectPanel.transform, "SkillDescription").GetComponent<Text>();
        go.GetComponent<SkillDragScript>().hintText = MyUtil.FindOneInChildren(PanelManagerInVillage.Instance.Panels[NameMap.PanelSkill].GameObjectPanel.transform, "HintText").GetComponent<Text>();
        go.GetComponent<SkillDragScript>().UpgradeButton = MyUtil.FindOneInChildren(PanelManagerInVillage.Instance.Panels[NameMap.PanelSkill].GameObjectPanel.transform, "Button_SkillUpgrade").GetComponent<Button>(); ;
        go.GetComponent<SkillDragScript>().Skill = i;
    }

    public void RemoveRecord(Module_ItemInfo i)
    {
        Slot OldSlot = SlotList.Find((slot) =>
        {
            if (slot.ic.module.GetHashCode() == i.GetHashCode())
            {
                return true;
            }
            else
            {
                return false;
            }
        });
        //GameObject.Destroy(OldSlot.controller.view.gameObject);
        OldSlot.ic = null;
    }
    private bool GetAvaliableSlot(out Slot s, ItemType itemType)
    {

        for (int i = 0; i < SlotGrouper.transform.childCount; i++)
        {
            if (SlotGrouper.transform.GetChild(i).childCount == 0)
            {
                s = SlotList.Find((slot) =>
                {
                    if (slot.SlotObject == SlotGrouper.transform.GetChild(i).gameObject
                    && (slot.itemType == ItemType.All || slot.itemType == itemType))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });
                if (s != null)
                {
                    return true;
                }
            }
        }
        s = null;
        return false;
    }
}

public class Slot
{
    public ItemType itemType;
    public GameObject SlotObject;
    public Control_ItemInfo ic;
    public Control_QuestInfo qc;
}

public enum SlotType
{
    Inventory,
    Equipment,
    Quest,
    Skill
}

