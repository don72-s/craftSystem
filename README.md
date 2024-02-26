># craftSystem
>물질의 태그와 조합법의 조건과 규칙에 따라 조합을 시행하는 제작 시스템을 구현.

<br><br>

># 개요
>재료 아이템을 투입하여 제작 종류에 따라 나온 결과물의 조건에 맞는 아이템을 반환하는 시스템을 구현한다.   
><br>
>**주된 구현부는 화살표 부분에 해당되는 기능이라고 할 수 있다.**
>![craftflow](https://github.com/don72-s/craftSystem/assets/66211881/1db895fc-0be2-4b8a-9fe4-7a12212a8d1c)

<br><br>

># 목차
>
><h3>
>
> [1. 태그와 아이템 구조 정의](#태그와_아이템_구조_정의)   
><br>
> [2. 제작 시스템 정의](#제작_시스템_정의)   
><br>
> [3. 아이템 결정 시스템 정의](#아이템_결정_시스템_정의)   
>
> [4. 데이터베이스 입력](#데이터베이스_입력)   
></h3>
>

<br><br>

># 태그와_아이템_구조_정의
>기본적인 아이템 구조와 태그들을 정의한다.   
><br>
>
>>## 태그의 정의
>>아이템 구분을 위한 **아이템 ID 태그**와 제조기술 구분을 위한 **기술태그**, 조합에 이용될 **속성 태그** 총 세 종류를 정의한다.   
>><br>
>>
>>```cpp
>>
> > 
> > //아이템 ID 태그
> > public enum ItemID {
> > 
> >     /// <summary> 에러발생 ID </summary>
> >     _99999_ERROR_OCCURRED,
> > 
> >     ///<summary>때죽나무 수지</summary>
> >     _01_AGARWOOD_RESIN,
> >     ///<summary>몰약 수지</summary>
> >     _02_FRANKINCENSE_RESIN,
> >     ///<summary>유향 수지</summary>
> >     _03_MYRRH_RESIN,
> >     ///<summary>시더우드 가루</summary>
> >     _04_CEDARWOOD_POWDER,
> > 
> >     ...생략...
> > }
> > 
> > //기술 태그
> > public enum TechType { 
> > 
> >     /// <summary>
> >     /// 조각
> >     /// </summary>
> >     CARVE,
> >     /// <summary>
> >     /// 압연
> >     /// </summary>
> >     ROLLING,
> >     /// <summary>
> >     /// 단조 용접
> >     /// </summary>
> >     FORGED_WELDING,
> >     /// <summary>
> >     /// 망치질
> >     /// </summary>
> >     HAMMERING,
> > 
> >     ...생략...
> > }
> > 
> > //속성 태그
> > public enum Tags {
> >     /// <summary>
> >     /// 번영
> >     /// </summary>
> >     PROSPERITY,
> >     /// <summary>
> >     /// 수지
> >     /// </summary>
> >     RESIN,
> > 
> >     ...생략...
> > }
><br><br>
>
>>## 아이템 구조의 정의
>>아이템 객체의 정의는 **종류가 결정된 아이템 객체**타입과 **종류가 결정되지 않은 정보만 가진 객체**타입 두가지를 정의한다.   
>> <br> 전자는 아이템 출력 후에 **종류가 결정되었을 때** 사용될 타입이고, 후자는 조합 후 어느 아이템으로 결정될지 **아직 결정되지 않았을 때 임시**로 사용될 타입이다.   
>><br>
>>```cpp
> > //종류가 결정된 아이템 타입.
> > //id태그, 이름, 제조시 아이템 소모 여부, 설명, 품질, 보유 속성 태그 리스트들을 속성으로 가진다.
> > public class Item {
> > 
> >     public ItemID itemID;
> >     public string name;
> >     public bool IsDestroyOnCraft = false;
> >     public string description;
> >     public int quality;
> >     public Dictionary<Tags, int> tagDic;
> > 
> > }
> > 
> > //종류가 결정되지 않은 아이템 타입
> > //아이템 종류 결정에 필요한 정보인 품질, 보유 속성 태그 리스트, 투입되었던 아이템 종류 리스트들을 속성으로 가진다.
> > public class UndeterminedItemInfo{
> > 
> >     public int quality;
> >     public Dictionary<Tags, int> tagDic;
> >     public List<Item> enteredItemList;
> > 
> > }
>>```
<br><br>

># 제작_시스템_정의
>제작 시스템은 제작 시 적용될 기술을 선택하며 이후 제작 프로세스는 다음과 같다.   
><br>
>![tequProcess](https://github.com/don72-s/craftSystem/assets/66211881/c9fe7cb9-75cf-49f6-9fa1-b4da8a618b04)
>
>이 중, 체크와 연산이 이루어지는 부분은 하늘색 부분이며 **시행 조건, 조건부 연산, 태그 연산, 품질 연산**의 행위들은 적용될 기술에 따라 있을수도, 없을 수도 있다.   
><br>
>기본적으로 아무 조건이 없을 때 제작의 결과는 입력된 아이템들의 **모든 태그들의 합, 모든 품질의 합산, 입력된 아이템의 총 갯수**를 속성으로 가지는 결과물을 반환한다.   
><br>
>
>>## 간단한 예시
>>위의 체크와 연산 종류에 대해 간단한 예시를 들어본다.   
>><br>
>>- **시행 조건** : 해당 기술이 적용되기 위한 필수 조건을 의미한다.   
>>　　　　　ex) **조각**기술을 사용하기 위해서는 입력 아이템 중에 **설계도**속성 태그를 가진 아이템이 존재해야 한다.   
>>  <br>
>>
>>- **조건부 연산** : 입력 아이템들의 속성 정보에 따라 추가로 적용될 연산들을 의미한다.   
>>　　　　　　ex) **아말감화**기술을 사용하여 제작 시, **금속**속성 태그가 없는 아이템들의 데이터는 연산에서 제외시킨다.
>>  <br>
>>
>>- **태그 연산** : 결과물이 가질 태그 정보를 정의하는 연산을 의미한다.   
>>　　　　　ex) **분향**기술을 사용하여 제작 시, 반환되는 태그 리스트에 **분향**속성 태그를 1개 추가한다.
>>  <br>
>>
>>- **품질 연산** : 결과물이 가질 품질을 정의하는 연산을 의미한다.   
>>　　　　　ex) **망치질**기술을 사용하여 제작 시, 결과물로 나오는 아이템의 품질은   
>>　　　　　　**(모든 재료 아이템 품질의 합 / 재료 아이템의 갯수) + 2**의 연산을 따른다.   
>
><br><br>
>
>>## 구조 설계
>>모든 제작은 위의 흐름도의 흐름을 따른다. 언제나 **정해진 흐름**을 벗어나지 않으므로 **템플릿**패턴을 고려할 수 있다.   
>><br>
>>전체적인 설계 구조는 다음과 같다.   
>><br>
>>![techprocess](https://github.com/don72-s/craftSystem/assets/66211881/11755acc-c966-49a3-990f-cb77479d1735)
>>
>>기술 제작의 흐름은 **PlayTech**메소드의 호출로 이루어지며 이는 아래의 4가지 조건확인과 연산을 시행한다.   
>><br>
>>모든 제작 종류들은 **Tech**를 상속하며, 제작의 고유한 행위가 있을 경우 **Virtual**메소드를 오버라이드하여 연산을 재정의한다.   
>><br>
>>
>>```cpp
> >         public UndeterminedItemInfo PlayTech(List<Item> Items)
> >         {
> > 
> >             //4종류의 연산을 순차적으로 시행.
> >             if (!IsValid()) return null;
> >             ConditionOperation();
> >             TagOperation();
> >             QualityOperation();
> > 
> >             //결과 데이터를 반환
> >             return returnData.ChangeDataTypeToUndeterminedItemInfo();
> > 
> >         }
> >
>>```
>><br>
>>
>>**설계도 태그 보유** 라는 **사용조건**   
>>**모든 태그 반환, 아름다움 태그 1개 추가, 설계도 태그 제거** 라는 **태그 연산**   
>>위의 두 연산이 존재하는 **조각-Curve**기술의 예시는 다음과 같다.   
>>```cpp
> >     class Carve : Tech
> >     {
> >         //조건 연산의 override
> >         protected override bool IsValid()
> >         {
> >             //설계도 태그의 존재 여부 확인.
> >             return itemContainer.IsContainsTag(Tags.BLUEPRINT);
> >         }
> > 
> >         //태그 연산의 override
> >         protected override void TagOperation()
> >         {
> >             //기존 태그 연산 시행.
> >             base.TagOperation();
> > 
> >             //아름다움 태그 추가.
> >             returnData.AddTag(Tags.BEAUTY, 1);
> > 
> >             //설계도 태그 제거.
> >             returnData.RemoveTag(Tags.BLUEPRINT);
> >         }
> >     }
> >
>>```
>>
>>
