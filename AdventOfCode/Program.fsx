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

        elif day = 2 then
            Day2.Star1 (InputLoader.GetInputFromFile "inputs_test/day2_1.txt")
            //Day2.Star1 (InputLoader.GetInputFromFile "inputs_real/day2.txt")
            |> printfn "Day 2, Star 1: %s"
        
            Day2.Star2 (InputLoader.GetInputFromFile "inputs_test/day2_2.txt")
            //Day2.Star2 (InputLoader.GetInputFromFile "inputs_real/day2.txt")
            |> printfn "Day 2, Star 2: %s"

        elif day = 3 then
            Day3.Star1 (InputLoader.GetInputFromFile "inputs_test/day3_1.txt")
            //Day3.Star1 (InputLoader.GetInputFromFile "inputs_real/day3.txt")
            |> printfn "Day 3, Star 1: %s"
        
            Day3.Star2 (InputLoader.GetInputFromFile "inputs_test/day3_2.txt")
            //Day3.Star2 (InputLoader.GetInputFromFile "inputs_real/day3.txt")
            |> printfn "Day 3, Star 2: %s"

        elif day = 4 then
            Day4.Star1 (InputLoader.GetInputFromFile "inputs_test/day4_1.txt")
            //Day4.Star1 (InputLoader.GetInputFromFile "inputs_real/day4.txt")
            |> printfn "Day 4, Star 1: %s"
        
            Day4.Star2 (InputLoader.GetInputFromFile "inputs_test/day4_2.txt")
            //Day4.Star2 (InputLoader.GetInputFromFile "inputs_real/day4.txt")
            |> printfn "Day 4, Star 2: %s"

        elif day = 5 then
            Day5.Star1 (InputLoader.GetInputFromFile "inputs_test/day5_1.txt")
            //Day5.Star1 (InputLoader.GetInputFromFile "inputs_real/day5.txt")
            |> printfn "Day 5, Star 1: %s"
        
            Day5.Star2 (InputLoader.GetInputFromFile "inputs_test/day5_2.txt")
            //Day5.Star2 (InputLoader.GetInputFromFile "inputs_real/day5.txt")
            |> printfn "Day 5, Star 2: %s"

        elif day = 6 then
            Day6.Star1 (InputLoader.GetInputFromFile "inputs_test/day6_1.txt")
            //Day6.Star1 (InputLoader.GetInputFromFile "inputs_real/day6.txt")
            |> printfn "Day 6, Star 1: %s"
        
            Day6.Star2 (InputLoader.GetInputFromFile "inputs_test/day6_2.txt")
            //Day6.Star2 (InputLoader.GetInputFromFile "inputs_real/day6.txt")
            |> printfn "Day 6, Star 2: %s"

        elif day = 7 then
            Day7.Star1 (InputLoader.GetInputFromFile "inputs_test/day7_1.txt")
            //Day7.Star1 (InputLoader.GetInputFromFile "inputs_real/day7.txt")
            |> printfn "Day 7, Star 1: %s"
        
            Day7.Star2 (InputLoader.GetInputFromFile "inputs_test/day7_2.txt")
            //Day7.Star2 (InputLoader.GetInputFromFile "inputs_real/day7.txt")
            |> printfn "Day 7, Star 2: %s"

        elif day = 8 then
            //Day8.Star1 (InputLoader.GetInputFromFile "inputs_test/day8_1.txt")
            Day8.Star1 (InputLoader.GetInputFromFile "inputs_real/day8.txt")
            |> printfn "Day 8, Star 1: %s"
        
            //Day8.Star2 (InputLoader.GetInputFromFile "inputs_test/day8_2.txt")
            Day8.Star2 (InputLoader.GetInputFromFile "inputs_real/day8.txt")
            |> printfn "Day 8, Star 2: %s"

        else
            printfn "Day not solved yet."
        
        0
    with
    | FileException m -> eprintfn "%s" m; 1
    | :? ArgumentException as exn -> eprintf $"Argument Invalid: {exn.Message}"; 1
    | :? NotImplementedException as exn -> eprintf $"Not Implemented: {exn.Message}"; 1
    | :? Exception as exn -> eprintf $"Unknown Error: {exn.Message}"; 1
    | _ -> eprintf "Unknown Exception."; 1