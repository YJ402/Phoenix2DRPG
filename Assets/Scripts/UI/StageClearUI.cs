using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageClearUI : BaseUI
{
    [SerializeField] Button BackToStageSelectButton;
    [SerializeField] TextMeshProUGUI levelNumTxt;
    [SerializeField] TextMeshProUGUI plusExpTxt;
    [SerializeField] GameObject levelUp;
    [SerializeField] Slider expSlider;
    protected override UIState GetUIState()
    {
        return UIState.StageClear;
    }
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        if (BackToStageSelectButton != null)
            BackToStageSelectButton.onClick.AddListener(() => OnClickButton());
    }
    public void UpdateStageClearUI(bool isLevelUp, int plusExp,int currentLevel,int currentExp)
    {
        levelUp.SetActive(isLevelUp);
        plusExpTxt.text = plusExp.ToString();
        levelNumTxt.text = currentLevel.ToString();
        expSlider.value = (float)currentExp / (currentLevel * 5);
    }
    void OnClickButton()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
