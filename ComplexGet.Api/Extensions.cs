using System;
using System.Collections.Generic;
using System.Web.OData;
using System.Web.OData.Builder;
using System.Web.OData.Query;

namespace ComplexGet.Api
{
    public static class Extensions
    {
        public static ODataQueryOptions CreateQueryOptions<T>(this ComplexGet.Client.ComplexGet complexGet) where T : class
        {
            var queryString = "";
            if (!string.IsNullOrWhiteSpace(complexGet.Filter))
            {
                queryString += "$filter=" + complexGet.Filter;
            }

            if (!string.IsNullOrWhiteSpace(complexGet.OrderBy))
            {
                if (queryString != "") queryString += "&";
                queryString += "$orderby=" + complexGet.OrderBy;
            }

            var skip = (complexGet.PageNumber - 1) * complexGet.ItemsPerPage;
            if (skip != 0)
            {
                if (queryString != "") queryString += "&";
                queryString += "$skip=" + skip;
            }

            if (complexGet.ItemsPerPage != 0)
            {
                if (queryString != "") queryString += "&";
                queryString += "$top=" + complexGet.ItemsPerPage;
            }

            ODataModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<T>("Fake");
            var ctx = new ODataQueryContext(modelBuilder.GetEdmModel(), typeof(T), new System.Web.OData.Routing.ODataPath(new List<System.Web.OData.Routing.ODataPathSegment>()));

            var msg = new System.Net.Http.HttpRequestMessage();
            msg.RequestUri = new Uri(string.Format("http://localhost?{0}", queryString));

            return new ODataQueryOptions(ctx, msg);
        }
    }
}
