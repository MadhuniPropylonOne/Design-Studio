using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.GL;
using PX.Objects.PM;
using PX.Objects;
using System.Collections.Generic;
using System;

namespace PX.Objects.GL
{
  public class AccountExt : PXCacheExtension<PX.Objects.GL.Account>
  {
    #region UsrReportingGroup
    [PXDBString(100)]
    [PXUIField(DisplayName="Reporting Group")]

    public virtual string UsrReportingGroup { get; set; }
    public abstract class usrReportingGroup : PX.Data.BQL.BqlString.Field<usrReportingGroup> { }
    #endregion
  }
}