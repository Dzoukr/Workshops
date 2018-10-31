module WebApp.Example04

open System
open Giraffe
open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks.V2


let customTextHandler (str:string) (next:HttpFunc) (ctx:HttpContext) =
    task {
        let bytes = Text.Encoding.UTF8.GetBytes(str)
        do! ctx.Response.Body.WriteAsync(bytes, 0, bytes.Length)
        return! next ctx
    }

let stopHandler (next:HttpFunc) (ctx:HttpContext) =
    task {
        return None
    }

let notCallingNextHandler (next:HttpFunc) (ctx:HttpContext) =
    task {
        let bytes = Text.Encoding.UTF8.GetBytes("Not calling next")
        do! ctx.Response.Body.WriteAsync(bytes, 0, bytes.Length)
        return Some ctx
    }

// with routing by method and path
let server : HttpHandler = 
    choose [
        stopHandler >=> Successful.OK "Hammer time"
        customTextHandler "HELLO FROM MY HANDLER"
        //notCallingNextHandler >=> Successful.OK "NOT CALLED THIS"
    ]