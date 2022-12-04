using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo
{
    public static string username;
    public static string password;
    public static int CharacterIndex = 0;
    public static string CurrentCharacterName = "Default";

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

    public string Name
    {
        get { return _name; }
        set
        {
            _name = value;
            if (NameTexts != null)
            {
                foreach (Text item in NameTexts)
                {
                    item.text = _name;
                }
            }
        }
    }
    public string Profile
    {
        get { return _profile; }
        set
        {
            _profile = value;
        }
    }
    public int Level
    {
        get { return _level; }
        set
        {
            _level = value; if (LevelTexts != null)
            {
                foreach (Text item in LevelTexts)
                {
                    item.text = _level.ToString();
                }
            }
        }
    }
    public int Power
    {
        get { return _power; }
        set
        {
            _power = value; if (PowerTexts != null)
            {
                foreach (Text item in PowerTexts)
                {
                    item.text = _power.ToString();
                }
            }
        }
    }
    public int Exp
    {
        get { return _exp; }
        set
        {
            _exp = value; if (ExpTexts != null)
            {
                foreach (Text item in ExpTexts)
                {
                    item.text = _exp.ToString();
                }
            }
            PanelManagerInVillage.Instance.ExpBar.fillAmount = _exp / 100f;

        }
    }
    public int Diamond
    {
        get { return _diamond; }
        set
        {
            _diamond = value; if (DiamondTexts != null)
            {
                foreach (Text item in DiamondTexts)
                {
                    item.text = _diamond.ToString();
                }
            }
        }
    }
    public int Coin
    {
        get { return _coin; }
        set
        {
            _coin = value; if (CoinTexts != null)
            {
                foreach (Text item in CoinTexts)
                {
                    item.text = _coin.ToString();
                }
            }
        }
    }
    public int Hp
    {
        get { return _hp; }
        set
        {
            _hp = value; if (HpTexts != null)
            {
                foreach (Text item in HpTexts)
                {
                    item.text = _hp.ToString();
                }
            }
            PanelManagerInVillage.Instance.HPBar.fillAmount = _hp / 100f;
        }
    }
    public int Mp
    {
        get { return _mp; }
        set
        {
            _mp = value; if (MpTexts != null)
            {
                foreach (Text item in MpTexts)
                {
                    item.text = _mp.ToString();
                }
            }
            PanelManagerInVillage.Instance.MPBar.fillAmount = _hp / 100f;

        }
    }
    public int Life
    {
        get { return _life; }
        set
        {
            _life = value; if (LifeTexts != null)
            {
                foreach (Text item in LifeTexts)
                {
                    item.text = _life.ToString();
                }
            }
        }
    }
    public int Atk
    {
        get { return _atk; }
        set
        {
            _atk = value; if (AtkTexts != null)
            {
                foreach (Text item in AtkTexts)
                {
                    item.text = _atk.ToString();
                }
            }
        }
    }

    private List<Text> NameTexts = new List<Text>();
    private List<Text> LevelTexts = new List<Text>();
    private List<Text> PowerTexts = new List<Text>();
    private List<Text> ExpTexts = new List<Text>();
    private List<Text> DiamondTexts = new List<Text>();
    private List<Text> CoinTexts = new List<Text>();
    private List<Text> HpTexts = new List<Text>();
    private List<Text> MpTexts = new List<Text>();
    private List<Text> LifeTexts = new List<Text>();
    private List<Text> AtkTexts = new List<Text>();
    public List<List<Text>> TexstList;

    public Dictionary<ItemType, ItemInfo> Equipments;
    public Dictionary<int, ItemInfo> Inventory;
    public Dictionary<int, QuestInfo> Quests;



    public PlayerInfo()
    {
        TexstList = new List<List<Text>>() {
            NameTexts ,
            LevelTexts ,
            PowerTexts ,
            ExpTexts ,
            DiamondTexts,
            CoinTexts ,
            HpTexts ,
            MpTexts,
            LifeTexts,
            AtkTexts
        };
        Equipments = new Dictionary<ItemType, ItemInfo>() {
            {ItemType.Head,null },
            {ItemType.Cloth,null },
            {ItemType.Weapon,null },
            {ItemType.Shoes,null },
            {ItemType.Necklace,null },
            {ItemType.Bracelet,null },
            {ItemType.Ring,null },
            {ItemType.Wing,null }
        };
        Inventory = new Dictionary<int, ItemInfo>();
        LinkPropertyToUI();
        InitDefaultValue();
        GetBaseValue();
        BuildRandomPlayerInventory();//GetInventory();
        BuildRandomPlayerEquipments();//GetEquipments();
    }
    public PlayerInfo(bool flag)
    {
        Equipments = new Dictionary<ItemType, ItemInfo>() {
            {ItemType.Head,null },
            {ItemType.Cloth,null },
            {ItemType.Weapon,null },
            {ItemType.Shoes,null },
            {ItemType.Necklace,null },
            {ItemType.Bracelet,null },
            {ItemType.Ring,null },
            {ItemType.Wing,null }
        };
        InitDefaultValue();
        GetBaseValue();
        BuildRandomPlayerEquipments();//GetEquipments();
    }

    public void LinkPropertyToUI()
    {
        string[] tags = { "Name", "Level", "Power", "Exp", "Diamond", "Coin", "HP", "MP", "Life", "Atk" };
        for (int i = 0; i < tags.Length; i++)
        {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag(tags[i]))
            {
                TexstList[i].Add(go.GetComponent<Text>());
            }
        }
    }
    private void InitDefaultValue()
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
    }
    private void GetBaseValue() { }
    private void GetEquipments() { }
    void GetInventory() { }


    bool ifExchange = false;
    float PowerBeforeUnequip;

    public bool TryEquipItem(ItemInfo item)
    {
        if (Equipments.ContainsKey(item.Type))
        {
            if (Equipments[item.Type] != null)
            {
                PowerBeforeUnequip = _power;
                TryUnequipItem(item.Type);
                ifExchange = true;
            }
            Equipments[item.Type] = item;

            Life = _life + Equipments[item.Type].Life;
            Atk = _atk + Equipments[item.Type].Atk;
            if (ifExchange)
            {
                RegisterDynamicValueChange(PowerTexts, PowerBeforeUnequip, _life + _atk, 3);
            }
            else { 
            RegisterDynamicValueChange(PowerTexts, _power, _life + _atk, 3);
            }
            _power = _life + _atk;
            PanelManagerInVillage.Instance.PlayerEquipmnent.TryGetItemFrom(item, PanelManagerInVillage.Instance.PlayerInventory);

            ifExchange = false;
            return true;
        }
        return false;
    }
    public bool TryUnequipItem(ItemType type)
    {
        if (Equipments.ContainsKey(type))
        {
            if (Equipments[type] != null)
            {
                Life = _life - Equipments[type].Life;
                Atk = _atk - Equipments[type].Atk;

                if (ifExchange != true)
                {
                    RegisterDynamicValueChange(PowerTexts, _power, _life + _atk, 3);
                }
                Power = _life + _atk;
                PanelManagerInVillage.Instance.PlayerInventory.TryGetItemFrom(Equipments[type], PanelManagerInVillage.Instance.PlayerEquipmnent);
                Equipments[type] = null;
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
        GameManagerInVillage.Instance.timers.Add(new CustomTimer(texts, 0, time, ac));
    }

    public bool TryPickItem(ItemInfo item)
    {
        if (item.Type == ItemType.Consume)
        {
            if (Inventory.ContainsKey(item.Id))
            {
                Inventory[item.Id].Count++;
                return true;
            }
            else
            {
                Inventory.Add(item.Id, item);
            }
        }
        else
        {
            Inventory.Add(item.specialId, item);
        }
        PanelManagerInVillage.Instance.PlayerInventory.TryAddItem(item);
        return true;
    }
    public bool TryDropItem(ItemInfo item, SlotManager toSlotManager)
    {
        if (item.Type == ItemType.Consume)
        {
            if (Inventory.ContainsKey(item.Id))
            {
                Inventory[item.Id].Count--;
                if (Inventory[item.Id].Count <= 0)
                {
                    Inventory.Remove(item.Id);
                }
            }
        }
        else
        {
            Inventory.Remove(item.specialId);
        }
        PanelManagerInVillage.Instance.PlayerInventory.RemoveItem(item);
        return true;
    }

    private void BuildRandomPlayerInventory()
    {
        for (int i = 0; i < 9; i++)
        {
            ItemInfo item = ItemInfo.GetRandomItem();
            if (item.Type != ItemType.Consume)
            {
                item.specialId = 2000 + i;
            }
            else
            {
                item.specialId = item.Id;
            }
            TryPickItem(item);
        }

    }
    private void BuildRandomPlayerEquipments()
    {
        //int i = 0;
        //foreach (KeyValuePair<int, ItemInfo> k in Inventory)
        //{
        //    PanelManagerInVillage.Instance.PlayerInventory.InsertItem(k.Value);

        //}

    }

}
