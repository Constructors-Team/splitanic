using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void PlayButton(){
        SceneManager.LoadScene("BoatScene");
        Debug.Log("Lance la scène du jeu");
    }
    public void CreditsButton(){
        SceneManager.LoadScene("Credits");
        Debug.Log("Affiche les crédits");
    }
    public void QuitButton(){
        Application.Quit();
        Debug.Log("Quitte l'application");
    }
    public void BackMainMenu(){
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Back to main menu");
    }
}
