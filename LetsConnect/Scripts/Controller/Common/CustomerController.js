var app = angular.module('LC');
app.controller('CustomerController', CustomerController);
CustomerController.$inject = ['LcService'];

function CustomerController(LcService) {
    var cc = this;
    cc.CustomerList = [];
    cc.Customer = {};
    cc.view = {};
    var Id = 0;
    cc.GetCustomerById = GetCustomerById;
    cc.UpdateCustomer = UpdateCustomer;
    cc.AddCustomer = AddCustomer;
    cc.DeleteCustomer = DeleteCustomer;
    cc.OnlyNumber = OnlyNumber;
    cc.OnlyDecimalNumber = OnlyDecimalNumber;
    cc.AddNewSection = AddNewSection;
    cc.BackToList = BackToList;

    LoadPremissionPageSettings();

    function GetAllCustomers() {
        resetprocess();
        StartLoading();
        var url = "/api/CustomerAPI/GetAll/";
        LcService.getAllCustomerList(url).success(function (data, status, headers, config) {
            if (status == 200) {
                debugger;
                cc.CustomerList = data;
                StopLoading();
                ShowList();
            }
            else if (data == "false") {
                AddProcessErrorValue(true, 'Error has been occured , please contact administrator.!', false);
                ShowList();
            }
            else {
                AddProcessErrorValue(true, 'Error has been occured , please contact administrator.!', false);
                ShowList();
            }
        }).error(function (data) {
            AddProcessErrorValue(true, 'Error has been occured , please contact administrator.!', false);
            ShowList();
        });

    }
    GetAllCustomers();

    function GetCustomerById(Id) {
        debugger;
        resetprocess();
        StartLoading();

        var url = "/api/CustomerAPI/GetById?Id=" + Id;
        LcService.getCustomerById(url).success(function (data, status, headers, config) {
            if (status == 200) {
                debugger;
                cc.Customer = data;
                StopLoading();
                HideList();
            }
            else if (data == "false") {
                AddProcessErrorValue(true, 'Error has been occured , please contact administrator.!', false);
                HideList();
            }
            else {
                AddProcessErrorValue(true, 'Error has been occured , please contact administrator.!', false);
                HideList();
            }
        }).error(function (data) {
            AddProcessErrorValue(true, 'Error has been occured , please contact administrator.!', false);
            HideList();
        });

    }

    function UpdateCustomer(customer) {
        debugger;
        resetprocess();
        StartLoading();

        if (!isNullOrEmpty(customer.firstName)) {
            AddProcessErrorValue(true, 'Please Enter First Name.', false);
            return false;
        }

        if (!isNullOrEmpty(customer.lastName)) {
            AddProcessErrorValue(true, 'Please Enter Last Name.', false);
            return false;
        }

        if (!isNullOrEmpty(customer.emailId)) {
            AddProcessErrorValue(true, 'Please Enter Emai Id.', false);
            return false;
        }

        if (!isNullOrEmpty(customer.mobileNo)) {
            AddProcessErrorValue(true, 'Please Enter Mobile No.', false);
            return false;
        }

        if (!isNullOrEmpty(customer.address)) {
            AddProcessErrorValue(true, 'Please Enter Address.', false);
            return false;
        }

        if (!isNullOrEmpty(customer.latitude)) {
            AddProcessErrorValue(true, 'Please Enter Latitude.', false);
            return false;
        }

        if (!isNullOrEmpty(customer.longitude)) {
            AddProcessErrorValue(true, 'Please Enter Longitude.', false);
            return false;
        }

        if (!isValidEmailAddress(customer.emailId)) {
            AddProcessErrorValue(true, 'Please Enter valid EmailAddress.', false);
            return false;
        }

        var url = "/api/CustomerAPI/Update/";
        LcService.updateCustomer(url, customer).success(function (data, status, headers, config) {
            if (status == 200 && data == 1) {
                AddProcessSuccessValue(true, 'Customer Updated Successfully', false);
                GetAllCustomers();
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

    function AddCustomer(customer) {
        resetprocess();
        StartLoading();

        if (!isNullOrEmpty(customer.firstName)) {
            AddProcessErrorValue(true, 'Please Enter First Name.', false);
            return false;
        }

        if (!isNullOrEmpty(customer.lastName)) {
            AddProcessErrorValue(true, 'Please Enter Last Name.', false);
            return false;
        }

        if (!isNullOrEmpty(customer.emailId)) {
            AddProcessErrorValue(true, 'Please Enter Emai Id.', false);
            return false;
        }

        if (!isNullOrEmpty(customer.mobileNo)) {
            AddProcessErrorValue(true, 'Please Enter Mobile No.', false);
            return false;
        }

        if (!isNullOrEmpty(customer.address)) {
            AddProcessErrorValue(true, 'Please Enter Address.', false);
            return false;
        }

        if (!isNullOrEmpty(customer.latitude)) {
            AddProcessErrorValue(true, 'Please Enter Latitude.', false);
            return false;
        }

        if (!isNullOrEmpty(customer.longitude)) {
            AddProcessErrorValue(true, 'Please Enter Longitude.', false);
            return false;
        }

        if (!isValidEmailAddress(customer.emailId)) {
            AddProcessErrorValue(true, 'Please Enter valid EmailAddress.', false);
            return false;
        }

        if (customer.customerId == undefined || customer.customerId == null || customer.customerId == 0)
            var url = "/api/CustomerAPI/Add/";
        else
            var url = "/api/CustomerAPI/Update/";

        LcService.addCustomer(url, customer).success(function (data, status, headers, config) {
            console.log(data, status, headers, config, 'data, status, headers, config');
            if (status == 200 && data == 1) {
                AddProcessSuccessValue(true, 'Customer Added Successfully', false);
                GetAllCustomers();
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

    function DeleteCustomer(Id) {
        debugger;
        resetprocess();
        StartLoading();

        var url = "/api/CustomerAPI/Delete?Id=" + Id;
        LcService.deleteCustomerById(url).success(function (data, status, headers, config) {
            if (status == 200) {
                GetAllCustomers();
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

    function AddNewSection() {
        $("#List").hide();
        $("#NonList").show();
    }

    function BackToList() {
        $("#List").show();
        $("#NonList").hide();
    }

    function OnlyNumber() {
        if (cc.Customer.mobileNo == null || cc.Customer.mobileNo == undefined) {
            cc.Customer.mobileNo = "";
        }
        else {
            if (isNaN(cc.Customer.mobileNo) == true)
                cc.Customer.mobileNo = "";
        }
    }

    function OnlyDecimalNumber() {
        if (cc.Customer.latitude == null || cc.Customer.latitude == undefined) {
            cc.Customer.latitude = "";
        }
        else {
            if (isNaN(cc.Customer.latitude) == true)
                cc.Customer.latitude = "";
        }

        if (cc.Customer.longitude == null || cc.Customer.longitude == undefined) {
            cc.Customer.longitude = "";
        }
        else {
            if (isNaN(cc.Customer.longitude) == true)
                cc.Customer.longitude = "";
        }
    }

};

app.directive("limitTo", [function () {
    return {
        restrict: "A",
        link: function (scope, elem, attrs) {
            var limit = parseInt(attrs.limitTo);
            angular.element(elem).on("keypress", function (e) {
                if (this.value.length == limit) e.preventDefault();
            });
        }
    }
}]);