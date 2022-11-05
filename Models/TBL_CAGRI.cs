using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CagriMerkeziAPI.Models
{
    public class TBL_CAGRI
    {
        public int ID { get; set; } = -1;
        public int PERSONEL_ID { get; set; } = -1;
        public string CUSTOMER_NAME { get; set; } = "";
        public string CUSTOMER_PHONE { get; set; } = "";
        public string SUBJECT { get; set; } = "";
        public string DESCRIPTION { get; set; } = "";
        public decimal PRICE { get; set; } = -1;
        public DateTime CALL_DATE { get; set; } = DateTime.Now;
    }
}
