using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageClearUI : BaseUI
{
    [SerializeField] Button BackToStageSelectButton;
    [SerializeField] BattleManager BattleManager;
    protected override UIState GetUIState()
    {
        throw new System.NotImplementedException();
    }
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        if (BackToStageSelectButton != null)
            BackToStageSelectButton.onClick.AddListener(() => OnClickButton());
    }
    void OnClickButton()
    {

    }
}
