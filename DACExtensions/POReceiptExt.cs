using CRLocation = PX.Objects.CR.Standalone.Location;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.Common.Bql;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.PO;
using PX.Objects;
using PX.TM;
using System.Collections.Generic;
using System;
using PX.Data.BQL.Fluent;

namespace PX.Objects.PO
{
    public class POReceiptExt : PXCacheExtension<PX.Objects.PO.POReceipt>
    {

        #region UsrLoaded
        [PXBool()]
        [PXUIField(DisplayName = "Selected")]
        [PXUnboundDefault(false)]
        public virtual bool? UsrLoaded { get; set; }
        public abstract class usrLoaded : PX.Data.BQL.BqlBool.Field<usrLoaded> { }
        #endregion

        //#region UsrStyle
        //[PXDBString(30)]
        //[PXUIField(DisplayName = "Style")]
        //[PXSelector(typeof(SelectFrom<SegmentValue>.
        //                    Where<SegmentValue.active.IsEqual<True>.
        //                            And<SegmentValue.dimensionID.IsEqual<usrStyle.dSDimensionIDSub>>.
        //                                And<SegmentValue.segmentID.IsEqual<usrStyle.dSDimensionSegID2>>>.SearchFor<SegmentValue.value>),
        //    new Type[]
        //    {
        //        typeof(SegmentValue.value),
        //        typeof(SegmentValue.descr)
        //    },

        //    DescriptionField = typeof(SegmentValue.descr)
        //    )]


        //public virtual string UsrStyle { get; set; }
        //public abstract class usrStyle : PX.Data.BQL.BqlString.Field<usrStyle>
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
        //        public dSDimensionSegID1() : base(1) { }
        //    }

        //    public class dSDimensionSegID4 : PX.Data.BQL.BqlInt.Constant<dSDimensionSegID4>
        //    {
        //        public dSDimensionSegID4() : base(4) { }
        //    }
        //}
        //#endregion

        //#region UsrBrand
        //[PXDBString(30)]
        //[PXUIField(DisplayName = "Brand")]
        //[PXSelector(typeof(SelectFrom<SegmentValue>.
        //                    Where<SegmentValue.active.IsEqual<True>.
        //                            And<SegmentValue.dimensionID.IsEqual<usrStyle.dSDimensionIDSub>>.
        //                                And<SegmentValue.segmentID.IsEqual<usrStyle.dSDimensionSegID1>>>.SearchFor<SegmentValue.value>),
        //    new Type[]
        //    {
        //        typeof(SegmentValue.value),
        //        typeof(SegmentValue.descr)
        //    },

        //    DescriptionField = typeof(SegmentValue.descr)
        //    )]

        //public virtual string UsrBrand { get; set; }
        //public abstract class usrBrand : PX.Data.BQL.BqlString.Field<usrBrand> { }
        //#endregion

        //#region UsrBuyer
        //[PXDBString(30)]
        //[PXUIField(DisplayName = "Buyer")]
        //[PXSelector(typeof(SelectFrom<SegmentValue>.
        //                    Where<SegmentValue.active.IsEqual<True>.
        //                            And<SegmentValue.dimensionID.IsEqual<usrStyle.dSDimensionIDSub>>.
        //                                And<SegmentValue.segmentID.IsEqual<usrStyle.dSDimensionSegID4>>>.SearchFor<SegmentValue.value>),
        //    new Type[]
        //    {
        //        typeof(SegmentValue.value),
        //        typeof(SegmentValue.descr)
        //    },

        //    DescriptionField = typeof(SegmentValue.descr)
        //    )]
        //public virtual string UsrBuyer { get; set; }
        //public abstract class usrBuyer : PX.Data.BQL.BqlString.Field<usrBuyer> { }
        //#endregion

        #region UsrType
        [PXDBString(3)]
        [PXUIField(DisplayName = "Type")]
        [PXStringList(
            new string[]
            {
                CstDesignStudio.Descriptor.Messages.Z000,
                CstDesignStudio.Descriptor.Messages.CIF,
                CstDesignStudio.Descriptor.Messages.NFE,
                CstDesignStudio.Descriptor.Messages.FOB
            },
            new string[]
            {
                CstDesignStudio.Descriptor.Messages.Z000,
                CstDesignStudio.Descriptor.Messages.CIF,
                CstDesignStudio.Descriptor.Messages.NFE,
                CstDesignStudio.Descriptor.Messages.FOB
            })]
        public virtual string UsrType { get; set; }
        public abstract class usrType : PX.Data.BQL.BqlString.Field<usrType> { }
        #endregion

        #region UsrStyleTag       
        [SubAccount]
        [PXUIField(DisplayName = "Style Tag")]

        public virtual int? UsrStyleTag { get; set; }
        public abstract class usrStyleTag : PX.Data.BQL.BqlString.Field<usrStyleTag> { }
        #endregion
    }
}