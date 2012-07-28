App.ViewModels.DaysAtHomeVM = kb.ViewModel.extend({
    constructor: function (model) {
        kb.ViewModel.__super__.constructor.apply(this, arguments);

        this.todaysEntries = kb.collectionObservable(model, { view_model: App.ViewModels.DayAtHomeVM });
        this.newNote = ko.observable();
    },
    
    createNew: function () {
        this.todaysEntries.collection().create({ note: this.newNote() }, {wait: true});
        this.newNote("");
    }
});