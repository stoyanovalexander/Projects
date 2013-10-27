/// <reference path="//Microsoft.WinJS.1.0/js/base.js" />

WinJS.Namespace.define("Mixins", {
    MushroomMixin: {
        grow: function (waterLiters) {
            this.size += waterLiters;
        }
    }
});