using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame_Sound : MonoBehaviour
{
    public GameObject test;
    public GameObject test2;
    AudioSource audiotest;
    AudioSource audiotest2;
    AudioClip PlayerAttack1;
    AudioClip PlayerAttack2;
    AudioClip PlayerBuff;
    AudioClip PlayerDead;
    AudioClip PlayerWalk;
    AudioClip Monster_Skelleton_Attack;
    AudioClip Monster_Skelleton_Dead;
    AudioClip Monster_Ston_Attack;
    AudioClip Monster_Golem_Attack;
    AudioClip Monster_Grim_Attack;
    AudioClip Monster_Moros_Attack;
    AudioClip Monster_Dead;
    // Start is called before the first frame update
    void Start()
    {
        audiotest = test.GetComponent<AudioSource>();
        audiotest2 = test2.GetComponent<AudioSource>();
        PlayerAttack1 = Resources.Load<AudioClip>("Sound/SFX/player_Attack1_new");
        PlayerAttack2 = Resources.Load<AudioClip>("Sound/SFX/player_Attack2_new");
        PlayerBuff = Resources.Load<AudioClip>("Sound/SFX/player_Buff");
        PlayerDead = Resources.Load<AudioClip>("Sound/SFX/player_dead");
        PlayerWalk = Resources.Load<AudioClip>("Sound/SFX/player_walk_onestep_new");
        Monster_Skelleton_Attack = Resources.Load<AudioClip>("Sound/SFX/monster_skelleton_A");
        Monster_Skelleton_Dead = Resources.Load<AudioClip>("Sound/SFX/monster_skelleton_D");
        Monster_Ston_Attack = Resources.Load<AudioClip>("Sound/SFX/monster_stone_A");
        Monster_Golem_Attack = Resources.Load<AudioClip>("Sound/SFX/monster_golem_A");
        Monster_Grim_Attack = Resources.Load<AudioClip>("Sound/SFX/monster_grim_A");
        Monster_Moros_Attack = Resources.Load<AudioClip>("Sound/SFX/monster_moros_A");
        Monster_Dead = Resources.Load<AudioClip>("Sound/SFX/monster_dead");
        
    }

    public void playerAttack_SFX(int i)
    {
        if (i == 0)
        {
            audiotest.PlayOneShot(PlayerAttack1);
        }
        else if(i == 1)
        {
            audiotest.PlayOneShot(PlayerAttack2);
        }
    }

    public void playerBuff_SFX()
    {
        audiotest.PlayOneShot(PlayerBuff);
    }

    public void PlayerDead_SFX()
    {
        audiotest.PlayOneShot(PlayerDead);
    }

    public void PlayerWalk_SFX(int i)
    {
        if(i == 0)
        {
            audiotest.loop = true;
            audiotest.clip = PlayerWalk;
            audiotest.Play();
        }
        else if(i == 1)
        {
            audiotest.loop = false;
            audiotest.Pause();
        }
    }

    public void MonsterAttack_SFX(string str)
    {
        if (str.Equals("½ºÄÌ·¹Åæ"))
        {
            audiotest2.PlayOneShot(Monster_Skelleton_Attack);
        }
        else if (str.Equals("½ºÅæÇÇ½ºÆ®"))
        {
            audiotest2.PlayOneShot(Monster_Ston_Attack);
        } else if (str.Equals("¾ÆÀÌ½º°ñ·½"))
        {
            audiotest2.PlayOneShot(Monster_Golem_Attack);
        } else if (str.Equals("±×¸² ¸®ÆÛ"))
        {
            audiotest2.PlayOneShot(Monster_Grim_Attack);
        } else if (str.Equals("ÆÄ±«ÀÚ ¸ð·Î½º"))
        {
            audiotest2.PlayOneShot(Monster_Moros_Attack);
        }
    }

    public void MonsterDead_SFX(string str)
    {
        if (str.Equals("½ºÄÌ·¹Åæ"))
        {
            audiotest2.PlayOneShot(Monster_Skelleton_Dead);
        }
        else
        {
            audiotest2.PlayOneShot(Monster_Dead);
        }
    }
}
