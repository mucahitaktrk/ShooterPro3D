using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour,IBulletObject
{
    [SerializeField] private float _bulletSpeed = 0.0f;
    public void OnObjectSpawn()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * _bulletSpeed, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            CharacterManager.score++;
            collision.gameObject.layer = 0;
        }
    }
}
