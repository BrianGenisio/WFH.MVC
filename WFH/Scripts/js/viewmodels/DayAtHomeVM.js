App.ViewModels.DayAtHomeVM = kb.ViewModel.extend({
   
    deleteEntry: function () {
        this.model().destroy();
    }
});