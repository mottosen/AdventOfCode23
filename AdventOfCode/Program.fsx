open AdventOfCode

open System

[<EntryPoint>]
let main (args : string[]) : int =
    try
        let mutable day = 0

        if (args.Length <> 1 || not (Int32.TryParse(args[0], &day))) then
            raise (new ArgumentException("Program must take exactly one argument of the day to run."))

        if day = 1 then
            //Day1.Star1 [| "1abc2" ; "pqr3stu8vwx" ; "a1b2c3d4e5f" ; "treb7uchet" |]
            Day1.Star1 (InputLoader.GetInputFromFile "inputs/day1.txt")
            |> printfn "Day 1, Star 1: %s"
        
            //Day1.Star2 [| "two1nine" ; "eightwothree" ; "abcone2threexyz" ; "xtwone3four" ; "4nineeightseven2" ; "zoneight234" ; "7pqrstsixteen" |]
            Day1.Star2 (InputLoader.GetInputFromFile "inputs/day1.txt")
            |> printfn "Day 1, Star 2: %s"

        0
    with
    | :? ArgumentException as exn -> eprintf "Argument Invalid: %s" exn.Message; 1
    | :? NotImplementedException as exn -> eprintf "Not Implemented: %s" exn.Message; 1
    | FileException m -> eprintfn "%s" m; 1
    | _ -> eprintf "Unknown Exception."; 1