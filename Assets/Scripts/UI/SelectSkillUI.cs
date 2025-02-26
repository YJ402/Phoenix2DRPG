using UnityEngine;
using UnityEngine.UI;
using System;

public class SelectSkillUI : BaseUI
{
    [Header("Select Skill UI Root")]
    [SerializeField] private GameObject selectSkillUI; // Unity에서 연결 (필요하다면)

    // [Header("Skill Slots")]
    [Serializable]
    public class SkillSlot
    {
        public Text skillInfoText;
        public Button selectButton;
    }

    [Header("Slot Array")]
    [SerializeField] private SkillSlot[] skillSlots = new SkillSlot[3];
    
    protected override UIState GetUIState()
    {
        return UIState.SelectSkill;
    }
    
    private SkillManager skillManager;
    
    // 최초 스킬 선택 - UI가 활성화될 때마다 초기화
    private bool isFirstSkillSelection = true;
    
    private void OnEnable()
    {
        isFirstSkillSelection = true;
    }
    
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
        
    }

    // 스킬 선택 버튼을 눌렀을 때
    private void OnSelectSkill(int slotIndex)
    {
        Debug.Log($"Skill Slot {slotIndex} selected.");
        
        // 최초 스킬 선택 - 최초 1회는 스킬 포인트 체크 없이 진행
        if (isFirstSkillSelection)
        {
            Debug.Log("최초 스킬 선택: 스킬 포인트 차감 없이 스킬 장착.");
            isFirstSkillSelection = false;
        }
        else
        {
            // 최초 이후는 SkillManager 내에서 스킬 포인트 체크 및 차감 로직이 진행됨
        }

        // SkillManager에 선택된 스킬 옵션 전달
        if (skillManager != null)
        {
            skillManager.SelectSkillOption(slotIndex);
        }

        // UI 닫기
        if (selectSkillUI != null)
        {
            selectSkillUI.SetActive(false);
        }
    }
}