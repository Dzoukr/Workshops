module WebApp.Example01

open System
open Giraffe
open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks.V2
open Giraffe.HttpStatusCodeHandlers

// simple but useless
let server : HttpHandler = Successful.OK "HELLO"


// with routing by path
