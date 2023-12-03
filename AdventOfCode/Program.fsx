open AdventOfCode

open System

[<EntryPoint>]
let main (args : string[]) : int =
    try
        let mutable day = 0

        if (args.Length <> 1 || not (Int32.TryParse(args[0], &day))) then
            raise (new ArgumentException("Program must take exactly one argument of the day to run."))

        if day = 1 then
            Day1.Star1 (InputLoader.GetInputFromFile "inputs_test/day1_1.txt")
            //Day1.Star1 (InputLoader.GetInputFromFile "inputs_real/day1.txt")
            |> printfn "Day 1, Star 1: %s"
        
            Day1.Star2 (InputLoader.GetInputFromFile "inputs_test/day1_2.txt")
            //Day1.Star2 (InputLoader.GetInputFromFile "inputs_real/day1.txt")
            |> printfn "Day 1, Star 2: %s"

        if day = 2 then
            Day2.Star1 (InputLoader.GetInputFromFile "inputs_test/day2_1.txt")
            //Day2.Star1 (InputLoader.GetInputFromFile "inputs_real/day2.txt")
            |> printfn "Day 2, Star 1: %s"
        
            Day2.Star2 (InputLoader.GetInputFromFile "inputs_test/day2_2.txt")
            //Day2.Star2 (InputLoader.GetInputFromFile "inputs_real/day2.txt")
            |> printfn "Day 2, Star 2: %s"

        if day = 3 then
            Day3.Star1 (InputLoader.GetInputFromFile "inputs_test/day3_1.txt")
            //Day3.Star1 (InputLoader.GetInputFromFile "inputs_real/day3.txt")
            |> printfn "Day 3, Star 1: %s"
        
            Day3.Star2 (InputLoader.GetInputFromFile "inputs_test/day3_2.txt")
            //Day3.Star2 (InputLoader.GetInputFromFile "inputs_real/day3.txt")
            |> printfn "Day 3, Star 2: %s"

        0
    with
    | FileException m -> eprintfn "%s" m; 1
    | :? ArgumentException as exn -> eprintf $"Argument Invalid: {exn.Message}"; 1
    | :? NotImplementedException as exn -> eprintf $"Not Implemented: {exn.Message}"; 1
    | :? Exception as exn -> eprintf $"Unknown Error: {exn.Message}"; 1
    | _ -> eprintf "Unknown Exception."; 1