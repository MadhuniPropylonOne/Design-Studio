using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.Common.Bql;
using PX.Objects.Common.Discount.Attributes;
using PX.Objects.Common.Discount;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.DR;
using PX.Objects.GL;
using PX.Objects.IN.Matrix.Interfaces;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.SO;
using PX.Objects.TX;
using PX.Objects;
using System.Collections.Generic;
using System.Collections;
using System;
using PX.Data.BQL;

namespace PX.Objects.SO
{
    public class SOLineExt : PXCacheExtension<PX.Objects.SO.SOLine>
    {
        #region InventoryID  
        [PXMergeAttributes(Method = MergeMethod.Merge)]
        // [PXRestrictor(typeof(Where<InventoryItemExt.usrStyleTag.IsEqual<SOOrderExt.usrStyleTag.FromCurrent>.And<SOLine.orderType.FromCurrent.IsEqual<SOOrderTypeConstants.salesOrder>>>),"Restricted Styles",typeof(InventoryItem.inventoryID),ShowWarning = true)]
        //[PXRestrictor(typeof( Where<InventoryItemExt.usrStyleTag.IsNotEqual<SOOrderExt.usrStyleTag.FromCurrent>                                        
        //    .And<SOLine.orderType.FromCurrent.IsEqual<SOOrderTypeConstants.transferOrder>>>), "Restricted Styles", typeof(InventoryItem.inventoryID), ShowWarning = true)]
        [PXRestrictor(typeof(Where<Brackets<SOLine.orderType.FromCurrent.IsEqual<SOOrderTypeConstants.salesOrder>.And<InventoryItemExt.usrStyleTag.IsEqual<SOOrderExt.usrStyleTag.FromCurrent>>>.
                                Or<Brackets<SOLine.orderType.FromCurrent.IsEqual<CstDesignStudio.Descriptor.salesOrderSA>.And<InventoryItemExt.usrStyleTag.IsEqual<SOOrderExt.usrStyleTag.FromCurrent>>>.
                                Or<Brackets<SOLine.orderType.FromCurrent.IsEqual<CstDesignStudio.Descriptor.salesOrderRC>.And<InventoryItemExt.usrStyleTag.IsEqual<SOOrderExt.usrStyleTag.FromCurrent>>>.
                                Or<Brackets<SOLine.orderType.FromCurrent.IsEqual<SOOrderTypeConstants.transferOrder>>>>>>), 
                                "Restricted Styles", typeof(InventoryItem.inventoryID), ShowWarning = true)]
        public int? InventoryID { get; set; }
        #endregion

  

        #region UsrPacksBaseQty
        [PXDBQuantity]
        [PXUIField(DisplayName = "Packs Base Qty",Enabled = false)]

        public virtual Decimal? UsrPacksBaseQty { get; set; }
        public abstract class usrPacksBaseQty : PX.Data.BQL.BqlDecimal.Field<usrPacksBaseQty> { }
        #endregion

        #region UsrPackRatio
        [PXDBQuantity()]
      [PXDefault(typeof(decimal1))]
        [PXUIField(DisplayName = "Pack Ratio")]

        public virtual Decimal? UsrPackRatio { get; set; }
        public abstract class usrPackRatio : PX.Data.BQL.BqlDecimal.Field<usrPackRatio> { }
        #endregion

        #region UsrPackingID
        [PXDBString(255)]
        [PXUIField(DisplayName = "Pack ID")]

        public virtual string UsrPackingID { get; set; }
        public abstract class usrPackingID : PX.Data.BQL.BqlString.Field<usrPackingID> { }
        #endregion

        #region UsrSKUNbr
        [PXDBInt]
        [PXUIField(DisplayName = "SKU Nbr.")]
        public virtual int? UsrSKUNbr { get; set; }
        public abstract class usrSKUNbr : PX.Data.BQL.BqlInt.Field<usrSKUNbr> { }
        #endregion

        #region SalesAcctID
        [PXCustomizeBaseAttribute(typeof(PXUIFieldAttribute), nameof(PXUIFieldAttribute.Visible), true)]
        public int? SalesAcctID { get; set; }
        #endregion

        #region SalesSubID
        [PXCustomizeBaseAttribute(typeof(PXUIFieldAttribute), nameof(PXUIFieldAttribute.Visible), true)]
        public int? SalesSubID { get; set; }
        #endregion
    }
}