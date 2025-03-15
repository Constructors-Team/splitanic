using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float frameDuration = 0.1f;

    private int score;

    public TextMeshProUGUI scoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
        StartCoroutine(CheckTitanicStillAlive());
    }

    IEnumerator CheckTitanicStillAlive()
    {
        while (true) {
    
            Titanic titanicRef = GameObject.FindFirstObjectByType<Titanic>();

            if (titanicRef != null) {
                ++score;
                Debug.Log("Titanic still alive");
            } else {
                Debug.Log("score : " + score);
                Debug.Log("Titanic is dead");
            }

            scoreText.text = $"Score : {score}";

            yield return new WaitForSeconds(frameDuration);
        }
    }
}
