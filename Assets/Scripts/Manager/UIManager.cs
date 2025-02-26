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
    SkillPoint,
    HealthBar,
    SelectSkill,
    EquipSkill,
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
        selectSkillUI?.Init(this);
        // EquipSkillUI = GetComponentInChildren<EquipSkillUI>(true);
        // EquipSkillUI.Init(this);
        // gameOverUI = GetComponentInChildren<GameOverUI>(true);
        // gameOverUI.Init(this);
        
    }

}