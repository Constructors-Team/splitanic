using System;
using UnityEngine;

public class BoatSoundManager : MonoBehaviour
{
     [SerializeField] private AudioClip wallCollisionSound;
     private AudioSource audioSource;

     private void Awake()
     {
          audioSource = GetComponent<AudioSource>();
     }

     private void OnCollisionEnter2D(Collision2D collision)
     {
          if (collision.gameObject.CompareTag("Walls"))
          {
               Debug.Log("[+] Boat hit a wall!");

               if (audioSource != null && wallCollisionSound != null)
               {
                    audioSource.PlayOneShot(wallCollisionSound);
               }
          }
     }
}
