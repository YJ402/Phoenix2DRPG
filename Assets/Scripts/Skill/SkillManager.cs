using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ� ����
    public static SkillManager Instance { get; private set; }
    
    // Awake���� �ν��Ͻ� �ʱ�ȭ
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // Inspector�� ��ϵ� ��ü ��ų ��� (BaseSkill�� ����� ��ų��)
    public List<BaseSkill> everyskill;

    // �������� ������ 3���� ��ų �ɼ� (�ߺ� ����, ���� ������) //@@ 2 �ֵ�������
    public List<BaseSkill> randomSkill = new List<BaseSkill>();

    public GameObject player;
    public PlayerSkill playerSkill;

    // ���� ����� ��Ƽ�� ��ų (�� ����), playerData �� �ִ� ���� ��ų ���� ��������
    private PlayerData HasSkill;


    // �̹� ����� �нú� ��ų���� �����ϴ� ��ųʸ� (Ű: ��ų Ÿ��, ��: �ش� �нú� ��ų ������Ʈ)
    private Dictionary<System.Type, PassiveSkill> passiveSkills = new Dictionary<System.Type, PassiveSkill>();

    // ���� Ȯ�ο� �߰� >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    
    // ���� ��ų ���� - SelectSkillUI�� Ȱ��ȭ�� ������ 
    private bool isFirstSkillSelection = true;
    
    private PlayerData playerData;
    
    public void SelectSkillOption(int index)
    {
        if (index < 0 || index >= randomSkill.Count)
        {
            return;
        }

        BaseSkill selectedSkill = randomSkill[index];

        // ��ų ����Ʈ ����
        PlayerData.Instance.SkillPoint--;
        Debug.Log($"��ų ����Ʈ 1�� ���. ���� ����Ʈ: {PlayerData.Instance.SkillPoint}");
        
        // ���õ� ��ų�� ��Ƽ�� ��ų���� �нú� ��ų���� �б�
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
        // currentActiveSkill = newSkill;

        if (playerSkill != null)
        {
            // ������ ���� Ÿ���� ��Ƽ�� ��ų�� ������ ���� ��
            playerSkill.SetorUpgradeActiveSkill(newSkill);
        }
    }

    private void HandlePassiveSkill(PassiveSkill newSkill)
    {
        System.Type skillType = newSkill.GetType();
        if (passiveSkills.ContainsKey(skillType))
        {
            // �̹� ���� Ÿ���� �нú� ��ų�� ������ ���� ��
            PassiveSkill existingSkill = passiveSkills[skillType];
            existingSkill.UpgradeSkill();
            Debug.Log($"�нú� ��ų {newSkill.skillName} ���� ��!");
        }
        else
        {
            // ���� ����
            PassiveSkill addedSkill = player.AddComponent(skillType) as PassiveSkill;
            addedSkill.skillName = newSkill.skillName;
            addedSkill.level = newSkill.level;
            passiveSkills.Add(skillType, addedSkill);
            Debug.Log($"�нú� ��ų {newSkill.skillName} ���� ����");
        }
    }
    // ���� Ȯ�ο� �߰� <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

    public void MakeSkillOptions()     // @@@�ָ� ȣ��              //��ų���� UI�� ȣ��ɶ� ���� ȣ��Ǵ� �޼��� (��ų ������ ����)
    {
        randomSkill.Clear();

        if (everyskill.Count < 3)
        {
            Debug.LogError("��� ������ ��ų�� 3�� �̸��Դϴ�.");
            return;
        }

        // ���� �ɼ����� ���õ� ��ų�� �ε����� ������ ���� (selectedNumbers)
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

        // ���õ� ��ų �ɼǵ��� ������ �������� �����ϴ�.
        ShuffleList(randomSkill);
    }

    public List<BaseSkill> GetRandomSkillOptions()
    {
        return randomSkill;
    }
    // ���� Ȯ�ο� ��Ȱ��ȭ >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    // public void SelectSkillOption(int index)                    //��ų���������� ȣ���� �޼���
    // {
    //     if (index < 0 || index >= randomSkill.Count)
    //     {
    //         return;
    //     }
    //
    //     BaseSkill selectedSkill = randomSkill[index];
    //
    //     // ���õ� ��ų�� ��Ƽ�� ��ų���� �нú� ��ų������ ���� �б�
    //     if (selectedSkill is ActiveSkill)
    //     {
    //         HandleActiveSkill(selectedSkill as ActiveSkill);
    //     }
    //     else if (selectedSkill is PassiveSkill)
    //     {
    //         HandlePassiveSkill(selectedSkill as PassiveSkill);
    //     }
    // }
    //
    // private void HandleActiveSkill(ActiveSkill newSkill)
    // {
    //     currentActiveSkill = newSkill;
    //
    //     if (playerSkill != null)
    //     {
    //         playerSkill.SetorUpgradeActiveSkill(newSkill);
    //     }
    // }
    //
    // private void HandlePassiveSkill(PassiveSkill newSkill)
    // {
    //     System.Type skillType = newSkill.GetType();
    //     if (passiveSkills.ContainsKey(skillType))
    //     {
    //         PassiveSkill existingSkill = passiveSkills[skillType];
    //         existingSkill.UpgradeSkill();
    //     }
    //     else
    //     {
    //         PassiveSkill addedSkill = player.AddComponent(skillType) as PassiveSkill;
    //         addedSkill.skillName = newSkill.skillName;
    //         addedSkill.level = newSkill.level;
    //         passiveSkills.Add(skillType, addedSkill);
    //     }
    // }
    // ���� Ȯ�ο� ��Ȱ��ȭ <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

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
