using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using Exceptions;

namespace CommonUtils
{
    public class RequestAPI
    {
        private string _url = string.Empty;
        private List<KeyValuePair<string, string>> _parameters = null;

        public string URL
        {
            get { return _url; }
            set { _url = value; }
        }

        /// <summary>
        /// Gets response from called webservice
        /// </summary>
        public string Response
        {
            get { return GetResponse(); }
        }

        public RequestAPI()
            : base()
        {

        }
        public RequestAPI(string url)
        {
            _url = url;
        }

        /// <summary>
        /// Get the response from the given url
        /// </summary>
        /// <returns></returns>
        private string GetResponse()
        {
            try
            {
                string str = "";
                if (_url != string.Empty && _parameters != null)
                {
                    for (int i = 0; i < _parameters.Count; i++)
                    {
                        _url = _url.Replace(_parameters[i].Key, _parameters[i].Value);
                    }
                    string strReq = _url;
                    WebRequest req = WebRequest.Create(strReq);
                    req.Method = "GET";
                    WebResponse res = req.GetResponse();
                    StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
                    str = sr.ReadToEnd();
                    return str;
                }
                else
                {
                    throw new Exception("You must set the url and parameters before call");
                }
                _parameters = null;
            }
            catch (Exception ex)
            {
                _parameters = null;
                //CommonException.HandleException(ref ex);
                throw ex;
            }
        }


        /// <summary>
        /// Add parameters to the url if it has any in @paramName format
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddParams(string key, string value)
        {
            if (_parameters == null)
            {
                _parameters = new List<KeyValuePair<string, string>>();
                _parameters.Add(new KeyValuePair<string, string>(key, value));
            }
            else
            {
                _parameters.Add(new KeyValuePair<string, string>(key, value));
            }
        }
    }
}
