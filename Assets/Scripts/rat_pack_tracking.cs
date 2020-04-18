using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class rat_pack_tracking : MonoBehaviour
{

    public Transform BossRat;
    public NavMeshAgent agent;
    public int MoveSpeed = 4;
    public int MaxDist = 10;
    public int MinDist = 5;

    void Start()
    {   //might need to use a player manager but we dont have that yet so hacky hack
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        transform.LookAt(BossRat);

        if (Vector3.Distance(transform.position, BossRat.position) >= MinDist)
        {

            agent.SetDestination(BossRat.position);


            if (Vector3.Distance(transform.position, BossRat.position) <= MaxDist)
            {
                //Here Call any function U want Like Shoot at here or something
            }

        }
    }
}
