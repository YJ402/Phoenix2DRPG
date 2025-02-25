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
    HealthBar,
    SelectSkill,
    EquipSkill,
    GameOver
}

public class UIManager : MonoBehaviour
{   
    TitleUI titleUI;
    LobbyUI lobbyUI;
    // BattleUI battleUI;
    // HealthBarUI healBarUI;
    // SelectSkillUI selectSkillUI;
    // EquipSkillUI equipSkillUI;
    // GameOverUI gameOverUI;
    //
    private UIState currentState;

    private void Awake()
    {   
        titleUI = GetComponentInChildren<TitleUI>(true);
        titleUI?.Init(this);
        lobbyUI = GetComponentInChildren<LobbyUI>(true);
        lobbyUI?.Init(this);
        // BattleUI = GetComponentInChildren<BattleUI>(true);
        // BattleUI.Init(this);
        // HealthBarUI = GetComponentInChildren<HealthBarUI>(true);
        // HealthBarUI.Init(this);
        // SelectSkillUI = GetComponentInChildren<SelectSkillUI>(true);
        // SelectSkillUI.Init(this);
        // EquipSkillUI = GetComponentInChildren<EquipSkillUI>(true);
        // EquipSkillUI.Init(this);
        // gameOverUI = GetComponentInChildren<GameOverUI>(true);
        // gameOverUI.Init(this);
        
    }

}