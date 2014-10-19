using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Text;

namespace ComplexGet.Client
{
    public static class Extensions
    {
        //public static DataServiceQuery<T> CreateQuery<T>()
        //{
        //    var xddd = new DataServiceContext(new Uri("http://localhost"));

        //    return xddd.CreateQuery<T>("Fake") as DataServiceQuery<T>;
        //}

        public static ComplexGet Compile<T>(this DataServiceQuery<T> dataServiceQuery)
        {
            var query = dataServiceQuery.RequestUri.Query;

            var collection = HttpUtility.ParseQueryString(query);

            var filter = collection["$filter"] ?? "";
            var orderby = collection["$orderby"] ?? "";
            var skip = collection["$skip"] ?? "";
            var top = collection["$top"] ?? "";

            var int_skip = string.IsNullOrWhiteSpace(skip) ? 1 : int.Parse(skip);
            var int_top = string.IsNullOrWhiteSpace(top) ? 25 : int.Parse(top);

            var pageNumber = (int)(int_skip / int_top) > 0 ? (int)(int_skip / int_top) : 1;

            return new ComplexGet
            {
                Filter = filter,
                OrderBy = orderby,
                ItemsPerPage = int_top,
                PageNumber = pageNumber
            };
        }
    }
}
