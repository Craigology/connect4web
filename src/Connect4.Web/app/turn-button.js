import {inject, bindable, customElement} from 'aurelia-framework';

@customElement('turn-button')
@inject(Element)
export class TurnButton {
    @bindable column;
    @bindable css;
    constructor(element) {
        this.element = element;
    }

    bind(bindingContext, overrideContext) {
        this.parent = overrideContext.parentOverrideContext.bindingContext;
    }

    turn() {
        this.element.blur();
        this.parent.attemptTurn(this.column);
    }
}
