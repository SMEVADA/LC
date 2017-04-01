app.controller('CustomerController', CustomerController);
CustomerController.$inject = ['$scope', 'filterFilter', 'LcService'];
function CustomerController($scope, filterFilter, LcService) {
    /* start section for default for pagenation,search,select ,reset filter  */
    var cc = this;
    cc.items = [];
    cc.search = {};
    cc.filter = filter;
    cc.resetfilter = resetfilter;
    cc.currentPage = 1;
    cc.NcurrentPage = 1;
    cc.totalItems = 0;
    cc.entryLimit = 10; // items per page
    cc.selectvalues = [{ Id: '10', value: 10 }, { Id: '25', value: 25 }, { Id: '50', value: 50 }, { Id: '100', value: 100 }];
    cc.noOfPages = Math.ceil(cc.totalItems / cc.entryLimit);
    cc.statrtPageFunction = statrtPageFunction;
    cc.endPageFunction = endPageFunction;
    cc.totalPageFunction = totalPageFunction;

    function totalPageFunction() {
        return cc.totalItems;
    }
    function endPageFunction() {
        return cc.NcurrentPage * cc.entryLimit;
    }
    function statrtPageFunction() {
        if (cc.NcurrentPage == 1)
            return 1;
        else
            return (cc.NcurrentPage - 1) * cc.entryLimit;
    }

    function getPaginationValue() {
        cc.statrtPageFunction();
        cc.endPageFunction();
        cc.totalPageFunction();
    }
    $scope.$watch('cc.currentPage', function (newVal, oldVal) {
            cc.currentPage = newVal;
            cc.NcurrentPage = newVal;
            cc.filter();
    }, true);

    function resetfilter() {
        cc.search = {};
        cc.filter();
    }
    /* end section for default for pagenation,search,select ,reset filter  */

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

    function validateValues() {
        cc.currentPage = setNullIdInvalid(cc.currentPage);
        cc.entryLimit = setNullIdInvalid(cc.entryLimit);
        cc.search.firstname = setNullIdInvalid(cc.search.firstname);
        cc.search.mobileNo = setNullIdInvalid(cc.search.mobileNo);
        cc.search.emailId = setNullIdInvalid(cc.search.emailId);
        cc.search.address = setNullIdInvalid(cc.search.address);
    }

    function filter() {
        resetprocess();
        StartLoading();
        validateValues();
        var url = "/api/CustomerAPI/GetAllByParameters?PageNumber=" + cc.currentPage + "&PageSize=" + cc.entryLimit + "&Name=" + cc.search.firstname + "&MobileNo=" + cc.search.mobileNo + "&EmailId=" + cc.search.emailId + "&Address=" + cc.search.address;
        LcService.getAllCustomerListByParameters(url).success(function (data, status, headers, config) {
            if (status == 200 && data != undefined) {
                    setTimeout(function () {
                        cc.CustomerList = data;
                        cc.items = data;
                        if (data.length > 0)
                            cc.totalItems = data[0].TotalRows;
                        else
                            cc.totalItems = 0;
                        cc.noOfPages = Math.ceil(cc.totalItems / cc.entryLimit);
                        cc.filtered = filterFilter(cc.items, {});
                        getPaginationValue();
                        $scope.$apply();
                    }, 10);
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

    LoadPremissionPageSettings();

    function GetAllCustomers() {
        resetprocess();
        StartLoading();
        var url = "/api/CustomerAPI/GetAll/";
        LcService.getAllCustomerList(url).success(function (data, status, headers, config) {
            if (status == 200) {
                cc.CustomerList = data;
                cc.items = data;
                cc.totalItems = data[0].TotalRows;
                cc.noOfPages = Math.ceil(cc.totalItems / cc.entryLimit);
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
        resetprocess();
        StartLoading();
        var url = "/api/CustomerAPI/GetById?Id=" + Id;
        LcService.getCustomerById(url).success(function (data, status, headers, config) {
            if (status == 200) {
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

app.filter('startFrom', function () {
    return function (input, start) {
        if (input) {
            if (input.length > start) {
                start = +start;
                return input.slice(start);
            }
            else
                return input;
        }
        return [];
    };
});