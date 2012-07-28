window.App = {
    Models: {},
    Collections: {},
    Templates: {},
    Views: {},
    Routers: {}
};

jQuery(function ($) {
    App.Routers.mainRouter = new App.Routers.MainRouter();
    Backbone.history.start();
});