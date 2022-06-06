using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using PX.Common;
using PX.Data;
using PX.Objects.CM;
using PX.Objects.GL;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.PM;
using PX.Objects.TX;
using PX.Objects.EP;
using SOOrder = PX.Objects.SO.SOOrder;
using SOLine4 = PX.Objects.SO.SOLine4;
using PX.Objects.SO;
using System.Linq;
using CRLocation = PX.Objects.CR.Standalone.Location;
using SiteStatus = PX.Objects.IN.Overrides.INDocumentRelease.SiteStatus;
using LocationStatus = PX.Objects.IN.Overrides.INDocumentRelease.LocationStatus;
using LotSerialStatus = PX.Objects.IN.Overrides.INDocumentRelease.LotSerialStatus;
using ItemLotSerial = PX.Objects.IN.Overrides.INDocumentRelease.ItemLotSerial;
using SiteLotSerial = PX.Objects.IN.Overrides.INDocumentRelease.SiteLotSerial;
using PX.Objects.AP.MigrationMode;
using PX.Objects.Common;
using PX.CS.Contracts.Interfaces;
using PX.Data.DependencyInjection;

using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.GL.FinPeriods;
using PX.Objects.Common.Extensions;
using PX.Objects.PO.LandedCosts;
using System.Runtime.Serialization;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common.Bql;
using PX.Objects.Extensions.CostAccrual;
using PX.Data.BQL.Fluent;
using PX.Data.BQL;
using PX.Objects;
using PX.Objects.PO;

namespace PX.Objects.PO
{
    public class POReceiptEntryExt : PXGraphExtension<POReceiptEntry>
    {
        #region Event Handlers
        protected virtual void _(Events.RowSelected<POReceiptLine> e)
        {
            var row = e.Row as POReceiptLine;
            if (row == null) return;
            var rowExt = row.GetExtension<POReceiptLineExt>();
            if (string.IsNullOrEmpty(rowExt.UsrPOExtInfo) && !string.IsNullOrEmpty(row.POType) && !string.IsNullOrEmpty(row.PONbr) && row.POLineNbr != null)
            {
                var poline = SelectFrom<POLine>.Where<POLine.orderType.IsEqual<@P.AsString>.And<POLine.orderNbr.IsEqual<@P.AsString>>
                                                   .And<POLine.lineNbr.IsEqual<@P.AsInt>>>.View.Select(Base, row.POType, row.PONbr, row.POLineNbr).FirstTableItems.FirstOrDefault();
                if (poline != null)
                {
                    rowExt.UsrPOExtInfo = (poline.GetExtension<POLineExt>()).UsrPOExtInfo;
                }

            }



        }

        protected virtual void _(Events.RowUpdating<POReceiptLine> e)
        {
            var row = e.Row as POReceiptLine;
            if (row == null)
                return;
            var receiptExt = Base.Document.Current.GetExtension<POReceiptExt>();
            if(row.PONbr ==null &  receiptExt.UsrStyleTag!=null)
            {
                row.POAccrualSubID = receiptExt.UsrStyleTag;
            }
        }


        protected virtual void _(Events.RowPersisting<POReceipt> e)
        {
            var row = e.Row as POReceipt;
            if (row == null)
                return;
            var rowExt = row.GetExtension<POReceiptExt>();
            if(e.Operation==PXDBOperation.Insert || e.Operation==PXDBOperation.Update)
            {
                Vendor vendor = null;
                if (rowExt.UsrLoaded != true)

                {
                    vendor = SelectFrom<Vendor>.Where<Vendor.bAccountID.IsEqual<@P.AsInt>>.View.Select(Base, row.VendorID);
                    rowExt.UsrLoaded = true;
                }

                //if (vendor != null && vendor.VendorClassID.StartsWith("ACC"))
                //{
                //   if(string.IsNullOrWhiteSpace(rowExt.usr))
                //    {
                //        e.Cache.RaiseExceptionHandling<POReceiptExt.usrStyle>(e.Row, "", new PXSetPropertyException(CstDesignStudio.Descriptor.Messages.FillCannotBeBlank, PXErrorLevel.Error));
                //        e.Cancel = true;
                //    }
                //    if (string.IsNullOrWhiteSpace(rowExt.UsrBrand))
                //    {
                //        e.Cache.RaiseExceptionHandling<POReceiptExt.usrBrand>(e.Row, "", new PXSetPropertyException(CstDesignStudio.Descriptor.Messages.FillCannotBeBlank, PXErrorLevel.Error));
                //        e.Cancel = true;
                //    }
                //    if (string.IsNullOrWhiteSpace(rowExt.UsrBuyer))
                //    {
                //        e.Cache.RaiseExceptionHandling<POReceiptExt.usrBuyer>(e.Row, "", new PXSetPropertyException(CstDesignStudio.Descriptor.Messages.FillCannotBeBlank, PXErrorLevel.Error));
                //        e.Cancel = true;
                //    }                   


                //}
               
                var count = 0;
                foreach (POReceiptLine item in Base.transactions.Select())
                {
                    var poline = SelectFrom<POLine>.Where<POLine.orderType.IsEqual<@P.AsString>.And<POLine.orderNbr.IsEqual<@P.AsString>>
                                                     .And<POLine.lineNbr.IsEqual<@P.AsInt>>>.View.Select(Base, item.POType, item.PONbr, item.POLineNbr).FirstTableItems.FirstOrDefault();
                    if (poline != null)
                    {
                        count++;
                    }
                }
                if (count == 0)
                    rowExt.UsrType = CstDesignStudio.Descriptor.Messages.NFE;
                if(count>0 && rowExt.UsrType== CstDesignStudio.Descriptor.Messages.NFE)
                {
                    e.Cache.RaiseExceptionHandling<POReceiptExt.usrType>(e.Row, "", new PXSetPropertyException(CstDesignStudio.Descriptor.Messages.CannotChooseNFE, PXErrorLevel.Error));
                    e.Cancel = true;
                }
            }
           
        }
        #endregion
    }
}