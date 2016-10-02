"use strict";

// контроллер
formCtrl = myApp.controller("formCtrl", function formCtrl($scope, $http) {

  // затемнённый фон для фотографирования
  $scope.clientPhotoFormBackgroundShow = false;

  $scope.imgClientPhoto = "/Content/no-user-image.gif";

  // сделать фото
  $scope.clientPhotoFormShow = function clientPhotoFormShow() {

    $scope.clientPhotoFormBackgroundShow = true;

    Webcam.set({
      width: 320,
      height: 240,
      image_format: 'jpeg',
      jpeg_quality: 90
    });
    Webcam.attach('#my_camera');

  };

  $scope.takeSnapshot = function takeSnapshot() {
    // сделать фото и получить данные
    Webcam.snap(function (data_uri) {
      // отобразить результат на странице
      document.getElementById('my_camera_results').innerHTML =
        '<h2>Here is your image:</h2>' +
        '<img src="' + data_uri + '"/>';
    });
  };

  // сохранить результаты сделанного фото
  $scope.clientPhotoFormSave = function clientPhotoFormSave() {
    $scope.imgClientPhoto = document.getElementById('my_camera_results')
                                    .getElementsByTagName("img")[0].getAttribute("src");
    $scope.clientPhotoFormBackgroundShow = false;
    Webcam.reset()
  };

  // закрыть окно фотосъемки
  $scope.clientPhotoFormClose = function clientPhotoFormClose() {
    $scope.clientPhotoFormBackgroundShow = false;
    Webcam.reset()
  };



});