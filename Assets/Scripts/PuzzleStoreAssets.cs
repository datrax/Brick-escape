/// Copyright (C) 2012-2014 Soomla Inc.
///
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///      http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Soomla.Store;
namespace PuzzleStore
{
    /// <summary>
    /// This class defines our game's economy, which includes virtual goods, virtual currencies
    /// and currency packs, virtual categories
    /// </summary>
    public class PuzzleStoreAssets : IStoreAssets
    {

        /// <summary>
        /// see parent.
        /// </summary>
        public int GetVersion()
        {
            return 0;
        }

        /// <summary>
        /// see parent.
        /// </summary>
        public VirtualCurrency[] GetCurrencies()
        {
            return new VirtualCurrency[] { TEN_TIPS_CURRENCY, FIFTY_TIPS_CURRENCY, HUNDRED_TIPS_CURRENCY };
        }

        /// <summary>
        /// see parent.
        /// </summary>
        public VirtualGood[] GetGoods()
        {
            return new VirtualGood[] { NO_ADS_LTVG, OPEN_NEXT_LEVEL };
        }

        /// <summary>
        /// see parent.
        /// </summary>
        public VirtualCurrencyPack[] GetCurrencyPacks()
        {
            return new VirtualCurrencyPack[] { FIFTYTIPS_PACK, TENTIPS_PACK, HUNDREDTIPS_PACK };
        }

        /// <summary>
        /// see parent.
        /// </summary>
        public VirtualCategory[] GetCategories()
        {
            return new VirtualCategory[] { GENERAL_CATEGORY };
        }

        /** Static Final Members **/



        public const string TEN_TIPS_PACK_ID = "buy10tips";

        public const string FIFTY_TIPS_PACK_ID = "buy50tips";

        public const string HUNDRED_TIPS_PACK_ID = "buy100tips";

        public const string REMOVEADS_PACK_ID = "removeads";

        public const string UNLOCKLEVEL_PACK_ID = "unlocklevel";

        public const string TENTIPS_ITEM_ID = "tentips";

        public const string FIFTY_ITEM_ID = "fiveteentips";

        public const string HUNDRED_ITEM_ID = "hundredtips";

        public const string REMOVEADS_ITEM_ID = "removeads";

        public const string UNLOCKLEVEL_ITEM_ID = "unlocklevel";


        /** Virtual Currencies **/

        public static VirtualCurrency TEN_TIPS_CURRENCY = new VirtualCurrency(
                "TenTips",                                      // name
                "",                                             // description
                TEN_TIPS_PACK_ID                         // item id
        );
        public static VirtualCurrency FIFTY_TIPS_CURRENCY = new VirtualCurrency(
        "FiftyTips",                                      // name
        "",                                             // description
        FIFTY_TIPS_PACK_ID                      // item id
            );
        public static VirtualCurrency HUNDRED_TIPS_CURRENCY = new VirtualCurrency(
        "HundredTips",                                      // name
        "",                                             // description
        HUNDRED_TIPS_PACK_ID                      // item id
        );
        /** Virtual Currency Packs **/

        public static VirtualCurrencyPack TENTIPS_PACK = new VirtualCurrencyPack(
                "10 TIPS",                                   // name
                "TEN TIPS",                       // description
                TENTIPS_ITEM_ID,                                   // item id
                1,                                             // number of currencies in the pack
                TEN_TIPS_PACK_ID,                        // the currency associated with this pack
                new PurchaseWithMarket(TEN_TIPS_PACK_ID, 0.99)
        );

        public static VirtualCurrencyPack FIFTYTIPS_PACK = new VirtualCurrencyPack(
                "50 TIPS",                                   // name
                "FIFTY TIPS",                 // description
                FIFTY_ITEM_ID,                                   // item id
                1,                                             // number of currencies in the pack
                FIFTY_TIPS_PACK_ID,                        // the currency associated with this pack
                new PurchaseWithMarket(FIFTY_TIPS_PACK_ID, 2.99)
        );

        public static VirtualCurrencyPack HUNDREDTIPS_PACK = new VirtualCurrencyPack(
                "100 TIPS",                                  // name
                "HUNDRED TIPS",                     // description
                HUNDRED_ITEM_ID,                                  // item id
                1,                                            // number of currencies in the pack
                HUNDRED_TIPS_PACK_ID,                        // the currency associated with this pack
                new PurchaseWithMarket(HUNDRED_TIPS_PACK_ID, 4.99)
        );

        /** Virtual Categories **/
        public static VirtualCategory GENERAL_CATEGORY = new VirtualCategory(
                "General", new List<string>(new string[] { TENTIPS_ITEM_ID, FIFTY_ITEM_ID, HUNDRED_ITEM_ID, REMOVEADS_ITEM_ID, UNLOCKLEVEL_ITEM_ID })
        );


        /** LifeTimeVGs **/
        // Note: create non-consumable items using LifeTimeVG with PuchaseType of PurchaseWithMarket
        public static VirtualGood NO_ADS_LTVG = new LifetimeVG(
            "No Ads",                                                       // name
            "No More Ads!",                                                 // description
            "removeads",                                                       // item id
            new PurchaseWithMarket(REMOVEADS_ITEM_ID, 0.99));  // the way this virtual good is purchased
        public static VirtualGood OPEN_NEXT_LEVEL = new LifetimeVG(
    "Opening next level",                                                       // name
    "Open next level",                                                 // description
    "unlocklevel",                                                       // item id
    new PurchaseWithMarket(UNLOCKLEVEL_ITEM_ID, 0.99));  // the way this virtual good is purchased

        //public static ItemData GetItem(string id)
        //{
        //    ItemData itemData = new ItemData();
        //    PurchasableVirtualItem pvi = StoreInfo.GetPurchasableItemWithProductId(id);
        //    if (pvi != null)
        //    {
        //        MarketItem mi = ((PurchaseWithMarket)pvi.PurchaseType).MarketItem;
        //        itemData.Id = id;
        //        itemData.Title = mi.MarketDescription;
        //        itemData.Price = mi.Price.ToString();
        //    }
        //    return itemData;
        //}
        //public class ItemData
        //{
        //    public ItemData()
        //    {
        //        Id = "";
        //        Title = "";
        //        Price = "";
        //        Description = "";
        //    }
        //        public string Id { get; set; }
        //    public string Title { get; set; }
        //    public string Price { get; set; }
        //    public string Description { get; set; }

        //}
    }

}