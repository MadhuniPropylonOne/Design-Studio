using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects;
using System.Collections.Generic;
using System;

namespace PX.Objects.CA
{
    public class CATransferExt : PXCacheExtension<PX.Objects.CA.CATransfer>
    {
        #region UsrFTApproved
        [PXDBBool]
        [PXUIField(DisplayName = "Transfer Approved")]
        public virtual bool? UsrFTApproved { get; set; }
        public abstract class usrFTApproved : PX.Data.BQL.BqlBool.Field<usrFTApproved> { }
        #endregion
    }
}