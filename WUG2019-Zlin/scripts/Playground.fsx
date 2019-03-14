let add x y = x + y
let add10 = add 10

add10 90

let add2 x =
    let c = ""
    fun y -> x + y

let myRecord = {| Name = "Roman" |}
let myEnhancedRec = {| myRecord with Age = 36 |}

let printAge (args: {| Age: int |}) = 
    printfn "My age is %i" args.Age