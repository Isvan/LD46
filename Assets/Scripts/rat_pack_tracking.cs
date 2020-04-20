using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class rat_pack_tracking : MonoBehaviour
{
    public GameObject bloodSplat;
    public Transform BossRat;
    public NavMeshAgent agent;
    public float bloodSplatLifetime;
    public int MoveSpeed = 4;
    public int Range = 10;
    public Vector3 rotationOffset;
    public Vector3 wanderStepRange;

    float distCheese = Mathf.Infinity;
    string cheeseTag = "Cheese";
    Transform Cheese;
    Transform target;

    public float randTargetAcquireDistance = 5;
    Vector3 randTarget;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("UpdateCheeseTarget", 0f, 0.5f);
        randTarget = transform.position + new Vector3(Random.Range(-wanderStepRange.x, wanderStepRange.x), Random.Range(-wanderStepRange.y, wanderStepRange.y), Random.Range(-wanderStepRange.z, wanderStepRange.z));

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
       
        if(distCheese < Range)
        {
            if (Cheese != null) {
                target = Cheese;
            } 
        }

        if (Vector3.Distance(transform.position, target.position) <= Range)
        {
            transform.LookAt(target);
            agent.SetDestination(target.position);

        }
        else
        {
            target = null;

            if(Vector3.Distance(transform.position,randTarget) <= randTargetAcquireDistance || !agent.hasPath)
            {

                randTarget = transform.position + new Vector3(Random.Range(-wanderStepRange.x, wanderStepRange.x), Random.Range(-wanderStepRange.y, wanderStepRange.y), Random.Range(-wanderStepRange.z, wanderStepRange.z));
               
            }

            transform.LookAt(randTarget);
            agent.SetDestination(randTarget);
            //target.position = randTarget;
        }

       
        transform.Rotate(rotationOffset);
        

    }
    private void OnDestroy()
    {
        Debug.Log("rat died");
        BloodSplatter(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Range);
        if(target != null)
        {
            Gizmos.DrawCube(target.transform.position, new Vector3(3.0f, 0.5f, 1.0f));
            Gizmos.DrawCube(randTarget, new Vector3(0.5f, 0.5f, 1.0f));

        }
    }

    public bool InBossRange()
    {
        return (Vector3.Distance(transform.position, BossRat.position) <= Range);
    }

    private void BloodSplatter(GameObject rat)
    {
        Vector3 ratPos = rat.transform.position;
        Quaternion ratRot = rat.transform.rotation;

        GameObject blood = Instantiate(bloodSplat, ratPos, ratRot);
        Destroy(blood, bloodSplatLifetime);
    }
}
