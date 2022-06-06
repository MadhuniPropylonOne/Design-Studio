using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data;
using PX.Objects.AM.Attributes;
using PX.Objects.AM.CacheExtensions;
using PX.Objects.AM.GraphExtensions;
using PX.Objects.AM;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.SO;
using PX.Objects;
using PX.TM;
using System.Collections.Generic;
using System;

namespace PX.Objects.AM
{
    public class AMProdItemExt : PXCacheExtension<PX.Objects.AM.AMProdItem>
    {
        #region UsrCustomerOrder
        [PXString(255)]
        [PXUIField(DisplayName = "CustomerOrder")]

        public virtual string UsrCustomerOrder { get; set; }
        public abstract class usrCustomerOrder : PX.Data.BQL.BqlString.Field<usrCustomerOrder> { }
        #endregion

        #region UsrDestinationRef
        [PXString(255)]
        [PXUIField(DisplayName = "DestinationRef")]

        public virtual string UsrDestinationRef { get; set; }
        public abstract class usrDestinationRef : PX.Data.BQL.BqlString.Field<usrDestinationRef> { }
        #endregion
    }
}