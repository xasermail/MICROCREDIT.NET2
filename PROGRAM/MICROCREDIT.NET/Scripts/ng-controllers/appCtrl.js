"use strict";
var myApp;
var appCtrl;
var mainCtrl;
var reestrKlientovCtrl;
var formCtrl;
myApp = angular.module("myApp", []);


// контроллер
appCtrl = myApp.controller("appCtrl", function appCtrl($scope, $http) {

  $scope.formCtrlShow = false;

  // при нажатии на пункт меню отобразить div с соответствующим контроллером
  $scope.leftMenuClickHandler = function leftMenuClickHandler(e) {

    // тот <li> по которому фактически нажали
    var targetDataDivNgController;

    if (!e.target.getAttribute("data-div-ng-controller")) {
      return;
    }

    // <li data-div-ng-controller="reestrKlientovCtrl">
    targetDataDivNgController = e.target.getAttribute("data-div-ng-controller");

    // прячу div для всех контроллеров
    $("#leftMenuTopUl").find("[data-div-ng-controller]").each(function eachDataDivNgController(idx, item) {
      $scope[item.getAttribute("data-div-ng-controller") + "Show"] = false;
    });

    // показываю div соответствующий нажатому пункту меню
    $scope[targetDataDivNgController + "Show"] = true;

  };


  // отобразить Анкету
  $scope.showForm = function showForm() {
    $scope.formCtrlShow = true;
  };


  // на время отладки
  $scope.reestrKlientovCtrlShow = true;

});