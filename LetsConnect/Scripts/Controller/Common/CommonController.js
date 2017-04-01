app.controller('LoginController', LoginController);
LoginController.$inject = ['$scope', 'LcService'];

function LoginController($scope,LcService) {
    $scope.user = {};
    $scope.process = {};
    $scope.process.isloading = false;
    $scope.process.isErrorMsg = false;
    $scope.process.isSuccessMsg = false;


    ClearCookies();
    // Sign In Button Click Event
    $scope.CheckLogin = function (user) {
        $scope.resetprocess();
        $scope.process.isloading = true;
        var email = $("#emailAddress").val();
        var password = $("#password").val();
        if (!isNullOrEmpty(email)) {
            $scope.AddProcessErrorValue(true, 'Please Enter Email-Id.', false);
            return false;
        }

        if (!isNullOrEmpty(password)) {
            $scope.AddProcessErrorValue(true, 'Please Enter Password.', false);
            return false;
        }

        if (!isValidEmailAddress(email)) {
            $scope.AddProcessErrorValue(true, 'Please Enter valid EmailAddress.', false);
            return false;
        }
        
        

        var url = "/Common/Login?emailAddress=" + email + "&password=" + password;
        LcService.checkUserLogin(url).success(function (data, status, headers, config) {
            if (data == "success") {
                $scope.AddProcessSuccessValue(true, 'Login Successfully..! You Will Redirected in 5 Seconds.', true);

                setTimeout(function () {
                    $scope.AddProcessSuccessValue(true, 'Login Successfully..! You Will Redirected in 4 Seconds.', true);
                    $scope.$apply();
                }, 1000);

                setTimeout(function () {
                    $scope.AddProcessSuccessValue(true, 'Login Successfully..! You Will Redirected in 3 Seconds.', true);
                    $scope.$apply();
                }, 2000);

                setTimeout(function () {
                    $scope.AddProcessSuccessValue(true, 'Login Successfully..! You Will Redirected in 2 Seconds.', true);
                    $scope.$apply();
                }, 3000);

                setTimeout(function () {
                    $scope.AddProcessSuccessValue(true, 'Login Successfully..! You Will Redirected in 1 Seconds.', true);
                    $scope.$apply();
                }, 4000);

                setTimeout(function () {
                    window.location.href = window.location.origin + "/Admin/Admin/index";
                    $scope.$apply();
                }, 5000);
               
            }
            else if (data == "false") {
                $scope.AddProcessValue(true, 'Please Enter Valid Credentials.', false);
            }
            else {
                $scope.AddProcessValue(true,'Error has been occured , please contact administrator.!',false);
            }
            }).error(function (data) {
                $scope.AddProcessValue(true, 'Error has been occured , please contact administrator.!', false);
            });
      
    }

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

app.controller('ForgotPasswordController', ForgotPasswordController);
ForgotPasswordController.$inject = ['$scope', 'LcService'];

function ForgotPasswordController($scope, LcService) {

    $scope.process = {};
    $scope.process.isloading = false;
    $scope.process.isErrorMsg = false;
    $scope.process.isSuccessMsg = false;

    // Sign In Button Click Event
    $scope.CheckEmail = function (user) {
        $scope.resetprocess();
        $scope.process.isloading = true;
        var email = $("#emailAddress").val();
        if (!isNullOrEmpty(email)) {
            $scope.AddProcessErrorValue(true, 'Please Enter Email-Id.', false);
            return false;
        }
        if (!isValidEmailAddress(email)) {
            $scope.AddProcessErrorValue(true, 'Please Enter valid EmailAddress.', false);
            return false;
        }

        var url = "/Common/Forgetpassword?emailAddress=" + email;
        LcService.checkUserLogin(url).success(function (data, status, headers, config) {
            if (data == "success") {

                $scope.AddProcessSuccessValue(true, 'Password Sent To Your Registered Email Id.', false);

                setTimeout(function () {
                    window.location.href = window.location.origin + "/Account/Login/";
                }, 5000);
               
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
    $scope.AddProcessErrorValue = function (isErrorMsg, Msg, isloading) {
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

