using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMaker
{


    public static class TechRecipeChecker {


        private static Dictionary<ItemID, TechRecipe> dic_TechRecipes = null;
        private static List<ItemID> filteredRecipes = null;

        public static ItemID CheckRecipe(UndeterminedItemInfo _inputItemInfo) {


            if (dic_TechRecipes == null) {
                initTechRecipeTable();
            }


            filteredRecipes = new List<ItemID>();

            long tagsL = ConvertTagsToLongType(_inputItemInfo.tagDic);


            BasicRecipeFilter(tagsL, _inputItemInfo.quality);
            if (filteredRecipes.Count == 0) return ItemID._57_UNKNOWN_SUBSTANCE;

            string tmp = "recipe1 : ";

            foreach (ItemID i in filteredRecipes) {

                tmp = tmp + i + " / ";

            }

            Debug.Log(tmp);


            DetailReqCheck(tagsL, _inputItemInfo);
            if (filteredRecipes.Count == 0) return ItemID._57_UNKNOWN_SUBSTANCE;
            if (filteredRecipes.Count == 1) return filteredRecipes[0];


            tmp = "recipe2 : ";

            foreach (ItemID i in filteredRecipes)
            {

                tmp = tmp + i + " / ";

            }

            Debug.Log(tmp);


            UniqueRecipeCheck();
            if (filteredRecipes.Count == 0) return ItemID._57_UNKNOWN_SUBSTANCE;
            if (filteredRecipes.Count == 1) return filteredRecipes[0];




            Debug.LogError("여러개의 레시피가 남음.");

            foreach (ItemID _id in filteredRecipes) {
                Debug.Log(_id);
            }

            return ItemID._99999_ERROR_OCCURRED;

        }



        private static long ConvertTagsToLongType(Dictionary<Tags,int> _tags) {

            long tagsL = 0;

            Debug.Log("태그 테스트!!!");

            

            foreach (Tags _tag in _tags.Keys) {

                tagsL += (1L << (int)_tag);
            }


            return tagsL;

        }




        private static void BasicRecipeFilter(long _tagsL, int _quality) {

            foreach (ItemID _id in dic_TechRecipes.Keys) { 
            
                if(dic_TechRecipes[_id].BasicReqCheck(_tagsL, _quality)) filteredRecipes.Add(_id);

            }

        }

        private static void DetailReqCheck(long _tagsL, UndeterminedItemInfo _inputItemInfo) { 
        
            List<ItemID> victimList = new List<ItemID>();

            foreach (ItemID _id in filteredRecipes) { 
            
                if(!dic_TechRecipes[_id].DetailReqCheck(_tagsL, _inputItemInfo)) { victimList.Add(_id); }

            }

            foreach (ItemID _id in victimList) {

                filteredRecipes.Remove(_id);

            }


        }


        private static void UniqueRecipeCheck() {


            List<ItemID> victimList = new List<ItemID>();


            

            foreach (ItemID _id in filteredRecipes) {

                if (dic_TechRecipes[_id].NeedUniqueTagCheck() != null) {

                    List<Tags> cmpTagList = dic_TechRecipes[_id].NeedUniqueTagCheck();

                    foreach (ItemID _innerId in filteredRecipes) {

                        if (_id != _innerId && dic_TechRecipes[_innerId].IsContainsTags(cmpTagList)) {

                            Debug.Log("banned id : " + _id);

                            victimList.Add(_id);
                            break;

                        }

                    }


                }
            }

            foreach (ItemID _id in victimList) { 
            
                filteredRecipes.Remove(_id);

            }

            victimList.Clear();

            foreach (ItemID _id in filteredRecipes) {


                if (dic_TechRecipes[_id].NeedUniqueRecipeCheck()) {

                    if (dic_TechRecipes.Count != 1) victimList.Add(_id);
                    Debug.Log("banned_2 id : " + _id);

                }


            }

            foreach (ItemID _id in victimList)
            {

                filteredRecipes.Remove(_id);

            }



        }




        private static void initTechRecipeTable()
        {
            dic_TechRecipes = new Dictionary<ItemID, TechRecipe>();


            #region 아이템 기술 레시피 초기화 영역

            dic_TechRecipes.Add(ItemID._11_CEDARWOOD_OIL, new CedarwoodOilTechRecipe());
            dic_TechRecipes.Add(ItemID._12_SANDALWOOD_OIL, new SandalwoodOilTechRecipe());
            dic_TechRecipes.Add(ItemID._13_LAVENDER_OIL, new LavenderOilTechRecipe());
            dic_TechRecipes.Add(ItemID._14_ROSEMARY_OIL, new RosemaryOilTechRecipe());
            dic_TechRecipes.Add(ItemID._15_CYPRESS_OIL, new CypressOilTechRecipe());
            dic_TechRecipes.Add(ItemID._16_SPIKE_NARD_OIL, new SpikeNardOilTechRecipe());
            dic_TechRecipes.Add(ItemID._17_ROSE_OIL, new RoseOilTechRecipe());
            dic_TechRecipes.Add(ItemID._18_MIXED_OIL, new MixedOilTechRecipe());
            dic_TechRecipes.Add(ItemID._32_AMALGAM, new AmalgamTechRecipe());
            dic_TechRecipes.Add(ItemID._33_GOLD_LEAF, new GoldLeafTechRecipe());
            dic_TechRecipes.Add(ItemID._34_WAX_BURNER, new WaxBurnerTechRecipe());
            dic_TechRecipes.Add(ItemID._35_COPPER_BURNER, new CopperBurnerTechRecipe());
            dic_TechRecipes.Add(ItemID._36_SILVER_BURNER, new SilverBurnerTechRecipe());
            dic_TechRecipes.Add(ItemID._37_PURE_GOLD_BURNER, new PureGoldBurnerTechRecipe());
            dic_TechRecipes.Add(ItemID._38_SACRED_WAX_FIGURINE, new SacredWaxFigurineTechRecipe());
            dic_TechRecipes.Add(ItemID._39_DEMONIC_WAX_FIGURINE, new DemonicWaxFigurineTechRecipe());
            dic_TechRecipes.Add(ItemID._40_GILDED_BURNER, new GildedBurnerTechRecipe());
            dic_TechRecipes.Add(ItemID._41_RESIN_MIX, new ResinMixTechRecipe());
            dic_TechRecipes.Add(ItemID._42_RESIN_INCENSE, new ResinIncenseTechRecipe());
            dic_TechRecipes.Add(ItemID._43_GOLD_DUST, new GoldDustTechRecipe());
            dic_TechRecipes.Add(ItemID._44_COPPER_DUST, new CopperDustTechRecipe());
            dic_TechRecipes.Add(ItemID._45_BRONZE_LEAF, new BronzeLeafTechRecipe());
            dic_TechRecipes.Add(ItemID._46_SACRED_FIGURINE_WAX_BURNER, new SacredFigurineWaxBurnerTechRecipe());
            dic_TechRecipes.Add(ItemID._47_DEMONIC_FIGURINE_WAX_BURNER, new DemonicFigurineWaxBurnerTechRecipe());
            dic_TechRecipes.Add(ItemID._48_SACRED_FIGURINE_COPPER_BURNER, new SacredFigurineCopperBurnerTechRecipe());
            dic_TechRecipes.Add(ItemID._49_DEMONIC_FIGURINE_COPPER_BURNER, new DemonicFigurineCopperBurnerTechRecipe());
            dic_TechRecipes.Add(ItemID._50_SACRED_COPPER_FIGURINE, new SacredCopperFigurineTechRecipe());
            dic_TechRecipes.Add(ItemID._51_DEMONIC_COPPER_FIGURINE, new DemonicCopperFigurineTechRecipe());
            dic_TechRecipes.Add(ItemID._52_SACRED_FIGURINE_GILDED_BURNER, new SacredFigurineGildedBurnerTechRecipe());
            dic_TechRecipes.Add(ItemID._53_DEMONIC_FIGURINE_GILDED_BURNER, new DemonicFigurineGildedBurnerTechRecipe());
            dic_TechRecipes.Add(ItemID._54_PURIFIED_SACRED_FIGURINE_GILDED_BURNER, new PurifiedSacredFigurineGildedBurnerTechRecipe());
            dic_TechRecipes.Add(ItemID._55_ABHORRENT_DEMONIC_FIGURINE_GILDED_BURNER, new AbhorrentDemonicFigurineGildedBurnerTechRecipe());
            dic_TechRecipes.Add(ItemID._56_HEALING_COPPER_BURNER, new HealingCopperBurnerTechRecipe());

            dic_TechRecipes.Add(ItemID._58_STRANGE_METAL_FILM, new StrangeMetalFilmTechRecipe());
            dic_TechRecipes.Add(ItemID._59_UNKNOWN_SHAVINGS, new UnknownShavingsTechRecipe());
            dic_TechRecipes.Add(ItemID._60_LEAD_INLAY, new LeadInlayTechRecipe());
            dic_TechRecipes.Add(ItemID._61_SILVER_INLAY, new SilverInlayTechRecipe());
            dic_TechRecipes.Add(ItemID._62_LEAD_SHAVINGS, new LeadShavingsTechRecipe());
            dic_TechRecipes.Add(ItemID._63_SILVER_SHAVINGS, new SilverShavingsTechRecipe());
            dic_TechRecipes.Add(ItemID._64_SACRED_FIGURINE_SILVER_BURNER, new SacredFigurineSilverBurnerTechRecipe());
            dic_TechRecipes.Add(ItemID._65_SACRED_FIGURINE_PURE_GOLD_BURNER, new SacredFigurinePureGoldBurnerTechRecipe());
            dic_TechRecipes.Add(ItemID._66_DEMONIC_FIGURINE_SILVER_BURNER, new DemonicFigurineSilverBurnerTechRecipe());
            dic_TechRecipes.Add(ItemID._67_DEMONIC_FIGURINE_PURE_GOLD_BURNER, new DemonicFigurinePureGoldBurnerTechRecipe());
            dic_TechRecipes.Add(ItemID._68_INCENSED_SACRED_FIGURINE_COPPER_BURNER, new IncensedSacredFigurineCopperBurnerTechRecipe());
            dic_TechRecipes.Add(ItemID._69_INCENSED_SACRED_FIGURINE_SILVER_BURNER, new IncensedSacredFigurineSilverBurnerTechRecipe());
            dic_TechRecipes.Add(ItemID._70_INCENSED_SACRED_FIGURINE_GILDED_BURNER, new IncensedSacredFigurineGildedBurnerTechRecipe());
            dic_TechRecipes.Add(ItemID._71_INCENSED_DEMONIC_FIGURINE_COPPER_BURNER, new IncensedDemonicFigurineCopperBurnerTechRecipe());
            dic_TechRecipes.Add(ItemID._72_INCENSED_DEMONIC_FIGURINE_SILVER_BURNER, new IncensedDemonicFigurineSilverBurnerTechRecipe());
            dic_TechRecipes.Add(ItemID._73_INCENSED_SACRED_FIGURINE_PURE_GOLD_BURNER, new IncensedSacredFigurinePureGoldBurnerTechRecipe());
            dic_TechRecipes.Add(ItemID._74_INCENSED_DEMONIC_FIGURINE_PURE_GOLD_BURNER, new IncensedDemonicFigurinePureGoldBurnerTechRecipe());
            dic_TechRecipes.Add(ItemID._75_INCENSED_DEMONIC_FIGURINE_GILDED_BURNER, new IncensedDemonicFigurineGildedBurnerTechRecipe());
            dic_TechRecipes.Add(ItemID._76_SACRED_GOLD_FIGURINE, new SacredGoldFigurineTechRecipe());
            dic_TechRecipes.Add(ItemID._77_SACRED_SILVER_FIGURINE, new SacredSilverFigurineTechRecipe());
            dic_TechRecipes.Add(ItemID._78_ALLOY_BURNER, new AlloyBurnerTechRecipe());
            dic_TechRecipes.Add(ItemID._79_UNKNOWN_METAL, new UnknownMetalTechRecipe());
            dic_TechRecipes.Add(ItemID._80_SACRED_ALLOY_FIGURINE_BURNER, new SacredAlloyFigurineBurnerTechRecipe());
            dic_TechRecipes.Add(ItemID._81_SACRED_ALLOY_FIGURINE, new SacredAlloyFigurineTechRecipe());
            dic_TechRecipes.Add(ItemID._82_DEMONIC_ALLOY_FIGURINE, new DemonicAlloyFigurineTechRecipe());
            dic_TechRecipes.Add(ItemID._83_DEMONIC_ALLOY_FIGURINE_BURNER, new DemonicAlloyFigurineBurnerTechRecipe());
            dic_TechRecipes.Add(ItemID._84_INCENSED_SACRED_ALLOY_FIGURINE_BURNER, new IncensedSacredAlloyFigurineBurnerTechRecipe());
            dic_TechRecipes.Add(ItemID._85_INCENSED_DEMONIC_ALLOY_FIGURINE_BURNER, new IncensedDemonicAlloyFigurineBurnerTechRecipe());

            #endregion


        }
    }


}

