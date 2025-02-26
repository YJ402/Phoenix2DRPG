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

    // ���� ����� ��Ƽ�� ��ų (�� ����)
    private ActiveSkill currentActiveSkill;


    // �̹� ����� �нú� ��ų���� �����ϴ� ��ųʸ� (Ű: ��ų Ÿ��, ��: �ش� �нú� ��ų ������Ʈ)
    private Dictionary<System.Type, PassiveSkill> passiveSkills = new Dictionary<System.Type, PassiveSkill>();

    

    public void MakeSkillOptions()                   //��ų���� UI�� ȣ��ɶ� ���� ȣ��Ǵ� �޼��� (��ų ������ ����)
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

    public void SelectSkillOption(int index)                    //��ų���������� ȣ���� �޼���
    {
        if (index < 0 || index >= randomSkill.Count)
        {
            return;
        }

        BaseSkill selectedSkill = randomSkill[index];

        // ���õ� ��ų�� ��Ƽ�� ��ų���� �нú� ��ų������ ���� �б�
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
