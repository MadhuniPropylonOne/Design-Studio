using System;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PO;

namespace CstDesignStudio
{
    [Serializable]
    [PXCacheName("CstPOLineSum")]
    public class CstPOLineSum : IBqlTable
    {
        #region OrderType
        [PXDBString(2, IsKey = true, IsFixed = true, InputMask = "")]
        [PXUIField(DisplayName = "Order Type")]
        [PXDBDefault(typeof(POOrder.orderType))]

        public virtual string OrderType { get; set; }
        public abstract class orderType : PX.Data.BQL.BqlString.Field<orderType> { }
        #endregion

        #region OrderNbr
        [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Order Nbr")]
        [PXDBDefault(typeof(POOrder.orderNbr))]
        [PXParent(typeof(SelectFrom<POOrder>.Where<POOrder.orderType.IsEqual<CstPOLineSum.orderType>.And<POOrder.orderNbr.IsEqual<CstPOLineSum.orderNbr>>>))]
      //  [PXParent(typeof(POLine.FK.Order)]
        public virtual string OrderNbr { get; set; }
        public abstract class orderNbr : PX.Data.BQL.BqlString.Field<orderNbr> { }
        #endregion

        #region LineNbr
        [PXDBInt(IsKey =true)]
        [PXUIField(DisplayName = "Line Nbr")]
        [PXLineNbr(typeof(POOrderExt.usrSumLineNbr))]
        public virtual int? LineNbr { get; set; }
        public abstract class lineNbr : PX.Data.BQL.BqlInt.Field<lineNbr> { }
        #endregion

        #region InventoryID
        public abstract class inventoryID : PX.Data.BQL.BqlInt.Field<inventoryID>
        {
    
        }
        protected Int32? _InventoryID;
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [POLineInventoryItem(Filterable = true, Enabled = false)]
   
        public virtual Int32? InventoryID
        {
            get
            {
                return this._InventoryID;
            }
            set
            {
                this._InventoryID = value;
            }
        }
        #endregion

        #region SubItemID
    
        [PXUIField(DisplayName = "Sub Item ID",Enabled =false)]
        [SubItem(typeof(CstPOLineSum.inventoryID))]
        public virtual int? SubItemID { get; set; }
        public abstract class subItemID : PX.Data.BQL.BqlInt.Field<subItemID> { }
        #endregion

        #region Subid
        [PXDBInt()]
        [PXUIField(DisplayName = "Sub Account", Enabled = false)]
        [PXSelector(typeof(SelectFrom<Sub>.Where<Sub.active.IsEqual<True>>.SearchFor<Sub.subID>),
            new Type[]
            {
                typeof(Sub.subCD),
                typeof(Sub.description)
            },
            SubstituteKey = typeof(Sub.subCD),
            DescriptionField = typeof(Sub.description)
            )]
        public virtual int? Subid { get; set; }
        public abstract class subid : PX.Data.BQL.BqlInt.Field<subid> { }
        #endregion

        #region TotalQty
        [PXDBQuantity(MinValue =0)]
        [PXUIField(DisplayName = "Total Qty")]
        public virtual Decimal? TotalQty { get; set; }
        public abstract class totalQty : PX.Data.BQL.BqlDecimal.Field<totalQty> { }
        #endregion

        #region UnitCost
        [PXUIField(DisplayName = "Unit Cost", Enabled = false)]
        [PXDBPriceCost()]
  
        public virtual Decimal? UnitCost { get; set; }
        public abstract class unitCost : PX.Data.BQL.BqlDecimal.Field<unitCost> { }
        #endregion

        #region CreatedDateTime
        [PXDBCreatedDateTime()]
        public virtual DateTime? CreatedDateTime { get; set; }
        public abstract class createdDateTime : PX.Data.BQL.BqlDateTime.Field<createdDateTime> { }
        #endregion

        #region CreatedByID
        [PXDBCreatedByID()]
        public virtual Guid? CreatedByID { get; set; }
        public abstract class createdByID : PX.Data.BQL.BqlGuid.Field<createdByID> { }
        #endregion

        #region CreatedByScreenID
        [PXDBCreatedByScreenID()]
        public virtual string CreatedByScreenID { get; set; }
        public abstract class createdByScreenID : PX.Data.BQL.BqlString.Field<createdByScreenID> { }
        #endregion

        #region LastModifiedDateTime
        [PXDBLastModifiedDateTime()]
        public virtual DateTime? LastModifiedDateTime { get; set; }
        public abstract class lastModifiedDateTime : PX.Data.BQL.BqlDateTime.Field<lastModifiedDateTime> { }
        #endregion

        #region LastModifiedByID
        [PXDBLastModifiedByID()]
        public virtual Guid? LastModifiedByID { get; set; }
        public abstract class lastModifiedByID : PX.Data.BQL.BqlGuid.Field<lastModifiedByID> { }
        #endregion

        #region LastModifiedByScreenID
        [PXDBLastModifiedByScreenID()]
        public virtual string LastModifiedByScreenID { get; set; }
        public abstract class lastModifiedByScreenID : PX.Data.BQL.BqlString.Field<lastModifiedByScreenID> { }
        #endregion

        #region Tstamp
        [PXDBTimestamp()]
        [PXUIField(DisplayName = "Tstamp")]
        public virtual byte[] Tstamp { get; set; }
        public abstract class tstamp : PX.Data.BQL.BqlByteArray.Field<tstamp> { }
        #endregion
    }
}