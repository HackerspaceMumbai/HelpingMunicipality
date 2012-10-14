using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Backpack;
using System.Configuration;
using Backpack.Geocode;
namespace DataObjects
{
    public class IssuesDL : ParentData
    {
        public int InsertIssues(string gpslat, string gpslng, string imageurl, string name, string priority, string message)
        {
            string query = "insert into issues (issueLat, issueLng, issueImage, message, priority, name) values (@issueLat, @issueLng, @issueImage, @message, @priority, @name);" +
                            "select max(id) as id from issues";
            int maxId = 0;
            this.AddParameter("@issueLat", gpslat, 0);
            this.AddParameter("@issueLng", gpslng, 0);
            this.AddParameter("@issueImage", imageurl, 0);
            this.AddParameter("@name", name, 0);
            this.AddParameter("@message", message, 0);
            this.AddParameter("@priority", priority, 0); 
            DataSet ds = this.ExecuteDataSet(query);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    maxId = GetInt(dt.Rows[0]["id"]);
                }

            }
            return maxId;
        }

        public List<Issues> SelectAllIssues()
        {
            string query = "select * from issues";
            DataSet ds = this.ExecuteDataSet(query);
            List<Issues> lstIssues = null;
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    lstIssues = new List<Issues>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string lat = GetString(dt.Rows[i]["issueLat"]); 
                        string lng = GetString(dt.Rows[i]["issueLng"]);
                        CommonUtils.RequestAPI rapi = new CommonUtils.RequestAPI(ConfigurationManager.AppSettings["geocodingApi"].ToString());
                        rapi.AddParams("@latlng", lat + "," + lng);
                        string address = rapi.Response;
                        RootObject root = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<RootObject>(address);
                        if (root.results.Count > 0)
                        {
                            address = root.results[0].formatted_address;
                            address = address.Substring(0, address.IndexOf(", Maharashtra, India"));
                        }
                        else
                        {
                            address = string.Empty;
                        }

                        Issues issues = new Issues(
                            GetString(dt.Rows[i]["id"]), GetString(dt.Rows[i]["issueLat"]), GetString(dt.Rows[i]["issueLng"]),
                            GetString(dt.Rows[i]["fixLat"]), GetString(dt.Rows[i]["fixLng"]), GetString(dt.Rows[i]["issueImage"]), GetString(dt.Rows[i]["fixImage"]),
                            GetString(dt.Rows[i]["message"]), GetString(dt.Rows[i]["priority"]), GetString(dt.Rows[i]["name"]),
                            GetString(dt.Rows[i]["status"]), address, GetDate(dt.Rows[i]["creationDate"]).ToString("hh:mm tt dd/MM/yyyy"), GetString(dt.Rows[i]["modificationDate"]));
                        lstIssues.Add(issues);
                    }
                }

            }
            return lstIssues;
        }

        public int UpdateIssue(string uid, string gpsLat, string gpsLng, string imageUrl, string status)
        {
            string query = "update issues set fixLat = @fixLat, fixLng = @fixLng, fixImage = @fixImage, status=@status where id = @id";

            this.AddParameter("@fixLat", gpsLat, 0);
            this.AddParameter("@fixLng", gpsLng, 0);
            this.AddParameter("@fixImage", imageUrl, 0);
            this.AddParameter("@status", status, 0);
            this.AddParameter("@id", uid, 0);
            return this.ExecuteNonQuery(query);


        }


    }
}
