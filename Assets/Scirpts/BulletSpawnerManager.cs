using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnerManager : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(nameof(BulletSpawn));
    }

    IEnumerator BulletSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            ObjectPool.Instance.SpawnFromPool("Bullet", transform.position, Quaternion.identity);
        }
    }
}
