namespace WebApiExtendedTemplate.Common {
    public class ApiRoutes {
        public const string ID_ROUTE_PARAMETER = "{id}";
        public const string API_ROUTE = "/api";
        public static class Info {
            public const string INFO = "info";
            public const string ROUTE = $"{API_ROUTE}/{INFO}";
        }
        //#if(UseSql)
        public static class Persons {
            public const string PERSONS = "persons";
            public const string ROUTE = $"{API_ROUTE}/{PERSONS}";
            public const string ROUTE_BY_ID = $"{ROUTE}/{ID_ROUTE_PARAMETER}";
        }
        //#endif

        //#if(useMongo)
        public static class Posts {
            public const string POSTS = "posts";
            public const string ROUTE = $"{API_ROUTE}/{POSTS}";
            public const string ROUTE_BY_ID = $"{ROUTE}/{ID_ROUTE_PARAMETER}";
        }
        //#endif

        //#if(useAzureTable)
        public static class Publications {
            public const string PUBLICATIONS = "publications";
            public const string TYPE_ROUTE_PARAMETER = "{publicationType}";
            public const string TYPE_AND_ID_ROUTE_PARAMETERS = $"{TYPE_ROUTE_PARAMETER}/{ID_ROUTE_PARAMETER}";
            public const string ROUTE = $"{API_ROUTE}/{PUBLICATIONS}";
            public const string ROUTE_BY_TYPE = $"{ROUTE}/{TYPE_ROUTE_PARAMETER}";
            public const string ROUTE_BY_ID = $"{ROUTE}/{TYPE_AND_ID_ROUTE_PARAMETERS}";
        }
        //#endif
    }
}
