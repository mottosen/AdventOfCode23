namespace AdventOfCode

open System
open System.Text.RegularExpressions

type Day3() =
    static member Star1 (input : string[]) : string =
        let nextToSymbol (num : Match) (line : string) (neighbors : (string option)*(string option)) : bool =
            let (s1, s2) = (Math.Max(num.Groups[1].Index - 1, 0), Math.Min(num.Groups[1].Index + num.Length, line.Length - 1))
            let symbols = "[^\d\.]"

            (try Regex.IsMatch(line[s1..s2], symbols) with | _ -> false)
            || match neighbors with
                | None, None -> false
                | Some l, None | None, Some l -> Regex.IsMatch(l[s1..s2], symbols)
                | Some l1, Some l2 -> Regex.IsMatch(l1[s1..s2], symbols) || Regex.IsMatch(l2[s1..s2], symbols)
        
        let lineSum (idx : int) (lines : string[]) : int =
            let nums = Regex.Matches(lines[idx], "(\d+)")
            let neighbors = ((try Some (lines[idx-1]) with | _ -> None), (try Some (lines[idx+1]) with | _ -> None))

            (0, nums) ||> Seq.fold (fun acc num -> if (nextToSymbol num lines[idx] neighbors) then acc+int(num.Value) else acc)

        input
        |> Array.mapi (fun i line -> lineSum i input)
        |> Array.fold (fun acc sum -> acc + sum) 0 |> string

    static member Star2 (input : string[]) : string =
        let getNeighborNums ((s1,s2) : int*int) (line : string) : float seq =
            Regex.Matches(line, "(\d+)")
            |> Seq.filter (fun (m:Match) ->
                    let num = m.Groups[1]
                    let (ns,ne) = (num.Index, (num.Index + num.Length - 1))

                    ne = s1 || ns = s2 || (ns<=s1+1 && ne>=s2-1))
            |> Seq.map (fun (m:Match) -> float(m.Value))

        let gearRatio (gear : Match) (line : string) ((n1,n2) : (string option)*(string option)) : float =
            let (s1, s2) = (Math.Max(gear.Groups[1].Index - 1, 0), Math.Min(gear.Groups[1].Index + 1, line.Length - 1))

            let neighbors =
                [ (getNeighborNums (s1,s2) line);
                  if n1 = None then [] else (getNeighborNums (s1,s2) n1.Value);
                  if n2 = None then [] else (getNeighborNums (s1,s2) n2.Value)
                ] |> Seq.concat

            if (Seq.length neighbors) <> 2 then 0.0
            else neighbors |> Seq.fold (fun acc f -> acc * f) 1.0
        
        let lineSum (idx : int) (lines : string[]) : double =
            let gears = Regex.Matches(lines[idx], "(\*)")
            let neighbors = ((try Some (lines[idx-1]) with | _ -> None), (try Some (lines[idx+1]) with | _ -> None))

            (0.0, gears) ||> Seq.fold (fun acc gear -> acc + (gearRatio gear lines[idx] neighbors))

        input
        |> Array.mapi (fun i line -> lineSum i input)
        |> Array.fold (fun acc sum -> acc + sum) 0.0 |> string
