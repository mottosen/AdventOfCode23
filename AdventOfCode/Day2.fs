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
            | _ -> (r,g,b))

    static member Star1 (input : string[]) : string =
        let (rl,gl,bl) = 12,13,14

        let handleGame (game : string) : int =
            let gameID = Regex.Match(game, "Game (\d+):").Groups[1].Value |> int

            let validGame =
                Regex.Matches(game, "(((\d+) (blue|red|green)),? ?)+")
                |> Seq.map (fun (m : Match) -> Day2.handleSet m.Value)
                |> Seq.forall (fun (r,g,b) -> r <= rl && g <= gl && b <= bl)

            if validGame then gameID else 0

        (0, input) ||> Array.fold (fun acc game -> acc + (handleGame game)) |> string

    static member Star2 (input : string[]) : string =
        let handleGame (game : string) : int =
            let (r,g,b) =
                Regex.Matches(game, "(((\d+) (blue|red|green)),? ?)+")
                |> Seq.map (fun (m : Match) -> Day2.handleSet m.Value)
                |> Seq.fold (fun (ra,ga,ba) (r,g,b) -> (Math.Max(ra, r), Math.Max(ga, g), Math.Max(ba, b))) (0,0,0)

            r*g*b

        (0, input) ||> Array.fold (fun acc game -> acc + (handleGame game)) |> string
