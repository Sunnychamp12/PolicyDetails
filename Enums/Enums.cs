using DocumentFormat.OpenXml;
using System.ComponentModel;

namespace PolicyDetails.Enums
{
    public static class Enums
    {
        public enum status
        {
            Success,
            Error
        }
        public enum HeaderValues
        {
            [EnumString("c06fc4189a5645e4a4fd480e8b1556e7")]
            HeaderToken,
        }
        public enum APPName
        {
            PolicyDetails
        }
    }
}
