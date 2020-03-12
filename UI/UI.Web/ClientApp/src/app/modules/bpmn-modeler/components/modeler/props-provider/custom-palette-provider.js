"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var CustomPaletteProvider = /** @class */ (function () {
    // Note that names of arguments must match injected modules, see InjectionNames.
    // I don't know why originalPaletteProvider matters but it breaks if it isn't there.
    // I guess since this component is injected, and it requires an instance of originalPaletteProvider,
    // originalPaletteProvider will be new'ed and thus call palette.registerProvider for itself.
    // There probably is a better way.
    function CustomPaletteProvider(palette, originalPaletteProvider, elementFactory) {
        this.palette = palette;
        this.originalPaletteProvider = originalPaletteProvider;
        // console.log(this.constructor.name, "constructing", palette, originalPaletteProvider);
        palette.registerProvider(this);
        this.elementFactory = elementFactory;
    }
    CustomPaletteProvider.prototype.getPaletteEntries = function () {
        var _this = this;
        // console.log(this.constructor.name, "getPaletteEntries", this.palette, this.originalPaletteProvider);
        return {
            save: {
                group: 'tools',
                className: ['fa-save', 'fa'],
                title: 'TEST',
                action: {
                    click: function () { return console.log('TEST Action clicked! Elementfactory: ', _this.elementFactory); }
                }
            }
        };
    };
    CustomPaletteProvider.$inject = ['palette', 'originalPaletteProvider', 'elementFactory'];
    return CustomPaletteProvider;
}());
exports.CustomPaletteProvider = CustomPaletteProvider;
//# sourceMappingURL=CustomPaletteProvider.js.map