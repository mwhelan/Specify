using System.Collections.Generic;
using Microsoft.AspNetCore.WebUtilities;

namespace ApiTemplate.Api.Contracts
{
    public static class ApiRoutes
    {
        private const string Root = "";      // E.g. "api"

        private const string Version = "";    // E.g. "/v1";

        public const string Base = Root + Version;

        public static class ToDo
        {
            public const string Get = Base + "/todo/{id}";
            public const string GetAll = Base + "/todo";
            public const string Create = Base + "/todo";
            public const string Update = Base + "/todo";
            public const string Delete = Base + "/todo/{id}";

            public static string GetFor(int id) => Get.Replace("{id}", id.ToString());
            public static string DeleteFor(int id) => Delete.Replace("{id}", id.ToString());
        }

        public static class Master
        {
            public static string GetFor<TEntity>(int id)
            {
                return Base + $"/master/{typeof(TEntity).Name}/{id}";
            }

            public static string GetAllFor<TEntity>(int page = 1,
                int? pageSize = null, string orderBy = null, bool orderByDesc = false,
                string filterField = null, string filterText = null)
            {
                var baseUrl = Base + $"/master/{typeof(TEntity).Name}";

                var queryArguments = new Dictionary<string, string> { { "page", page.ToString() } };
                if (pageSize.HasValue) queryArguments.Add("pageSize", pageSize.ToString());

                if (orderBy != null)
                {
                    queryArguments.Add("orderBy", orderBy);
                    queryArguments.Add("orderByDesc", orderByDesc.ToString());
                }

                if (filterField != null) queryArguments.Add("filterField", filterField);
                if (filterText != null) queryArguments.Add("filterText", filterText);

                var url = QueryHelpers.AddQueryString(baseUrl, queryArguments);

                return url;
            }

            public static string CreateFor<TEntity>()
            {
                return Base + $"/master/{typeof(TEntity).Name}";
            }

            public static string UpdateFor<TEntity>()
            {
                return Base + $"/master/{typeof(TEntity).Name}";
            }

            public static string DeleteFor<TEntity>(int id)
            {
                return Base + $"/master/{typeof(TEntity).Name}/{id}";
            }
        }

        public static class WeatherForecast
        {
            public const string GetAll = Base + "/weatherforecast";
        }

        public static class Identity
        {
            public const string Login = Base + "/identity/login";

            public const string Register = Base + "/identity/register";

            public const string Refresh = Base + "/identity/refresh";

            public const string FacebookAuth = Base + "/identity/auth/fb";
        }
    }
}