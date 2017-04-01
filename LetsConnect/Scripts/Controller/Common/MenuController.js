var app = angular.module('LC');
app.controller('MenuController', MenuController);
MenuController.$inject = ['$scope', 'LcService'];

function MenuController($scope, LcService) {
    $scope.user = {};
    $scope.process = {};
    $scope.process.isloading = false;
    $scope.process.isErrorMsg = false;
    $scope.process.isSuccessMsg = false;

    $scope.resetprocess = function () {
        $scope.process = {};
        $scope.process.isloading = false;
        $scope.process.isErrorMsg = false;
        $scope.process.isSuccessMsg = false;
    }
    $scope.Loadingprocess = function () {
        $scope.process = {};
        $scope.process.isloading = true;
    }
    $scope.AddProcessValue = function (isErrorMsg, Msg, isloading) {
        $scope.process.isErrorMsg = isErrorMsg;
        $scope.process.ErrorMsg = Msg;
        $scope.process.isloading = isloading;
    }
    $scope.AddProcessSuccessValue = function (isSuccessMsg, Msg, isloading) {
        $scope.process.isSuccessMsg = isSuccessMsg;
        $scope.process.SuccessMsg = Msg;
        $scope.process.isloading = isloading;
    }
};

app.directive("limitTo", [function() {
    return {
        restrict: "A",
        link: function(scope, elem, attrs) {
            var limit = parseInt(attrs.limitTo);
            angular.element(elem).on("keypress", function(e) {
                if (this.value.length == limit) e.preventDefault();
            });
        }
    }
}]);