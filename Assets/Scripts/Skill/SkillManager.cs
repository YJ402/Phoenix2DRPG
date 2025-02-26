using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PassiveSkillData
{
    public string skillName;
    public int level;
    public string typeName; // 스킬 타입의 FullName
}

public class SkillManager : MonoBehaviour
{
    // Inspector에 등록된 전체 스킬 목록 (BaseSkill을 상속한 스킬들)
    public List<BaseSkill> everyskill;

    // 보상으로 보여줄 3개의 스킬 옵션 (중복 없이, 순서 무작위)
    private List<BaseSkill> randomSkill = new List<BaseSkill>();

    public GameObject player;
    public PlayerSkill playerSkill;

    // 싱글턴 인스턴스 (씬 전환 후에도 유지됨)
    public static SkillManager Instance { get; private set; }

    // 패시브 스킬 프리팹 참조들을 저장하는 리스트
    // 이 리스트는 플레이어에게 적용된 패시브 스킬 프리팹들을 저장하여
    // 씬 전환 후 새 플레이어에 다시 적용할 때 사용합니다.
    private List<GameObject> savedPassiveSkillPrefabs = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // 씬 전환 시 파괴되지 않도록 함
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    /// <summary>
    /// 보상 스킬 선택 UI가 호출될 때, everyskill 목록에서 중복 없이 3개의 스킬 옵션을 생성합니다.
    /// </summary>
    public void MakeSkillOptions()
    {
        randomSkill.Clear();

        if (everyskill.Count < 3)
        {
            Debug.LogError("사용 가능한 스킬이 3개 미만입니다.");
            return;
        }

        // 이미 선택된 인덱스를 저장할 변수 (selectedNumbers)
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

    /// <summary>
    /// 외부(UI)에서 보상 옵션 리스트를 가져갈 수 있도록 Getter를 제공합니다.
    /// </summary>
    public List<BaseSkill> GetRandomSkillOptions()
    {
        return randomSkill;
    }

    /// <summary>
    /// 플레이어가 보상 스킬 옵션을 선택했을 때 호출되는 메서드입니다.
    /// </summary>
    public void SelectSkillOption(int index)
    {
        if (index < 0 || index >= randomSkill.Count)
        {
            return;
        }

        BaseSkill selectedSkill = randomSkill[index];

        // 선택된 스킬이 액티브 스킬이면 처리
        if (selectedSkill is ActiveSkill)
        {
            HandleActiveSkill(selectedSkill as ActiveSkill);
        }
        // 선택된 스킬이 패시브 스킬이면 처리
        else if (selectedSkill is PassiveSkill)
        {
            // 여기서 프리팹으로서 패시브 스킬을 적용
            ApplyPassiveSkill(player, (selectedSkill as PassiveSkill).gameObject);
        }
    }

    /// <summary>
    /// 액티브 스킬은 PlayerSkill에 위임하여 설정하거나 업그레이드합니다.
    /// </summary>
    private void HandleActiveSkill(ActiveSkill newSkill)
    {
        if (playerSkill != null)
        {
            playerSkill.SetOrUpgradeActiveSkill(newSkill);
        }
    }

    /// <summary>
    /// 패시브 스킬 프리팹을 플레이어에 적용합니다.
    /// 만약 이미 해당 프리팹이 저장되어 있다면, 이미 적용된 스킬의 UpgradeSkill()을 호출하여 레벨을 올립니다.
    /// </summary>
    /// <param name="player">패시브 스킬을 적용할 플레이어 GameObject</param>
    /// <param name="passiveSkillPrefab">패시브 스킬 프리팹 (해당 스킬의 GameObject)</param>
    public void ApplyPassiveSkill(GameObject player, GameObject passiveSkillPrefab)
    {
        if (savedPassiveSkillPrefabs.Contains(passiveSkillPrefab))
        {
            // 이미 해당 프리팹이 적용되어 있다면, player에서 해당 스킬 컴포넌트를 찾아 업그레이드합니다.
            PassiveSkill existingSkill = player.GetComponent(passiveSkillPrefab.GetComponent<PassiveSkill>().GetType()) as PassiveSkill;
            if (existingSkill != null)
            {
                existingSkill.UpgradeSkill();
                Debug.Log("패시브 스킬 업그레이드: " + existingSkill.skillName + " Level: " + existingSkill.level);
            }
            else
            {
                Debug.LogError("이미 적용된 패시브 스킬 컴포넌트를 찾을 수 없습니다.");
            }
        }
        else
        {
            // 새로운 패시브 스킬이면, 프리팹을 인스턴스화하여 player에 추가하고 저장합니다.
            GameObject instance = Instantiate(passiveSkillPrefab, player.transform);
            PassiveSkill ps = instance.GetComponent<PassiveSkill>();
            if (ps != null)
            {
                Debug.Log("패시브 스킬 추가: " + ps.skillName);
                savedPassiveSkillPrefabs.Add(passiveSkillPrefab);
            }
            else
            {
                Debug.LogError("인스턴스화한 객체에 PassiveSkill 컴포넌트가 없습니다.");
            }
        }
    }

    /// <summary>
    /// 씬 전환 후, 새 플레이어 GameObject에 저장된 패시브 스킬 프리팹들을 재적용합니다.
    /// </summary>
    /// <param name="newPlayer">새로운 플레이어 GameObject</param>
    public void ReapplyPassiveSkills(GameObject newPlayer)
    {
        foreach (GameObject prefab in savedPassiveSkillPrefabs)
        {
            Instantiate(prefab, newPlayer.transform);
        }
    }

    /// <summary>
    /// 리스트의 요소들을 무작위로 섞는 유틸리티 메서드 (Fisher-Yates 알고리즘)
    /// </summary>
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

    /// <summary>
    /// 필요할 때 SkillManager의 자식 오브젝트(예: 보상 스킬 옵션)를 정리합니다.
    /// </summary>
    public void ClearSkillList()
    {
        foreach (Transform skill in transform)
        {
            Destroy(skill.gameObject);
        }
    }
}
