using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentSizeFiter : MonoBehaviour
{

    public GridLayoutGroup gridLayoutGroup;
    public RectTransform trans;

    public float defaultContentHeight;
    public float defaultElementPadding;
    public float defaultElementHeight;

    void Update()
    {
        ResizeGridLayout();
    }

    void ResizeGridLayout()
    {

        if (trans.rect.size.x <= 0 || trans.rect.size.y <= 0) return;

        int padding = Mathf.RoundToInt((defaultElementPadding * trans.rect.height) / defaultContentHeight);

        gridLayoutGroup.padding.top = padding;
        gridLayoutGroup.padding.bottom = padding;
        gridLayoutGroup.spacing = new Vector2(gridLayoutGroup.spacing.x, padding);

        gridLayoutGroup.cellSize = new Vector2(gridLayoutGroup.cellSize.x, (defaultElementHeight * trans.rect.height) / defaultContentHeight);

    }

}
