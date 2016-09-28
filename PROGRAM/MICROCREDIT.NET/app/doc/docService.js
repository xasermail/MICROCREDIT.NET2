appModule.factory(
  "docService",
  function ($http) {
  
    var getDoc = function(COUNTER0_FIO, LPUIN_FIO) {
      return $http.get(
         "/Home/GetDoc"
        ,{params: {COUNTER0_FIO: COUNTER0_FIO, LPUIN_FIO: LPUIN_FIO}}
      );
    };

    var getSexOptions = function() {
      return $http.get("/Home/GetSexOptions");
    };
    
    return {
      getDoc: getDoc,
      getSexOptions: getSexOptions
    };
    
  }
);