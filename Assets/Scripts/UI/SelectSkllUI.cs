using UnityEngine;
using UnityEngine.UI;
using System;

public class SelectSkillUI : BaseUI
{
    [Header("Select Skill UI Root")]
    [SerializeField] private GameObject selectSkillUI; // Unity에서 연결 (필요하다면)

    [Header("UI Text Elements")]
    [SerializeField] private Text titleText;   // "Select Skill"
    [SerializeField] private Text pointText;   // "Point 3" 등 포인트 표시

    // [Header("Skill Slots")]
    [Serializable]
    public class SkillSlot
    {
        public Text skillInfoText;
        public Button selectButton;
    }

    [Header("Slot Array")]
    [SerializeField] private SkillSlot[] skillSlots = new SkillSlot[3];

    // BaseUI 구현: 어떤 UIState를 담당하는지 반환
    protected override UIState GetUIState()
    {
        return UIState.SelectSkill;
    }

    // 초기화 (UIManager에서 호출)
    public override void Init(UIManager manager)
    {
        base.Init(manager);

        // 스킬 슬롯의 버튼에 클릭 이벤트 등록
        for (int i = 0; i < skillSlots.Length; i++)
        {
            int index = i; // 람다 캡처용 임시 변수
            if (skillSlots[i].selectButton != null)
            {
                skillSlots[i].selectButton.onClick.AddListener(() => OnSelectSkill(index));
            }
        }

        // 필요하다면 제목/포인트 텍스트 초기화
        if (titleText != null) titleText.text = "Select Skill";
        if (pointText != null) pointText.text = "Point 3";
    }

    // 상태 전환 시 활성/비활성
    public override void SetActive(UIState currentState)
    {
        // SelectSkillUI는 UIState.SelectSkill일 때만 활성화
        bool isActive = (currentState == UIState.SelectSkill);
        gameObject.SetActive(isActive);
    }

    // 스킬 슬롯 정보를 갱신하는 함수 (예시)
    public void UpdateSkillSlot(int slotIndex, string info)
    {
        if (slotIndex < 0 || slotIndex >= skillSlots.Length) return;

        var slot = skillSlots[slotIndex];
        if (slot.skillInfoText != null)
        {
            slot.skillInfoText.text = info;
        }
    }

    // 스킬 선택 버튼을 눌렀을 때
    private void OnSelectSkill(int slotIndex)
    {
        Debug.Log($"Skill Slot {slotIndex} selected.");

        // selectSkillUI 오브젝트를 꺼야 한다면:
        if (selectSkillUI != null)
        {
            selectSkillUI.SetActive(false);
        }
        else
        {
            Debug.LogWarning("selectSkillUI 오브젝트가 설정되지 않았습니다.");
        }

        // 이후 스킬 선택 로직, 씬 전환, 포인트 차감 등 원하는 작업을 진행
    }
}