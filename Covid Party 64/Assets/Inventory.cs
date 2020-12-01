using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int maskCount;
    public Text maskCountText;
    public int radioCount;
    public Text radioCountText;
    public int bottleCount = 0;
    public Text bottleCountText;

    public static Inventory instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de Inventory dans la scène");
            return;
        }
        instance = this;
    }

    public void addObjet(GameObject objet)
    {
        switch (objet.tag)
        {
            case "Mask":
                maskCount++;
                maskCountText.text = maskCount.ToString();
                break;
            case "Radio":
                radioCount++;
                radioCountText.text = radioCount.ToString();
                break;
            case "BottleGel":
                bottleCount++;
                bottleCountText.text = bottleCount.ToString();
                break;
        }
    }

}
