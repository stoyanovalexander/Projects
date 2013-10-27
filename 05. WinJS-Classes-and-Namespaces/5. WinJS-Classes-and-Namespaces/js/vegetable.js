/// <reference path="//Microsoft.WinJS.1.0/js/base.js" />

WinJS.Namespace.define("Plants", {
    Vegetable: WinJS.Class.define(function (color, canBeEaten, size) {
        this.color = color;
        this.canBeEaten = canBeEaten;
        this._size = size;
        this.plantName;
    }, {
        size: {
            get: function () { return this._size; },
            set: function (val) { this._size = val; }
        },
        info: function () {
            console.log("Plant: " + this.plantName);
            console.log("Color: " + this.color);
            console.log("Can be eaten directly? -> " + this.canBeEaten);
            console.log("Size: " + this.size);
        }
    }, {

    })
});