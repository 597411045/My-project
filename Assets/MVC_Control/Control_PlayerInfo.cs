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

    public SlotManager PlayerInventory;
    public SlotManager PlayerEquipmnent;
    public SlotManager PlayerQuest;

    public Control_PlayerInfo(Module_PlayerInfo player, View_PlayerInfo view, PlayerType p)
    {
        this.module = player;
        this.module.c = this;
        this.view = view;
        this.view.c = this;

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
        module.Refresh();

        if (p == PlayerType.Player)
        {
            PlayerInventory = new SlotManager(MyUtil.FindOneInChildren(GameObject.Find("PanelInventory").transform, "Slots").gameObject, SlotType.Inventory);
            AddPlayerItemToSlotManager();

            PlayerEquipmnent = new SlotManager(MyUtil.FindOneInChildren(GameObject.Find("PanelEquipmnemt").transform, "Equips").gameObject, SlotType.Equipment);
            AddPlayerEquipToSlotManager();

            PlayerQuest = new SlotManager(MyUtil.FindOneInChildren(GameObject.Find("PanelQuest").transform, "Slots").gameObject, SlotType.Quest);
            AddPlayerQuestToSlotManager();
            BuildPlayerSkill();
        }
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
            PlayerEquipmnent.TryGetObjectFrom(item, PlayerInventory);

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
                PlayerInventory.TryGetObjectFrom(module.Equipments[type], PlayerEquipmnent);
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
        GameManagerInVillage.timers.Add(new DynamicValue(texts, time, ac));
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
        PlayerInventory.TryAddItem(item);
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
        PlayerInventory.RemoveRecord(item);
        return true;
    }

    private void AddPlayerItemToSlotManager()
    {
        foreach (var i in module.Inventory)
        {
            PlayerInventory.TryAddItem(i.Value);
        }
    }

    private void AddPlayerEquipToSlotManager()
    {
        foreach (var i in module.Equipments)
        {
            if (i.Value != null) PlayerEquipmnent.TryAddItem(i.Value);
        }
    }

    private void AddPlayerQuestToSlotManager()
    {
        foreach (var i in module.Quests)
        {
            if (i.Value != null) PlayerQuest.TryAddQuest(i.Value);
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

public class BaseUpdateAction
{
    protected bool isStoped;
    protected float f;
    float deadline;
    public Action action;

    public BaseUpdateAction(float deadline)
    {
        this.deadline = deadline;
        action += () =>
        {
            if (this.f >= deadline)
            {
                GameManagerInVillage.LaterAction += () => { GameManagerInVillage.timers.Remove(this); };
                isStoped = true;
            }
        };
    }
}

public class DynamicValue : BaseUpdateAction
{
    List<Text> t;
    AnimationCurve c;

    public DynamicValue(List<Text> t, float deadline, AnimationCurve c) : base(deadline)
    {
        this.t = t;
        this.c = c;
        action += doAction;
    }

    public void doAction()
    {
        if (isStoped == false)
        {
            f += Time.deltaTime;
            foreach (var item in t)
            {
                item.text = c.Evaluate(f).ToString();
            }
        }
    }
}