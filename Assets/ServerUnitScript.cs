using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ServerUnitScript : MonoBehaviour
{
    public string ip;
    private string serverName;
    public string ServerName
    {
        get {
            return serverName;
        }

        set
        {
            serverName = value;
            text.text = serverName;
        }
    }
    public int popularity;

    public TextMeshProUGUI text;

    private void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(()=> {
            PanelManager.ChosenServerUnit.GetComponent<Image>().sprite = this.gameObject.GetComponent<Image>().sprite;
            PanelManager.ChosenServerUnit.GetComponentInChildren<TextMeshProUGUI>().text = this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text;
        });
    }

}
