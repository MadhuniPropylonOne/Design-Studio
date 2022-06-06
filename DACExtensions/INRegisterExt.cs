using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data;
using PX.Objects.CM;
using PX.Objects.Common.Attributes;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PO;
using PX.Objects.SO;
using PX.Objects;
using System.Collections.Generic;
using System;
using PX.Data.BQL.Fluent;
using PX.TM;

namespace PX.Objects.IN
{
    public class INRegisterExt : PXCacheExtension<PX.Objects.IN.INRegister>
    {
        //#region UsrExtSegment
        //[PXDBString(30)]
        //[PXUIField(DisplayName = "Style")]
        //[PXSelector(typeof(SelectFrom<SegmentValue>.
        //                    Where<SegmentValue.active.IsEqual<True>.
        //                            And<SegmentValue.dimensionID.IsEqual<usrExtSegment.dSDimensionIDSub>>.
        //                                And<SegmentValue.segmentID.IsEqual<usrExtSegment.dSDimensionSegID2>>>.SearchFor<SegmentValue.value>),
        //    new Type[]
        //    {
        //        typeof(SegmentValue.value),
        //        typeof(SegmentValue.descr)
        //    },

        //    DescriptionField = typeof(SegmentValue.descr)
        //    )]


        //public virtual string UsrExtSegment { get; set; }
        //public abstract class usrExtSegment : PX.Data.BQL.BqlString.Field<usrExtSegment>
        //{

        //    public class dSDimensionIDSub : PX.Data.BQL.BqlString.Constant<dSDimensionIDSub>
        //    {
        //        public dSDimensionIDSub() : base("SUBACCOUNT") { }
        //    }


        //    public class dSDimensionSegID2 : PX.Data.BQL.BqlInt.Constant<dSDimensionSegID2>
        //    {
        //        public dSDimensionSegID2() : base(2) { }
        //    }


        //    public class dSDimensionSegID1 : PX.Data.BQL.BqlInt.Constant<dSDimensionSegID1>
        //    {
        //        public dSDimensionSegID1() : base(1){ }
        //    }

        //    public class dSDimensionSegID4 : PX.Data.BQL.BqlInt.Constant<dSDimensionSegID4>
        //    {
        //        public dSDimensionSegID4() : base(4){ }
        //    }
        //}
        //#endregion

        #region UsrStyleTag       
        [SubAccount]
        [PXUIField(DisplayName = "Style Tag")]

        public virtual int? UsrStyleTag { get; set; }
        public abstract class usrStyleTag : PX.Data.BQL.BqlString.Field<usrStyleTag> { }
        #endregion

        #region UsrEmployeeID

        [Owner()]
        [PXUIField(DisplayName = "Notify")]
        public virtual int? UsrEmployeeID { get; set; }
        public abstract class usrEmployeeID : PX.Data.BQL.BqlInt.Field<usrEmployeeID> { }
        #endregion

    }
}