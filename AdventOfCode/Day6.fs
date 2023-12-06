namespace AdventOfCode

open System.Text.RegularExpressions

type Day6() =
    static member Star1 (input : string[]) : string =
        let handlePaper (paper : string[]) : (int[] list)*(int []) =
            (([],[||]), input)
            ||> Array.fold (fun (times,records) line ->
                let timeMatch = Regex.Match(line, "^Time: +(((\d+) *)+)$")
                let distMatch = Regex.Match(line, "^Distance: +(((\d+) *)+)$")

                if timeMatch.Success then
                    let nums = Regex.Matches(timeMatch.Groups[1].Value, "\d+")
                            |> Seq.map (fun n -> n.Value |> int) |> Seq.toList
                            |> List.map (fun i -> Array.init (i+1) (fun j -> i-j))
                    (nums, records)
                elif distMatch.Success then
                    (times, (Regex.Matches(distMatch.Groups[1].Value, "\d+") |> Seq.map (fun n -> n.Value |> int) |> Seq.toArray))
                else (times,records))

        let (times,records) = input |> handlePaper
        times |> List.mapi (fun i subTimes -> subTimes |> Array.mapi (fun j n -> if (n*j > records[i]) then 1 else 0) |> Array.sum)
        |> List.fold (fun res st -> res*st) 1 |> string

    static member Star2 (input : string[]) : string =
        let handlePaper (paper : string[]) : int64*int64 =
            ((0L,0L), paper)
            ||> Array.fold (fun (time,record) line ->
                let timeMatch = Regex.Match(line, "^Time: +(((\d+) *)+)$")
                let distMatch = Regex.Match(line, "^Distance: +(((\d+) *)+)$")

                if timeMatch.Success then
                    (Regex.Matches(timeMatch.Groups[1].Value, "\d+") |> Seq.fold (fun acc e -> acc + e.Value) "" |> int64, record)
                elif distMatch.Success then
                    (time, Regex.Matches(distMatch.Groups[1].Value, "\d+") |> Seq.fold (fun acc e -> acc + e.Value) "" |> int64)
                else (time,record))

        let rec helper (res : int64) (i : int64) (t : int64) (r : int64) : int64 =
            if t < 0L then res else helper (res + (if i*t > r then 1L else 0L)) (i+1L) (t-1L) r

        input |> handlePaper ||> helper 0L 0L |> string
