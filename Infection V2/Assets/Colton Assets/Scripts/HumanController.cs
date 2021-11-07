using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanController : MonoBehaviour {

    public float health = 3;
    public float speed = 10f;
    public float timer;
    public float InfectionDistanceRun = 10.0f;
    private NavMeshAgent humanAgent;
    public Rigidbody prefabInfection;

    void Start()
    {
        humanAgent = GetComponent<NavMeshAgent>();
        humanAgent.speed = speed;
    }

    void Update()
    {
        RunAway();
    }

    void RunAway()
    {
        GameObject[] Infections = GameObject.FindGameObjectsWithTag("Infection");
        foreach (GameObject infection in Infections)
        {
            float distance = Vector3.Distance(transform.position, infection.transform.position);
            if (distance < InfectionDistanceRun)
            {
                Vector3 DirToPlayer = transform.position - infection.transform.position;
                Vector3 newPos = transform.position + DirToPlayer;
                humanAgent.SetDestination(newPos);
            }
        }
    }

    void OnCollisionStay(Collision stayInfo)
    {
        if(stayInfo.collider.tag == "Infection") SufferInfection();
    }
    
    void SufferInfection()
    {
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            health -= 1f;
            timer = 0;
        }
        if (health <= 0) Die();
    }
    void Die()
    {
        if (health <= 0)
            {
                Destroy(gameObject);
                Instantiate(prefabInfection, transform.position, transform.rotation);
            }
    }
}