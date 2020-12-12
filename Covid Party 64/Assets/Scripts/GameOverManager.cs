using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject respawnUI;

    public static GameOverManager instance;

     private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de GameOverManager dans la scène.");
            return;
        }

        instance = this;
    }

    //affiche le menu de game over
    public void OnPlayerDeath()
    {
        gameOverUI.SetActive(true);
        //Time.timeScale = 0f;
    }

    //affiche le menu de respawn (si le joueur possède un masque)
    public void OnPlayerRespawnActive()
    {
        respawnUI.SetActive(true);
    }

    //desactive le menu de respawn
    public void OnPlayerRespawnNoActive()
    {
        respawnUI.SetActive(false);
    }

    
}
