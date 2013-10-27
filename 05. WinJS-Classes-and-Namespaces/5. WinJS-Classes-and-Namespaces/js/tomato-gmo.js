/// <reference path="//Microsoft.WinJS.1.0/js/base.js" />
/// <reference path="tomato.js" />
/// <reference path="mushroom-mixin.js" />
WinJS.Namespace.define("VegetableGmos", {
    TomatoGmo: WinJS.Class.mix(Plants.Vegetables.Tomato, Mixins.MushroomMixin)
});