using CRLocation = PX.Objects.CR.Standalone.Location;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.Common.Bql;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.IN;
using PX.Objects.PO;
using PX.Objects.RQ;
using PX.Objects.SO;
using PX.Objects;
using PX.SM;
using PX.TM;
using System.Collections.Generic;
using System;

namespace PX.Objects.RQ
{
    public class RQRequisitionExt : PXCacheExtension<PX.Objects.RQ.RQRequisition>
    {
        #region UsrCheckedByFin
        [PXDBBool]
        [PXUIField(DisplayName = "Checked By Finance")]

        public virtual bool? UsrCheckedByFin { get; set; }
        public abstract class usrCheckedByFin : PX.Data.BQL.BqlBool.Field<usrCheckedByFin> { }
        #endregion
    }
}
