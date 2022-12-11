using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript1 : NPCScript
{

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        module = new Module_PlayerInfo(PlayerType.NPC);
        module.Name = this.gameObject.name;
        module.Profile = "NPC1";
        view = new View_PlayerInfo(PlayerType.NPC);
        control = new Control_PlayerInfo(module, view, PlayerType.NPC);
        module.Quests = new Dictionary<int, Module_QuestInfo>();


        module.dialogList = Module_Dialogue.DialogueDic;
        module.dialogueIndex = 2001;

    }


}
