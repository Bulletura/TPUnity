using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    public Transform target;
    public Transform bankVault;

    private bool hasGold = false;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.SetDestination(target.position);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(hasGold){
            agent.SetDestination(bankVault.position);
        } else{
            agent.SetDestination(target.position);
        }
        // Check if we've reached the destination
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    anim.SetBool("isIdle", true);
                }
            } else if(anim.GetBool("isIdle") == true){
                anim.SetBool("isIdle", false);
            }
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player"){
            hasGold = true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        
        print("VAULT");
        if(other.gameObject.tag == "BankVault"){
            hasGold = false;
        }
    }
}
