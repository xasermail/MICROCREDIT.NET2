"use strict";

// контроллер
mainCtrl = myApp.controller("mainCtrl", function mainCtrl($scope, $http) {

  // последние кредиты
  $http.get("/Home/GetLastCredits/").then(function getLastCredits(response) {
    $scope.lastCredits = response.data;
  });

  // последние платежи
  $http.get("/Home/GetLastPayments/").then(function getLastPayments(response) {
    $scope.lastPayments = response.data;
  });

  // сегодняшние займы
  $http.get("/Home/GetCurrentCredits/").then(function getCurrentCredits(response) {
    $scope.currentCredits = response.data;
  });

  // события органайзера на сегодня
  $http.get("/Home/GetCurrentOrg/").then(function getCurrentOrg(response) {
    $scope.currentOrg = response.data;
  });

});