using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleUI : BaseUI
{
    [Header("Battle Info UI")]
    [SerializeField] private Text stageText;      // 현재 스테이지 표시
    [SerializeField] private Text roundText;      // 현재 라운드 표시
    [SerializeField] private Text enemyCountText; // 남은 적 수 표시

    [Header("Overlay UIs")]
    [SerializeField] private GameObject startStageUI;   // StartStageUI 오브젝트 (SelectSkillUI, EquipSkillUI, SkillPointUI 포함)
    [SerializeField] private GameObject clearRewardUI;  // ClearRewardUI 오브젝트 (SelectSkillUI, EquipSkillUI 포함)

    private int currentStage;
    private int currentRound;
    private int currentEnemyCount;

    protected override UIState GetUIState()
    {
        // 이 UI는 UIState.Battle 상태일 때만 활성화
        return UIState.Battle;
    }

    // 초기화 (UIManager에서 호출)
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        // 처음엔 오버레이 패널들 비활성화
        startStageUI.SetActive(true);
        clearRewardUI.SetActive(false);

        // 배틀 정보 초기화 (예: 스테이지1, 라운드1, 적0)
        UpdateBattleInfo(1, 1, 0);
    }

    // 상태 전환 시 UI 활성/비활성
    public override void SetActive(UIState currentState)
    {
        // Battle 상태일 때만 보이도록
        gameObject.SetActive(currentState == UIState.Battle);
    }
    
    // 배틀 정보(스테이지, 라운드, 남은 적)를 갱신하여 텍스트 표시
    public void UpdateBattleInfo(int stage, int round, int enemyCount)
    {
        currentStage = stage;
        currentRound = round;
        currentEnemyCount = enemyCount;

        if (stageText != null) 
            stageText.text = $"Stage {currentStage}";
        if (roundText != null) 
            roundText.text = $"Round {currentRound}";
        if (enemyCountText != null) 
            enemyCountText.text = $"Enemies: {currentEnemyCount}";
    }

 
    // 배틀씬 최초 접근 시, StartStageUI를 키고 배틀 일시정지
    public void ShowStartStageUI()
    {
        if (startStageUI != null)
        {
            startStageUI.SetActive(true);
            PauseBattle();
        }
    }

  
    // StartStageUI 끄고 배틀 재개
    public void HideStartStageUI()
    {
        if (startStageUI != null)
        {
            startStageUI.SetActive(false);
        }
    }


    // 라운드 종료 시, ClearRewardUI를 띄우고 배틀 일시정지
    public void ShowClearRewardUI()
    {
        if (clearRewardUI != null)
        {
            clearRewardUI.SetActive(true);
            PauseBattle();
        }
    }
    
    // ClearRewardUI 닫고 배틀 재개
    public void HideClearRewardUI()
    {
        if (clearRewardUI != null)
        {
            clearRewardUI.SetActive(false);
        }
    }
    
    // 배틀 진행 일시정지
    private void PauseBattle()
    {
        Time.timeScale = 0;
    }
    

    
}