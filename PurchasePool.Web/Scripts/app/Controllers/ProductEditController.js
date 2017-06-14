function ProductEditController($scope, $state, productService) {
    $scope.Caption = 'The form of edit the product';
    var id = $state.params.id;
    $scope.SavingError = null;
    var self = this;
    productService.GetProduct(id).then(function (data) {
        $scope.Product = data;
        ProductCommon.call(self, $scope, $state, productService);
    })
}

ppApp.controller('ProductEditController', ['$scope', '$state', 'ProductService', ProductEditController]);