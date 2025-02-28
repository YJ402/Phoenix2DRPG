using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    // Inspector�� ��ϵ� ��ü ��ų ��� (BaseSkill�� ����� ��ų��)
    public List<BaseSkill> everyskill;
    // �������� ������ 3���� ��ų �ɼ� (�ߺ� ����, ���� ������)
    private List<BaseSkill> randomSkill = new List<BaseSkill>();

    public GameObject player;
    public PlayerSkill playerSkill;

    // �̹� ����� �нú� ��ų���� �����ϴ� ��ųʸ� (Ű: ��ų Ÿ��, ��: �ش� �нú� ��ų ������Ʈ)
    private Dictionary<System.Type, PassiveSkill> passiveSkills = new Dictionary<System.Type, PassiveSkill>();

    // ���� ��ų ���� ���� (SelectSkillUI�� Ȱ��ȭ�� ������)
    private bool isFirstSkillSelection = true;

    private PlayerData playerData;

    public void SelectSkillOption(int index, PlayerData playerData)
    {
        if (index < 0 || index >= randomSkill.Count) return;

        // PlayerData�� PlayerLevel�� ��ų ����Ʈ�� ��� (����)
        int skillPoints = PlayerData.Instance.PlayerLevel; // ����

        if (skillPoints <= 0)
        {
            Debug.Log("��ų ����Ʈ�� �����մϴ�!");
            return;
        }

        BaseSkill selectedSkill = randomSkill[index];

        // ��ų ����Ʈ ���� (���⼭�� SkillPoint�� ����)
        PlayerData.Instance.SkillPoint--;
        Debug.Log($"��ų ����Ʈ 1�� ���. ���� ����Ʈ: {PlayerData.Instance.SkillPoint}");

        // ���õ� ��ų�� ��Ƽ������ �нú����� �б�
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
            Debug.Log($"�нú� ��ų {newSkill.skillName} ���� ��!");
        }
        else
        {
            PassiveSkill addedSkill = player.AddComponent(skillType) as PassiveSkill;
            addedSkill.skillName = newSkill.skillName;
            addedSkill.level = newSkill.level;
            passiveSkills.Add(skillType, addedSkill);
            Debug.Log($"�нú� ��ų {newSkill.skillName} ���� ����");
        }
    }

    public void MakeSkillOptions()
    {
        randomSkill.Clear();

        if (everyskill.Count < 3)
        {
            Debug.LogError("��� ������ ��ų�� 3�� �̸��Դϴ�.");
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
        // �߰� ������ �ʿ��ϸ� ���⿡ �ۼ�
    }
}
