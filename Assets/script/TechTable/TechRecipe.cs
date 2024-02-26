using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class TechRecipe
{

    protected delegate int GetMakeAmountDelegate(UndeterminedItemInfo _itemInfo);

    protected long reqTags;
    protected int minReqQuality;
    protected int maxReqQuality;



    /// <summary>
    /// 초기 데이터 설정
    /// </summary>
    /// <param name="_minQuality">최소 요구 품질</param>
    /// <param name="_maxQuality">최대 요구 품질</param>
    /// <param name="_tags">기본 요구 태그들</param>
    protected void InitData(int _minQuality, int _maxQuality, params Tags[] _tags) { 
    
        minReqQuality = _minQuality;
        maxReqQuality = _maxQuality;

        reqTags = 0;
        foreach (Tags _tag in _tags) {
            reqTags +=  1L << ((int)_tag);
        }


    }


    /// <summary>
    /// 기본적인 레시피 요구사항 확인.[ 요구 태그, 요구 품질 ]
    /// </summary>
    /// <returns>레시피 부합 여부</returns>
    public bool BasicReqCheck(long _inputTags, int _quality) {

        return ((reqTags & _inputTags) == reqTags) && (_quality >= minReqQuality) && (_quality <= maxReqQuality);

    }

    /// <summary>
    /// 세부적인 레시피 요구사항 부합 여부 확인.[ 요구 태그, 입력 아이템 정보 ]
    /// </summary>
    /// <param name="_inputTags">요구 태그</param>
    /// <param name="_itemInfo">입력 아이템 정보</param>
    /// <returns>레시피 부합 여부</returns>
    public bool DetailReqCheck(long _inputTags, UndeterminedItemInfo _itemInfo) {

        return IsPerfectlyMatch(_inputTags)
            && IsContainsOtherTag(_inputTags)
            && RequestTagCounts(_itemInfo) 
            && CheckBanTag(_inputTags);

    }



    public List<Tags> NeedUniqueTagCheck() {

        return IsUniqueTagRecipe();

    }

    public bool NeedUniqueRecipeCheck() {

        return IsUniqueRecipe();

    }




    #region 세부사항 함수.

    /// <summary>
    /// 태그가 완전하게 일치해야 하는지 여부
    /// </summary>
    /// <param name="_inputTags">재료의 태그 정보.</param>
    /// <returns>체크의 성공 여부</returns>
    protected virtual bool IsPerfectlyMatch(long _inputTags) { return true; }

    /// <summary>
    /// 필수요구태그 이외의 다른 태그가 하나 이상 존재하는지 확인해야 하는지에 대한 여부
    /// </summary>
    /// <param name="_inputTags">재료의 태그 정보.</param>
    /// <returns>체크의 성공 여부</returns>
    protected virtual bool IsContainsOtherTag(long _inputTags) { return true; }

    /// <summary>
    /// 특정 태그들의 요구사항 갯수를 만족하는지에 대한 확인의 필요 여부
    /// </summary>
    /// <param name="_inputTags">재료의 태그 정보.</param>
    /// <returns>체크의 성공 여부</returns>
    protected virtual bool RequestTagCounts(UndeterminedItemInfo _itemInfo) { return true; }

    /// <summary>
    /// 밴 태그 포함에 대한 확인의 필요 여부
    /// </summary>
    /// <param name="_inputTags">재료의 태그 정보.</param>
    /// <returns>체크의 성공 여부</returns>
    protected virtual bool CheckBanTag(long _inputTags) { return true; }


    #endregion

    #region 왕따 레시피 체크 함수.

    /// <summary>
    /// 조건이 없을경우 null 반환.
    /// </summary>
    /// <returns></returns>
    protected virtual List<Tags> IsUniqueTagRecipe() { return null; }

    /// <summary>
    /// 해당 레시피만 존재해야 하는지에 대한 여부
    /// </summary>
    /// <returns>해당하면 true반환</returns>
    protected virtual bool IsUniqueRecipe() { return false; }

    #endregion

    /// <summary>
    /// 만들어질 아이템의 갯수 [ 기본 : 1 ]
    /// </summary>
    /// <param name="_itemInfo">제작 시 아이템 정보</param>
    /// <returns>만들어질 갯수</returns>
    protected virtual int GetMakeAmount(UndeterminedItemInfo _itemInfo) {
        return 1;
    }








    /// <summary>
    /// 제공되는 태그들이 모두 포함된 레시피인지에 대한 판단.
    /// </summary>
    /// <param name="_tagList">포함 확인을 요구할 태그들의 리스트</param>
    /// <returns>포함하는 레시피인 경우 [true] 반환</returns>
    public bool IsContainsTags(List<Tags> _tagList) {

        long tmpTagL = 0;

        foreach (Tags _tag in _tagList)
        {

            tmpTagL += 1L << (int)_tag;

        }

        return (tmpTagL & reqTags) == tmpTagL;

    }

}



class OilTechRecipe : TechRecipe {

    protected override bool IsPerfectlyMatch(long _inputTags)
    {
        return _inputTags == reqTags;
    }

    protected override int GetMakeAmount(UndeterminedItemInfo _itemInfo)
    {
        int inputItemCount = _itemInfo.enteredItemList.Count;

        foreach (Item item in _itemInfo.enteredItemList)
        {

            if (item.itemID == ItemID._24_WATER) inputItemCount--;

        }

        return inputItemCount;

    }

}


class CedarwoodOilTechRecipe : OilTechRecipe
{
    public CedarwoodOilTechRecipe() { 
        InitData(12, 12, Tags.STRENGTH, Tags.STABILITY, Tags.OIL, Tags.FLUIDITY);
    }

}

class SandalwoodOilTechRecipe : OilTechRecipe
{
    public SandalwoodOilTechRecipe()
    {
        InitData(12, 12, Tags.DEVOTION, Tags.PEACE, Tags.OIL, Tags.FLUIDITY);
    }

}

class LavenderOilTechRecipe : OilTechRecipe
{
    public LavenderOilTechRecipe()
    {
        InitData(11, 11, Tags.RECOVERY, Tags.PURITY, Tags.OIL, Tags.FLUIDITY);
    }

}

class RosemaryOilTechRecipe : OilTechRecipe
{
    public RosemaryOilTechRecipe()
    {
        InitData(11, 11, Tags.TRANQUILITY, Tags.OIL, Tags.FLUIDITY);
    }
}

class CypressOilTechRecipe : OilTechRecipe
{
    public CypressOilTechRecipe()
    {
        InitData(11, 11, Tags.PATIENCE, Tags.CHANGE, Tags.OIL, Tags.FLUIDITY);
    }
}

class SpikeNardOilTechRecipe : OilTechRecipe
{
    public SpikeNardOilTechRecipe()
    {
        InitData(13, 13, Tags.JOY, Tags.OIL, Tags.FLUIDITY);
    }

}

class RoseOilTechRecipe : OilTechRecipe
{
    public RoseOilTechRecipe()
    {
        InitData(15, 15, Tags.LOVE, Tags.OIL, Tags.PURITY, Tags.FLUIDITY);
    }

}

class MixedOilTechRecipe : OilTechRecipe
{
    public MixedOilTechRecipe()
    {
        InitData(11, 19, Tags.OIL, Tags.FLUIDITY);
    }

    protected override bool IsPerfectlyMatch(long _inputTags)
    {
        return true;
    }

    protected override List<Tags> IsUniqueTagRecipe()
    {
        List<Tags> uniqueTags = new List<Tags>();
        uniqueTags.Add(Tags.OIL);
        return uniqueTags;
    }

}



class AmalgamTechRecipe : TechRecipe
{
    public AmalgamTechRecipe()
    {
        InitData(63, 107, Tags.METAL, Tags.CHANGE, Tags.INSTABILITY, Tags.DEATH);
    }

    // protected override bool IsContainsOtherTag(long _inputTags)
    // {
    //     return (_inputTags - reqTags) != 0;
    // }

    protected override bool CheckBanTag(long _inputTags)
    {

        long banTag = 1L << (int)Tags.FLUIDITY;

        return (banTag & _inputTags) == 0;
    }

}

class GoldLeafTechRecipe : TechRecipe
{
    public GoldLeafTechRecipe()
    {
        InitData(79, 93, Tags.HOLINESS, Tags.JOY, Tags.LOVE, Tags.PROSPERITY, Tags.METAL, Tags.FOIL);
    }

    protected override bool IsPerfectlyMatch(long _inputTags)
    {
        return _inputTags == reqTags;
    }

    protected override int GetMakeAmount(UndeterminedItemInfo _itemInfo)
    {
        return _itemInfo.enteredItemList.Count;
    }

}

class WaxBurnerTechRecipe : TechRecipe
{
    public WaxBurnerTechRecipe()
    {
        InitData(131, 131, Tags.HOLINESS, Tags.PROTECTION, Tags.STABILITY, Tags.CHANGE, Tags.BEAUTY, Tags.FORM_INCENSE_BURNER, Tags.UNSTRUCTURED);
    }

    protected override bool IsPerfectlyMatch(long _inputTags)
    {
        return _inputTags == reqTags;
    }
}

class CopperBurnerTechRecipe : TechRecipe
{
    public CopperBurnerTechRecipe()
    {
        InitData(217, 228, Tags.HOLINESS, Tags.PURIFICATION, Tags.STRENGTH, Tags.METAL, Tags.FORM_INCENSE_BURNER);
    }

    protected override bool RequestTagCounts(UndeterminedItemInfo _itemInfo)
    {
        return _itemInfo.tagDic[Tags.METAL] == 3;
    }

}

class SilverBurnerTechRecipe : TechRecipe
{
    public SilverBurnerTechRecipe()
    {
        InitData(271, 282, Tags.HOLINESS, Tags.PURIFICATION, Tags.INSANITY, Tags.TRANQUILITY, Tags.METAL, Tags.FORM_INCENSE_BURNER);
    }

    protected override bool RequestTagCounts(UndeterminedItemInfo _itemInfo)
    {
        return _itemInfo.tagDic[Tags.METAL] == 3;
    }

}

class PureGoldBurnerTechRecipe : TechRecipe
{
    public PureGoldBurnerTechRecipe()
    {
        InitData(367, 378, Tags.HOLINESS, Tags.PROSPERITY, Tags.JOY, Tags.LOVE, Tags.METAL, Tags.FORM_INCENSE_BURNER);
    }

    protected override bool RequestTagCounts(UndeterminedItemInfo _itemInfo)
    {
        return _itemInfo.tagDic[Tags.METAL] == 3 && _itemInfo.tagDic[Tags.HOLINESS] == 2;
    }


}

class SacredWaxFigurineTechRecipe : TechRecipe
{
    public SacredWaxFigurineTechRecipe()
    {
        InitData(14, 14, Tags.HOLINESS, Tags.BEAUTY, Tags.FORM_SACRED_SCULPTURE, Tags.CHANGE, Tags.PROTECTION, Tags.UNSTRUCTURED, Tags.STABILITY);
    }

    protected override bool IsPerfectlyMatch(long _inputTags)
    {
        return _inputTags == reqTags;
    }
}

class DemonicWaxFigurineTechRecipe : TechRecipe
{
    public DemonicWaxFigurineTechRecipe()
    {
        InitData(13, 13, Tags.BLASPHEMY, Tags.BEAUTY, Tags.FORM_DEVILISH_SCULPTURE);
    }
    protected override bool IsPerfectlyMatch(long _inputTags)
    {
        return _inputTags == reqTags;
    }
}

class GildedBurnerTechRecipe : TechRecipe
{
    public GildedBurnerTechRecipe()
    {
        InitData(216, 247, Tags.HOLINESS, Tags.PURIFICATION, Tags.STRENGTH, Tags.METAL, Tags.JOY, Tags.PROSPERITY, Tags.LOVE, Tags.FORM_INCENSE_BURNER);
    }

    protected override bool RequestTagCounts(UndeterminedItemInfo _itemInfo)
    {
        return _itemInfo.tagDic[Tags.METAL] == 3 && _itemInfo.tagDic[Tags.HOLINESS] == 2;
    }

}

class ResinMixTechRecipe : TechRecipe
{
    public ResinMixTechRecipe()
    {
        InitData(1, 10, Tags.RESIN);
    }

    protected override bool IsContainsOtherTag(long _inputTags)
    {
        return (_inputTags - reqTags) != 0;
    }

    protected override int GetMakeAmount(UndeterminedItemInfo _itemInfo)
    {
        return _itemInfo.enteredItemList.Count;
    }
}

class ResinIncenseTechRecipe : TechRecipe
{
    public ResinIncenseTechRecipe()
    {
        InitData(6, 15, Tags.RESIN, Tags.OIL);
    }

    protected override bool IsContainsOtherTag(long _inputTags)
    {
        return (_inputTags - reqTags) != 0;
    }

    protected override int GetMakeAmount(UndeterminedItemInfo _itemInfo)
    {
        return _itemInfo.enteredItemList.Count;
    }
}

class GoldDustTechRecipe : TechRecipe // MOSSY_NAMING: GoldDust로 바꾸고 싶어요.
{
    public GoldDustTechRecipe()
    {
        InitData(-2147483648, 78, Tags.FOIL, Tags.METAL, Tags.HOLINESS, Tags.PROSPERITY, Tags.JOY, Tags.LOVE);
    }

    protected override bool IsPerfectlyMatch(long _inputTags)
    {
        return _inputTags == reqTags;
    }


    protected override bool CheckBanTag(long _inputTags)
    {

        long banTag = 1L << (int)Tags.STABILITY;

        return (banTag & _inputTags) == 0;
    }

    protected override int GetMakeAmount(UndeterminedItemInfo _itemInfo)
    {
        return _itemInfo.enteredItemList.Count;
    }


}

class CopperDustTechRecipe : TechRecipe // MOSSY_NAMING: CopperDust로 바꾸고 싶어요.
{
    public CopperDustTechRecipe()
    {
        InitData(-2147483648, 28, Tags.FOIL, Tags.METAL, Tags.STRENGTH, Tags.PURIFICATION);
    }

    protected override bool IsPerfectlyMatch(long _inputTags)
    {
        return _inputTags == reqTags;
    }


    protected override bool CheckBanTag(long _inputTags)
    {

        long banTag = 1L << (int)Tags.STABILITY;

        return (banTag & _inputTags) == 0;
    }

    protected override int GetMakeAmount(UndeterminedItemInfo _itemInfo)
    {
        return _itemInfo.enteredItemList.Count;
    }

}

class BronzeLeafTechRecipe : TechRecipe // MOSSY_NAMING: BronzeLeaf로 바꾸고 싶은데 적용된 곳을 전부 찾을 자신이 없습니다.
{
    public BronzeLeafTechRecipe()
    {
        InitData(29, 43, Tags.FOIL, Tags.METAL, Tags.STRENGTH, Tags.PURIFICATION);
    }

    protected override bool IsPerfectlyMatch(long _inputTags)
    {
        return _inputTags == reqTags;
    }


    protected override int GetMakeAmount(UndeterminedItemInfo _itemInfo)
    {
        return _itemInfo.enteredItemList.Count;
    }
}

class SacredFigurineWaxBurnerTechRecipe : TechRecipe
{
    public SacredFigurineWaxBurnerTechRecipe()
    {
        InitData(135, 135, Tags.HOLINESS, Tags.BEAUTY, Tags.PROTECTION, Tags.STABILITY, Tags.CHANGE, Tags.FORM_INCENSE_BURNER, Tags.FORM_SACRED_SCULPTURE);
    }

    protected override bool IsPerfectlyMatch(long _inputTags)
    {
        return _inputTags == reqTags;
    }

    protected override bool RequestTagCounts(UndeterminedItemInfo _itemInfo)
    {
        return _itemInfo.tagDic[Tags.BEAUTY] == 2;
    }



}

class DemonicFigurineWaxBurnerTechRecipe : TechRecipe
{
    public DemonicFigurineWaxBurnerTechRecipe()
    {
        InitData(134, 134, Tags.BLASPHEMY, Tags.BEAUTY, Tags.PROTECTION, Tags.STABILITY, Tags.CHANGE, Tags.FORM_INCENSE_BURNER, Tags.FORM_DEVILISH_SCULPTURE);
    }

    protected override bool IsPerfectlyMatch(long _inputTags)
    {
        return _inputTags == reqTags;
    }

    protected override bool RequestTagCounts(UndeterminedItemInfo _itemInfo)
    {
        return _itemInfo.tagDic[Tags.BEAUTY] == 2;
    }

}

class SacredFigurineCopperBurnerTechRecipe : TechRecipe
{
    public SacredFigurineCopperBurnerTechRecipe()
    {
        InitData(242, 280, Tags.HOLINESS, Tags.BEAUTY, Tags.PURIFICATION, Tags.STRENGTH, Tags.METAL, Tags.FORM_INCENSE_BURNER, Tags.FORM_SACRED_SCULPTURE);
    }
}

class DemonicFigurineCopperBurnerTechRecipe : TechRecipe
{
    public DemonicFigurineCopperBurnerTechRecipe()
    {
        InitData(241, 279, Tags.BLASPHEMY, Tags.BEAUTY, Tags.STRENGTH, Tags.PURIFICATION, Tags.METAL, Tags.FORM_INCENSE_BURNER, Tags.FORM_DEVILISH_SCULPTURE);
    }
}

class SacredCopperFigurineTechRecipe : TechRecipe
{
    public SacredCopperFigurineTechRecipe()
    {
        InitData(36, 63, Tags.HOLINESS, Tags.BEAUTY, Tags.STRENGTH, Tags.PURIFICATION, Tags.METAL, Tags.FORM_SACRED_SCULPTURE);
    }
}

class DemonicCopperFigurineTechRecipe : TechRecipe
{
    public DemonicCopperFigurineTechRecipe()
    {
        InitData(35, 62, Tags.BLASPHEMY, Tags.BEAUTY, Tags.STRENGTH, Tags.PURIFICATION, Tags.METAL, Tags.FORM_DEVILISH_SCULPTURE);
    }
}

class SacredFigurineGildedBurnerTechRecipe : TechRecipe
{
    public SacredFigurineGildedBurnerTechRecipe()
    {
        InitData(241, 299, Tags.HOLINESS, Tags.BEAUTY, Tags.STRENGTH, Tags.PURIFICATION, Tags.METAL, Tags.JOY, Tags.PROSPERITY, Tags.LOVE, Tags.FORM_INCENSE_BURNER, Tags.FORM_SACRED_SCULPTURE);
    }

    protected override bool RequestTagCounts(UndeterminedItemInfo _itemInfo)
    {
        return _itemInfo.tagDic[Tags.HOLINESS] == 3;
    }

}

class DemonicFigurineGildedBurnerTechRecipe : TechRecipe
{
    public DemonicFigurineGildedBurnerTechRecipe()
    {
        InitData(240, 278, Tags.BLASPHEMY, Tags.BEAUTY, Tags.STRENGTH, Tags.PURIFICATION, Tags.METAL, Tags.JOY, Tags.PROSPERITY, Tags.LOVE, Tags.FORM_INCENSE_BURNER, Tags.FORM_DEVILISH_SCULPTURE);
    }

}

class PurifiedSacredFigurineGildedBurnerTechRecipe : TechRecipe
{
    public PurifiedSacredFigurineGildedBurnerTechRecipe()
    {
        InitData(220, 278, Tags.HOLINESS, Tags.BEAUTY, Tags.STRENGTH, Tags.PURIFICATION, Tags.METAL, Tags.JOY, Tags.PROSPERITY, Tags.LOVE, Tags.INCENSED, Tags.FORM_INCENSE_BURNER, Tags.FORM_SACRED_SCULPTURE);
    }

    protected override bool RequestTagCounts(UndeterminedItemInfo _itemInfo)
    {
        return _itemInfo.tagDic[Tags.HOLINESS] == 3 && _itemInfo.tagDic[Tags.PURIFICATION] == 9 && _itemInfo.tagDic[Tags.STRENGTH] < 6;
    }

}

class AbhorrentDemonicFigurineGildedBurnerTechRecipe : TechRecipe
{
    public AbhorrentDemonicFigurineGildedBurnerTechRecipe()
    {
        InitData(219, 257, Tags.BEAUTY, Tags.BLASPHEMY, Tags.INCENSED, Tags.STRENGTH, Tags.METAL, Tags.PURIFICATION, Tags.JOY, Tags.PROSPERITY, Tags.LOVE, Tags.HATRED, Tags.FORM_INCENSE_BURNER, Tags.FORM_DEVILISH_SCULPTURE);
    }

    protected override bool RequestTagCounts(UndeterminedItemInfo _itemInfo)
    {
        return _itemInfo.tagDic[Tags.HATRED] == 5;
    }
}

class HealingCopperBurnerTechRecipe : TechRecipe
{
    public HealingCopperBurnerTechRecipe()
    {
        InitData(225, 236, Tags.INCENSED, Tags.HOLINESS, Tags.PURIFICATION, Tags.METAL, Tags.STRENGTH, Tags.OIL, Tags.RECOVERY, Tags.FORM_INCENSE_BURNER);
    }


}

class StrangeMetalFilmTechRecipe : TechRecipe
{
    public StrangeMetalFilmTechRecipe()
    {
        InitData(1, 96, Tags.FOIL, Tags.METAL);
    }

    protected override bool CheckBanTag(long _inputTags)
    {

        long banTag = 1L << (int)Tags.STABILITY;

/*        Debug.Log((banTag & _inputTags) == 0);

        Debug.Log(System.Convert.ToString(banTag, 2));
        Debug.Log(System.Convert.ToString(_inputTags, 2));*/

        return (banTag & _inputTags) == 0;
    }

    protected override List<Tags> IsUniqueTagRecipe()
    {
        List<Tags> uniqueTags = new List<Tags>();
        uniqueTags.Add(Tags.FOIL);
        return uniqueTags;
    }

    protected override int GetMakeAmount(UndeterminedItemInfo _itemInfo)
    {
        return _itemInfo.enteredItemList.Count;
    }
}

class UnknownShavingsTechRecipe : TechRecipe
{
    public UnknownShavingsTechRecipe()
    {
        InitData(-2147483648, 0, Tags.FOIL, Tags.METAL);
    }

    protected override bool CheckBanTag(long _inputTags)
    {

        long banTag = 1L << (int)Tags.STABILITY;

        return (banTag & _inputTags) == 0;
    }

    protected override List<Tags> IsUniqueTagRecipe()
    {
        List<Tags> uniqueTags = new List<Tags>();
        uniqueTags.Add(Tags.FOIL);
        return uniqueTags;
    }

    protected override int GetMakeAmount(UndeterminedItemInfo _itemInfo)
    {
        return _itemInfo.enteredItemList.Count;
    }
}

class LeadInlayTechRecipe : TechRecipe
{
    public LeadInlayTechRecipe()
    {
        InitData(82, 96, Tags.FOIL, Tags.METAL, Tags.HATRED, Tags.PROTECTION, Tags.CHANGE);
    }

    protected override bool IsPerfectlyMatch(long _inputTags)
    {
        return _inputTags == reqTags;
    }

    protected override int GetMakeAmount(UndeterminedItemInfo _itemInfo)
    {
        return _itemInfo.enteredItemList.Count;
    }
}

class SilverInlayTechRecipe : TechRecipe
{
    public SilverInlayTechRecipe()
    {
        InitData(47, 61, Tags.FOIL, Tags.METAL, Tags.INSANITY, Tags.TRANQUILITY, Tags.PURIFICATION);
    }

    protected override bool IsPerfectlyMatch(long _inputTags)
    {
        return _inputTags == reqTags;
    }

    protected override int GetMakeAmount(UndeterminedItemInfo _itemInfo)
    {
        return _itemInfo.enteredItemList.Count;
    }
}

class LeadShavingsTechRecipe : TechRecipe
{
    public LeadShavingsTechRecipe()
    {
        InitData(-2147483648, 81, Tags.FOIL, Tags.METAL, Tags.HATRED, Tags.PROTECTION, Tags.CHANGE);
    }

    protected override bool IsPerfectlyMatch(long _inputTags)
    {
        return _inputTags == reqTags;
    }

    protected override bool CheckBanTag(long _inputTags)
    {

        long banTag = 1L << (int)Tags.STABILITY;

        return (banTag & _inputTags) == 0;
    }

    protected override int GetMakeAmount(UndeterminedItemInfo _itemInfo)
    {
        return _itemInfo.enteredItemList.Count;
    }

}

class SilverShavingsTechRecipe : TechRecipe
{
    public SilverShavingsTechRecipe()
    {
        InitData(int.MinValue, 46, Tags.TRANQUILITY, Tags.INSANITY, Tags.PURIFICATION, Tags.FOIL, Tags.METAL);
    }

    protected override bool IsPerfectlyMatch(long _inputTags)
    {
        return _inputTags == reqTags;
    }

    protected override bool CheckBanTag(long _inputTags)
    {

        long banTag = 1L << (int)Tags.STABILITY;

        return (banTag & _inputTags) == 0;
    }

    protected override int GetMakeAmount(UndeterminedItemInfo _itemInfo)
    {
        return _itemInfo.enteredItemList.Count;
    }

}

class SacredFigurineSilverBurnerTechRecipe : TechRecipe
{
    public SacredFigurineSilverBurnerTechRecipe()
    {
        InitData(296, 363, Tags.HOLINESS, Tags.PURIFICATION, Tags.INSANITY, Tags.TRANQUILITY, Tags.METAL, Tags.BEAUTY, Tags.FORM_INCENSE_BURNER, Tags.FORM_SACRED_SCULPTURE);
    }

}

class SacredFigurinePureGoldBurnerTechRecipe : TechRecipe
{
    public SacredFigurinePureGoldBurnerTechRecipe()
    {
        InitData(392, 491, Tags.HOLINESS, Tags.PROSPERITY, Tags.JOY, Tags.LOVE, Tags.METAL, Tags.BEAUTY, Tags.FORM_INCENSE_BURNER, Tags.FORM_SACRED_SCULPTURE);
    }

    protected override bool RequestTagCounts(UndeterminedItemInfo _itemInfo)
    {
        return _itemInfo.tagDic[Tags.HOLINESS] == 2;
    }

}

class DemonicFigurineSilverBurnerTechRecipe : TechRecipe
{
    public DemonicFigurineSilverBurnerTechRecipe()
    {
        InitData(392, 491, Tags.BLASPHEMY, Tags.PURIFICATION, Tags.INSANITY, Tags.TRANQUILITY, Tags.METAL, Tags.BEAUTY, Tags.FORM_INCENSE_BURNER, Tags.FORM_DEVILISH_SCULPTURE);
    }


}

class DemonicFigurinePureGoldBurnerTechRecipe : TechRecipe
{
    public DemonicFigurinePureGoldBurnerTechRecipe()
    {
        InitData(391, 490, Tags.BLASPHEMY, Tags.PROSPERITY, Tags.JOY, Tags.LOVE, Tags.METAL, Tags.BEAUTY, Tags.FORM_INCENSE_BURNER, Tags.FORM_DEVILISH_SCULPTURE);
    }
}

class IncensedSacredFigurineCopperBurnerTechRecipe : TechRecipe
{
    public IncensedSacredFigurineCopperBurnerTechRecipe()
    {
        InitData(241, 295, Tags.HOLINESS, Tags.BEAUTY, Tags.STRENGTH, Tags.PURIFICATION, Tags.METAL, Tags.INCENSED, Tags.FORM_INCENSE_BURNER, Tags.FORM_SACRED_SCULPTURE);
    }

}

class IncensedSacredFigurineSilverBurnerTechRecipe : TechRecipe
{
    public IncensedSacredFigurineSilverBurnerTechRecipe()
    {
        InitData(297, 378, Tags.HOLINESS, Tags.BEAUTY, Tags.TRANQUILITY, Tags.PURIFICATION, Tags.INSANITY, Tags.INCENSED, Tags.FORM_INCENSE_BURNER, Tags.FORM_SACRED_SCULPTURE);
    }
}

class IncensedSacredFigurineGildedBurnerTechRecipe : TechRecipe
{
    public IncensedSacredFigurineGildedBurnerTechRecipe()
    {
        InitData(242, 314, Tags.HOLINESS, Tags.BEAUTY, Tags.STRENGTH, Tags.PURIFICATION, Tags.METAL, Tags.JOY, Tags.PROSPERITY, Tags.LOVE, Tags.INCENSED, Tags.FORM_INCENSE_BURNER, Tags.FORM_SACRED_SCULPTURE);
    }

    protected override bool RequestTagCounts(UndeterminedItemInfo _itemInfo)
    {
        return _itemInfo.tagDic[Tags.HOLINESS] == 2;
    }

}

class IncensedDemonicFigurineCopperBurnerTechRecipe : TechRecipe
{
    public IncensedDemonicFigurineCopperBurnerTechRecipe()
    {
        InitData(240, 294, Tags.BLASPHEMY, Tags.BEAUTY, Tags.STRENGTH, Tags.PURIFICATION, Tags.METAL, Tags.INCENSED, Tags.FORM_INCENSE_BURNER, Tags.FORM_DEVILISH_SCULPTURE);
    }
}

class IncensedDemonicFigurineSilverBurnerTechRecipe : TechRecipe
{
    public IncensedDemonicFigurineSilverBurnerTechRecipe()
    {
        InitData(294, 361, Tags.BLASPHEMY, Tags.BEAUTY, Tags.TRANQUILITY, Tags.PURIFICATION, Tags.INSANITY, Tags.INCENSED, Tags.FORM_INCENSE_BURNER, Tags.FORM_DEVILISH_SCULPTURE);
    }
}

class IncensedSacredFigurinePureGoldBurnerTechRecipe : TechRecipe
{
    public IncensedSacredFigurinePureGoldBurnerTechRecipe()
    {
        InitData(393, 506, Tags.HOLINESS, Tags.BEAUTY, Tags.STRENGTH, Tags.PURIFICATION, Tags.METAL, Tags.JOY, Tags.PROSPERITY, Tags.LOVE, Tags.INCENSED, Tags.FORM_INCENSE_BURNER, Tags.FORM_SACRED_SCULPTURE);
    }

    protected override bool RequestTagCounts(UndeterminedItemInfo _itemInfo)
    {
        return _itemInfo.tagDic[Tags.HOLINESS] == 2;
    }
}

class IncensedDemonicFigurinePureGoldBurnerTechRecipe : TechRecipe
{
    public IncensedDemonicFigurinePureGoldBurnerTechRecipe()
    {
        InitData(392, 505, Tags.BEAUTY, Tags.INCENSED, Tags.STRENGTH, Tags.METAL, Tags.STABILITY, Tags.JOY, Tags.PROSPERITY, Tags.LOVE, Tags.BLASPHEMY, Tags.FORM_INCENSE_BURNER, Tags.FORM_DEVILISH_SCULPTURE);
    }

    protected override bool RequestTagCounts(UndeterminedItemInfo _itemInfo)
    {
        return _itemInfo.tagDic[Tags.HOLINESS] == 2;
    }


}

class IncensedDemonicFigurineGildedBurnerTechRecipe : TechRecipe
{
    public IncensedDemonicFigurineGildedBurnerTechRecipe()
    {
        InitData(241, 313, Tags.BEAUTY, Tags.INCENSED, Tags.STRENGTH, Tags.METAL, Tags.PURIFICATION, Tags.JOY, Tags.PROSPERITY, Tags.LOVE, Tags.BLASPHEMY, Tags.FORM_INCENSE_BURNER, Tags.FORM_DEVILISH_SCULPTURE);
    }
}

class SacredGoldFigurineTechRecipe : TechRecipe
{
    public SacredGoldFigurineTechRecipe()
    {
        InitData(86, 113, Tags.HOLINESS, Tags.BEAUTY, Tags.JOY, Tags.PROSPERITY, Tags.LOVE, Tags.METAL, Tags.FORM_SACRED_SCULPTURE);
    }
}

class SacredSilverFigurineTechRecipe : TechRecipe
{
    public SacredSilverFigurineTechRecipe()
    {
        InitData(54, 81, Tags.HOLINESS, Tags.BEAUTY, Tags.TRANQUILITY, Tags.INSANITY, Tags.METAL, Tags.PURIFICATION, Tags.FORM_SACRED_SCULPTURE);
    }
}

class AlloyBurnerTechRecipe : TechRecipe
{
    public AlloyBurnerTechRecipe()
    {
        InitData(134, 531, Tags.METAL, Tags.BEAUTY, Tags.FORM_INCENSE_BURNER);
    }

    protected override bool RequestTagCounts(UndeterminedItemInfo _itemInfo)
    {
        return _itemInfo.tagDic[Tags.METAL] == 3;
    }

    protected override bool IsUniqueRecipe()
    {
        return true;
    }
}

class UnknownMetalTechRecipe : TechRecipe
{
    public UnknownMetalTechRecipe()
    {
        InitData(1, 130, Tags.METAL);
    }

    protected override bool CheckBanTag(long _inputTags)
    {

        long banTag = 1L << (int)Tags.FOIL;

        return (banTag & _inputTags) == 0;
    }

    protected override List<Tags> IsUniqueTagRecipe()
    {
        List<Tags> uniqueTags = new List<Tags>();
        uniqueTags.Add(Tags.METAL);
        return uniqueTags;
    }

}

class SacredAlloyFigurineBurnerTechRecipe : TechRecipe
{
    public SacredAlloyFigurineBurnerTechRecipe()
    {
        InitData(144, 695, Tags.METAL, Tags.BEAUTY, Tags.FORM_INCENSE_BURNER, Tags.FORM_SACRED_SCULPTURE);
    }

    protected override List<Tags> IsUniqueTagRecipe()
    {
        List<Tags> uniqueTags = new List<Tags>();
        uniqueTags.Add(Tags.METAL);
        uniqueTags.Add(Tags.FORM_SACRED_SCULPTURE);
        uniqueTags.Add(Tags.FORM_INCENSE_BURNER);
        return uniqueTags;
    }

}

class SacredAlloyFigurineTechRecipe : TechRecipe
{
    public SacredAlloyFigurineTechRecipe()
    {
        InitData(8, 164, Tags.FORM_SACRED_SCULPTURE, Tags.BEAUTY, Tags.METAL);
    }

    protected override bool RequestTagCounts(UndeterminedItemInfo _itemInfo)
    {
        return _itemInfo.tagDic[Tags.METAL] == 3;
    }

    protected override bool IsUniqueRecipe()
    {
        return true;
    }

}

class DemonicAlloyFigurineTechRecipe : TechRecipe
{
    public DemonicAlloyFigurineTechRecipe()
    {
        InitData(7, 163, Tags.FORM_DEVILISH_SCULPTURE, Tags.BEAUTY, Tags.METAL);
    }

    protected override bool RequestTagCounts(UndeterminedItemInfo _itemInfo)
    {
        return _itemInfo.tagDic[Tags.METAL] == 3;
    }

    protected override bool IsUniqueRecipe()
    {
        return true;
    }
}

class DemonicAlloyFigurineBurnerTechRecipe : TechRecipe
{
    public DemonicAlloyFigurineBurnerTechRecipe()
    {
        InitData(143, 694, Tags.FORM_INCENSE_BURNER, Tags.FORM_DEVILISH_SCULPTURE, Tags.METAL, Tags.BEAUTY);
    }

    protected override List<Tags> IsUniqueTagRecipe()
    {
        List<Tags> uniqueTags = new List<Tags>();
        uniqueTags.Add(Tags.METAL);
        uniqueTags.Add(Tags.FORM_SACRED_SCULPTURE);
        uniqueTags.Add(Tags.FORM_INCENSE_BURNER);
        return uniqueTags;
    }
}

class IncensedSacredAlloyFigurineBurnerTechRecipe : TechRecipe
{
    public IncensedSacredAlloyFigurineBurnerTechRecipe()
    {
        InitData(145, 710, Tags.FORM_INCENSE_BURNER, Tags.FORM_SACRED_SCULPTURE, Tags.METAL, Tags.BEAUTY, Tags.INCENSED);
    }

    protected override List<Tags> IsUniqueTagRecipe()
    {
        List<Tags> uniqueTags = new List<Tags>();
        uniqueTags.Add(Tags.METAL);
        uniqueTags.Add(Tags.FORM_SACRED_SCULPTURE);
        uniqueTags.Add(Tags.FORM_INCENSE_BURNER);
        return uniqueTags;
    }
}

class IncensedDemonicAlloyFigurineBurnerTechRecipe : TechRecipe
{
    public IncensedDemonicAlloyFigurineBurnerTechRecipe()
    {

        InitData(144, 709, Tags.FORM_INCENSE_BURNER, Tags.FORM_DEVILISH_SCULPTURE, Tags.METAL, Tags.BEAUTY, Tags.INCENSED);

    }

    protected override List<Tags> IsUniqueTagRecipe()
    {
        List<Tags> uniqueTags = new List<Tags>();
        uniqueTags.Add(Tags.METAL);
        uniqueTags.Add(Tags.FORM_SACRED_SCULPTURE);
        uniqueTags.Add(Tags.FORM_INCENSE_BURNER);
        return uniqueTags;
    }

}


