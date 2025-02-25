// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public enum UIState
// {
//     Lobby,
//     Battle,
//     HealthBar,
//     SelectSkill,
//     GameOver
// }
//
// public class UIManager : MonoBehaviour
// {   
//     LobbyUI lobbyUI;
//     BattleUI battleUI;
//     HealthBarUI healBarUI;
//     SelectSkilllUI selectUI;
//     GameOverUI gameOverUI;
//     private UIState currentState;
//
//     private void Awake()
//     {
//         lobbyUI = GetComponentInChildren<GameUI>(true);
//         lobbyUI.Init(this);
//         BattleUI = GetComponentInChildren<GameUI>(true);
//         BattleUI.Init(this);
//         HealthBarUI = GetComponentInChildren<GameUI>(true);
//         HealthBarUI.Init(this);
//         SelectSkillUI = GetComponentInChildren<GameUI>(true);
//         SelectSkillUI.Init(this);
//         gameOverUI = GetComponentInChildren<GameOverUI>(true);
//         gameOverUI.Init(this);
//         
//         ChangeState(UIState.Home);
//     }
//
// }