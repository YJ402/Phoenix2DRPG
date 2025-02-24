using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public GameObject ArrowPrefab;

    public GameObject BombPrefab;

    public GameObject currentBulletPrefab;

    private void Awake()
    {
        currentBulletPrefab = ArrowPrefab;
    }

    public void Fire()
    {
        Instantiate(currentBulletPrefab, transform.position, transform.rotation);
    }

    public IEnumerator SwitchToBombArrow(float duration)
    {
        // 현재 사용중인 프리팹 백업
        GameObject originalPrefab = currentBulletPrefab;
        // 폭탄 화살 프리팹으로 교체
        currentBulletPrefab = BombPrefab;

        // 스킬 효과 지속 시간 동안 대기
        yield return new WaitForSeconds(duration);

        // 지속 시간 종료 후 원래의 화살 프리팹으로 복원
        currentBulletPrefab = originalPrefab;
    }
}
