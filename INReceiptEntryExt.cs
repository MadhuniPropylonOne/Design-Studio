using System;
using System.Collections;
using System.Collections.Generic;
using PX.Data;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.GL;
using System.Text;
using PX.Data.DependencyInjection;
using System.Linq;
using PX.Objects;
using PX.Objects.IN;
using PX.Objects.PO;
using PX.Data.BQL.Fluent;
using PX.Data.BQL;

namespace PX.Objects.IN
{
    public class INReceiptEntryExt : PXGraphExtension<INReceiptEntry>
    {
        #region Event Handlers  
        protected virtual void _(Events.RowSelected<INTran> e)
        {
            var row = e.Row as INTran;
            if (row == null) return;
            var rowExt = row.GetExtension<INTranExt>();
            if (  string.IsNullOrEmpty(rowExt.UsrPOExtInfo) && !string.IsNullOrEmpty(row.POReceiptNbr) && !string.IsNullOrEmpty(row.POReceiptType) && row.POReceiptLineNbr != null)
            {
              
                var poReceiptLine = SelectFrom<POReceiptLine>.Where<POReceiptLine.receiptType.IsEqual<@P.AsString>.And<POReceiptLine.receiptNbr.IsEqual<@P.AsString>>
                                                  .And<POReceiptLine.lineNbr.IsEqual<@P.AsInt>>>.View.Select(Base, row.POReceiptType, row.POReceiptNbr, row.POReceiptLineNbr).FirstTableItems.FirstOrDefault();
                if(poReceiptLine!=null)
                {
                    var poline = SelectFrom<POLine>.Where<POLine.orderType.IsEqual<@P.AsString>.And<POLine.orderNbr.IsEqual<@P.AsString>>
                                                   .And<POLine.lineNbr.IsEqual<@P.AsInt>>>.View.Select(Base, poReceiptLine.POType, poReceiptLine.PONbr, poReceiptLine.POLineNbr).FirstTableItems.FirstOrDefault();
                    if (poline != null)
                    {
                        rowExt.UsrPOExtInfo = (poline.GetExtension<POLineExt>()).UsrPOExtInfo;
                    }
                }
                

            }
        }
        #endregion
    }
}