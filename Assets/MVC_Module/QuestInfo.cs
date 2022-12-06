using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class QuestInfo
{
    public static List<QuestInfo> BaseQuestList = new List<QuestInfo>();

    private int _id;
    private string _name;
    private string _profile;

    private int _rewardExp = 0;
    private int _rewardCoin = 0;
    private int _rewardBaseItemId = 0;
    private string _description;

    public static string Accept = "Accept";
    public static string Abandon = "Abandon";
    public static string Finish = "Finish";

    public Vector3 NPCPosition;
    public Text StatusUI;
    public void SetStatus(string s)
    {
        StatusUI.text = s;
    }

    public int Id { get => _id; set => _id = value; }
    public string Name { get => _name; set => _name = value; }
    public string Profile { get => _profile; set => _profile = value; }
    public int RewardExp { get => _rewardExp; set => _rewardExp = value; }
    public int RewardCoin { get => _rewardCoin; set => _rewardCoin = value; }
    public int RewardBaseItemId { get => _rewardBaseItemId; set => _rewardBaseItemId = value; }
    public string Description { get => _description; set => _description = value; }


    static QuestInfo()
    {
        GetBaseListFromCSV();
        BuildPlayerQuest();
    }

    public QuestInfo()
    {

    }

    public static void GetBaseListFromCSV()
    {
        TextAsset t = Resources.Load<TextAsset>("Quest");
        string[] lines = t.ToString().Replace("\r", "").Split('\n');

        QuestInfo questInfo;
        //PropertyInfo[] propertys = itemInfo.GetType().GetProperties();
        PropertyInfo[] propertys = typeof(QuestInfo).GetProperties();

        for (int j = 1; j < lines.Length; j++)
        {
            string[] propertyCSV = lines[j].Split(',');
            questInfo = new QuestInfo();

            for (int i = 0; i < propertys.Length; i++)
            {
                if (propertys[i].PropertyType == typeof(int))
                {
                    propertys[i].SetValue(questInfo, int.Parse(propertyCSV[i]));
                    continue;
                }
                if (propertys[i].PropertyType == typeof(string))
                {
                    propertys[i].SetValue(questInfo, propertyCSV[i]);
                    continue;
                }
            }
            BaseQuestList.Add(questInfo);
        }
    }

    private static void BuildPlayerQuest()
    {
        foreach (var item in BaseQuestList)
        {
            PanelManagerInVillage.Instance.PlayerQuest.TryAddQuest(item);
        }
    }
}
