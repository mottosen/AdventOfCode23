namespace AdventOfCode

type Day9() =
    static member private line2num : int list -> int =
        let rec helper (res : int) (diffs : int list) (line : int list) : int =
            if diffs.Length = 0 && (line |> List.forall ((=) 0)) then res
            else match line with
                 | e1 :: e2 :: r -> let diff = e2 - e1 in helper res (diff :: diffs) (e2 :: r)
                 | [e] -> helper (res + e) [] (diffs |> List.rev)
        helper 0 []

    static member Star1 : string[] -> string =
        Array.map (fun line -> line.Split(' ') |> Array.map int |> List.ofArray |> Day9.line2num)
        >> Array.sum >> string

    static member Star2 : string[] -> string =
        Array.map (fun line -> line.Split(' ') |> Array.map int |> Array.rev |> List.ofArray |> Day9.line2num)
        >> Array.sum >> string
