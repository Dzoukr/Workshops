module WebApp.Example03

open System
open Giraffe
open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks.V2
open Giraffe.HttpStatusCodeHandlers


// with routing by method and path
let server : HttpHandler = 
    choose [
        subRoute "/subroute" (
            choose [
                route "/test" >=> Successful.OK "Hi from subroute"
            ]
        )
        route "/test" >=> Successful.OK "This is just a test"
        route "/" >=> Successful.OK "Root"
    ]