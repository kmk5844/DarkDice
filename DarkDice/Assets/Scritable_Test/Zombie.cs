using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public ZombieData zombieData;
    public void PrintZombieData()
    {
        Debug.Log("���� �̸� :: " + zombieData.ZombieName);
        Debug.Log("ü�� :: " + zombieData.Hp);
        Debug.Log("���ݷ� :: " + zombieData.Damage);
        Debug.Log("�þ� :: " + zombieData.SightRange);
        Debug.Log("�̵� �ӵ� :: " + zombieData.MoveSpeed);
        Debug.Log("===========================================");
    }
}
