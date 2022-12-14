using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using XLua;

[Hotfix]
public class Module_PlayerInfo
{
    public Dictionary<int, Module_Effect> effectList = new Dictionary<int, Module_Effect>();
    public Dictionary<int, Module_Dialogue> dialogList = new Dictionary<int, Module_Dialogue>();

    public EventHandler<Module_Dialogue> DialogHandler;
    public void GetDialogue()
    {
        if (DialogHandler != null && dialogList.ContainsKey(dialogueIndex))
        {
            DialogHandler(null, dialogList[dialogueIndex]);
        }
    }

    public int dialogueIndex;
    public void SetDialogueIndex(int i)
    {
        dialogueIndex = i;

        PanelManagerInVillage.Instance.ChangePanel(null, NameMap.PanelDialogue, () =>
        {
            PanelManagerInVillage.Instance.Panels[NameMap.PanelDialogue].SetPlayer(GameManagerInVillage.PlayerObject.GetComponent<PlayMoveScript>().NearestNPC.view);
        });
        for (int j = 0; j < 3; j++)
        {
            if (dialogList[i][j]._dialogue != null)
            {
                if (dialogList[i][j]._dialogue.Contains("end"))
                {
                    dialogList[i][j]._choiceAction += (x, y) =>
                    {
                        dialogueIndex = dialogList[x][y]._nextid;
                        PanelManagerInVillage.Instance.ChangePanel(NameMap.PanelDialogue, null);
                    };
                }
                else
                {
                    dialogList[i][j]._choiceAction += (x, y) =>
                    {
                        SetDialogueIndex(dialogList[x][y]._nextid);
                    };
                }
            }
        }
    }


    public static string LocalUsername;
    public static string LocalPassword;
    public static int CharacterId = 0;
    public static string CurrentCharacterName = "Default";

    private string _name;
    private string _profile;
    private int _level;
    private int _power;
    private int _exp;
    private int _diamond;
    private int _coin;
    private int _hp;
    private int _mp;
    private int _atk;
    private int _life;

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
            _power = _life + _atk;
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



    public Dictionary<ItemType, Module_ItemInfo> Equipments;
    public Dictionary<int, Module_ItemInfo> Inventory;
    public Dictionary<int, Module_QuestInfo> Quests;
    public Dictionary<int, SkillInfo> Skill;


    public Module_PlayerInfo(PlayerType p)
    {
        if (p == PlayerType.Player)
        {
            GetPlayerDataFromCSV();
            Inventory = new Dictionary<int, Module_ItemInfo>();
            GetItemDataFromCSV();

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
            GetEquipDataFromCSV();

            Quests = new Dictionary<int, Module_QuestInfo>();
            GetQuestsDataFromCSV();

            Skill = new Dictionary<int, SkillInfo>();
        }
        //BuildRandomPlayerInventory();//GetInventory();
        //BuildRandomPlayerEquipments();//GetEquipments();
    }

    //private void GetEquipments() { }
    //void GetInventory() { }

    public void Refresh()
    {
        PropertyInfo[] propertys = typeof(Module_PlayerInfo).GetProperties();
        for (int i = 0; i < propertys.Length; i++)
        {
            propertys[i].SetValue(this, propertys[i].GetValue(this));
        }
        GetDialogue();
    }

    private void GetPlayerDataFromCSV()
    {
        TextAsset t = Resources.Load<TextAsset>("Player");
        string[] lines = t.ToString().Replace("\r", "").Split('\n');
        PropertyInfo[] propertys = this.GetType().GetProperties();
        string[] CSVData = lines[1].Split(',');
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

    [LuaCallCSharp]
    private void GetItemDataFromCSV()
    {
        int[] itemIds = { 1001, 1002, 1007, 1008, 1009, 1010, 1011 };
        foreach (var i in itemIds)
        {
            Module_ItemInfo m = new Module_ItemInfo(i);
            Inventory.Add(i, m);
            if (i == 1001)
            {
                Effect_AboutHealth tmpEff = new Effect_AboutHealth();
                m.effectList.Add(i, tmpEff);
            }
            if (i == 1002)
            {
                Effect_AboutHealth tmpEff = new Effect_AboutHealth();
                tmpEff.Duration = 10;
                tmpEff.Inverval = 2;
                m.effectList.Add(i, tmpEff);
            }
        }
    }
    private void GetEquipDataFromCSV()
    {
        int[] itemIds = { 1003, 1004, 1005, 1006 };
        foreach (var i in itemIds)
        {
            Module_ItemInfo module = new Module_ItemInfo(i);
            Equipments[module.Type] = module;

            this.Life = this.Life + module.Life;
            this.Atk = this.Atk + module.Atk;
            this.Power = this.Life + module.Atk;
        }
    }

    private void GetQuestsDataFromCSV()
    {
        int[] itemIds = { 1004 };
        foreach (var i in itemIds)
        {
            Quests.Add(i, new Module_QuestInfo("PlayerQuest", i));
        }
    }
}

