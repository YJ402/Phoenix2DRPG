using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 씬 전환 시 필요

public class LobbyUI : BaseUI
{
    protected override UIState GetUIState()
    {
        return UIState.Lobby;
    }
    
    // [Header("Stage Select Buttons")]
    // [SerializeField] private Button selectButton1;
    // [SerializeField] private Button selectButton2;
    // [SerializeField] private Button selectButton3;
    //
    // [Header("Point Text")]
    // [SerializeField] private Text pointText; // 화면 상단 "Point 0" 텍스트 등
    //
    // // 예: 플레이어가 모은 포인트나 골드 표시를 위해
    // private int currentPoint = 0;
    //
    // public override void Init(UIManager manager)
    // {
    //     base.Init(manager);
    //
    //     // 버튼 이벤트 연결
    //     if (selectButton1 != null)
    //         selectButton1.onClick.AddListener(() => OnSelectStage(1));
    //
    //     if (selectButton2 != null)
    //         selectButton2.onClick.AddListener(() => OnSelectStage(2));
    //
    //     if (selectButton3 != null)
    //         selectButton3.onClick.AddListener(() => OnSelectStage(3));
    //
    //     // 초기 포인트 표시
    //     UpdatePointText();
    // }
    //
    // public override void SetActive(UIState currentState)
    // {
    //     // 로비 상태일 때만 활성화
    //     bool isActive = (currentState == UIState.Lobby);
    //     gameObject.SetActive(isActive);
    // }
    //
    // // 스테이지 선택 버튼 클릭 시 호출
    // private void OnSelectStage(int stageIndex)
    // {
    //     Debug.Log($"Stage {stageIndex} selected. Transition to battle scene...");
    //     // 1) UIManager를 통해 상태 전환
    //     // uiManager.ChangeState(UIState.Game);
    //     // 2) 또는 직접 씬 로드
    //     SceneManager.LoadScene("BattleScene");
    // }
    //
    // // 포인트 갱신 함수
    // public void UpdatePoint(int newPoint)
    // {
    //     currentPoint = newPoint;
    //     UpdatePointText();
    // }
    //
    // private void UpdatePointText()
    // {
    //     if (pointText != null)
    //     {
    //         pointText.text = $"Point {currentPoint}";
    //     }
    // }
}