appModule.controller(
  "docController",
  function docController($scope, $location, $routeParams, docService) {


    docService.getSexOptions().then(function (response) {
      console.log(response);
      $scope.SEX_OPTIONS = response.data;
    });
    
    
    if ($routeParams.COUNTER0_FIO) {

      docService.getDoc($routeParams.COUNTER0_FIO, $routeParams.LPUIN_FIO).then(function (response) {
        $scope.COUNTER0_FIO = response.data.COUNTER0_FIO;
        $scope.LPUIN_FIO = response.data.LPUIN_FIO;
        $scope.SURNAME = response.data.SURNAME;
        $scope.NAME = response.data.NAME;
        $scope.SECNAME = response.data.SECNAME;
        $scope.SEX = response.data.SEX;
        $scope.BIRTHDAY = new Date(response.data.BIRTHDAY);
      });

    }


    $scope.btnSaveClick = function() {
      alert("a");
    };

    $scope.redirectTo = function() {
      $location.path("/");
    };

    $scope.btnCancelClick = function() {
      $location.path("/");
    };

  }
);