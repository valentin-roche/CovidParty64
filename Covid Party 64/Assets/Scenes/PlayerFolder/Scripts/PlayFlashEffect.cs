using UnityEngine;
using UnityEngine.UI;

public class PlayFlashEffect : MonoBehaviour
{
    //Declaration of objects
    public GameObject flashContainer;
    public Image imageFade;
    //Instance
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
        imageFade.canvasRenderer.SetAlpha(1f);                                      //Make Flash visible
    }

    void DesactiveFlashImage()
    {
        flashContainer.SetActive(false);                                            //Make Flash invisible
    }
    //Make the Flash image twinkle
    public void FlashEffect()
    {
        //Make Flash visible
        flashContainer.SetActive(true);
        //Fade out the Flash
        Invoke("FirstFadeOut", 0);
        //Fade in the Flash after .3 seconds
        Invoke("ResetImageAlpha", .3f);
        //Fade out the Flash after .3 seconds
        Invoke("LastFadeOut", .3f);
        //Make Flash invisible
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
