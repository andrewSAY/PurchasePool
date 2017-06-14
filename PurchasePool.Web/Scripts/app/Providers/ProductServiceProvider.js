function ProductServiceProvider() {
    function ProductServiceProviderFactory($http) {

        this.GetProducts = function () {
            return $http.get("/Api/Product/GetAll").then(function (response) {
                return response.data;
            });
        }

        this.GetCategories= function () {
            return $http.get("/Api/Product/GetCategories").then(function (response) {
                return response.data;
            });
        }

        this.GetProduct = function (id) {
            return $http.get("/Api/Product/GetProduct/" + id).then(function (response) {
                return response.data;
            });
        }

        this.SaveProduct = function (product) {
            return $http.post("/Api/Product/PostProduct", product).then(function (response) {
                return response.data;
            });
        }
    }

    this.$get = ['$http', function($http) {
        return new ProductServiceProviderFactory($http);
    }];
}

ppApp.provider('ProductService', ProductServiceProvider);