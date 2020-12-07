using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour 
{
    
    private int totalWeapons = 1;
    private int currentWeaponIndex;

    private GameObject[] guns;
    public GameObject weaponHolder;
    private GameObject currentWeapon;

    public static WeaponSwitch instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("Il y a plus d'une instance de WeaponSwitch dans la scene!");
            return;
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        totalWeapons = weaponHolder.transform.childCount;
        guns = new GameObject[totalWeapons];
        Debug.Log("Nombre d'armes détectées : "+totalWeapons);
        for(int i = 0; i < totalWeapons; i++)
        {
            guns[i] = weaponHolder.transform.GetChild(i).gameObject;
            guns[i].SetActive(false);
        }

       // currentWeaponIndex = Stats.PlayerStat.WeaponLevel-1;
        currentWeaponIndex = 1;

        guns[currentWeaponIndex].SetActive(true);
        currentWeapon = guns[currentWeaponIndex];

    }

    // Update is called once per frame
    void Update()
    {
        //Code de test pour switch weapon
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //        Stats.PlayerStat.WeaponLevel++;
            
            
        //}
        //Test if current weapon is up to date and if max weapon level is reached
        if (currentWeaponIndex != Stats.PlayerStat.WeaponLevel - 1 && (Stats.PlayerStat.WeaponLevel <= totalWeapons))
        {
            guns[currentWeaponIndex].SetActive(false);
            currentWeaponIndex = Stats.PlayerStat.WeaponLevel - 1;
            guns[currentWeaponIndex].SetActive(true);
        }
    }

    public int TotalWeapon { get => totalWeapons; set => totalWeapons = value; }
}
