window.App = {
    Models: {},
    Collections: {},
    ViewModels: {},
    Routers: {}
};

$(function () {
    var todaysItems = new App.Collections.DaysAtHome();
    var vm = new App.ViewModels.DaysAtHomeVM(todaysItems);
    ko.applyBindings(vm, $('#body')[0]);
    todaysItems.fetch();
})