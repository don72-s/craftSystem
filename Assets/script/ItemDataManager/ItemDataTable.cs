using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public static partial class ItemDataTable
{

    private static Dictionary<ItemID, BaseItemData> baseItemDataDictionary = null;
    private static Dictionary<ItemID, ProvidedItemData> providedItemDataDictonary = null;


    private static Item CreateItem(ItemID _id, ProvidedItemData _pd) {

        if (!baseItemDataDictionary.ContainsKey(_id)) {

            Debug.LogError("아이템 등록정보가 없음.");
            return null;
        }

        BaseItemData bd = baseItemDataDictionary[_id];

        Item retItem = new Item(_id, bd.name, bd.IsDestroyOnCraft, bd.description, _pd.quality, new Dictionary<Tags, int>(_pd.tagDic));

        return retItem; 
    
    }


    public static Item CreateItem(System_Provid_ItemID _id) {

        if (baseItemDataDictionary == null) InitDataTable();

        ItemID id = TagAdapter.SystemItem_To_ItemTag(_id);

        if (!providedItemDataDictonary.ContainsKey(id))
        {
            Debug.LogError("시스템 아이템 등록정보가 없음.");
            return null;
        }

        return CreateItem(id, providedItemDataDictonary[id]);

    }


    public static Item CreateItem(ItemID _id,  UndeterminedItemInfo _itemInfo) {

        if (baseItemDataDictionary == null) InitDataTable();

        ProvidedItemData pd = new ProvidedItemData(_id, _itemInfo.quality, _itemInfo.tagDic);

        return CreateItem(_id, pd);

    }




    private class BaseItemData {

        public readonly ItemID itemID;
        public readonly string name;
        public readonly bool IsDestroyOnCraft = false;
        public readonly string description;

        public BaseItemData(ItemID _itemID, string _name, string _description ,bool _isDestroyOnCraft) {

            itemID = _itemID;
            name = _name;
            IsDestroyOnCraft = _isDestroyOnCraft;
            description = _description;


        }
    }

    private class ProvidedItemData {

        public readonly ItemID itemID;
        public readonly int quality;
        public Dictionary<Tags, int> tagDic;

        /// <summary>
        /// 태그의 갯수가 모두 하나가 아닌 경우의 생성자
        /// </summary>
        /// <param name="_itemID">아이템 id</param>
        /// <param name="_quality">고유값으로 제공될 품질</param>
        /// <param name="_tags">고유값으로 제공될 태그들과 각 태그의 갯수</param>
        public ProvidedItemData(ItemID _itemID, int _quality,params (Tags, int)[] _tags) {

            itemID = _itemID;
            quality = _quality;

            tagDic = new Dictionary<Tags, int>();

            foreach ((Tags, int) tag in _tags) {
                tagDic.Add(tag.Item1, tag.Item2);
            }

        }

        /// <summary>
        /// 태그의 갯수가 모두 하나가 아닌 경우의 생성자
        /// </summary>
        /// <param name="_itemID">아이템 id</param>
        /// <param name="_quality">고유값으로 제공될 품질</param>
        /// <param name="_tags">고유값으로 제공될 태그들과 각 태그의 갯수</param>
        public ProvidedItemData(ItemID _itemID, int _quality, Dictionary<Tags, int> _tags)
        {

            itemID = _itemID;
            quality = _quality;

            tagDic = new Dictionary<Tags, int>(_tags);

        }

        /// <summary>
        /// 모든 태그의 갯수가 하나인 경우의 생성자.
        /// </summary>
        /// <param name="_itemID">아이템 id</param>
        /// <param name="_quality">고유값으로 제공될 품질</param>
        /// <param name="_tags">고유값으로 제공될 태그들</param>
        public ProvidedItemData(ItemID _itemID, int _quality, params Tags[] _tags)
        {

            itemID = _itemID;
            quality = _quality;

            tagDic = new Dictionary<Tags, int>();

            foreach (Tags tag in _tags)
            {
                tagDic.Add(tag, 1);
            }

        }

    }





    private static void InitDataTable()
    {

        baseItemDataDictionary = new Dictionary<ItemID, BaseItemData>();
        providedItemDataDictonary = new Dictionary<ItemID, ProvidedItemData>();

        InitItemDataInfo();
        InitConstProvideInfo();

    }


}

/// <summary>
/// 일단은 내가 사용할 아이템 구조.
/// </summary>
public class Item {

    public ItemID itemID;
    public string name;
    public bool IsDestroyOnCraft = false;
    public string description;
    public int quality;
    public Dictionary<Tags, int> tagDic;

    public Item(ItemID _id, string _name, bool _isDestroyOnCraft,string _description, int _quality, Dictionary<Tags, int> _tagDic) { 
    
        itemID = _id;
        name = _name;
        IsDestroyOnCraft = _isDestroyOnCraft;
        description = _description;
        quality = _quality;
        tagDic = _tagDic;

    }

}


public class UndeterminedItemInfo{

    public int quality;
    public Dictionary<Tags, int> tagDic;
    public List<Item> enteredItemList;

    public UndeterminedItemInfo() {

        tagDic = new Dictionary<Tags, int>();
        enteredItemList = new List<Item>();

    }

}
