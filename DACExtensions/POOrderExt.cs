using CRLocation = PX.Objects.CR.Standalone.Location;
using PX.Common;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.Common.Bql;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.PO;
using PX.Objects;
using PX.SM;
using PX.TM;
using System.Collections.Generic;

using System;
using PX.Objects.GL;
using PX.Data.BQL.Fluent;

namespace PX.Objects.PO
{
    public class POOrderExt : PXCacheExtension<PX.Objects.PO.POOrder>
    {
        #region UsrMassSubItem
        [PXDBInt]
        [PXUIField(DisplayName = "Mass Sub Acct")]
        [PXSelector(typeof(SelectFrom<Sub>.Where<Sub.active.IsEqual<True>>.SearchFor<Sub.subID>),
            new Type[]
            {
                typeof(Sub.subCD),
                typeof(Sub.description)
            },
            SubstituteKey = typeof(Sub.subCD),
            DescriptionField = typeof(Sub.description)
            )]

        public virtual int? UsrMassSubItem { get; set; }
        public abstract class usrMassSubItem : PX.Data.BQL.BqlInt.Field<usrMassSubItem> { }
        #endregion

        #region UsrSumLineNbr
        [PXDBInt()]
        [PXDefault(0)]
        [PXUIField(DisplayName = "Sum Line Nbr")]

        public virtual int? UsrSumLineNbr { get; set; }
        public abstract class usrSumLineNbr : PX.Data.BQL.BqlInt.Field<usrSumLineNbr> { }
        #endregion

        #region UsrETD
        [PXDBDate]
        [PXUIField(DisplayName = "ETD", Required = true)]
       

        public virtual DateTime? UsrETD { get; set; }
        public abstract class usrETD : PX.Data.BQL.BqlDateTime.Field<usrETD> { }
        #endregion

        #region UsrETA
        [PXDBDate]
        [PXUIField(DisplayName = "ETA", Required = true)]
    

        public virtual DateTime? UsrETA { get; set; }
        public abstract class usrETA : PX.Data.BQL.BqlDateTime.Field<usrETA> { }
        #endregion
    }
}