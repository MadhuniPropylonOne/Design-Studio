using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.Common.Attributes;
using PX.Objects.Common.Bql;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.PO;
using PX.Objects.SO;
using PX.Objects;
using System.Collections.Generic;
using System;
using PX.Data.BQL.Fluent;

namespace PX.Objects.IN
{
    public class INTranExt : PXCacheExtension<PX.Objects.IN.INTran>
    {
        #region UsrPOExtInfo
        [PXString(4000)]
        [PXUIField(DisplayName = "PO Ext Info")]

        public virtual string UsrPOExtInfo { get; set; }
        public abstract class usrPOExtInfo : PX.Data.BQL.BqlString.Field<usrPOExtInfo> { }
        #endregion


        #region UsrExtSegment
        [PXDBString(30)]
        [PXUIField(DisplayName = "Style")]
        [PXSelector(typeof(SelectFrom<SegmentValue>.
                            Where<SegmentValue.active.IsEqual<True>.
                                    And<SegmentValue.dimensionID.IsEqual<usrExtSegment.dSDimensionIDSub>>.
                                        And<SegmentValue.segmentID.IsEqual<usrExtSegment.dSDimensionSegID>>>.SearchFor<SegmentValue.value>),
            new Type[]
            {
                typeof(SegmentValue.value),
                typeof(SegmentValue.descr)
            },
     
            DescriptionField = typeof(SegmentValue.descr)
            )]


        public virtual string UsrExtSegment { get; set; }
        public abstract class usrExtSegment : PX.Data.BQL.BqlString.Field<usrExtSegment> {           

            public class dSDimensionIDSub : PX.Data.BQL.BqlString.Constant<dSDimensionIDSub>
            {
                public dSDimensionIDSub() : base("SUBACCOUNT") { }
            }


            public class dSDimensionSegID : PX.Data.BQL.BqlInt.Constant<dSDimensionSegID>
            {
                public dSDimensionSegID() : base(2) { }
            }
        }
        #endregion
    }
}