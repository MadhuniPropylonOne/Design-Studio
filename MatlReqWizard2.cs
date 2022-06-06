using System;
using PX.Data;
using System.Collections.Generic;
using PX.Objects.CS;
using System.Collections;
using System.Linq;
using PX.Objects.IN;
using PX.Objects.AM.Attributes;
using PX.Objects.AM.GraphExtensions;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AM.CacheExtensions;
using PX.Objects.PM;
using PX.Data.BQL.Fluent;
using PX.Data.BQL;
using PX.Objects.Common.Extensions;

namespace PX.Objects.AM
{
    public class MatlReqWizard2 : MatlWizard2
    {

        //public PXProcessing<AMWrkMatl, Where<AMWrkMatl.userID, Equal<Current<AccessInfo.userID>>>> ProcessMatl;
        public static PXSelectJoin<InventoryItem,
                            LeftJoin<AM.AMProdItem, On<AM.AMProdItem.inventoryID, Equal<InventoryItem.inventoryID>>>,
                            Where<AM.AMProdItem.prodOrdID, Equal<Required<AM.AMProdItem.prodOrdID>>>> MFGItem;
        [PXMergeAttributes(Method = MergeMethod.Merge)]
        [PXUIField(DisplayName = "Requested Qty")]
        protected virtual void AMWrkMatl_MatlQty_CacheAttached(PXCache cache)
        {

        }

        public MatlReqWizard2()
        {
            ProcessMatl.SetProcessDelegate(BuildMaterialTransaction);
            ProcessMatl.SetProcessCaption(Messages.Select);
            ProcessMatl.SetProcessAllCaption(Messages.SelectAll);
            PXUIFieldAttribute.SetEnabled<AMWrkMatl.uOM>(ProcessMatl.Cache, null, true);
            PXUIFieldAttribute.SetEnabled<AMWrkMatl.matlQty>(ProcessMatl.Cache, null, true);
        }
        public new static void BuildMaterialTransaction(List<AMWrkMatl> list)
        {
            // Create an instance of the INTransferEntry graph.
            var transferEntry = CreateInstance<INTransferEntry>();
            // Initialize the summary of the transfer.
            var doc = new INRegister() { TransferType = INTransferType.OneStep };
            // Fetch Extension
            var extDoc = PXCache<INRegister>.GetExtension<IN.INRegisterExt>(doc);
            //int? siteID = 0;
            //Copy data record for validation
            var buildDoc = doc;
            ValidateAndBuildSummary(list, doc, extDoc, out buildDoc);

            //Set Validated InRegister Summary values
            doc.SiteID = buildDoc.SiteID;
            doc.ToSiteID = buildDoc.SiteID;
            doc.TranDesc = buildDoc.TranDesc;
            doc = transferEntry.CurrentDocument.Insert(doc);

            try
            {

                foreach (var wrkMatl in list)
                {

                    var inTran = transferEntry.transactions.Insert();
                    inTran.SiteID = doc.SiteID;
                    inTran.ToSiteID = doc.SiteID;
                    inTran.InventoryID = wrkMatl.InventoryID;
                    inTran.SubItemID = wrkMatl.SubItemID;
                    inTran.LocationID = wrkMatl.LocationID;
                    inTran.UOM = wrkMatl.UOM;
                    inTran.Qty = wrkMatl.MatlQty.GetValueOrDefault();
                    //inTran.CuryUnitPrice = line.BasePrice;

                    if (inTran.ToLocationID == null) transferEntry.transactions.Cache.SetDefaultExt<INTran.toLocationID>(inTran);
                    if (inTran.LocationID == null) transferEntry.transactions.Cache.SetDefaultExt<INTran.locationID>(inTran);
                    if (inTran.TranDesc == null) transferEntry.transactions.Cache.SetDefaultExt<INTran.tranDesc>(inTran);
                    transferEntry.transactions.Update(inTran);

                }
                //throw new PXException("Before1");
                //MatlTransferBuilder.CreateMaterialTransaction(rm, inTrans, null);


                //rm.Persist();*/


            }
            catch (Exception e)
            {
                PXTraceHelper.PxTraceException(e);
                throw new PXException(Messages.MaterialWizardBatchCreationError);
            }
            transferEntry.CurrentDocument.Update(doc);
            throw new PXRedirectRequiredException(transferEntry, "Document", true);
        }

        protected static void ValidateAndBuildSummary(List<AMWrkMatl> list, INRegister doc, IN.INRegisterExt extDoc, out INRegister buildDoc)
        {
            //Copy data record for validation
            var valDoc = doc;
            int? valMFGItemID = 0;

            valDoc.TotalQty = 0;
            valDoc.ExtRefNbr = "";
            string prodOrdList = "-";

            // Check if all lines associated with the Material Request are from the same warehouse & same style
            foreach (var wrkMatl in list)
            {
                if (valDoc.SiteID == null || valDoc.SiteID == 0) valDoc.SiteID = wrkMatl.SiteID;
                if (valDoc.SiteID != wrkMatl.SiteID)
                {
                    throw new PXSetPropertyException("Multiple Warehouses: Materials to be transferred should be from one warehouse. Failed to create a transfer request", PXErrorLevel.RowError);
                }
                //Fetch Inventory Info
                InventoryItem inItem = (InventoryItem)MFGItem.Select(wrkMatl.ProdOrdID);

                if (valMFGItemID != inItem.InventoryID && !(valMFGItemID == null || valMFGItemID == 0))
                {
                    throw new PXSetPropertyException("Multiple Styles: Materials to be transferred should be for the same Style. Failed to create a transfer request", PXErrorLevel.RowError);
                }

                var extMFGItem = PXCache<InventoryItem>.GetExtension<IN.InventoryItemExt>(inItem);
                extDoc.UsrStyleTag = extMFGItem.UsrStyleTag;
                valDoc.SiteID = wrkMatl.SiteID;
                valMFGItemID = inItem.InventoryID;
                valDoc.TranDesc = "Material Transfer Request for " + inItem.InventoryCD;

                if ((prodOrdList.Contains(wrkMatl.ProdOrdID)) == false)
                {
                    if ((prodOrdList == "-") && !(wrkMatl.ProdOrdID == null))
                    {
                        prodOrdList = wrkMatl.ProdOrdID;
                    }
                    else
                    {
                        prodOrdList = prodOrdList + "|" + wrkMatl.ProdOrdID;
                    }
                }

            }
            valDoc.TranDesc = valDoc.TranDesc + prodOrdList;
            buildDoc = valDoc;
        }

    }
}