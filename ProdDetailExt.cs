using System;
using System.Collections;
using System.Collections.Generic;
using PX.Objects.PO;
using System.Linq;
using PX.Data;
using PX.Data.EP;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Common;
using POLine = PX.Objects.PO.POLine;
using POOrder = PX.Objects.PO.POOrder;
using PX.Objects.AP;
using PX.Objects.CR;
using PX.Objects.AM.Attributes;
using PX.Objects.AM.CacheExtensions;
using PX.Objects.AM.GraphExtensions;
using PX.Objects;
using PX.Objects.AM;
using PX.Data.BQL.Fluent;
using PX.Objects.SO;
using PX.Data.BQL;

namespace PX.Objects.AM
{
    public class ProdDetail_Extension : PXGraphExtension<ProdDetail>
    {
        #region Event Handlers
        protected virtual void _(Events.RowSelected<AMProdItem> e)
        {
            var row = e.Row;
            if (row == null) return;
            var rowExt = row.GetExtension<AMProdItemExt>();
            if (!string.IsNullOrEmpty(row.OrdNbr) && string.IsNullOrEmpty(rowExt.UsrCustomerOrder))
            {
                var order = SelectFrom<SOOrder>.
                                Where<SOOrder.orderNbr.IsEqual<@P.AsString>.
                                    And<SOOrder.orderType.IsEqual<@P.AsString>>>.View.Select(Base, row.OrdNbr, row.OrdTypeRef).FirstTableItems.FirstOrDefault();
                if (order != null)
                {
                    rowExt.UsrCustomerOrder = order.CustomerOrderNbr;
                    rowExt.UsrDestinationRef = order.CustomerRefNbr;
                }
            }
        }
        #endregion
    }
}