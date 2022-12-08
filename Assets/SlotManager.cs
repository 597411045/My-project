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
                SlotList.Add(new Slot() { SlotGameObject = SlotGrouper.transform.GetChild(i).gameObject, itemType = ItemType.All });
            }
            if (slotType == SlotType.Equipment)
            {
                SlotList.Add(new Slot() { SlotGameObject = SlotGrouper.transform.GetChild(i).gameObject, itemType = (ItemType)i });
            }
            if (slotType == SlotType.Quest)
            {
                SlotList.Add(new Slot() { SlotGameObject = SlotGrouper.transform.GetChild(i).gameObject });
            }
        }
        this.slotType = slotType;
    }

    public void TryAddItem(Module_ItemInfo i)
    {
        if (GetAvaliableSlot(out avaliableSlot, i.Type))
        {
            GameObject go = GameManagerInVillage.Instance.CustomInstantiate(Resources.Load<GameObject>("Item"));
            View_ItemInfo v = go.AddComponent<View_ItemInfo>();
            Control_ItemInfo c = new Control_ItemInfo(i, v);

            go.transform.SetParent(avaliableSlot.SlotGameObject.transform);
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;


            v.slotType = this.slotType;
            c.module.Refresh();
            SlotList[SlotList.IndexOf(avaliableSlot)].ic = c;
        }
    }
    public bool TryGetItemFrom(Module_ItemInfo itemToBeTransfered, SlotManager fromSlotManager)
    {
        if (GetAvaliableSlot(out avaliableSlot, itemToBeTransfered.Type))
        {
            Control_ItemInfo c = fromSlotManager.FindControlByModule(itemToBeTransfered);
            c.view.gameObject.transform.SetParent(avaliableSlot.SlotGameObject.transform);
            c.view.gameObject.transform.localPosition = Vector3.zero;
            c.view.slotType = this.slotType;

            avaliableSlot.ic = c;
            fromSlotManager.Remove(c);
            return true;
        }
        return false;
    }
    public void TryAddQuest(Module_QuestInfo i)
    {
        GameObject go = GameManagerInVillage.Instance.CustomInstantiate(Resources.Load<GameObject>("Quest"));
        View_QuestInfo v = go.AddComponent<View_QuestInfo>();
        Control_QuestInfo c = new Control_QuestInfo(i, v);

        go.transform.SetParent(SlotGrouper.transform);
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = Vector3.one;

        v.slotType = this.slotType;
        c.module.Refresh();

        go.GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            Button_InVillage.Instance.OnQuestButtonClick(i);
        });
    }
    public bool TryAcceptQuestFrom(Module_QuestInfo i, SlotManager fromSlotManager)
    {
        GameObject go = GameManagerInVillage.Instance.CustomInstantiate(Resources.Load<GameObject>("Quest"));
        View_QuestInfo v = go.AddComponent<View_QuestInfo>();
        Control_QuestInfo c = new Control_QuestInfo(i, v);

        go.transform.SetParent(SlotGrouper.transform);
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = Vector3.one;

        v.slotType = this.slotType;
        c.module.Refresh();

        go.GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            Button_InVillage.Instance.OnQuestButtonClick(i);

        });



        return true;
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

    private bool GetAvaliableSlot(out Slot s, ItemType itemType)
    {
        for (int i = 0; i < SlotList.Count; i++)
        {
            if (SlotList[i].ic == null && (SlotList[i].itemType == ItemType.All || SlotList[i].itemType == itemType))
            {
                s = SlotList[i];
                return true;
            }
        }
        //for (int i = 0; i < SlotGrouper.transform.childCount; i++)
        //{
        //    if (SlotGrouper.transform.GetChild(i).childCount == 0)
        //    {
        //        s = SlotList.Find((slot) =>
        //        {
        //            if (slot.SlotGameObject == SlotGrouper.transform.GetChild(i).gameObject
        //            && (slot.itemType == ItemType.All || slot.itemType == itemType))
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        });
        //        if (s != null)
        //        {
        //            return true;
        //        }
        //    }
        //}
        s = null;
        return false;
    }

    public void Remove(Control_ItemInfo c)
    {
        for (int i = 0; i < SlotList.Count; i++)
        {
            if (SlotList[i].ic != null && SlotList[i].ic.module.Id == c.module.Id)
            {
                SlotList[i].ic = null;
                return;
            }
        }
    }

    public Control_ItemInfo FindControlByModule(Module_ItemInfo m)
    {
        for (int i = 0; i < SlotList.Count; i++)
        {
            if (SlotList[i].ic != null && SlotList[i].ic.module.Id == m.Id)
            {
                return SlotList[i].ic;
            }
        }
        return null;
    }

    public void Clear()
    {

    }
}

public class Slot
{
    public ItemType itemType;
    public GameObject SlotGameObject;
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

