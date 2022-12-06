using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Module_PlayerInfo
{
    public static string LocalUsername;
    public static string LocalPassword;
    public static int CharacterId = 0;
    public static string CurrentCharacterName = "Default";
    public Control_PlayerInfo c;

    private string _name;
    private string _profile;
    private int _level = 1;
    private int _power = 0;
    private int _exp = 0;
    private int _diamond = 0;
    private int _coin = 0;
    private int _hp = 0;
    private int _mp = 0;
    private int _atk = 0;
    private int _life = 0;

    //public delegate void NameHandlerDelegate(string s);
    //public EventHandler NameHandler;
    public EventHandler<string> NameHandler;
    public string Name
    {
        get { return _name; }
        set
        {
            _name = value;
            if (NameHandler != null)
            {
                //PlayerArgs args = new PlayerArgs() { Name = _name };
                NameHandler(null, _name);
            }
        }
    }

    public EventHandler<string> ProfileHandler;
    public string Profile
    {
        get { return _profile; }
        set
        {
            _profile = value;
            if (ProfileHandler != null)
            {
                ProfileHandler(null, _profile);
            }
        }
    }

    public EventHandler<int> LevelHandler;
    public int Level
    {
        get { return _level; }
        set
        {
            _level = value;
            if (LevelHandler != null)
            {
                LevelHandler(null, _level);
            }
        }
    }

    public EventHandler<int> PowerHandler;
    public int Power
    {
        get { return _power; }
        set
        {
            _power = value;
            if (PowerHandler != null)
            {
                PowerHandler(null, _power);
            }
        }
    }

    public EventHandler<int> ExpHandler;
    public int Exp
    {
        get { return _exp; }
        set
        {
            _exp = value;
            if (ExpHandler != null)
            {
                ExpHandler(null, _exp);
            }
        }
    }

    public EventHandler<int> DiamondHandler;
    public int Diamond
    {
        get { return _diamond; }
        set
        {
            _diamond = value;
            if (DiamondHandler != null)
            {
                DiamondHandler(null, _diamond);
            }
        }
    }

    public EventHandler<int> CoinHandler;
    public int Coin
    {
        get { return _coin; }
        set
        {
            _coin = value;
            if (CoinHandler != null)
            {
                CoinHandler(null, _coin);
            }
        }
    }

    public EventHandler<int> HpHandler;
    public int Hp
    {
        get { return _hp; }
        set
        {
            _hp = value;
            if (HpHandler != null)
            {
                HpHandler(null, _hp);
            }
        }
    }

    public EventHandler<int> MpHandler;
    public int Mp
    {
        get { return _mp; }
        set
        {
            _mp = value;
            if (MpHandler != null)
            {
                MpHandler(null, _mp);
            }
        }
    }

    public EventHandler<int> LifeHandler;
    public int Life
    {
        get { return _life; }
        set
        {
            _life = value;
            if (LifeHandler != null)
            {
                LifeHandler(null, _life);
            }
        }
    }

    public EventHandler<int> AtkHandler;
    public int Atk
    {
        get { return _atk; }
        set
        {
            _atk = value;
            if (AtkHandler != null)
            {
                AtkHandler(null, _atk);
            }
        }
    }

    public Dictionary<ItemType, Module_ItemInfo> Equipments;
    public Dictionary<int, Module_ItemInfo> Inventory;
    public Dictionary<int, QuestInfo> Quests;
    public Dictionary<int, SkillInfo> Skill;

    public Module_PlayerInfo()
    {
        Name = "DefaultPlayerName";
        Coin = 0;
        Diamond = 0;
        Hp = 0;
        Mp = 0;
        Level = 0;
        Power = 0;
        Exp = 0;
        Life = 0;
        Atk = 0;

        Equipments = new Dictionary<ItemType, Module_ItemInfo>() {
            {ItemType.Head,null },
            {ItemType.Cloth,null },
            {ItemType.Weapon,null },
            {ItemType.Shoes,null },
            {ItemType.Necklace,null },
            {ItemType.Bracelet,null },
            {ItemType.Ring,null },
            {ItemType.Wing,null }
        };
        Inventory = new Dictionary<int, Module_ItemInfo>();
        Skill = new Dictionary<int, SkillInfo>();
        //GetBaseValue();
        //BuildRandomPlayerInventory();//GetInventory();
        //BuildRandomPlayerEquipments();//GetEquipments();
    }

    //private void GetBaseValue() { }
    //private void GetEquipments() { }
    //void GetInventory() { }
    
    public void Refresh()
    {
        PropertyInfo[] propertys = typeof(Module_PlayerInfo).GetProperties();
        for (int i = 0; i < propertys.Length; i++)
        {
            propertys[i].SetValue(this, propertys[i].GetValue(this));
        }
    }
}
