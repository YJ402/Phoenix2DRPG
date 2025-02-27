using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectSkillUI : BaseUI
{
    protected override UIState GetUIState()
    {
        return UIState.SelectSkill;
    }
    
    public Text SkillListText;
    
    public void Start()
    { 
        SkillManager.Instance.MakeSkillOptions();
        SkillListText.text = MakeSkillOptions();
    }

    // 스킬 선택지를 텍스트로 출력하는 함수
    string MakeSkillOptions()
    {
        // 열때마다 옵션 갱신내용 여기에
        List<BaseSkill> randomSkill = SkillManager.Instance.randomSkill;
        
            
        string result = "";
        for (int i = 0; i < randomSkill.Count; i++)
        {
            result += $"{i + 1}. {randomSkill[i].name}\n";
        }
        return result;
    }

    public void Update()
    {
        
    }
}