using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SimpleERP.Libraries.API.Http
{
    public static class RequestHelper
    {
        
        public static async Task<NameValueCollection> GetRequestDataAsync(this HttpRequestMessage request)
        {
            var result = new NameValueCollection();

            //1: query string
            foreach (var item in request.GetQueryNameValuePairs())
                result.Add(item.Key, item.Value);

            //2: form data
            NameValueCollection form = null;

            try
            {
                form = await request.Content.ReadAsFormDataAsync();

                if (form == null || form.Count == 0)//there is a bug in .Net!! https://github.com/aspnet/Mvc/issues/5258
                {
                    //در واقع هرجا که از پارامتر بایندیگ استفاده کردیم یا پارامترهای کامپلکس دارید، این مقدار خالی بازگردانده می شود
                    //بطور مثال در متد ExectuvesController.Get
                    //که یک پارامتر بایندیگ داریم و یک پارامتر کامپلکس
                    //در حال حاضر فقط مشاهده می شود که می توان مقدار را بصورت رشته ای خواند
                    var str = await request.Content.ReadAsStringAsync();
                    if (str != null && str.Length > 0)
                    {
                        form = HttpUtility.ParseQueryString(str);
                        request.Content = new StringContent(str);//return to the latest state (although it should be NameValueCollection but in that it has bug, we keep the bug state to prevent further unkown behavior)
                    }

                }
            }
            catch
            {
            }

            if (form != null)
            {
                foreach (var key in form.AllKeys)
                    result.Add(key, form[key]);
            }

            return result;
        }
    }
}
