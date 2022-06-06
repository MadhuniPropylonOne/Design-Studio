using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PX.Common;
using PX.Data;
using PX.Objects.GL;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.CR;
using PX.Objects.TX;
using PX.Objects.IN;
using PX.Objects.EP;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.SO;
using SOOrder = PX.Objects.SO.SOOrder;
using SOLine = PX.Objects.SO.SOLine;
using PX.Data.DependencyInjection;
using PX.Data.ReferentialIntegrity.Attributes;

using PX.Objects.PM;
using CRLocation = PX.Objects.CR.Standalone.Location;
using PX.Objects.AP.MigrationMode;
using PX.Objects.Common;
using PX.Objects.Common.Discount;
using PX.Data.BQL.Fluent;
using PX.Data.BQL;
using PX.Objects.Common.Bql;
using PX.Objects.Extensions.CostAccrual;
using PX.Objects.DR;
using PX.Objects;
using PX.Objects.PO;
using CstDesignStudio;

namespace PX.Objects.PO
{
    public class POOrderEntryExt : PXGraphExtension<POOrderEntry>
    {
        public SelectFrom<CstPOLineSum>.Where<CstPOLineSum.orderType.IsEqual<POOrder.orderType.FromCurrent>.And<CstPOLineSum.orderNbr.IsEqual<POOrder.orderNbr.FromCurrent>>>.View CstPOSumLines;

        #region Event Handlers

        protected virtual void _(Events.RowSelected<POOrder> e)
        {
            var row = e.Row as POOrder;
            if (row == null) return;
            MassUpdateSub.SetEnabled(Base.complete.GetEnabled());
            CstPOSumLines.AllowInsert = Base.Transactions.AllowInsert;
            CstPOSumLines.AllowUpdate = Base.Transactions.AllowUpdate;
            CstPOSumLines.AllowDelete = Base.Transactions.AllowDelete;

        }
        protected virtual void _(Events.RowSelected<CstPOLineSum> e)
        {
            var row = e.Row as CstPOLineSum;
            if (row == null) return;
            PXUIFieldAttribute.SetEnabled<CstPOLineSum.totalQty>(e.Cache, row, Base.Transactions.AllowUpdate);
        }

        protected virtual void _(Events.RowUpdated<POLine> e)
        {
            var row = e.Row;
            if (row == null) return;           
            foreach (var item in CstPOSumLines.Select().FirstTableItems)
            {
                CstPOSumLines.Cache.Delete(item);
            }

            var query = (from t in Base.Transactions.Select().FirstTableItems.Where(x => x.InventoryID.HasValue)
                         group t by new { t.InventoryID, t.SubItemID, t.POAccrualSubID, t.UnitCost }
                       into rs
                         select new
                         {
                             rs.Key.InventoryID,
                             rs.Key.SubItemID,
                             rs.Key.POAccrualSubID,
                             rs.Key.UnitCost,
                             Qty = rs.Sum(t => t.OrderQty)
                         }).ToList();
            foreach (var item in query)
            {
                CstPOLineSum newSumLine = new CstPOLineSum();
                newSumLine.InventoryID = item.InventoryID;
                newSumLine.Subid = item.POAccrualSubID;
                newSumLine.SubItemID = item.SubItemID;
                newSumLine.UnitCost = item.UnitCost;
                newSumLine.TotalQty = item.Qty;
                CstPOSumLines.Cache.Insert(newSumLine);
               
            }
            CstPOSumLines.View.RequestRefresh();


        }
        protected virtual void _(Events.RowUpdated<CstPOLineSum> e)
        {
            var row = e.Row as CstPOLineSum;
            if (row == null)
                return;
            var list = Base.Transactions.Select().FirstTableItems.Where(x => x.InventoryID == row.InventoryID
                                                                  && x.SubItemID == row.SubItemID
                                                                  && x.POAccrualSubID == row.Subid
                                                                  && x.UnitCost == row.UnitCost);
            if(list.Count()>0)
            {
                var totalQtyLines = list.Sum(x => x.OrderQty);
                if (row.TotalQty != totalQtyLines)
                {
                    var remaintQty = row.TotalQty;
                    foreach (var item in list)
                    {                     
                        Base.Transactions.Cache.SetValueExt<POLine.orderQty>(item, remaintQty);                      
                        remaintQty -= item.OrderQty;
                    }
                    
                }
           
            }
        }
        protected virtual void _(Events.RowDeleted<POLine> e)
        {
            var row = e.Row;
            if (row == null) return;
            var sumRow = CstPOSumLines.Select().FirstTableItems.Where(x => x.InventoryID == row.InventoryID && x.SubItemID == row.SubItemID && x.Subid == row.POAccrualSubID && x.UnitCost == row.UnitCost).FirstOrDefault();
            if (sumRow != null)
            {
                CstPOSumLines.Cache.Delete(sumRow);
            }        
            CstPOSumLines.View.RequestRefresh();
        }

        protected virtual void _(Events.FieldVerifying<POOrderExt.usrETA> e)
        {
            var row = e.Row as POOrder;
            if (row == null) return;
            var rowExt = row.GetExtension<POOrderExt>();
            DateTime newETA = (DateTime)e.NewValue;
            if(newETA !=null)
            {
                if(newETA<rowExt.UsrETD)
                {
                    e.Cache.RaiseExceptionHandling<POOrderExt.usrETA>(e.Row, "", new PXSetPropertyException(CstDesignStudio.Descriptor.Messages.CompareDateETAETAD, PXErrorLevel.Error));
                    e.Cancel = true;
                }

            }
        }




        #endregion

        #region Actions
        public PXAction<POOrder> MassUpdateSub;
        [PXButton(CommitChanges = true)]
        [PXUIField(DisplayName = "Mass Update Sub Acc", Enabled = false)]
        protected virtual IEnumerable massUpdateSub(PXAdapter adapter)
        {
            var currentPO = Base.Document.Current;
            var currentPOExt = currentPO.GetExtension<POOrderExt>();
            if (currentPOExt.UsrMassSubItem == null || currentPOExt.UsrMassSubItem == 0)
                 return adapter.Get(); ;
            foreach (POLine item in Base.Transactions.Select())
            {
                item.POAccrualSubID = currentPOExt.UsrMassSubItem;
                Base.Transactions.Current = item;
                Base.Transactions.Update(Base.Transactions.Current);

            }
            Base.Save.Press();
            return adapter.Get();
        }
        #endregion

        #region Functions
        //public void InsertPOSummary(POLine row)
        //{
          
        //}
        #endregion
    }
}