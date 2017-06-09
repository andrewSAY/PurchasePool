function AppController($scope, $state) {
    $state.go('products.index');
}

ppApp.controller('AppController', ['$scope', '$state', AppController]);