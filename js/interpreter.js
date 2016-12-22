"use strict";

var Region = function (bindings, parent) {
    this.bindings = bindings;
    this.parent = parent;

    this.get = function (identifier) {
        if (identifier in this.bindings)
            return this.bindings[identifier];
        if (this.parent !== undefined)
            return this.parent.get(identifier);
        throw 'Unbounded identifier.';
    };
};

var standardLibrary = {

};

function interpret(data, region) {
    if (region === undefined)
        // Global environment.
        interpret(data, new Region(standardLibrary));

    
}

function evaluate(expression, region) {
    var keyword = expression.car;
    if (keyword.type === 'NIL')
        throw 'Syntax error.';
    if (keyword.type === 'literal')
        throw 'Syntax error.';
    if (keyword.type === 'identifier') {
        var object = region.bindings.get(keyword);
    }
    throw 'Not implemented.';
}