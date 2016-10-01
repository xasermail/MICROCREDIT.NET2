"use strict";
var myApp;
var myCtrl;
myApp = angular.module("myApp", []);

// контроллер
myCtrl = myApp.controller("myCtrl", function myCtrl($scope, $http) {

  // последние кредиты
  $http.get("/Home/GetLastCredits/").then(function getLastCredits(response) {
    $scope.lastCredits = response.data;
  });

  // последние платежи
  $http.get("/Home/GetLastPayments/").then(function getLastPayments(response) {
    $scope.lastPayments = response.data;
  });

  // сегодняшние займы
  $http.get("/Home/GetCurrentCredits/").then(function getCurrentCreditss(response) {
    $scope.currentCredits = response.data;
  });

});