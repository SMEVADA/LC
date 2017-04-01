app.controller('AdministratorController', AdministratorController);
AdministratorController.$inject = ['$scope', 'LcService'];

function AdministratorController($scope, LcService) {
    $scope.Administrator = {};
    $scope.AdministratorList = [];
    $scope.process = {};
    $scope.process.isloading = false;
    $scope.process.isErrorMsg = false;
    $scope.process.isSuccessMsg = false;
    $scope.view = {};

    //not working so using id
    $scope.view.isList = true;
    $("#List").show();
    $("#NonList").hide();

    $("#content-page").show();
    $("#loading").hide();

    //general functions
    $scope.resetprocess = function () {
        $scope.process = {};
        $scope.process.isloading = false;
        $scope.process.isErrorMsg = false;
        $scope.process.isSuccessMsg = false;
        $("#content-page").show();
        $("#loading").hide();
    }
    $scope.Loadingprocess = function () {
        $scope.process = {};
        $scope.process.isloading = true;
        $("#content-page").hide();
        $("#loading").show();
    }
    $scope.AddProcessErrorValue = function (isErrorMsg, Msg, isloading) {
        $scope.process.isErrorMsg = isErrorMsg;
        $scope.process.ErrorMsg = Msg;
        $scope.process.isloading = isloading;
        $("html, body").animate({ scrollTop: 0 }, "slow");
        $("#content-page").show();
        $("#loading").hide();
    }
    $scope.AddProcessSuccessValue = function (isSuccessMsg, Msg, isloading) {
        $scope.process.isSuccessMsg = isSuccessMsg;
        $scope.process.SuccessMsg = Msg;
        $scope.process.isloading = isloading;
        $("html, body").animate({ scrollTop: 0 }, "slow");
        $("#content-page").show();
        $("#loading").hide();
    }

    //get all Admin List
    function GetAll() {
        $scope.resetprocess();
        $scope.process.isloading = true;
        $("#content-page").show();
        $("#loading").hide();

        var url = "/api/AdminAPI/GetAll/";
        LcService.getAllAdminList(url).success(function (data, status, headers, config) {
            if (status == 200) {
                $scope.AdministratorList = data;
                $scope.process.isloading = false;
            }
            else if (data == "false") {
                $scope.AddProcessErrorValue(true, 'Please Enter Valid Credentials.', false);
            }
            else {
                $scope.AddProcessErrorValue(true, 'Error has been occured , please contact administrator.!', false);
            }
        }).error(function (data) {
            $scope.AddProcessErrorValue(true, 'Error has been occured , please contact administrator.!', false);
        });

    }
    GetAll();

    //add new section
    $scope.AddNewSection = function () {
        $scope.view.isList = false;
        setTimeout(function () {
            $("#List").hide();
            $("#NonList").show();
            $scope.view.isList = false;
            console.log($scope.view.isList);
            $scope.$apply();
        }, 10);

    }

    $scope.BackToList = function () {
        $("#List").show();
        $("#NonList").hide();
    }

    // Button Click Event
    $scope.Register = function (Administrator) {
        $scope.resetprocess();
        $scope.process.isloading = true;
        $("#content-page").show();
        $("#loading").hide();

        if (!isNullOrEmpty(Administrator.userName)) {
            $scope.AddProcessErrorValue(true, 'Please Enter User Name.', false);
            return false;
        }
        if (!isNullOrEmpty(Administrator.password)) {
            $scope.AddProcessErrorValue(true, 'Please Enter Password.', false);
            return false;
        }
        if (!isNullOrEmpty(Administrator.fullName)) {
            $scope.AddProcessErrorValue(true, 'Please Enter Full Name.', false);
            return false;
        }
        if (!isNullOrEmpty(Administrator.emailID)) {
            $scope.AddProcessErrorValue(true, 'Please Enter Email-Id.', false);
            return false;
        }
        if (!isValidEmailAddress(Administrator.emailID)) {
            $scope.AddProcessErrorValue(true, 'Please Enter valid EmailAddress.', false);
            return false;
        }
        if (!isNullOrEmpty(Administrator.mobileNo)) {
            $scope.AddProcessErrorValue(true, 'Please Enter Mobile No.', false);
            return false;
        }
        
        if (Administrator.administratorId == undefined || Administrator.administratorId == null || Administrator.administratorId == "")
            var url = "/api/AdminAPI/Add/";
        else
            var url = "/api/AdminAPI/Update/";

        if (!isNullOrEmpty(Administrator.administratorId))
            Administrator.administratorId = 0;
        if (!isNullOrEmpty(Administrator.isActive))
            Administrator.isActive = true;
        if (!isNullOrEmpty(Administrator.administratorType))
            Administrator.administratorType = 2

        LcService.registerAdmin(url, Administrator).success(function (data, status, headers, config) {
            if (status == 200) {
                $scope.AddProcessSuccessValue(true, 'Admin Registered Successfully', false);
                $scope.view.isList = true;
                debugger;
                window.location.href = window.location.origin + "/Admin/Admin/PemissionList?AdministratorId=" + data.administratorId;
            }
            else if (data == "false") {
                $scope.AddProcessErrorValue(true, 'Error has been occured , please contact administrator.!', false);
            }
            else {
                $scope.AddProcessErrorValue(true, 'Error has been occured , please contact administrator.!', false);
            }
        }).error(function (data) {
            $scope.AddProcessErrorValue(true, 'Error has been occured , please contact administrator.!', false);
        });

    }

    //Edit
    $scope.Edit = function (Administrator) {
        debugger;
        $scope.resetprocess();
        $scope.process.isloading = true;
        $("#content-page").show();
        $("#loading").hide();
        var url = "/api/AdminAPI/GetAdminById?id=" + Administrator.administratorId;
        LcService.getAdminById(url).success(function (data, status, headers, config) {
            if (status == 200) {
                $("#List").hide();
                $("#NonList").show();
                $scope.Administrator = data;
                $scope.process.isloading = false;
            }
            else if (data == "false") {
                $scope.AddProcessErrorValue(true, 'Error has been occured , please contact administrator.!', false);
            }
            else {
                $scope.AddProcessErrorValue(true, 'Error has been occured , please contact administrator.!', false);
            }
        }).error(function (data) {
            $scope.AddProcessErrorValue(true, 'Error has been occured , please contact administrator.!', false);
        });
    }

    //delete
    $scope.Delete = function (Administrator) {
        $scope.resetprocess();
        $scope.process.isloading = true;
        $("#content-page").show();
        $("#loading").hide();

        var url = "/api/AdminAPI/Delete?administratorId=" + Administrator.administratorId;
        LcService.deleteAdmin(url).success(function (data, status, headers, config) {
            if (status == 200) {
                $scope.process.isloading = false;
                GetAll();
            }
            else if (data == "false") {
                $scope.AddProcessErrorValue(true, 'Error has been occured , please contact administrator.!', false);
            }
            else {
                $scope.AddProcessErrorValue(true, 'Error has been occured , please contact administrator.!', false);
            }
        }).error(function (data) {
            $scope.AddProcessErrorValue(true, 'Error has been occured , please contact administrator.!', false);
        });
    }

    // Button Click Event
    $scope.Update = function (Administrator) {
        $scope.resetprocess();
        $scope.process.isloading = true;
        $("#content-page").show();
        $("#loading").hide();

        if (!isNullOrEmpty(Administrator.userName)) {
            $scope.AddProcessErrorValue(true, 'Please Enter User Name.', false);
            return false;
        }
        if (!isNullOrEmpty(Administrator.password)) {
            $scope.AddProcessErrorValue(true, 'Please Enter Password.', false);
            return false;
        }
        if (!isNullOrEmpty(Administrator.fullName)) {
            $scope.AddProcessErrorValue(true, 'Please Enter Full Name.', false);
            return false;
        }

        if (!isNullOrEmpty(Administrator.emailID)) {
            $scope.AddProcessErrorValue(true, 'Please Enter Email-Id.', false);
            return false;
        }

        if (!isNullOrEmpty(Administrator.mobileNo)) {
            $scope.AddProcessErrorValue(true, 'Please Enter Mobile No.', false);
            return false;
        }

        if (!isValidEmailAddress(Administrator.emailID)) {
            $scope.AddProcessErrorValue(true, 'Please Enter valid EmailAddress.', false);
            return false;
        }

        var url = "/api/AdminAPI/Update/";
        LcService.registerAdmin(url, Administrator).success(function (data, status, headers, config) {
            if (status == 200) {
                $scope.AddProcessSuccessValue(true, 'Admin Registered Successfully', false);
                $scope.view.isList = true;
                window.location.href = window.location.origin + "/Admin/Admin/PemissionList?AdministratorId=" + data.administratorId;
            }
            else if (data == "false") {
                $scope.AddProcessErrorValue(true, 'Error has been occured , please contact administrator.!', false);
            }
            else {
                $scope.AddProcessErrorValue(true, 'Error has been occured , please contact administrator.!', false);
            }
        }).error(function (data) {
            $scope.AddProcessErrorValue(true, 'Error has been occured , please contact administrator.!', false);
        });

    }

    // For mobile no validation
    $scope.OnlyNumber = function () {
        if ($scope.Administrator.mobileNo == null || $scope.Administrator.mobileNo == undefined) {
            $scope.Administrator.mobileNo = "";
        }
        else {
            if (isNaN($scope.Administrator.mobileNo) == true)
                $scope.Administrator.mobileNo = "";
        }
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