using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.FA;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PO;
using PX.Objects;
using System.Collections.Generic;
using System;

namespace PX.Objects.FA
{
    public class FADetailsExt : PXCacheExtension<PX.Objects.FA.FADetails>
    {
        #region UsrSNMotor
        [PXDBString(30)]
        [PXUIField(DisplayName = "Serial - Motor")]
        public virtual string UsrSNMotor { get; set; }
        public abstract class usrSNMotor : PX.Data.BQL.BqlString.Field<usrSNMotor> { }
        #endregion
    }
}
