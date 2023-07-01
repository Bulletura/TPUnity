using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI timeText;
    public GameObject gameMenuUI;
    private int nbGold;
    private float actualTime;
    private bool isNearVault = false;
    // Start is called before the first frame update
    void Start()
    {
        actualTime += Time.deltaTime;
        timeText.text = "Time : " + actualTime.ToString("F2");
        nbGold = 0;
        goldText.text = "Gold : " + nbGold;
    }

    // Update is called once per frame
    void Update()
    {
        actualTime += Time.deltaTime;
        timeText.text = "Time : " + actualTime.ToString("F2");
        if(Input.GetKeyDown(KeyCode.E) && isNearVault){
            print("OPEN MENU");
        }
        
        if(Input.GetKeyDown(KeyCode.Escape)){
            gameMenuUI.SetActive(!gameMenuUI.activeSelf);
        }
        
        goldText.text = "Gold : " + nbGold;
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "GoldPurse"){
            Object.Destroy(other.gameObject);
            nbGold += 100;
        } else if(other.gameObject.tag == "SmallGoldPurse"){
            Object.Destroy(other.gameObject);
            nbGold += 20;
        } else if(other.gameObject.tag == "BankVault"){
            isNearVault = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "BankVault"){
            isNearVault = false;
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Ennemy"){
            nbGold -= 100;
            if(nbGold <= 0 ){
                nbGold = 0;
            }
        }
    }
}
