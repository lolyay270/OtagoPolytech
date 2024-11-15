// place files you want to import through the `$lib` alias in this folder.

let first_vari = "hello, it me, first variable, im a string, but you dont define me as string";
const CONSTANT = 400; //constants are constant, cannot be changed
var dontUseMe = "sometimes you find var online, you shouldnt use it cause its old";

console.log(first_vari); //shows in console, JS ver of print/writeline
first_vari = "im now overwritten, dont use let here"

{
    //different code block makes a different variable, different SCOPE
    let first_vari = "im a completely dif variable";
    console.log(first_vari); //will show new vari
}

console.log(first_vari); //will show overwritten vari

let a = "string here"; //interpreter understands it a string
let b = 10; //interpreter knows is int

let x = 10;
let y = "60"; 
console.log(x + y) //will result in "1060" cause it assumes string is never going to be int

console.log(x + parseInt(y)) //we can force it back to int


export {
    x, 
    y, 
    CONSTANT, 
    first_vari
};

//import {x, y, CONSTANT} from '$lib.....';     dont have to take all of them, only the ones you want