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
}

Atom.prototype.toString = function () {
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

function read(input) {
    var token = input.shift();
    if (token === undefined)
        return NIL;
    if (token === '(') {
        var car = read(input);
        var cdr = read(input);
        return new Pair(car, cdr);
    }
    if (token === ')')
        return NIL;

    car = new Atom('atom', token);
    cdr = read(input);
    return new Pair(car, cdr);
}

var tokens = tokenize('((1 "2") 3 4)');
console.log(tokens);
var structure = read(tokens);
console.log(structure.toString());