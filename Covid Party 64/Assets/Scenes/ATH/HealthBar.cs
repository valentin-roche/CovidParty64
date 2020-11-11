
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

    public void SetContaminationInit(int _contamination)
    {
        slider.maxValue = PlayerStat.MaxContamination;
        slider.value = _contamination;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetContamination(int _contamination)
    {
        slider.value = _contamination;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
