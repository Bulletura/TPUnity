using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BootsItem : Items
{
    [SerializeField] GameObject itemImageComponent;
    [SerializeField] Sprite itemImage;
    private Image image;
    private bool isPurchased = false;
    void Start(){
        image = itemImageComponent.GetComponent<Image>();
        image.sprite = Resources.Load<Sprite>("Images/"+itemName);
    }
    public override void BuyItem()
    {
        if(this.playerController.nbGold > this.itemValue && !isPurchased){
            this.playerController.nbGold -= this.itemValue;
            image.sprite = Resources.Load<Sprite>("Images/Checked"+itemName);
            isPurchased = true;
            this.playerController.hasBoots = true;
        }
    }

}
