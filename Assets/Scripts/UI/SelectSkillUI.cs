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
    // 각 슬롯별 스킬 이름을 표시할 텍스트 배열(최소 한 요소씩 할당)
    [SerializeField] private TextMeshProUGUI[] skillNameTxt1;
    [SerializeField] private TextMeshProUGUI[] skillNameTxt2;
    [SerializeField] private TextMeshProUGUI[] skillNameTxt3;
    [SerializeField] Button skillButton1;
    [SerializeField] Button skillButton2;
    [SerializeField] Button skillButton3;
    // SkillManager 참조는 Inspector에서 연결 (변경 없이 사용)
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

        // 각 버튼의 클릭 이벤트 연결 (인덱스는 1,2,3로 설정 → 내부에서 0,1,2로 조정)
        if (skillButton1 != null)
            skillButton1.onClick.AddListener(() => OnSelectSkill(1));

        if (skillButton2 != null)
            skillButton2.onClick.AddListener(() => OnSelectSkill(2));

        if (skillButton3 != null)
            skillButton3.onClick.AddListener(() => OnSelectSkill(3));
    }
    // SkillManager의 MakeSkillOptions() 및 GetRandomSkillOptions()를 사용해 슬롯 갱신
    private void UpdateSkillSelectUI()
    {
        skillPointTxt.text = skillPoint.ToString();

        if (skillManager == null) return;

        skillManager.MakeSkillOptions();
        List<BaseSkill> randomSkills = skillManager.GetRandomSkillOptions();

        if (randomSkills.Count >= 3)
        {
            Debug.Log($"Slot1 skillName = {randomSkills[0].skillName}");
            Debug.Log($"Slot2 skillName = {randomSkills[1].skillName}");
            Debug.Log($"Slot3 skillName = {randomSkills[2].skillName}");
        }

        // 만약 스킬이 3개 뽑혔다고 가정
        if (randomSkills.Count >= 3)
        {
            // skillNameTxt1[0], skillNameTxt2[0], skillNameTxt3[0]에 스킬 이름 할당
            if (skillNameTxt1 != null && skillNameTxt1.Length > 0)
                skillNameTxt1[0].text = randomSkills[0].skillName;

            if (skillNameTxt2 != null && skillNameTxt2.Length > 0)
                skillNameTxt2[0].text = randomSkills[1].skillName;

            if (skillNameTxt3 != null && skillNameTxt3.Length > 0)
                skillNameTxt3[0].text = randomSkills[2].skillName;
        }
    }



    // 슬롯 선택 시 호출되는 메서드
    private void OnSelectSkill(int slotIndex)
    {
        Debug.Log("Skill Slot selected.");

        // 인덱스 조정 (버튼에서 1,2,3을 보내므로 내부에서는 0,1,2로)
        int adjustedIndex = slotIndex - 1;
        if (skillManager != null && PlayerData.Instance != null)
        {
            skillManager.SelectSkillOption(adjustedIndex, PlayerData.Instance);
        }

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
