using System.Data.Services.Client;

namespace ComplexGet.Client
{
    public static class Extensions
    {
        public static ComplexGetModel Compile<T>(this DataServiceQuery<T> dataServiceQuery)
        {
            var query = dataServiceQuery.RequestUri.Query;

            var collection = HttpUtility.ParseQueryString(query);

            var filter = collection["$filter"] ?? "";
            var orderby = collection["$orderby"] ?? "";
            var skip = collection["$skip"] ?? "";
            var top = collection["$top"] ?? "";

            var int_skip = string.IsNullOrWhiteSpace(skip) ? 1 : int.Parse(skip);
            var int_top = string.IsNullOrWhiteSpace(top) ? 50 : int.Parse(top);

            var page = (int)(int_skip / int_top) > 0 ? (int)(int_skip / int_top) : 1;

            return new ComplexGetModel
            {
                Filter = filter,
                Sort = orderby,
                PerPage = int_top,
                Page = page
            };
        }
    }
}
