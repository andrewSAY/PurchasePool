function ProductCreateController($scope, $state, productService) {
    $scope.Caption = 'The form of creating a new product';
    $scope.Product = {
        Id: null,
        Name: null,
        Description: null,
        WebLink: null,
        Categories: []
    };
    ProductCommon.call(this, $scope, $state, productService);
}

ppApp.controller('ProductCreateController', ['$scope', '$state', 'ProductService', ProductCreateController]);