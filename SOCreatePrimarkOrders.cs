using System;
using System.Collections;
using System.Collections.Generic;
using PX.Common;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Objects.AM.CacheExtensions;
using PX.Objects.CS;
using PX.Objects.IN;

namespace PX.Objects.SO
{
  public class SOCreatePrimarkOrders : PXGraph<SOCreatePrimarkOrders, PrimarkOrders> 
  {

        #region Public Selects
        [PXImport(typeof(PrimarkOrders))]
        public SelectFrom<PrimarkOrders>.View PrimarkOrders;
        
        public SelectFrom<PrimarkOrders>
            .Where<PrimarkOrders.customerOrderNbr.IsEqual<PrimarkOrders.customerOrderNbr.FromCurrent>>.View CurrentDocument;
        #endregion
        int sizelimit;

        #region Constructor
        public SOCreatePrimarkOrders()
        {
            clearLines.SetEnabled(false);
            validateLines.SetEnabled(false);
            createOrders.SetEnabled(false);
            sizelimit = 14;
        }
        #endregion
        #region Actions
        public PXAction<PrimarkOrders> clearLines;
        [PXButton(CommitChanges = true), PXUIField(DisplayName = "Clear",
            Enabled = true, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
        protected virtual IEnumerable ClearLines(PXAdapter adapter)
        {
            PrimarkOrders row = PrimarkOrders.Current;
            PXCache cache = PrimarkOrders.Cache;
            if (row == null) return adapter.Get();
            var res = PrimarkOrders.Select();
            foreach (PXResult<PrimarkOrders> rec in res)
            {
                PrimarkOrders orders = (PrimarkOrders)rec;
                cache.Delete(orders);
            }
            this.Actions.PressSave();
            validateLines.SetEnabled(false);
            createOrders.SetEnabled(false);
            clearLines.SetEnabled(false);
            return adapter.Get();
        }

        public PXAction<PrimarkOrders> validateLines;
        [PXButton(CommitChanges = true), PXUIField(DisplayName = "Validate",
            Enabled = true, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
        protected virtual IEnumerable ValidateLines(PXAdapter adapter)
        {
            PrimarkOrders row = PrimarkOrders.Current;
            PXCache cache = PrimarkOrders.Cache;
            PXGraph graph = cache.Graph;
            int? branchID;
            int? siteID;

            if (row == null) return adapter.Get();
            var res = PrimarkOrders.Select();
            if (res != null)
            {
                PXLongOperation.StartOperation(this, delegate ()
                {
                    try
                    {
                        foreach (PXResult<PrimarkOrders> rec in res)
                        {
                            PrimarkOrders orders = (PrimarkOrders)rec;
                            branchID = PXAccess.GetBranchID((string)orders.Branch?.ToUpper());
                            siteID = GetSite(orders.Warehouse?.ToUpper());

                            //Validate all un-processed rows.
                            if (orders.Processed == false)
                            {
                                //Clear previous Remarks
                                orders.Remark = null;
                                orders.Error = false;

                                
                                if (orders.Value.Contains("RATIO")|| orders.Value.Contains("TOTAL"))
                                {
                                    orders.Active = true;
                                    orders.Processed = true;
                                    orders.Remark = "Information Row Only:"+ orders.Value;
                                }
                                else
                                {
                                    if (orders.InventoryCD == null)
                                    {
                                        orders.Remark = "Error: Inventory ID Required";
                                    }
                                    else
                                    {
                                        IN.InventoryItem item = PXSelect<IN.InventoryItem,
                                        Where<IN.InventoryItem.inventoryCD, Equal<Required<IN.InventoryItem.inventoryCD>>>>.Select(this, orders.InventoryCD);
                                        if (item == null) orders.Remark = "Error: Inventory ID not in the System";
                                        if (item != null)
                                        {
                                            IN.InventoryItemExt itemExt = PXCache<IN.InventoryItem>.GetExtension<IN.InventoryItemExt>(item);
                                            orders.StyleTag = itemExt.UsrStyleTag;                                 
                                            orders.InventoryID = item.InventoryID;
                                            /// Currency Unit Price
                                            if ((orders.CuryUnitPrice == null) || (orders.CuryUnitPrice == 0))
                                            {
                                                orders.Remark += "|Warning: No Currency Unit Price";
                                                orders.UnitCost = item.BasePrice;
                                                // Check Unit Cost
                                                if ((orders.UnitCost == null)||(orders.UnitCost== 0)) 
                                                    orders.Remark += "|Warning: No default price defined for the stock item";

                                            }
                                        }
                                    }
                                    /// Check Delivery Date
                                    if(orders.Delivery is null) orders.Remark += "|Warning: No Delivery Date";
                                    /// Check Branch
                                    if (orders.Branch != null)
                                    {
                                        if (branchID == null)
                                        {
                                            orders.Remark += "|Error: Invalid Branch";
                                        }
                                    }
                                    /// Check Warehouse
                                    if (orders.Warehouse != null)
                                    {
                                        if (siteID == null)
                                        {
                                            orders.Remark += "|Error: Invalid Warehouse";
                                        }
                                    }
                                    /// Packing Method
                                    if (!PackingMethodNullOrValid(orders))
                                    {
                                        orders.Remark += "|Error: Invalid Packing Method";
                                    }
                                    /// Ship Via
                                    if (!ShipViaNullOrValid(orders))
                                    {
                                        orders.Remark += "|Error: Invalid Ship Via";
                                    }
                                    orders.Active = true;
                                    orders.Processed = (false);
                                    orders.Error = (orders.Remark != null && orders.Remark.Contains("Error"));



                                }
                                orders = (PrimarkOrders)graph.Caches[typeof(PrimarkOrders)].Update(orders);
                                
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        throw new PXException(e.Message); 
                    }
                    graph.Actions.PressSave();
                });
            }
            return adapter.Get();
        }

        public PXAction<PrimarkOrders> createOrders;
        [PXButton(CommitChanges = true), PXUIField(DisplayName = "Create SO",
            Enabled = true, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
        protected virtual IEnumerable CreateOrders(PXAdapter adapter)
        {
            PrimarkOrders row = PrimarkOrders.Current;
            PXCache cache = PrimarkOrders.Cache;
            PXGraph pmgraph = cache.Graph;
            if (row == null) return adapter.Get();
            var res = PrimarkOrders.Select();
            if (res.Count > 0)
            {
                // Unpack Sizes
                //TODO - Take values Dynamically
                List<String> sizeList = new List<string> { "0S04", "0S06", "0S08", "0S10", "0S12", "0S14", "0S16", "0S18", "0S20", "0XS", "0S", "0M", "0L", "0XL" };
                List<int?> subList = new List<int?>();
                for (int i = 0; i < sizelimit; i++)
                {
                    // Get SubItem ID
                    IN.INSubItem sub = PXSelect<IN.INSubItem,
                        Where<IN.INSubItem.subItemCD, Equal<Required<IN.INSubItem.subItemCD>>>>.Select(this, sizeList[i]);
                    subList.Add(sub.SubItemID);
                }

                PXLongOperation.StartOperation(this, delegate ()
                {
                    try
                    {
                        foreach (PXResult<PrimarkOrders> rec in res)
                        {
                            PrimarkOrders orders = (PrimarkOrders)rec;
                            if ((orders != null) && (orders.Active == true) && (orders.Processed == false) && (orders.Error == false))
                            {
                                // Unpack Sizes
                                //TODO - Take values Dynamically
                                List<int?> valList = new List<int?>{
                                    orders.S4, orders.S6, orders.S8, orders.S10, orders.S12,
                                    orders.S14, orders.S16, orders.S18, orders.S20, 
                                    orders.XS, orders.S, orders.M, orders.L, orders.XL };

                                string custref = GetCustomerRef(orders);
                                                               
                                // Create SO Header
                                SOOrderEntry sograph = PXGraph.CreateInstance<SOOrderEntry>();
                                var so = (SOOrder)sograph.Caches[typeof(SOOrder)].Insert(new SOOrder
                                {
                                    OrderType = "SO",
                                    CustomerID = 8382,
                                    BranchID = PXAccess.GetBranchID((string)orders.Branch.ToUpper()),
                                    CustomerOrderNbr = orders.CustomerOrderNbr,
                                    CustomerRefNbr = custref,
                                    RequestDate = orders.Delivery,
                                    OrderDesc = orders.OrderDesc,
                                    ShipVia = orders.ShipVia                       
                                });;
                                SOOrderExt soExt = PXCache<SOOrder>.GetExtension<SOOrderExt>(so);
                                soExt.UsrStyleTag = orders.StyleTag;
                                soExt.UsrCustStyleRef = orders.BrandStyle;
                                soExt.UsrPackingMethod = orders.PackingMethod;
                                soExt.UsrKimballNbr = orders.KimballNbr;
                                soExt = (SOOrderExt)sograph.Caches[typeof(SOOrderExt)].Update(soExt);

                                //Create SO Lines
                                for (int i = 0; i < sizelimit; i++)
                                {
                                    if (valList[i] != null)
                                    {
                                        var soLine = (SOLine)sograph.Caches[typeof(SOLine)].Insert(new SOLine
                                        {
                                            OrderType = so.OrderType,
                                            OrderNbr = so.OrderNbr,
                                            BranchID = so.BranchID,
                                            CustomerID = so.CustomerID,
                                            OrderDate = so.OrderDate,
                                            InventoryID = orders.InventoryID,
                                            SubItemID = subList[i],
                                            OrderQty = valList[i] ?? 0,
                                            BaseOrderQty = valList[i] ?? 0,
                                            UnitCost = orders.UnitCost,
                                            RequestDate = so.RequestDate,
                                            SiteID = GetSite(orders.Warehouse.ToUpper()),
                                            CuryUnitPrice = orders.CuryUnitPrice
                                        });
                                        SOLineExt lineExt = PXCache<SOLine>.GetExtension<SOLineExt>(soLine);
                                        lineExt.UsrPackingID = orders.PackNbr;
                                        lineExt.UsrPacksBaseQty = orders.RatioPack;
                                        lineExt.UsrPackRatio = valList[i] / orders.RatioPack;                                    
                                        soLine = (SOLine)sograph.Caches[typeof(SOLine)].Update(soLine);
                                        lineExt = (SOLineExt)sograph.Caches[typeof(SOLineExt)].Update(lineExt);
                                    }
                                }
                                sograph.Actions.PressSave();
                                sograph.releaseFromHold.Press();
                                orders.OrderNbr = so.OrderNbr;
                                orders.Processed = (orders.Error == false);
                                orders = (PrimarkOrders)pmgraph.Caches[typeof(PrimarkOrders)].Update(orders);
                            }
                        }
                        pmgraph.Actions.PressSave();


                    }
                    catch (Exception e)
                    {
                        throw new PXException(e.Message);
                    }

                });
            }
            return adapter.Get();
        }
        #endregion
        #region Event Handlers
        protected void _(Events.RowSelected<PrimarkOrders> e)
        {
            var row = e.Row as PrimarkOrders;
            if (row == null) return;
            var res = PrimarkOrders.Select();
            clearLines.SetEnabled(res.Count > 0);
            validateLines.SetEnabled(res.Count > 0 && e.Cache.IsDirty == false && CanValidateOrders(this) == true);
            createOrders.SetEnabled(res.Count > 0 && e.Cache.IsDirty == false && CanCreateOrders(this) == true);
            //e.Cache.AllowInsert = false;
            //e.Cache.AllowInsert = false;
        }
        protected void _(Events.FieldSelecting<PrimarkOrders.lineID> e)
        {
            if (e.Row != null && e.ReturnValue is int?)
            {
                if ((e.ReturnValue as int?).GetValueOrDefault() < 0)
                {
                    e.ReturnValue = null;
                }
            }
        }


        #endregion

        private bool CanCreateOrders(SOCreatePrimarkOrders graph)
            {
                var res = PrimarkOrders.Select();
                if (res.Count <= 0) return false;
                foreach (PXResult<PrimarkOrders> rec in res)
                {
                    PrimarkOrders orders = (PrimarkOrders)rec;
                    if ((orders != null) && (orders.Active == true) && (orders.Processed == false) && (orders.Error == false))
                    {
                        return true;
                    }
                }
                return false;
            }
        private bool CanValidateOrders(SOCreatePrimarkOrders graph)
        {
            var res = PrimarkOrders.Select();
            if (res.Count <= 0) return false;
            foreach (PXResult<PrimarkOrders> rec in res)
            {
                PrimarkOrders orders = (PrimarkOrders)rec;
                if ((orders != null) && (orders.Processed == false))
                {
                    return true;
                }
            }
            return false;
        }

        private bool PackingMethodNullOrValid(PrimarkOrders orders)
        {
            if (string.IsNullOrWhiteSpace(orders.PackingMethod)) return true;
            if (orders.PackingMethod.Equals("F", StringComparison.OrdinalIgnoreCase) || orders.PackingMethod.Equals("G", StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }
        private bool ShipViaNullOrValid(PrimarkOrders orders)
        {
            if (string.IsNullOrWhiteSpace(orders.ShipVia)) return true;
            var rec = PXSelect<Carrier,
                            Where<Carrier.carrierID, Equal<Required<Carrier.carrierID>>>>.Select(this, orders.ShipVia.ToUpper());

            return (rec.CheckedAny());
        }
        protected int? GetSite(string siteCd)
        {
            if (siteCd == null)
            {
                return null;
            }

            INSite site = PXSelect<INSite, Where<INSite.siteCD, Equal<Required<INSite.siteCD>>>>.Select(this, siteCd);

            if (site?.SiteID != null)
            {
                return site.SiteID;
            }

            return null;
        }
        protected string GetCustomerRef(PrimarkOrders orders)
        {
            string custref = null;
            /// Set CustomerRef (Destination Ref)
            if (!String.IsNullOrWhiteSpace(orders.Country))
            {
                custref = orders.Country;
            }
            if (!String.IsNullOrWhiteSpace(orders.DestinationNbr))
            {
                if (String.IsNullOrWhiteSpace(custref))
                {
                    custref = orders.DestinationNbr;
                }
                else
                {
                    custref = custref + "-" + orders.DestinationNbr;
                }
            }
            if (!String.IsNullOrWhiteSpace(orders.PackNbr))
            {
                if (String.IsNullOrWhiteSpace(custref))
                {
                    custref = orders.PackNbr;
                }
                else
                {
                    custref = custref + "-" + orders.PackNbr;
                }
            }
            if (custref.Length > 40)
            {
                custref.Substring(1, 40);
            }

            return custref;
        }
    }
}