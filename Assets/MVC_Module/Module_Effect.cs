using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using XLua;

public class Module_Effect
{
    public static Dictionary<int, Module_Effect> BaseEffectList = new Dictionary<int, Module_Effect>();
    //public SlotType slotType;

    private int _id;
    private string _name;
    private string _profile;
    private int _duration;
    private int _interval;

    private Action<Module_PlayerInfo> affect;
    private float timer;
    private float totalTimer;

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

    public EventHandler<int> DurationHandler;
    public int Duration
    {
        get => _duration; set
        {
            _duration = value;
            if (DurationHandler != null)
            {
                DurationHandler(null, _duration);
            }
        }
    }

    public EventHandler<int> InvervalHandler;
    public int Inverval
    {
        get => _interval; set
        {
            _interval = value;
            if (InvervalHandler != null)
            {
                InvervalHandler(null, _interval);
            }
        }
    }

    public Action<Module_PlayerInfo> Affect { get => affect; set => affect = value; }
    public float Timer { get => timer; set => timer = value; }
    public float TotalTimer { get => totalTimer; set => totalTimer = value; }

    public Module_Effect(int id)
    {
        GetDataFromCSV(id); 
    }

    public Module_Effect()
    {

    }


    private void GetDataFromCSV(int id)
    {
        TextAsset t = Resources.Load<TextAsset>("Effect");
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
    //public virtual Module_Effect Clone()
    //{
    //    Module_Effect item = new Module_Effect();
    //    PropertyInfo[] propertys = item.GetType().GetProperties();
    //    for (int i = 0; i < propertys.Length; i++)
    //    {
    //        propertys[i].SetValue(item, propertys[i].GetValue(this));
    //    }
    //    return item;
    //}

    public void Refresh()
    {
        PropertyInfo[] propertys = typeof(Module_Effect).GetProperties();
        for (int i = 0; i < propertys.Length; i++)
        {
            propertys[i].SetValue(this, propertys[i].GetValue(this));
        }
    }
}

[Hotfix]
public class Effect_AboutHealth : Module_Effect
{
    public Effect_AboutHealth() : base()
    {
        base.Affect += action;
    }

    [LuaCallCSharp]
    public void action(Module_PlayerInfo targetPerson)
    {
        targetPerson.Hp += 10;
        Debug.Log($"trigger Effect_AboutHealth, remain {base.Duration-base.TotalTimer},interval {base.Inverval}");
    }

}


