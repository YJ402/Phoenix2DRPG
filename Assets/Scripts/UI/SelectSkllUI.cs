// using UnityEngine;
// using UnityEngine.UI;
// using System;
//
// // BaseUI를 상속한다고 가정 (없다면 MonoBehaviour로 교체)
// public class SelectSkillUI : BaseUI
// {
//     [Header("UI Text Elements")]
//     [SerializeField] private Text titleText;   // "Select Skill"
//     [SerializeField] private Text pointText;   // "Point 3" 등 포인트 표시
//
//     [Header("Skill Slots")]
//     // 스킬 슬롯 구조체
//     [Serializable]
//     public class SkillSlot
//     {
//         public Image skillIcon;      // 스킬 아이콘 이미지
//         public Text skillInfoText;   // "SkillInfoText"
//         public Button selectButton;  // "Select" 버튼
//     }
//
//     // 3개의 슬롯을 배열로 관리
//     [SerializeField] private SkillSlot[] skillSlots = new SkillSlot[3];
//
//     // UIManager 연동 (BaseUI 상속 시)
//     public override void Init(UIManager manager)
//     {
//         base.Init(manager);
//
//         // 스킬 슬롯의 버튼에 클릭 이벤트 등록
//         for (int i = 0; i < skillSlots.Length; i++)
//         {
//             int index = i; // 캡처 변수
//             if (skillSlots[i].selectButton != null)
//             {
//                 skillSlots[i].selectButton.onClick.AddListener(() => OnSelectSkill(index));
//             }
//         }
//
//         // 기본 제목 설정 (원한다면 Inspector에서 직접 설정 가능)
//         if (titleText != null)
//         {
//             titleText.text = "Select Skill";
//         }
//     }
//
//     public override void SetActive(UIState currentState)
//     {
//         // 예: UIState.Home → 로비, UIState.Game → 전투, UIState.SelectSkill → 스킬 선택
//         // 프로젝트 상황에 맞게 상태 조건을 바꾸세요.
//         bool isActive = (currentState == UIState.Home); 
//         gameObject.SetActive(isActive);
//     }
//
//     // 스킬 슬롯 정보를 갱신하는 함수
//     public void UpdateSkillSlot(int slotIndex, Sprite icon, string info)
//     {
//         if (slotIndex < 0 || slotIndex >= skillSlots.Length) return;
//
//         SkillSlot slot = skillSlots[slotIndex];
//         if (slot.skillIcon != null)
//             slot.skillIcon.sprite = icon;
//
//         if (slot.skillInfoText != null)
//             slot.skillInfoText.text = info;
//     }
//
//     // 상단 포인트 표시
//     public void UpdatePoint(int point)
//     {
//         if (pointText != null)
//         {
//             pointText.text = $"Point {point}";
//         }
//     }
//
//     // 스킬 선택 버튼을 눌렀을 때 호출
//     private void OnSelectSkill(int slotIndex)
//     {
//         Debug.Log($"Skill Slot {slotIndex} selected.");
//         // 여기서 실제 로직(예: 게임 매니저에 선택 결과 전달, 씬 전환 등)을 수행
//         // e.g. uiManager.ChangeState(UIState.Game) or SceneManager.LoadScene("BattleScene");
//     }
// }