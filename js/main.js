"use strict";

var tokens = tokenize('((1 "2") 3 4)');
console.log(tokens);
var data = read(tokens);
console.log(data);
console.log(data.toString());