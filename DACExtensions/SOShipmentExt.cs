using PX.Data;
using System;

namespace PX.Objects.SO
{
    public class SOShipmentExt : PXCacheExtension<PX.Objects.SO.SOShipment>
    {
        #region UsrKimballNbr
        [PXDBInt]
        [PXUIField(DisplayName = "Kimball Nbr")]

        public virtual int? UsrKimballNbr { get; set; }
        public abstract class usrKimballNbr : PX.Data.BQL.BqlInt.Field<usrKimballNbr> { }
        #endregion

        #region UsrQAChecked
        [PXDBBool]
        [PXUIField(DisplayName = "QA Passed")]

        public virtual bool? UsrQAChecked { get; set; }
        public abstract class usrQAChecked : PX.Data.BQL.BqlBool.Field<usrQAChecked> { }
        #endregion

        #region UsrQACheckedDate
        [PXDBDate]
        [PXUIField(DisplayName = "Checked On")]
        public virtual DateTime? UsrQACheckedDate { get; set; }
        public abstract class usrQACheckedDate : PX.Data.BQL.BqlDateTime.Field<usrQACheckedDate> { }
        #endregion

        #region UsrQACheckedByID
        [PXDBInt]
        [PXSelector(typeof(Search<PX.Objects.EP.EPEmployee.bAccountID>),
                    typeof(PX.Objects.EP.EPEmployee.acctCD),
                    typeof(PX.Objects.EP.EPEmployee.acctName),
                    SubstituteKey = typeof(PX.Objects.EP.EPEmployee.acctCD),
                    DescriptionField = typeof(PX.Objects.EP.EPEmployee.acctName))]
        [PXUIField(DisplayName = "Checked By ID")]
        public virtual int? UsrQACheckedByID { get; set; }
        public abstract class usrQACheckedByID : PX.Data.BQL.BqlInt.Field<usrQACheckedByID> { }
        #endregion

        #region UsrLSReconByID
        [PXDBInt]
        [PXSelector(typeof(Search<PX.Objects.EP.EPEmployee.bAccountID>),
                    typeof(PX.Objects.EP.EPEmployee.acctCD),
                    typeof(PX.Objects.EP.EPEmployee.acctName),
                    SubstituteKey = typeof(PX.Objects.EP.EPEmployee.acctCD),
                    DescriptionField = typeof(PX.Objects.EP.EPEmployee.acctName))]
        [PXUIField(DisplayName = "Checked By ID")]
        public virtual int? UsrLSReconByID { get; set; }
        public abstract class usrLSReconByID : PX.Data.BQL.BqlInt.Field<usrLSReconByID> { }
        #endregion

        #region UsrLSReconChecked
        [PXDBBool]
        [PXUIField(DisplayName = "By Line Supervisor")]
        public virtual bool? UsrLSReconChecked { get; set; }
        public abstract class usrLSReconChecked : PX.Data.BQL.BqlBool.Field<usrLSReconChecked> { }
        #endregion

        #region UsrLSReconDate
        [PXDBDate]
        [PXUIField(DisplayName = "Checked On")]

        public virtual DateTime? UsrLSReconDate { get; set; }
        public abstract class usrLSReconDate : PX.Data.BQL.BqlDateTime.Field<usrLSReconDate> { }
        #endregion

        #region UsrFMReconChecked
        [PXDBBool]
        [PXUIField(DisplayName = "By Factory Merchandiser")]
        public virtual bool? UsrFMReconChecked { get; set; }
        public abstract class usrFMReconChecked : PX.Data.BQL.BqlBool.Field<usrFMReconChecked> { }
        #endregion

        #region UsrFMReconByID
        [PXDBInt]
        [PXSelector(typeof(Search<PX.Objects.EP.EPEmployee.bAccountID>),
                    typeof(PX.Objects.EP.EPEmployee.acctCD),
                    typeof(PX.Objects.EP.EPEmployee.acctName),
                    SubstituteKey = typeof(PX.Objects.EP.EPEmployee.acctCD),
                    DescriptionField = typeof(PX.Objects.EP.EPEmployee.acctName))]
        [PXUIField(DisplayName = "Checked By ID")]
        public virtual int? UsrFMReconByID { get; set; }
        public abstract class usrFMReconByID : PX.Data.BQL.BqlInt.Field<usrFMReconByID> { }
        #endregion

        #region UsrFMReconDate
        [PXDBDate]
        [PXUIField(DisplayName = "Checked On")]

        public virtual DateTime? UsrFMReconDate { get; set; }
        public abstract class usrFMReconDate : PX.Data.BQL.BqlDateTime.Field<usrFMReconDate> { }
        #endregion

        #region UsrPMReconChecked
        [PXDBBool]
        [PXUIField(DisplayName = "By Packing Manager")]
        public virtual bool? UsrPMReconChecked { get; set; }
        public abstract class usrPMReconChecked : PX.Data.BQL.BqlBool.Field<usrPMReconChecked> { }
        #endregion

        #region UsrPMReconDate
        [PXDBDate]
        [PXUIField(DisplayName = "Checked On")]

        public virtual DateTime? UsrPMReconDate { get; set; }
        public abstract class usrPMReconDate : PX.Data.BQL.BqlDateTime.Field<usrPMReconDate> { }
        #endregion

        #region UsrPMReconByID
        [PXDBInt]
        [PXSelector(typeof(Search<PX.Objects.EP.EPEmployee.bAccountID>),
                    typeof(PX.Objects.EP.EPEmployee.acctCD),
                    typeof(PX.Objects.EP.EPEmployee.acctName),
                    SubstituteKey = typeof(PX.Objects.EP.EPEmployee.acctCD),
                    DescriptionField = typeof(PX.Objects.EP.EPEmployee.acctName))]
        [PXUIField(DisplayName = "Checked By ID")]
        public virtual int? UsrPMReconByID { get; set; }
        public abstract class usrPMReconByID : PX.Data.BQL.BqlInt.Field<usrPMReconByID> { }
        #endregion

        #region UsrCDReconChecked
        [PXDBBool]
        [PXUIField(DisplayName = "By COO/DOO")]
        public virtual bool? UsrCDReconChecked { get; set; }
        public abstract class usrCDReconChecked : PX.Data.BQL.BqlBool.Field<usrCDReconChecked> { }
        #endregion

        #region UsrCDReconByID
        [PXDBInt]
        [PXSelector(typeof(Search<PX.Objects.EP.EPEmployee.bAccountID>),
                    typeof(PX.Objects.EP.EPEmployee.acctCD),
                    typeof(PX.Objects.EP.EPEmployee.acctName),
                    SubstituteKey = typeof(PX.Objects.EP.EPEmployee.acctCD),
                    DescriptionField = typeof(PX.Objects.EP.EPEmployee.acctName))]
        [PXUIField(DisplayName = "Checked By ID")]
        public virtual int? UsrCDReconByID { get; set; }
        public abstract class usrCDReconByID : PX.Data.BQL.BqlInt.Field<usrCDReconByID> { }
        #endregion

        #region UsrCDReconDate
        [PXDBDate]
        [PXUIField(DisplayName = "Checked On")]

        public virtual DateTime? UsrCDReconDate { get; set; }
        public abstract class usrCDReconDate : PX.Data.BQL.BqlDateTime.Field<usrCDReconDate> { }
        #endregion

        #region UsrSupersedeChecked
        [PXDBBool]
        [PXUIField(DisplayName = "Superseded")]

        public virtual bool? UsrSupersedeChecked { get; set; }
        public abstract class usrSupersedeChecked : PX.Data.BQL.BqlBool.Field<usrSupersedeChecked> { }
        #endregion

        #region UsrSupersededByID
        [PXDBInt]
        [PXSelector(typeof(Search<PX.Objects.EP.EPEmployee.bAccountID>),
                    typeof(PX.Objects.EP.EPEmployee.acctCD),
                    typeof(PX.Objects.EP.EPEmployee.acctName),
                    SubstituteKey = typeof(PX.Objects.EP.EPEmployee.acctCD),
                    DescriptionField = typeof(PX.Objects.EP.EPEmployee.acctName))]
        [PXUIField(DisplayName = "Superseded By ID")]
        public virtual int? UsrSupersededByID { get; set; }
        public abstract class usrSupersededByID : PX.Data.BQL.BqlInt.Field<usrSupersededByID> { }
        #endregion

        #region UsrSupersededDate
        [PXDBDate]
        [PXUIField(DisplayName = "Superseded Date")]

        public virtual DateTime? UsrSupersededDate { get; set; }
        public abstract class usrSupersededDate : PX.Data.BQL.BqlDateTime.Field<usrSupersededDate> { }
        #endregion

        #region UsrFullyReconciled
        [PXDBBool]
        [PXUIField(DisplayName = "Fully Reconciled")]

        public virtual bool? UsrFullyReconciled { get; set; }
        public abstract class usrFullyReconciled : PX.Data.BQL.BqlBool.Field<usrFullyReconciled> { }
        #endregion

        #region UsrConVehicleNbr
        [PXDBString(20)]
        [PXUIField(DisplayName = "Container / Vehicle Nbr.")]

        public virtual string UsrConVehicleNbr { get; set; }
        public abstract class usrConVehicleNbr : PX.Data.BQL.BqlString.Field<usrConVehicleNbr> { }
        #endregion

        #region UsrMDAASNNbr
        [PXDBInt]
        [PXUIField(DisplayName = "MDA / ASN Nbr.")]

        public virtual int? UsrMDAASNNbr { get; set; }
        public abstract class usrMDAASNNbr : PX.Data.BQL.BqlInt.Field<usrMDAASNNbr> { }
        #endregion

        #region UsrPCSPerCarton
        [PXDBInt]
        [PXUIField(DisplayName = "PCs per Carton")]

        public virtual int? UsrPCSPerCarton { get; set; }
        public abstract class usrPCSPerCarton : PX.Data.BQL.BqlInt.Field<usrPCSPerCarton> { }
        #endregion

        #region UsrPackingMethod
        [PXDBString(20)]
        [PXStringList(new string[] { "F", "G" }, new string[] { "FLAT PACK (Carton)", "GOH (Hanging)" })]
        [PXUIField(DisplayName = "Packing Method")]
        public virtual string UsrPackingMethod { get; set; }
        public abstract class usrPackingMethod : PX.Data.BQL.BqlString.Field<usrPackingMethod> { }
        #endregion

        #region UsrNumberOfCartons
        [PXDBInt]
        [PXUIField(DisplayName = "Number of Cartons")]

        public virtual int? UsrNumberOfCartons { get; set; }
        public abstract class usrNumberOfCartons : PX.Data.BQL.BqlInt.Field<usrNumberOfCartons> { }
        #endregion

        #region UsrCubicMeter
        [PXDBDecimal(4)]
        [PXUIField(DisplayName = "Cubic Meter")]
        public virtual Decimal? UsrCubicMeter { get; set; }
        public abstract class usrCubicMeter : PX.Data.BQL.BqlDecimal.Field<usrCubicMeter> { }
        #endregion

        #region UsrDimLengthcm
        [PXDBDecimal(4)]
        [PXUIField(DisplayName = "Dim. Length (cm)")]
        public virtual Decimal? UsrDimLengthcm { get; set; }
        public abstract class usrDimLengthcm : PX.Data.BQL.BqlDecimal.Field<usrDimLengthcm> { }
        #endregion

        #region UsrDimWidthcm
        [PXDBDecimal(4)]
        [PXUIField(DisplayName = "Dim. Width (cm)")]
        public virtual Decimal? UsrDimWidthcm { get; set; }
        public abstract class usrDimWidthcm : PX.Data.BQL.BqlDecimal.Field<usrDimWidthcm> { }
        #endregion

        #region UsrDimHeightcm
        [PXDBDecimal(4)]
        [PXUIField(DisplayName = "Dim. Height (cm)")]
        public virtual Decimal? UsrDimHeightcm { get; set; }
        public abstract class usrDimHeightcm : PX.Data.BQL.BqlDecimal.Field<usrDimHeightcm> { }
        #endregion

        #region UsrGrossWeightKg
        [PXDBDecimal(4)]
        [PXUIField(DisplayName = "Gross Weight (Kg)")]
        public virtual Decimal? UsrGrossWeightKg { get; set; }
        public abstract class usrGrossWeightKg : PX.Data.BQL.BqlDecimal.Field<usrGrossWeightKg> { }
        #endregion
    }
}