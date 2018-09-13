module WebApp.Routes

open System
open Giraffe
open Microsoft.AspNetCore.Http

(*
Fundamentals
*)

let ignoreHandler (next : HttpFunc) (ctx : HttpContext) =
    task {
        return None
    }

let handleOnlyGet (next : HttpFunc) (ctx : HttpContext) =
    task {
        if ctx.Request.Method = "get" then 
            return None
        else return! (next ctx)
    }

let addText (text:string) (next : HttpFunc) (ctx : HttpContext) =
        task {
            text 
            |> Text.Encoding.UTF8.GetBytes
            |> (fun b -> ctx.Response.Body.WriteAsync(b, 0, b.Length))
            |> ignore

            return! (next ctx) // continue pipeline
            //return Some ctx // stop pipeline
        }


let webApp : HttpHandler =
    choose [
        ignoreHandler
        handleOnlyGet >=> addText "RESPONSE ONE" >=> addText "RESPONSE TWO"
        GET >=> route "/test" >=> text "Routing works!"
        GET >=> route "/" >=> text "Hello F#"
    ]