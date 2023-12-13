using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Costume : MonoBehaviour
{
    Player_Scritable playerData;

    void Start()
    {
        playerData = GetComponent<Player_Scritable>();
        switch (playerData.weapon.name)
        {
            case "Weapon0":
                break;
            case "Weapon1":
                this.GetComponent<GearEquipper>().Melee = 50;
                this.GetComponent<GearEquipper>().Armor = 14;
                this.GetComponent<GearEquipper>().Helmet = 14;
                this.GetComponent<GearEquipper>().Shoulder = 14;
                this.GetComponent<GearEquipper>().Arm = 14;
                this.GetComponent<GearEquipper>().Feet = 14;
                this.GetComponent<GearEquipper>().ApplySkinChanges();
                break;
            case "Weapon2":
                this.GetComponent<GearEquipper>().Melee = 5;
                this.GetComponent<GearEquipper>().Armor = 5;
                this.GetComponent<GearEquipper>().Helmet = 5;
                this.GetComponent<GearEquipper>().Shoulder = 5;
                this.GetComponent<GearEquipper>().Arm = 5;
                this.GetComponent<GearEquipper>().Feet = 5;
                this.GetComponent<GearEquipper>().ApplySkinChanges();
                break;
            case "Weapon3":
                this.GetComponent<GearEquipper>().Melee = 49;
                this.GetComponent<GearEquipper>().Armor = 29;
                this.GetComponent<GearEquipper>().Helmet = 29;
                this.GetComponent<GearEquipper>().Shoulder = 29;
                this.GetComponent<GearEquipper>().Arm = 29;
                this.GetComponent<GearEquipper>().Feet = 29;
                this.GetComponent<GearEquipper>().ApplySkinChanges();
                break;
        }
    }

}