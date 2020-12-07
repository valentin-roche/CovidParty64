using UnityEngine;
using UnityEngine.UI;

public class PlayFlashEffect : MonoBehaviour
{
    public GameObject flashContainer;
    public Image imageFade;

    public static PlayFlashEffect instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("Il y a plus d'une instance de PlayEffectFlash dans la scene");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        imageFade.canvasRenderer.SetAlpha(1f);
    }

    void DesactiveFlashImage()
    {
        flashContainer.SetActive(false);
    }

    public void FlashEffect()
    {
        flashContainer.SetActive(true);
        Invoke("FirstFadeOut", 0);
        Invoke("ResetImageAlpha", .3f);
        Invoke("LastFadeOut", .3f);
        Invoke("DesactiveFlashImage", 1.8f);
    }

    void ResetImageAlpha()
    {
        imageFade.canvasRenderer.SetAlpha(1f);
    }

    void FirstFadeOut()
    {
        imageFade.CrossFadeAlpha(0, .5f, false);
        
    }

    void LastFadeOut()
    {
        imageFade.CrossFadeAlpha(0, 1.5f, false);
    }

}
