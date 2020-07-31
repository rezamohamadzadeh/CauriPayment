using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CauriPayment.Models
{
    public class SCIModel
    {
        public string ik_co_id { get; set; }
        public string ik_pm_no { get; set; }
        public string ik_cur { get; set; }
        public string ik_am { get; set; }
        public string ik_am_t { get; set; }
        public string ik_desc { get; set; }
        public string ik_exp { get; set; }
        public string ik_ltm { get; set; }
        public string ik_pw_on { get; set; }
        public string ik_pw_off { get; set; }
        public string ik_pw_via { get; set; }
        public string ik_sign { get; set; }
        public string ik_loc { get; set; }
        public string ik_enc { get; set; }
        public string ik_ia_u { get; set; }
        public string ik_ia_m { get; set; }
        public string ik_suc_u { get; set; }
        public string ik_suc_m { get; set; }
        public string ik_pnd_u { get; set; }
        public string ik_pnd_m { get; set; }
        public string ik_fal_u { get; set; }
        public string ik_act { get; set; }
        public string ik_int { get; set; }
        public string ik_fal_m { get; set; }
        public string ik_trn_id { get; set; }
        public string ik_ci { get; set; }
        public string ci { get; set; }
        public string ik_inv_st { get; set; }
        public string ik_ps_price { get; set; }
        public string ik_co_rfn { get; set; }
        public string ik_inv_id { get; set; }
        public string ik_inv_crt { get; set; }
        public string ik_pay_token { get; set; }
    }
    
}
