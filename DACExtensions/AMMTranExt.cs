using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data;
using PX.Objects.AM.Attributes;
using PX.Objects.AM.CacheExtensions;
using PX.Objects.AM.GraphExtensions;
using PX.Objects.AM;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects;
using System.Collections.Generic;
using System;

namespace PX.Objects.AM
{
    public class AMMTranExt : PXCacheExtension<PX.Objects.AM.AMMTran>
    {
        #region UsrBarRackNbr
        [PXDBString(50)]
        [PXUIField(DisplayName = "Bar/Rack Nbr")]
        public virtual string UsrBarRackNbr { get; set; }
        public abstract class usrBarRackNbr : PX.Data.BQL.BqlString.Field<usrBarRackNbr> { }
        #endregion
    }
}
