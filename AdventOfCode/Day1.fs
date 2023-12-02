namespace AdventOfCode

open System.Text.RegularExpressions

type Day1() =
    static member private line2int (line : string) : int =
        let ints = Regex.Matches(line, "\d")

        if (ints.Count = 0) then 0
        else
            Array.init (ints.Count) (fun i -> ints[i].Value)
            |> fun arr -> int (Array.head arr + Array.last arr)

    static member private fixLine (line : string) : string =
        Regex.Replace(line, "one", "o1ne")
        |> fun l -> Regex.Replace(l, "two", "t2wo")
        |> fun l -> Regex.Replace(l, "three", "t3hree")
        |> fun l -> Regex.Replace(l, "four", "f4our")
        |> fun l -> Regex.Replace(l, "five", "f5ive")
        |> fun l -> Regex.Replace(l, "six", "s6ix")
        |> fun l -> Regex.Replace(l, "seven", "s7even")
        |> fun l -> Regex.Replace(l, "eight", "e8ight")
        |> fun l -> Regex.Replace(l, "nine", "n9ine")

    static member Star1 (input : string[]) : string =
        (0, input)
        ||> Array.fold (fun acc s -> acc + (Day1.line2int s))
        |> string

    static member Star2 (input : string[]) : string =
        (0, input)
        ||> Array.fold (fun acc s -> acc + (s |> Day1.fixLine |> Day1.line2int))
        |> string