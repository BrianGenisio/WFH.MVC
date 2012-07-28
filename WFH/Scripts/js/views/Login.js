App.Views.Login = Backbone.View.extend({
    tagName: 'div',
    template: "<label>UserName: <input type='text' id='userName'></label><label>Password: <input type='password' id='password'></label><button id='submit'>Login</button>",
    
    events: { "click #submit": "login" },

    render: function () {
        this.$el.html(this.template);
        return this;
    },
    
    login: function () {
        var loginModel = new App.Models.Login({
            UserName: $("#userName", this.el).val(),
            Password: $("#password", this.el).val()
        });

        loginModel.save({}, {success: this.loginComplete, error: this.loginFailure});
    },
    
    loginComplete: function () {
        App.Routers.mainRouter.navigate("days", {trigger: true});
    },
    
    loginFailure: function () {
        alert("Bad username or password.  Please try again");
    }
    
});