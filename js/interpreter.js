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

var Function = function (func) {
    this.func = func;
};

var standardLibrary = {

};

function interpret(data) {
    var globalEnvironment = new Region(standardLibrary);
    if (typeof data === 'Pair')
        evaluate(data, globalEnvironment);
}

function evaluate(expression, region) {
    var keyword = expression.car;
    if (typeof keyword === 'Pair') {
        var evaluatedKeyword = evaluate(keyword, region);
        evaluate(evaluatedKeyword, region);
        return;
    }
    if (keyword.type === 'NIL')
        throw 'Syntax error.';
    if (keyword.type === 'literal')
        throw 'Syntax error.';
    if (keyword.type === 'identifier') {
        var object = region.bindings.get(keyword);
        if (typeof object !== 'Function')
            throw 'Syntax error.';
        console.log('lol');
        return;
    }
    throw 'Not implemented.';
}