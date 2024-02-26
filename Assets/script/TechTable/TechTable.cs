using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechClass
{

    //실제 호출에 이용되는 부분.
    #region 팩토리 구현

    /// <summary>
    /// 기술 제작 팩토리.
    /// </summary>
    public static class TechFactoroy
    {

        private static Dictionary<TechType, ITech> techFactoryDic = null;

        /// <summary>
        /// 팩토리 초기화.
        /// </summary>
        private static void InitDictionary()
        {

            techFactoryDic = new Dictionary<TechType, ITech>();

            techFactoryDic.Add(TechType.CARVE, new Carve());
            techFactoryDic.Add(TechType.ROLLING, new Rolling());
            techFactoryDic.Add(TechType.FORGED_WELDING, new ForgedWelding());
            techFactoryDic.Add(TechType.HAMMERING, new Hammering());
            techFactoryDic.Add(TechType.AMALGAMATION, new Amalgamation());
            techFactoryDic.Add(TechType.CASTING, new Casting());
            techFactoryDic.Add(TechType.HOTFORGING, new HotForging());
            techFactoryDic.Add(TechType.STEAMDISTILLATION, new SteamDistillation());
            techFactoryDic.Add(TechType.MIX, new Mix());
            techFactoryDic.Add(TechType.GRIND, new Grind());
            techFactoryDic.Add(TechType.COMBINE, new Combine());
            techFactoryDic.Add(TechType.MERCURY_DISTILLATION, new MercuryDistillation());
            techFactoryDic.Add(TechType.ADHESION, new Adhesion());
            techFactoryDic.Add(TechType.BURNINCENSE, new BurnIncense());

        }

        public static ITech GetTechType(TechType _techType)
        {

            if (techFactoryDic == null) InitDictionary();

            if (!techFactoryDic.ContainsKey(_techType))
            {
                Debug.Log("등록되지 않은 기술 행위.");
                return null;
            }

            return techFactoryDic[_techType];

        }

    }

    #endregion


    /// <summary>
    /// 기술연산용 중간정제 데이터 형식
    /// </summary>
    public class ItemData
    {

        public Dictionary<Tags, int> dic_tagAndCount;
        public int quality;

        public ItemData(int _quality = 0)
        {
            dic_tagAndCount = new Dictionary<Tags, int>();
            quality = _quality;
        }

        //todo : 디버그용 출력 함수.
        public void printInfo() {

            Debug.Log("연산 결과 출력");

            foreach (Tags key in dic_tagAndCount.Keys) { 
            
                Debug.Log(key + " / " + dic_tagAndCount[key]);

            }

            Debug.Log("quality : " + this.quality);

        }

        #region 기술 연산을 위한 데이터 제공 함수 영역

        public void AddTag(Tags _tag, int _count)
        {

            if (dic_tagAndCount.ContainsKey(_tag))
            {
                dic_tagAndCount[_tag] += _count;
            }
            else
            {
                dic_tagAndCount.Add(_tag, _count);
            }
        }

        public void RemoveTag(Tags _tags)
        {

            if (!IsContainTag(_tags))
            {
                Debug.Log("없는 태그 제거 시도");
                return;
            }

            dic_tagAndCount.Remove(_tags);

        }

        public void SetTagCount(Tags _tags, int _count)
        {

            if (!dic_tagAndCount.ContainsKey(_tags)) return;
            dic_tagAndCount[_tags] = _count;

        }

        public void SetAllTagCount(int _count)
        {

            var keys = new List<Tags>(dic_tagAndCount.Keys);

            foreach (Tags _tag in keys)
            {
                dic_tagAndCount[_tag] = _count;
            }
        }

        public void SetAllTagCountByDivisor(float _divisor)
        {

            var keys = new List<Tags>(dic_tagAndCount.Keys);

            foreach (Tags _tag in keys)
            {
                dic_tagAndCount[_tag] = (int)Mathf.Ceil(dic_tagAndCount[_tag] / _divisor);
            }
        }


        public int GetTagCount(Tags _tag)
        {
            if (!dic_tagAndCount.ContainsKey(_tag)) return 0;
            return dic_tagAndCount[_tag];
        }

        public int GetAllTagCount()
        {

            int ret = 0;

            foreach (Tags _tag in dic_tagAndCount.Keys)
            {
                ret += dic_tagAndCount[_tag];
            }

            return ret;

        }
        public bool IsContainTag(Tags _tag)
        {
            return dic_tagAndCount.ContainsKey(_tag);
        }



        public void CombineTagDictionary(Dictionary<Tags, int> _newDictionary)
        {

            foreach (Tags key in _newDictionary.Keys)
            {
                AddTag(key, _newDictionary[key]);
            }

        }

        public virtual bool IsAmalgam()
        {
            return false;
        }

        #endregion


        /// <summary>
        /// 데이터를 UndeterminedItemInfo타입으로 전환해 반환 [ enteredItemList는 초기화되지 않음! ] 
        /// </summary>
        /// <returns>기술연산 완료된 UndeterminedItemInfo 객체</returns>
        public UndeterminedItemInfo ChangeDataTypeToUndeterminedItemInfo() {

            UndeterminedItemInfo retInfo = new UndeterminedItemInfo();
            retInfo.quality = quality;
            retInfo.tagDic = new Dictionary<Tags, int>(dic_tagAndCount);

            return retInfo;

        }

    }

    class AmalgamItem_ItemType : ItemData
    {

        public AmalgamItem_ItemType(int _quality) : base(_quality) { }

        public override bool IsAmalgam()
        {
            return true;
        }

    }


    class TechContainer
    {

        private List<ItemData> items;




        public TechContainer() { 
        
        }

        public TechContainer(List<Item> _InputItems) {

            items = new List<ItemData>();

            ItemData retItemData;

            foreach (Item _itemData in _InputItems) {

                retItemData = new ItemData(_itemData.quality);

                if (_itemData.itemID == ItemID._32_AMALGAM) retItemData = new AmalgamItem_ItemType(_itemData.quality);

                foreach (Tags _tag in _itemData.tagDic.Keys) {
                    retItemData.AddTag(_tag, _itemData.tagDic[_tag]);
                }
                items.Add(retItemData);
            }

        }

        /// <summary>
        /// 디버그용 정보 출력
        /// </summary>
        public void printInfo()
        {

            foreach (ItemData _data in items)
            {

                foreach (Tags key in _data.dic_tagAndCount.Keys)
                {
                    Debug.Log(key + " / " + _data.dic_tagAndCount[key]);
                }

                Debug.Log("item quality : " + _data.quality);

            }

            Debug.Log("============ 입력 아이템 정보 출력 종료 ============");

        }




        public bool IsContainsTag(Tags _tag)
        {

            foreach (ItemData item in items)
            {

                if (item.IsContainTag(_tag)) return true;

            }

            return false;

        }

        public void AddTagsIfContains(Tags _newTag, Tags _conditionTag)
        {

            foreach (ItemData item in items)
            {

                if (item.IsContainTag(_conditionTag))
                {
                    item.AddTag(_newTag, item.GetTagCount(_conditionTag));
                }

            }

        }

        public void RemoveTagsToAllItems(Tags _tag)
        {
            foreach (ItemData item in items)
            {
                if (item.IsContainTag(_tag)) item.RemoveTag(_tag);
            }
        }

        public int GetTagCount(Tags _tag)
        {

            int count = 0;

            foreach (ItemData item in items)
            {
                count += item.GetTagCount(_tag);
            }

            return count;

        }

        public int GetAllTagCount()
        {

            int ret = 0;

            foreach (ItemData item in items)
            {
                ret += item.GetAllTagCount();
            }

            return ret;
        }


        public int GetItemCount()
        {
            return items.Count;
        }

        public int GetQuality_IncludeTag(Tags _tag)
        {

            int ret = 0;

            foreach (ItemData item in items)
            {
                if (item.IsContainTag(_tag)) ret += item.quality;
            }

            return ret;

        }

        public int GetQuality_IncludeTag_ExcludeTag(Tags _includeTag, Tags _excludeTag)
        {

            int ret = 0;

            foreach (ItemData item in items)
            {
                if (item.IsContainTag(_includeTag) && !item.IsContainTag(_excludeTag)) ret += item.quality;
            }

            return ret;

        }

        public bool IsItemExist_IncludeExcludeTag1_2(Tags _includeTag, Tags _excludeTag)
        {

            foreach (ItemData item in items)
            {
                if (item.IsContainTag(_includeTag) && !item.IsContainTag(_excludeTag)) return true;
            }

            return false;

        }

        public void SetTagCount(Tags _tag, int _count)
        {
            foreach (ItemData item in items)
            {
                item.SetTagCount(_tag, _count);
            }
        }

        public void SetAllTagCount(int _count)
        {
            foreach (ItemData item in items)
            {
                item.SetAllTagCount(_count);
            }
        }

        public void SetAllTagCount_HasTag(Tags _tag, int _count)
        {
            foreach (ItemData item in items)
            {
                if (item.IsContainTag(_tag)) item.SetAllTagCount(_count);
            }
        }

        public void DecreaseQualityValueByRatio(Tags _tag, float _ratio)
        {

            foreach (ItemData item in items)
            {
                if (item.IsContainTag(_tag)) item.quality = (int)(item.quality * _ratio);
            }

        }

        public ItemData GetResultTagData()
        {

            ItemData retData = new ItemData();

            foreach (ItemData item in items)
            {
                retData.CombineTagDictionary(item.dic_tagAndCount);
            }

            return retData;

        }

        public ItemData GetResultCommonMinimumTags()
        {            

            if(items.Count == 1) return items[0]; // MOSSY: 아이템이 하나일 때엔 해당 아이템의 태그를 그대로 반환.

            ItemData retData = new ItemData();

            Dictionary<Tags, int> crossCheckDic = new Dictionary<Tags, int>();
            Dictionary<Tags, int> retDic = new Dictionary<Tags, int>();


            foreach (ItemData item in items)
            {

                foreach (Tags tag in item.dic_tagAndCount.Keys)
                {

                    if (!crossCheckDic.ContainsKey(tag))
                    {
                        crossCheckDic.Add(tag, item.dic_tagAndCount[tag]);
                    }
                    else
                    {
                        if (retDic.ContainsKey(tag))
                        {
                            retDic[tag] = Mathf.Min(crossCheckDic[tag], item.dic_tagAndCount[tag]);
                            crossCheckDic[tag] = retDic[tag];
                        }
                        else
                        {
                            retDic.Add(tag, Mathf.Min(crossCheckDic[tag], item.dic_tagAndCount[tag]));
                            crossCheckDic[tag] = retDic[tag];
                        }
                    }

                }

            }

            retData.CombineTagDictionary(retDic);

            return retData;
        }

        public int GetResultQualityData(bool isAverage = false)
        {

            int sumQ = 0;

            foreach (ItemData item in items)
            {
                sumQ += item.quality;
            }

            return sumQ;

        }

        public List<ItemData> HasAmalgams()
        {

            List<ItemData> amalgamList = new List<ItemData>();

            foreach (ItemData item in items)
            {
                if (item.IsAmalgam()) amalgamList.Add(item);
            }


            return amalgamList.Count == 0 ? null : amalgamList;

        }



    }


    public interface ITech
    {
        /// <summary>
        /// 주어진 ItemList에 대응하는 연산 후 결과 반환 [ 기술연산 조건에 충족하지 않으면 null 반환 ]
        /// </summary>
        /// <param name="Items">주어질 Item 리스트</param>
        /// <returns>결과 아이템 정보.</returns>
        UndeterminedItemInfo PlayTech(List<Item> Items);
    }

    class Tech : ITech
    {

        protected TechContainer itemContainer;
        protected ItemData returnData;

        public UndeterminedItemInfo PlayTech(List<Item> Items)
        {
            
            InitData(Items);

            //디버그
            itemContainer.printInfo();


            if (!IsValid()) return null;
            ConditionOperation();
            TagOperation();
            QualityOperation();

            returnData.printInfo();

            return returnData.ChangeDataTypeToUndeterminedItemInfo();

        }

        protected void InitData(List<Item> Items)
        {

            //=> 데이터 초기화.
            itemContainer = new TechContainer(Items);
            returnData = new ItemData();
        }

        /// <summary>
        /// 제한없음.
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsValid() { return true; }

        /// <summary>
        /// 조건부 연산 - 기본상태 - 아무것도 안함.
        /// </summary>
        protected virtual void ConditionOperation() { }

        /// <summary>
        /// 기본상태 - 모든 아이템 태그 반환/ returnData 설정.
        /// </summary>
        protected virtual void TagOperation()
        {

            returnData = itemContainer.GetResultTagData();

        }

        /// <summary>
        /// 기본상태 - 모든 재료 아이템의 품질의 합/ returnData 설정.
        /// </summary>
        protected virtual void QualityOperation()
        {

            returnData.quality = itemContainer.GetResultQualityData();

        }


    }

    #region 서브클래스들

    class Carve : Tech
    {

        protected override bool IsValid()
        {
            return itemContainer.IsContainsTag(Tags.BLUEPRINT);
        }

        protected override void TagOperation()
        {
            base.TagOperation();
            returnData.AddTag(Tags.BEAUTY, 1);
            returnData.RemoveTag(Tags.BLUEPRINT);
        }
    }

    class Rolling : Tech
    {

        protected override void ConditionOperation()
        {
            itemContainer.AddTagsIfContains(Tags.FOIL, Tags.METAL);
        }

        protected override void TagOperation()
        {
            returnData = itemContainer.GetResultCommonMinimumTags();
        }

        protected override void QualityOperation()
        {
            int aveQuality = itemContainer.GetResultQualityData() / itemContainer.GetItemCount(); // MOSSY, CHANGED: 평균값으로 반환
            int foilCount = itemContainer.GetTagCount(Tags.FOIL);

            returnData.quality = aveQuality - (foilCount * foilCount) + (4 * foilCount);

        }


    }

    class ForgedWelding : Tech
    {

        protected override bool IsValid()
        {
            return (itemContainer.IsContainsTag(Tags.METAL) && !(itemContainer.IsContainsTag(Tags.FOIL)));
        }


    }

    class Hammering : Tech
    {

        private bool badFlag;

        protected override void ConditionOperation()
        {

            badFlag = false;

            itemContainer.AddTagsIfContains(Tags.FOIL, Tags.METAL);

            if (itemContainer.GetTagCount(Tags.FOIL) >= 5)
            {
                badFlag = true;
            }

        }

        protected override void QualityOperation()
        {
            if (badFlag)
            {
                returnData.quality = 0;
                return;
            }

            returnData.quality = (itemContainer.GetResultQualityData() / itemContainer.GetItemCount()) + 2;
        }

    }

    class Amalgamation : Tech
    {

        protected override bool IsValid()
        {
            return (itemContainer.IsContainsTag(Tags.MERCURY) && itemContainer.IsItemExist_IncludeExcludeTag1_2(Tags.METAL, Tags.MERCURY));
        }

        protected override void TagOperation()
        {
            base.TagOperation();
            returnData.RemoveTag(Tags.FLUIDITY);
        }

        protected override void QualityOperation()
        {

            int includQuality = itemContainer.GetQuality_IncludeTag(Tags.MERCURY);
            int excludeQuality = itemContainer.GetQuality_IncludeTag_ExcludeTag(Tags.METAL, Tags.MERCURY);
            int count = itemContainer.GetItemCount();

            returnData.quality =
                ((includQuality + excludeQuality) / count) -
                ((includQuality - excludeQuality) / count);

        }

    }

    class Casting : Tech
    {

        protected override bool IsValid()
        {
            return itemContainer.IsContainsTag(Tags.STABILITY);
        }



    }

    class HotForging : Tech
    {

        protected override bool IsValid()
        {
            return (itemContainer.IsContainsTag(Tags.UNSTRUCTURED) && itemContainer.IsContainsTag(Tags.BLUEPRINT)) ||
                   (!itemContainer.IsContainsTag(Tags.UNSTRUCTURED) && (!itemContainer.IsContainsTag(Tags.BLUEPRINT)));
        }

        protected override void ConditionOperation()
        {
            if (itemContainer.IsContainsTag(Tags.UNSTRUCTURED) && itemContainer.IsContainsTag(Tags.BLUEPRINT))
            {
                itemContainer.RemoveTagsToAllItems(Tags.UNSTRUCTURED);
                itemContainer.RemoveTagsToAllItems(Tags.BLUEPRINT);
            }
        }

        protected override void QualityOperation()
        {
            base.QualityOperation();
            returnData.quality += 20;
        }

    }

    class SteamDistillation : Tech
    {

        protected override bool IsValid()
        {
            return itemContainer.IsContainsTag(Tags.HERB);
        }

        protected override void ConditionOperation()
        {
            itemContainer.RemoveTagsToAllItems(Tags.HERB);
        }

        //todo : 상의해볼것. 
        protected override void TagOperation()
        {
            base.TagOperation();
            returnData.AddTag(Tags.OIL, 1);
            returnData.AddTag(Tags.FLUIDITY, 1);

            //float totalTagCnt = returnData.dic_tagAndCount.Count;
            float itemCount = itemContainer.GetItemCount();
            //returnData.SetAllTagCount((int)Mathf.Ceil(totalTagCnt / itemCount));

            returnData.SetAllTagCountByDivisor(itemCount);
        }

        protected override void QualityOperation()
        {
            base.QualityOperation();
            returnData.quality = returnData.quality / itemContainer.GetItemCount() + 10;
        }

    }

    class Mix : Tech
    {
        protected override void ConditionOperation()
        {
            if (itemContainer.GetTagCount(Tags.FLUIDITY) <= itemContainer.GetItemCount()) itemContainer.RemoveTagsToAllItems(Tags.FLUIDITY);
        }

        protected override void TagOperation()
        {
            base.TagOperation();
            float totalTagCnt = returnData.GetAllTagCount();
            float itemCount = itemContainer.GetItemCount();
            returnData.SetAllTagCount((int)Mathf.Ceil(totalTagCnt / itemCount));
        }

        protected override void QualityOperation()
        {
            base.QualityOperation();
            returnData.quality /= itemContainer.GetItemCount();
        }

    }

    class Grind : Tech
    {

        protected override void TagOperation()
        {
            base.TagOperation();
            float totalTagCnt = returnData.GetAllTagCount();
            float itemCount = itemContainer.GetItemCount();
            returnData.SetAllTagCount((int)Mathf.Ceil(totalTagCnt / itemCount));
        }

        protected override void QualityOperation()
        {
            base.QualityOperation();
            returnData.quality /= itemContainer.GetItemCount();
        }

    }

    class Combine : Tech
    {



    }

    class MercuryDistillation : Tech
    {

        List<ItemData> amalgamList;

        protected override bool IsValid()
        {
            amalgamList = itemContainer.HasAmalgams();

            return amalgamList != null;
        }

        protected override void TagOperation()
        {
            base.TagOperation();
            returnData.RemoveTag(Tags.DEATH);
            returnData.RemoveTag(Tags.CHANGE);
            returnData.RemoveTag(Tags.INSTABILITY);
        }

        protected override void QualityOperation()
        {
            base.QualityOperation();

            int amalgamQuality = 0;
            int amalgamCount = amalgamList.Count;

            foreach (ItemData item in amalgamList)
            {
                amalgamQuality += item.quality;
            }

            int qualityWidthoutAmalgam = returnData.quality - amalgamQuality;


            returnData.quality = ((100 - amalgamQuality) / (1 + amalgamCount)) + qualityWidthoutAmalgam;

        }

    }

    class Adhesion : Tech
    {

        protected override void ConditionOperation()
        {
            itemContainer.DecreaseQualityValueByRatio(Tags.FOIL, 0.02f);
            itemContainer.SetAllTagCount_HasTag(Tags.FOIL, 1);
            itemContainer.RemoveTagsToAllItems(Tags.FOIL);
        }



    }

    class BurnIncense : Tech
    {

        protected override bool IsValid()
        {
            return itemContainer.IsContainsTag(Tags.RESIN) && !itemContainer.IsContainsTag(Tags.INCENSED);
        }

        protected override void TagOperation()
        {
            base.TagOperation();
            returnData.AddTag(Tags.INCENSED, 1);
        }

    }

    #endregion


}