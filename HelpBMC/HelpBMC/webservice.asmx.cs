using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.IO;
using BusinessObjects;


namespace HelpBMC
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Service1 : System.Web.Services.WebService
    {

        private string GetExceptionMessage(Exception ex){
            return "Oops!!! something went wrong and it says " + ex.Message;
        }
        [WebMethod]
        public void ReportIssues(string gpslat, string gpslng, string imageurl, string name, string message, string priority)
        {
            try
            {
                IssueBL issueBl = new IssueBL();
                Context.Response.Write(issueBl.AddIssue(gpslat, gpslng, imageurl, name, priority, message));
            }
            catch (Exception ex) {
                Context.Response.Write(GetExceptionMessage(ex));
            }
        }

        [WebMethod]
        public void UpdateIssues(string uid, string gpslat, string gpslng, string imageurl, string status)
        {
            try
            {
                IssueBL issueBl = new IssueBL();
                if (issueBl.UpdateIssueStatus(uid, gpslat, gpslng, imageurl, status))
                {
                    Context.Response.Write( "1");
                }
                else
                     Context.Response.Write( "0");

            }
            catch (Exception ex)
            {
                Context.Response.Write(GetExceptionMessage(ex));
            }
        }

        [WebMethod]
        public void GetAllIssues()
        {
            try
            {
                IssueBL issueBl = new IssueBL();
                Context.Response.Write(issueBl.GetAllIssues());
            }
            catch (Exception ex)
            {
                Context.Response.Write( GetExceptionMessage(ex));
            }
        }

    }
}
