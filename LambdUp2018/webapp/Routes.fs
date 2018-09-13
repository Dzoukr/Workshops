module WebApp.Routes

open System
open Giraffe
open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks.V2

(*
Fundamentals
*)

(* Models *)
[<CLIMutable>]
type Reservation = {
    Seats : int
    ForName : string
    Date : DateTime
}

type ReservationV2 = {
    Seats : int
    ForName : string
    Date : DateTime
    Hours : int
}

let dummyReservation = 
    {
        Seats = 4
        ForName = "Roman"
        Date = DateTime.UtcNow
    }

let dummyReservationV2 = 
    {
        Seats = 4
        ForName = "Roman"
        Date = DateTime.UtcNow
        Hours = 2
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
        let reservations = [ dummyReservation ]
        return! json reservations next ctx
    }

let setApiVersionHeader (next : HttpFunc) (ctx : HttpContext) =
    setHttpHeader "X-ApiVersion" "1.0" next ctx

let readApiVersion (next:HttpFunc) (ctx:HttpContext) =
    task {
        match ctx.TryGetRequestHeader "X-ApiVersion" with
        | None ->
            return! json dummyReservation next ctx
        | Some "1.1" ->
            return! json dummyReservationV2 next ctx
        | Some _ -> 
            return! json dummyReservation next ctx
    }

let bindRequest (next:HttpFunc) (ctx:HttpContext) =
    task {
        let! model = ctx.BindJsonAsync<Reservation>()

        return! Successful.CREATED model next ctx
    }


let webApp : HttpHandler =
    choose [
        ignoreHandler
        handleOnlyGet >=> addText "RESPONSE ONE" >=> addText "RESPONSE TWO"
        GET >=> route "/test" >=> text "Routing works!"
        GET >=> route "/reservation" >=> readApiVersion
        POST >=> route "/reservation" >=> bindRequest
        GET >=> route "/reservations" >=> getReservations
        GET >=> route "/" >=> text "Hello F#"
    ]