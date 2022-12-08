using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class View_PlayerInfo
{
    Transform Canvas;
    public Control_PlayerInfo c;

    public List<List<Text>> TexstList;
    public View_PlayerInfo(PlayerType p)
    {
        if (p == PlayerType.Player)
        {
            BuildTexts();
            BuildImages();
        }
    }



    private void BuildTexts()
    {
        TexstList = new List<List<Text>>() {
            NameUI ,
            LevelUI ,
            PowerUI ,
            ExpUI ,
            DiamondUI,
            CoinUI ,
            HpUI ,
            MpUI,
            AtkUI,
            LifeUI
        };
        string[] tags = { "PlayerNameUI", "PlayerLevelUI", "PlayerPowerUI", "PlayerExpUI", "PlayerDiamondUI", "PlayerCoinUI", "PlayerHpUI", "PlayerMpUI", "PlayerAtkUI", "PlayerLifeUI" };
        Canvas = GameObject.Find("Canvas").transform;
        List<Transform> tmp = new List<Transform>();
        for (int i = 0; i < tags.Length; i++)
        {
            MyUtil.FindAllInChildren(tmp, Canvas, tags[i]);

            foreach (var j in tmp)
            {
                TexstList[i].Add(j.GetComponent<Text>());
            }
            tmp.Clear();
        }
    }
    private void BuildImages()
    {
        ExpBarUI.Add(GameObject.Find("PlayerExpBarUI").GetComponent<Image>());
        HpBarUI.Add(GameObject.Find("PlayerHpBarUI").GetComponent<Image>());
        MpBarUI.Add(GameObject.Find("PlayerMpBarUI").GetComponent<Image>());
    }

    public List<Text> NameUI = new List<Text>();
    public void NameUIAction(object sender, string arg)
    {
        foreach (var i in NameUI)
        {
            i.text = arg;
        }
    }

    public List<Image> ProfileUI = new List<Image>();
    public void ProfileUIAction(object sender, string arg)
    {
        foreach (var i in ProfileUI)
        {
            i.sprite = Resources.Load<Sprite>(arg);
        }
    }

    public List<Text> LevelUI = new List<Text>();
    public void LevelUIAction(object sender, int arg)
    {
        foreach (var i in LevelUI)
        {
            i.text = arg.ToString();
        }
    }

    public List<Text> PowerUI = new List<Text>();
    public void PowerUIAction(object sender, int arg)
    {
        foreach (var i in PowerUI)
        {
            i.text = arg.ToString();
        }
    }

    public List<Text> ExpUI = new List<Text>();
    public List<Image> ExpBarUI = new List<Image>();
    public void ExpUIAction(object sender, int arg)
    {

        foreach (var i in ExpUI)
        {
            i.text = arg.ToString();
        }
        foreach (var i in ExpBarUI)
        {
            i.fillAmount = arg / 100f;
        }
    }

    public List<Text> DiamondUI = new List<Text>();
    public void DiamondUIAction(object sender, int arg)
    {
        foreach (var i in DiamondUI)
        {
            i.text = arg.ToString();
        }
    }

    public List<Text> CoinUI = new List<Text>();
    public void CoinUIAction(object sender, int arg)
    {
        foreach (var i in CoinUI)
        {
            i.text = arg.ToString();
        }
    }

    public List<Text> HpUI = new List<Text>();
    public List<Image> HpBarUI = new List<Image>();
    public void HpUIAction(object sender, int arg)
    {
        foreach (var i in HpUI)
        {
            i.text = arg.ToString();
        }
        foreach (var i in HpBarUI)
        {
            i.fillAmount = arg / 100f;
        }
    }

    public List<Text> MpUI = new List<Text>();
    public List<Image> MpBarUI = new List<Image>();
    public void MpUIAction(object sender, int arg)
    {
        foreach (var i in MpUI)
        {
            i.text = arg.ToString();
        }
        foreach (var i in MpBarUI)
        {
            i.fillAmount = arg / 100f;
        }
    }

    public List<Text> LifeUI = new List<Text>();
    public void LifeUIAction(object sender, int arg)
    {
        foreach (var i in LifeUI)
        {
            i.text = arg.ToString();
        }

    }

    public List<Text> AtkUI = new List<Text>();
    public void AtkUIAction(object sender, int arg)
    {
        foreach (var i in AtkUI)
        {
            i.text = arg.ToString();
        }

    }
}

public enum PlayerType
{
    Player,
    NPC,
    Other
}