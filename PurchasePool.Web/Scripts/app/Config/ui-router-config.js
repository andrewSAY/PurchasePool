ppApp.config(function ($stateProvider) {
    var states = [
        {
            name: 'products',
            url: '/products',
            abstract: true,
            template: '<ui-view/>'
        },
        {
            name: 'products.index',
            url: '/index',            
            templateUrl: 'Main/GetTemplate/Products',            
        },
        {
            name: 'products.edit',
            url: '/edit/{id}',
            templateUrl: '/Main/GetTemplate/ProductCard',
            controller: 'ProductEditController'
        },
        {
            name: 'products.new',
            url: '/new',
            templateUrl: '/Main/GetTemplate/ProductCard',
            controller: 'ProductCreateController'
        },
    ];
    for (var i in states){
        $stateProvider.state(states[i]);
    }
});