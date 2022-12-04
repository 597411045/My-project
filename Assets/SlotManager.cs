using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager
{
    public GameObject SlotGrouper;
    public List<Slot> SlotList;
    public SlotType slotType;
    private Slot avaliableSlot;

    public SlotManager(GameObject slotsGO, SlotType slotType)
    {
        SlotList = new List<Slot>();
        SlotGrouper = slotsGO;
        for (int i = 0; i < SlotGrouper.transform.childCount; i++)
        {
            if (slotType == SlotType.Inventory)
            {
                SlotList.Add(new Slot() { go = SlotGrouper.transform.GetChild(i).gameObject, itemType = ItemType.All });
            }
            if (slotType == SlotType.Equipment)
            {
                SlotList.Add(new Slot() { go = SlotGrouper.transform.GetChild(i).gameObject, itemType = (ItemType)i });
            }
        }
        this.slotType = slotType;
    }

    public bool TryGetItemFrom(ItemInfo i, SlotManager fromSlotManager)
    {
        if (GetAvaliableSlot(out avaliableSlot, i.Type))
        {
            i.GameObjectItem.transform.SetParent(avaliableSlot.go.transform);
            i.GameObjectItem.transform.localPosition = Vector3.zero;
            i.slotType = this.slotType;

            fromSlotManager.RemoveItem(i);
            return true;
        }
        return false;
    }

    public void TryAddItem(ItemInfo i)
    {
        if (GetAvaliableSlot(out avaliableSlot, i.Type))
        {
            GameObject go = GameManagerInVillage.Instance.CustomInstantiate(Resources.Load<GameObject>("Item"), avaliableSlot.go.transform);
            go.GetComponent<ItemDragScript>().profile = go.GetComponent<Image>();
            go.GetComponent<ItemDragScript>().count = go.transform.Find("Num").gameObject.GetComponentInChildren<Text>();
            go.GetComponent<ItemDragScript>().id = go.transform.Find("Id").gameObject.GetComponentInChildren<Text>();
            go.GetComponent<ItemDragScript>().Item = i;
            go.GetComponent<ItemDragScript>().Item.slotType = this.slotType;
        }
    }

    public void RemoveItem(ItemInfo i, bool flag = false)
    {
        if (flag)
        {
            GameObject.Destroy(i.GameObjectItem);
        }
    }

    private bool GetAvaliableSlot(out Slot slot, ItemType itemType)
    {
        //foreach (KeyValuePair<GameObject, bool> k in SlotList)
        //{
        //    if (k.Value)
        //    {
        //        slot = k.Key;
        //        return true;
        //    }
        //}


        for (int i = 0; i < SlotGrouper.transform.childCount; i++)
        {
            if (SlotGrouper.transform.GetChild(i).childCount == 0)
            {
                slot = SlotList.Find((e) =>
                {
                    if (e.go == SlotGrouper.transform.GetChild(i).gameObject
                    && (e.itemType == ItemType.All || e.itemType == itemType))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });
                if (slot != null)
                {
                    return true;
                }
            }
        }
        slot = null;
        return false;
    }

}

public class Slot
{
    public ItemType itemType;
    public GameObject go;
}

public enum SlotType
{
    Inventory,
    Equipment
}

