using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public enum UIState
{
    Title,
    Lobby,
    Battle,
    SelectSkill,
    EquipSkill,
    GameOver
}

public class UIManager : MonoBehaviour
{   
    TitleUI titleUI;
    LobbyUI lobbyUI;
    BattleUI battleUI;
    SelectSkillUI selectSkillUI;
    SkillSlotUI skillSlotUI;
    private UIState currentState;

    private void Awake()
    {   
        titleUI = GetComponentInChildren<TitleUI>(true);
        titleUI?.Init(this);
        lobbyUI = GetComponentInChildren<LobbyUI>(true);
        lobbyUI?.Init(this);
        battleUI = GetComponentInChildren<BattleUI>(true);
        battleUI?.Init(this); 
        selectSkillUI = GetComponentInChildren<SelectSkillUI>(true);
        selectSkillUI?.Init(this);
        
    }

}