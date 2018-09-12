module WebApp.Routes

open Giraffe

(*

    Your playground starts here

*)


let webApp : HttpHandler =
    choose [
        GET >=> route "/test" >=> text "Routing works!"
        GET >=> route "/" >=> text "Hello F#"
    ]