﻿"use strict";
var myApp;
var myCtrl;
myApp = angular.module("myApp", []);
myCtrl = myApp.controller("myCtrl", function myCtrl($scope, $http) {
  $http.get("/Home/GetLastCredits/").then(function getLastCredits(response) {
    $scope.lastCredits = response.data;
  });
});