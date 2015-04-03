using ComplexGet.Contracts;
using System;
using System.Collections.Generic;
using System.Web.Http.OData;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Query;

namespace ComplexGet.Api
{
    public static class Extensions
    {
        public static ODataQueryOptions CreateODataQueryOptions<T>(this ComplexGetModel complexGetModel) where T : class
        {
            var queryString = "";
            if (!string.IsNullOrWhiteSpace(complexGetModel.Filter))
            {
                queryString += "$filter=" + complexGetModel.Filter;
            }

            if (!string.IsNullOrWhiteSpace(complexGetModel.Sort))
            {
                if (queryString != "") queryString += "&";
                queryString += "$orderby=" + complexGetModel.Sort;
            }

            var skip = (complexGetModel.Page - 1) * complexGetModel.PerPage;
            if (skip != 0)
            {
                if (queryString != "") queryString += "&";
                queryString += "$skip=" + skip;
            }

            if (complexGetModel.PerPage != 0)
            {
                if (queryString != "") queryString += "&";
                queryString += "$top=" + complexGetModel.PerPage;
            }

            ODataModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<T>("Fake");
            var ctx = new ODataQueryContext(modelBuilder.GetEdmModel(), typeof(T));

            var msg = new System.Net.Http.HttpRequestMessage();
            msg.RequestUri = new Uri(string.Format("http://localhost?{0}", queryString));

            return new ODataQueryOptions(ctx, msg);
        }
    }
}
