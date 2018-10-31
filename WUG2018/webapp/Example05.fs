module WebApp.Example05

open System
open Giraffe
open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks.V2

// with routing by method and path
let server : HttpHandler = 
    choose [
        routef "/hello/%s" (fun name -> sprintf "Hello %s" name |> Successful.OK)
        Successful.OK "Try path /hello/something"
    ]