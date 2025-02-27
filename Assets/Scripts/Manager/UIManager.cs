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
    GameOver
}

public class UIManager : MonoBehaviour
{   
    TitleUI titleUI;
    LobbyUI lobbyUI;
    BattleUI battleUI;
    // SkillPointUI skillPointUI;
    // HealthBarUI healBarUI;
    SelectSkillUI selectSkillUI;
    // EquipSkillUI equipSkillUI;
    SkillSlotUI skillSlotUI;
    // GameOverUI gameOverUI;
    //
    private UIState currentState;

    private void Awake()
    {   
        titleUI = GetComponentInChildren<TitleUI>(true);
        titleUI?.Init(this);
        lobbyUI = GetComponentInChildren<LobbyUI>(true);
        lobbyUI?.Init(this);
        battleUI = GetComponentInChildren<BattleUI>(true);
        battleUI?.Init(this); 
        // skillPointUI = GetComponentInChildren<SkillPointUI>(true);
        // skillPointUI?.Init(this);
        // HealthBarUI = GetComponentInChildren<HealthBarUI>(true);
        // HealthBarUI.Init(this);
        selectSkillUI = GetComponentInChildren<SelectSkillUI>(true);
        //selectSkillUI?.Init(this);
        // EquipSkillUI = GetComponentInChildren<EquipSkillUI>(true);
        // EquipSkillUI.Init(this);
        skillSlotUI = GetComponentInChildren<SkillSlotUI>(true);
        //skillSlotUI?.init(this);
        // gameOverUI = GetComponentInChildren<GameOverUI>(true);
        // gameOverUI.Init(this);
        
    }
    public void ChangeState(UIState state)
    {
        currentState = state;
        titleUI.SetActive(currentState);
        lobbyUI.SetActive(currentState);
        battleUI.SetActive(currentState);
        selectSkillUI.SetActive(currentState);
    }

}