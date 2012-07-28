App.Views.DaysAtHome = Backbone.View.extend({
   
    initialize: function () {
        _.bindAll(this, "renderItem", "render");
        this.model.bind("reset", this.render);
        this.model.bind("add", this.renderItem);
    },

    render: function () {
        this.$el.html("<h2>Hello Days</h2><ul class='daysList'></ul>");
        this.model.each(this.renderItem);
        return this;
    },
    
    renderItem: function (item) {
        $(".daysList", this.el).append("<li>" + item.get("name") + "</li>");
    }

});