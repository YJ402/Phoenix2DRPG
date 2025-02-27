using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EquipSkillUI : BaseUI
{
    [Header("Active Skill UI")]
    [SerializeField] private Image skillImage;         // ActiveSkills → Skill → Image
    [SerializeField] private Text skillInfoText;       // ActiveSkills → Skill → SkillInfo
    [SerializeField] private Text skillLevelNumber;    // ActiveSkills → Skill → SkillLevel → LevelNumber
    [SerializeField] private Text skillLevelText;      // ActiveSkills → Skill → SkillLevel → LevelText

    [Header("Passive Skills Scroll View")]
    [SerializeField] private Transform passiveSkillsContent; // Scroll View → Viewport → Content
    [SerializeField] private GameObject passiveSkillTextPrefab; 
    // 패시브 스킬 하나를 표시할 텍스트 프리팹 (Text 컴포넌트만 있어도 됨)
    
    protected override UIState GetUIState()
    {
        return UIState.EquipSkill;
    }

    // 상태 전환 시 활성/비활성
    public override void SetActive(UIState currentState)
    {
        bool isActive = (currentState == UIState.EquipSkill);
        gameObject.SetActive(isActive);
    }

    // 액티브 스킬 정보를 UI에 표시
    public void ActiveSkills(ActiveSkill activeSkill)
    {
        if (activeSkill != null)
        {
            // if (skillImage != null)
            //     skillImage.sprite = activeSkill.skillImage; // 이미지예: BaseSkill에 sprite 필드가 있다고 가정
            if (skillInfoText != null)
                skillInfoText.text = activeSkill.skillName; // 스킬 이름/정보
            if (skillLevelNumber != null)
                skillLevelNumber.text = activeSkill.level.ToString();
            if (skillLevelText != null)
                skillLevelText.text = "Lv."; // 레벨 텍스트 접두사 (원하면 "Lv." + activeSkill.level로 합쳐도 됨)
        }
    }
    
    // 여러 개의 패시브 스킬 정보를 Scroll View에 텍스트로 표시
    public void PassiveSkills(List<PassiveSkill> passiveSkills)
    {
        // 패시브 스킬 리스트를 순회하여 각 스킬 정보를 텍스트로 생성
        foreach (var pSkill in passiveSkills)
        {
            if (pSkill == null) continue;
            string displayText = $"{pSkill.skillName} (Lv.{pSkill.level})";
            CreatePassiveSkillText(displayText);
        }
    }


    // Scroll View Content 아래에 패시브 스킬 텍스트 하나를 생성
    private void CreatePassiveSkillText(string textValue)
    {
        if (passiveSkillsContent == null || passiveSkillTextPrefab == null)
        {
            Debug.LogWarning("PassiveSkill UI references not set properly.");
            return;
        }

        GameObject textObj = Instantiate(passiveSkillTextPrefab, passiveSkillsContent);
        Text txt = textObj.GetComponent<Text>();
        if (txt != null)
        {
            txt.text = textValue;
        }
    }
    
}