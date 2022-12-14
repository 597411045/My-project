using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using XLua;

public class Module_ItemInfo
{
    //public static List<Module_ItemInfo> BaseItemList = new List<Module_ItemInfo>();
    //public SlotType slotType;

    private int _id;
    private string _name;
    private string _profile;
    private ItemType _type;

    private int _exp;
    private int _level;
    private int _quality;
    private int _power;

    private int _price;
    private int _atk;
    private int _life;

    private int _count;
    private string _description;

    public Dictionary<int, Module_Effect> effectList = new Dictionary<int, Module_Effect>();

    public EventHandler<int> IdHandler;
    public int Id
    {
        get => _id; set
        {
            _id = value;
            if (IdHandler != null)
            {
                IdHandler(null, _id);
            }
        }
    }
    public EventHandler<string> NameHandler;
    public string Name
    {
        get => _name; set
        {
            _name = value;
            if (NameHandler != null)
            {
                NameHandler(null, _name);
            }
        }
    }

    public EventHandler<string> ProfileHandler;
    public string Profile
    {
        get => _profile; set
        {
            _profile = value;
            if (ProfileHandler != null)
            {
                ProfileHandler(null, _profile);
            }
        }
    }

    public EventHandler<ItemType> TypeHandler;
    public ItemType Type
    {
        get => _type; set
        {
            _type = value;
            if (TypeHandler != null)
            {
                TypeHandler(null, _type);
            }
        }
    }
    public EventHandler<int> ExpHandler;
    public int Exp
    {
        get => _exp; set
        {
            _exp = value;
            if (ExpHandler != null)
            {
                ExpHandler(null, _exp);
            }
        }
    }
    public EventHandler<int> LevelHandler;
    public int Level
    {
        get => _level; set
        {
            _level = value;
            if (LevelHandler != null)
            {
                LevelHandler(null, _level);
            }
        }
    }
    public EventHandler<int> QualityHandler;
    public int Quality
    {
        get => _quality; set
        {
            _quality = value;
            if (QualityHandler != null)
            {
                QualityHandler(null, _quality);
            }
        }
    }
    public EventHandler<int> PowerHandler;
    public int Power
    {
        get => _power;
        set
        {
            _power = value;
            if (PowerHandler != null)
            {
                PowerHandler(null, _power);
            }
        }
    }
    public EventHandler<int> PriceHandler;
    public int Price
    {
        get => _price; set
        {
            _price = value;
            if (PriceHandler != null)
            {
                PriceHandler(null, _price);
            }
        }
    }
    public EventHandler<int> AtkHandler;
    public int Atk
    {
        get => _atk; set
        {
            _atk = value;
            if (AtkHandler != null)
            {
                AtkHandler(null, _atk);
            }
        }
    }
    public EventHandler<int> LifeHandler;
    public int Life
    {
        get => _life; set
        {
            _life = value;
            if (LifeHandler != null)
            {
                LifeHandler(null, _life);
            }
        }
    }
    public EventHandler<string> DescriptionHandler;
    public string Description
    {
        get => _description; set
        {
            _description = value;
            if (DescriptionHandler != null)
            {
                DescriptionHandler(null, _description);
            }
        }
    }
    public EventHandler<int> CountHandler;
    public int Count
    {
        get => _count; set
        {
            _count = value;
            if (CountHandler != null)
                CountHandler(null, _count);
        }
    }

    public Module_ItemInfo(int id)
    {
        GetDataFromCSV(id);
    }

    private Module_ItemInfo()
    {
    }

    private void GetDataFromCSV(int id)
    {
        TextAsset t = Resources.Load<TextAsset>("Inventory");
        string[] lines = t.ToString().Replace("\r", "").Split('\n');
        //Module_ItemInfo itemInfo;
        //PropertyInfo[] propertys = itemInfo.GetType().GetProperties();
        PropertyInfo[] propertys = typeof(Module_ItemInfo).GetProperties();
        for (int j = 1; j < lines.Length; j++)
        {
            string[] CSVData = lines[j].Split(',');
            if (int.Parse(CSVData[0]) != id) continue;
            //itemInfo = new Module_ItemInfo();

            for (int i = 0; i < propertys.Length; i++)
            {
                if (propertys[i].PropertyType == typeof(int))
                {
                    propertys[i].SetValue(this, int.Parse(CSVData[i]));
                    continue;
                }
                if (propertys[i].PropertyType == typeof(string))
                {
                    propertys[i].SetValue(this, CSVData[i]);
                    continue;
                }
                if (propertys[i].PropertyType == typeof(ItemType))
                {
                    propertys[i].SetValue(this, Enum.Parse(typeof(ItemType), CSVData[i]));
                    continue;
                }
            }
            //BaseItemList.Add(itemInfo);
        }
    }
    public Module_ItemInfo Clone()
    {
        Module_ItemInfo item = new Module_ItemInfo();
        PropertyInfo[] propertys = item.GetType().GetProperties();
        for (int i = 0; i < propertys.Length; i++)
        {
            propertys[i].SetValue(item, propertys[i].GetValue(this));
        }
        return item;
    }

    public void Refresh()
    {
        PropertyInfo[] propertys = typeof(Module_ItemInfo).GetProperties();
        for (int i = 0; i < propertys.Length; i++)
        {
            propertys[i].SetValue(this, propertys[i].GetValue(this));
        }
    }
}

public enum ItemType
{
    Head,
    Cloth,
    Weapon,
    Shoes,
    Necklace,
    Bracelet,
    Ring,
    Wing,
    Consume,
    All
}

