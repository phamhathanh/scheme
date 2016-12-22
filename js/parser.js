"use strict";

var Atom = function (type, value) {
    this.type = type;
    this.value = value;
};

Atom.fromToken = function (token) {
    var isAValidNumber = !isNaN(parseFloat(token));
    if (isAValidNumber)
        return new Atom('literal', parseFloat(token));

    var isAValidString = token[0] === '"' && token.slice(-1) === '"';
    if (isAValidString)
        return new Atom('literal', token.slice(1, -1));
    
    var isValidIdentifier = true;
    // TODO: implement.
    if (isValidIdentifier)
        return new Atom('identifier', token);
        
    throw 'Parse Exception.';
};

Atom.prototype.toString = function () {
    if (typeof this.value === 'string')
        return '"' + this.value + '"';
    if (this.value === null)
        return '()';
    return this.value;
};

const NIL = new Atom('NIL', null);

var Pair = function (car, cdr) {
    this.car = car;
    this.cdr = cdr;
};

Pair.prototype.toString = function () {
    return '(' + this.car.toString() + ' . ' + this.cdr.toString() + ')';
};

function tokenize(input) {
    return input.replace(/\(/g, ' ( ')
        .replace(/\)/g, ' ) ')
        .trim()
        .split(/\s+/);
}

function read(tokens) {
    var token = tokens.shift();
    if (token === undefined)
        return NIL;
    if (token === '(') {
        var car = read(tokens);
        var cdr = read(tokens);
        return new Pair(car, cdr);
    }
    if (token === ')')
        return NIL;

    car = Atom.fromToken(token);
    cdr = read(tokens);
    return new Pair(car, cdr);
}