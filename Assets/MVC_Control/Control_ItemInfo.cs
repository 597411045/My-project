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
        this.view = view;
        this.view.c = this;

        item.IdHandler += view.IdUIAction;
        item.NameHandler += view.NameUIAction;
        item.ProfileHandler += view.ProfileUIAction;
        item.TypeHandler += view.TypeUIAction;
        item.ExpHandler += view.ExpUIAction;
        item.LevelHandler += view.LevelUIAction;
        item.QualityHandler += view.QualityUIAction;
        item.PowerHandler += view.PowerUIAction;
        item.PriceHandler += view.PriceUIAction;
        item.AtkHandler += view.AtkUIAction;
        item.LifeHandler += view.LifeUIAction;
        item.CountHandler += view.CountUIAction;
        item.DescriptionHandler += view.DescriptionUIAction;
        module.Refresh();
    }

 
}
