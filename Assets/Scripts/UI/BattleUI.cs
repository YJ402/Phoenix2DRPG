using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BattleUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI stageText;      // 현재 스테이지 표시
    [SerializeField] private TextMeshProUGUI roundText;      // 현재 라운드 표시
    [SerializeField] private TextMeshProUGUI enemyCountText; // 남은 적 수 표시

    protected override UIState GetUIState()
    {
        return UIState.Battle;
    }

    // 초기화 (UIManager에서 호출)
    public override void SetActive(UIState currentState)
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(currentState == UIState.Battle);
    }
    public void UpdateEnemyCountText(int count)
    {
        enemyCountText.text = count.ToString();
    }
    public void UpdateRoundText(int round)
    {
        roundText.text = round.ToString();
    }
    public void UpdateStageText(int stage)
    {
        stageText.text = stage.ToString();
    }
}