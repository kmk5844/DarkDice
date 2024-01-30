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
            atk = 0;
        }
        else if(playerData.weapon.name == "Weapon1")
        {
            atk = 1;
            this.GetComponent<GearEquipper>().Melee = 50;
        }
        else if (playerData.weapon.name == "Weapon2")
        {
            atk = 2;
            this.GetComponent<GearEquipper>().Melee = 5;
        }
        else if (playerData.weapon.name == "Weapon3")
        {
            atk = 3;
            this.GetComponent<GearEquipper>().Melee = 49;
        }
        else if (playerData.weapon.name == "Weapon4")
        {
            atk = 4;
            this.GetComponent<GearEquipper>().Melee = 24;
        }

        if (playerData.d_weapon.name == "D_Weapon0")
        {
            def = 0;
        }
        else if (playerData.d_weapon.name == "D_Weapon1")
        {
            def = 1;
            this.GetComponent<GearEquipper>().Shield = 7;
        }
        else if (playerData.d_weapon.name == "D_Weapon2")
        {
            def = 2;
            this.GetComponent<GearEquipper>().Shield = 6;
        }
        else if (playerData.d_weapon.name == "D_Weapon3")
        {
            def = 3;
            this.GetComponent<GearEquipper>().Shield = 3;
        }
        else if (playerData.d_weapon.name == "D_Weapon4")
        {
            def = 4;
            this.GetComponent<GearEquipper>().Shield = 17;
        }

        if(atk > def)
        {
            if(def == 3)
            {
                Change(29);
            }
            else if(def == 2)
            {
                Change(5);
            }
            else if(def == 1)
            {
                Change(14);
            }
            else if(def == 0)
            {

            }
        }else if(atk < def)
        {
            if (atk == 3)
            {
                Change(29);
            }
            else if (atk == 2)
            {
                Change(5);
            }
            else if (atk == 1)
            {
                Change(14);
            }
            else if (atk == 0)
            {

            }
        }
        else if(atk == def)
        {
            if(atk == 4)
            {
                Change(15);
            }
            else if(atk == 3)
            {
                Change(29);
            }
            else if(atk == 2)
            {
                Change(5);

            }
            else if(atk == 1)
            {
                Change(14);
            }
            else if (atk == 0)
            {

            }
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
    //weapon1 = 14;
    //weapon2 = 5;
    //weapon3 = 29;
    //weapon4 = 15; // юс╫ц
}