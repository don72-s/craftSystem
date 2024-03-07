# craftSystem
물질의 태그와 조합법의 조건과 규칙에 따라 조합을 시행하는 제작 시스템을 구현.

unity ver - 2019.4.29f1

<br><br>

# 개요
재료 아이템을 투입하여 제작 종류에 따라 나온 결과물의 조건에 맞는 아이템을 반환하는 시스템을 구현한다.   
<br>
**주된 구현부는 화살표 부분에 해당되는 기능이라고 할 수 있다.**
![craftflow](https://github.com/don72-s/craftSystem/assets/66211881/1db895fc-0be2-4b8a-9fe4-7a12212a8d1c)

<br><br>

# 목차

<h3>

 [1. 태그와 아이템 구조 정의](#태그와_아이템_구조_정의)   
<br>
 [2. 제작 시스템 정의](#제작_시스템_정의)   
<br>
 [3. 아이템 결정 시스템 정의](#아이템_결정_시스템_정의)   
<br>
 [4. 간단한 조합 예시 및 설명](#간단한_조합_예시_및_설명)   
</h3>


<br><br>

# 태그와_아이템_구조_정의
기본적인 아이템 구조와 태그들을 정의한다.   
<br>

## 태그의 정의
아이템 구분을 위한 **아이템 ID 태그**와 제조기술 구분을 위한 **기술태그**, 조합에 이용될 **속성 태그** 총 세 종류를 정의한다.   
<br>

```cpp

  
  //아이템 ID 태그
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
  
      ...생략...
  }
  
  //기술 태그
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
  
      ...생략...
  }
  
  //속성 태그
  public enum Tags {
      /// <summary>
      /// 번영
      /// </summary>
      PROSPERITY,
      /// <summary>
      /// 수지
      /// </summary>
      RESIN,
  
      ...생략...
  }

```

<br><br>

## 아이템 구조의 정의
아이템 객체의 정의는 **종류가 결정된 아이템 객체**타입과 **종류가 결정되지 않은 정보만 가진 객체**타입 두가지를 정의한다.   
 <br> 전자는 아이템 출력 후에 **종류가 결정되었을 때** 사용될 타입이고, 후자는 조합 후 어느 아이템으로 결정될지 **아직 결정되지 않았을 때 임시**로 사용될 타입이다.   
<br>
```cpp
  //종류가 결정된 아이템 타입.
  //id태그, 이름, 제조시 아이템 소모 여부, 설명, 품질, 보유 속성 태그 리스트들을 속성으로 가진다.
  public class Item {
  
      public ItemID itemID;
      public string name;
      public bool IsDestroyOnCraft = false;
      public string description;
      public int quality;
      public Dictionary<Tags, int> tagDic;
  
  }
  
  //종류가 결정되지 않은 아이템 타입
  //아이템 종류 결정에 필요한 정보인 품질, 보유 속성 태그 리스트, 투입되었던 아이템 종류 리스트들을 속성으로 가진다.
  public class UndeterminedItemInfo{
  
      public int quality;
      public Dictionary<Tags, int> tagDic;
      public List<Item> enteredItemList;
  
  }
```
<br><br>

# 제작_시스템_정의
제작 시스템은 제작 시 적용될 기술을 선택하며 이후 제작 프로세스는 다음과 같다.   
<br>
![tequProcess](https://github.com/don72-s/craftSystem/assets/66211881/c9fe7cb9-75cf-49f6-9fa1-b4da8a618b04)

이 중, 체크와 연산이 이루어지는 부분은 하늘색 부분이며 **시행 조건, 조건부 연산, 태그 연산, 품질 연산**의 행위들은 적용될 기술에 따라 있을수도, 없을 수도 있다.   
<br>
기본적으로 아무 조건이 없을 때 제작의 결과는 입력된 아이템들의 **모든 태그들의 합, 모든 품질의 합산, 입력된 아이템의 총 갯수**를 속성으로 가지는 결과물을 반환한다.   
<br>

## 간단한 예시
위의 체크와 연산 종류에 대해 간단한 예시를 들어본다.   
<br>
- **시행 조건** : 해당 기술이 적용되기 위한 필수 조건을 의미한다.   
　　　　　ex) **조각**기술을 사용하기 위해서는 입력 아이템 중에 **설계도**속성 태그를 가진 아이템이 존재해야 한다.   
  <br>

- **조건부 연산** : 입력 아이템들의 속성 정보에 따라 추가로 적용될 연산들을 의미한다.   
　　　　　　ex) **아말감화**기술을 사용하여 제작 시, **금속**속성 태그가 없는 아이템들의 데이터는 연산에서 제외시킨다.
  <br>

- **태그 연산** : 결과물이 가질 태그 정보를 정의하는 연산을 의미한다.   
　　　　　ex) **분향**기술을 사용하여 제작 시, 반환되는 태그 리스트에 **분향**속성 태그를 1개 추가한다.
  <br>

- **품질 연산** : 결과물이 가질 품질을 정의하는 연산을 의미한다.   
　　　　　ex) **망치질**기술을 사용하여 제작 시, 결과물로 나오는 아이템의 품질은   
　　　　　　**(모든 재료 아이템 품질의 합 / 재료 아이템의 갯수) + 2**의 연산을 따른다.   

<br><br>

## 구조 설계
모든 제작은 위의 흐름도의 흐름을 따른다. 언제나 **정해진 흐름**을 벗어나지 않으므로 **템플릿**패턴을 고려할 수 있다.   
<br>
전체적인 설계 구조는 다음과 같다.   
<br>
![techprocess](https://github.com/don72-s/craftSystem/assets/66211881/11755acc-c966-49a3-990f-cb77479d1735)

기술 제작의 흐름은 **PlayTech**메소드의 호출로 이루어지며 이는 아래의 4가지 조건확인과 연산을 시행한다.   
<br>
모든 제작 종류들은 **Tech**를 상속하며, 제작의 고유한 행위가 있을 경우 **Virtual**메소드를 오버라이드하여 연산을 재정의한다.   
<br>

```cpp
          public UndeterminedItemInfo PlayTech(List<Item> Items)
          {
  
              //4종류의 연산을 순차적으로 시행.
              if (!IsValid()) return null;
              ConditionOperation();
              TagOperation();
              QualityOperation();
  
              //결과 데이터를 반환
              return returnData.ChangeDataTypeToUndeterminedItemInfo();
  
          }
 
```
<br>

**설계도 태그 보유** 라는 **사용조건**   
**모든 태그 반환, 아름다움 태그 1개 추가, 설계도 태그 제거** 라는 **태그 연산**   
위의 두 연산이 존재하는 **조각-Curve**기술의 예시는 다음과 같다.   
```cpp
      class Carve : Tech
      {
          //조건 연산의 override
          protected override bool IsValid()
          {
              //설계도 태그의 존재 여부 확인.
              return itemContainer.IsContainsTag(Tags.BLUEPRINT);
          }
  
          //태그 연산의 override
          protected override void TagOperation()
          {
              //기존 태그 연산 시행.
              base.TagOperation();
  
              //아름다움 태그 추가.
              returnData.AddTag(Tags.BEAUTY, 1);
  
              //설계도 태그 제거.
              returnData.RemoveTag(Tags.BLUEPRINT);
          }
      }
 
```


<br><br>

# 아이템_결정_시스템_정의
제작 결과 출력된 결과물**UndetermindItem**의 속성과 조건들을 비교하여 해당하는 **Item**종류를 결정하는 시스템을 정의한다.   
<br>
아이템의 결정 조건은 **필요 태그들을 포함**하는지 (기본 체크사항)   
다음으로 **추가 선택 기준을 만족**하는지 (세부 선택 기준)   
마지막으로 **유일한 선택사항으로서 존재**해야 하는지 (조건에 부합하는 유일한 아이템 종류)   
<br>
위의 세가지 체크의 순으로 이루어지며 이러한 체크를 **모든**아이템에 대하여 수행한다.   
<br>
순차적으로 체크한다는 **변하지 않는 흐름**이 존재하므로 이 또한 **템플릿 패턴**을 적용할 수 있으며, 전체적인 흐름은 다음과 같다.   
<br>
![filteringFlow](https://github.com/don72-s/craftSystem/assets/66211881/297a30f5-afaf-4541-993e-f14b15c01956)

## 태그의 이진화
태그의 **포함, 정확히 일치, 포함하지 않음**등의 태그 연산을 조금 더 용이하게 하기 위해서 가지고있는 태그 정보들을 하나의 정수로 통합시킨다.   
<br>
태그는 **Enum**값을 가지므로 결국 **정수값**이며 **서로 겹치지 않는**다.   
<br>
필요한 연산조건들과 태그 데이터의 특성을 고려하면 **비트연산**으로 처리하기 최적인 구조임을 알 수 있다.
```cpp

  long tagL = 0;

      //태그들의 리스트를 받아 long타입의 정수로 변환.
      public void TagsToLong(params Tags[] _tags) { 
      
          tagL = 0;
          foreach (Tags _tag in _tags) {
              //각 태그의 enum값 만큼 왼쪽으로 시프트 한 값을 누적해서 더함.
              tagL +=  1L << ((int)_tag);
          }
          
      }
```
<br>

## 태그 연산의 예시
1번 태그와 3번 태그를 가진 값이 변환된 long타입의 변수는 이진수로 나타내면 다음과 같을 것이다.   
**101**(b)   
<br>
**포함 연산** : 1번, 3번, 4번 태그가 포함되어있는지 확인을 원한다면 **or** 연산 후 **원래 값이 변했는지**확인해 보면 된다.   
(0101 | 1101) == 0101 //좌측값은 1101이 되므로 false가 나오며 **4번값**이 없기 때문에 옳은 판단을 했음을 알 수 있다.    
<br>
**완벽 일치 연산** : 일치 연산은 **기본 필터**연산 이후에 이루어지며, 완벽 일치 연산은 **기본 필터**에서 요구되는 태그들 이외에는 존재하지 않음을 의미한다.   
따라서 단순히 **기본 요구 태그**값과 **같은 값**인지 확인하면 된다.   
101 == 101 //두 값이 **정확히**일치하므로 완벽 일치 연산을 통과한다고 볼 수 있다.   
<br>
**포함하지 않음 연산** : 2번 태그를 포함하지 않는지를 확인하기 위해서는 **and**연산 후 결과값이 **0**인지 확인해 보면 된다.   
(101 & 010) == 0 //좌측값은 0이 되므로 true가 나오며 **2번값**을 포함하지 않기에 옳은 판단을 했음을 알 수 있다.   

<br><br>

## 구조 설계
**템플릿 패턴**을 이용하므로 이전의 기술 제조 클래스와 유사한 구조로 설계한다.
```cpp
  public static class TechRecipeChecker {
  
  
          private static Dictionary<ItemID, TechRecipe> dic_TechRecipes = null;
          private static List<ItemID> filteredRecipes = null;
  
          public static ItemID CheckRecipe(UndeterminedItemInfo _inputItemInfo) {
  
              if (dic_TechRecipes == null) {
                  initTechRecipeTable();
              }
  
              filteredRecipes = new List<ItemID>();
  
              //태그의 long타입 변환
              long tagsL = ConvertTagsToLongType(_inputItemInfo.tagDic);
  
              //기본 요구태그 확인
              BasicRecipeFilter(tagsL, _inputItemInfo.quality);
              if (filteredRecipes.Count == 0) return ItemID._57_UNKNOWN_SUBSTANCE;
  
              //추가 태그 조건 확인
              DetailReqCheck(tagsL, _inputItemInfo);
              if (filteredRecipes.Count == 0) return ItemID._57_UNKNOWN_SUBSTANCE;
              if (filteredRecipes.Count == 1) return filteredRecipes[0];
  
              //유일성 확인
              UniqueRecipeCheck();
              if (filteredRecipes.Count == 0) return ItemID._57_UNKNOWN_SUBSTANCE;
              if (filteredRecipes.Count == 1) return filteredRecipes[0];
  
              //두개 이상의 선택지가 남을 경우 레벨디자인(설계) 오류 처리.
              return ItemID._99999_ERROR_OCCURRED;
  
          }
```

위와 같이 정해진 흐름에 따라 진행되며, 모든 아이템 조건은 미리 초기화된 테이블을 사용한다.   
<br>
초기화 예시 [ 왁스 향로 ]
```cpp
  class WaxBurnerTechRecipe : TechRecipe
  {
      public WaxBurnerTechRecipe()
      {
          //품질은 131이상 131이하인 값을 요구하며, 이후의 태그들이 기본 요구 태그를 의미.
          InitData(131, 131, Tags.HOLINESS, Tags.PROTECTION, Tags.STABILITY, Tags.CHANGE, Tags.BEAUTY, Tags.FORM_INCENSE_BURNER, Tags.UNSTRUCTURED);
      }
  
      //완벽 일치를 요구하며, 따라서 완벽 요구 메소드를 오버라이드 하여 조건을 활성화 시킨다.
      protected override bool IsPerfectlyMatch(long _inputTags)
      {
          return _inputTags == reqTags;
      }
  }
```
**※부모인 TechRecipe**는 **abstract**클래스로 선언하여 구체화되지 않은 아이템 레시피로서의 객체화를 방지하기로 한다.

<br><br>

# 간단한_조합_예시_및_설명
![cr1](https://github.com/don72-s/craftSystem/assets/66211881/459ad55c-9e52-4a90-96a8-f91a973d6e78)   
**첫번째 조합** : **시더우드 가루**와 **왁스**를 **압연**기술을 이용해 조합한다.   
 **압연**은 **공통된 태그**만을 반환하는 조합법으로 결과물의 태그에는 **안정-STABILITY**만이 존재하며   
결과물 조건에 부합하는 아이템이 존재하지 않아 **알수없는 무언가 - UNKNOWN_SUBSTANCE**를 반환한다.   
<hr>

![cr2](https://github.com/don72-s/craftSystem/assets/66211881/b7ef06db-b913-4b21-9ffd-b04661352aa7)   
**두번째 조합** : **시더우드 가루**와 **왁스**를 **수증기 증류**기술을 이용해 조합한다.   
**수증기 증류**기술의 사용 조건은 **허브 태그를 가진 아이템 존재**이며, **시더우드 가루**에 **허브**태그가 존재하므로 조합이 가능하다.   
결과물 조건에 부합하는 **믹스 오일**아이템을 반환한다.   
<hr>

![cr3](https://github.com/don72-s/craftSystem/assets/66211881/d04072b0-5e70-46bf-8bc6-b66b202b47c4)   
**세번째 조합** : **시더우드 가루**를 **수증기 증류**기술을 이용해 조합한다.
조합 결과에 부합하는 아이템은 **시더우드 오일**과 **믹스 오일**이다.   
하지만 **믹스 오일**은 다른 **오일** 태그를 가진 결과 아이템이 **존재하지 않을 경우** 선택됨 이라는 특징을 가지므로   
결과 대상에서 제외되어, 남은 아이템인 **시더우드 오일**이 최종 결과물로써 반환된다.



