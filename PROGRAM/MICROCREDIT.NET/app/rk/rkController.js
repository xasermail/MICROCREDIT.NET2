appModule.controller("rkController",

  function($scope, $location) {
    
    $scope.btnEditClick = function(COUNTER0_FIO, LPUIN_FIO) {
      $location.path("/Form/" + COUNTER0_FIO + "/" + LPUIN_FIO);
    }

  }

);