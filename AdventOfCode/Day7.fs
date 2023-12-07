namespace AdventOfCode

open System
open System.Text.RegularExpressions

type Hand = char seq
type HandType = HighCard of Hand
                | FiveKind of Hand | FourKind of Hand | ThreeKind of Hand
                | FullHouse of Hand | TwoPair of Hand | OnePair of Hand

type Day7() =
    static member parseChar (c : char) (star : int) : int =
            match c with
            | 'A' -> 14 | 'K' -> 13 | 'Q' -> 12 | 'J' when star=1 -> 11 | 'T' -> 10 | 'J' when star=2 -> 1
            |'2' -> 2 | '3' -> 3 | '4' -> 4 | '5' -> 5 | '6' -> 6 | '7' -> 7 | '8' -> 8 | '9' -> 9

    static member Star1 (input : string[]) : string =
        let parseHand (hand : string) =
            let handParsed = hand |> Seq.toList
            let handCounts = handParsed |> List.mapi (fun i c -> (c, handParsed[i..] |> List.filter (fun e -> e = c) |> List.length))
                            |> List.distinctBy (fun (c,_) -> c) |> List.sortWith (fun (_,c1) (_,c2) -> compare c2 c1)

            match handCounts with
            | (_,c1) :: (_,c2) :: r ->
                if c1 = 5 then FiveKind handParsed
                elif c1 = 4 then FourKind handParsed
                elif c1 = 3 then
                    if c2 = 2 then FullHouse handParsed
                    else ThreeKind handParsed
                elif c1 = 2 then
                    if c2 = 2 then TwoPair handParsed
                    else OnePair handParsed
                else HighCard handParsed
            | (_,c) :: r ->
                if c = 5 then FiveKind handParsed
                elif c = 4 then FourKind handParsed
                elif c = 3 then ThreeKind handParsed
                elif c = 2 then OnePair handParsed
                else HighCard handParsed

        let compareHands (ht1 : HandType) (ht2 : HandType) : int =
            let rec helper (h1 : char seq) (h2 : char seq) : int =
                (0, h1,h2) |||> Seq.fold2 (fun acc c1 c2 -> if acc <> 0 then acc else (compare (Day7.parseChar c1 1) (Day7.parseChar c2 1)))

            match ht1 with
            | FiveKind h1 ->
                match ht2 with FiveKind h2 -> helper h1 h2 | _ -> 1
            | FourKind h1 ->
                match ht2 with FiveKind _ -> -1 | FourKind h2 -> helper h1 h2 | _ -> 1
            | FullHouse h1 ->
                match ht2 with FiveKind _ | FourKind _ -> -1 | FullHouse h2 -> helper h1 h2 | _ -> 1
            | ThreeKind h1 ->
                match ht2 with FiveKind _ | FourKind _ | FullHouse _ -> -1 | ThreeKind h2 -> helper h1 h2 | _ -> 1
            | TwoPair h1 ->
                match ht2 with OnePair _ | HighCard _ -> 1 | TwoPair h2 -> helper h1 h2 | _ -> -1
            | OnePair h1 ->
                match ht2 with HighCard _ -> 1 | OnePair h2 -> helper h1 h2 | _ -> -1
            | HighCard h1 ->
                match ht2 with HighCard h2 -> helper h1 h2 | _ -> -1

        input |> Array.map (fun line ->
            let parse = Regex.Match(line, "^(.*) (\d+)$")
            (parse.Groups[1].Value |> parseHand, parse.Groups[2].Value |> int))
        |> Array.sortWith (fun (h1,_) (h2,_) -> compareHands h1 h2)
        |> Array.mapi (fun i (h,b) -> b*(i+1))
        |> Array.sum |> string

    static member Star2 (input : string[]) : string =
        let parseHand (hand : string) =
            let handParsed = hand |> Seq.toList
            let handCounts = handParsed |> List.mapi (fun i c -> (c, handParsed[i..] |> List.filter (fun e -> e = c || e = 'J') |> List.length))
                            |> List.distinctBy (fun (c,_) -> c) |> List.sortWith (fun (_,c1) (_,c2) -> compare c2 c1)

            match handCounts with
            | (_,c1) :: (_,c2) :: r ->
                if c1 = 5 then FiveKind handParsed
                elif c1 = 4 then FourKind handParsed
                elif c1 = 3 then
                    if c2 = 2 then FullHouse handParsed
                    else ThreeKind handParsed
                elif c1 = 2 then
                    if c2 = 2 then TwoPair handParsed
                    else OnePair handParsed
                else HighCard handParsed
            | (_,c) :: r ->
                if c = 5 then FiveKind handParsed
                elif c = 4 then FourKind handParsed
                elif c = 3 then ThreeKind handParsed
                elif c = 2 then OnePair handParsed
                else HighCard handParsed

        let compareHands (ht1 : HandType) (ht2 : HandType) : int =
            let rec helper (h1 : char seq) (h2 : char seq) : int =
                (0, h1,h2) |||> Seq.fold2 (fun acc c1 c2 -> if acc <> 0 then acc else (compare (Day7.parseChar c1 2) (Day7.parseChar c2 2)))

            match ht1 with
            | FiveKind h1 ->
                match ht2 with FiveKind h2 -> helper h1 h2 | _ -> 1
            | FourKind h1 ->
                match ht2 with FiveKind _ -> -1 | FourKind h2 -> helper h1 h2 | _ -> 1
            | FullHouse h1 ->
                match ht2 with FiveKind _ | FourKind _ -> -1 | FullHouse h2 -> helper h1 h2 | _ -> 1
            | ThreeKind h1 ->
                match ht2 with FiveKind _ | FourKind _ | FullHouse _ -> -1 | ThreeKind h2 -> helper h1 h2 | _ -> 1
            | TwoPair h1 ->
                match ht2 with OnePair _ | HighCard _ -> 1 | TwoPair h2 -> helper h1 h2 | _ -> -1
            | OnePair h1 ->
                match ht2 with HighCard _ -> 1 | OnePair h2 -> helper h1 h2 | _ -> -1
            | HighCard h1 ->
                match ht2 with HighCard h2 -> helper h1 h2 | _ -> -1

        input |> Array.map (fun line ->
            let parse = Regex.Match(line, "^(.*) (\d+)$")
            (parse.Groups[1].Value |> parseHand, parse.Groups[2].Value |> int))
        |> Array.sortWith (fun (h1,_) (h2,_) -> compareHands h1 h2)
        |> Array.mapi (fun i (h,b) -> b*(i+1))
        |> Array.sum |> string
