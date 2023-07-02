using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    public void BuyItem(string itemName, int itemValue){
        if(itemName == "Boots"){
            if(playerController.nbGold > 250){
                playerController.nbGold -= 250;
            }
        }
    }

    public void Buy(string itemName){
        if(itemName == "Boots"){
            
        }
    }
    
    private void OnEnable() {
        Time.timeScale = 0;
    }

    private void OnDisable() {
        Time.timeScale = 1;
    }
}
