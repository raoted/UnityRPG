using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FootManFSM : EnemyFSM
{
    new enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return, 
        Damaged, 
        Die
    }
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
