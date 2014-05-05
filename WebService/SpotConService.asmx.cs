using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebService
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://spotcon.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class SpotConWebService : System.Web.Services.WebService
    {
        private static string stored;

        [WebMethod]
        public void Set(string value)
        {
            stored = value;
        }

        [WebMethod]
        public string Get()
        {
            return stored;
        }
    }
}