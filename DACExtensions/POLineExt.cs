using PX.Common;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.Common.Bql;
using PX.Objects.Common.Discount.Attributes;
using PX.Objects.Common.Discount;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN.Matrix.Interfaces;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.PO;
using PX.Objects.TX;
using PX.Objects;
using System.Collections.Generic;
using System;

namespace PX.Objects.PO
{
    public class POLineExt : PXCacheExtension<PX.Objects.PO.POLine>
    {
        #region UsrPOExtInfo
        [PXDBString(4000)]
        [PXUIField(DisplayName = "Ext Info")]

        public virtual string UsrPOExtInfo { get; set; }
        public abstract class usrPOExtInfo : PX.Data.BQL.BqlString.Field<usrPOExtInfo> { }
        #endregion
    }
}