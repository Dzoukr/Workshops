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
        setApiVersionHeader
        route "/reservation" >=> choose [
            POST >=> bindRequest
            GET >=> readApiVersion
        ]

        GET >=> choose [
            route "/test" >=> text "Routing works!"
            route "/reservations" >=> getReservations
            route "/" >=> text "Hello F#"
        ]
    ]