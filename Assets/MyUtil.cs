using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class MyUtil
{
    public static string mainPath = Application.dataPath + "/Resources";


    public static Transform FindOneInChildren(Transform t, string childName)
    {
        Transform tmp = t.Find(childName);
        if (tmp == null)
        {
            for (int i = 0; i < t.childCount; i++)
            {
                tmp = FindOneInChildren(t.GetChild(i), childName);
                if (tmp != null)
                {
                    return tmp;
                }
            }
        }
        return tmp;
    }

    public static void FindAllInChildren(List<Transform> Transforms, Transform t, string childName)
    {
        for (int i = 0; i < t.childCount; i++)
        {
            if (t.GetChild(i).gameObject.name.Contains(childName))
            {
                Transforms.Add(t.GetChild(i));
            }
            FindAllInChildren(Transforms, t.GetChild(i), childName);
        }
    }


    public static void ChangeLayerIncludeChildren(Transform t, int layer)
    {
        t.gameObject.layer = layer;
        if (t.childCount > 0)
        {
            for (int i = 0; i < t.childCount; i++)
            {
                t.GetChild(i).gameObject.layer = layer;
                ChangeLayerIncludeChildren(t.GetChild(i), layer);
            }
        }
    }

    public static T GetClassFromBinary<T>(string path)
    {
        T tmp;
        FileStream fs = File.Open(path, FileMode.Open);
        BinaryFormatter bf = new BinaryFormatter();
        tmp = (T)bf.Deserialize(fs);
        fs.Close();
        return tmp;
    }

    public static void SaveClassToBinary<T>(string path, T tmp)
    {
        FileStream fs = File.Open(path, FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, tmp);
        fs.Close();
    }

    static string connStr = "Server=81.68.87.60;Port=3307;Database=unity;Uid=sa;Pwd=P@ss1234;Charset=utf8";
    static MySqlConnection conn;
    static MySqlDataAdapter adapter;
    static MySqlCommand cmd;


    public static int GetDataFromMySQL(string cmdStr, DataSet ds)
    {
        int result = 0;
        try
        {
            if (conn == null)
            {
                conn = new MySqlConnection(connStr);
            }
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                adapter = new MySqlDataAdapter(cmdStr, conn);
                adapter.Fill(ds);
                result = 1;
            }
        }
        catch (Exception e)
        {
            result = -1;
        }
        finally
        {
            conn.Close();
        }
        return result;
    }

    public static int SaveDataToMySQL(string cmdStr)
    {
        int result = 0;
        try
        {
            if (conn == null)
            {
                conn = new MySqlConnection(connStr);
            }
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                cmd = new MySqlCommand(cmdStr, conn);
                result = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            result = -1;
        }
        finally
        {
            conn.Close();
        }
        return result;
    }

    //static Vector3[] tmpV4 = new Vector3[4];
    //public static Vector3 GetQuaterPosition(GameObject go, Quater q)
    //{
    //    go.GetComponent<RectTransform>().GetWorldCorners(tmpV4);
    //    switch (q)
    //    {
    //        case Quater.DownLeft:
    //            return tmpV4[(int)q];
    //        case Quater.UpLeft:
    //            return tmpV4[(int)q];
    //        case Quater.UpRight:
    //            return tmpV4[(int)q];
    //        case Quater.DownRight:
    //            return tmpV4[(int)q];
    //    }
    //    return Vector3.zero;
    //}
}
public enum Quater
{
    DownLeft,
    UpLeft,
    UpRight,
    DownRight
}