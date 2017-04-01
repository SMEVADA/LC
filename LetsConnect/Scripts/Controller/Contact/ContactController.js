app.controller('ContactController', ContactController);
ContactController.$inject = ['$scope', 'filterFilter', 'LcService'];
function ContactController($scope, filterFilter, LcService) {
   
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
	
    cc.ContactList = [];
    cc.Contact = {};
    cc.view = {};
    var Id = 0;
    cc.GetContactById = GetContactById;
    cc.UpdateContact = UpdateContact;
    cc.AddContact = AddContact;
    cc.DeleteContact = DeleteContact;
    cc.OnlyNumber = OnlyNumber;
    cc.AddNewSection = AddNewSection;
    cc.BackToList = BackToList;
    cc.ContactList =[];
	cc.CustomerList =[];
	 
	
	 function validateValues() {
        cc.currentPage = setNullIdInvalid(cc.currentPage);
        cc.entryLimit = setNullIdInvalid(cc.entryLimit);
        cc.search.name = setNullIdInvalid(cc.search.name);
        cc.search.mobileNo = setNullIdInvalid(cc.search.mobileNo);
        cc.search.emailId = setNullIdInvalid(cc.search.emailId);
        cc.search.customerId = setNullIdInvalid(cc.search.customerId);
    }

    function filter() {
        resetprocess();
        StartLoading();
        validateValues();
        debugger;
        var url = "/api/ContactAPI/GetAllByParameters?PageNumber=" + cc.currentPage + "&PageSize=" + cc.entryLimit + "&Name=" + cc.search.name + "&MobileNo=" + cc.search.mobileNo + "&EmailId=" + cc.search.emailId + "&CustomerId=" + cc.search.customerId;
        LcService.getAllContactListByParameters(url).success(function (data, status, headers, config) {
            if (status == 200 && data != undefined) {
                    setTimeout(function () {
                        cc.ContactList = data;
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
	filter();
	
	 function GetAllCustomers() {
        resetprocess();
        StartLoading();
        var url = "/api/CustomerAPI/GetAll/";
        LcService.getAllCustomerList(url).success(function (data, status, headers, config) {
            if (status == 200) {
                cc.CustomerList = data;
               
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
	
	
    LoadPremissionPageSettings();

    
    //function GetAllContacts() {
    //    resetprocess();
    //    StartLoading();
    //    var url = "/api/ContactAPI/GetAll/";
    //    LcService.getAllContactList(url).success(function (data, status, headers, config) {
    //        if (status == 200) {
    //            debugger;
    //            cc.ContactList = data;
    //            StopLoading();
    //            ShowList();
    //        }
    //        else if (data == "false") {
    //            AddProcessErrorValue(true, 'Error has been occured , please contact administrator.!', false);
    //            ShowList();
    //        }
    //        else {
    //            AddProcessErrorValue(true, 'Error has been occured , please contact administrator.!', false);
    //            ShowList();
    //        }
    //    }).error(function (data) {
    //        AddProcessErrorValue(true, 'Error has been occured , please contact administrator.!', false);
    //        ShowList();
    //    });

    //}
    //GetAllContacts();

    function GetContactById(Id) {
        debugger;
        resetprocess();
        StartLoading();

        var url = "/api/ContactAPI/GetById?Id=" + Id;
        LcService.getContactById(url).success(function (data, status, headers, config) {
            if (status == 200) {
                debugger;
                cc.Contact = data;
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

    function UpdateContact(contact) {
        debugger;
        resetprocess();
        StartLoading();

        if (!isNullOrEmpty(contact.customerId)) {
            AddProcessErrorValue(true, 'Please Select Customer.', false);
            return false;
        }
		
        if (!isNullOrEmpty(contact.name)) {
            AddProcessErrorValue(true, 'Please Enter Name.', false);
            return false;
        }

        if (!isNullOrEmpty(contact.emailId)) {
            AddProcessErrorValue(true, 'Please Enter Emai Id.', false);
            return false;
        }

        if (!isNullOrEmpty(contact.mobileNo)) {
            AddProcessErrorValue(true, 'Please Enter Mobile No.', false);
            return false;
        }

        if (!isNullOrEmpty(contact.website)) {
            AddProcessErrorValue(true, 'Please Enter website.', false);
            return false;
        }

        
        if (!isValidEmailAddress(contact.emailId)) {
            $scope.AddProcessErrorValue(true, 'Please Enter valid EmailAddress.', false);
            return false;
        }
        
        var url = "/api/ContactAPI/Update/";
        LcService.updateContact(url, contact).success(function (data, status, headers, config) {
            if (status == 200 && data == 1) {
                AddProcessSuccessValue(true, 'Contact Updated Successfully', false);
                GetAllContacts();
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

    function AddContact(contact) {
        resetprocess();
        StartLoading();

        if (!isNullOrEmpty(contact.customerId)) {
            AddProcessErrorValue(true, 'Please Customer.', false);
            return false;
        }

        if (!isNullOrEmpty(contact.name)) {
            AddProcessErrorValue(true, 'Please Enter Name.', false);
            return false;
        }

        if (!isNullOrEmpty(contact.emailId)) {
            AddProcessErrorValue(true, 'Please Enter Emai Id.', false);
            return false;
        }

        if (!isNullOrEmpty(contact.mobileNo)) {
            AddProcessErrorValue(true, 'Please Enter Mobile No.', false);
            return false;
        }

        if (!isNullOrEmpty(contact.website)) {
            AddProcessErrorValue(true, 'Please Enter website.', false);
            return false;
        }


        if (!isValidEmailAddress(contact.emailId)) {
            $scope.AddProcessErrorValue(true, 'Please Enter valid EmailAddress.', false);
            return false;
        }

        if (contact.contactId == undefined || contact.contactId == null || contact.contactId == 0)
            var url = "/api/ContactAPI/Add/";
        else
            var url = "/api/ContactAPI/Update/";

        LcService.addContact(url, contact).success(function (data, status, headers, config) {
            console.log(data, status, headers, config, 'data, status, headers, config');
            if (status == 200 && data == 1) {
                AddProcessSuccessValue(true, 'Contact Added Successfully', false);
                GetAllContacts();
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

    function DeleteContact(Id) {
        debugger;
        resetprocess();
        StartLoading();

        var url = "/api/ContactAPI/Delete?Id=" + Id;
        LcService.deleteContactById(url).success(function (data, status, headers, config) {
            if (status == 200) {
                GetAllContacts();
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
        if (cc.Contact.mobileNo == null || cc.Contact.mobileNo == undefined) {
            cc.Contact.mobileNo = "";
        }
        else {
            if (isNaN(cc.Contact.mobileNo) == true)
                cc.Contact.mobileNo = "";
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
    