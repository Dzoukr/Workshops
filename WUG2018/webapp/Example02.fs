module WebApp.Example02

open System
open Giraffe
open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks.V2
open Giraffe.HttpStatusCodeHandlers


// with routing by method
let server : HttpHandler = choose [
    GET >=> Successful.OK "GET works!"
    POST >=> Successful.OK "POST works"
]