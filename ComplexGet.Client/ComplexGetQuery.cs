using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexGet.Client
{
    public static class ComplexGetQuery
    {
        public static DataServiceQuery<T> CreateQuery<T>()
        {
            return new DataServiceContext(new Uri("http://localhost")).CreateQuery<T>("Fake");
        }
    }
}
