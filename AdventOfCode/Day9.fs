namespace AdventOfCode

type Day9() =
    static member private line2num (line : int list) : int =
        let rec helper (diffs : int list) (line : int list) : int =
            if diffs.Length = 0 && (line |> List.forall ((=) 0)) then 0
            else match line with
                 | e1 :: e2 :: r -> let diff = e2 - e1 in helper (diffs @ [diff]) (e2 :: r)
                 | [e] -> e + (helper [] diffs)
        helper [] line

    static member Star1 (input : string[]) : string =
        input
        |> Array.map (fun line -> line.Split(' ') |> Array.map (fun n -> int n) |> fun arr -> Day9.line2num (arr |> List.ofArray))
        |> Array.sum |> string

    static member Star2 (input : string[]) : string =
        input
        |> Array.map (fun line -> line.Split(' ') |> Array.map (fun n -> int n) |> fun arr -> Day9.line2num (arr |> Array.rev |> List.ofArray))
        |> Array.sum |> string
