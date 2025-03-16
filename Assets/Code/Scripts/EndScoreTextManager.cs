using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScoreTextManager : MonoBehaviour
{
    public TextMeshProUGUI endScoreText;
    public Image endScorebackground;
    public GameObject endScoreButton;
    
    private static EndScoreTextManager instance;

    private void Awake()
    {
        instance = this;
        endScoreButton.SetActive(false);
        endScoreText.gameObject.SetActive(false);
        endScorebackground.gameObject.SetActive(false);
    }

    public static void DisplayEndScoreText()
    {
        // Set end score text
        int endsScore = GameManager.Instance.getScore();
        instance.endScoreText.text = "You failed to protect the Titanic! Your score is " + endsScore;
        
        // Display end score text
        instance.endScoreButton.gameObject.SetActive(true);
        instance.endScoreText.gameObject.SetActive(true);
        instance.endScorebackground.gameObject.SetActive(true);
    }
}
