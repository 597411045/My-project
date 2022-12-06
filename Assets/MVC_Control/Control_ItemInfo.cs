using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Control_ItemInfo
{
    public Module_ItemInfo module;
    public View_ItemInfo view;

    public Control_ItemInfo(Module_ItemInfo item, View_ItemInfo view)
    {
        this.module = item;
        this.module.c = this;
        this.view = view;
        this.view.c = this;

        item.IdHandler += view.IdUIAction;
        item.CountHandler += view.CountUIAction;
        item.ProfileHandler += view.ProfileUIAction;
        item.DescriptionHandler += view.DescriptionUIAction;
        module.Refresh();
    }

    public static Module_ItemInfo GetRandomItem()
    {
        Module_ItemInfo item = Module_ItemInfo.BaseItemList[UnityEngine.Random.Range(0, Module_ItemInfo.BaseItemList.Count - 1)].Clone();
        item.Atk = UnityEngine.Random.Range(1, 10);
        item.Life = UnityEngine.Random.Range(1, 10);
        item.Power = item.Atk + item.Life;
        item.Count = 1;
        return item;
    }
}
