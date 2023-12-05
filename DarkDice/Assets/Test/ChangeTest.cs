using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class ChangeTest : MonoBehaviour
{
    public Transform Player;
    [SerializeField]
    private int Count;

    // Start is called before the first frame update
    void Start()
    {
        switch (Count)
        {
            case 0:
                break;
            case 1:
                Player.GetComponent<GearEquipper>().Melee = 2;
                Player.GetComponent<GearEquipper>().Armor = 2;
                Player.GetComponent<GearEquipper>().Helmet = 2;
                Player.GetComponent<GearEquipper>().Shoulder = 2;
                Player.GetComponent<GearEquipper>().Arm = 2;
                Player.GetComponent<GearEquipper>().ApplySkinChanges();
                break;
            case 2:
                Player.GetComponent<GearEquipper>().Melee = 4;
                Player.GetComponent<GearEquipper>().Armor = 4;
                Player.GetComponent<GearEquipper>().Helmet = 4;
                Player.GetComponent<GearEquipper>().Shoulder = 4;
                Player.GetComponent<GearEquipper>().Arm = 4;
                Player.GetComponent<GearEquipper>().ApplySkinChanges();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
