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
using PX.Objects.SM;
using PX.Objects;
using PX.SM;
using PX.TM;
using System.Collections.Generic;
using System;

namespace PX.Objects.RQ
{
    public class RQRequestExt : PXCacheExtension<PX.Objects.RQ.RQRequest>
    {
        #region UsrCheckedByStores
        [PXDBBool]
        [PXUIField(DisplayName = "Checked By Stores")]
        public virtual bool? UsrCheckedByStores { get; set; }
        public abstract class usrCheckedByStores : PX.Data.BQL.BqlBool.Field<usrCheckedByStores> { }
        #endregion

        #region UsrReadyForStores
        [PXDBBool]
        [PXUIField(DisplayName = "Ready For Stores")]

        public virtual bool? UsrReadyForStores { get; set; }
        public abstract class usrReadyForStores : PX.Data.BQL.BqlBool.Field<usrReadyForStores> { }
        #endregion
    }
}
