namespace DaveVentura.WebApiExtendedTemplate.Constants {
    public class ApiRoutes {
        //#if(UseSql)
        public static class Persons {
            public const string PERSONS = "persons";
            public const string ROUTE = "/api/" + PERSONS;
        }
        //#endif

        //#if(useMongo)
        public static class Posts {
            public const string POSTS = "posts";
            public const string ROUTE = "/api/" + POSTS;
        }
        //#endif
    }
}
