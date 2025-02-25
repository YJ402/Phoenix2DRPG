// using UnityEngine;
// using UnityEngine.UI;
//
// // BaseUI를 상속받는 예시
// public class BattleUI : BaseUI
// {
//     [Header("Battle UI Elements")]
//     [SerializeField] private Text stageText;
//     [SerializeField] private Text roundText;
//     [SerializeField] private Text enemyLeftText;
//
//     // BaseUI에서 Init(this)로 UIManager를 전달받음
//     public override void Init(UIManager manager)
//     {
//         base.Init(manager);
//         // 필요한 초기화 로직 추가 가능
//     }
//
//     // UIState에 따라 활성/비활성 제어
//     public override void SetActive(UIState currentState)
//     {
//         // 예: UIState.Game일 때만 활성화한다면
//         bool isActive = (currentState == UIState.Game);
//         gameObject.SetActive(isActive);
//     }
//
//     // 스테이지/라운드/남은 적 수 등 배틀 정보 업데이트
//     public void UpdateBattleInfo(int stage, int round, int enemyCount)
//     {
//         if (stageText != null)
//             stageText.text = $"Stage {stage}";
//         
//         if (roundText != null)
//             roundText.text = $"Round {round}";
//         
//         if (enemyLeftText != null)
//             enemyLeftText.text = $"Enemy Left {enemyCount}";
//     }
// }