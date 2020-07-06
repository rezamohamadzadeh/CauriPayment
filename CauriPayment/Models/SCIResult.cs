using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CauriPayment.Models
{
    public class SCIPaymentResult
    {
        public int resultCode { get; set; }
        public string resultMsg { get; set; }
        public ResultData resultData { get; set; }
    }

    public class ResultData
    {
        public List<PaywaySet> paywaySet { get; set; }
    }

    public class PaywaySet
    {
        public string _id { get; set; }
        public string als { get; set; }
        public string cur { get; set; }
        public string curAls { get; set; }
        public string @in { get; set; }
        public string inAls { get; set; }
        public string insInId { get; set; }
        public string ps { get; set; }
        public string ser { get; set; }
        public string srt { get; set; }


    }
    public class SCIResult
    {
        public string status { get; set; }
        public int code { get; set; }
        public data data { get; set; }

    }
    public partial class data
    {
        public EUR EUR { get; set; }
        public USD USD { get; set; }
        public UAH UAH { get; set; }
        public GEL GEL { get; set; }
        public RUB RUB { get; set; }
        public KZT KZT { get; set; }
        public BYN BYN { get; set; }
        public TMT TMT { get; set; }
        public GBP GBP { get; set; }
        public TRY TRY { get; set; }
        public STH STH { get; set; }
        public WMX WMX { get; set; }
        public USDT USDT { get; set; }
        public LTC LTC { get; set; }
        public ETH ETH { get; set; }
        public IKX IKX { get; set; }
    }
    public class EUR : BaseCurrency
    {

        public UAH UAH { get; set; }
        public TMT TMT { get; set; }
        public GEL GEL { get; set; }
        public USD USD { get; set; }
        public RUB RUB { get; set; }
        public KZT KZT { get; set; }
        public BYN BYN { get; set; }
        public STH STH { get; set; }
        public WMX WMX { get; set; }
        public GBP GBP { get; set; }
        public TRY TRY { get; set; }
        public IKX IKX { get; set; }
        public USDT USDT { get; set; }
        public LTC LTC { get; set; }
        public ETH ETH { get; set; }
    }
    public class UAH : BaseCurrency
    {

        public EUR EUR { get; set; }
        public TMT TMT { get; set; }
        public GEL GEL { get; set; }
        public USD USD { get; set; }
        public RUB RUB { get; set; }
        public KZT KZT { get; set; }
        public BYN BYN { get; set; }
        public STH STH { get; set; }
        public WMX WMX { get; set; }
        public GBP GBP { get; set; }
        public TRY TRY { get; set; }
        public IKX IKX { get; set; }
        public USDT USDT { get; set; }
        public LTC LTC { get; set; }
        public ETH ETH { get; set; }
    }
    public class TMT : BaseCurrency
    {
        public EUR EUR { get; set; }
        public UAH UAH { get; set; }
        public GEL GEL { get; set; }
        public USD USD { get; set; }
        public RUB RUB { get; set; }
        public KZT KZT { get; set; }
        public BYN BYN { get; set; }
        public STH STH { get; set; }
        public WMX WMX { get; set; }
        public GBP GBP { get; set; }
        public TRY TRY { get; set; }
        public IKX IKX { get; set; }
        public USDT USDT { get; set; }
        public LTC LTC { get; set; }
        public ETH ETH { get; set; }
    }
    public class GEL : BaseCurrency
    {

        public EUR EUR { get; set; }
        public UAH UAH { get; set; }
        public TMT TMT { get; set; }
        public USD USD { get; set; }
        public RUB RUB { get; set; }
        public KZT KZT { get; set; }
        public BYN BYN { get; set; }
        public STH STH { get; set; }
        public WMX WMX { get; set; }
        public GBP GBP { get; set; }
        public TRY TRY { get; set; }
        public IKX IKX { get; set; }
        public USDT USDT { get; set; }
        public LTC LTC { get; set; }
        public ETH ETH { get; set; }
    }
    public class USD : BaseCurrency
    {

        public EUR EUR { get; set; }
        public UAH UAH { get; set; }
        public TMT TMT { get; set; }
        public GEL GEL { get; set; }
        public RUB RUB { get; set; }
        public KZT KZT { get; set; }
        public BYN BYN { get; set; }
        public STH STH { get; set; }
        public WMX WMX { get; set; }
        public GBP GBP { get; set; }
        public TRY TRY { get; set; }
        public IKX IKX { get; set; }
        public USDT USDT { get; set; }
        public LTC LTC { get; set; }
        public ETH ETH { get; set; }
    }
    public class RUB : BaseCurrency
    {

        public EUR EUR { get; set; }
        public UAH UAH { get; set; }
        public TMT TMT { get; set; }
        public GEL GEL { get; set; }
        public USD USD { get; set; }
        public KZT KZT { get; set; }
        public BYN BYN { get; set; }
        public STH STH { get; set; }
        public WMX WMX { get; set; }
        public GBP GBP { get; set; }
        public TRY TRY { get; set; }
        public IKX IKX { get; set; }
        public USDT USDT { get; set; }
        public LTC LTC { get; set; }
        public ETH ETH { get; set; }
    }
    public class KZT : BaseCurrency
    {
        public EUR EUR { get; set; }
        public UAH UAH { get; set; }
        public TMT TMT { get; set; }
        public GEL GEL { get; set; }
        public USD USD { get; set; }
        public RUB RUB { get; set; }
        public BYN BYN { get; set; }
        public STH STH { get; set; }
        public WMX WMX { get; set; }
        public GBP GBP { get; set; }
        public TRY TRY { get; set; }
        public IKX IKX { get; set; }
        public USDT USDT { get; set; }
        public LTC LTC { get; set; }
        public ETH ETH { get; set; }
    }
    public class STH : BaseCurrency
    {

        public EUR EUR { get; set; }
        public UAH UAH { get; set; }
        public TMT TMT { get; set; }
        public GEL GEL { get; set; }
        public USD USD { get; set; }
        public RUB RUB { get; set; }
        public BYN BYN { get; set; }
        public KZT KZT { get; set; }
        public WMX WMX { get; set; }
        public GBP GBP { get; set; }
        public TRY TRY { get; set; }
        public IKX IKX { get; set; }
        public USDT USDT { get; set; }
        public LTC LTC { get; set; }
        public ETH ETH { get; set; }
    }
    public class WMX : BaseCurrency
    {
        public EUR EUR { get; set; }
        public UAH UAH { get; set; }
        public TMT TMT { get; set; }
        public GEL GEL { get; set; }
        public USD USD { get; set; }
        public RUB RUB { get; set; }
        public BYN BYN { get; set; }
        public KZT KZT { get; set; }
        public STH STH { get; set; }
        public GBP GBP { get; set; }
        public TRY TRY { get; set; }
        public IKX IKX { get; set; }
        public USDT USDT { get; set; }
        public LTC LTC { get; set; }
        public ETH ETH { get; set; }
    }
    public class GBP : BaseCurrency
    {
        public EUR EUR { get; set; }
        public UAH UAH { get; set; }
        public TMT TMT { get; set; }
        public GEL GEL { get; set; }
        public USD USD { get; set; }
        public RUB RUB { get; set; }
        public BYN BYN { get; set; }
        public KZT KZT { get; set; }
        public STH STH { get; set; }
        public WMX WMX { get; set; }
        public TRY TRY { get; set; }
        public IKX IKX { get; set; }
        public USDT USDT { get; set; }
        public LTC LTC { get; set; }
        public ETH ETH { get; set; }
    }
    public class TRY : BaseCurrency
    {
        public EUR EUR { get; set; }
        public UAH UAH { get; set; }
        public TMT TMT { get; set; }
        public GEL GEL { get; set; }
        public USD USD { get; set; }
        public RUB RUB { get; set; }
        public BYN BYN { get; set; }
        public KZT KZT { get; set; }
        public STH STH { get; set; }
        public WMX WMX { get; set; }
        public GBP GBP { get; set; }
        public IKX IKX { get; set; }
        public USDT USDT { get; set; }
        public LTC LTC { get; set; }
        public ETH ETH { get; set; }
    }
    public class IKX : BaseCurrency
    {
        public EUR EUR { get; set; }
        public UAH UAH { get; set; }
        public TMT TMT { get; set; }
        public GEL GEL { get; set; }
        public USD USD { get; set; }
        public RUB RUB { get; set; }
        public BYN BYN { get; set; }
        public KZT KZT { get; set; }
        public STH STH { get; set; }
        public WMX WMX { get; set; }
        public GBP GBP { get; set; }
        public TRY TRY { get; set; }
        public USDT USDT { get; set; }
        public LTC LTC { get; set; }
        public ETH ETH { get; set; }
    }
    public class USDT : BaseCurrency
    {

        public EUR EUR { get; set; }
        public UAH UAH { get; set; }
        public TMT TMT { get; set; }
        public GEL GEL { get; set; }
        public USD USD { get; set; }
        public RUB RUB { get; set; }
        public BYN BYN { get; set; }
        public KZT KZT { get; set; }
        public STH STH { get; set; }
        public WMX WMX { get; set; }
        public GBP GBP { get; set; }
        public TRY TRY { get; set; }
        public IKX IKX { get; set; }
        public LTC LTC { get; set; }
        public ETH ETH { get; set; }
    }
    public class LTC : BaseCurrency
    {

        public EUR EUR { get; set; }
        public UAH UAH { get; set; }
        public TMT TMT { get; set; }
        public GEL GEL { get; set; }
        public USD USD { get; set; }
        public RUB RUB { get; set; }
        public BYN BYN { get; set; }
        public KZT KZT { get; set; }
        public STH STH { get; set; }
        public WMX WMX { get; set; }
        public GBP GBP { get; set; }
        public TRY TRY { get; set; }
        public IKX IKX { get; set; }
        public USDT USDT { get; set; }
        public ETH ETH { get; set; }
    }
    public class ETH : BaseCurrency
    {
        public EUR EUR { get; set; }
        public UAH UAH { get; set; }
        public TMT TMT { get; set; }
        public GEL GEL { get; set; }
        public USD USD { get; set; }
        public RUB RUB { get; set; }
        public BYN BYN { get; set; }
        public KZT KZT { get; set; }
        public STH STH { get; set; }
        public WMX WMX { get; set; }
        public GBP GBP { get; set; }
        public TRY TRY { get; set; }
        public IKX IKX { get; set; }
        public USDT USDT { get; set; }
        public LTC LTC { get; set; }
    }
    public class BYN : BaseCurrency
    {
        public EUR EUR { get; set; }
        public UAH UAH { get; set; }
        public TMT TMT { get; set; }
        public GEL GEL { get; set; }
        public USD USD { get; set; }
        public RUB RUB { get; set; }
        public ETH ETH { get; set; }
        public KZT KZT { get; set; }
        public STH STH { get; set; }
        public WMX WMX { get; set; }
        public GBP GBP { get; set; }
        public TRY TRY { get; set; }
        public IKX IKX { get; set; }
        public USDT USDT { get; set; }
        public LTC LTC { get; set; }
    }
    public abstract class BaseCurrency
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [Column(Order = 1)]
        public int? id { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? @in { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? @out { get; set; }
    }
}
