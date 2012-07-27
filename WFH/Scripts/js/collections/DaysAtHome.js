App.Collections.DaysAtHome = Backbone.Collection.extend({
    model: App.Models.DayAtHome,
    url: "/api/DaysAtHome"
});