using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : BaseUI
{
    [SerializeField] Button BackToLobbyButton;
    protected override UIState GetUIState()
    {
        return UIState.GameOver;
    }
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        if (BackToLobbyButton != null)
            BackToLobbyButton.onClick.AddListener(() => OnClickButton());
    }
    void OnClickButton()
    {
        PlayerData.Instance.StageEndSetting();
        SceneManager.LoadScene("LobbyScene");
    }
}
