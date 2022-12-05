using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class SkillInfo
{
    public static List<SkillInfo> BaseItemList = new List<SkillInfo>();

    private int _id;
    private string _name;
    private string _profile;

    private int _level = 1;

    private int _price = 0;
    private int _atk = 0;
    private int _life = 0;

    private string _description;

    public int Id { get => _id; set => _id = value; }
    public string Name { get => _name; set => _name = value; }
    public string Profile { get => _profile; set => _profile = value; }
    public int Level { get => _level; set => _level = value; }
    public int Price { get => _price; set => _price = value; }
    public int Atk { get => _atk; set => _atk = value; }
    public int Life { get => _life; set => _life = value; }
    public string Description { get => _description; set => _description = value; }

    static SkillInfo()
    {
        GetBaseListFromCSV();
    }
    public static void GetBaseListFromCSV()
    {
        TextAsset t = Resources.Load<TextAsset>("Skill");
        string[] lines = t.ToString().Replace("\r", "").Split('\n');

        SkillInfo skillInfo;
        //PropertyInfo[] propertys = itemInfo.GetType().GetProperties();
        PropertyInfo[] propertys = typeof(SkillInfo).GetProperties();

        for (int j = 1; j < lines.Length; j++)
        {
            string[] propertyCSV = lines[j].Split(',');
            skillInfo = new SkillInfo();

            for (int i = 0; i < propertys.Length; i++)
            {
                if (propertys[i].PropertyType == typeof(int))
                {
                    propertys[i].SetValue(skillInfo, int.Parse(propertyCSV[i]));
                    continue;
                }
                if (propertys[i].PropertyType == typeof(string))
                {
                    propertys[i].SetValue(skillInfo, propertyCSV[i]);
                    continue;
                }
                if (propertys[i].PropertyType == typeof(ItemType))
                {
                    propertys[i].SetValue(skillInfo, Enum.Parse(typeof(ItemType), propertyCSV[i]));
                    continue;
                }
            }
            BaseItemList.Add(skillInfo);
        }
    }

    public SkillInfo Clone()
    {
        SkillInfo item = new SkillInfo();
        PropertyInfo[] propertys = item.GetType().GetProperties();
        for (int i = 0; i < propertys.Length; i++)
        {
            propertys[i].SetValue(item, propertys[i].GetValue(this));
        }
        return item;
    }
}
