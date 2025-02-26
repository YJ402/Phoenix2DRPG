using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    // Inspector에 등록된 전체 스킬 목록 (BaseSkill을 상속한 스킬들)
    public List<BaseSkill> everyskill;

    // 보상으로 보여줄 3개의 스킬 옵션 (중복 없이, 순서 무작위)
    private List<BaseSkill> randomSkill = new List<BaseSkill>();

    public GameObject player;
    public PlayerSkill playerSkill;

    // 현재 적용된 액티브 스킬 (한 슬롯)
    private ActiveSkill currentActiveSkill;


    // 이미 적용된 패시브 스킬들을 관리하는 딕셔너리 (키: 스킬 타입, 값: 해당 패시브 스킬 컴포넌트)
    private Dictionary<System.Type, PassiveSkill> passiveSkills = new Dictionary<System.Type, PassiveSkill>();

    

    public void MakeSkillOptions()                   //스킬선택 UI가 호출될때 같이 호출되는 메서드 (스킬 선택지 생성)
    {
        randomSkill.Clear();

        if (everyskill.Count < 3)
        {
            Debug.LogError("사용 가능한 스킬이 3개 미만입니다.");
            return;
        }

        // 보상 옵션으로 선택된 스킬의 인덱스를 저장할 변수 (selectedNumbers)
        List<int> selectedNumbers = new List<int>();
        while (randomSkill.Count < 3)
        {
            int index = Random.Range(0, everyskill.Count);
            if (!selectedNumbers.Contains(index))
            {
                selectedNumbers.Add(index);
                randomSkill.Add(everyskill[index]);
            }
        }

        // 선택된 스킬 옵션들의 순서를 무작위로 섞습니다.
        ShuffleList(randomSkill);
    }

    public List<BaseSkill> GetRandomSkillOptions()
    {
        return randomSkill;
    }

    public void SelectSkillOption(int index)                    //스킬선택했을때 호출할 메서드
    {
        if (index < 0 || index >= randomSkill.Count)
        {
            return;
        }

        BaseSkill selectedSkill = randomSkill[index];

        // 선택된 스킬이 액티브 스킬인지 패시브 스킬인지에 따라 분기
        if (selectedSkill is ActiveSkill)
        {
            HandleActiveSkill(selectedSkill as ActiveSkill);
        }
        else if (selectedSkill is PassiveSkill)
        {
            HandlePassiveSkill(selectedSkill as PassiveSkill);
        }
    }

    private void HandleActiveSkill(ActiveSkill newSkill)
    {
        currentActiveSkill = newSkill;

        if (playerSkill != null)
        {
            playerSkill.SetorUpgradeActiveSkill(newSkill);
        }
    }

    private void HandlePassiveSkill(PassiveSkill newSkill)
    {
        System.Type skillType = newSkill.GetType();
        if (passiveSkills.ContainsKey(skillType))
        {
            PassiveSkill existingSkill = passiveSkills[skillType];
            existingSkill.UpgradeSkill();
        }
        else
        {
            PassiveSkill addedSkill = player.AddComponent(skillType) as PassiveSkill;
            addedSkill.skillName = newSkill.skillName;
            addedSkill.level = newSkill.level;
            passiveSkills.Add(skillType, addedSkill);
        }
    }


    private void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
    public void ApplySkill()
    {

    }
}
