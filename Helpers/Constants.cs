using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

public static class Constants
{
    public static string SiteUrl
    {
        get { return ConfigurationManager.AppSettings["SiteUrl"].ToString(); }
    }

    public static string BrowserName
    {
        get { return ConfigurationManager.AppSettings["BrowserName"].ToString(); }
    }
}
