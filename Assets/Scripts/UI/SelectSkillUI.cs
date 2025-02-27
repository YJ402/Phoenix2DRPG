using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SelectSkillUI : MonoBehaviour
{
    [Header("Skill Slots")]
    [SerializeField] private SkillSlotUI[] skillSlots; // Slot0, Slot1, Slot2를 Inspector에서 연결

    // SkillManager 참조
    private SkillManager skillManager;

    private void Awake()
    {
        // 씬 내 SkillManager를 찾음 (또는 Inspector 연결)
        skillManager = FindObjectOfType<SkillManager>();
    }

    private void OnEnable()
    {
        // UI가 활성화될 때, 스킬 목록 생성 & 슬롯 업데이트
        RefreshSkillSlots();
    }

    // SkillManager의 MakeSkillOptions() -> GetRandomSkillOptions()를 사용해 슬롯 갱신
    private void RefreshSkillSlots()
    {
        if (skillManager == null) return;

        // 1) 스킬 옵션 생성
        skillManager.MakeSkillOptions();

        // 2) 스킬 리스트 가져오기
        List<BaseSkill> randomSkills = skillManager.GetRandomSkillOptions();

        // 슬롯 갯수와 리스트 갯수를 맞춰 세팅
        for (int i = 0; i < skillSlots.Length; i++)
        {
            if (i < randomSkills.Count && randomSkills[i] != null)
            {
                BaseSkill skill = randomSkills[i];
                // 스킬 정보 설정 (예: skill.skillImage, skill.skillName)
                // onSelect 콜백: 슬롯 선택 시 SkillManager.SelectSkillOption(i)를 호출
                skillSlots[i].SetSkillData(
                    // skill.skillImage,                // 예: BaseSkill 내 sprite
                    skill.skillName,                 // 예: 스킬 이름
                    () => OnSelectSkill(i)           // 버튼 클릭 시 실행할 메서드
                );
            }
            // else
            // {
            //     // 슬롯을 비움
            //     skillSlots[i].ClearSlot();
            // }
        }
    }

    // 슬롯 선택 시 호출되는 메서드
    private void OnSelectSkill(int slotIndex)
    {
        Debug.Log($"Skill Slot {slotIndex} selected.");
        // SkillManager에 선택된 슬롯 인덱스를 전달
        skillManager.SelectSkillOption(slotIndex);

        // 스킬 선택 후 UI 닫기 등
        gameObject.SetActive(false);
    }
}