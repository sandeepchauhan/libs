///<reference path="typings/knockout/knockout.d.ts" />

var model = {
    firstName: ko.observable("sandeep"),
    lastName: ko.observable("chauhan"),
    changeName: function () {
        this.firstName("firstName");
    }
};

var context = document.getElementById("context");
ko.applyBindings(model, context);