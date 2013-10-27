/// <reference path="//Microsoft.WinJS.1.0/js/base.js" />
/// <reference path="cucumber.js" />
/// <reference path="mushroom-mixin.js" />

WinJS.Namespace.define("VegetableGmos", {
    CucumberGmo: WinJS.Class.mix(Plants.Vegetables.Cucumber, Mixins.MushroomMixin)
});