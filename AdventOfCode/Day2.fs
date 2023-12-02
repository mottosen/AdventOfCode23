namespace AdventOfCode

open System
open System.Text.RegularExpressions

type Day2() =
    static member private handleSet (set : string) : int*int*int =
        let cubes : Match seq = Regex.Matches(set, "(\d+) (blue|red|green)") |> Seq.cast

        ((0,0,0), cubes)
        ||> Seq.fold (fun (r,g,b) a -> 
            match a.Groups[2].Value with
            | "red" -> (Math.Max(r, a.Groups[1].Value |> int), g, b)
            | "green" -> (r, Math.Max(g, a.Groups[1].Value |> int), b)
            | "blue" -> (r, g, Math.Max(b, a.Groups[1].Value |> int))
            | _ -> (r,g,b)
        )

    static member Star1 (input : string[]) : string =
        let (red_limit,green_limit,blue_limit) = 12,13,14

        let handleGame (game : string) : int =
            let gameID = Regex.Match(game, "Game (\d+):").Groups[1].Value |> int

            let sets = Regex.Matches(game, "(((\d+) (blue|red|green)),? ?)+")

            let validGame =
                Seq.map (fun (x : Match) -> Day2.handleSet x.Value) sets
                |> Seq.forall (fun (r,g,b) -> r <= red_limit && g <= green_limit && b <= blue_limit)

            if validGame then gameID else 0

        (0, input)
        ||> Array.fold (fun acc game -> acc + (handleGame game))
        |> string

    static member Star2 (input : string[]) : string =
        let handleGame (game : string) : int =
            let sets = Regex.Matches(game, "(((\d+) (blue|red|green)),? ?)+")

            let (r,g,b) =
                Seq.map (fun (x : Match) -> Day2.handleSet x.Value) sets
                |> Seq.fold (fun (ra,ga,ba) (r,g,b) -> (Math.Max(ra, r), Math.Max(ga, g), Math.Max(ba, b))) (0,0,0)

            r*g*b

        (0, input)
        ||> Array.fold (fun acc game -> acc + (handleGame game))
        |> string
