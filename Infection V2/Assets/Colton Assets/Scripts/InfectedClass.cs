using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class InfectedClass : MonoBehaviour {
 	public int health;
    public float speed;
    public float range;
    public float spawnChance;
    private Transform targetHuman;
    NavMeshAgent infectedAgent;
    public string humanTag = "Human";
    public string vaccineTag = "Vaccine";

    void Start ()
    {
        infectedAgent = GetComponent<NavMeshAgent>();
        infectedAgent.autoBraking = false;
        infectedAgent.speed = speed;
 	}

    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
 
    void Update()
    {
        UpdateTarget();
    }
 
    void UpdateTarget()
    {
        GameObject[] Humans = GameObject.FindGameObjectsWithTag(humanTag);
        GameObject[] Vaccines = GameObject.FindGameObjectsWithTag(vaccineTag);
        GameObject[] HV = Humans.Concat(Vaccines).ToArray();

        float shortestDistance = Mathf.Infinity;
        GameObject nearestHuman = null;
        Vector3 currentPosition = this.transform.position;

        foreach (GameObject huvacs in HV)
        {
            //Vector3 directionToTarget = huvacs.transform.position - currentPosition;
            float distanceToTarget = Vector3.Distance(transform.position, huvacs.transform.position);
            if (distanceToTarget < shortestDistance)
            {
                shortestDistance = distanceToTarget;
                nearestHuman = huvacs;
            }
            else
            {
                targetHuman = null;
            }
        }

        if (nearestHuman != null && shortestDistance <= range)
        {
            targetHuman = nearestHuman.transform;
            infectedAgent.SetDestination(targetHuman.transform.position);
        }
    }
    public void damageTaken (int damage)
    {
        Debug.Log(health);
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }
     
    void Die()
    {
        Destroy(gameObject);
    }
 }