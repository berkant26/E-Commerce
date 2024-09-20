using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constant
{
    public static class Messages
    {
        public static string ProductAdded = "Eklendi";
        public static string ProductNameInvalid = "Urun ismi gecersiz";
        internal static List<Product> MaintenanceTime;
        internal static string ProductListed;
        public static string ProductCountOfProductError = " count hatasi";
    }
}
