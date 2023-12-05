namespace AdventOfCode

open System
open System.Text.RegularExpressions

type Day5() =
    static member private createMaps (input : string[]) : (int64 list)*(((int64*int64*int64) list) list) =
        (([],[]), input)
        ||> Array.fold (fun (seeds, maps) line -> 
            let seedMatches = Regex.Match(line, "seeds:\s((\d+\s?)+)")
            let mapMatch = Regex.Match(line, "map")
            let rangeMatches = Regex.Match(line, "^(\d+) (\d+) (\d+)$")

            if (seedMatches.Success) then ((seedMatches.Groups[1].Value.Split(' ') |> Array.map (fun n -> int64 n) |> Array.toList), maps)
            elif (mapMatch.Success) then (seeds, []::maps)
            elif (rangeMatches.Success) then
                let (d,s,r) = (rangeMatches.Groups[1].Value |> int64, rangeMatches.Groups[2].Value |> int64, rangeMatches.Groups[3].Value |> int64)
                match maps with elm :: rem -> (seeds, (elm @ [(d,s,r)]) :: rem)
            else (seeds,maps))
    
    static member Star1 (input : string[]) : string =
        let applyMaps (maps : (int64*int64*int64) list) (seeds : int64 list) : int64 list =
            seeds |> List.map (fun seed ->
                (seed,maps) ||> List.fold (fun fs (d,s,r) -> if s<=seed && seed<(s+r) then d+seed-s else fs))

        let seeds,maps = Day5.createMaps input
        let [hum2loc;temp2hum;light2temp;water2light;fert2water;soil2fert;seed2soil] = maps

        seeds
        |> applyMaps seed2soil |> applyMaps soil2fert |> applyMaps fert2water
        |> applyMaps water2light |> applyMaps light2temp |> applyMaps temp2hum
        |> applyMaps hum2loc |> List.min |> sprintf "%A"

    static member Star2 (input : string[]) : string =
        let rec applyMaps (maps : (int64*int64*int64) list) (seeds : (int64*int64) list) : (int64*int64) list =
            match seeds with
            | [] -> []
            | seed :: lst ->
                let (seedStart,seedLimit) = seed

                let (n1,n2,n3) =
                    ((None, Some seed, None), maps)
                    ||> List.fold (fun acc (d,s,r) ->
                        let (mapStart, mapLimit) = (s, s+r-1L)
                        
                        if mapLimit < seedStart || seedLimit < mapStart then acc

                        elif seedStart < mapStart && mapLimit < seedLimit then
                            (Some (seedStart, mapStart-1L), Some (d, d+mapLimit-mapStart), Some (mapLimit+1L,seedLimit))

                        elif mapStart <= seedStart then
                            if seedLimit <= mapLimit then (None, Some (d+seedStart-mapStart, d+seedLimit-mapStart), None)
                            else (None, Some (d+seedStart-mapStart, d+mapLimit-mapStart), Some (mapLimit+1L, seedLimit))
                        
                        elif seedLimit <= mapLimit then
                            if mapStart <= seedStart then (None, Some (d+seedStart-mapStart, d+seedLimit-mapStart), None)
                            else (Some (seedStart, mapStart-1L), Some (d, d+seedLimit-mapStart), None)
                        
                        else acc
                    )

                match (n1,n2,n3) with
                | (Some sl, Some sn, Some sr) -> sn :: (applyMaps maps (sl :: sr :: lst))
                | (Some se, Some sn, None) | (None, Some sn, Some se) -> sn :: (applyMaps maps (se :: lst))
                | (None, Some sn, None) -> sn :: (applyMaps maps lst)

        let rec parseSeeds : int64 list -> (int64*int64) list = function [] -> [] | s :: r :: rem -> (s,s+r-1L) :: (parseSeeds rem)

        let seeds,maps = Day5.createMaps input
        let [hum2loc;temp2hum;light2temp;water2light;fert2water;soil2fert;seed2soil] = maps

        seeds |> parseSeeds |> applyMaps seed2soil
        |> applyMaps soil2fert |> applyMaps fert2water |> applyMaps water2light
        |> applyMaps light2temp |> applyMaps temp2hum |> applyMaps hum2loc
        |> List.fold (fun acc (s1,_) -> if s1 < acc then s1 else acc) Int64.MaxValue
        |> sprintf "%A"
