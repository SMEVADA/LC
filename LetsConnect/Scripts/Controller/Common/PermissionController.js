app.controller('PermissionController', PermissionController);
PermissionController.$inject = ['$scope', 'LcService'];

function PermissionController($scope, LcService) {

    $scope.PermissionsList = [];
    $scope.AdminList = [];
    $scope.process = {};
    $scope.process.isloading = false;
    $scope.process.isErrorMsg = false;
    $scope.process.isSuccessMsg = false;
    $scope.view = {};
    $scope.selectedAdmin = null;
    $scope.selectedValue = null;

    //not working so using id
    $scope.view.isList = true;

    LoadPremissionPageSettings();

    function getQueryValue() {
        if(window.location.href.indexOf('=')>-1)
        {
            debugger;
            var valueArray = window.location.href.split('=');
            var value = valueArray[valueArray.length - 1];
            $scope.selectedValue = parseInt(value);
            $scope.GetAllPermissionsByAdminId($scope.selectedValue);

        }
    }
    
     //get all Admin List
    function GetAllAdministratives() {
        resetprocess();
        StartLoading();
        //$scope.process.isloading = true;

        var url = "/api/AdminAPI/GetAll/";
        LcService.getAllAdminList(url).success(function (data, status, headers, config) {
            if (status == 200) {
                $scope.AdminList = data;
                //$scope.process.isloading = false;
                StopLoading();
                getQueryValue();

            }
            else if (data == "false") {
                AddProcessErrorValue(true, 'Error has been occured , please contact administrator.!', false);
            }
            else {
                AddProcessErrorValue(true, 'Error has been occured , please contact administrator.!', false);
            }
        }).error(function (data) {
            AddProcessErrorValue(true, 'Error has been occured , please contact administrator.!', false);
        });

    }
    GetAllAdministratives();
    //get all permission List
    $scope.GetAllPermissionsByAdminId = function (id) {
        debugger;
        resetprocess();
        StartLoading();

        var url = "/api/PermissionAPI/GetPermissionsByAdminId?Id=" + id;
        LcService.GetPermissionsByAdminId(url).success(function (data, status, headers, config) {
            if (status == 200) {
                $scope.PermissionsList = data;
                StopLoading();
            }
            else if (data == "false") {
                AddProcessErrorValue(true, 'Error has been occured , please contact administrator.!', false);
            }
            else {
                AddProcessErrorValue(true, 'Error has been occured , please contact administrator.!', false);
            }
        }).error(function (data) {
            AddProcessErrorValue(true, 'Error has been occured , please contact administrator.!', false);
        });

    }

    // Button Click Event
    $scope.UpdatePermissions = function (PermissionsList) {
        debugger; 
        resetprocess();
        StartLoading();

        var url = "/api/PermissionAPI/UpdateMultiple/";
        LcService.registerAdmin(url, PermissionsList).success(function (data, status, headers, config) {
            if (status == 200 && data == 1) {
                console.log(data, status, headers, config,'data, status, headers, config');
                AddProcessSuccessValue(true, 'Permisions Updated Successfully',false);
                $scope.view.isList = true;
                StopLoading();
            }
            else if (data == "false") {
                AddProcessErrorValue(true, 'Error has been occured , please contact administrator.!', false);
            }
            else {
                AddProcessErrorValue(true, 'Error has been occured , please contact administrator.!', false);
            }
        }).error(function (data) {
            AddProcessErrorValue(true, 'Error has been occured , please contact administrator.!', false);
        });

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