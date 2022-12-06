using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Control_PlayerInfo
{
    public Module_PlayerInfo module;
    public View_PlayerInfo view;

    public Control_PlayerInfo(Module_PlayerInfo player,View_PlayerInfo view)
    {
        this.module = player;
        this.module.c = this;
        this.view = view;

        player.NameHandler += view.NameUIAction;
        player.ProfileHandler += view.ProfileUIAction;
        player.LevelHandler += view.LevelUIAction;
        player.PowerHandler += view.PowerUIAction;
        player.ExpHandler += view.ExpUIAction;
        player.DiamondHandler += view.DiamondUIAction;
        player.CoinHandler += view.CoinUIAction;
        player.HpHandler += view.HpUIAction;
        player.MpHandler += view.MpUIAction;
        player.LifeHandler += view.LifeUIAction;
        player.AtkHandler += view.AtkUIAction;

        BuildRandomPlayerInventory();
        BuildPlayerSkill();
        module.Refresh();
    }

    bool ifExchange = false;
    float PowerBeforeUnequip;

    public bool TryEquipItem(Module_ItemInfo item)
    {
        if (module.Equipments.ContainsKey(item.Type))
        {
            if (module.Equipments[item.Type] != null)
            {
                PowerBeforeUnequip = module.Power;
                TryUnequipItem(item.Type);
                ifExchange = true;
            }
            module.Equipments[item.Type] = item;

            module.Life = module.Life + module.Equipments[item.Type].Life;
            module.Atk = module.Atk + module.Equipments[item.Type].Atk;
            if (ifExchange)
            {
                RegisterDynamicValueChange(view.PowerUI, PowerBeforeUnequip, module.Life + module.Atk, 3);
            }
            else
            {
                RegisterDynamicValueChange(view.PowerUI, module.Power, module.Life + module.Atk, 3);
            }
            module.Power = module.Life + module.Atk;
            PanelManagerInVillage.Instance.PlayerEquipmnent.TryGetObjectFrom(item, PanelManagerInVillage.Instance.PlayerInventory);

            ifExchange = false;
            return true;
        }
        return false;
    }

    public bool TryUnequipItem(ItemType type)
    {
        if (module.Equipments.ContainsKey(type))
        {
            if (module.Equipments[type] != null)
            {
                module.Life = module.Life - module.Equipments[type].Life;
                module.Atk = module.Atk - module.Equipments[type].Atk;

                if (ifExchange != true)
                {
                    RegisterDynamicValueChange(view.PowerUI, module.Power, module.Life + module.Atk, 3);
                }
                module.Power = module.Life + module.Atk;
                PanelManagerInVillage.Instance.PlayerInventory.TryGetObjectFrom(module.Equipments[type], PanelManagerInVillage.Instance.PlayerEquipmnent);
                module.Equipments[type] = null;
                return true;
            }
        }
        return false;
    }

    private void RegisterDynamicValueChange(List<Text> texts, float from, float to, float time)
    {
        Keyframe k1 = new Keyframe(0, from, 0, (to - from) / time, 0.3f, 0.3f);
        Keyframe k2 = new Keyframe(time, to, (to - from) / time, 0, 0.3f, 0.3f);
        AnimationCurve ac = new AnimationCurve(k1, k2);
        GameManagerInVillage.Instance.timers.Add(new CustomTimer(texts, 0, time, ac));
    }

    public bool TryPickItem(Module_ItemInfo item)
    {
        if (item.Type == ItemType.Consume)
        {
            if (module.Inventory.ContainsKey(item.Id))
            {
                module.Inventory[item.Id].Count++;
                return true;
            }
            else
            {
                module.Inventory.Add(item.Id, item);
            }
        }
        else
        {
            //Inventory.Add(item.specialId, item);
        }
        PanelManagerInVillage.Instance.PlayerInventory.TryAddObject(item);
        return true;
    }
    public bool TryDropItem(Module_ItemInfo item, SlotManager toSlotManager)
    {
        if (module.Inventory.ContainsKey(item.Id))
        {
            module.Inventory[item.Id].Count--;
            if (module.Inventory[item.Id].Count <= 0)
            {
                module.Inventory.Remove(item.Id);
            }
        }
        PanelManagerInVillage.Instance.PlayerInventory.RemoveRecord(item);
        return true;
    }

    private void BuildRandomPlayerInventory()
    {
        for (int i = 0; i < 9; i++)
        {
            Module_ItemInfo item = Control_ItemInfo.GetRandomItem();

            TryPickItem(item);
        }

    }
    private void BuildPlayerSkill()
    {
        for (int i = 0; i < SkillInfo.BaseItemList.Count; i++)
        {
            module.Skill.Add(SkillInfo.BaseItemList[i].Id, SkillInfo.BaseItemList[i].Clone());
            PanelManagerInVillage.Instance.PlayerSkill.TryAddSkill(module.Skill[SkillInfo.BaseItemList[i].Id]);
        }
    }







}

















public class PlayerArgs : EventArgs
{
    public string Name;
    public int Level;
}

