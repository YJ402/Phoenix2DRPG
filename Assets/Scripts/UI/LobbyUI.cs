using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyUI : BaseUI
{
    protected override UIState GetUIState()
    {
        return UIState.Lobby;
    }
    
    // 스테이지는 개별 생성, 추후 스테이지 개수 늘어나면 리스트 방식으로 변경 필요
    [Header("Stage Select Buttons")]
    [SerializeField] private Button stageButton1;
    [SerializeField] private Button stageButton2;
    [SerializeField] private Button stageButton3;
    
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        
        // 각 버튼의 클릭 이벤트 연결
        if (stageButton1 != null)
            stageButton1.onClick.AddListener(() => OnSelectStage(1));
    
        if (stageButton2 != null)
            stageButton2.onClick.AddListener(() => OnSelectStage(2));
    
        if (stageButton3 != null)
            stageButton3.onClick.AddListener(() => OnSelectStage(3));
    }
    
    // 스테이지 버튼 클릭 시 호출되는 함수
    private void OnSelectStage(int stage)
    {
        PlayerData.Instance.CurrentStage = stage;
        Debug.Log("Stage selected. Transition to battle scene...");
        SceneManager.LoadScene("BattleScene_Test");
    }
    
    
}