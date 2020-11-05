
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int minContamination = 0;
    public int currentContamination;

    public HealthBar healthBar;


    void Start()
    {
        currentContamination = minContamination;
        healthBar.SetMaxHealth(minContamination);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            GetContaminate(20);
        }
    }

    void GetContaminate(int _contamination)
    {
        currentContamination += _contamination;
        healthBar.SetHealth(currentContamination);
        if(currentContamination >= 100)
        {
            Die();
            return;
        }
    }

    void Die()
    {
        Debug.Log("Le joueur a été contaminé ! GAME OVER");
        //Bloquer mouvements du personnages
        PlayerMovement.instance.enabled = false;
        //Jouer animation de mort
        PlayerMovement.instance.animator.SetTrigger("Death");
        //Empêcher l'interaction avec le reste de la scène.

    }
}
