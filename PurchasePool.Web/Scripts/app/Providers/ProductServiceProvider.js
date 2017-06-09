function ProductServiceProvider() {
    function ProductServiceProviderFactory($http) {

        this.GetProducts = function () {
            return $http.get("/Api/Product/GetAll").then(function (response) {
                return response.data;
            });
        }
    }

    this.$get = ['$http', function($http) {
        return new ProductServiceProviderFactory($http);
    }];
}

ppApp.provider('ProductService', ProductServiceProvider);