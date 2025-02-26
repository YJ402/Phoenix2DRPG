using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public ActiveSkill activeSkill;   // 현재 등록된 액티브 스킬
    private float lastSkillTime = -Mathf.Infinity;  // 마지막 스킬 사용 시간

    void Start()
    {
        // 테스트용 초기 스킬 설정
        // 예시: AttackIncreaseSkill을 초기 스킬로 지정
        BulletDoubleSkill initialSkill = gameObject.AddComponent<BulletDoubleSkill>();
        if (initialSkill == null)
        {
            return;
        }
        initialSkill.bulletMultiplier = 2f;
        initialSkill.coolDown = 60f;
        initialSkill.duration = 10f;
        activeSkill = initialSkill;
    }

    void Update()
    {
        // Z 키를 누르면 activeSkill이 존재할 경우 활성화 (쿨다운 체크 포함)
        if (Input.GetKeyDown(KeyCode.Z) && activeSkill != null && Time.time >= lastSkillTime + activeSkill.coolDown)
        {
            activeSkill.Activate(gameObject);
            lastSkillTime = Time.time;
        }
    }

    /// <summary>
    /// 외부(예: 보상 시스템)에서 새로운 액티브 스킬을 설정하거나 업그레이드할 때 호출합니다.
    /// </summary>
    public void SetOrUpgradeActiveSkill(ActiveSkill newSkill)
    {
        // 같은 타입의 스킬이면 업그레이드
        if (activeSkill != null && activeSkill.GetType() == newSkill.GetType())
        {
            activeSkill.UpgradeSkill();
            Debug.Log("액티브 스킬 업그레이드: " + activeSkill.skillName + " 레벨 " + activeSkill.level);
        }
        else
        {
            // 다른 타입이면 기존 스킬 제거 후 새로운 스킬을 activeSkill에 할당
            if (activeSkill != null)
            {
                Destroy(activeSkill);
            }
            // newSkill는 이미 플레이어에 추가된 컴포넌트라고 가정합니다.
            activeSkill = newSkill;
            Debug.Log("새 액티브 스킬 설정: " + activeSkill.skillName);
        }
    }

    public void ActivateActiveSkill()
    {
        if (activeSkill == null)
        {
            return;
        }
        if (Time.time >= lastSkillTime + activeSkill.coolDown)
        {
            activeSkill.Activate(gameObject);
            lastSkillTime = Time.time;
        }
        else
        {
            Debug.Log("스킬 쿨타임입니다.");
        }
    }
}
