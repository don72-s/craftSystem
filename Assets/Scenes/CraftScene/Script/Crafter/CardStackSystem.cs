using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStackSystem : MonoBehaviour
{
    List<GameObject> stack = new List<GameObject>();
    private RectTransform trans;
    private Vector2 CardSizeVec2;


    // Start is called before the first frame update
    void Start()
    {

        trans = GetComponent<RectTransform>();
        CardSizeVec2 = CustomCardSize ? new Vector2(cardWidth, cardHeight) : new Vector2(trans.rect.width / 3, trans.rect.height / 10 * 8);

    }

    public void initState()
    {

        ClearStack();

    }


    //todo : 어떤 카드인지 전달받고 스택에 저장해야 함.
    private void PushCardStack()
    {

        if (stack.Count >= MAX_CARD_STACKS && !isOverStackable)
        {
            Debug.Log("최대 갯수 초과로 카드를 쌓을 수 없음.");
            return;
        }

        GameObject g;

        g = Instantiate(img, transform.position, Quaternion.identity, transform);
        g.GetComponent<RectTransform>().sizeDelta = CardSizeVec2;
        stack.Add(g);

        SortCardStack();

    }

    //todo : 어떤 카드를 제거했는지 정보를 전달해야 함.
    private void PopCardStack()
    {

        if (stack.Count <= 0)
        {
            Debug.Log("스택이 비어있음.");
            return;
        }

        GameObject g = stack[stack.Count - 1];
        stack.RemoveAt(stack.Count - 1);
        Destroy(g);

        SortCardStack();

        //todo : 정보 리턴.

    }


    private void SortCardStack()
    {

        float cardLength = CardSizeVec2.x;

        float baseVal = -0.5f * trans.rect.width + 0.1f * cardLength + 0.5f * cardLength;
        float limitVal = 0.5f * trans.rect.width - 0.1f * cardLength - 0.5f * cardLength;
        float offset = (limitVal - baseVal) / (stack.Count + 1);

        if (limitVal < baseVal) {

            offset = -offset;
            baseVal = limitVal;

        }

        for (int i = 0; i < stack.Count; i++)
        {
            stack[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(baseVal + (offset * (i + 1)), 0f);
        }

    }

    private void ClearStack()
    {

        //todo : foreach를 반복하면서 준비된 객체를 인벤토리로 반환.

        foreach (GameObject _g in stack)
        { //파괴 말고 인벤토리로 권한 위임 방식 사용. => 현재는 디버그용.
            Destroy(_g);
        }

    }


    public bool isOverStackable = true;
    public int MAX_CARD_STACKS = 7;

    public bool CustomCardSize;
    public float cardWidth;
    public float cardHeight;

    // Update is called once per frame
    void Update()
    {
        //디버그용 = > 후에는 push, pop메소드를 공개.
        if (Input.GetKeyDown(KeyCode.A))
        {
            PushCardStack();

        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            PopCardStack();

        }

    }

    public GameObject img;
}
