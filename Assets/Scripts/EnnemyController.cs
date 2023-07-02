using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class EnnemyController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ennemyGoldText;
    private NavMeshAgent agent;
    private Animator anim;
    public PlayerController player;
    public Transform target;
    public Transform bankVault;

    public int stolenGoldValue = 100;

    private bool hasGold = false;
    private int storedGold;
    private int carriedGold;
    // Start is called before the first frame update
    void Start()
    {
        storedGold = 0;
        carriedGold = 0;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.SetDestination(target.position);
        ennemyGoldText.text = "Ennemy Gold : 0/" + player.goldNeeded;
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
            if(player.nbGold < stolenGoldValue){
                this.carriedGold = player.nbGold;
                player.nbGold = 0;
            } else {
                this.carriedGold = stolenGoldValue;
                player.nbGold -= stolenGoldValue;
            }
            hasGold = true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        
        if(other.gameObject.tag == "BankVault"){
            if((this.storedGold + this.carriedGold) >= player.goldNeeded){
                
                ennemyGoldText.text = "Ennemy Gold : " + player.goldNeeded + "/" + player.goldNeeded;
                player.Loose();
                return;
            }
            this.storedGold += this.carriedGold;
            this.carriedGold = 0;
            ennemyGoldText.text = "Ennemy Gold : " + this.storedGold + "/" + player.goldNeeded;
            hasGold = false;
        }
        if(other.gameObject.GetComponent<GoldPurseScript>() as GoldPurseScript != null){
            Object.Destroy(other.gameObject);
            this.carriedGold += other.gameObject.GetComponent<GoldPurseScript>().goldValue;
            hasGold = true;
        }
    }
}
