using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PassiveSkillData
{
    public string skillName;
    public int level;
    public string typeName; // ��ų Ÿ���� FullName
}

public class SkillManager : MonoBehaviour
{
    // Inspector�� ��ϵ� ��ü ��ų ��� (BaseSkill�� ����� ��ų��)
    public List<BaseSkill> everyskill;

    // �������� ������ 3���� ��ų �ɼ� (�ߺ� ����, ���� ������)
    private List<BaseSkill> randomSkill = new List<BaseSkill>();

    public GameObject player;
    public PlayerSkill playerSkill;

    // �̱��� �ν��Ͻ� (�� ��ȯ �Ŀ��� ������)
    public static SkillManager Instance { get; private set; }

    // �нú� ��ų ������ �������� �����ϴ� ����Ʈ
    // �� ����Ʈ�� �÷��̾�� ����� �нú� ��ų �����յ��� �����Ͽ�
    // �� ��ȯ �� �� �÷��̾ �ٽ� ������ �� ����մϴ�.
    private List<GameObject> savedPassiveSkillPrefabs = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // �� ��ȯ �� �ı����� �ʵ��� ��
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    /// <summary>
    /// ���� ��ų ���� UI�� ȣ��� ��, everyskill ��Ͽ��� �ߺ� ���� 3���� ��ų �ɼ��� �����մϴ�.
    /// </summary>
    public void MakeSkillOptions()
    {
        randomSkill.Clear();

        if (everyskill.Count < 3)
        {
            Debug.LogError("��� ������ ��ų�� 3�� �̸��Դϴ�.");
            return;
        }

        // �̹� ���õ� �ε����� ������ ���� (selectedNumbers)
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

    /// <summary>
    /// �ܺ�(UI)���� ���� �ɼ� ����Ʈ�� ������ �� �ֵ��� Getter�� �����մϴ�.
    /// </summary>
    public List<BaseSkill> GetRandomSkillOptions()
    {
        return randomSkill;
    }

    /// <summary>
    /// �÷��̾ ���� ��ų �ɼ��� �������� �� ȣ��Ǵ� �޼����Դϴ�.
    /// </summary>
    public void SelectSkillOption(int index)
    {
        if (index < 0 || index >= randomSkill.Count)
        {
            return;
        }

        BaseSkill selectedSkill = randomSkill[index];

        // ���õ� ��ų�� ��Ƽ�� ��ų�̸� ó��
        if (selectedSkill is ActiveSkill)
        {
            HandleActiveSkill(selectedSkill as ActiveSkill);
        }
        // ���õ� ��ų�� �нú� ��ų�̸� ó��
        else if (selectedSkill is PassiveSkill)
        {
            // ���⼭ ���������μ� �нú� ��ų�� ����
            ApplyPassiveSkill(player, (selectedSkill as PassiveSkill).gameObject);
        }
    }

    /// <summary>
    /// ��Ƽ�� ��ų�� PlayerSkill�� �����Ͽ� �����ϰų� ���׷��̵��մϴ�.
    /// </summary>
    private void HandleActiveSkill(ActiveSkill newSkill)
    {
        if (playerSkill != null)
        {
            playerSkill.SetOrUpgradeActiveSkill(newSkill);
        }
    }

    /// <summary>
    /// �нú� ��ų �������� �÷��̾ �����մϴ�.
    /// ���� �̹� �ش� �������� ����Ǿ� �ִٸ�, �̹� ����� ��ų�� UpgradeSkill()�� ȣ���Ͽ� ������ �ø��ϴ�.
    /// </summary>
    /// <param name="player">�нú� ��ų�� ������ �÷��̾� GameObject</param>
    /// <param name="passiveSkillPrefab">�нú� ��ų ������ (�ش� ��ų�� GameObject)</param>
    public void ApplyPassiveSkill(GameObject player, GameObject passiveSkillPrefab)
    {
        if (savedPassiveSkillPrefabs.Contains(passiveSkillPrefab))
        {
            // �̹� �ش� �������� ����Ǿ� �ִٸ�, player���� �ش� ��ų ������Ʈ�� ã�� ���׷��̵��մϴ�.
            PassiveSkill existingSkill = player.GetComponent(passiveSkillPrefab.GetComponent<PassiveSkill>().GetType()) as PassiveSkill;
            if (existingSkill != null)
            {
                existingSkill.UpgradeSkill();
                Debug.Log("�нú� ��ų ���׷��̵�: " + existingSkill.skillName + " Level: " + existingSkill.level);
            }
            else
            {
                Debug.LogError("�̹� ����� �нú� ��ų ������Ʈ�� ã�� �� �����ϴ�.");
            }
        }
        else
        {
            // ���ο� �нú� ��ų�̸�, �������� �ν��Ͻ�ȭ�Ͽ� player�� �߰��ϰ� �����մϴ�.
            GameObject instance = Instantiate(passiveSkillPrefab, player.transform);
            PassiveSkill ps = instance.GetComponent<PassiveSkill>();
            if (ps != null)
            {
                Debug.Log("�нú� ��ų �߰�: " + ps.skillName);
                savedPassiveSkillPrefabs.Add(passiveSkillPrefab);
            }
            else
            {
                Debug.LogError("�ν��Ͻ�ȭ�� ��ü�� PassiveSkill ������Ʈ�� �����ϴ�.");
            }
        }
    }

    /// <summary>
    /// �� ��ȯ ��, �� �÷��̾� GameObject�� ����� �нú� ��ų �����յ��� �������մϴ�.
    /// </summary>
    /// <param name="newPlayer">���ο� �÷��̾� GameObject</param>
    public void ReapplyPassiveSkills(GameObject newPlayer)
    {
        foreach (GameObject prefab in savedPassiveSkillPrefabs)
        {
            Instantiate(prefab, newPlayer.transform);
        }
    }

    /// <summary>
    /// ����Ʈ�� ��ҵ��� �������� ���� ��ƿ��Ƽ �޼��� (Fisher-Yates �˰���)
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
    /// �ʿ��� �� SkillManager�� �ڽ� ������Ʈ(��: ���� ��ų �ɼ�)�� �����մϴ�.
    /// </summary>
    public void ClearSkillList()
    {
        foreach (Transform skill in transform)
        {
            Destroy(skill.gameObject);
        }
    }
}
