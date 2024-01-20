using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Costume : MonoBehaviour
{
    Player_Scritable playerData;
    int atk;
    int def;
    void Start()
    {
        playerData = GetComponent<Player_Scritable>();
        if(playerData.weapon.name == "Weapon0")
        {
        }
        else if(playerData.weapon.name == "Weapon1")
        {
            this.GetComponent<GearEquipper>().Melee = 50;
            Change(14);
        }
        else if (playerData.weapon.name == "Weapon2")
        {
            this.GetComponent<GearEquipper>().Melee = 5;
            Change(5);
        }
        else if (playerData.weapon.name == "Weapon3")
        {
            this.GetComponent<GearEquipper>().Melee = 49;
            Change(29);
        }
        else if (playerData.weapon.name == "Weapon4")
        {
            this.GetComponent<GearEquipper>().Melee = 24;
            Change(31);
        }

        if (playerData.d_weapon.name == "D_Weapon0")
        {

        }
        else if (playerData.d_weapon.name == "D_Weapon1")
        {
            this.GetComponent<GearEquipper>().Shield = 7;
        }
        else if (playerData.d_weapon.name == "D_Weapon2")
        {
            this.GetComponent<GearEquipper>().Shield = 6;
        }
        else if (playerData.d_weapon.name == "D_Weapon3")
        {
            this.GetComponent<GearEquipper>().Shield = 3;
        }
        else if (playerData.d_weapon.name == "D_Weapon4")
        {
            this.GetComponent<GearEquipper>().Shield = 17;
        }

        this.GetComponent<GearEquipper>().ApplySkinChanges();
    }

    void Change(int num)
    {
        this.GetComponent<GearEquipper>().Armor = num;
        this.GetComponent<GearEquipper>().Helmet = num;
        this.GetComponent<GearEquipper>().Shoulder = num;
        this.GetComponent<GearEquipper>().Arm = num;
        this.GetComponent<GearEquipper>().Feet = num;
    }
}