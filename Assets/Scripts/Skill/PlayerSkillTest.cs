//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerSkill : MonoBehaviour
//{
//    public ActiveSkill activeSkill;   // ���� ��ϵ� ��Ƽ�� ��ų
//    private float lastSkillTime = -Mathf.Infinity;  // ������ ��ų ��� �ð�

//    void Start()
//    {
//        // �׽�Ʈ�� �ʱ� ��ų ����
//        // ����: AttackIncreaseSkill�� �ʱ� ��ų�� ����
//        AttackIncreaseSkill initialSkill = gameObject.AddComponent<AttackIncreaseSkill>();
//        if (initialSkill == null)
//        {
//            Debug.LogError("AttackIncreaseSkill ������Ʈ�� �߰����� �ʾҽ��ϴ�.");
//            return;
//        }
//        initialSkill.baseAttackMultiplier = 2f;
//        initialSkill.coolDown = 60f;
//        initialSkill.duration = 10f;
//        activeSkill = initialSkill;
//        Debug.Log("�ʱ� ��Ƽ�� ��ų ����: " + activeSkill.skillName);
//    }

//    void Update()
//    {
//        // Z Ű�� ������ activeSkill�� ������ ��� Ȱ��ȭ (��ٿ� üũ ����)
//        if (Input.GetKeyDown(KeyCode.Z) && activeSkill != null && Time.time >= lastSkillTime + activeSkill.coolDown)
//        {
//            activeSkill.Activate(gameObject);
//            lastSkillTime = Time.time;
//        }
//    }

//    /// <summary>
//    /// �ܺ�(��: ���� �ý���)���� ���ο� ��Ƽ�� ��ų�� �����ϰų� ���׷��̵��� �� ȣ���մϴ�.
//    /// </summary>
//    public void SetOrUpgradeActiveSkill(ActiveSkill newSkill)
//    {
//        // ���� Ÿ���� ��ų�̸� ���׷��̵�
//        if (activeSkill != null && activeSkill.GetType() == newSkill.GetType())
//        {
//            activeSkill.UpgradeSkill();
//            Debug.Log("��Ƽ�� ��ų ���׷��̵�: " + activeSkill.skillName + " ���� " + activeSkill.level);
//        }
//        else
//        {
//            // �ٸ� Ÿ���̸� ���� ��ų ���� �� ���ο� ��ų�� activeSkill�� �Ҵ�
//            if (activeSkill != null)
//            {
//                Destroy(activeSkill);
//            }
//            // newSkill�� �̹� �÷��̾ �߰��� ������Ʈ��� �����մϴ�.
//            activeSkill = newSkill;
//            Debug.Log("�� ��Ƽ�� ��ų ����: " + activeSkill.skillName);
//        }
//    }

//    public void ActivateActiveSkill()
//    {
//        if (activeSkill == null)
//        {
//            return;
//        }
//        if (Time.time >= lastSkillTime + activeSkill.coolDown)
//        {
//            activeSkill.Activate(gameObject);
//            lastSkillTime = Time.time;
//        }
//        else
//        {
//            Debug.Log("��ų ��Ÿ���Դϴ�.");
//        }
//    }
//}
