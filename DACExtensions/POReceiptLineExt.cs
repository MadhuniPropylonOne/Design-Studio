using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.Common.Bql;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.PO;
using PX.Objects.TX;
using PX.Objects;
using System.Collections.Generic;
using System;
using PX.Data.BQL.Fluent;

namespace PX.Objects.PO
{
    public class POReceiptLineExt : PXCacheExtension<PX.Objects.PO.POReceiptLine>
    {
        #region UsrPOExtInfo
        [PXString(4000)]
        [PXUIField(DisplayName = "PO Ext Info")]

        public virtual string UsrPOExtInfo { get; set; }
        public abstract class usrPOExtInfo : PX.Data.BQL.BqlString.Field<usrPOExtInfo> { }
        #endregion

    
    }
}