using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data;
using PX.Objects.Common.Extensions;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.DR;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.IN.Matrix.Attributes;
using PX.Objects.IN.Matrix.Graphs;
using PX.Objects.IN;
using PX.Objects.TX;
using PX.Objects;
using PX.TM;
using System.Collections.Generic;
using System;

namespace PX.Objects.IN
{
    public class InventoryItemExt : PXCacheExtension<PX.Objects.IN.InventoryItem>
    {
        #region UsrStyleTag       
        [SubAccount]
        [PXUIField(DisplayName = "Style Tag")]

        public virtual int? UsrStyleTag { get; set; }
        public abstract class usrStyleTag : PX.Data.BQL.BqlString.Field<usrStyleTag> { }
        #endregion
    }
}