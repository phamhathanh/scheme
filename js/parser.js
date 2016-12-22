"use strict";

function tokenize(input) {
    return input.replace(/\(/g, ' ( ')
        .replace(/\)/g, ' ) ')
        .trim()
        .split(/\s+/);
}

var Atom = function (type, value) {
    this.type = type;
    this.value = value;
};

Atom.fromToken = function (token) {
    var isAValidNumber = !isNaN(parseFloat(token));
    if (isAValidNumber)
        return new Atom('number', parseFloat(token));

    var isAValidString = token[0] === '"' && token.slice(-1) === '"';
    if (isAValidString)
        return new Atom('string', token.slice(1, -1));
    
    var isValidIdentifier = true;
    // TODO: implement.
    if (isValidIdentifier)
        return new Atom('identifier', token);
        
    throw 'Parse Exception.';
};

Atom.prototype.toString = function () {
    if (this.type === 'string')
        return '"' + this.value + '"';
    return this.value;
};

const NIL = new Atom('NIL', 'NIL');

var Pair = function (car, cdr) {
    this.car = car;
    this.cdr = cdr;
};

Pair.prototype.toString = function () {
    return '(' + this.car.toString() + ' . ' + this.cdr.toString() + ')';
};

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

var tokens = tokenize('((1 "2") 3 4)');
console.log(tokens);
var structure = read(tokens);
console.log(structure.toString());