using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;

public class SelectSkillUI : BaseUI
{
    int skillPoint;
    [SerializeField] TextMeshProUGUI titleTxt;
    [SerializeField] TextMeshProUGUI skillPointTxt;
    [SerializeField] private TextMeshProUGUI[] skillNameTxt1;
    [SerializeField] private TextMeshProUGUI[] skillNameTxt2;
    [SerializeField] private TextMeshProUGUI[] skillNameTxt3;
    [SerializeField] Button skillButton1;
    [SerializeField] Button skillButton2;
    [SerializeField] Button skillButton3;
    // SkillManager 참조
    [SerializeField] SkillManager skillManager;

    protected override UIState GetUIState()
    {
        return UIState.SelectSkill;
    }

    private void OnEnable()
    {
        Time.timeScale = 0f;
        skillPoint = PlayerData.Instance.SkillPoint;
        if (PlayerData.Instance.isLevelSkillSelect)
            titleTxt.text = "Start Skill Select";
        else
            titleTxt.text = "Round Clear";
        UpdateSkillSelectUI();
    }
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        // 각 버튼의 클릭 이벤트 연결
        if (skillButton1 != null)
            skillButton1.onClick.AddListener(() => OnSelectSkill(1));

        if (skillButton2 != null)
            skillButton2.onClick.AddListener(() => OnSelectSkill(2));

        if (skillButton3 != null)
            skillButton3.onClick.AddListener(() => OnSelectSkill(3));
    }
    // SkillManager의 MakeSkillOptions() -> GetRandomSkillOptions()를 사용해 슬롯 갱신
    private void UpdateSkillSelectUI()
    {
        skillPointTxt.text = skillPoint.ToString();
        if (skillManager == null) return;
        // 1) 스킬 옵션 생성
        skillManager.MakeSkillOptions();

        // 2) 스킬 리스트 가져오기
        List<BaseSkill> randomSkills = skillManager.GetRandomSkillOptions();

        // 슬롯 갯수와 리스트 갯수를 맞춰 세팅
        
    }

    // 슬롯 선택 시 호출되는 메서드
    private void OnSelectSkill(int slotIndex)
    {
        Debug.Log($"Skill Slot selected.");
        //선택 스킬 추가

        skillPoint--;
        if (skillPoint <= 0)
        {
            Time.timeScale = 1f;
            PlayerData.Instance.isLevelSkillSelect = false;
            PlayerData.Instance.SkillPoint = 0;
            uiManager.ChangeState(UIState.Battle);
        }
        else
        {
            UpdateSkillSelectUI();
        }

    }
}