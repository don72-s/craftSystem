using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//현재 기술이 전체 로직에서 미치는 범위와 내용정의.
// 사용조건.
//      - n개의 페어에 대한 and 조건
//      - n개의 페어에 대한 or 조건
//      - 조건 정의는 2가지에 대해서
//          - 아이템 그 자체를 사용한 조건
//          - 아이템이 아이템 연산에서 입력한 테그들에 대해서만 간섭하는 조건.
// 연산규칙.
//      - 반환 테그에 대한 규칙
//      - 테그별, 품질계수 통합규칙.
//      - 기타 규칙. 
//      - 조건 정의는 2가지에 대해서
//          - 아이템 그 자체를 사용한 조건
//          - 아이템이 아이템 연산에서 입력한 테그들에 대해서만 간섭하는 조건.
/// <summary>
/// 스킬 관련 데이터 구조체.<br/>
/// use_condition : T => Dictionary(string, (string, int)) => 테그,  규칙,  개수||<br/>
/// use_condition_operations : T=> string => 사용조건<br/>
/// tag_operation : T=> string => 테그 연산.<br/>
/// </summary>
public struct skill_data{
    /// <summary>
    /// 사용을 위한 조건. <br/>
    /// (사용 조건 연산 로직 , (조건, (테그, 개수, 계수)))
    /// </summary>
    public Dictionary<string, Dictionary<string,(string, int, int)>> use_conditions;
    /// <summary>
    /// 기술연산 관련 규칙 및 조건<br/>
    /// (연산규칙 , (조건, (테그, 개수, 계수)))
    /// </summary>
    public Dictionary<string, skill_operation_data> skill_operation_data;
    /// <summary>
    /// 스킬설명
    /// </summary>
    public string description;
}

public struct skill_operation_data{
    /// <summary>
    /// 연산 조건 정의 인자
    /// </summary>
    public Dictionary<string, Dictionary<string,(string, int, int)>> operation_conditions;
    /// <summary>
    /// 연산 항목 정의 인자.
    /// </summary>
    public Dictionary<string, Dictionary<string,(string, int, int)>> skill_operation;
}

public struct skill_operation_result{
    public string skill_name;// 기술 이름
    public Dictionary<string, (int, int)> result_item_data; // 결과 아이템 데이터 컨테이너.
    public Dictionary<string, item_data> result_item_list;// 아이템 기반 연산을 위한 결과아이템 데이터 컨테이너.
    public string log;
}

public struct use_condition_result{
    public bool result;
    public string log;
}



/// <summary>
/// skill_operations에서 함수를 쉽게 사용하기 위한 구조체<br/>
/// operation => T:string =>스킬연산 종류 명시<br/>
/// conditions => (테그, (연산종류, 개수, 계수))<br/>
/// </summary>
public struct skill_operation_datas{
    //기술이름 or id
    public string skill;
    //사용 아이템의 데이터 리스트
    public Dictionary<string, item_data[]> item_data;//아이템 데이터를 이름 기반으로 전달한다.
    /// <summary>
    /// 아이템 연산 결과물을 데이터로 입력받음
    /// </summary>
    public Dictionary<string, (int, List<int>)> craft_data;
}

/// <summary>
/// 기술사용조건 관련 판정을 위한 함수를 정의하는 클래스. 
/// </summary>
public class use_conditions{

     //-----------------종합조건 판정 함수------------------//
    //기술 사용조건 판정 델리게이트 선언
    public delegate use_condition_result use_condition_delegate(
                                        use_condition_result result,
                                        skill_operation_datas skill_operation_data,
                                        (string,int,int) condition );
    // 기술 사용조건 델리게이트 딕셔너리 선언.
    public Dictionary<string,use_condition_delegate> use_condition_dict
        = new Dictionary<string,use_condition_delegate>();

    // 기술 사용조건 델리케이드 딕셔너리 반환 및 추가 함수 선언.
    public Dictionary<string,use_condition_delegate> use_condition_list(){
        //이 아래로 신규 함수를 추가.
        use_condition_dict[nameof(have_tag)] = have_tag;
        use_condition_dict[nameof(have_tag_num_over_then)] = have_tag_num_over_then;
        use_condition_dict[nameof(have_item)] = have_item;
        use_condition_dict[nameof(have_item_num_over_then)] = have_item_num_over_then;
        return use_condition_dict;
    }

    /// <summary>
    /// 해당 테그를 가졌는지 체크
    /// </summary>
    /// <param name="skill_Operation_Datas"></param>
    /// <param name="condition"></param>
    /// <returns></returns>
    public use_condition_result have_tag (  
                        use_condition_result result,
                        skill_operation_datas skill_Operation_Datas,
                        (string,int,int) condition ){
            result.result = skill_Operation_Datas.craft_data.ContainsKey(condition.Item1);
            result.log = "\n 해당 태그 보유 여부 : " + result.result;
        return result;
    }
    /// <summary>
    /// 해당 태그를 가짐과 동시해. 일정 개수이상 가졌는지 체크
    /// </summary>
    /// <param name="skill_Operation_Datas"></param>
    /// <param name="condition"></param>
    /// <returns></returns>
    public use_condition_result have_tag_num_over_then (  
                        use_condition_result result,
                        skill_operation_datas skill_Operation_Datas,
                        (string,int,int) condition ){
        bool it_have_over_then_condition_num = 
                skill_Operation_Datas.craft_data[condition.Item1].Item1 > condition.Item2;
        result.result = have_tag(result, skill_Operation_Datas, condition).result && it_have_over_then_condition_num;
        result.log =    "\n해당 태그 보유 여부 : " + have_tag(result, skill_Operation_Datas, condition).result
                        + "\n조건 개수 이상 보유 여부 : " + it_have_over_then_condition_num
                        + "(" + skill_Operation_Datas.craft_data[condition.Item1].Item1 +" / " + it_have_over_then_condition_num + ")";
        return  result;
    }
    /// <summary>
    /// 해당 아이템을 가지고 있는지 체크
    /// </summary>
    /// <param name="skill_Operation_Datas"></param>
    /// <param name="condition"></param>
    /// <returns></returns>
    public use_condition_result have_item (  
                        use_condition_result result,
                        skill_operation_datas skill_Operation_Datas,
                        (string,int,int) condition ){
        
        result.result = skill_Operation_Datas.item_data.ContainsKey(condition.Item1);
        result.log= "\n 해당 아이템 보유 여부 : " + result.result; 
        return  result;
    }
    /// <summary>
    /// 해당 아이템을 일정 개수 이상 가지고 있는지 체크.
    /// </summary>
    /// <param name="skill_Operation_Datas"></param>
    /// <param name="condition"></param>
    /// <returns></returns>
    public use_condition_result have_item_num_over_then (  
                        use_condition_result result,
                        skill_operation_datas skill_Operation_Datas,
                        (string,int,int) condition ){
        bool it_have_over_then_condition_num = 
                skill_Operation_Datas.item_data[condition.Item1].Length > condition.Item2;

                result.result = have_item(result, skill_Operation_Datas, condition).result && it_have_over_then_condition_num;
                result.log= "\n 해당 아이템 보유 여부 : " + have_item(result, skill_Operation_Datas, condition).result
                            + "\n 해당 아이템 보유개수 충족여부 : " + it_have_over_then_condition_num; 
        return  result;
    }








    //-----------------종합 조건 상위 연산 함수------------------


    public delegate use_condition_result use_condition_operation_delegate(
                                        use_condition_result result,
                                        Dictionary<string,(string, int, int)> conditions,
                                        skill_operation_datas skill_Operation_Datas );
    // 기술 사용조건 델리게이트 딕셔너리 선언.
    public Dictionary<string,use_condition_operation_delegate> use_condition_operation_dict
        = new Dictionary<string,use_condition_operation_delegate>();

    // 기술 사용조건 델리케이드 딕셔너리 반환 및 추가 함수 선언.
    public Dictionary<string,use_condition_operation_delegate> use_condition_operation_list(){
        //이 아래로 신규 함수를 추가.
        use_condition_operation_dict[nameof(or_mach_check)] = or_mach_check;
        use_condition_operation_dict[nameof(and_mach_check)] = and_mach_check;
        return use_condition_operation_dict;
    }
    /// <summary>
    /// 조건들에 대한 or 연산
    /// </summary>
    /// <param name="skill_operation_data"></param>
    /// <param name="debug_log_Use"></param>
    /// <returns></returns>
    public use_condition_result or_mach_check (
                    use_condition_result result,
                    Dictionary<string,(string, int, int)> conditions,
                    skill_operation_datas skill_Operation_Datas
                    ){
        result.result=false;
        foreach(KeyValuePair<string, (string,int,int)> condition in conditions){
            string check = condition.Key;
            result.result = result.result||use_condition_dict[check](result, skill_Operation_Datas, condition.Value).result;
            result.log = use_condition_dict[check](result, skill_Operation_Datas, condition.Value).log;
        }
        return result;
    }
    public use_condition_result and_mach_check (
                    use_condition_result result,
                    Dictionary<string,(string, int, int)> conditions,
                    skill_operation_datas skill_Operation_Datas
                    ){
        result.result = true;
        foreach(KeyValuePair<string, (string, int, int)> condition in conditions){
            string check = condition.Key;
            result.result = result.result&&use_condition_dict[check](result, skill_Operation_Datas, condition.Value).result;
            result.log = use_condition_dict[check](result, skill_Operation_Datas, condition.Value).log;
        }
        return result;
    }
    
}

// A&&B||C&&D

// A&&B||C&&D

// =>((A&&B)||C)&&D



/// <summary>
/// 스킬 연산 로직들을 보관하는 class 
/// 값을 입력하면 skill_operation_selector()에서,  확인 후 제공한다. 
/// </summary>
public class skill_operations{

    //기술연산 델리게이트 선언
    public delegate int skill_operation_delegate(
                                        skill_operation_datas skill_operation_data,
                                        bool debug_log_Use = false
                                            );
    // 기술연산 델리게이트 딕셔너리 선언.
   public Dictionary<string,skill_operation_delegate> skill_operation_dict
        = new Dictionary<string,skill_operation_delegate>();

    // 기술연산 델리케이드 딕셔너리 반환 및 추가 함수 선언.
    public Dictionary<string,skill_operation_delegate> skill_operation_list(){
        
        return skill_operation_dict;
    }
    //------------테그 규칙
    //------------테그 연산
    //------------품질 연산
    //------------반환 연산
    //------------특수 연산
    // public Dictionary<string, (int, int)> skill_operation_selector(skill_operation_datas skill_operation_data){
    //     string operation_name = skill_operation_data.operation;
    //     Dictionary<string, (int, int)> result;
    //     return result;
    // }

    
}

public class skill_table : skill_operations
{
    public Dictionary<string, skill_data> skill_table_list = new Dictionary<string, skill_data>();
    public Dictionary<string, (int, List<int>)> craft_data = new Dictionary<string, (int, List<int>)>();

    /// <summary>
    /// 기술 사용 조건 
    /// </summary>
    /// <param name="operation_datas"></param>
    /// <returns></returns>
    public use_condition_result skill_operation_condition(skill_operation_datas operation_datas){
        use_conditions use_Conditions = new use_conditions();
        var condition_checker=use_Conditions.use_condition_operation_list();
        
        Dictionary<string, Dictionary<string,(string, int, int)>> 
                operation_conditions = skill_table_list[operation_datas.skill].use_conditions;

        Dictionary<string, (int, List<int>)> craft_data
                =operation_datas.craft_data;

        use_condition_result result = new use_condition_result();

        foreach(KeyValuePair<string, Dictionary<string, (string, int, int)>> operation_condition_data in operation_conditions){
            string operation = operation_condition_data.Key;
            Dictionary<string, (string, int, int)> operation_condition = operation_condition_data.Value;
            result = condition_checker[operation](result, operation_condition, operation_datas);
        }
        
        return result;
    }

    public skill_operation_result skill_operation(skill_operation_datas operation_datas){
        skill_operation_result skill_Operation_Result = new skill_operation_result();
        var skill_Data = skill_table_list[operation_datas.skill].skill_operation_data;
        // var operation_condition = skill_Data[].operation_conditions;
        // var skill_operation = skill_Data[].skill_operation;
    // public Dictionary<string, skill_operation_data> skill_operation_data

        return skill_Operation_Result;
    }








    // Start is called before the first frame update
    void Start()
    {
        

    }

    //스킬 연산의 입력값은 Dictionary<string,  (int, List<int>)>다
    //따라서 스킬 연산의 모든 함수는 Dictionary<string,  (int, List<int>)>를 입력 값으로 받게 된다. 
    //스킬연산의 결과물은. Dictionary<string,  (int, int)>다
    //따라서 연산 결과로 Dictionary<string,  (int, int)>를 반환한다.
    //이는 recipe_opertaion에서 사용되며 이 정보를 아이템은 계승한다.
    // Update is called once per frame
    void Update()
    {
        
    }
}
