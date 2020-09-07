using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WKAPI.Models;

namespace WKAPI.Controllers
{
    public class APPPayController : ApiController
    {
        /// <summary>
        /// APP支付
        /// </summary>
        /// <param name="method">微信或者支付宝</param>
        /// <param name="orderID">订单号</param>
        /// <param name="title">付款标题</param>
        /// <param name="money">价格</param>
        /// <param name="remarks">备注</param>
        /// <returns>返回微信统一下单的相关结果</returns>
        [HttpPost, HttpGet]
        public HttpResponseMessage Pay(string method, string orderID, string title, long money, string remarks)
        {
            string strJson = string.Empty;
            try
            {
                var payment = new APPPayment("1486338752",
                "wxc28fb95061bb878b",
                "P39gzLgbjJ88y9pFIwDGzh9JwTaY7BdM",
                "http://wxpay.lmx.ren/ResultNotify");
                var orderId = "TS" + DateTime.Now.ToString("yyyyMMddhhmmssffff");
                string strIP = GetUserIp();
                strJson = payment.Pay(money, orderId, title, strIP);
            }
            catch (Exception ex)
            {
                //FileLog.WriteLog(ex.ToString());

            }
            return Request.CreateResponse(HttpStatusCode.OK, strJson);
        }

        /// <summary>
        /// 客户端ip(访问用户)
        /// </summary>
        public string GetUserIp()
        {
            string realRemoteIP = "";
            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                realRemoteIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(',')[0];
            }
            if (string.IsNullOrEmpty(realRemoteIP))
            {
                realRemoteIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (string.IsNullOrEmpty(realRemoteIP))
            {
                realRemoteIP = System.Web.HttpContext.Current.Request.UserHostAddress;
            }
            return realRemoteIP;
        }
    }
}
