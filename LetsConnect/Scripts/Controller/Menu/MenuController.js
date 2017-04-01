
app.controller('MenuController', MenuController);
//MenuController.$inject = ['LcService'];
MenuController.$inject = ['$http'];

function MenuController($http) {

    var mu = this;
    mu.MenuList = [];
    mu.UserMenuList = [];
    mu.Menu = {};
    mu.GetAllMenu = GetAllMenu;
    mu.GetAdminMenu = GetAdminMenu;

    function GetAdminMenu() {
        var url = "/api/MenuAPI/GetAllByUserId/";

        $http.get(url).success(function (data, status, headers, config) {
            if (status == 200) {
                mu.UserMenuList = data;
                console.log(mu.UserMenuList,' mu.UserMenuList');
            }
        });
    };
    GetAdminMenu();

    function GetAllMenu() {
        var url = "/api/MenuAPI/GetAll/";
        $http.get(url).success(function (data, status, headers, config) {
            if (status == 200) {
                mu.MenuList = data;
            }
        });
    };
    GetAllMenu();
}

   
    

