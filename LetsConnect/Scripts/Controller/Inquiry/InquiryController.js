app.controller('InquiryController', InquiryController);
InquiryController.$inject = ['$scope', 'LcService'];
function InquiryController($scope,  LcService) {
    var ic =this;

    ic.SumbitInquiry = SumbitInquiry;
    ic.BackToList=BackToList;
    ic.ShowInquiry=ShowInquiry;
    ic.GetAllInquiry = GetAllInquiry;
    ic.OnlyNumber = OnlyNumber;

    ic.InquiryList = [];
    ic.InquiryDetail = {};
    ic.IsUserLogin = false;
    ic.InquiryList = true;
    ic.Inquiry = false;
    ic.Isloading = false;
    ic.Person = {};

       function OnlyNumber () {
        if(ic.Person.mobile ==null || ic.Person.mobile == undefined)
        {
            ic.Person.mobile = "";
        }
        else
        {
            if (isNaN(ic.Person.mobile) == true)
                ic.Person.mobile = "";
        }
    }

    function SumbitInquiry(inquiryModel) {

        if (!isNullOrEmpty(inquiryModel.Name)) {
            AddInquiryErrorValue(true, 'Please Enter Name.', false);
            return false;
        }

        if (!isNullOrEmpty(inquiryModel.Email)) {
            AddInquiryErrorValue(true, 'Please Enter Emai Id.', false);
            return false;
        }
        if (!isNullOrEmpty(inquiryModel.mobile)) {
            AddInquiryErrorValue(true, 'Please Enter Mobile No.', false);
            return false;
        }
        if (!isNullOrEmpty(inquiryModel.Address)) {
            AddInquiryErrorValue(true, 'Please Enter Address.', false);
            return false;
        }
        if (!isNullOrEmpty(inquiryModel.Message)) {
            AddInquiryErrorValue(true, 'Please Enter Message.', false);
            return false;
        }

        if (!isValidEmailAddress(inquiryModel.Email)) {
            AddInquiryErrorValue(true, 'Please Enter valid EmailAddress.', false);
            return false;
        }

        resetprocess();
        StartLoading();

        var url = "/api/InquiryAPI/AddInquiry?Name="+inquiryModel.Name+"&mobile="+inquiryModel.mobile+"&Message="+inquiryModel.Message+"&Email="+inquiryModel.Email+"&Address="+inquiryModel.Address;
        LcService.addInquiry(url).success(function (data, status, headers, config) {
                console.log(data, status, headers, config);
                if (status == "200") {
                    AddInquirySuccessValue(true, 'Inquiry Submitted successfully', false);
                    ic.Person = [];
					 StopLoading();
                ShowList();
                }
                else
                {
                    AddInquiryErrorValue(true, 'Error has been occured , please contact administrator.!', false);
                    ShowList();
                }
               
            })
            .error(function (data, status, header, config) {
                AddInquiryErrorValue(true, 'Error has been occured , please contact administrator.!', false);
                ShowList();
            });

    }

    function BackToList() {
        ic.GetAllInquiry();
        ic.InquiryDetail = {};
        ic.InquiryList = true;
        ic.Inquiry = false;
        ic.Isloading = false;
    }
    function ShowInquiry(InquiryID) {
		 resetprocess();
        StartLoading();
		
        ic.InquiryList = false;
        ic.Inquiry = false;
        ic.Isloading = true;

		 var url = "/api/InquiryAPI/GetInquiryByInquityId?inquiryID=" + InquiryID;
        LcService.getInquiryById(url).success(function (data, status, headers, config) {
               debugger;
               ic.InquiryDetail = data;
               ic.InquiryList = false;
               ic.Inquiry = true;
               ic.Isloading = false;
               StopLoading();
               ShowList();
           })
           .error(function (data, status, header, config) {
               AddInquiryErrorValue(true, 'Error has been occured , please contact administrator.!', false);
               ShowList();
           });
    }

    function GetAllInquiry() {
        //validation
		 resetprocess();
        StartLoading();
		
		  var url = "/api/InquiryAPI/GetAllInquiry";
        LcService.getAllInquiryList(url).success(function (data, status, headers, config) {
                        ic.IsUserLogin = true;
            ic.InquiryList = data.Inquiry;
            ic.totalCount = ic.InquiryList.length;
            ic.readdatacount = data.readdatacount;
            ic.unreaddatacount = data.unreaddatacount;
             StopLoading();
                ShowList();
            setTimeout(function () {
                $scope.$apply();
            }, 100);
        })
        .error(function (data, status, header, config) {
            AddInquiryErrorValue(true, 'Error has been occured , please contact administrator.!', false);
            ShowList();
        });

    }
    GetAllInquiry();
};