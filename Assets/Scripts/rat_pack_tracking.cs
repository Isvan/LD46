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
    public int Range = 10;

    public Vector3 rotationOffset;

    float distCheese = Mathf.Infinity;
    string cheeseTag = "Cheese";
    Transform Cheese;
    Transform target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("UpdateCheeseTarget", 0f, 0.5f);
    }
     
    void UpdateCheeseTarget()
    {
        GameObject[] cheeses = GameObject.FindGameObjectsWithTag(cheeseTag);
        float shortestDist = Mathf.Infinity;
        GameObject nearestCheese = null;

        foreach(GameObject cheese in cheeses)
        {
            float dist = Vector3.Distance(transform.position, cheese.transform.position);
            if(dist < shortestDist)
            {
                shortestDist = dist;
                nearestCheese = cheese;
            }
        }

        if(shortestDist <= Range)
        {
            Cheese = nearestCheese.transform;
        }
        distCheese = shortestDist;
    }

    void Update()
    {
        target = BossRat;
        float distBossRat = Vector3.Distance(transform.position, BossRat.position);

        if(distCheese < Range)
        {
            if (Cheese != null) {
                target = Cheese;
            } 
        }

        if (Vector3.Distance(transform.position, target.position) <= Range)
        {
            agent.SetDestination(target.position);
            transform.LookAt(target);
            transform.Rotate(rotationOffset);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Range);
        if(target != null)
        {
            Gizmos.DrawCube(target.transform.position, new Vector3(3.0f, 0.5f, 1.0f));

        }
    }
}
