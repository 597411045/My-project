using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using XLua;

[Hotfix]
[CSharpCallLua]
public class LuaScript : MonoBehaviour
{
    [HideInInspector]
    public static LuaEnv luaEnv;
    [HideInInspector]
    public LuaTable luaTable;

    Action luaAwake;
    Action luaStart;
    Action luaUpdate;
    Action luaDestroy;
    Action luaHotFix;

    public static LuaScript Instance;



    byte[] LuaLoader(ref string s)
    {
        //StreamReader sr = new StreamReader(Application.streamingAssetsPath + "/Battle/" + s + ".lua.txt");
        //if (sr == null)
        //{
        //    return null;
        //}
        //return System.Text.Encoding.UTF8.GetBytes(sr.ReadToEnd());
        return System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(Application.streamingAssetsPath + "/Battle/" + s + ".lua.txt"));
    }

    private void Awake()
    {
        Instance = this;

        luaEnv = new LuaEnv();
        luaEnv.AddLoader(LuaLoader);
        //luaEnv.DoString("require 'main'");
        luaEnv.DoString("require 'HotFix'");




        //luaTable = luaEnv.NewTable();
        //LuaTable meta = luaEnv.NewTable();
        //meta.Set("__index", luaEnv.Global);
        //luaTable.SetMetaTable(meta);
        //meta.Dispose();


        //luaTable.Set("GlobalTable", this);


        //luaScript1 = Resources.Load<TextAsset>("Battle/main.lua");
        //luaEnv.DoString(luaScript1.text, "", luaGlobalTable);

        //luaEnv.Global.Set("", luaTable);

        //luaScript2 = Resources.Load<TextAsset>("Battle/hotfix.lua");
        //luaEnv.DoString(luaScript2.text);

        //luaAwake = luaTable.Get<Action>("luaAwake");
        //luaStart = luaTable.Get<Action>("luaStart");
        //luaUpdate = luaTable.Get<Action>("luaUpdate");
        //luaDestroy = luaTable.Get<Action>("luaDestroy");
        //luaManualAttack = luaTable.Get<Action<BaseCharacter, BaseCharacter>>("ManualAttack");
        //luaHotFix = luaTable.Get<Action>("HotFix");

        //if (luaAwake != null)
        //{
        //    luaAwake();
        //}
    }



    void Start()
    {
        //if (luaStart != null)
        //{
        //    //luaManualAttack(1, 2);
        //    luaStart();
        //    GetTeams();
        //    //Debug.Log(t4.Name);

        //}
    }

    private void GetTeams()
    {
        //LuaTable t1 = luaEnv.Global.Get<LuaTable>("CharacterBuilder");
        //List<LuaTable> t3 = t1.Get<List<LuaTable>>("Team1");
        //BaseCharacters.Add(t3[0].Get<BaseCharacter>("object"));
        //BaseCharacters.Add(t3[1].Get<BaseCharacter>("object"));
        //BaseCharacters.Add(t3[2].Get<BaseCharacter>("object"));
        //t3 = t1.Get<List<LuaTable>>("Team2");
        //BaseCharacters.Add(t3[0].Get<BaseCharacter>("object"));
        //BaseCharacters.Add(t3[1].Get<BaseCharacter>("object"));
        //BaseCharacters.Add(t3[2].Get<BaseCharacter>("object"));
    }

    //bool doCoroutine = true;
    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0) && doCoroutine && luaUpdate != null)
    //    {
    //        StartCoroutine(CR_luaUpdate());
    //        doCoroutine = false;
    //    }
    //    if (Input.GetMouseButton(0))
    //    {
    //        HotFixTest();
    //    }
    //    if (Input.GetMouseButtonDown(1))
    //    {
    //        //luaEnv.DoString(@"
    //        //    xlua.hotfix(CS.LuaScript, 'HotFixTest', function()
    //        //        print('lua')
    //        //    end)
    //        //");
    //        luaHotFix();
    //    }
    //}

    //public void HotFixTest()
    //{
    //    Debug.Log("C#");
    //}

    //IEnumerator CR_luaUpdate()
    //{
    //    luaUpdate();
    //    yield return new WaitForSeconds(1);
    //    doCoroutine = true;
    //}

    private void OnDisable()
    {
        //luaEnv.DoString("require 'LuaDispose'");
    }

    private void OnDestroy()
    {
        //if (luaDestroy != null)
        //{
        //    luaDestroy();
        //}
        //luaTable.Dispose();
    }

    //public double TestFunc(TmpPara p1, ref int p2, out string p3, Action luafunc, out
    //    Action csfunc)
    //{
    //    Debug.Log(p1.x + " " + p1.y + " " + p2);
    //    luafunc();
    //    p2 = p2 * p1.x;
    //    p3 = "Test" + p1.y;
    //    csfunc = () => { Debug.Log("CS func"); };
    //    return 1.23;
    //}

    //public void ManualAttack(string AttackName, string InjuredName)
    //{
    //    luaManualAttack(BaseCharacters[int.Parse(AttackName.Substring(1, 1)) - 1], BaseCharacters[int.Parse(InjuredName.Substring(1, 1)) - 1]);
    //}

}

//public class TmpPara
//{
//    public int x;
//    public string y;
//}

