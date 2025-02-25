using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public ActiveSkill activeSkill;

    private float lastSkillTime = -Mathf.Infinity;

    void Update()
    {
        // Z Ű�� ������, ��ٿ��� �������� ��ų Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.Z) && Time.time >= lastSkillTime + ((ActiveSkill)activeSkill).coolDown)
        {
            activeSkill.Activate(gameObject);
            lastSkillTime = Time.time;
        }
    }
}
