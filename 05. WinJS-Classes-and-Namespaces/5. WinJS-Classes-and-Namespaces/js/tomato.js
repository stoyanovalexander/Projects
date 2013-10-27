/// <reference path="//Microsoft.WinJS.1.0/js/base.js" />
/// <reference path="vegetable.js" />

WinJS.Namespace.defineWithParent(Plants, "Vegetables", {
    Tomato: WinJS.Class.derive(Plants.Vegetable, function (radius) {
        Plants.Vegetable.call(this, "red", true, radius);
        this.plantName = "Tomato";
    })
});