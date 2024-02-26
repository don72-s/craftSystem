using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// <para>번영 - Prosperity - 0 시작</para>
/// <para>수지 - Resin</para>
/// <para>힘 - Strength</para>
/// <para>안정 - Stability</para>
/// <para>오일 - Oil</para>
/// <para>헌신 - Devotion</para>
/// <para>평화 - Peace</para>
/// <para>회복 - Recovery</para>
/// <para>순수 - Purity</para>
/// <para>고요 - Tranquility</para>
/// <para>인내 - Patience</para>
/// <para>변화 - Change</para>
/// <para>기쁨 - Joy</para>
/// <para>사랑 - Love</para>
/// <para>금속 - Metal</para>
/// <para>불안정 - Instability</para>
/// <para>죽음 - Death</para>
/// <para>성스러움 - Holiness</para>
/// <para>불경함 - Blasphemy</para>
/// <para>아름다움 - Beauty</para>
/// <para>박막 - Foil</para>
/// <para>보호 - Protection</para>
/// <para>정화 - Purification</para>
/// <para>광기 - Insanity</para>
/// <para>유동 - Fluidity</para>
/// <para>분향 - Incensed</para>
/// <para>혐오 - Hatred</para>
/// <para>형상:향로 - Form: Incense burner</para>
/// <para>형상:성스러운조각 - Form: Sacred sculpture</para>
/// <para>형상:악마조각 - Form: Devilish sculpture</para>
/// </summary>
public enum Tags {
    /// <summary>
    /// 번영
    /// </summary>
    PROSPERITY,
    /// <summary>
    /// 수지
    /// </summary>
    RESIN,
    /// <summary>
    /// 힘
    /// </summary>
    STRENGTH,
    /// <summary>
    /// 안정
    /// </summary>
    STABILITY,
    /// <summary>
    /// 오일
    /// </summary>
    OIL,
    /// <summary>
    /// 헌신
    /// </summary>
    DEVOTION,
    /// <summary>
    /// 평화
    /// </summary>
    PEACE,
    /// <summary>
    /// 회복
    /// </summary>
    RECOVERY,
    /// <summary>
    /// 순수
    /// </summary>
    PURITY,
    /// <summary>
    /// 고요
    /// </summary>
    TRANQUILITY,
    /// <summary>
    /// 인내
    /// </summary>
    PATIENCE,
    /// <summary>
    /// 변화
    /// </summary>
    CHANGE,
    /// <summary>
    /// 기쁨
    /// </summary>
    JOY,
    /// <summary>
    /// 사랑
    /// </summary>
    LOVE,
    /// <summary>
    /// 금속
    /// </summary>
    METAL,
    /// <summary>
    /// 불안정
    /// </summary>
    INSTABILITY,
    /// <summary>
    /// 죽음
    /// </summary>
    DEATH,
    /// <summary>
    /// 성스러움
    /// </summary>
    HOLINESS,
    /// <summary>
    /// 불경함
    /// </summary>
    BLASPHEMY,
    /// <summary>
    /// 아름다움
    /// </summary>
    BEAUTY,
    /// <summary>
    /// 박막
    /// </summary>
    FOIL,
    /// <summary>
    /// 보호
    /// </summary>
    PROTECTION,
    /// <summary>
    /// 정화
    /// </summary>
    PURIFICATION,
    /// <summary>
    /// 광기
    /// </summary>
    INSANITY,
    /// <summary>
    /// 유동
    /// </summary>
    FLUIDITY,
    /// <summary>
    /// 분향
    /// </summary>
    INCENSED,
    /// <summary>
    /// 혐오
    /// </summary>
    HATRED,
    /// <summary>
    /// 형상 : 향로
    /// </summary>
    FORM_INCENSE_BURNER,
    /// <summary>
    /// 형상 : 성스러운 조각
    /// </summary>
    FORM_SACRED_SCULPTURE,
    /// <summary>
    /// 형상 : 악마 조각
    /// </summary>
    FORM_DEVILISH_SCULPTURE,

    /// <summary>
    /// 설계도
    /// </summary>
    BLUEPRINT,

    /// <summary>
    /// 수은
    /// </summary>
    MERCURY,

    /// <summary>
    /// 비정형
    /// </summary>
    UNSTRUCTURED,

    /// <summary>
    /// 허브
    /// </summary>
    HERB,

    /// <summary>
    /// 생명
    /// </summary>
    LIFE,

    /// <summary>
    /// 정의
    /// </summary>
    JUSTICE,

    /// <summary>
    /// 오류 발생!!
    /// </summary>
    ERRORED

};

/// <summary>
/// <para>조각 - Carve - 0 시작</para>
/// <para>압연 - Rolling</para>
/// <para>단조 용접 - ForgedWelding</para>
/// <para>망치질 - Hammering</para>
/// <para>아말감화 - Amalgamation</para>
/// <para>주물 - Casting</para>
/// <para>열간단조 - HotForging</para>
/// <para>수증기 증류 - SteamDistillation</para>
/// <para>섞기 - Mix</para>
/// <para>갈기 - Grind</para>
/// <para>아이템 결합 - Combine</para>
/// <para>수은 증류 - MercuryDistillation</para>
/// <para>접착 - Adhesion</para>
/// <para>분향 - BurnIncense</para>
/// </summary>
public enum TechType { 

    /// <summary>
    /// 조각
    /// </summary>
    CARVE,
    /// <summary>
    /// 압연
    /// </summary>
    ROLLING,
    /// <summary>
    /// 단조 용접
    /// </summary>
    FORGED_WELDING,
    /// <summary>
    /// 망치질
    /// </summary>
    HAMMERING,
    /// <summary>
    /// 아말감화
    /// </summary>
    AMALGAMATION,
    /// <summary>
    /// 주물
    /// </summary>
    CASTING,
    /// <summary>
    /// 열간단조
    /// </summary>
    HOTFORGING,
    /// <summary>
    /// 수증기 증류
    /// </summary>
    STEAMDISTILLATION,
    /// <summary>
    /// 섞기
    /// </summary>
    MIX,
    /// <summary>
    /// 갈기
    /// </summary>
    GRIND,
    /// <summary>
    /// 아이템 결합
    /// </summary>
    COMBINE,
    /// <summary>
    /// 수은 증류
    /// </summary>
    MERCURY_DISTILLATION,
    /// <summary>
    /// 접착
    /// </summary>
    ADHESION,
    /// <summary>
    /// 분향
    /// </summary>
    BURNINCENSE

}


#region 아이템 ID

/// <summary>
/// <para>전체 아이템 ID</para>
/// <para>0번은 error</para>
/// <para>아이템 코드는 1번부터.</para>
/// </summary>
public enum ItemID {

    /// <summary> 에러발생 ID </summary>
    _99999_ERROR_OCCURRED,

    ///<summary>때죽나무 수지</summary>
    _01_AGARWOOD_RESIN,
    ///<summary>몰약 수지</summary>
    _02_FRANKINCENSE_RESIN,
    ///<summary>유향 수지</summary>
    _03_MYRRH_RESIN,
    ///<summary>시더우드 가루</summary>
    _04_CEDARWOOD_POWDER,
    ///<summary>샌달우드 가루</summary>
    _05_SANDALWOOD_POWDER,
    ///<summary>라벤더 꽃봉오리</summary>
    _06_LAVENDER_FLOWER_BUDS,
    ///<summary>로즈마리 잎</summary>
    _07_ROSEMARY_LEAVES,
    ///<summary>사이프러스 잎</summary>
    _08_CYPRESS_LEAVES,
    ///<summary>스파이크너드 뿌리</summary>
    _09_SPIKE_NARD_ROOTS,
    ///<summary>장미꽃</summary>
    _10_ROSE_FLOWERS,
    ///<summary>시더우드 오일</summary>
    _11_CEDARWOOD_OIL,
    ///<summary>샌달우드 오일</summary>
    _12_SANDALWOOD_OIL,
    ///<summary>라벤더 오일</summary>
    _13_LAVENDER_OIL,
    ///<summary>로즈마리 오일</summary>
    _14_ROSEMARY_OIL,
    ///<summary>사이프러스 오일</summary>
    _15_CYPRESS_OIL,
    ///<summary>스파이크너드 오일</summary>
    _16_SPIKE_NARD_OIL,
    ///<summary>장미 오일</summary>
    _17_ROSE_OIL,
    ///<summary>믹스 오일</summary>
    _18_MIXED_OIL,
    ///<summary>금 조각</summary>
    _19_GOLD_NUGGET,
    ///<summary>구리 조각</summary>
    _20_COPPER_NUGGET,
    ///<summary>수은</summary>
    _21_MERCURY,
    ///<summary>왁스</summary>
    _22_WAX,
    ///<summary>석고</summary>
    _23_PLASTER,
    ///<summary>물</summary>
    _24_WATER,
    ///<summary>고무</summary>
    _25_RUBBER,
    ///<summary>향로의 설계도</summary>
    _26_INCENSE_BURNER_BLUEPRINT,
    ///<summary>성스러운 조각의 설계도</summary>
    _27_SACRED_FIGURINE_BLUEPRINT,
    ///<summary>악마 조각의 설계도</summary>
    _28_DEMONIC_FIGURINE_BLUEPRINT,
    ///<summary>은 조각</summary>
    _29_SILVER_NUGGET,
    ///<summary>납 조각</summary>
    _30_LEAD_NUGGET,
    ///<summary>철 조각</summary>
    _31_IRON_NUGGET,
    ///<summary>아말감</summary>
    _32_AMALGAM,
    ///<summary>금박</summary>
    _33_GOLD_LEAF,
    ///<summary>왁스 향로</summary>
    _34_WAX_BURNER,
    ///<summary>구리 향로</summary>
    _35_COPPER_BURNER,
    ///<summary>은 향로</summary>
    _36_SILVER_BURNER,
    ///<summary>순금 향로</summary>
    _37_PURE_GOLD_BURNER,
    ///<summary>성스러운 왁스 조각상</summary>
    _38_SACRED_WAX_FIGURINE,
    ///<summary>악마의 왁스 조각상</summary>
    _39_DEMONIC_WAX_FIGURINE,
    ///<summary>금동 향로</summary>
    _40_GILDED_BURNER,
    ///<summary>레진 믹스</summary>
    _41_RESIN_MIX,
    ///<summary>레진 인센스</summary>
    _42_RESIN_INCENSE,
    ///<summary>금 부스러가</summary>
    _43_GOLD_DUST,
    ///<summary>구리 부스러기</summary>
    _44_COPPER_DUST,
    ///<summary>동박</summary>
    _45_BRONZE_LEAF,
    ///<summary>성스러운 조각 왁스 향로</summary>
    _46_SACRED_FIGURINE_WAX_BURNER,
    ///<summary>악마의 조각 왁스 향로</summary>
    _47_DEMONIC_FIGURINE_WAX_BURNER,
    ///<summary>성스러운 조각 구리 향로</summary>
    _48_SACRED_FIGURINE_COPPER_BURNER,
    ///<summary>악마의 조각 구리 향로</summary>
    _49_DEMONIC_FIGURINE_COPPER_BURNER,
    ///<summary>성스러운 구리 조각상</summary>
    _50_SACRED_COPPER_FIGURINE,
    ///<summary>악마의 구리 조각상</summary>
    _51_DEMONIC_COPPER_FIGURINE,
    ///<summary>성스러운 조각 금동 향로</summary>
    _52_SACRED_FIGURINE_GILDED_BURNER,
    ///<summary>악마의 조각 금동 향로</summary>
    _53_DEMONIC_FIGURINE_GILDED_BURNER,
    ///<summary>정화의 성스러운 조각 금동 향로</summary>
    _54_PURIFIED_SACRED_FIGURINE_GILDED_BURNER,
    ///<summary>혐오하는 악마의 조각 금동 향로</summary>
    _55_ABHORRENT_DEMONIC_FIGURINE_GILDED_BURNER,
    ///<summary>회복의 구리 향로</summary>
    _56_HEALING_COPPER_BURNER,
    ///<summary>알 수 없는 무언가</summary>
    _57_UNKNOWN_SUBSTANCE,
    ///<summary>이상한 금속 박막</summary>
    _58_STRANGE_METAL_FILM,
    ///<summary>알 수 없는 부스러기</summary>
    _59_UNKNOWN_SHAVINGS,
    ///<summary>납박</summary>
    _60_LEAD_INLAY,
    ///<summary>은박</summary>
    _61_SILVER_INLAY,
    ///<summary>납 부스러기</summary>
    _62_LEAD_SHAVINGS,
    ///<summary>은 부스러기</summary>
    _63_SILVER_SHAVINGS,
    ///<summary>성스러운 조각 은 향로</summary>
    _64_SACRED_FIGURINE_SILVER_BURNER,
    ///<summary>성스러운 조각 순금 향로</summary>
    _65_SACRED_FIGURINE_PURE_GOLD_BURNER,
    ///<summary>악마의 조각 은 향로</summary>
    _66_DEMONIC_FIGURINE_SILVER_BURNER,
    ///<summary>악마의 조각 순금 향로</summary>
    _67_DEMONIC_FIGURINE_PURE_GOLD_BURNER,
    ///<summary>분향된 성스러운 조각 구리 향로</summary>
    _68_INCENSED_SACRED_FIGURINE_COPPER_BURNER,
    ///<summary>분향된 성스러운 조각 은 향로</summary>
    _69_INCENSED_SACRED_FIGURINE_SILVER_BURNER,
    ///<summary>분향된 성스러운 조각 금동 향로</summary>
    _70_INCENSED_SACRED_FIGURINE_GILDED_BURNER,
    ///<summary>분향된 악마의 조각 구리 향로</summary>
    _71_INCENSED_DEMONIC_FIGURINE_COPPER_BURNER,
    ///<summary>분향된 악마의 조각 은 향로</summary>
    _72_INCENSED_DEMONIC_FIGURINE_SILVER_BURNER,
    ///<summary>분향된 성스러운 조각 순금 향로</summary>
    _73_INCENSED_SACRED_FIGURINE_PURE_GOLD_BURNER,
    ///<summary>분향된 악마의 조각 순금 향로</summary>
    _74_INCENSED_DEMONIC_FIGURINE_PURE_GOLD_BURNER,
    ///<summary>분향된 악마의 조각 금동 향로</summary>
    _75_INCENSED_DEMONIC_FIGURINE_GILDED_BURNER,
    ///<summary>성스러운 금 조각상</summary>
    _76_SACRED_GOLD_FIGURINE,
    ///<summary>성스러운 은 조각상</summary>
    _77_SACRED_SILVER_FIGURINE,
    ///<summary>합금 향로</summary>
    _78_ALLOY_BURNER,
    ///<summary>알 수 없는 금속</summary>
    _79_UNKNOWN_METAL,
    ///<summary>성스러운 조각 합금 향로</summary>
    _80_SACRED_ALLOY_FIGURINE_BURNER,
    ///<summary>성스러운 합금 조각상</summary>
    _81_SACRED_ALLOY_FIGURINE,
    ///<summary>악마의 합금 조각상</summary>
    _82_DEMONIC_ALLOY_FIGURINE,
    ///<summary>악마의 조각 합금 향로</summary>
    _83_DEMONIC_ALLOY_FIGURINE_BURNER,
    ///<summary>분향된 성스러운 조각 합금 향로</summary>
    _84_INCENSED_SACRED_ALLOY_FIGURINE_BURNER,
    ///<summary>분향된 악마의 조각 합금 향로</summary>
    _85_INCENSED_DEMONIC_ALLOY_FIGURINE_BURNER

}

/// <summary>
/// 시스템 제공으로만 얻을 수 있는 아이템. - 0번은 error
///<para>때죽나무 수지 - AGARWOOD_RESIN</para>
///<para>몰약 수지 - FRANKINCENSE_RESIN</para>
///<para>유향 수지 - MYRRH_RESIN</para>
///<para>시더우드 가루 - CEDARWOOD_POWDER</para>
///<para>샌달우드 가루 - SANDALWOOD_POWDER</para>
///<para>라벤더 꽃봉오리 - LAVENDER_FLOWER_BUDS</para>
///<para>로즈마리 잎 - ROSEMARY_LEAVES</para>
///<para>사이프러스 잎 - CYPRESS_LEAVES</para>
///<para>스파이크너드 뿌리 - SPIKE_NARD_ROOTS</para>
///<para>장미꽃 - ROSE_FLOWERS</para>
///<para>금 조각 - GOLD_NUGGET</para>
///<para>구리 조각 - COPPER_NUGGET</para>
///<para>수은 - MERCURY</para>
///<para>왁스 - WAX</para>
///<para>석고 - PLASTER</para>
///<para>물 - WATER</para>
///<para>고무 - RUBBER</para>
///<para>향로의 설계도 - INCENSE_BURNER_BLUEPRINT</para>
///<para>성스러운 조각의 설계도 - SACRED_FIGURINE_BLUEPRINT</para>
///<para>악마 조각의 설계도 - DEMONIC_FIGURINE_BLUEPRINT</para>
///<para>은 조각 - SILVER_NUGGET</para>
///<para>납 조각 - LEAD_NUGGET</para>
///<para>철 조각 - IRON_NUGGET</para>
/// </summary>
public enum System_Provid_ItemID {

    /// <summary> 에러발생 ID </summary>
    ERROR_OCCURRED,

    ///<summary>때죽나무 수지</summary>
    AGARWOOD_RESIN,
    ///<summary>몰약 수지</summary>
    FRANKINCENSE_RESIN,
    ///<summary>유향 수지</summary>
    MYRRH_RESIN,
    ///<summary>시더우드 가루</summary>
    CEDARWOOD_POWDER,
    ///<summary>샌달우드 가루</summary>
    SANDALWOOD_POWDER,
    ///<summary>라벤더 꽃봉오리</summary>
    LAVENDER_FLOWER_BUDS,
    ///<summary>로즈마리 잎</summary>
    ROSEMARY_LEAVES,
    ///<summary>사이프러스 잎</summary>
    CYPRESS_LEAVES,
    ///<summary>스파이크너드 뿌리</summary>
    SPIKE_NARD_ROOTS,
    ///<summary>장미꽃</summary>
    ROSE_FLOWERS,
    ///<summary>금 조각</summary>
    GOLD_NUGGET,
    ///<summary>구리 조각</summary>
    COPPER_NUGGET,
    ///<summary>수은</summary>
    MERCURY,
    ///<summary>왁스</summary>
    WAX,
    ///<summary>석고</summary>
    PLASTER,
    ///<summary>물</summary>
    WATER,
    ///<summary>고무</summary>
    RUBBER,
    ///<summary>향로의 설계도</summary>
    INCENSE_BURNER_BLUEPRINT,
    ///<summary>성스러운 조각의 설계도</summary>
    SACRED_FIGURINE_BLUEPRINT,
    ///<summary>악마 조각의 설계도</summary>
    DEMONIC_FIGURINE_BLUEPRINT,
    ///<summary>은 조각</summary>
    SILVER_NUGGET,
    ///<summary>납 조각</summary>
    LEAD_NUGGET,
    ///<summary>철 조각</summary>
    IRON_NUGGET

}

/// <summary>
/// <para>캐릭터가 조합할 수 있는 아이템.</para>
/// <para>0번은 error</para>
/// <para>아이템 코드는 1번부터.</para>
/// </summary>
public enum Player_Creatable_ItemID {

    /// <summary> 에러발생 ID </summary>
    ERROR_OCCURRED,

    ///<summary>시더우드 오일</summary>
    CEDARWOOD_OIL,
    ///<summary>샌달우드 오일</summary>
    SANDALWOOD_OIL,
    ///<summary>라벤더 오일</summary>
    LAVENDER_OIL,
    ///<summary>로즈마리 오일</summary>
    ROSEMARY_OIL,
    ///<summary>사이프러스 오일</summary>
    CYPRESS_OIL,
    ///<summary>스파이크너드 오일</summary>
    SPIKE_NARD_OIL,
    ///<summary>장미 오일</summary>
    ROSE_OIL,
    ///<summary>믹스 오일</summary>
    MIXED_OIL,
    ///<summary>아말감</summary>
    AMALGAM,
    ///<summary>금박</summary>
    GOLD_LEAF,
    ///<summary>왁스 향로</summary>
    WAX_BURNER,
    ///<summary>구리 향로</summary>
    COPPER_BURNER,
    ///<summary>은 향로</summary>
    SILVER_BURNER,
    ///<summary>순금 향로</summary>
    PURE_GOLD_BURNER,
    ///<summary>성스러운 왁스 조각상</summary>
    SACRED_WAX_FIGURINE,
    ///<summary>악마의 왁스 조각상</summary>
    DEMONIC_WAX_FIGURINE,
    ///<summary>금동 향로</summary>
    GILDED_BURNER,
    ///<summary>레진 믹스</summary>
    RESIN_MIX,
    ///<summary>레진 인센스</summary>
    RESIN_INCENSE,
    ///<summary>금 부스러가</summary>
    GOLD_DUST,
    ///<summary>구리 부스러기</summary>
    COPPER_DUST,
    ///<summary>동박</summary>
    BRONZE_LEAF,
    ///<summary>성스러운 조각 왁스 향로</summary>
    SACRED_FIGURINE_WAX_BURNER,
    ///<summary>악마의 조각 왁스 향로</summary>
    DEMONIC_FIGURINE_WAX_BURNER,
    ///<summary>성스러운 조각 구리 향로</summary>
    SACRED_FIGURINE_COPPER_BURNER,
    ///<summary>악마의 조각 구리 향로</summary>
    DEMONIC_FIGURINE_COPPER_BURNER,
    ///<summary>성스러운 구리 조각상</summary>
    SACRED_COPPER_FIGURINE,
    ///<summary>악마의 구리 조각상</summary>
    DEMONIC_COPPER_FIGURINE,
    ///<summary>성스러운 조각 금동 향로</summary>
    SACRED_FIGURINE_GILDED_BURNER,
    ///<summary>악마의 조각 금동 향로</summary>
    DEMONIC_FIGURINE_GILDED_BURNER,
    ///<summary>정화의 성스러운 조각 금동 향로</summary>
    PURIFIED_SACRED_FIGURINE_GILDED_BURNER,
    ///<summary>혐오하는 악마의 조각 금동 향로</summary>
    ABHORRENT_DEMONIC_FIGURINE_GILDED_BURNER,
    ///<summary>회복의 구리 향로</summary>
    HEALING_COPPER_BURNER,
    ///<summary>알 수 없는 무언가</summary>
    UNKNOWN_SUBSTANCE,
    ///<summary>이상한 금속 박막</summary>
    STRANGE_METAL_FILM,
    ///<summary>알 수 없는 부스러기</summary>
    UNKNOWN_SHAVINGS,
    ///<summary>납박</summary>
    LEAD_INLAY,
    ///<summary>은박</summary>
    SILVER_INLAY,
    ///<summary>납 부스러기</summary>
    LEAD_SHAVINGS,
    ///<summary>은 부스러기</summary>
    SILVER_SHAVINGS,
    ///<summary>성스러운 조각 은 향로</summary>
    SACRED_FIGURINE_SILVER_BURNER,
    ///<summary>성스러운 조각 순금 향로</summary>
    SACRED_FIGURINE_PURE_GOLD_BURNER,
    ///<summary>악마의 조각 은 향로</summary>
    DEMONIC_FIGURINE_SILVER_BURNER,
    ///<summary>악마의 조각 순금 향로</summary>
    DEMONIC_FIGURINE_PURE_GOLD_BURNER,
    ///<summary>분향된 성스러운 조각 구리 향로</summary>
    INCENSED_SACRED_FIGURINE_COPPER_BURNER,
    ///<summary>분향된 성스러운 조각 은 향로</summary>
    INCENSED_SACRED_FIGURINE_SILVER_BURNER,
    ///<summary>분향된 성스러운 조각 금동 향로</summary>
    INCENSED_SACRED_FIGURINE_GILDED_BURNER,
    ///<summary>분향된 악마의 조각 구리 향로</summary>
    INCENSED_DEMONIC_FIGURINE_COPPER_BURNER,
    ///<summary>분향된 악마의 조각 은 향로</summary>
    INCENSED_DEMONIC_FIGURINE_SILVER_BURNER,
    ///<summary>분향된 성스러운 조각 순금 향로</summary>
    INCENSED_SACRED_FIGURINE_PURE_GOLD_BURNER,
    ///<summary>분향된 악마의 조각 순금 향로</summary>
    INCENSED_DEMONIC_FIGURINE_PURE_GOLD_BURNER,
    ///<summary>분향된 악마의 조각 금동 향로</summary>
    INCENSED_DEMONIC_FIGURINE_GILDED_BURNER,
    ///<summary>성스러운 금 조각상</summary>
    SACRED_GOLD_FIGURINE,
    ///<summary>성스러운 은 조각상</summary>
    SACRED_SILVER_FIGURINE,
    ///<summary>합금 향로</summary>
    ALLOY_BURNER,
    ///<summary>알 수 없는 금속</summary>
    UNKNOWN_METAL,
    ///<summary>성스러운 조각 합금 향로</summary>
    SACRED_ALLOY_FIGURINE_BURNER,
    ///<summary>성스러운 합금 조각상</summary>
    SACRED_ALLOY_FIGURINE,
    ///<summary>악마의 합금 조각상</summary>
    DEMONIC_ALLOY_FIGURINE,
    ///<summary>악마의 조각 합금 향로</summary>
    DEMONIC_ALLOY_FIGURINE_BURNER,
    ///<summary>분향된 성스러운 조각 합금 향로</summary>
    INCENSED_SACRED_ALLOY_FIGURINE_BURNER,
    ///<summary>분향된 악마의 조각 합금 향로</summary>
    INCENSED_DEMONIC_ALLOY_FIGURINE_BURNER


}


public static class TagAdapter
{

    /// <summary>
    /// 시스템 타입 코드를 아이템 테이블 코드 타입으로 변환.
    /// </summary>
    /// <param name="systemProvidItemID">시스템 아이템 아이디</param>
    /// <returns>아이템 ID</returns>
    public static ItemID SystemItem_To_ItemTag(System_Provid_ItemID systemProvidItemID)
    {
        switch (systemProvidItemID)
        {
            case System_Provid_ItemID.AGARWOOD_RESIN:
                return ItemID._01_AGARWOOD_RESIN;
            case System_Provid_ItemID.FRANKINCENSE_RESIN:
                return ItemID._02_FRANKINCENSE_RESIN;
            case System_Provid_ItemID.MYRRH_RESIN:
                return ItemID._03_MYRRH_RESIN;
            case System_Provid_ItemID.CEDARWOOD_POWDER:
                return ItemID._04_CEDARWOOD_POWDER;
            case System_Provid_ItemID.SANDALWOOD_POWDER:
                return ItemID._05_SANDALWOOD_POWDER;
            case System_Provid_ItemID.LAVENDER_FLOWER_BUDS:
                return ItemID._06_LAVENDER_FLOWER_BUDS;
            case System_Provid_ItemID.ROSEMARY_LEAVES:
                return ItemID._07_ROSEMARY_LEAVES;
            case System_Provid_ItemID.CYPRESS_LEAVES:
                return ItemID._08_CYPRESS_LEAVES;
            case System_Provid_ItemID.SPIKE_NARD_ROOTS:
                return ItemID._09_SPIKE_NARD_ROOTS;
            case System_Provid_ItemID.ROSE_FLOWERS:
                return ItemID._10_ROSE_FLOWERS;
            case System_Provid_ItemID.GOLD_NUGGET:
                return ItemID._19_GOLD_NUGGET;
            case System_Provid_ItemID.COPPER_NUGGET:
                return ItemID._20_COPPER_NUGGET;
            case System_Provid_ItemID.MERCURY:
                return ItemID._21_MERCURY;
            case System_Provid_ItemID.WAX:
                return ItemID._22_WAX;
            case System_Provid_ItemID.PLASTER:
                return ItemID._23_PLASTER;
            case System_Provid_ItemID.WATER:
                return ItemID._24_WATER;
            case System_Provid_ItemID.RUBBER:
                return ItemID._25_RUBBER;
            case System_Provid_ItemID.INCENSE_BURNER_BLUEPRINT:
                return ItemID._26_INCENSE_BURNER_BLUEPRINT;
            case System_Provid_ItemID.SACRED_FIGURINE_BLUEPRINT:
                return ItemID._27_SACRED_FIGURINE_BLUEPRINT;
            case System_Provid_ItemID.DEMONIC_FIGURINE_BLUEPRINT:
                return ItemID._28_DEMONIC_FIGURINE_BLUEPRINT;
            case System_Provid_ItemID.SILVER_NUGGET:
                return ItemID._29_SILVER_NUGGET;
            case System_Provid_ItemID.LEAD_NUGGET:
                return ItemID._30_LEAD_NUGGET;
            case System_Provid_ItemID.IRON_NUGGET:
                return ItemID._31_IRON_NUGGET;
            default:
                Debug.Log("대치 항목 없음");
                return ItemID._99999_ERROR_OCCURRED;
        }
    }

    /// <summary>
    /// 제작가능 타입 코드를 아이템 테이블 코드 타입으로 변환.
    /// </summary>
    /// <param name="playerCreatableItemID"></param>
    /// <returns></returns>
    public static ItemID PlayerItem_To_ItemTag(Player_Creatable_ItemID playerCreatableItemID)
    {
        switch (playerCreatableItemID)
        {
            case Player_Creatable_ItemID.CEDARWOOD_OIL: 
                return ItemID._11_CEDARWOOD_OIL;
            case Player_Creatable_ItemID.SANDALWOOD_OIL: 
                return ItemID._12_SANDALWOOD_OIL;
            case Player_Creatable_ItemID.LAVENDER_OIL: 
                return ItemID._13_LAVENDER_OIL;
            case Player_Creatable_ItemID.ROSEMARY_OIL: 
                return ItemID._14_ROSEMARY_OIL;
            case Player_Creatable_ItemID.CYPRESS_OIL: 
                return ItemID._15_CYPRESS_OIL;
            case Player_Creatable_ItemID.SPIKE_NARD_OIL: 
                return ItemID._16_SPIKE_NARD_OIL;
            case Player_Creatable_ItemID.ROSE_OIL: 
                return ItemID._17_ROSE_OIL;
            case Player_Creatable_ItemID.MIXED_OIL: 
                return ItemID._18_MIXED_OIL;
            case Player_Creatable_ItemID.AMALGAM: 
                return ItemID._32_AMALGAM;
            case Player_Creatable_ItemID.GOLD_LEAF: 
                return ItemID._33_GOLD_LEAF;
            case Player_Creatable_ItemID.WAX_BURNER: 
                return ItemID._34_WAX_BURNER;
            case Player_Creatable_ItemID.COPPER_BURNER: 
                return ItemID._35_COPPER_BURNER;
            case Player_Creatable_ItemID.SILVER_BURNER: 
                return ItemID._36_SILVER_BURNER;
            case Player_Creatable_ItemID.PURE_GOLD_BURNER: 
                return ItemID._37_PURE_GOLD_BURNER;
            case Player_Creatable_ItemID.SACRED_WAX_FIGURINE: 
                return ItemID._38_SACRED_WAX_FIGURINE;
            case Player_Creatable_ItemID.DEMONIC_WAX_FIGURINE: 
                return ItemID._39_DEMONIC_WAX_FIGURINE;
            case Player_Creatable_ItemID.GILDED_BURNER: 
                return ItemID._40_GILDED_BURNER;
            case Player_Creatable_ItemID.RESIN_MIX: 
                return ItemID._41_RESIN_MIX;
            case Player_Creatable_ItemID.RESIN_INCENSE: 
                return ItemID._42_RESIN_INCENSE;
            case Player_Creatable_ItemID.GOLD_DUST: 
                return ItemID._43_GOLD_DUST;
            case Player_Creatable_ItemID.COPPER_DUST: 
                return ItemID._44_COPPER_DUST;
            case Player_Creatable_ItemID.BRONZE_LEAF: 
                return ItemID._45_BRONZE_LEAF;
            case Player_Creatable_ItemID.SACRED_FIGURINE_WAX_BURNER: 
                return ItemID._46_SACRED_FIGURINE_WAX_BURNER;
            case Player_Creatable_ItemID.DEMONIC_FIGURINE_WAX_BURNER: 
                return ItemID._47_DEMONIC_FIGURINE_WAX_BURNER;
            case Player_Creatable_ItemID.SACRED_FIGURINE_COPPER_BURNER: 
                return ItemID._48_SACRED_FIGURINE_COPPER_BURNER;
            case Player_Creatable_ItemID.DEMONIC_FIGURINE_COPPER_BURNER: 
                return ItemID._49_DEMONIC_FIGURINE_COPPER_BURNER;
            case Player_Creatable_ItemID.SACRED_COPPER_FIGURINE: 
                return ItemID._50_SACRED_COPPER_FIGURINE;
            case Player_Creatable_ItemID.DEMONIC_COPPER_FIGURINE: 
                return ItemID._51_DEMONIC_COPPER_FIGURINE;
            case Player_Creatable_ItemID.SACRED_FIGURINE_GILDED_BURNER: 
                return ItemID._52_SACRED_FIGURINE_GILDED_BURNER;
            case Player_Creatable_ItemID.DEMONIC_FIGURINE_GILDED_BURNER: 
                return ItemID._53_DEMONIC_FIGURINE_GILDED_BURNER;
            case Player_Creatable_ItemID.PURIFIED_SACRED_FIGURINE_GILDED_BURNER: 
                return ItemID._54_PURIFIED_SACRED_FIGURINE_GILDED_BURNER;
            case Player_Creatable_ItemID.ABHORRENT_DEMONIC_FIGURINE_GILDED_BURNER: 
                return ItemID._55_ABHORRENT_DEMONIC_FIGURINE_GILDED_BURNER;
            case Player_Creatable_ItemID.HEALING_COPPER_BURNER: 
                return ItemID._56_HEALING_COPPER_BURNER;
            case Player_Creatable_ItemID.UNKNOWN_SUBSTANCE: 
                return ItemID._57_UNKNOWN_SUBSTANCE;
            case Player_Creatable_ItemID.STRANGE_METAL_FILM: 
                return ItemID._58_STRANGE_METAL_FILM;
            case Player_Creatable_ItemID.UNKNOWN_SHAVINGS: 
                return ItemID._59_UNKNOWN_SHAVINGS;
            case Player_Creatable_ItemID.LEAD_INLAY: 
                return ItemID._60_LEAD_INLAY;
            case Player_Creatable_ItemID.SILVER_INLAY: 
                return ItemID._61_SILVER_INLAY;
            case Player_Creatable_ItemID.LEAD_SHAVINGS: 
                return ItemID._62_LEAD_SHAVINGS;
            case Player_Creatable_ItemID.SILVER_SHAVINGS: 
                return ItemID._63_SILVER_SHAVINGS;
            case Player_Creatable_ItemID.SACRED_FIGURINE_SILVER_BURNER: 
                return ItemID._64_SACRED_FIGURINE_SILVER_BURNER;
            case Player_Creatable_ItemID.SACRED_FIGURINE_PURE_GOLD_BURNER: 
                return ItemID._65_SACRED_FIGURINE_PURE_GOLD_BURNER;
            case Player_Creatable_ItemID.DEMONIC_FIGURINE_SILVER_BURNER: 
                return ItemID._66_DEMONIC_FIGURINE_SILVER_BURNER;
            case Player_Creatable_ItemID.DEMONIC_FIGURINE_PURE_GOLD_BURNER: 
                return ItemID._67_DEMONIC_FIGURINE_PURE_GOLD_BURNER;
            case Player_Creatable_ItemID.INCENSED_SACRED_FIGURINE_COPPER_BURNER: 
                return ItemID._68_INCENSED_SACRED_FIGURINE_COPPER_BURNER;
            case Player_Creatable_ItemID.INCENSED_SACRED_FIGURINE_SILVER_BURNER: 
                return ItemID._69_INCENSED_SACRED_FIGURINE_SILVER_BURNER;
            case Player_Creatable_ItemID.INCENSED_SACRED_FIGURINE_GILDED_BURNER: 
                return ItemID._70_INCENSED_SACRED_FIGURINE_GILDED_BURNER;
            case Player_Creatable_ItemID.INCENSED_DEMONIC_FIGURINE_COPPER_BURNER: 
                return ItemID._71_INCENSED_DEMONIC_FIGURINE_COPPER_BURNER;
            case Player_Creatable_ItemID.INCENSED_DEMONIC_FIGURINE_SILVER_BURNER: 
                return ItemID._72_INCENSED_DEMONIC_FIGURINE_SILVER_BURNER;
            case Player_Creatable_ItemID.INCENSED_SACRED_FIGURINE_PURE_GOLD_BURNER: 
                return ItemID._73_INCENSED_SACRED_FIGURINE_PURE_GOLD_BURNER;
            case Player_Creatable_ItemID.INCENSED_DEMONIC_FIGURINE_PURE_GOLD_BURNER: 
                return ItemID._74_INCENSED_DEMONIC_FIGURINE_PURE_GOLD_BURNER;
            case Player_Creatable_ItemID.INCENSED_DEMONIC_FIGURINE_GILDED_BURNER: 
                return ItemID._75_INCENSED_DEMONIC_FIGURINE_GILDED_BURNER;
            case Player_Creatable_ItemID.SACRED_GOLD_FIGURINE: 
                return ItemID._76_SACRED_GOLD_FIGURINE;
            case Player_Creatable_ItemID.SACRED_SILVER_FIGURINE: 
                return ItemID._77_SACRED_SILVER_FIGURINE;
            case Player_Creatable_ItemID.ALLOY_BURNER:
                return ItemID._78_ALLOY_BURNER;
            case Player_Creatable_ItemID.UNKNOWN_METAL: 
                return ItemID._79_UNKNOWN_METAL;
            case Player_Creatable_ItemID.SACRED_ALLOY_FIGURINE_BURNER: 
                return ItemID._80_SACRED_ALLOY_FIGURINE_BURNER;
            case Player_Creatable_ItemID.SACRED_ALLOY_FIGURINE:
                return ItemID._81_SACRED_ALLOY_FIGURINE;
            case Player_Creatable_ItemID.DEMONIC_ALLOY_FIGURINE: 
                return ItemID._82_DEMONIC_ALLOY_FIGURINE;
            case Player_Creatable_ItemID.DEMONIC_ALLOY_FIGURINE_BURNER: 
                return ItemID._83_DEMONIC_ALLOY_FIGURINE_BURNER;
            case Player_Creatable_ItemID.INCENSED_SACRED_ALLOY_FIGURINE_BURNER: 
                return ItemID._84_INCENSED_SACRED_ALLOY_FIGURINE_BURNER;
            case Player_Creatable_ItemID.INCENSED_DEMONIC_ALLOY_FIGURINE_BURNER:
                return ItemID._85_INCENSED_DEMONIC_ALLOY_FIGURINE_BURNER;
            default: 
                return ItemID._99999_ERROR_OCCURRED;
        }
    }

}
#endregion


