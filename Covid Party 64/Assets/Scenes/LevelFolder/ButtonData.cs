using UnityEngine;
using UnityEngine.UI;

public class ButtonData : MonoBehaviour
{
    private Couple linkedCouple;

    public Couple LinkedCouple { get => linkedCouple; set => linkedCouple = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetComponentInChildren<Text>().text = LinkedCouple.DisplayName;
    }
}
