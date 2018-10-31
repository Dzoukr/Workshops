- title : Introduction to F#
- description : WUG 2018 Workshop
- author : Roman Provaznik
- theme : night
- transition : none

****************************************************************************

# Introduction to F#

<br/><br/><br/><br/><br/>
### Roman Provazník

[@rprovaznik](https://twitter.com/rprovaznik) | [@fsharping](https://twitter.com/fsharping) | [fsharping.com](https://fsharping.com)


****************************************************************************

## Agenda for today

### Part one - **Introduction to F#**

Basic syntax

Basic types

Pattern matching

Currying

Application & Composition

Custom operators

Code organization

****************************************************************************

## Agenda for today

### Part two - **REST API with F#**

Giraffe fundamentals

Routing & Filters

Working with Request

Working with Response

****************************************************************************

## Download

[https://github.com/Dzoukr/Workshops/tree/master/WUG2018/webapp](https://github.com/Dzoukr/Workshops/tree/master/WUG2018/webapp)

****************************************************************************

## And now some **F# fun!**

****************************************************************************

## F#

<table><tr><td class="table-leftcol">

**Multi**-paradigm

**Strongly** typed

**.NET** ** language

</td><td class="table-rightcol">
<img src="images/fsharp-logo.png" />
</td></tr></table>

****************************************************************************

## F#

<table><tr><td class="table-leftcol">

Version 1.0 in **May 2005**

Designed by **Don Syme**

Microsoft Research

</td><td class="table-rightcol">
<img src="images/donsyme.jpg" style="width:400px" />
</td></tr></table>

****************************************************************************

## Basic syntax

<table><tr><td class="table-leftcol">

Based on **OCAML**

(Almost) **no** brackets

**No** semicolons

</td><td class="table-rightcol">
<img src="images/ocaml.png" style="width:300px;" />
</td></tr></table>

----------------------------------------------------------------------------

## Basic syntax

```fsharp
let add x y = x + y
```

or

```fsharp
let add x y = 
    x + y
```

You can try it in F# **REPL**

----------------------------------------------------------------------------

## REPL

Read -> Evaluate -> Print -> Loop

It is **much easier** to write

1. Highlight your code
2. Press `Alt+Enter`
3. See the result

----------------------------------------------------------------------------

## Did you notice?

```fsharp
let tenTimes x = x * 10
```

**Type inference** works for you

Still **strongly-typed**


****************************************************************************

## Basic types

Functions (named + anonymous)

Records

Lists

Discriminated Unions

Tuples

Options

----------------------------------------------------------------------------

## Functions

**Named** functions

```fsharp
let sayHello() = printf "Hello"
```

or anonymous functions (**Lambdas**)

```fsharp
fun () -> printf "Hello"
```

----------------------------------------------------------------------------

## Help type inference

You can define exactly type of input parameters

```fsharp
let joinTwoStrings (txt1:string) (txt2:string) = txt1 + txt2
```

----------------------------------------------------------------------------

## Functions in F#

Basic **building brick** for any F# code

You can **compose** them

You can **return** them from any function 

You can use them as **input** value 

**First-class** citizen

----------------------------------------------------------------------------

## Keep in mind! 

Functions must **always return** something

All branches of functions must return **same type**

F# allows impure functions, but try to keep them **pure**

Signature is written as: **InputType** -> **OutputType**


****************************************************************************

## Records

Immutable data structure designed for **storing data**

Syntax similar to **JSON**

Equality by design (value of some type filled with same values **equals** to different value of same type with same data)

----------------------------------------------------------------------------

## Records

```fsharp
type Person = {
    Name : string
    Age : int
}

let person1 = { Name = "Roman"; Age = 36 }
let person2 = { Name = "Roman"; Age = 36 }
person1 = person2 // equals true
let ageOfPerson1 = person1.Age
```

----------------------------------------------------------------------------

## Records

Immutable = **cannot be changed**

```fsharp
let person1 = { Name = "Roman"; Age = 36 }
person1.Age <- 40 // NOPE
```

...but you can create **new value** based on existing one

```fsharp
let person2 = { person1 with Age = 40 }
```

----------------------------------------------------------------------------

## Keep in mind! 

Records are **immutable**

Records of same **type** with same **data** are **equal**

Single values of records are available by **dot** after value name <br/>(e.g. person**.FirstName**)

****************************************************************************

## Lists

Immutable data type for holding **1..N values** of same types

```fsharp
let listOfInts = [1;3;5;8]
let listOfStrings = ["hello";"world"]
let listOfFuncs = [add;multiply;divide]
```

Note: There are more types of collections in F#

----------------------------------------------------------------------------

## Lists

There are many ways how to create list

```fsharp
let oneToTen = [1..10]
let oneToHundredByTen = [1..10..100]
let joinedList = oneToTen @ oneToHundredByTen
let joinedListWithOne = 1 :: joinedList
```

----------------------------------------------------------------------------

## Lists

Try **List module** to find more functions to work with lists

```fsharp
List.filter (fun x -> x > 5) [1..10]
List.max [10;5;4;12;5]
List.sort [5;6;4;1;45]
```

----------------------------------------------------------------------------

## Keep in mind! 

Lists are **immutable**

**Prepend** is always **better** (faster) than append

All values in list must be of **same type**

Check **List module** for helper functions

****************************************************************************

## Discriminated Unions

When you want to defined some type as **one of many possible cases**, but each case can have **different value**

```fsharp
type Payment =
    | Cash of amount:int
    | Card of cardNumber:int64 * amount:int
    | Barter of goodsDescription:string
```
Similar to **Enumerations**, but much more powerful

----------------------------------------------------------------------------

## Discriminated Unions

```fsharp

// tennis game designed on 7 rows
type Points = Zero | Fifteen | Thirty | Fourty
type Player = A | B
type Game =
    | Score of Points * Points
    | Advantage of Player
    | Victory of Player
```

Often used to describe **business logic**

----------------------------------------------------------------------------

## Keep in mind! 

Discriminated Unions **don't allow** to create case with **wrong values** (make your code 100% safe at **compile** time)

Powerful way to describe **application state**

Combined with **pattern matching** one of the best things on F#

****************************************************************************

## Tuples

Data structure for holding **two or more values** of **various types**

```fsharp
let nameAndAge = "Roman",36
let twoStrings = "Hello","World"
let threePersons = person1,person2,person3
```

Signature for tuple of 2 values (most common): **Type1 * Type2**

----------------------------------------------------------------------------

## Tuples

You can do **destructuring** to "take out" the values

```fsharp
// destructuring
let name,age = nameAndAge // take-out values
let name,_ = nameAndAge // don't care about age

// or use pre-defined functions
let name = fst nameAndAge
let age = snd nameAndAge
```

Or use pre-defined functions **fst** & **snd**

----------------------------------------------------------------------------

## Keep in mind! 

Tuples are great for **intermediate** output values (internal functions)

Pre-defined functions are available **only for tuples with two values**

If you need more than two, **use records instead**

****************************************************************************

## Options

Instead of using **NULL** (or undefined), F# has **Option type**

```fsharp
type Person2 = {
    Name : string
    Age : int option
}

let personWithoutAge = { Name = "Roman"; Age = None }
let personWithAge = { Name = "Roman"; Age = Some 36 }
```

**Safe** way to define some values are **not always available**

----------------------------------------------------------------------------

## Options

Great example of power of **Discriminated Unions**

```fsharp
type Option<'a> =
    | Some of value:'a
    | None
```

----------------------------------------------------------------------------

## Keep in mind! 

F# does not allow **NULL** values (until you say explicitly you want to use them)

Use of Option type gives you **100% safety** in compile time

**No more null-checks** on each function/method

Again, combined with **pattern matching** one of the most powerful things in F#

****************************************************************************

## Patter matching

It is something like **switch** on heavy drugs

```fsharp
let optionalGreetings = Some "Hello world"
let display (value:string option) =
    match value with
    | Some v -> "We found value : " + v
    | None -> "Nothing to print"
```

Note the **match ... with** syntax

----------------------------------------------------------------------------

## Patter matching

Works with tuples

```fsharp
let nameAndAge = "Roman",36

let sayHelloByAge value =
    match value with
    | "Roman", 36 -> "Hi you are 36 years old Roman"
    | "Roman",_ -> "Hi any Roman"
    | name, 50 -> "You are fifty now and your name is: " + name
    | _, age when age > 99 -> "You look old"
    | _ -> "This is some fallback message :)" 
    
```

----------------------------------------------------------------------------

## Patter matching

Works with records

```fsharp
let person = { Name = "Roman"; Age = Some 36 }

let sayHelloByAge value =
    match value with
    | { Name = "Roman"; Age = Some 36 } -> "Hi you are 36 years old Roman"
    | { Name = "Roman" } -> "Hi any Roman"
    | { Name = name; Age = Some 50 } -> "You are fifty now and your name is: " + name
    | { Age = Some age } when age > 99 -> "You look old"
    | _ -> "This is some fallback message :)" 
    
```
----------------------------------------------------------------------------

## Patter matching

Works with lists

```fsharp
let numbers = [1;2;4;8;10]

let isListEmpty list =
    match list with
    | [] -> true
    | _ -> false
```

Works with most of F# types

----------------------------------------------------------------------------

## Keep in mind! 

All cases must return **same type of value**

Uncovered case can cause **exception at runtime** (but you got warning during compilation)

**Order of cases** matters

**Underscore (_)** is used as **safe default**

****************************************************************************

## Currying & Partial application

One of few **gotchas** in F#

```fsharp
let add x y = x + y
let add10 = add 10 // this is not error, but new function
```

Why does it work like this?

----------------------------------------------------------------------------

## Currying

Each function with **more than one** parameter is converted to **chain of functions** with **just one** parameter

```fsharp
let add x y = x + y

let addCurried x =
    let subAdd y =
        x + y
    subAdd

let lambdaAdd = fun x -> fun y -> x + y
```

All three functions are the same : **int -> int -> int**

----------------------------------------------------------------------------

## Partial application

When you **"bake in" parameters** in function to get** new function with fewer parameters**

```fsharp
let add x y = x + y
let add10 = add 10 // now you have func with one param
let add90 = add 90 // now you have func with one param
```

You can try it in **REPL**

----------------------------------------------------------------------------

## What is it good for?

----------------------------------------------------------------------------

## Piping & Composition

Thanks to currying & partial application you can **compose** functions like **LEGO**

```fsharp
let add x y = x + y
let add10 = add 10
let add90 = add 90

let add100 = add10 >> add90
let add360 = add90 >> add90 >> add90 >> add90
```

Use operator **>>** to glue together functions

----------------------------------------------------------------------------

## Piping & Composition

You can **compose** any functions if they obey simple **rule**:

**Output type of Func1 must be the same as input type of Fun2**

```fsharp
let toUpper (str:string) = str.ToUpper()
let addHello (str:string) = "Hello " + str

let composed = toUpper >> addHello
```

----------------------------------------------------------------------------

## Piping & Composition

You can **pipe forward** result of **left function** as **last parameter** of function **on right side** 
using **|>** operator

```fsharp
let result = add 10 90 // you got 100
let result2 = 90 |> add 10

```

What is it **good** for?

----------------------------------------------------------------------------

## Piping & Composition

Look at the difference:

```fsharp
[1..100]
|> List.filter (fun x -> x > 50)
|> List.map (fun x -> x * 2)
|> List.filter (fun x -> x%2 = 0)
|> List.rev

```

vs

```fsharp
List.rev 
    (List.filter (fun x -> x%2 = 0) 
        (List.map (fun x -> x * 2)
            (List.filter (fun x -> x > 50) [1..100])))
```

Piping allows to **read code as normal text** - from left to right, from top to bottom

****************************************************************************

## Keep in mind! 

In F# functions are **automatically curried**

Providing fewer arguments is **not an error** - you get **partial applied function**

Functions can be composed together using **>>** operator

Functions can be piped using **|>** operator

Piping allows to **read code as normal text** 

****************************************************************************

## Who wants **coffee**?

****************************************************************************

## Custom operators

You can create **custom operator** as shortcut for function

```fsharp
// custom operator
let (===) str regex = 
	System.Text.RegularExpressions.Regex.Match(str, regex).Success

"abcd" === "bc"
"abcd" === "de"
```

Note the **infix** notation (first param before operator)

----------------------------------------------------------------------------

## Keep in mind! 

Defined as normal function, but with parens **([MY\_OPERATORS\_HERE])**

Custom operators can make your codebase **nicer**

Custom operators can make your codebase **uglier**

**Think twice** (or more) before using them

You will see few of them in **Giraffe** library

****************************************************************************

## Code organization & Hints

Two types of F# code file extension: **.fsx** and **.fs**

.fsx files are for **quick scripting** (like in REPL)

.fs files are meant to be **part of project**

**Hint:** Play with fsx first and then move code to fs files

----------------------------------------------------------------------------

## Code organization & Hints

Code can be organized in **modules** or **namespaces**

Namespaces cannot contain values, only types

Modules can contain both

**Hint:** If you are not sure, just **use module** (you can change it later)

****************************************************************************

## Before we go build some **web** application...

****************************************************************************

## Giraffe

<table><tr><td class="table-leftcol">

F# library for **web development**

Built on top of **ASP.NET**

**Functional** API

Uses Task **computation expression**

</td><td class="table-rightcol">
<img style="background-color: white;" src="images/giraffe.png" />
</td></tr></table>

****************************************************************************

## Computation expressions

F# **syntax sugar** to work with **monadic** types

Uses exclamation mark (pronounced as **bang**) to do **bind**

Logic quite similar to C# **async/await** construct

----------------------------------------------------------------------------

## Async/Await in C#

```csharp
async Task<int> AccessTheWebAsync()  
{   
    HttpClient client = new HttpClient();  
    Task<string> getStringTask = client.GetStringAsync("https://msdn.microsoft.com");  
    string urlContents = await getStringTask;  
    return urlContents.Length;  
}  
```

----------------------------------------------------------------------------

## Async/Await (-ish) in F#

```fsharp
let accessTheWeb() =
    async {
        let client = new HttpClient()
        let getStringTask = client.GetStringAsync("https://msdn.microsoft.com")
        let! urlContents = getStringTask // note the ! after let here
        return urlContents.Length
    }
```

**let!**, **do!**, **return!** for unwrapping the value

There are **many** computation expression in F#

You can write **your own**

----------------------------------------------------------------------------

## Option computation expression

```fsharp

let maybeReturnSomething() =
    option {
        let! x = getSomeOptionalValue()
        let! y = x |> doSomethingWithXandReturnOptionAgain
        return y
    }

```

In case **x is None**, y part is **not executed at all**

----------------------------------------------------------------------------

## Task computation expression

```fsharp

let taskOfSomething() =
    task {
        let! x = getSomeTaskValue()
        let! y = x |> doSomethingWithXandReturnTaskAgain
        return y
    }

```

Flow is **the same** as for Option computation expression

Return value is **Task<'a> (or Task<'T> if you like)**

Task computation expression are heavily used in **Giraffe**

****************************************************************************

## Ok, let's go build some **web** application

****************************************************************************
