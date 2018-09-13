module WebApp.Routes

open System
open Giraffe
open Microsoft.AspNetCore.Http

(*
Fundamentals
*)

(* Models *)
type Reservation = {
    Seats : int
    ForName : string
    Date : DateTime
}

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

let getReservations (next : HttpFunc) (ctx : HttpContext) =
    task {
        let reservations = 
            [
                {
                    Seats = 4
                    ForName = "Provaznik"
                    Date = DateTime.UtcNow
                }
            ]
        return! json reservations next ctx
    }

let setApiVersionHeader (next : HttpFunc) (ctx : HttpContext) =
    setHttpHeader "X-ApiVersion" "1.0" next ctx


let webApp : HttpHandler =
    choose [
        ignoreHandler
        handleOnlyGet >=> addText "RESPONSE ONE" >=> addText "RESPONSE TWO"
        GET >=> route "/test" >=> text "Routing works!"
        GET >=> route "/reservations" >=> getReservations
        GET >=> route "/" >=> text "Hello F#"
    ]