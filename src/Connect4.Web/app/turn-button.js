import {inject, bindable, customElement} from 'aurelia-framework';

@customElement('turn-button')
@inject(Element)
export class TurnButton {
    @bindable column;
    constructor(element) {
        this.element = element;
    }

    bind(bindingContext, overrideContext) {
        this.parent = overrideContext.parentOverrideContext.bindingContext;
    }

    turn() {
        this.parent.attemptTurn(this.column);
    }
}
