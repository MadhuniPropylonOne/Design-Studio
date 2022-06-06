using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using PX.Common;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.DR;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.PO;
using PX.Objects.TX;
using POLine = PX.Objects.PO.POLine;
using POOrder = PX.Objects.PO.POOrder;
using System.Threading.Tasks;

using CRLocation = PX.Objects.CR.Standalone.Location;
using PX.Objects.AR.CCPaymentProcessing;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;
using ARRegisterAlias = PX.Objects.AR.Standalone.ARRegisterAlias;
using PX.Objects.AR.MigrationMode;
using PX.Objects.Common;
using PX.Objects.Common.Discount;
using PX.Objects.Common.Extensions;
using PX.Objects.IN.Overrides.INDocumentRelease;
using PX.CS.Contracts.Interfaces;
using PX.Data.DependencyInjection;
using PX.Data.WorkflowAPI;
using PX.Objects.Extensions.PaymentTransaction;
using PX.Objects.SO.GraphExtensions.CarrierRates;
using PX.Objects.SO.Attributes;
using PX.Objects.Common.Attributes;
using PX.Objects.Common.Bql;
using OrderActions = PX.Objects.SO.SOOrderEntryActionsAttribute;
using PX.Objects;
using PX.Objects.SO;

namespace PX.Objects.SO
{
    public class SOOrderEntryExt : PXGraphExtension<SOOrderEntry>
    {
        #region Event Handlers
        protected virtual void _(Events.RowUpdating<SOLine> e)
        {
           // var row = e.Row as SOLine;
            var newRow = e.NewRow as SOLine;
         //   if (row == null) return;
        //    var rowExt = row.GetExtension<SOLineExt>();
            var newRowExt = newRow.GetExtension<SOLineExt>();
            //if (newRowExt.UsrPacksBaseQty != null || newRowExt.UsrPacksBaseQty > decimal.Zero)
            //    newRowExt.UsrPackRatio = newRow.OrderQty / newRowExt.UsrPacksBaseQty;
            //else
            //    newRowExt.UsrPackRatio = 0;

            if (newRowExt.UsrPackRatio != null || newRowExt.UsrPackRatio > decimal.Zero)
                newRowExt.UsrPacksBaseQty = newRow.OrderQty / newRowExt.UsrPackRatio;
            else
                newRowExt.UsrPacksBaseQty = 0;
        }


        protected virtual void _(Events.FieldUpdated<SOLine.inventoryID> e)
        {
            var row = e.Row as SOLine;
            if (row == null) return;
            var currentOrder = Base.Document.Current;
            InventoryItem inv = (InventoryItem)PXSelectorAttribute.Select<SOLine.inventoryID>(e.Cache, row,row.InventoryID);
            if( inv !=null)
            {
                row.TranDesc = currentOrder.CustomerOrderNbr + " " + currentOrder.CustomerRefNbr + " " + inv.Descr;
            }
        }


        protected virtual void _(Events.RowInserting<SOLine> e)
        {
            var row = e.Row as SOLine;
            if (row == null)
                return;
            var orderExt = Base.Document.Current.GetExtension<SOOrderExt>();
            if (orderExt.UsrStyleTag != null)
            {
                row.SalesSubID = orderExt.UsrStyleTag;
            }
        }

        #endregion
    }
}