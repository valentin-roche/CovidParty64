using UnityEngine;

public class AuraManager : MonoBehaviour
{
    private int totalAuras = 1;
    private int currentAuraIndex;

    private GameObject[] auras;
    public GameObject auraManager;
    private GameObject currentAura;

    public static AuraManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Il y a plus d'une instance de WeaponSwitch dans la scene!");
            return;
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        totalAuras = auraManager.transform.childCount;
        auras = new GameObject[totalAuras];

        for (int i = 0; i < totalAuras; i++)
        {
            auras[i] = auraManager.transform.GetChild(i).gameObject;
            auras[i].SetActive(false);
        }

        // currentAuraIndex = Stats.PlayerStat.DefenseLevel-1;
        currentAuraIndex = 0;

        auras[currentAuraIndex].SetActive(true);
        currentAura = auras[currentAuraIndex];

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    if (currentAuraIndex < totalAuras - 1)
        //    {
        //        auras[currentAuraIndex].SetActive(false);
        //        currentAuraIndex += 1;
        //        auras[currentAuraIndex].SetActive(true);
        //    }
        //    else if (currentAuraIndex == totalAuras - 1)
        //    {
        //        auras[currentAuraIndex].SetActive(false);
        //        currentAuraIndex = 0;
        //        auras[currentAuraIndex].SetActive(true);
        //    }
        //}
        //Test if current weapon is up to date and if max weapon level is reached
        if (currentAuraIndex != Stats.PlayerStat.DefenseLevel - 1 && (Stats.PlayerStat.DefenseLevel < totalAuras))
        {
            auras[currentAuraIndex].SetActive(false);
            currentAuraIndex = Stats.PlayerStat.DefenseLevel - 1;
            auras[currentAuraIndex].SetActive(true);
        }
    }

    public int TotalAura { get => totalAuras; set => totalAuras = value; }
}
