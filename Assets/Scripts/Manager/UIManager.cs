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
    StageClear,
    GameOver
}

public class UIManager : MonoBehaviour
{   
    TitleUI titleUI;
    LobbyUI lobbyUI;
    BattleUI battleUI;
    SelectSkillUI selectSkillUI;
    StageClearUI stageClearUI;
    GameOverUI gameOverUI;
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
        stageClearUI = GetComponentInChildren<StageClearUI>(true);
        stageClearUI?.Init(this);
        gameOverUI = GetComponentInChildren<GameOverUI>(true);
        gameOverUI?.Init(this);

    }
    public void ChangeState(UIState state)
    {
        currentState = state;
        titleUI?.SetActive(currentState);
        lobbyUI?.SetActive(currentState);
        battleUI?.SetActive(currentState);
        selectSkillUI?.SetActive(currentState);
        stageClearUI?.SetActive(currentState);
        gameOverUI?.SetActive(currentState);
    }
    public void UpdateStageClearUI(bool isLevelUp, int plusExp, int currentLevel,int currentExp)
    {
        stageClearUI.UpdateStageClearUI(isLevelUp, plusExp, currentLevel,currentExp);
    }
    public void UpdateEnemyCountInBattleUI(int count)
    {
        battleUI.UpdateEnemyCountText(count);
    }
    public void UpdateRoundTxtInBattleUI(int round)
    {
        battleUI.UpdateRoundText(round);
    }
    public void UpdateStageTxtInBattleUI(int stage)
    {
        battleUI.UpdateStageText(stage);
    }
}