using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Text;

namespace ComplexGet.Client
{
    public class ComplexGet
    {
        public string Filter { get; set; }

        public string OrderBy { get; set; }

        public int PageNumber { get; set; }

        public int ItemsPerPage { get; set; }
        
        public ComplexGet()
        {
            this.PageNumber = 1;
            this.ItemsPerPage = 25;
            this.Filter = "";
            this.OrderBy = "";
        }

        public static DataServiceQuery<T> CreateQuery<T>()
        {
            return new DataServiceContext(new Uri("http://localhost")).CreateQuery<T>("Fake");
        }

        public string ToQueryString()
        {
            // Get all properties on the object
            var properties = new Dictionary<string, object>();
            properties.Add("Filter", this.Filter);
            properties.Add("OrderBy", this.OrderBy);
            properties.Add("PageNumber", this.PageNumber);
            properties.Add("ItemsPerPage", this.ItemsPerPage);

            // Get names for all IEnumerable properties (excl. string)
            var propertyNames = properties
                .Select(x => x.Key)
                .ToList();

            // Concat all key/value pairs into a string separated by ampersand
            return string.Join("&", properties
                .Select(x => string.Concat(
                    Uri.EscapeDataString(x.Key), "=",
                    Uri.EscapeDataString(x.Value.ToString()))));
        }
    }
}