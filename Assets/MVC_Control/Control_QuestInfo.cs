using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Control_QuestInfo
{
    public Module_QuestInfo module;
    public View_QuestInfo view;

    public Control_QuestInfo(Module_QuestInfo item, View_QuestInfo view)
    {
        this.module = item;
        this.view = view;
        this.view.c = this;

        LinkMV();

        module.Refresh();
    }

    private void LinkMV()
    {
        this.module.IdHandler += this.view.IdUIAction;
        this.module.NameHandler += this.view.NameUIAction;
        this.module.ProfileHandler += this.view.ProfileUIAction;
        this.module.RewardItemIdHandler += this.view.RewardItemIdUIAction;
        this.module.DescriptionHandler += this.view.DescriptionUIAction;
        this.module.StatusHandler += this.view.StatusUIAction;
    }

    public void UnlinkMV()
    {
        this.module.IdHandler -= this.view.IdUIAction;
        this.module.NameHandler -= this.view.NameUIAction;
        this.module.ProfileHandler -= this.view.ProfileUIAction;
        this.module.RewardItemIdHandler -= this.view.RewardItemIdUIAction;
        this.module.DescriptionHandler -= this.view.DescriptionUIAction;
        this.module.StatusHandler -= this.view.StatusUIAction;
    }

    //public static Module_ItemInfo GetRandomItem()
    //{
    //    Module_ItemInfo item = Module_ItemInfo.BaseItemList[UnityEngine.Random.Range(0, Module_ItemInfo.BaseItemList.Count - 1)].Clone();
    //    item.Atk = UnityEngine.Random.Range(1, 10);
    //    item.Life = UnityEngine.Random.Range(1, 10);
    //    item.Power = item.Atk + item.Life;
    //    item.Count = 1;
    //    return item;
    //}
}
