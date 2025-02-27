using UnityEngine;
using UnityEngine.UI;
using System;

public class SkillSlotUI : MonoBehaviour
{
    [SerializeField] private Text skillNameText;
    [SerializeField] private Button selectButton;

    Text skillNameText = (BaseSkill.SkillName string, level To string)
    // 슬롯에 표시할 스킬 데이터를 설정하는 함수
    public SkillManager skillManager;
    
    // 리스트는 스킬매니저에 이미 있으니까 스킬 슬롯에는 정제해서 출력
    public void SetSkillData(BaseSkill baseSkill, Action onSelect)
    {
        if (skillNameText != null)
            skillNameText.text = $"{baseSkill.skillName} (Lv. {baseSkill.level})";
    
        if (selectButton != null)
        {
            selectButton.onClick.RemoveAllListeners();
            if (onSelect != null)
                selectButton.onClick.AddListener(() => onSelect.Invoke());
        }
    }

    
}