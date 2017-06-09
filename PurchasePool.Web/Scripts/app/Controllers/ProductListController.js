function ProductListController($scope, $state, productService) {
    $scope.Products = [];

    productService.GetProducts().then(function (data) {
        $scope.Products = data;
    });

    $scope.GetCategories = function (product) {
        var stringCategories = product.Categories
            .map(function (item) { return item.Name })
            .join(', ');       
        return stringCategories;
    }

    $scope.Edit = function(product){
        $state.go('products.edit', { id: product.Id });
    }

    $scope.New = function () {
        $state.gp('products.new');
    }
}

ppApp.controller('ProductListController', ['$scope', '$state', 'ProductService', ProductListController]);