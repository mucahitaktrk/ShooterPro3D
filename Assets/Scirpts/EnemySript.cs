using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySript : MonoBehaviour
{
    private Animator enemyAnimator;
    private NavMeshAgent _enemyNavMeshAgent;
    private GameObject _playerObject;
    private void Awake()
    {
        enemyAnimator = GetComponent<Animator>();
        _playerObject = GameObject.FindGameObjectWithTag("Player");
        _enemyNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _enemyNavMeshAgent.destination = _playerObject.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            enemyAnimator.SetBool("Dead", true);
            _enemyNavMeshAgent.speed = 0.0f;
            gameObject.tag = "Untagged";
            Destroy(gameObject, 4f);
        }
    }
}
