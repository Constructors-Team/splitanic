using System;
using UnityEngine;

public class BoatSoundManager : MonoBehaviour
{
     [SerializeField] private AudioClip forwardMotorSound;
     [SerializeField] private AudioClip backwardMotorSound;
     [SerializeField] private AudioClip wallCollisionSound;
     
     private AudioSource audioSource;

     private void Awake()
     {
          audioSource = GetComponent<AudioSource>();
          if (audioSource == null)
          {
               Debug.LogError("[+] AudioSource component missing from boat!");
          }
     }

     public void PlayLoopingSound(bool movingForward, bool movingBackward)
     {
          if (audioSource == null) return;
          Debug.Log("[+] BoatSoundManager.PlayLoopingSound() called!");

          if (movingForward && forwardMotorSound != null)
          {
               Debug.Log("[+] Boat is moving forward!");
               if (audioSource.clip != forwardMotorSound) // Avoid restarting the sound
               {
                    Debug.Log("[+] Audio set to forwardSound!");
                    audioSource.clip = forwardMotorSound;
                    audioSource.loop = true;
                    audioSource.volume = 0.7f;
                    audioSource.Play();
               }
          }
          else if (movingBackward && backwardMotorSound != null)
          {
               Debug.Log("[+] Boat is moving backward!");
               if (audioSource.clip != backwardMotorSound)
               {
                    Debug.Log("[+] Audio set to backwardSound!!");
                    audioSource.clip = backwardMotorSound;
                    audioSource.loop = true;
                    audioSource.volume = 0.7f;
                    audioSource.Play();
               }
          }
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
