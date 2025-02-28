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

    // 이미 적용된 패시브 스킬들을 관리하는 딕셔너리 (키: 스킬 타입, 값: 해당 패시브 스킬 컴포넌트)
    private Dictionary<System.Type, PassiveSkill> passiveSkills = new Dictionary<System.Type, PassiveSkill>();

    // 최초 스킬 선택 여부 (SelectSkillUI가 활성화될 때마다)
    private bool isFirstSkillSelection = true;

    private PlayerData playerData;

    public void SelectSkillOption(int index, PlayerData playerData)
    {
        if (index < 0 || index >= randomSkill.Count) return;

        // PlayerData의 PlayerLevel을 스킬 포인트로 사용 (예시)
        int skillPoints = PlayerData.Instance.PlayerLevel; // 복사

        if (skillPoints <= 0)
        {
            Debug.Log("스킬 포인트가 부족합니다!");
            return;
        }

        BaseSkill selectedSkill = randomSkill[index];

        // 스킬 포인트 차감 (여기서는 SkillPoint를 차감)
        PlayerData.Instance.SkillPoint--;
        Debug.Log($"스킬 포인트 1점 사용. 남은 포인트: {PlayerData.Instance.SkillPoint}");

        // 선택된 스킬이 액티브인지 패시브인지 분기
        if (selectedSkill is ActiveSkill activeSkill)
        {
            HandleActiveSkill(activeSkill);
        }
        else if (selectedSkill is PassiveSkill passiveSkill)
        {
            HandlePassiveSkill(passiveSkill);
        }
    }

    private void HandleActiveSkill(ActiveSkill newSkill)
    {
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
            Debug.Log($"패시브 스킬 {newSkill.skillName} 레벨 업!");
        }
        else
        {
            PassiveSkill addedSkill = player.AddComponent(skillType) as PassiveSkill;
            addedSkill.skillName = newSkill.skillName;
            addedSkill.level = newSkill.level;
            passiveSkills.Add(skillType, addedSkill);
            Debug.Log($"패시브 스킬 {newSkill.skillName} 새로 장착");
        }
    }

    public void MakeSkillOptions()
    {
        randomSkill.Clear();

        if (everyskill.Count < 3)
        {
            Debug.LogError("사용 가능한 스킬이 3개 미만입니다.");
            return;
        }

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

        ShuffleList(randomSkill);
    }

    public List<BaseSkill> GetRandomSkillOptions()
    {
        return randomSkill;
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
        // 추가 구현이 필요하면 여기에 작성
    }
}
