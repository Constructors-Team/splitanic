using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float frameDuration = 0.1f;

    [SerializeField]
    private int score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
        StartCoroutine(CheckTitanicStillAlive());
    }

    IEnumerator CheckTitanicStillAlive()
    {
        while (true) {
    
            GameObject titanicRef = GameObject.Find("Titanic");

            if (titanicRef != null) {
                ++score;
                Debug.Log("Titanic still alive");
            } else {
                Debug.Log("score : " + score);
                Debug.Log("Titanic is dead");
            }

            yield return new WaitForSeconds(frameDuration);
        }
    }
}
