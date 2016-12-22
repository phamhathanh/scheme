"use strict";

class Atom {
    constructor(value) {
        this.value = value;
    }

    static fromToken(token) {
        const isAValidNumber = !isNaN(parseFloat(token));
        if (isAValidNumber)
            return new Literal(parseFloat(token));

        const isAValidString = token[0] === '"' && token.slice(-1) === '"';
        if (isAValidString)
            return new Literal(token.slice(1, -1));
        
        const isValidIdentifier = true;
        // TODO: validate.
        if (isValidIdentifier)
            return Identifier.fromString(token);
            
        throw 'Parse Exception.';
    }
    
    toString() {
        return this.value;
    }
}

class Identifier extends Atom {
    static dictionary = {};
    static fromString(key) {
        const created = key in Identifier.dictionary;
        if (!created)
            dictionary[key] = new Identifier(Symbol(key));
        return dictionary[key];
    }
}

class Literal extends Atom {
    toString() {
        if (typeof this.value === 'string')
            return '"' + this.value + '"';
        if (this.value === null)
            return '()';
        return this.value;
    }

}

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