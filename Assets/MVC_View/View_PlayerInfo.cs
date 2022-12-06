using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class View_PlayerInfo
{
    public List<List<Text>> TexstList;
    public View_PlayerInfo()
    {
        BuildTexts();
        BuildImages();
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
            LifeUI,
            AtkUI
        };
        string[] tags = { "Name", "Level", "Power", "Exp", "Diamond", "Coin", "HP", "MP", "Life", "Atk" };
        for (int i = 0; i < tags.Length; i++)
        {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag(tags[i]))
            {
                TexstList[i].Add(go.GetComponent<Text>());
            }
        }
    }
    private void BuildImages()
    {
        ExpBarUI.Add(GameObject.Find("Exp/Bar").GetComponent<Image>());
        HpBarUI.Add(GameObject.Find("BaseInfo/HP/Bar").GetComponent<Image>());
        MpBarUI.Add(GameObject.Find("BaseInfo/MP/Bar").GetComponent<Image>());
    }

    private List<Text> NameUI = new List<Text>();
    public void NameUIAction(object sender, string arg)
    {
        foreach (var i in NameUI)
        {
            i.text = arg;
        }
    }

    private List<Image> ProfileUI = new List<Image>();
    public void ProfileUIAction(object sender, string arg)
    {
        foreach (var i in ProfileUI)
        {
            i.sprite = Resources.Load<Sprite>(arg);
        }
    }

    private List<Text> LevelUI = new List<Text>();
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

    private List<Text> ExpUI = new List<Text>();
    private List<Image> ExpBarUI = new List<Image>();
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

    private List<Text> DiamondUI = new List<Text>();
    public void DiamondUIAction(object sender, int arg)
    {
        foreach (var i in DiamondUI)
        {
            i.text = arg.ToString();
        }
    }

    private List<Text> CoinUI = new List<Text>();
    public void CoinUIAction(object sender, int arg)
    {
        foreach (var i in CoinUI)
        {
            i.text = arg.ToString();
        }
    }

    private List<Text> HpUI = new List<Text>();
    private List<Image> HpBarUI = new List<Image>();
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

    private List<Text> MpUI = new List<Text>();
    private List<Image> MpBarUI = new List<Image>();
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

    private List<Text> LifeUI = new List<Text>();
    public void LifeUIAction(object sender, int arg)
    {
        foreach (var i in LifeUI)
        {
            i.text = arg.ToString();
        }

    }

    private List<Text> AtkUI = new List<Text>();
    public void AtkUIAction(object sender, int arg)
    {
        foreach (var i in AtkUI)
        {
            i.text = arg.ToString();
        }

    }
}