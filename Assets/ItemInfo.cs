using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo
{
    public static List<ItemInfo> BaseItemList = new List<ItemInfo>();
    public GameObject GameObjectItem;
    public SlotType slotType;

    private int _id;
    private string _name;
    private string _profile;
    private ItemType _type;

    private int _exp = 0;
    private int _level = 1;
    private int _quality = 1;
    private int _power = 0;

    private int _price = 0;
    private int _atk = 0;
    private int _life = 0;

    private int _count;
    private string _description;

    public int specialId;
    public Action UpdateCountUI;

    public int Id { get => _id; set => _id = value; }
    public string Name { get => _name; set => _name = value; }
    public string Profile { get => _profile; set => _profile = value; }
    public ItemType Type { get => _type; set => _type = value; }
    public int Exp { get => _exp; set => _exp = value; }
    public int Level { get => _level; set => _level = value; }
    public int Quality { get => _quality; set => _quality = value; }
    public int Power { get => _power; set { _power = value; _power = _life + _atk; } }
    public int Price { get => _price; set => _price = value; }
    public int Atk { get => _atk; set => _atk = value; }
    public int Life { get => _life; set => _life = value; }
    public string Description { get => _description; set => _description = value; }
    public int Count { get => _count; set { _count = value; if (UpdateCountUI != null) UpdateCountUI(); } }

    static ItemInfo()
    {
        GetBaseListFromCSV();
    }
    public static void GetBaseListFromCSV()
    {
        TextAsset t = Resources.Load<TextAsset>("Inventory");
        string[] lines = t.ToString().Replace("\r", "").Split('\n');

        ItemInfo itemInfo = new ItemInfo();
        //PropertyInfo[] propertys = itemInfo.GetType().GetProperties();
        PropertyInfo[] propertys = typeof(ItemInfo).GetProperties();

        for (int j = 1; j < lines.Length; j++)
        {
            string[] propertyCSV = lines[j].Split(',');
            itemInfo = new ItemInfo();

            for (int i = 0; i < propertys.Length; i++)
            {
                if (propertys[i].PropertyType == typeof(int))
                {
                    propertys[i].SetValue(itemInfo, int.Parse(propertyCSV[i]));
                    continue;
                }
                if (propertys[i].PropertyType == typeof(string))
                {
                    propertys[i].SetValue(itemInfo, propertyCSV[i]);
                    continue;
                }
                if (propertys[i].PropertyType == typeof(ItemType))
                {
                    propertys[i].SetValue(itemInfo, Enum.Parse(typeof(ItemType), propertyCSV[i]));
                    continue;
                }
            }
            BaseItemList.Add(itemInfo);
        }
    }

    public ItemInfo Clone()
    {
        ItemInfo item = new ItemInfo();
        PropertyInfo[] propertys = item.GetType().GetProperties();
        for (int i = 0; i < propertys.Length; i++)
        {
            propertys[i].SetValue(item, propertys[i].GetValue(this));
        }
        return item;
    }

    public static ItemInfo GetRandomItem()
    {
        ItemInfo item = ItemInfo.BaseItemList[UnityEngine.Random.Range(0, ItemInfo.BaseItemList.Count - 1)].Clone();
        item.Atk = UnityEngine.Random.Range(1, 10);
        item.Life = UnityEngine.Random.Range(1, 10);
        item.Power = item.Atk + item.Life;
        item.Count = 1;
        return item;
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