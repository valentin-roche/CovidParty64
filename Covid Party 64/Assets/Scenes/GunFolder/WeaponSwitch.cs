﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour 
{
    
    public int totalWeapons = 1;
    public int currentWeaponIndex;

    public GameObject[] guns;
    public GameObject weaponHolder;
    public GameObject currentWeapon;

    // Start is called before the first frame update
    void Start()
    {
        totalWeapons = weaponHolder.transform.childCount;
        guns = new GameObject[totalWeapons];

        for(int i = 0; i < totalWeapons; i++)
        {
            guns[i] = weaponHolder.transform.GetChild(i).gameObject;
            guns[i].SetActive(false);
        }

        guns[0].SetActive(true);
        currentWeapon = guns[0];
        currentWeaponIndex= 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(currentWeaponIndex < totalWeapons - 1)
            {
                guns[currentWeaponIndex].SetActive(false);
                currentWeaponIndex += 1;
                guns[currentWeaponIndex].SetActive(true);
            }
            else if(currentWeaponIndex == totalWeapons - 1)
            {
                guns[currentWeaponIndex].SetActive(false);
                currentWeaponIndex = 0;
                guns[currentWeaponIndex].SetActive(true);
            }
        }
    }
}