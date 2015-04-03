using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComplexGet.Contracts
{
    public class ComplexGetModel
    {
        public string Filter { get; set; }

        public string Sort { get; set; }

        public int Page { get; set; }

        public int PerPage { get; set; }

        public ComplexGetModel()
        {
            this.Page = 1;
            this.PerPage = 25;
            this.Filter = "";
            this.Sort = "";
        }

        public string ToQueryString()
        {
            // Get all properties on the object
            var properties = new Dictionary<string, object>();
            properties.Add("Filter", this.Filter);
            properties.Add("Sort", this.Sort);
            properties.Add("Page", this.Page);
            properties.Add("PerPage", this.PerPage);

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
