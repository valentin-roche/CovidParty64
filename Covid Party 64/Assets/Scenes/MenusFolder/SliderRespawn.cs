using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class SliderRespawn : MonoBehaviour
{
    public Slider slider;
    private float TimeRemaining;
    private const float TimerMax = 5f;

    

    private void Start()
    {
        TimeRemaining = TimerMax;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = CalculateSliderValue();

        if (TimeRemaining <= 0)
        {
            TimeRemaining = 0;
            PlayerStatsHandler.instance.Kill();
        }

        else if(TimeRemaining > 0)
        {
            TimeRemaining -= Time.deltaTime;
        } 
    }

    private float CalculateSliderValue()
    {
        return (TimeRemaining/TimerMax);
    }

    public void No()
    {
        PlayerStatsHandler.instance.Kill();
    }

    public void Yes()
    {
        PlayerStatsHandler.Respawn();
    }
}
