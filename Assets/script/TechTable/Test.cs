using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class testTeqItem
{

    [SerializeField]
    public List<tagInfo> tags = new List<tagInfo>();

    [SerializeField]
    public int qualilty;

    [SerializeField]
    public bool isAmalgam;

}

[System.Serializable]
public class tagInfo
{

    [SerializeField]
    public Tags tag;

    [SerializeField]
    public int count;

}

public class Test : MonoBehaviour
{


    public GameObject cardInstance;
    public Transform trans;
    public Dropdown dropdown;
    public Dropdown itemTypeDropdown;



    public void debug_createCard(Item _newItemData)
    {

        GameObject cardObject = Instantiate(cardInstance, new Vector3(700, 300, 0), Quaternion.identity);
        cardObject.transform.SetParent(trans);

        cardObject.GetComponent<CardInfo>().InitCardInfo(_newItemData, CardOnClicked);

    }


    public void debug_CreateSystemBaseCards() { 
    
        //스크롤에서 임시로 가져오는 아이템 아이디.
        Item item = ItemDataTable.CreateItem(itemTypeDropdown.GetComponent<DropDownCardBase>().getDropdownTechtype());



        //임시 카드 오브젝트 원본과 부모(cardDeck)
        GameObject cardObject = Instantiate(cardInstance, new Vector3(700, 300, 0), Quaternion.identity);
        cardObject.transform.SetParent(trans);


        //카드 정보 초기화.
        cardObject.GetComponent<CardInfo>().InitCardInfo(item, CardOnClicked);

    }


    public void debug_PlayTech_MakeCard()
    {

        TechType type = dropdown.GetComponent<DropdownScript>().getDropdownTechtype();

        List<Item> itemDataList = new List<Item>();


        foreach (CardInfo _cardInfo in dic_CardData.Values) {

            itemDataList.Add(_cardInfo.itemData);

        }


        //합침 연산 실행. todo : => Item 반환으로 수정
        UndeterminedItemInfo forCheckrecipe = TechClass.TechFactoroy.GetTechType(type).PlayTech(itemDataList);

        if (forCheckrecipe != null)
        {

            forCheckrecipe.enteredItemList = itemDataList;


            ItemID tmpRetID = ItemMaker.TechRecipeChecker.CheckRecipe(forCheckrecipe);



            Debug.Log("===========================연산결과 아웃풋 아이템================================");
            Debug.Log("===========================" + tmpRetID + "================================");
            Debug.Log("===========================연산결과 아웃풋 아이템================================");

            Item resultItem = ItemDataTable.CreateItem(tmpRetID, forCheckrecipe);

            debug_createCard(resultItem);

        }

        foreach (CardInfo _cardInfo in dic_CardData.Values) {

            _cardInfo.toActive();

        }

        dic_CardData.Clear();

    }






    Dictionary<int, CardInfo> dic_CardData = new Dictionary<int, CardInfo>();

    public void CardOnClicked(CardInfo _cardData)
    {

        if (dic_CardData.ContainsKey(_cardData.myId))
        {
            dic_CardData.Remove(_cardData.myId);
            _cardData.toActive();
        }
        else
        {
            dic_CardData.Add(_cardData.myId, _cardData);
            _cardData.toInActive();

        }

    }


}
