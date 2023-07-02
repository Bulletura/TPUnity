using UnityEngine;

public abstract class Items : MonoBehaviour {
    public string itemName;
    public int itemValue;
    
    [SerializeField] public PlayerController playerController;
    
    public abstract void BuyItem();
}