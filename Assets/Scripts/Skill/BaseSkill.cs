using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{
    public string skillName;    // 스킬 이름
    public float coolDown = 60;      // 쿨다운 시간
    public float duration = 10;      // 스킬 효과 지속 시간
    public int level = 1;       // 스킬 레벨 (기본 1)

    // 스킬 발동 시 호출되는 함수 (플레이어 오브젝트를 인자로 받음)
    public abstract void Activate(GameObject user);

    // 스킬 업그레이드 (레벨 상승)
    public virtual void UpgradeSkill()
    {
        level++;
    }
}
