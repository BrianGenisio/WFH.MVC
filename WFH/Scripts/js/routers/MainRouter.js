App.Routers.MainRouter = Backbone.Router.extend({
    routes: {
        "days": "days",
        "login": "login",
        "": "login"
    },

    login: function() {
        var loginView = new App.Views.Login();
        $(".appMain").html(loginView.render().el);
    },
    
    days: function () {
        var daysAtHome = new App.Collections.DaysAtHome();
        var daysView = new App.Views.DaysAtHome({model: daysAtHome});
        $(".appMain").html(daysView.render().el);
        daysAtHome.fetch();
    }

});