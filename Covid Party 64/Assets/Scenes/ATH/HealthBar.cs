
using Stats;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public Gradient gradient;
    public Image fill;

    public static HealthBar instance;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Une instance de HealthBar existe déjà dans la scène.");
            return;
        }
        instance = this;
    }

    //initialisaiton de la barre de vie
    public void SetContaminationInit(int _contamination)
    {
        slider.maxValue = PlayerStat.MaxContamination; //initialisation de la valeur max du slider
        slider.value = _contamination;                 //mise à jour de la vie 

        fill.color = gradient.Evaluate(1f);            //associe la couleur en fonction du taux de contamination 
    }

    //mise à jour de la barre de vie
    public void SetContamination(int _contamination)
    {
        slider.value = _contamination;

        fill.color = gradient.Evaluate(slider.normalizedValue); //associe la couleur en fonction du taux de contamination 
    }
}
