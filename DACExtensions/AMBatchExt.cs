using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data;
using PX.Objects.AM.Attributes;
using PX.Objects.AM;
using PX.Objects.CM;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects;
using System.Collections.Generic;
using System;
using PX.TM;

namespace PX.Objects.AM
{
    public class AMBatchExt : PXCacheExtension<PX.Objects.AM.AMBatch>
    {
        #region UsrEmployeeID
      
        
        [Owner()]
        [PXUIField(DisplayName = "Notify")]
        public virtual int? UsrEmployeeID { get; set; }
        public abstract class usrEmployeeID : PX.Data.BQL.BqlInt.Field<usrEmployeeID> { }
        #endregion

        #region UsrStyleTag       
        [SubAccount]
        [PXUIField(DisplayName = "Style Tag")]
        public virtual int? UsrStyleTag { get; set; }
        public abstract class usrStyleTag : PX.Data.BQL.BqlString.Field<usrStyleTag> { }
        #endregion

        #region UsrSendRequest
        [PXDBBool]
        [PXUIField(DisplayName = "Send Request")]
        public virtual bool? UsrSendRequest { get; set; }
        public abstract class usrSendRequest : PX.Data.BQL.BqlBool.Field<usrSendRequest> { }
        #endregion
    }
}