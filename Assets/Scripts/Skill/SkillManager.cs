using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance { get; set; }
    
    // Awake에서 인스턴스 초기화
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // 필요하다면 아래 줄을 활성화해서 씬 전환 시 파괴되지 않도록 할 수 있음.
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // Inspector에 등록된 전체 스킬 목록 (BaseSkill을 상속한 스킬들)
    public List<BaseSkill> everyskill;

    // 보상으로 보여줄 3개의 스킬 옵션 (중복 없이, 순서 무작위) //@@ 2 애도가저옴
    public List<BaseSkill> randomSkill = new List<BaseSkill>();

    public GameObject player;
    public PlayerSkill playerSkill;

    // 현재 적용된 액티브 스킬 (한 슬롯), playerData 에 있는 장착 스킬 정보 가져오기
    private PlayerData HasSkill;


    // 이미 적용된 패시브 스킬들을 관리하는 딕셔너리 (키: 스킬 타입, 값: 해당 패시브 스킬 컴포넌트)
    private Dictionary<System.Type, PassiveSkill> passiveSkills = new Dictionary<System.Type, PassiveSkill>();

    // 구동 확인용 추가 >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    
    // 최초 스킬 선택 - SelectSkillUI가 활성화될 때마다 
    private bool isFirstSkillSelection = true;
    
    private PlayerData playerData;
    
    public void SelectSkillOption(int index, PlayerData playerData)
    {
        if (index < 0 || index >= randomSkill.Count) return;

        BaseSkill selectedSkill = randomSkill[index];

        // 스킬 포인트 차감
        PlayerData.Instance.PlayerLevel--;
        Debug.Log($"스킬 포인트 1점 사용. 남은 포인트: {PlayerData.Instance.PlayerLevel}");
        
        // 선택된 스킬이 액티브 스킬인지 패시브 스킬인지 분기
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
        //currentActiveSkill = newSkill;

        if (playerSkill != null)
        {
            // 기존에 같은 타입의 액티브 스킬이 있으면 레벨 업
            playerSkill.SetorUpgradeActiveSkill(newSkill);
        }
    }

    private void HandlePassiveSkill(PassiveSkill newSkill)
    {
        System.Type skillType = newSkill.GetType();
        if (passiveSkills.ContainsKey(skillType))
        {
            // 이미 같은 타입의 패시브 스킬이 있으면 레벨 업
            PassiveSkill existingSkill = passiveSkills[skillType];
            existingSkill.UpgradeSkill();
            Debug.Log($"패시브 스킬 {newSkill.skillName} 레벨 업!");
        }
        else
        {
            // 새로 장착
            PassiveSkill addedSkill = player.AddComponent(skillType) as PassiveSkill;
            addedSkill.skillName = newSkill.skillName;
            addedSkill.level = newSkill.level;
            passiveSkills.Add(skillType, addedSkill);
            Debug.Log($"패시브 스킬 {newSkill.skillName} 새로 장착");
        }
    }
    // 구동 확인용 추가 <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

    public void MakeSkillOptions()     // @@@애를 호출              //스킬선택 UI가 호출될때 같이 호출되는 메서드 (스킬 선택지 생성)
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
