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
        // ���� ������� ������ ���
        GameObject originalPrefab = currentBulletPrefab;
        // ��ź ȭ�� ���������� ��ü
        currentBulletPrefab = BombPrefab;

        // ��ų ȿ�� ���� �ð� ���� ���
        yield return new WaitForSeconds(duration);

        // ���� �ð� ���� �� ������ ȭ�� ���������� ����
        currentBulletPrefab = originalPrefab;
    }
}
