using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] AudioClip goldPickupSound;
    AudioSource audioSource;
    public GameObject endScreen;
    public GameObject shopTooltip;
    public GameObject gameMenuUI;
    public GameObject shopMenuUI;
    public int nbGold;
    public int goldNeeded = 1000;
    private TextMeshProUGUI endText;
    private ThirdPersonCharacter thirdPersonCharacter;
    private float actualTime;
    private bool isNearShop = false;
    public bool hasBoots = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        endText = endScreen.transform.Find("EndText").GetComponent<TextMeshProUGUI>();
        actualTime += Time.deltaTime;
        timeText.text = "Time : " + actualTime.ToString("F2");
        nbGold = 0;
        goldText.text = "Gold : " + nbGold + "/" + goldNeeded;
        thirdPersonCharacter = this.GetComponent<ThirdPersonCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hasBoots){
            thirdPersonCharacter.m_MoveSpeedMultiplier = 1.5f;
        }

        actualTime += Time.deltaTime;
        timeText.text = "Time : " + actualTime.ToString("F2");
        if(Input.GetKeyDown(KeyCode.E) && isNearShop && !gameMenuUI.activeSelf){
            shopMenuUI.SetActive(!shopMenuUI.activeSelf);
        }
        
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(shopMenuUI.activeSelf){
                shopMenuUI.SetActive(false);
            } else { 
                gameMenuUI.SetActive(!gameMenuUI.activeSelf);
            }
        }

        if(nbGold >= goldNeeded){
            Win();
            return;
        }
        
        goldText.text = "Gold : " + nbGold + "/"+goldNeeded;
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.GetComponent<GoldPurseScript>() as GoldPurseScript != null){
            GoldPurseScript goldPurseItem = other.gameObject.GetComponent<GoldPurseScript>();
            nbGold += goldPurseItem.goldValue;
            PickUp();
            Object.Destroy(other.gameObject);
        } else if(other.gameObject.tag == "ShopKeeper"){
            shopTooltip.SetActive(true);
            isNearShop = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "ShopKeeper"){
            isNearShop = false;
            shopTooltip.SetActive(false);
            if(shopMenuUI.activeSelf){
                shopMenuUI.SetActive(false);
            }
        }
    }

    public void Win(){
        goldText.text = "Gold : " + goldNeeded + "/" + goldNeeded;
        Time.timeScale = 0;
        endText.text = "YOU WIN !";
        endScreen.SetActive(true);
    }

    public void Loose(){
        Time.timeScale = 0;
        endText.text = "YOU LOOSE !";
        endScreen.SetActive(true);
    }
    
    private void PickUp(){
        audioSource.clip = goldPickupSound;
        audioSource.Play();
    }
}
