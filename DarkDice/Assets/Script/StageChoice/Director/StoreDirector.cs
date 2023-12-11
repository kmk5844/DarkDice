using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StoreDirector : MonoBehaviour
{
    public GameObject playerObject; // 플레이어 정보를 가져온다.
    Player_Scritable player;

    public GameObject[] ItemObject_Data; //아이템 오브젝트 데이터를 가져온다.
    Item_Scritable[] item;

    public GameObject[] WeaponObject_Data; // 무기 오브젝트 데이터를 가져온다.
    Weapon_Scritable[] weapon;

    public TextMeshProUGUI CoinCount; // 현재 가지고 있는 코인

    public TextMeshProUGUI[] Item_Count; // 아이템 갯수
    public TextMeshProUGUI[] Item_Pride; // 아이템 가격
    public TextMeshProUGUI[] Weapon_Pride; // 무기 가격

    public Toggle[] Title_Toggle; // 아이템 상점과 무기 상점을 바꿀 수 있는 토글
    public GameObject[] Window_Toggle;

    public Button[] Item_Button;
    public Button[] Weapon_Button;
    public GameObject Buy_Window; // 마지막 확인
    public GameObject Dont_Click_Panel;
    Button YesButton;
    Button NoButton; 

    void Start()
    {
        player = playerObject.GetComponent<Player_Scritable>();
        item = new Item_Scritable[ItemObject_Data.Length];
        weapon = new Weapon_Scritable[WeaponObject_Data.Length];
        YesButton = Buy_Window.transform.GetChild(0).GetComponent<Button>();
        NoButton = Buy_Window.transform.GetChild(1).GetComponent<Button>();
        NoButton.onClick.AddListener(() => Buy_Window.SetActive(false));
        NoButton.onClick.AddListener(() => Dont_Click_Panel.SetActive(false));

        for (int i = 0; i < ItemObject_Data.Length; i++)
        {
            item[i] = ItemObject_Data[i].GetComponent<Item_Scritable>();
            Item_Pride[i].text = item[i].pride + "G";
        }

        for (int i = 0; i <  WeaponObject_Data.Length; i++)
        {
            weapon[i] = WeaponObject_Data[i].GetComponent<Weapon_Scritable>();
            Weapon_Pride[i].text = weapon[i].weapon_pride + "G";
        }
    }

    void Update()
    {
        CoinCount.text = player.coin.ToString();

        if (Title_Toggle[0].isOn) // 아이템 상점
        {
            for (int i = 0; i < item.Length; i++)
            {
                Item_Count[i].text = item[i].itemcount.ToString();
                if (player.coin < item[i].pride)
                {
                    Item_Button[i].interactable = false;
                }
                else
                {
                    Item_Button[i].interactable = true;
                }
            }

            if (player.coin < 500)
            {
                Item_Button[4].interactable = false;
            }
            else
            {
                Item_Button[4].interactable = true;
            }
        }
        else // 무기 상점
        {
            for (int i = 0; i < weapon.Length; i++)
            {
                if (player.coin < weapon[i].weapon_pride || weapon[i].storeflag == 1)
                {
                    Weapon_Button[i].interactable = false;
                    if (weapon[i].storeflag == 1)
                    {
                        Weapon_Button[i].GetComponentInChildren<TextMeshProUGUI>().text = "<color=white>보유 중</color>";
                    }
                    else
                    {
                        Weapon_Button[i].GetComponentInChildren<TextMeshProUGUI>().text = weapon[i].weapon_pride + "G";
                    }
                   
                }
                else
                {
                    Weapon_Button[i].interactable = true;
                }
            }
        }
    }

    public void OnItemBuyWindow(int i) {
        Dont_Click_Panel.SetActive(true);
        Buy_Window.SetActive(true);
        YesButton.onClick.AddListener(() => OnItemBuy(i));
    }

    public void OnWeaponBuyWindow(int i) {
        Dont_Click_Panel.SetActive(true);
        Buy_Window.SetActive(true);
        YesButton.onClick.AddListener(() => OnWeaponBuy(i));
    }

    public void OnItemBuy(int i)
    {
        YesButton.onClick.RemoveAllListeners();
        if(i == 4)
        {
            player.BuyCoin_Player(500);
            player.ItemUse_Init_Player();
        }
        else
        {
            item[i].Buy_Item();
            player.BuyCoin_Player(item[i].pride);
        }
        Buy_Window.SetActive(false);
        Dont_Click_Panel.SetActive(false);
    }
    public void OnWeaponBuy(int i)
    {
        YesButton.onClick.RemoveAllListeners();
        weapon[i].BuyWeapon_Weapon();
        player.BuyCoin_Player(weapon[i].weapon_pride);
        Buy_Window.SetActive(false);
        Dont_Click_Panel.SetActive(false);
    }

    public void OnTestCoinButton()
    {
        player.TestPlusCoinData();
    }

    public void OnInitButton()
    {
        player.playerInit();
        for(int i = 0; i < weapon.Length; i++)
        {
            weapon[i].weaponData.InitWeapon();
        }

        for(int i = 0; i < item.Length; i++)
        {
            item[i].itemData.Init();
        }
    }
}
