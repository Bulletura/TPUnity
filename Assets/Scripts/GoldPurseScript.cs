using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPurseScript : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip goldPickupSound;
    public int goldValue;
    void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    public void PickUp(){
        audioSource.clip = goldPickupSound;
        audioSource.Play();
        Object.Destroy(this.gameObject);
    }
}
