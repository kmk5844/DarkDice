using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame_Sound : MonoBehaviour
{
    AudioSource Player_SFX;
    AudioSource Monster_SFX;
    AudioClip PlayerAttack1;
    AudioClip PlayerAttack2;
    AudioClip PlayerDraw;
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
        Player_SFX = GameObject.Find("Sfx_Player").GetComponent<AudioSource>();
        Monster_SFX = GameObject.Find("Sfx_Monster").GetComponent<AudioSource>();
        PlayerAttack1 = Resources.Load<AudioClip>("Sound/SFX/player_Attack1_new");
        PlayerAttack2 = Resources.Load<AudioClip>("Sound/SFX/player_Attack2_new");
        PlayerDraw = Resources.Load<AudioClip>("Sound/SFX/player_Attack1_draw");
        PlayerBuff = Resources.Load<AudioClip>("Sound/SFX/player_Buff");
        PlayerDead = Resources.Load<AudioClip>("Sound/SFX/player_dead");
        PlayerWalk = Resources.Load<AudioClip>("Sound/SFX/player_walk_onestep_new_new");
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
            Player_SFX.PlayOneShot(PlayerAttack1);
        }
        else if(i == 1)
        {
            Player_SFX.PlayOneShot(PlayerAttack2);
        }else if(i == 2)
        {
            Player_SFX.PlayOneShot(PlayerDraw);
        }
    }

    public void playerBuff_SFX()
    {
        Player_SFX.PlayOneShot(PlayerBuff);
    }

    public void PlayerDead_SFX()
    {
        Player_SFX.PlayOneShot(PlayerDead);
    }

    public void PlayerWalk_SFX(int i)
    {
        if(i == 0)
        {
            Player_SFX.loop = true;
            Player_SFX.clip = PlayerWalk;
            Player_SFX.Play();
        }
        else if(i == 1)
        {
            Player_SFX.loop = false;
            Player_SFX.Pause();
        }
    }

    public void MonsterAttack_SFX(string str)
    {
        if (str.Equals("½ºÄÌ·¹Åæ"))
        {
            Monster_SFX.PlayOneShot(Monster_Skelleton_Attack);
        }
        else if (str.Equals("½ºÅæÇÇ½ºÆ®"))
        {
            Monster_SFX.PlayOneShot(Monster_Ston_Attack);
        } else if (str.Equals("¾ÆÀÌ½º°ñ·½"))
        {
            Monster_SFX.PlayOneShot(Monster_Golem_Attack);
        } else if (str.Equals("±×¸² ¸®ÆÛ"))
        {
            Monster_SFX.PlayOneShot(Monster_Grim_Attack);
        } else if (str.Equals("ÆÄ±«ÀÚ ¸ð·Î½º"))
        {
            Monster_SFX.PlayOneShot(Monster_Moros_Attack);
        }
    }

    public void MonsterDead_SFX(string str)
    {
        if (str.Equals("½ºÄÌ·¹Åæ"))
        {
            Monster_SFX.PlayOneShot(Monster_Skelleton_Dead);
        }
        else
        {
            Monster_SFX.PlayOneShot(Monster_Dead);
        }
    }
}
