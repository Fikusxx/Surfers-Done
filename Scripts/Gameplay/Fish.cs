using System;
using UnityEngine;


public class Fish : MonoBehaviour
{

    private Animator anim;
    [SerializeField] private AudioClip pickupSound;


    private void Awake()
    {
        anim = GetComponentInParent<Animator>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PickUpFish();
        }
    }

    private void PickUpFish()
    {
        anim.SetTrigger("PickUp");

        // Increment the fish count
        GameStats.Instance.CollectFish();

        // Increment the score


        // Play SFX
        AudioManager.Instance.PlaySFX(pickupSound);

        // Trigger animation

    }

    public void OnShowChunk()
    {
        anim.SetTrigger("Idle");
    }
}
