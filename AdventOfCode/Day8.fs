namespace AdventOfCode

open System
open System.Text.RegularExpressions

type Day8() =
    static member private lcm (a : int64) (b : int64) : int64 =
        let rec gcd (a : int64) (b : int64) : int64 = 
            match a, b with | (a,0L) -> a | (a,b) -> gcd b (a % b)
        (Math.Abs (a * b)) / (gcd a b)

    static member private walker (steps : int) (pos : string) (choices : char[]) (routes : Map<string, string*string>) : int64 =
        let rec walker (steps : int) (pos : string) : int64 =
            if Regex.IsMatch(pos, "^.*Z$") then int64 steps
            else
                let dir = choices[steps % choices.Length]
                let next = if dir = 'L' then fst routes[pos] else snd routes[pos]
                walker (steps + 1) next
        walker steps pos

    static member private splitAndCalc (regStart : string) (input : string[]) : int64 =
        (([], Array.empty<char>, []), input)
        ||> Array.fold (fun (positions, choices, routes) line ->
            let dirMatch = Regex.Match(line, "^(R|L)+$")
            let selMatch = Regex.Match(line, "^((\d|\w)+) = \(((\d|\w)+), ((\d|\w)+)\)$")

            if dirMatch.Success then (positions, dirMatch.Value |> Seq.toArray, routes)
            elif selMatch.Success then
                let pos = selMatch.Groups[1].Value
                ((if Regex.IsMatch(pos, regStart) then pos :: positions else positions), choices,
                (selMatch.Groups[1].Value, (selMatch.Groups[3].Value, selMatch.Groups[5].Value)) :: routes)
            else (positions, choices, routes))
        |> function (positions, choices, routes) -> (positions, choices, Map.ofList routes)
        |> function (positions, choices, routes) ->
                    (1L, positions) ||> List.fold (fun res pos -> Day8.lcm res (Day8.walker 0 pos choices routes))

    static member Star1 (input : string[]) : string =
        input |> Day8.splitAndCalc "^AAA$" |> string

    static member Star2 (input : string[]) : string =
        input |> Day8.splitAndCalc "^.*A$" |> string
