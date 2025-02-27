using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor.UI;

public enum UIState
{
    Title,
    Lobby,
    Battle,
    SelectSkill,
    SkillPoint,
    HealthBar,
    EquipSkill,
    SkillSlot,
    GameOver,
    StageClear
}

public class UIManager : MonoBehaviour
{   
    TitleUI titleUI;
    LobbyUI lobbyUI;
    BattleUI battleUI;
    SelectSkillUI selectSkillUI;
    SkillSlotUI skillSlotUI;
    GameOverUI gameOverUI;
    StageClearUI stageClearUI;
    
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
        gameOverUI = GetComponentInChildren<GameOverUI>(true);
        gameOverUI?.Init(this);
        stageClearUI = GetComponentInChildren<StageClearUI>(true);
        stageClearUI?.Init(this);
        
    }
    public void ChangeState(UIState state)
    {
        currentState = state;
        titleUI.SetActive(currentState);
        lobbyUI.SetActive(currentState);
        battleUI.SetActive(currentState);
        selectSkillUI.SetActive(currentState);
        gameOverUI.SetActive(currentState);
        stageClearUI.SetActive(currentState);
    }

}