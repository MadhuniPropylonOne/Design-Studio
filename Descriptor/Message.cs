using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CstDesignStudio.Descriptor
{
    [PXLocalizable]
    public static class Messages
    {
        public const string Z000 = "000";
        public const string CIF = "CIF";
        public const string NFE = "NFE";
        public const string FOB = "FOB";

        public const string CompareDateETAETAD = "ETA must be greater or equal to ETD";
        public const string FillCannotBeBlank = "This field cannot be blank.";
        public const string CannotChooseNFE = "Cannot choose type NFE if details have link to Purchase Order.";

        ///Shipment Inspection
        public const string ShipInspecIncomplete = "Shipment {0} inspection is not complete.";
    }
    public class salesOrderSA : PX.Data.BQL.BqlString.Constant<salesOrderSA> { public salesOrderSA() : base("SA") { } }
    public class salesOrderRC : PX.Data.BQL.BqlString.Constant<salesOrderRC> { public salesOrderRC() : base("RC") { } }

}
