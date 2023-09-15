using DocumentFormat.OpenXml;
using System.ComponentModel;

namespace PolicyDetails.Enums
{
    public static class Enums
    {
        public enum statusCode
        {
            Success = 1,
            Error = 0
        }
        public enum HeaderValues
        {
            HeaderToken,
            PolicyDetails,
            APPName
        }
    }
}
