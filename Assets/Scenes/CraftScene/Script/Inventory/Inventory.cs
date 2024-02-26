using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    private RectTransform trans;

    private float hideOffset;
    private bool hideState;
    private Vector2 targetPosition;
    public float lerpSpeed = 5f;

    public List<GameObject> inventoryList = new List<GameObject>();
    private Dictionary<InvenType, GameObject> invenDictionary = new Dictionary<InvenType, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<RectTransform>();
        hideOffset = trans.rect.height;
        DictionaryMapping();
        initState();
    }


    // Update is called once per frame
    void Update()
    {
        trans.anchoredPosition = Vector2.Lerp(trans.anchoredPosition, targetPosition, Time.deltaTime * lerpSpeed);
    }


    public void initState() {

        hideState = true;
        ChangeFocusInven(InvenType.MATERIAL);
        trans.anchoredPosition = new Vector2(trans.anchoredPosition.x, hideOffset);
        targetPosition = trans.anchoredPosition;
        

    }

    private void DictionaryMapping() {

        if (Enum.GetValues(typeof(InvenType)).Length != inventoryList.Count)
        {
            Debug.LogError("인벤토리 종류수와 전달 객체 수가 일치하지 않음.");
            return;
        }

        for (int i = 0; i < inventoryList.Count; i++)
        {
            invenDictionary.Add((InvenType)Enum.GetValues(typeof(InvenType)).GetValue(i), inventoryList[i]);
        }

    }


    private enum InvenType { MATERIAL, TECHNOLOGY };

    public void Btn_Cancel() {

        if (hideState) return;

        hideState = !hideState;

        targetPosition = new Vector2(trans.anchoredPosition.x, hideOffset);

    }

    public void Btn_Material() {

        if (hideState) hideState = !hideState;

        ChangeFocusInven(InvenType.MATERIAL);

    }

    public void Btn_Tech() {

        if (hideState) hideState = !hideState;

        ChangeFocusInven(InvenType.TECHNOLOGY);

    }


    private void ChangeFocusInven(InvenType _type) {

        foreach (GameObject _o in inventoryList) {
            _o.SetActive(false);
        }

        invenDictionary[_type].SetActive(true);

        targetPosition = new Vector2(trans.anchoredPosition.x, 0);

    }


}
