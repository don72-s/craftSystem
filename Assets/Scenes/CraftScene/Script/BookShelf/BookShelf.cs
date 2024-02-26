using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookShelf : MonoBehaviour
{

    private RectTransform trans;

    private float hideOffset;
    private bool hideState;
    private Vector2 targetPosition;
    public float lerpSpeed = 5f;

    public List<GameObject> shelfList = new List<GameObject>();
    private Dictionary<ShelfType, GameObject> shelfDictionary = new Dictionary<ShelfType, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<RectTransform>();
        hideOffset = trans.rect.width;

        DictonaryMapping();
        initState();

    }

    private void Update()
    {
        trans.anchoredPosition = Vector2.Lerp(trans.anchoredPosition, targetPosition, Time.deltaTime * lerpSpeed);
    }

    public void initState() {

        hideState = true;
        trans.anchoredPosition = new Vector2(-hideOffset, 0);
        targetPosition = trans.anchoredPosition;
        ChangeFocusShelf((ShelfType)Enum.GetValues(typeof(ShelfType)).GetValue(0));

    }

    private void DictonaryMapping() {

        if (Enum.GetValues(typeof(ShelfType)).Length != shelfList.Count) {
            Debug.LogError("책장 종류수와 전달 객체 수가 일치하지 않음.");
            return;
        }

        for (int i = 0; i < shelfList.Count; i++) {
            shelfDictionary.Add((ShelfType)Enum.GetValues(typeof(ShelfType)).GetValue(i), shelfList[i]);
        }

        
    }


    public void Btn_HideShelf() {

        targetPosition = hideState ? Vector2.zero : new Vector2(-hideOffset, 0);
        hideState = !hideState;

    }


    private enum ShelfType { GRIMOIRE, ITEM_DICTIONARY, TECHNOLOGY};

    public void Btn_Category_Grimoire()
    {
        ChangeFocusShelf(ShelfType.GRIMOIRE);
    }
    public void Btn_Category_ItemDic()
    {
        ChangeFocusShelf(ShelfType.ITEM_DICTIONARY);
    }
    public void Btn_Category_Technology()
    {
        ChangeFocusShelf(ShelfType.TECHNOLOGY);
    }

    private void ChangeFocusShelf(ShelfType _type) {

        foreach (GameObject _o in shelfList) { 
            _o.SetActive(false);
        }

        shelfDictionary[_type].SetActive(true);

    }


}
