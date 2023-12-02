namespace AdventOfCode

open System.Text.RegularExpressions

type Day1() =
    static member private line2int (line : string) : int =
        Regex.Matches(line, "\d")
        |> Seq.map (fun m -> m.Value)
        |> fun seq -> int (Seq.head seq + Seq.last seq)

    static member Star1 (input : string[]) : string =
        (0, input)
        ||> Array.fold (fun acc s -> acc + (Day1.line2int s))
        |> string

    static member Star2 (input : string[]) : string =
        let fixLine (line : string) : string =
            Regex.Replace(line, "one", "o1ne")
            |> fun l -> Regex.Replace(l, "two", "t2wo")
            |> fun l -> Regex.Replace(l, "three", "t3hree")
            |> fun l -> Regex.Replace(l, "four", "f4our")
            |> fun l -> Regex.Replace(l, "five", "f5ive")
            |> fun l -> Regex.Replace(l, "six", "s6ix")
            |> fun l -> Regex.Replace(l, "seven", "s7even")
            |> fun l -> Regex.Replace(l, "eight", "e8ight")
            |> fun l -> Regex.Replace(l, "nine", "n9ine")

        (0, input)
        ||> Array.fold (fun acc s -> acc + (s |> fixLine |> Day1.line2int))
        |> string
