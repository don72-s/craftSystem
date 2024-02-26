using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardInfo : MonoBehaviour, IDragHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    public delegate void CardClicked(CardInfo _cardData);


    private static int cardID = 0;
    public int myId { get; private set; }

    public Text text;
    public Image image;
    public GameObject description;



    public Item itemData { get; private set; }
    private CardClicked cardClickedHandler;

    

    public void InitCardInfo(Item _itemData, CardClicked _cardClickedHandler) {

        itemData = _itemData;

        cardClickedHandler = _cardClickedHandler;

        myId = cardID;
        cardID++;

        string description = "";

        description = description + itemData.itemID + "\n";

        foreach (Tags _tags in itemData.tagDic.Keys)
        {
            description = description + _tags + " : " + itemData.tagDic[_tags] + "\n";
        }

        description = description + "\n quality : " + itemData.quality + "\n";

        text.text = description;



        this.description.GetComponentInChildren<Text>().text = "[ " + _itemData.name + " ]\n\n" + _itemData.description;

    }



    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {

            cardClickedHandler?.Invoke(this);

        }
    }



    public void toActive() {

        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);

    }

    public void toInActive()
    {

        image.color = new Color(image.color.r, image.color.g, image.color.b, 0.5f);

    }


    #region 인터페이스 구현 영역

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        onFlag = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        overCount = 0;
        onFlag = false;
        description.SetActive(false);
    }

    #endregion

    private bool onFlag = false;

    private const float DescriptionShowOverSecond = 2;
    private float overCount = 0;

    private void Update()
    {

        if (onFlag)
        {
            overCount += Time.deltaTime;
            if (overCount > DescriptionShowOverSecond) { 
                description.SetActive(true);
            }
        }

    }


}
