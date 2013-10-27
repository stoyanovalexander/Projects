/// <reference path="//Microsoft.WinJS.1.0/js/base.js" />
/// <reference path="vegetable.js" />

WinJS.Namespace.defineWithParent(Plants, "Vegetables", {
    Cucumber: WinJS.Class.derive(Plants.Vegetable, function (length) {
        Plants.Vegetable.call(this, "green", false, length);
        this.plantName = "Cucumber";
    })
});