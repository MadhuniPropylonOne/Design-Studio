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


namespace PX.Objects.AM
{
    public class MatlReqWizard1 : MatlWizard1
    {
        public PXSelect<INItemSite,
               Where<INItemSite.siteID, Equal<Required<INItemSite.siteID>>,
               And<INItemSite.inventoryID, Equal<Required<INItemSite.inventoryID>>>>> itemsite;

        [PXMergeAttributes(Method = MergeMethod.Merge)]
        [PXUIField(DisplayName = "Qty to Produce", Enabled = false)]
        protected virtual void AMProdItem_QtytoProd_CacheAttached(PXCache cache)
        {

        }

        public MatlReqWizard1()
        {
            var wizFilter = (WizFilter)filter.Cache.CreateCopy(filter.Current);
            OpenOrders.SetProcessDelegate(
                delegate (List<AMProdItem> list)
                {
#pragma warning disable PX1088 // Processing delegates cannot use the data views from processing graphs except for the data views of the PXFilter, PXProcessingBase, or PXSetup types
#if DEBUG
                    // OK to ignore PX1088 per Dmitry 11/19/2019 - Cause is use of MatlXref to set results for next wizard screen
#endif
                    FillMatlWrk(list, wizFilter);
#pragma warning restore PX1088
                }
            );
            OpenOrders.SetProcessCaption(Messages.Select);
            OpenOrders.SetProcessAllCaption(Messages.SelectAll);
            PXUIFieldAttribute.SetEnabled<AMProdItem.qtytoProd>(OpenOrders.Cache, null, false);
        }
        public new static void FillMatlWrk(List<AMProdItem> list, WizFilter filter)
        {
            var mw = PXGraph.CreateInstance<MatlReqWizard1>();
            mw.DeleteWrkTableRecs();
            mw.Clear(PXClearOption.ClearAll);
            mw.filter.Current = filter;

            foreach (var amproditem in list.OrderBy(x => x.StartDate).ThenBy(x => x.SchPriority))
            {
                FillMatlWrk(mw, amproditem);
            }

            mw.Persist();

            //redirect to step 2
            var mw2 = PXGraph.CreateInstance<MatlReqWizard2>();
            throw new PXRedirectRequiredException(mw2, "Transfer Material");
        }

        public new static void FillMatlWrk(AMProdItem amproditem, PXRedirectHelper.WindowMode windowMode)
        {
            var mw = PXGraph.CreateInstance<MatlReqWizard1>();
            mw.DeleteWrkTableRecs();
            mw.Clear(PXClearOption.ClearAll);

            FillMatlWrk(mw, amproditem);

            mw.Persist();

            //redirect to step 2
            var mw2 = PXGraph.CreateInstance<MatlReqWizard2>();
            PXRedirectHelper.TryRedirect(mw2, windowMode);
        }

        public static void FillMatlWrk(MatlReqWizard1 mw, AMProdItem amproditem)
        {
            mw.ProcessMatlWrk(amproditem);
        }

        protected new virtual void ProcessMatlWrk(AMProdItem amproditem)
        {
            if (amproditem == null || string.IsNullOrWhiteSpace(amproditem.ProdOrdID))
            {
                return;
            }
#if DEBUG
            var sw = System.Diagnostics.Stopwatch.StartNew();
            var counter = 0;
#endif
            foreach (PXResult<AMProdMatl, InventoryItem, AMOrderType> result in PXSelectJoin<
                            AMProdMatl,
                            InnerJoin<InventoryItem,
                                On<AMProdMatl.inventoryID, Equal<InventoryItem.inventoryID>>,
                            InnerJoin<AMOrderType,
                                On<AMProdMatl.orderType, Equal<AMOrderType.orderType>>>>,
                            Where<AMProdMatl.orderType, Equal<Required<AMProdMatl.orderType>>,
                                And<AMProdMatl.prodOrdID, Equal<Required<AMProdMatl.prodOrdID>>,
                                And<AMProdMatl.bFlush, NotEqual<True>,
                                And<AMProdMatl.qtyReq, NotEqual<decimal0>>>>>,
                            OrderBy<
                                Asc<AMProdMatl.sortOrder,
                                Asc<AMProdMatl.lineID>>>>
                            .Select(this, amproditem.OrderType, amproditem.ProdOrdID))
            {
#if DEBUG
                if (counter == 0)
                {
                    AMDebug.TraceWriteMethodName(PXTraceHelper.CreateTimespanMessage(sw.Elapsed, $"{amproditem.OrderType}:{amproditem.ProdOrdID} material query time"));
                    //skip query time from overall total
                    sw = System.Diagnostics.Stopwatch.StartNew();
                }
                counter++;
#endif
                // Cache for reuse in ProcessMatlWrk
                this.Caches[typeof(AMOrderType)].Current = (AMOrderType)result;

                ProcessMatlWrk(amproditem, result, result);
            }
#if DEBUG
            var elapsed = sw.Elapsed;
            var averageMs = counter == 0 ? 0 : elapsed.TotalMilliseconds / counter;
            var averageTicks = counter == 0 ? 0 : elapsed.Ticks / counter;
            AMDebug.TraceWriteMethodName(PXTraceHelper.CreateTimespanMessage(elapsed, $"{amproditem.OrderType}:{amproditem.ProdOrdID}; Material Counter = {counter}; Avg milliseconds = {averageMs}; Total Ticks = {elapsed.Ticks}; Avg Ticks = {averageTicks}"));
#endif
        }
        protected new virtual void ProcessMatlWrk(AMProdItem amproditem, AMProdMatl amprodmatl, InventoryItem inventoryItem)
        {
            if (amprodmatl == null || amprodmatl.SubcontractSource == AMSubcontractSource.VendorSupplied || inventoryItem == null)
            {
                return;
            }

            var orderType = (AMOrderType)this.Caches[typeof(AMOrderType)].Current;

            var totalQtyReq = GetMaterialTotalQty(amprodmatl, amproditem.BaseQtytoProd);
            if (totalQtyReq == 0)
            {
                return;
            }

            INItemSite itemSite = this.itemsite.SelectWindowed(0, 1, amproditem.SiteID, amprodmatl.InventoryID);
            if (itemSite != null)
            {
                amprodmatl.LocationID = itemSite.DfltReceiptLocationID;
            }
            else
            {
                INSite site = INSite.PK.Find(this, amprodmatl.SiteID);
                if (site != null)
                {
                    amprodmatl.LocationID = site.ReceiptLocationID;
                }
            }

            var amWrkMatl = PXCache<AMWrkMatl>.CreateCopy(MatlXref.Insert(new AMWrkMatl()));
            amWrkMatl.BFlush = amprodmatl.BFlush;
            amWrkMatl.Descr = amprodmatl.Descr;
            amWrkMatl.InventoryID = amprodmatl.InventoryID;
            amWrkMatl.SubItemID = amprodmatl.SubItemID;
            amWrkMatl.LineID = amprodmatl.LineID;
            amWrkMatl.OperationID = amprodmatl.OperationID;
            amWrkMatl.OrderType = amprodmatl.OrderType;
            amWrkMatl.ProdOrdID = amprodmatl.ProdOrdID;
            amWrkMatl.IsByproduct = amprodmatl.IsByproduct;
            amWrkMatl.SiteID = amprodmatl.SiteID;

            amWrkMatl.LocationID = amprodmatl.LocationID;

            amWrkMatl.UOM = amprodmatl.UOM;
            amWrkMatl.UserID = Accessinfo.UserID;
            amWrkMatl.QtyReq = totalQtyReq;
            amWrkMatl.QtyRemaining = amprodmatl.QtyRemaining;
            amWrkMatl.BaseQtyRemaining = amprodmatl.BaseQtyRemaining;
            amWrkMatl.OverIssueMaterial = orderType?.OverIssueMaterial ?? SetupMessage.AllowMsg;
            amWrkMatl.SubcontractSource = amprodmatl.SubcontractSource;

            var avilQtyInBaseUnits = GetMaterialBaseQtyAvail(amprodmatl);
            amWrkMatl.QtyAvail = avilQtyInBaseUnits;
            avilQtyInBaseUnits -= GetBaseQtyUsed(amWrkMatl);

            if (UomHelper.TryConvertFromBaseQty<AMWrkMatl.inventoryID>(this.Caches<AMWrkMatl>(), amWrkMatl,
                amWrkMatl.UOM,
                avilQtyInBaseUnits,
                out var availQtyInProdUnits))
            {
                amWrkMatl.QtyAvail = availQtyInProdUnits.GetValueOrDefault();
            }

            amWrkMatl.MatlQty = amWrkMatl.QtyReq.GetValueOrDefault();
            if (amWrkMatl.QtyAvail.GetValueOrDefault() < amWrkMatl.QtyReq.GetValueOrDefault() &&
                inventoryItem.StkItem.GetValueOrDefault() && !inventoryItem.NegQty.GetValueOrDefault() &&
                !amWrkMatl.IsByproduct.GetValueOrDefault())
            {
                amWrkMatl.MatlQty = amWrkMatl.QtyAvail.GetValueOrDefault();
            }

            // Not allowed to over issue so adjust (as users can change production qty on wizard 1 which drive displayed qty shown on wizard 2)
            if (amWrkMatl.OverIssueMaterial == Attributes.SetupMessage.ErrorMsg && amWrkMatl.MatlQty.GetValueOrDefault() > amWrkMatl.QtyRemaining.GetValueOrDefault())
            {
                amWrkMatl.MatlQty = amWrkMatl.QtyRemaining.GetValueOrDefault();
            }

            if (amWrkMatl.QtyAvail.GetValueOrDefault() < 0)
            {
                amWrkMatl.QtyAvail = 0m;
            }

            var isOrderTypeCheckUnreleasedBatchQty = amWrkMatl.OverIssueMaterial != SetupMessage.AllowMsg && orderType?.IncludeUnreleasedOverIssueMaterial == true;
            var wizardCheckUnreleasedBatchQty = filter?.Current?.ExcludeUnreleasedBatchQty == true;
            if (wizardCheckUnreleasedBatchQty || isOrderTypeCheckUnreleasedBatchQty)
            {
                amWrkMatl.BaseUnreleasedBatchQty = ProductionTransactionHelper.GetUnreleasedMaterialBaseQty(this, amprodmatl);
                amWrkMatl.UnreleasedBatchQty = amWrkMatl.BaseUnreleasedBatchQty;
                if (amWrkMatl.BaseUnreleasedBatchQty != 0 && inventoryItem?.BaseUnit != null &&
                    !inventoryItem.BaseUnit.EqualsWithTrim(amprodmatl.UOM) && UomHelper.TryConvertFromBaseQty<AMProdMatl.inventoryID>(
                        this.Caches<AMProdMatl>(),
                        amprodmatl,
                        amprodmatl.UOM,
                        amWrkMatl.BaseUnreleasedBatchQty.GetValueOrDefault(),
                        out var unreleasedQty))
                {
                    amWrkMatl.UnreleasedBatchQty = unreleasedQty.GetValueOrDefault();
                }

                if (amWrkMatl.UnreleasedBatchQty.GetValueOrDefault() > 0m &&
                        wizardCheckUnreleasedBatchQty ||
                        // If warn message then we will let the qty be what the qty is - wizard2 will show a warning on the qty
                        (isOrderTypeCheckUnreleasedBatchQty && amWrkMatl.OverIssueMaterial == SetupMessage.ErrorMsg))
                {
                    amWrkMatl.MatlQty = amWrkMatl.MatlQty.GetValueOrDefault() -
                                        amWrkMatl.UnreleasedBatchQty.GetValueOrDefault();
                }
            }

#if DEBUG
            var shortageMsg = amWrkMatl.MatlQty.GetValueOrDefault() < amWrkMatl.QtyReq.GetValueOrDefault()
                ? "**Shortage**"
                : string.Empty;
            AMDebug.TraceWriteLine(
                $"{shortageMsg}[{amWrkMatl.OrderType.TrimIfNotNullEmpty()}-{amWrkMatl.ProdOrdID.TrimIfNotNullEmpty()}-({amWrkMatl.OperationID.GetValueOrDefault()})-{inventoryItem.InventoryCD.TrimIfNotNullEmpty()}] MatlQty = {amWrkMatl.MatlQty.GetValueOrDefault()} {amWrkMatl.UOM.TrimIfNotNullEmpty()}; QtyReq = {amWrkMatl.QtyReq.GetValueOrDefault()} {amWrkMatl.UOM.TrimIfNotNullEmpty()}; QtyAvail = {amWrkMatl.QtyAvail.GetValueOrDefault()} {amWrkMatl.UOM.TrimIfNotNullEmpty()}");
#endif
            if (amWrkMatl.MatlQty.GetValueOrDefault() > 0)
            {
                MatlXref.Update(amWrkMatl);
                return;
            }

            if (MatlXref.Cache.GetStatus(amWrkMatl) == PXEntryStatus.Inserted)
            {
                MatlXref.Cache.Remove(amWrkMatl);
                return;
            }

            MatlXref.Delete(amWrkMatl);
        }
    }
}
