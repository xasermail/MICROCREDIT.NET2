appModule.config(
  function ($routeProvider) {
    $routeProvider
      .when(
        "/",
        {
           templateUrl: "/Home/Index"
          ,controller : "rkController"
        }
      )
      .when(
        "/Form",
        {
          templateUrl: "/Home/Form",
          controller: "docController"
        }
      )
      .when(
        "/Form/:COUNTER0_FIO/:LPUIN_FIO",
        {
          templateUrl: "/Home/Form",
          controller: "docController"
        }
      )
      .otherwise(
        {redirectTo: "/"}
      )
  }
);