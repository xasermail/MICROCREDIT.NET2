"use strict";

// контроллер
reestrKlientovCtrl = myApp.controller("reestrKlientovCtrl", function reestrKlientovCtrl($scope, $http) {

  // поиск
  $scope.btnPoiskClickHandler = function btnPoiskClickHandler() {

    // результат поиска по реестру
    $http({
      method: "GET",
      url: "/Home/RequeryFio/",
      params: {
        fio: $scope.fio,
        phone: $scope.phone
      }
    }).then(function getRequeryFio(response) {
      $scope.reestrKlientovResults = response.data;
    });

  };

  $scope.neZakr = true;
  $scope.posl = true;

});