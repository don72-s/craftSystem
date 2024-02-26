using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class ItemDataTable {

    private static void InitItemDataInfo()
    {

        #region 기본 데이터 정보 입력 영역

        AddIBaseItemData(ItemID._99999_ERROR_OCCURRED, "에러코드", "미씽노같은건가? 아무튼 있어서는 안되는 존재");

        AddIBaseItemData((ItemID)1, "때죽나무 수지", "벤조인이라고도 불리는 때죽나무의 수지는 달콤한 바닐라 비슷한 향이 난다.");
        AddIBaseItemData((ItemID)2, "몰약 수지", "몰약나무의 껍질에 상처를 내어 흐르는 유액을 건조시켜 만든 수지. 고대부터 종교 의식에서 사용해 왔다.");
        AddIBaseItemData((ItemID)3, "유향 수지", "프랑킨센스라고도 불리는, 유향나무의 수지는 독특하지만 깨끗하고 신선한 향을 지니고 있다. 고대부터 종교 의식에 사용해 왔다.");
        AddIBaseItemData((ItemID)4, "시더우드 가루", "개잎갈나무를 말려서 빻아 만든 가루. 시더우드라고도 부른다. 연필심 냄새가 난다.");
        AddIBaseItemData((ItemID)5, "샌달우드 가루", "백단향나무를 말려서 빻아 만든 가루. 샌달우드라고도 부르는데, 다 자라는데 60년 이상 걸린다고 한다. 부드럽고 그윽한 향이 난다.");
        AddIBaseItemData((ItemID)6, "라벤더 꽃봉오리", "고대부터 사용해 온 허브의 대명사. 그 옅은 보라빛은 심신을 안정시키며, 은은한 향기는 마음을 정화한다.");
        AddIBaseItemData((ItemID)7, "로즈마리 잎", "요리에 활용되는 로즈마리는 향긋한 맛을 선사하며, 그 언제나 상쾌한 향은 마음을 싱그럽게 한다.");
        AddIBaseItemData((ItemID)8, "사이프러스 잎", "어쩌면 백삼사십 년 뒤 쯤에 사이프러스 나무와 별을 그린 유명한 그림이 한 장 정도 나올 수도 있지 않을까?");
        AddIBaseItemData((ItemID)9, "스파이크너드 뿌리", "스파이크너드, 혹은 머스크루트라고도 불리는 감송나무는 먼 고대부터 향수나 향료, 의료 용도로 사용되곤 했다.");
        AddIBaseItemData((ItemID)10, "장미꽃", "아저씨는 거짓말쟁이에요.꽃들은 연약하고 순수해요.가시가 있으면 무섭게 보인다고 생각하는 거예요.");
        AddIBaseItemData((ItemID)11, "시더우드 오일", "시더우드 에센셜 오일. 연필심 냄새가 난다. 왠지 마음이 편안해지는 냄새다.");
        AddIBaseItemData((ItemID)12, "샌달우드 오일", "샌달우드 에센셜 오일. 부드러운 나무 향이 난다. 냄새를 맡고 있으면 마음이 평화로워진다.");
        AddIBaseItemData((ItemID)13, "라벤더 오일", "라벤더 에센셜 오일. 라벤더의 은은하고 향긋한 향이 난다. 오일은 보랏빛이 나진 않는다.");
        AddIBaseItemData((ItemID)14, "로즈마리 오일", "로즈마리 에센셜 오일. 로즈마리의 상쾌한 향이 퍼진다. 왠지 맛있을 거 같지만 먹지 말자.");
        AddIBaseItemData((ItemID)15, "사이프러스 오일", "사이프러스 에센셜 오일. 노란색의 강한 스모키한 향을 맡으면 가슴이 상쾌해진다.");
        AddIBaseItemData((ItemID)16, "스파이크너드 오일", "스파이크너드를 말리고 갈아 추출한 에센셜 오일. 녹갈색이며 짙은 나무 냄새가 난다.");
        AddIBaseItemData((ItemID)17, "장미 오일", "장미로 만든 진한 오렌지색의 에센셜 오일. 향? 깊고, 달콤하고, 스파이시.");
        AddIBaseItemData((ItemID)18, "믹스 오일", "이것저것 추출해 섞은 에센셜 오일. 엎질러진 물을 다시 담을 수 없듯, 한 번 섞으면 되돌리기 힘드니 신중해야 한다네.");
        AddIBaseItemData((ItemID)19, "금 조각", "금, 태양신의 분신이자 인간의 모든 욕망의 상징, 기쁨, 환희, 그리고 그 무엇보다 중요한 것은, 엄청 비싸다는 것.");
        AddIBaseItemData((ItemID)20, "구리 조각", "사람들은 나를 금은동의 말석이라 무시하지만, 그들이 모르는 것이 있지. 금, 은, 동까지 엄연히 귀금속이라는 것 말야.");
        AddIBaseItemData((ItemID)21, "수은", "상온에서 액체인 유일한 금속이자 변화, 변혁, 반응성의 상징. 물론 맹독성 죽음도 포함해서.");
        AddIBaseItemData((ItemID)22, "왁스", "말랑말랑하니 쪼물딱거려서 갖고 놀기 좋습니다. 손에 묻는 건 뭐 알아서 하시고요.");
        AddIBaseItemData((ItemID)23, "석고", "아그리파, 줄리앙, 비너스, 그 영광의 이름들이 이 속에 잠겨 있거든.");
        AddIBaseItemData((ItemID)24, "물", "푸르고 투명하며 생명의 근원이자 주변에 널렸으면서도 깨끗한 걸 구하기는 의외로 힘든 것.");
        AddIBaseItemData((ItemID)25, "고무", "있잖아, 만약 내 손발이 고무처럼 늘어나서 새총처럼 쏠 수 있다면 진짜 재밌지 않을까?");
        AddIBaseItemData((ItemID)26, "향로의 설계도", "분향 예식에 사용하는 향로를 만들기 위한 설계도. 형태와 치수가 꽤 자세히 적혀 있다. 사용해도 사라지지 않는다.",false);
        AddIBaseItemData((ItemID)27, "성스러운 조각의 설계도", "성스러운 조각의 설계도. 어디를 어떻게 조각할지 적혀 있다. 경전에 나오는 장면을 묘사했다. 사용해도 사라지지 않는다.", false);
        AddIBaseItemData((ItemID)28, "악마 조각의 설계도", "악마를 묘사한 조각의 설계도. 보고 있노라면 기분이 언짢아진다. 사용해도 사라지지 않는다.", false);
        AddIBaseItemData((ItemID)29, "은 조각", "금이 태양이라면, 은은 달빛을 상징해요. 고요한 보름달이 뜨면 누군가는 광기에 빠지고 또 누군가는 그걸 정화하기도 합니다.");
        AddIBaseItemData((ItemID)30, "납 조각", "아이템 설명");
        AddIBaseItemData((ItemID)31, "철 조각", "아이템 설명");
        AddIBaseItemData((ItemID)32, "아말감", "아이템 설명");
        AddIBaseItemData((ItemID)33, "금박", "아이템 설명");
        AddIBaseItemData((ItemID)34, "왁스 향로", "아이템 설명");
        AddIBaseItemData((ItemID)35, "구리 향로", "아이템 설명");
        AddIBaseItemData((ItemID)36, "은 향로", "아이템 설명");
        AddIBaseItemData((ItemID)37, "순금 향로", "아이템 설명");
        AddIBaseItemData((ItemID)38, "성스러운 왁스 조각상", "아이템 설명");
        AddIBaseItemData((ItemID)39, "악마의 왁스 조각상", "아이템 설명");
        AddIBaseItemData((ItemID)40, "금동 향로", "아이템 설명");
        AddIBaseItemData((ItemID)41, "레진 믹스", "아이템 설명");
        AddIBaseItemData((ItemID)42, "레진 인센스", "아이템 설명");
        AddIBaseItemData((ItemID)43, "금 부스러기", "아이템 설명");
        AddIBaseItemData((ItemID)44, "구리 부스러기", "아이템 설명");
        AddIBaseItemData((ItemID)45, "동박", "아이템 설명");
        AddIBaseItemData((ItemID)46, "성스러운 조각 왁스 향로", "아이템 설명");
        AddIBaseItemData((ItemID)47, "악마의 조각 왁스 향로", "아이템 설명");
        AddIBaseItemData((ItemID)48, "성스러운 조각 구리 향로", "아이템 설명");
        AddIBaseItemData((ItemID)49, "악마의 조각 구리 향로", "아이템 설명");
        AddIBaseItemData((ItemID)50, "성스러운 구리 조각상", "아이템 설명");
        AddIBaseItemData((ItemID)51, "악마의 구리 조각상", "아이템 설명");
        AddIBaseItemData((ItemID)52, "성스러운 조각 금동 향로", "아이템 설명");
        AddIBaseItemData((ItemID)53, "악마의 조각 금동 향로", "아이템 설명");
        AddIBaseItemData((ItemID)54, "정화의 성스러운 조각 금동 향로", "아이템 설명");
        AddIBaseItemData((ItemID)55, "혐오하는 악마의 조각 금동 향로", "아이템 설명");
        AddIBaseItemData((ItemID)56, "회복의 구리 향로", "아이템 설명");
        AddIBaseItemData((ItemID)57, "알 수 없는 무언가", "괜찮아, 실패는 성공의 어머니라는 말도 있잖아. 지금의 실패는 쓰라리지만, 언젠가 이걸 양분 삼아 날아오를 날이 올 거야…");
        AddIBaseItemData((ItemID)58, "이상한 금속 박막", "아이템 설명");
        AddIBaseItemData((ItemID)59, "알 수 없는 부스러기", "아이템 설명");
        AddIBaseItemData((ItemID)60, "납박", "아이템 설명");
        AddIBaseItemData((ItemID)61, "은박", "아이템 설명");
        AddIBaseItemData((ItemID)62, "납 부스러기", "아이템 설명");
        AddIBaseItemData((ItemID)63, "은 부스러기", "아이템 설명");
        AddIBaseItemData((ItemID)64, "성스러운 조각 은 향로", "아이템 설명");
        AddIBaseItemData((ItemID)65, "성스러운 조각 순금 향로", "아이템 설명");
        AddIBaseItemData((ItemID)66, "악마의 조각 은 향로", "아이템 설명");
        AddIBaseItemData((ItemID)67, "악마의 조각 순금 향로", "아이템 설명");
        AddIBaseItemData((ItemID)68, "분향된 성스러운 조각 구리 향로", "아이템 설명");
        AddIBaseItemData((ItemID)69, "분향된 성스러운 조각 은 향로", "아이템 설명");
        AddIBaseItemData((ItemID)70, "분향된 성스러운 조각 금동 향로", "아이템 설명");
        AddIBaseItemData((ItemID)71, "분향된 악마의 조각 구리 향로", "아이템 설명");
        AddIBaseItemData((ItemID)72, "분향된 악마의 조각 은 향로", "아이템 설명");
        AddIBaseItemData((ItemID)73, "분향된 성스러운 조각 순금 향로", "아이템 설명");
        AddIBaseItemData((ItemID)74, "분향된 악마의 조각 순금 향로", "아이템 설명");
        AddIBaseItemData((ItemID)75, "분향된 악마의 조각 금동 향로", "아이템 설명");
        AddIBaseItemData((ItemID)76, "성스러운 금 조각상", "아이템 설명");
        AddIBaseItemData((ItemID)77, "성스러운 은 조각상", "아이템 설명");
        AddIBaseItemData((ItemID)78, "합금 향로", "아이템 설명");
        AddIBaseItemData((ItemID)79, "알 수 없는 금속", "아이템 설명");
        AddIBaseItemData((ItemID)80, "성스러운 조각 합금 향로", "아이템 설명");
        AddIBaseItemData((ItemID)81, "성스러운 합금 조각상", "아이템 설명");
        AddIBaseItemData((ItemID)82, "악마의 합금 조각상", "아이템 설명");
        AddIBaseItemData((ItemID)83, "악마의 조각 합금 향로", "아이템 설명");
        AddIBaseItemData((ItemID)84, "분향된 성스러운 조각 합금 향로", "아이템 설명");
        AddIBaseItemData((ItemID)85, "분향된 악마의 조각 합금 향로", "아이템 설명");

        #endregion

    }
    private static void AddIBaseItemData(ItemID _id, string _name, string _description, bool _destroyOnCraft = true) {

        if (!baseItemDataDictionary.ContainsKey(_id))
        {
            baseItemDataDictionary.Add(_id, new BaseItemData(_id, _name, _description, _destroyOnCraft));
        }
        else {
            Debug.LogError("중복된 아이템 테이블 재초기화");
        }

    }








    private static void InitConstProvideInfo()
    {

        #region 시스템 제공으로 고정시킬 추가 아이템 정보

        AddProvidedItemData(ItemID._99999_ERROR_OCCURRED, -999, Tags.ERRORED);

        AddProvidedItemData((ItemID)1, 2, Tags.PROSPERITY, Tags.RESIN);
        AddProvidedItemData((ItemID)2, 3, Tags.CHANGE, Tags.PROTECTION, Tags.RESIN);
        AddProvidedItemData((ItemID)3, 5, Tags.RESIN, Tags.PURIFICATION);
        AddProvidedItemData((ItemID)4, 2, Tags.STABILITY, Tags.HERB, Tags.STRENGTH);
        AddProvidedItemData((ItemID)5, 2, Tags.PEACE, Tags.HERB, Tags.DEVOTION);
        AddProvidedItemData((ItemID)6, 1, Tags.PURITY, Tags.HERB, Tags.RECOVERY);
        AddProvidedItemData((ItemID)7, 1, Tags.TRANQUILITY, Tags.HERB);
        AddProvidedItemData((ItemID)8, 1, Tags.CHANGE, Tags.PATIENCE, Tags.HERB);
        AddProvidedItemData((ItemID)9, 3, Tags.JOY, Tags.HERB);
        AddProvidedItemData((ItemID)10, 5, Tags.LOVE, Tags.PURITY, Tags.HERB);

        AddProvidedItemData((ItemID)19, 79, Tags.METAL, Tags.JOY, Tags.PROSPERITY, Tags.LOVE, Tags.HOLINESS); // MOSSY_CHANGED: UNSTRUCTURED(비정형) 태그 삭제
        AddProvidedItemData((ItemID)20, 29, Tags.METAL, Tags.PURIFICATION, Tags.STRENGTH); // MOSSY_CHANGED: UNSTRUCTURED(비정형) 태그 삭제
        AddProvidedItemData((ItemID)21, 80, Tags.METAL, Tags.CHANGE, Tags.INSTABILITY, Tags.UNSTRUCTURED, Tags.FLUIDITY, Tags.DEATH, Tags.MERCURY); // MOSSY_CHANGED: MERCURY(수은) 태그 추가
        AddProvidedItemData((ItemID)22, 7, Tags.CHANGE, Tags.PROTECTION, Tags.UNSTRUCTURED, Tags.STABILITY);
        AddProvidedItemData((ItemID)23, 20, Tags.UNSTRUCTURED, Tags.STABILITY);
        AddProvidedItemData((ItemID)24, 7, Tags.UNSTRUCTURED, Tags.LIFE, Tags.FLUIDITY);
        AddProvidedItemData((ItemID)25, 14, Tags.CHANGE, Tags.UNSTRUCTURED);
        AddProvidedItemData((ItemID)26, 110, Tags.BLUEPRINT, Tags.HOLINESS, Tags.FORM_INCENSE_BURNER);
        AddProvidedItemData((ItemID)27, 7, Tags.BLUEPRINT, Tags.HOLINESS, Tags.FORM_SACRED_SCULPTURE);
        AddProvidedItemData((ItemID)28, 6, Tags.BLASPHEMY, Tags.BLUEPRINT, Tags.FORM_DEVILISH_SCULPTURE);
        AddProvidedItemData((ItemID)29, 47, Tags.TRANQUILITY, Tags.INSTABILITY, Tags.METAL, Tags.PURIFICATION); // MOSSY_CHANGED: UNSTRUCTURED(비정형) 태그 삭제
        AddProvidedItemData((ItemID)30, 82, Tags.METAL, Tags.CHANGE, Tags.PROTECTION, Tags.HATRED); // MOSSY_CHANGED: UNSTRUCTURED(비정형) 태그 삭제
        AddProvidedItemData((ItemID)31, 26, Tags.METAL, Tags.JUSTICE, Tags.STRENGTH); // MOSSY_CHANGED: UNSTRUCTURED(비정형) 태그 삭제


        #endregion

    }
    /// <summary>
    /// 태그의 갯수를 지정할때.
    /// </summary>
    /// <param name="_itemID">아이템 ID</param>
    /// <param name="_quality">아이템 품질</param>
    /// <param name="_tags">태그 목록, 갯수</param>
    private static void AddProvidedItemData(ItemID _id, int _quality, params (Tags, int)[] _tags) {

        if (!providedItemDataDictonary.ContainsKey(_id))
        {
            providedItemDataDictonary.Add(_id, new ProvidedItemData(_id, _quality, _tags));
        }
        else
        {
            Debug.LogError("중복된 아이템 테이블 재초기화");
        }

    }
    /// <summary>
    /// 태그의 갯수가 1로 고정될때.
    /// </summary>
    /// <param name="_itemID">아이템 ID</param>
    /// <param name="_quality">아이템 품질</param>
    /// <param name="_tags">태그 목록</param>
    private static void AddProvidedItemData(ItemID _id, int _quality, params Tags[] _tags)
    {

        if (!providedItemDataDictonary.ContainsKey(_id))
        {
            providedItemDataDictonary.Add(_id, new ProvidedItemData(_id, _quality, _tags));
        }
        else
        {
            Debug.LogError("중복된 아이템 테이블 재초기화");
        }

    }
}

