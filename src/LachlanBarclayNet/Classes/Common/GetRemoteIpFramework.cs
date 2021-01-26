namespace LachlanBarclayNet.Controllers
{
    public class GetRemoteIpFramework: IRemoteIpLookup
    { 
        public string GetRemoteIp()
        {
            string ipAddress = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                    return addresses[0];
            }

            return System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }

    }

}
