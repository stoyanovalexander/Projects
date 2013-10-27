/// <reference path="//Microsoft.WinJS.1.0/js/base.js" />
/// <reference path="vegetable.js" />
/// <reference path="tomato.js" />
/// <reference path="cucumber.js" />
/// <reference path="mushroom-mixin.js" />
/// <reference path="tomato-gmo.js" />
/// <reference path="cucumber-gmo.js" />
/// <reference path="dom-logger.js" />

(function () {
    WinJS.Application.onactivated = function () {
        var tomato = new Plants.Vegetables.Tomato(10);
        var cucumber = new Plants.Vegetables.Cucumber(15);

        console = new DomLogger(document.getElementById("output"));

        console.log("---->Tomato:")
        tomato.info();

        console.log("---->Cucumber:");
        cucumber.info();

        var tomatoGmo = new VegetableGmos.TomatoGmo(10);
        var cucumberGmo = new VegetableGmos.CucumberGmo(15);

        tomatoGmo.grow(5);
        cucumberGmo.grow(5);

        console.log("---->Tomato GMO:")
        tomatoGmo.info();

        console.log("---->Cucumber GMO:");
        cucumberGmo.info();
    };

    WinJS.Application.start();
})();