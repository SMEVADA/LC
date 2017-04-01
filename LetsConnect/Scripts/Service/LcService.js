var app = angular.module('LC', ['ui.bootstrap', 'ngResource']);
app.factory('LcService', LcService);
LcService.$inject = ['$resource', '$http'];

function LcService($resource, $http) {

    var LcService = this;

    /* Start Admin Module*/

    LcService.checkUserLogin = function (url) {
        return $http.get(url);
    }

    LcService.registerAdmin = function (url, administatorViewModel) {
        // return $http.post(url, data);
        //debugger;
        return $http({
             url: url,
            method: "POST",
            data: administatorViewModel,
            headers: { 'Content-Type': 'application/json' }
        })
    }

    LcService.getAllAdminList = function (url) {
        return $http.get(url);
    }

    LcService.getAdminById = function (url) {
        return $http.get(url);
    }

    LcService.deleteAdmin = function (url) {
        return $http.get(url);
    }

    /* End Admin Module*/


    /* Start Permission Module*/

    //get permission by admin id
    LcService.GetPermissionsByAdminId = function (url) {
        return $http.get(url);
    }

    LcService.UpdateMultiple = function (url,PermissionList) {
        return $http({
            url: url,
            method: "POST",
            data: PermissionList,
            headers: { 'Content-Type': 'application/json' }
        })
    }

    /* End Permission Module*/



    /* Start Customer Module*/

    LcService.getAllCustomerList = function (url) {
        return $http.get(url);
    }
	
	 LcService.getAllCustomerListByParameters = function (url) {
        return $http.get(url);
    }

    LcService.updateCustomer = function (url, customer) {
        return $http({
            url: url,
            method: "POST",
            data: customer,
            headers: { 'Content-Type': 'application/json' }
        })
    }

    LcService.getCustomerById = function (url) {
        return $http.get(url);
    }

    LcService.addCustomer = function (url, customer) {
        return $http({
            url: url,
            method: "POST",
            data: customer,
            headers: { 'Content-Type': 'application/json' }
        })
    }

    LcService.deleteCustomerById = function (url) {
        return $http.get(url);
    }

    /* End Customer Module*/

    /* Start Contact Module*/

    LcService.getAllContactList = function (url) {
        return $http.get(url);
    }

    LcService.updateContact = function (url, customer) {
        return $http({
            url: url,
            method: "POST",
            data: customer,
            headers: { 'Content-Type': 'application/json' }
        })
    }

    LcService.getContactById = function (url) {
        return $http.get(url);
    }

    LcService.addContact = function (url, customer) {
        return $http({
            url: url,
            method: "POST",
            data: customer,
            headers: { 'Content-Type': 'application/json' }
        })
    }

    LcService.deleteContactById = function (url) {
        return $http.get(url);
    }
	
	 LcService.getAllContactListByParameters = function (url) {
        return $http.get(url);
    }

    /* End Contact Module*/
	
	
	/* Start Menu Module*/

    LcService.getAllMenuList = function (url) {
        return $http.get(url);
    }
	
	LcService.getAllMenuListByUser = function (url) {
        return $http.get(url);
    }
	

    /* End Menu Module*/
	
	
	 /* Start Inquiry Module*/
	  LcService.getAllInquiryList = function (url) {
        return $http.get(url);
    }

    LcService.updateInquiry = function (url, customer) {
        return $http({
            url: url,
            method: "POST",
            data: customer,
            headers: { 'Content-Type': 'application/json' }
        })
    }

    LcService.getInquiryById = function (url) {
        return $http.get(url);
    }

    LcService.addInquiry = function (url, customer) {
       return $http.get(url);
    }

    LcService.deleteInquiryById = function (url) {
        return $http.get(url);
    }

	  /* End Inquiry Module*/

    
    return LcService;

};