namespace AdventOfCode

open System

type Day9() =
    static member Star1 (input : string[]) : string =
        let rec line2num (diffs : int list) (line : int list) : int =
            if diffs.Length = 0 && (line |> List.forall ((=) 0)) then 0
            else
                match line with
                | e1 :: e2 :: r -> let diff = e2 - e1 in line2num (diffs @ [diff]) (e2 :: r)
                | [e] -> e + (line2num [] diffs)

        input
        |> Array.map (fun line -> line.Split(' ') |> Array.map (fun n -> int n) |> fun arr -> line2num [] (List.ofArray arr))
        |> Array.sum
        |> string

    static member Star2 (input : string[]) : string =
        raise (new NotImplementedException("Day9.Star2"))