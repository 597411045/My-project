using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using XLua;

public class Module_Dialogue
{
    public class Choice
    {
        public string _dialogue;
        public int _nextid;
        public Action<int,int> _choiceAction;
    }

    public static Dictionary<int, Module_Dialogue> DialogueDic = new Dictionary<int, Module_Dialogue>();

    private int _id;
    private string _dialogue;
    public Action DefaultAction;

    public Choice[] Choices = { new Choice(), new Choice(), new Choice() };

    public int Id { get => _id; set => _id = value; }
    public string Dialogue { get => _dialogue; set => _dialogue = value; }
    public string Choice1_dialogue { get => Choices[0]._dialogue; set => Choices[0]._dialogue = value; }
    public int Choice1_nextid { get => Choices[0]._nextid; set => Choices[0]._nextid = value; }
    public string Choice2_dialogue { get => Choices[1]._dialogue; set => Choices[1]._dialogue = value; }
    public int Choice2_nextid { get => Choices[1]._nextid; set => Choices[1]._nextid = value; }
    public string Choice3_dialogue { get => Choices[2]._dialogue; set => Choices[2]._dialogue = value; }
    public int Choice3_nextid { get => Choices[2]._nextid; set => Choices[2]._nextid = value; }

    public Module_Dialogue()
    {
    }

    static Module_Dialogue()
    {
        GetDataFromCSV();
    }

    public Choice this[int index]
    {
        get
        {
            return Choices[index];
        }
    }

    static void GetDataFromCSV()
    {
        TextAsset t = Resources.Load<TextAsset>("Dialogue");
        string[] lines = t.ToString().Replace("\r", "").Split('\n');
        Module_Dialogue d;

        PropertyInfo[] propertys = typeof(Module_Dialogue).GetProperties();
        for (int j = 1; j < lines.Length; j++)
        {
            if (lines[j].Length <= 1) continue;
            d = new Module_Dialogue();
            string[] CSVData = lines[j].Split(',');
            for (int i = 0; i < propertys.Length; i++)
            {
                if (propertys[i].PropertyType == typeof(int) && CSVData[i] != "")
                {
                    propertys[i].SetValue(d, int.Parse(CSVData[i]));
                }
                if (propertys[i].PropertyType == typeof(string) && CSVData[i] != "")
                {
                    propertys[i].SetValue(d, CSVData[i]);
                }
            }
            DialogueDic.Add(d.Id, d);
        }
    }

}
