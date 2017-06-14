function ProductCommon($scope, $state, productService)
{
    $scope.Categories = [];
    $scope.SavingError = null;
    $scope.SelectedCategoriesCount = 0;
    productService.GetCategories().then(function (data) {
        $scope.Categories = data;
        for (var i in $scope.Categories) {
            var index = $scope.Product.Categories.findIndex(function (el) {
                return el.Id === data[i].Id;
            });
            data[i].IsChecked = index !== -1;
        }
        $scope.SelectedCategoriesCount = $scope.Product.Categories.length;
    });

    $scope.Cancel = function () {
        $state.go('products.index');
    };

    $scope.Save = function () {
        productService.SaveProduct($scope.Product).then(function () {
            $scope.SavingError = null;
            $state.go('products.index');
        }, function (error) {
            $scope.SavingError = error.data.ExceptionMessage;
        })
    }

    $scope.CategorySelectedChange = function (category) {
        if (category.IsChecked) {
            var index = $scope.Product.Categories.findIndex(function (el) {
                return el.Id === category.Id;
            });
            $scope.Product.Categories.splice(index, 1);
        }
        else {
            var length = $scope.Product.Categories.length - 1;
            length = length !== -1 ? length : 0;
            $scope.Product.Categories.splice(length, 0, category);
        }
        category.IsChecked = !category.IsChecked;
        $scope.SelectedCategoriesCount = $scope.Product.Categories.length;
    }
}