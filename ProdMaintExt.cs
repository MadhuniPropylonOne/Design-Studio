using System;
using PX.Data;
using PX.Objects.IN;
using PX.Objects.CS;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using PX.Objects.AM.GraphExtensions;
using PX.Common;
using PX.Objects.Common;
using PX.Objects.SO;
using PX.Objects.AM.Attributes;
using System.Linq;
using PX.Objects.AM.CacheExtensions;
using PX.Objects.PO;
using PX.Data.BQL.Fluent;
using PX.Data.BQL;
using PX.Objects;
using PX.Objects.AM;

namespace PX.Objects.AM
{
    public class ProdMaint_Extension : PXGraphExtension<ProdMaint>
    {
        #region Event Handlers
        protected virtual void _(Events.RowSelected<AMProdItem> e)
        {
            var row = e.Row;
            if(row==null) return;
            var rowExt = row.GetExtension<AMProdItemExt>();
            if(!string.IsNullOrEmpty(row.OrdNbr) && string.IsNullOrEmpty(rowExt.UsrCustomerOrder))
            {
                var order = SelectFrom<SOOrder>.
                                Where<SOOrder.orderNbr.IsEqual<@P.AsString>.
                                    And<SOOrder.orderType.IsEqual<@P.AsString>>>.View.Select(Base, row.OrdNbr, row.OrdTypeRef).FirstTableItems.FirstOrDefault();
                if(order!=null)
                {
                    rowExt.UsrCustomerOrder = order.CustomerOrderNbr;
                    rowExt.UsrDestinationRef = order.CustomerRefNbr;
                }
            }
        }
        #endregion
    }
}