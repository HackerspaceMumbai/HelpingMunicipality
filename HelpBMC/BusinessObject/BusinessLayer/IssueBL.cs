using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataObjects;
using CommonUtils;
using Backpack;

namespace BusinessObjects
{
    public class IssueBL
    {
        public string AddIssue(string gpslat, string gpslng, string imageurl, string name, string priority, string message)
        {
            try
            {
                IssuesDL issueDl = new IssuesDL();
                string retVal = string.Empty;
                int maxVal = issueDl.InsertIssues(gpslat, gpslng, imageurl, name, priority, message);

                if (maxVal > 0)
                {
                    retVal = Convert.ToInt16(Enums.Status.Success) + "," + maxVal;

                }
                return retVal;
            }
            catch (Exception ex)
            {
                return "Something went wrong and it say's " + ex.Message;
            }
        }

        public string GetAllIssues()
        {
            try
            {
                IssuesDL issueDl = new IssuesDL();
                string retVal = string.Empty;
                List<Issues> lstIssues = issueDl.SelectAllIssues();

                System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string sJSON = oSerializer.Serialize(lstIssues);
                return sJSON;


            }
            catch (Exception ex)
            {
                return "Something went wrong and it say's " + ex.Message;
            }
        }

        public bool UpdateIssueStatus(string uid, string gpslat, string gpslng, string imageurl, string status)
        {
            try
            {
                IssuesDL issueDl = new IssuesDL();
                return issueDl.UpdateIssue(uid, gpslat, gpslng, imageurl, status) == 1 ? true : false;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
