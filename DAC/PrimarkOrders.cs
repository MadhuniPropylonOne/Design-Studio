using System;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.GL;

namespace PX.Objects.SO
{
  [Serializable]
  [PXCacheName("Primark Orders")]
  [PXPrimaryGraph(typeof(SOCreatePrimarkOrders))]
  public class PrimarkOrders : IBqlTable
  {
    #region LineID
    [PXDBIdentity(IsKey = true)]
    [PXUIField(DisplayName = "Line ID", Visible = false)]
    public virtual int? LineID { get; set; }
    public abstract class lineID : PX.Data.BQL.BqlInt.Field<lineID> { }
    #endregion

    #region Active
    [PXDBBool]
    [PXDefault(true)]
    [PXUIField(DisplayName = "Active", Enabled = false)]
    public virtual bool Active { get; set; }
    public abstract class active : PX.Data.BQL.BqlBool.Field<active> { }
    #endregion
    
    #region Processed
    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Processed", Enabled = false)]
    public virtual bool Processed { get; set; }
    public abstract class processed : PX.Data.BQL.BqlBool.Field<processed> { }
    #endregion
    #region Error
    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Error", Enabled = false)]
    public virtual bool Error { get; set; }
    public abstract class error : PX.Data.BQL.BqlBool.Field<error> { }
    #endregion

    #region Remark
    [PXDBString(250, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Remark", Enabled = false)]
    public virtual string Remark { get; set; }
    public abstract class remark : PX.Data.BQL.BqlString.Field<remark> { }
    #endregion
    #region OrderNbr
    [PXDBString(15, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "SO Nbr.", Enabled = false)]
    public virtual string OrderNbr { get; set; }
    public abstract class orderNbr : PX.Data.BQL.BqlString.Field<orderNbr> { }
    #endregion
    #region UnitCost
    [PXDBDecimal(4)]
    [PXUIField(DisplayName = "Unit Cost", Enabled = false)]
    public virtual decimal? UnitCost { get; set; }
    public abstract class unitCost : PX.Data.BQL.BqlDecimal.Field<unitCost> { }
    #endregion
    #region StyleTag
    [PXDBInt]
    [PXUIField(DisplayName = "Style Tag", Enabled = false)]
    public virtual int? StyleTag { get; set; }
    public abstract class styleTag : PX.Data.BQL.BqlInt.Field<styleTag> { }
    #endregion
    #region InventoryID
    [PXDBInt]
    [PXUIField(DisplayName = "Inventory ID", Enabled = false, Visible = false)]
    public virtual int? InventoryID { get; set; }
    public abstract class inventoryID : PX.Data.BQL.BqlInt.Field<inventoryID> { }
    #endregion
    #region SOCntr
    [PXDBInt]
    [PXUIField(DisplayName = "SO Counter", Enabled = false)]
    public virtual int? SOCntr { get; set; }
    public abstract class sOCntr : PX.Data.BQL.BqlInt.Field<sOCntr> { }
    #endregion

    #region CustomerOrderNbr
    [PXDBString(40, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Order Nbr")]
    public virtual string CustomerOrderNbr { get; set; }
    public abstract class customerOrderNbr : PX.Data.BQL.BqlString.Field<customerOrderNbr> { }
    #endregion

    #region KimballNbr
    [PXDBInt()]
    [PXUIField(DisplayName = "Kimball")]
    public virtual int? KimballNbr { get; set; }
    public abstract class kimballNbr : PX.Data.BQL.BqlInt.Field<kimballNbr> { }
    #endregion

    #region BrandStyle
    [PXDBString(20, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Brand Style")]
    public virtual string BrandStyle { get; set; }
    public abstract class brandStyle : PX.Data.BQL.BqlString.Field<brandStyle> { }
    #endregion

    #region BuyerStyle
    [PXDBString(20, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Buyer Style")]
    public virtual string BuyerStyle { get; set; }
    public abstract class buyerStyle : PX.Data.BQL.BqlString.Field<buyerStyle> { }
    #endregion

    #region InventoryCD
    [PXDBString(30, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Inventory ID")]
    public virtual string InventoryCD { get; set; }
    public abstract class inventoryCD : PX.Data.BQL.BqlString.Field<inventoryCD> { }
    #endregion
    #region OrderDesc
    [PXDBString(256, IsUnicode = true)]
    [PXUIField(DisplayName = "Description")]
    public virtual string OrderDesc { get; set; }
    public abstract class orderDesc : PX.Data.BQL.BqlString.Field<orderDesc> { }
    #endregion
    #region CuryUnitPrice
    [PXDBCurrency(typeof(Search<CommonSetup.decPlPrcCst>), typeof(SOLine.curyInfoID), typeof(SOLine.unitPrice))]
    [PXUIField(DisplayName = "Unit Price")]
    public virtual decimal? CuryUnitPrice { get; set; }
    public abstract class curyUnitPrice : PX.Data.BQL.BqlDecimal.Field<curyUnitPrice> { }
    #endregion
    #region Value
    [PXDBString(10, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Value")]
    public virtual string Value { get; set; }
    public abstract class value : PX.Data.BQL.BqlString.Field<value> { }
    #endregion
    
    #region S4
    [PXDBInt()]
    [PXUIField(DisplayName = "4")]
    public virtual int? S4 { get; set; }
    public abstract class s4 : PX.Data.BQL.BqlInt.Field<s4> { }
    #endregion

    #region S6
    [PXDBInt()]
    [PXUIField(DisplayName = "6")]
    public virtual int? S6 { get; set; }
    public abstract class s6 : PX.Data.BQL.BqlInt.Field<s6> { }
    #endregion

    #region S8
    [PXDBInt()]
    [PXUIField(DisplayName = "8")]
    public virtual int? S8 { get; set; }
    public abstract class s8 : PX.Data.BQL.BqlInt.Field<s8> { }
    #endregion

    #region S10
    [PXDBInt()]
    [PXUIField(DisplayName = "10")]
    public virtual int? S10 { get; set; }
    public abstract class s10 : PX.Data.BQL.BqlInt.Field<s10> { }
    #endregion

    #region S12
    [PXDBInt()]
    [PXUIField(DisplayName = "12")]
    public virtual int? S12 { get; set; }
    public abstract class s12 : PX.Data.BQL.BqlInt.Field<s12> { }
    #endregion

    #region S14
    [PXDBInt()]
    [PXUIField(DisplayName = "14")]
    public virtual int? S14 { get; set; }
    public abstract class s14 : PX.Data.BQL.BqlInt.Field<s14> { }
    #endregion

    #region S16
    [PXDBInt()]
    [PXUIField(DisplayName = "16")]
    public virtual int? S16 { get; set; }
    public abstract class s16 : PX.Data.BQL.BqlInt.Field<s16> { }
    #endregion

    #region S18
    [PXDBInt()]
    [PXUIField(DisplayName = "18")]
    public virtual int? S18 { get; set; }
    public abstract class s18 : PX.Data.BQL.BqlInt.Field<s18> { }
    #endregion

    #region S20
    [PXDBInt()]
    [PXUIField(DisplayName = "20")]
    public virtual int? S20 { get; set; }
    public abstract class s20 : PX.Data.BQL.BqlInt.Field<s20> { }
    #endregion
    #region XS
    [PXDBInt()]
    [PXUIField(DisplayName = "XS")]
    public virtual int? XS { get; set; }
    public abstract class xS : PX.Data.BQL.BqlInt.Field<xS> { }
    #endregion

    #region S
    [PXDBInt()]
    [PXUIField(DisplayName = "S")]
    public virtual int? S { get; set; }
    public abstract class s : PX.Data.BQL.BqlInt.Field<s> { }
    #endregion
    #region M
    [PXDBInt()]
    [PXUIField(DisplayName = "M")]
    public virtual int? M { get; set; }
    public abstract class m : PX.Data.BQL.BqlInt.Field<m> { }
    #endregion
    #region L
    [PXDBInt()]
    [PXUIField(DisplayName = "L")]
    public virtual int? L { get; set; }
    public abstract class l : PX.Data.BQL.BqlInt.Field<l> { }
    #endregion
    #region XL
    [PXDBInt()]
    [PXUIField(DisplayName = "XL")]
    public virtual int? XL { get; set; }
    public abstract class xL : PX.Data.BQL.BqlInt.Field<xL> { }
    #endregion
    #region Total
    [PXDBInt()]
    [PXUIField(DisplayName = "TOTAL")]
    public virtual int? Total { get; set; }
    public abstract class total : PX.Data.BQL.BqlInt.Field<total> { }
    #endregion

    #region RatioPack
    [PXDBInt()]
    [PXUIField(DisplayName = "Ratio Packs")]
    public virtual int? RatioPack { get; set; }
    public abstract class ratioPack : PX.Data.BQL.BqlInt.Field<ratioPack> { }
    #endregion
    #region PackingMethod
    [PXDBString(20)]
    //[PXStringList(new string[] { "F", "G" }, new string[] { "FLAT PACK (Carton)", "GOH (Hanging)" })]
    [PXUIField(DisplayName = "Packing Method")]
    public virtual string PackingMethod { get; set; }
    public abstract class packingMethod : PX.Data.BQL.BqlString.Field<packingMethod> { }
    #endregion
    #region PackNbr
    [PXDBString(20, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Pack")]
    public virtual string PackNbr { get; set; }
    public abstract class packNbr : PX.Data.BQL.BqlString.Field<packNbr> { }
    #endregion

    #region DestinationNbr
    [PXDBString(40, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Dest #")]
    public virtual string DestinationNbr { get; set; }
    public abstract class destinationNbr : PX.Data.BQL.BqlString.Field<destinationNbr> { }
    #endregion
    #region Country
    [PXDBString(20, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Country")]
    public virtual string Country { get; set; }
    public abstract class country : PX.Data.BQL.BqlString.Field<country> { }
    #endregion

    #region PriceTicket
    [PXDBString(20, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Price Ticket")]
    public virtual string PriceTicket { get; set; }
    public abstract class priceTicket : PX.Data.BQL.BqlString.Field<priceTicket> { }
    #endregion

    #region Delivery
    [PXDBDate()]
    [PXUIField(DisplayName = "Delivery")]
    public virtual DateTime? Delivery { get; set; }
    public abstract class delivery : PX.Data.BQL.BqlDateTime.Field<delivery> { }
    #endregion

    #region Branch
    [PXDBString(30, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Branch")]
    public virtual string Branch { get; set; }
    public abstract class branch : PX.Data.BQL.BqlString.Field<branch> { }
    #endregion

    #region Warehouse
    [PXDBString(30, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Warehouse")]
    public virtual string Warehouse { get; set; }
    public abstract class warehouse : PX.Data.BQL.BqlString.Field<warehouse> { }
    #endregion
    #region ShipVia
    [PXDBString(15, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Ship Via")]
    public virtual string ShipVia { get; set; }
    public abstract class shipVia : PX.Data.BQL.BqlString.Field<shipVia> { }
    #endregion

    #region Location
    [PXDBString(20, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Location")]
    public virtual string Location { get; set; }
    public abstract class location : PX.Data.BQL.BqlString.Field<location> { }
    #endregion

    #region Priority
    [PXDBString(20, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Priority")]
    public virtual string Priority { get; set; }
    public abstract class priority : PX.Data.BQL.BqlString.Field<priority> { }
    #endregion

    #region CreatedDateTime
    [PXDBCreatedDateTime()]
    [PXUIField(DisplayName = "Created Date")]
    public virtual DateTime? CreatedDateTime { get; set; }
    public abstract class createdDateTime : PX.Data.BQL.BqlDateTime.Field<createdDateTime> { }
    #endregion

    #region CreatedByScreenID
    [PXDBCreatedByScreenID()]
    public virtual string CreatedByScreenID { get; set; }
    public abstract class createdByScreenID : PX.Data.BQL.BqlString.Field<createdByScreenID> { }
    #endregion

    #region CreatedByID
    [PXDBCreatedByID()]
    public virtual Guid? CreatedByID { get; set; }
    public abstract class createdByID : PX.Data.BQL.BqlGuid.Field<createdByID> { }
    #endregion

    #region LastModifiedDateTime
    [PXDBLastModifiedDateTime()]
    public virtual DateTime? LastModifiedDateTime { get; set; }
    public abstract class lastModifiedDateTime : PX.Data.BQL.BqlDateTime.Field<lastModifiedDateTime> { }
    #endregion

    #region LastModifiedByScreenID
    [PXDBLastModifiedByScreenID()]
    public virtual string LastModifiedByScreenID { get; set; }
    public abstract class lastModifiedByScreenID : PX.Data.BQL.BqlString.Field<lastModifiedByScreenID> { }
    #endregion

    #region LastModifiedByID
    [PXDBLastModifiedByID()]
    public virtual Guid? LastModifiedByID { get; set; }
    public abstract class lastModifiedByID : PX.Data.BQL.BqlGuid.Field<lastModifiedByID> { }
    #endregion

    #region Tstamp
    [PXDBTimestamp()]
    [PXUIField(DisplayName = "Tstamp")]
    public virtual byte[] Tstamp { get; set; }
    public abstract class tstamp : PX.Data.BQL.BqlByteArray.Field<tstamp> { }
    #endregion

    }
}