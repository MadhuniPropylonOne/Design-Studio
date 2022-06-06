using System;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.EP;

namespace PX.Objects.SO
{
    public class SOShipmentEntryExt : PXGraphExtension<SOShipmentEntry>
    {
        #region Event Handlers
        public delegate void ValidateShipmentDelegate(SOShipment shiporder);
        [PXOverride]
        public void ValidateShipment(SOShipment shiporder, ValidateShipmentDelegate baseMethod)
        {
            baseMethod(shiporder);
            if (shiporder == null) return;
            var shipmentExt = shiporder.GetExtension<SOShipmentExt>();
            if (shipmentExt == null) return;
            if (shipmentExt.UsrFullyReconciled != true || shipmentExt.UsrQAChecked != true)
            {
                throw new PXException(CstDesignStudio.Descriptor.Messages.ShipInspecIncomplete, shiporder.ShipmentNbr);
            }

        }


        protected void SOShipment_RowSelected(PXCache cache, PXRowSelectedEventArgs e, PXRowSelected InvokeBaseHandler)
        {
            if (InvokeBaseHandler != null)
                InvokeBaseHandler(cache, e);
            var row = (SOShipment)e.Row;
            //Set Inspection Default Data
            if (row == null) return;
            var shipmentExt = row.GetExtension<SOShipmentExt>();
            if (shipmentExt == null) return;
            if (shipmentExt.UsrSupersedeChecked == true)
            {
                PXUIFieldAttribute.SetEnabled<SOShipmentExt.usrLSReconChecked>(cache, e.Row, false);
                PXUIFieldAttribute.SetEnabled<SOShipmentExt.usrFMReconChecked>(cache, e.Row, false);
                PXUIFieldAttribute.SetEnabled<SOShipmentExt.usrPMReconChecked>(cache, e.Row, false);
                PXUIFieldAttribute.SetEnabled<SOShipmentExt.usrCDReconChecked>(cache, e.Row, false);
            }
            else
            {
                PXUIFieldAttribute.SetEnabled<SOShipmentExt.usrLSReconChecked>(cache, e.Row, true);
                PXUIFieldAttribute.SetEnabled<SOShipmentExt.usrFMReconChecked>(cache, e.Row, true);
                PXUIFieldAttribute.SetEnabled<SOShipmentExt.usrPMReconChecked>(cache, e.Row, true);
                PXUIFieldAttribute.SetEnabled<SOShipmentExt.usrCDReconChecked>(cache, e.Row, true);
            }
            if ((shipmentExt.UsrLSReconChecked == true && shipmentExt.UsrFMReconChecked == true && shipmentExt.UsrPMReconChecked == true && shipmentExt.UsrCDReconChecked == true) ||
                (shipmentExt.UsrSupersedeChecked == true))
            {
                cache.SetValueExt<SOShipmentExt.usrFullyReconciled>(e.Row, true);
            }
            else
            {
                cache.SetValueExt<SOShipmentExt.usrFullyReconciled>(e.Row, false);
            }

        }



        protected void SOShipment_UsrCDReconChecked_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e, PXFieldUpdated InvokeBaseHandler)
        {
            if (InvokeBaseHandler != null)
                InvokeBaseHandler(cache, e);
            var row = (SOShipment)e.Row;
            //Set Inspection Default Data
            if (row == null) return;

            var shipmentExt = row.GetExtension<SOShipmentExt>();

            if (shipmentExt == null) return;
            if (shipmentExt.UsrCDReconChecked == true)
            {
                EPEmployee defEmployee = PXSelect<EPEmployee, Where<EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>.Select(this.Base);

                cache.SetValueExt<SOShipmentExt.usrCDReconDate>(e.Row, cache.Graph.Accessinfo.BusinessDate.GetValueOrDefault());
                if (defEmployee != null)
                    cache.SetValueExt<SOShipmentExt.usrCDReconByID>(e.Row, defEmployee.BAccountID);
            }
            else
            {

                cache.SetValueExt<SOShipmentExt.usrCDReconDate>(e.Row, null);
                cache.SetValueExt<SOShipmentExt.usrCDReconByID>(e.Row, null);

            }

        }

        protected void SOShipment_UsrFMReconChecked_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e, PXFieldUpdated InvokeBaseHandler)
        {
            if (InvokeBaseHandler != null)
                InvokeBaseHandler(cache, e);
            var row = (SOShipment)e.Row;
            //Set Inspection Default Data
            if (row == null) return;

            var shipmentExt = row.GetExtension<SOShipmentExt>();

            if (shipmentExt == null) return;
            if (shipmentExt.UsrFMReconChecked == true)
            {
                EPEmployee defEmployee = PXSelect<EPEmployee, Where<EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>.Select(this.Base);

                cache.SetValueExt<SOShipmentExt.usrFMReconDate>(e.Row, cache.Graph.Accessinfo.BusinessDate.GetValueOrDefault());
                if (defEmployee != null)
                    cache.SetValueExt<SOShipmentExt.usrFMReconByID>(e.Row, defEmployee.BAccountID);
            }
            else
            {
                cache.SetValueExt<SOShipmentExt.usrFMReconDate>(e.Row, null);
                cache.SetValueExt<SOShipmentExt.usrFMReconByID>(e.Row, null);

            }

        }



        protected void SOShipment_UsrLSReconChecked_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e, PXFieldUpdated InvokeBaseHandler)
        {
            if (InvokeBaseHandler != null)
                InvokeBaseHandler(cache, e);
            var row = (SOShipment)e.Row;
            //Set Inspection Default Data
            if (row == null) return;

            var shipmentExt = row.GetExtension<SOShipmentExt>();

            if (shipmentExt == null) return;
            if (shipmentExt.UsrLSReconChecked == true)
            {
                EPEmployee defEmployee = PXSelect<EPEmployee, Where<EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>.Select(this.Base);

                cache.SetValueExt<SOShipmentExt.usrLSReconDate>(e.Row, cache.Graph.Accessinfo.BusinessDate.GetValueOrDefault());
                if (defEmployee != null)
                    cache.SetValueExt<SOShipmentExt.usrLSReconByID>(e.Row, defEmployee.BAccountID);
            }
            else
            {
                cache.SetValueExt<SOShipmentExt.usrLSReconDate>(e.Row, null);
                cache.SetValueExt<SOShipmentExt.usrLSReconByID>(e.Row, null);

            }

        }

        protected void SOShipment_UsrPMReconChecked_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e, PXFieldUpdated InvokeBaseHandler)
        {
            if (InvokeBaseHandler != null)
                InvokeBaseHandler(cache, e);
            var row = (SOShipment)e.Row;
            //Set Inspection Default Data
            if (row == null) return;

            var shipmentExt = row.GetExtension<SOShipmentExt>();

            if (shipmentExt == null) return;
            if (shipmentExt.UsrPMReconChecked == true)
            {
                EPEmployee defEmployee = PXSelect<EPEmployee, Where<EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>.Select(this.Base);

                cache.SetValueExt<SOShipmentExt.usrPMReconDate>(e.Row, cache.Graph.Accessinfo.BusinessDate.GetValueOrDefault());
                if (defEmployee != null)
                    cache.SetValueExt<SOShipmentExt.usrPMReconByID>(e.Row, defEmployee.BAccountID);
            }
            else
            {
                cache.SetValueExt<SOShipmentExt.usrPMReconDate>(e.Row, null);
                cache.SetValueExt<SOShipmentExt.usrPMReconByID>(e.Row, null);

            }

        }

        protected void SOShipment_UsrQAChecked_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e, PXFieldUpdated InvokeBaseHandler)
        {
            if (InvokeBaseHandler != null)
                InvokeBaseHandler(cache, e);
            var row = (SOShipment)e.Row;

            //Set Inspection Default Data
            if (row == null) return;

            var shipmentExt = row.GetExtension<SOShipmentExt>();

            if (shipmentExt == null) return;
            if (shipmentExt.UsrQAChecked == true)
            {
                EPEmployee defEmployee = PXSelect<EPEmployee, Where<EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>.Select(this.Base);

                cache.SetValueExt<SOShipmentExt.usrQACheckedDate>(e.Row, cache.Graph.Accessinfo.BusinessDate.GetValueOrDefault());
                if (defEmployee != null)
                    cache.SetValueExt<SOShipmentExt.usrQACheckedByID>(e.Row, defEmployee.BAccountID);
            }
            else
            {
                //shipmentExt.GetExtension<SOShipmentExt>().UsrQACheckedDate = null;
                //shipmentExt.GetExtension<SOShipmentExt>().UsrQACheckedByID = null;  
                cache.SetValueExt<SOShipmentExt.usrQACheckedDate>(e.Row, null);
                cache.SetValueExt<SOShipmentExt.usrQACheckedByID>(e.Row, null);

            }

        }

        protected void SOShipment_UsrSupersedeChecked_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e, PXFieldUpdated InvokeBaseHandler)
        {
            if (InvokeBaseHandler != null)
                InvokeBaseHandler(cache, e);
            var row = (SOShipment)e.Row;
            //Set Inspection Default Data
            if (row == null) return;

            var shipmentExt = row.GetExtension<SOShipmentExt>();

            if (shipmentExt == null) return;
            if (shipmentExt.UsrSupersedeChecked == true)
            {
                EPEmployee defEmployee = PXSelect<EPEmployee, Where<EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>.Select(this.Base);
                cache.SetValueExt<SOShipmentExt.usrLSReconChecked>(e.Row, true);
                cache.SetValueExt<SOShipmentExt.usrFMReconChecked>(e.Row, true);
                cache.SetValueExt<SOShipmentExt.usrPMReconChecked>(e.Row, true);
                cache.SetValueExt<SOShipmentExt.usrCDReconChecked>(e.Row, true);
                cache.SetValueExt<SOShipmentExt.usrFullyReconciled>(e.Row, true);
                cache.SetValueExt<SOShipmentExt.usrSupersededDate>(e.Row, cache.Graph.Accessinfo.BusinessDate.GetValueOrDefault());


                //cache.SetValueExt<SOShipmentExt.usrLSReconDate>(e.Row, cache.Graph.Accessinfo.BusinessDate.GetValueOrDefault());
                //cache.SetValueExt<SOShipmentExt.usrFMReconDate>(e.Row, cache.Graph.Accessinfo.BusinessDate.GetValueOrDefault());
                //cache.SetValueExt<SOShipmentExt.usrPMReconDate>(e.Row, cache.Graph.Accessinfo.BusinessDate.GetValueOrDefault());
                //cache.SetValueExt<SOShipmentExt.usrCDReconDate>(e.Row, cache.Graph.Accessinfo.BusinessDate.GetValueOrDefault());
                if (defEmployee != null)
                {
                    cache.SetValueExt<SOShipmentExt.usrSupersededByID>(e.Row, defEmployee.BAccountID);
                    //cache.SetValueExt<SOShipmentExt.usrLSReconByID>(e.Row, defEmployee.BAccountID);
                    //cache.SetValueExt<SOShipmentExt.usrFMReconByID>(e.Row, defEmployee.BAccountID);
                    //cache.SetValueExt<SOShipmentExt.usrPMReconByID>(e.Row, defEmployee.BAccountID);
                    //cache.SetValueExt<SOShipmentExt.usrCDReconByID>(e.Row, defEmployee.BAccountID);
                }
            }

            else
            {
                //shipmentExt.GetExtension<SOShipmentExt>().UsrQACheckedDate = null;
                //shipmentExt.GetExtension<SOShipmentExt>().UsrQACheckedByID = null;  
                cache.SetValueExt<SOShipmentExt.usrSupersededDate>(e.Row, null);
                cache.SetValueExt<SOShipmentExt.usrSupersededByID>(e.Row, null);
                cache.SetValueExt<SOShipmentExt.usrLSReconChecked>(e.Row, false);
                cache.SetValueExt<SOShipmentExt.usrFMReconChecked>(e.Row, false);
                cache.SetValueExt<SOShipmentExt.usrPMReconChecked>(e.Row, false);
                cache.SetValueExt<SOShipmentExt.usrCDReconChecked>(e.Row, false);

                PXUIFieldAttribute.SetEnabled<SOShipmentExt.usrLSReconChecked>(cache, e.Row, true);
                PXUIFieldAttribute.SetEnabled<SOShipmentExt.usrFMReconChecked>(cache, e.Row, true);
                PXUIFieldAttribute.SetEnabled<SOShipmentExt.usrPMReconChecked>(cache, e.Row, true);
                PXUIFieldAttribute.SetEnabled<SOShipmentExt.usrCDReconChecked>(cache, e.Row, true);

            }
        }


        public delegate void CreateShipmentDelegate(SOOrder order, Nullable<Int32> SiteID, Nullable<DateTime> ShipDate, Nullable<Boolean> useOptimalShipDate, String operation, DocumentList<SOShipment> list, PXQuickProcess.ActionFlow quickProcessFlow);
        [PXOverride]
        public void CreateShipment(SOOrder order, Nullable<Int32> SiteID, Nullable<DateTime> ShipDate, Nullable<Boolean> useOptimalShipDate, String operation, DocumentList<SOShipment> list, PXQuickProcess.ActionFlow quickProcessFlow, CreateShipmentDelegate baseMethod)
        {
            baseMethod(order, SiteID, ShipDate, useOptimalShipDate, operation, list, quickProcessFlow);

            var shipment = Base.Document.Current;
            var shipmentExt = PXCache<SOShipment>.GetExtension<SOShipmentExt>(shipment);
            var orderExt = PXCache<SOOrder>.GetExtension<SOOrderExt>(order);
            //Set Shipment Other Data from Sales Order

            shipmentExt.UsrCubicMeter = orderExt.UsrCubicMeter;
            shipmentExt.UsrDimHeightcm = orderExt.UsrDimHeightcm;
            shipmentExt.UsrDimLengthcm = orderExt.UsrDimLengthcm;
            shipmentExt.UsrDimWidthcm = orderExt.UsrDimWidthcm;
            shipmentExt.UsrGrossWeightKg = orderExt.UsrGrossWeightKg;
            shipmentExt.UsrNumberOfCartons = orderExt.UsrNumberOfCartons;
            shipmentExt.UsrPackingMethod = orderExt.UsrPackingMethod;
            shipmentExt.UsrPCSPerCarton = orderExt.UsrPCSPerCarton;
            shipmentExt.UsrKimballNbr = orderExt.UsrKimballNbr;

            if (order.OrderQty != 0)
            {
                shipment.ControlQty = shipment.ControlQty + order.OrderQty;
            }
            //Update if the new record is not from Shipment screen
            if (shipment.CreatedByScreenID != "SO302000")
            {
                Base.Document.Update(shipment);
                Base.Actions.PressSave();
            }


        }

        #endregion
    }
}