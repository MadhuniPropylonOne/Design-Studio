using CRLocation = PX.Objects.CR.Standalone.Location;
using PX.Common;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.Common.Attributes;
using PX.Objects.Common.Extensions;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.SO.Attributes;
using PX.Objects.SO.Interfaces;
using PX.Objects.SO;
using PX.Objects.TX;
using PX.Objects;
using PX.TM;
using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace PX.Objects.SO
{
    public class SOOrderExt : PXCacheExtension<PX.Objects.SO.SOOrder>
    {
        #region UsrStyleTag       
        [SubAccount]
        [PXUIField(DisplayName = "Style Tag")]

        public virtual int? UsrStyleTag { get; set; }
        public abstract class usrStyleTag : PX.Data.BQL.BqlString.Field<usrStyleTag> { }
        #endregion
        #region UsrDocketNo
        [PXDBString(20)]
        [PXUIField(DisplayName = "Docket No")]

        public virtual string UsrDocketNo { get; set; }
        public abstract class usrDocketNo : PX.Data.BQL.BqlString.Field<usrDocketNo> { }
        #endregion

        #region UsrCustStyleRef
        [PXDBString(20)]
        [PXUIField(DisplayName = "Cust. Style Ref.")]

        public virtual string UsrCustStyleRef { get; set; }
        public abstract class usrCustStyleRef : PX.Data.BQL.BqlString.Field<usrCustStyleRef> { }
        #endregion

        #region UsrPCSPerCarton
        [PXDBInt]
        [PXUIField(DisplayName = "PCS per Carton")]

        public virtual int? UsrPCSPerCarton { get; set; }
        public abstract class usrPCSPerCarton : PX.Data.BQL.BqlInt.Field<usrPCSPerCarton> { }
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

        #region UsrGrossWeightKg
        [PXDBDecimal(4)]
        [PXUIField(DisplayName = "Gross Weight (Kg)")]
        public virtual Decimal? UsrGrossWeightKg { get; set; }
        public abstract class usrGrossWeightKg : PX.Data.BQL.BqlDecimal.Field<usrGrossWeightKg> { }
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

        #region UsrPackingMethod
        [PXDBString(20)]
        [PXStringList(new string[] { "F", "G" }, new string[] { "FLAT PACK (Carton)", "GOH (Hanging)" })]
        [PXUIField(DisplayName = "Packing Method")]
        public virtual string UsrPackingMethod { get; set; }
        public abstract class usrPackingMethod : PX.Data.BQL.BqlString.Field<usrPackingMethod> { }
        #endregion

        #region UsrKimballNbr
        [PXDBInt]
        [PXUIField(DisplayName = "Kimball Nbr.")]
        public virtual int? UsrKimballNbr { get; set; }
        public abstract class usrKimballNbr : PX.Data.BQL.BqlInt.Field<usrKimballNbr> { }
        #endregion
    }
}