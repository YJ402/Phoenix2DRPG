using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPointUI : MonoBehaviour
{

    [SerializeField] private Text skillPointText;
    private void Update()
    {
        if (skillPointText != null && PlayerData.Instance != null)
        {
            // 문자열로 변환해 표시
            skillPointText.text = PlayerData.Instance.SkillPoint.ToString();
        }
    }
    
    // 스킬 포인트가 변동될 때만 업데이트하도록 개선할 수 있다..
    
}