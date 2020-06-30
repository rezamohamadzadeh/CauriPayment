using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CauriPayment.Models
{
    public class Card
    {
        public string lastFour { get; set; }
        public string mask { get; set; }
        public string type { get; set; }
        public int expirationMonth { get; set; }
        public int expirationYear { get; set; }

    }
    public class Acs
    {
        public string url { get; set; }
        public Parameters parameters { get; set; }
    }
    public class Recurring
    {
        public string id { get; set; }
        public string interval { get; set; }
        public string endsAt { get; set; }
    }
    public class Parameters
    {
        public string MD { get; set; }
        public string PaReq { get; set; }
        public string TermUrl { get; set; }
    }
}
