using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Module_QuestInfo
{

    private int _id;
    private string _name;
    private string _profile;
    private int _rewardItemId;
    private string _description;
    public QuestStatus _status;


    public Vector3 NPCPosition;
    public Control_QuestInfo c;


    public EventHandler<int> IdHandler;
    public int Id
    {
        get => _id;
        set { _id = value; if (IdHandler != null) { IdHandler(null, _id); } }
    }
    public EventHandler<string> NameHandler;
    public string Name { get => _name; set { _name = value; if (NameHandler != null) { NameHandler(null, _name); } } }
    public EventHandler<string> ProfileHandler;
    public string Profile { get => _profile; set { _profile = value; if (ProfileHandler != null) { ProfileHandler(null, _profile); } } }
    public EventHandler<int> RewardItemIdHandler;
    public int RewardItemId { get => _rewardItemId; set { _rewardItemId = value; if (RewardItemIdHandler != null) { RewardItemIdHandler(null, _rewardItemId); } } }
    public EventHandler<string> DescriptionHandler;
    public string Description { get => _description; set { _description = value; if (DescriptionHandler != null) { DescriptionHandler(null, _description); } } }
    public EventHandler<QuestStatus> StatusHandler;
    public QuestStatus Status { get => _status; set { _status = value; if (StatusHandler != null) { StatusHandler(null, _status); } } }


    public Module_QuestInfo(string fileName, int id)
    {
        GetDataFromCSV(fileName, id);
    }

    private void GetDataFromCSV(string fileName, int id)
    {
        TextAsset t = Resources.Load<TextAsset>(fileName);
        string[] lines = t.ToString().Replace("\r", "").Split('\n');

        PropertyInfo[] propertys = typeof(Module_QuestInfo).GetProperties();

        for (int j = 1; j < lines.Length; j++)
        {
            string[] CSVData = lines[j].Split(',');
            if (int.Parse(CSVData[0]) != id) continue;

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
        }
    }
    public void Refresh()
    {
        PropertyInfo[] propertys = typeof(Module_QuestInfo).GetProperties();
        for (int i = 0; i < propertys.Length; i++)
        {
            propertys[i].SetValue(this, propertys[i].GetValue(this));
        }
    }
}

public enum QuestStatus
{
    New,
    Doing,
    Done
}
