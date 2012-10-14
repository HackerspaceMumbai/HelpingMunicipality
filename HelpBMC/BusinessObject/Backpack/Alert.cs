using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backpack
{
    public class Issues
    {
        private string _gpsLat;
        private string _gpsLng;
        private string _message;
        private string _id;
        private string _priority;
        private string _reporter_name;
        private string _imageUrl;
        private string _fixLat;
        private string _fixLng;
        private string _status;
        private string _fixImage;
        private string _address;
        private string _creationDate;
        private string _modificationDate;

        public string GpsLat
        {
            get { return _gpsLat; }

        }

        public string GpsLng
        {
            get { return _gpsLng; }

        }
        public string Message
        {
            get { return _message; }

        }

        public string ID
        {
            get { return _id; }

        }

        public string Priority
        {
            get { return _priority; }

        }

        public string ImageUrl
        {
            get { return _imageUrl; }
        }

        public string ReportName
        {
            get
            {
                return _reporter_name;
            }
        }

        public string FixLat
        {
            get { return _fixLat; }
        }

        public string FixLng
        {
            get { return _fixLng; }
        }

        public string Status
        {
            get { return _status; }
        }

        public string FixImage
        {
            get { return _fixImage; }
        }

        public string Address
        {
            get { return _address; }
        }

        public string CreationDate
        {
            get { return _creationDate; }
        }

        public string ModificationDate
        {
            get { return _modificationDate; }
        }

        public Issues(string id, string gpsLat, string gpsLng, string fixLat, string fixLng, string imageUrl, string fixImage, string message, string priority, 
            string reporterName, string status, string address, string creationDate, string modificationDate)
        {
            _gpsLat = gpsLat;
            _gpsLng = gpsLng;
            _imageUrl = imageUrl;
            _message = message;
            _priority = priority;
            _reporter_name = reporterName;
            _id = id;
            _fixLat = fixLat;
            _fixLng = fixLng;
            _address = address;
            _creationDate = creationDate;
            _modificationDate = modificationDate;
            
        }
    }
}
