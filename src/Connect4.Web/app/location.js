import {inject, bindable, customElement} from 'aurelia-framework';

@customElement('location')
@inject(Element)
export class Location {
    @bindable location;
    @bindable isYellow;
    @bindable isRed;
    @bindable isWin;
    @bindable css;
    constructor(element) {
        this.element = element;
    }

    bind(bindingContext, overrideContext) {
       this.evaluateCss();
    }
    
    isYellowChanged(newValue, oldValue) {
        this.evaluateCss();
    }

    isRedChanged(newValue, oldValue) {
        this.evaluateCss();
    }

    isWinChanged(newValue, oldValue) {
        this.evaluateCss();
    }

    evaluateCss() {
        if (this.isYellow === true) {
            this.css = "location-yellow";
        }  else if (this.isRed === true) {
            this.css = "location-red";
        } else {
            this.css = "location-empty";
        }
    }
}
