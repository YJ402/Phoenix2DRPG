using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectSkillUI : BaseUI
{
    protected override UIState GetUIState()
    {
        return UIState.SelectSkill;
    }
    
    public Text SkillListText;
    
    public void Start()
    { 
        // '초기화'하여 무료 스킬 포인트 가지기를 넣기
        SkillManager.Instance.MakeSkillOptions();
        SkillListText.text = MakeSkillOptions();
        
    }

    // 스킬 선택지를 '텍스트'로 출력하는 함수
    string MakeSkillOptions()
    {
        List<BaseSkill> randomSkill = SkillManager.Instance.randomSkill;
        // 조회하여 리스트 텍스트 넣는 곳 찾기 - > 리스트.. 블러오고. 
        string result = "";
        for (int i = 0; i < randomSkill.Count; i++)
        {
            result += $"{i + 1}. {randomSkill[i].name}\n";
        }
        return result;
    }

    public void Update()
    {
        // 갱신
    }
}