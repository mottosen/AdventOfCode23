namespace AdventOfCode

open System
open System.Text.RegularExpressions

type Hand = char seq
type HandType = HighCard of Hand
                | FiveKind of Hand | FourKind of Hand | ThreeKind of Hand
                | FullHouse of Hand | TwoPair of Hand | OnePair of Hand

type Day7() =
    static member Star1 (input : string[]) : string =
        let parseChar (c : char) : int =
            match c with
            | 'A' -> 14 | 'K' -> 13 | 'Q' -> 12 | 'J' -> 11 | 'T' -> 10 
            | '1' -> 1 | '2' -> 2 | '3' -> 3 | '4' -> 4 | '5' -> 5 | '6' -> 6 | '7' -> 7 | '8' -> 8 | '9' -> 9

        let parseHand (hand : string) : HandType =
            let rec helper (res : HandType) (foo : char) (nums : (char*int) list) : HandType =
                match res with 
                | FiveKind _ | FourKind _ | FullHouse _ | TwoPair _ -> res
                | ThreeKind h -> 
                    match nums with
                    | [] -> res
                    | (n,c) :: lst ->
                        if n<>foo && c = 2 then helper (FullHouse h) n lst
                        else helper res foo lst
                | OnePair h ->
                    match nums with
                    | [] -> res
                    | (n,c) :: lst ->
                        if n<>foo && c = 3 then helper (FullHouse h) n lst
                        elif n<>foo && c = 2 then helper (TwoPair h) n lst
                        else helper res foo lst
                | HighCard h ->
                    match nums with
                    | [] -> res
                    | (n,c) :: lst ->
                        if n<>foo && c = 5 then helper (FiveKind h) n lst
                        elif n<>foo && c = 4 then helper (FourKind h) n lst
                        elif n<>foo && c = 3 then helper (ThreeKind h) n lst
                        elif n<>foo && c = 2 then helper (OnePair h) n lst
                        else helper res foo lst

            let handParsed = hand |> Seq.toList
            let bar = handParsed |> List.mapi (fun i c -> (c, handParsed[i..] |> List.filter (fun e -> e = c) |> List.length))

            let baz = helper (HighCard handParsed) ' ' (bar |> Seq.toList)
            baz

        let compareHands (ht1 : HandType) (ht2 : HandType) : int =
            let rec helper (h1 : char seq) (h2 : char seq) : int =
                (0, h1,h2) |||> Seq.fold2 (fun acc c1 c2 -> if acc <> 0 then acc else (compare (parseChar c1) (parseChar c2)))

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
            let foo = Regex.Match(line, "^(.*) (\d+)$")

            (foo.Groups[1].Value |> parseHand, foo.Groups[2].Value |> int) // (hand, bid)
            )
        |> Array.sortWith (fun (h1,_) (h2,_) -> compareHands h1 h2)
        |> Array.mapi (fun i (_,b) -> b * (i+1))
        |> Array.sum |> string

    static member Star2 (input : string[]) : string =
        let parseChar (c : char) : int =
            match c with
            | 'A' -> 14 | 'K' -> 13 | 'Q' -> 12 | 'J' -> 1 | 'T' -> 10
            | '1' -> 1 | '2' -> 2 | '3' -> 3 | '4' -> 4 | '5' -> 5 | '6' -> 6 | '7' -> 7 | '8' -> 8 | '9' -> 9

        let parseHand (hand : string) : HandType =
            let rec helper (res : HandType) (foo : char) (nums : (char*int) list) : HandType =
                match res with 
                | FiveKind _ | FourKind _ | FullHouse _ | TwoPair _ -> res
                | ThreeKind h -> 
                    match nums with
                    | [] -> res
                    | (n,c) :: lst ->
                        if n<>foo && c = 4 then helper (FourKind h) n lst
                        elif n<>foo && c = 2 then helper (FullHouse h) n lst
                        else helper res foo lst
                | OnePair h ->
                    match nums with
                    | [] -> res
                    | (n,c) :: lst ->
                        if n<>foo && c = 3 then helper (FullHouse h) n lst
                        elif n<>foo && c = 2 then helper (TwoPair h) n lst
                        else helper res foo lst
                | HighCard h ->
                    match nums with
                    | [] -> res
                    | (n,c) :: lst ->
                        if n<>foo && c = 5 then helper (FiveKind h) n lst
                        elif n<>foo && c = 4 then helper (FourKind h) n lst
                        elif n<>foo && c = 3 then helper (ThreeKind h) n lst
                        elif n<>foo && c = 2 then helper (OnePair h) n lst
                        else helper res foo lst

            let handParsed = hand |> Seq.toList
            printfn "\n%A" (sprintf "%A" handParsed)
            let bar = handParsed |> List.mapi (fun i c -> (c, handParsed[i..] |> List.filter (fun e -> e = c || e = 'J') |> List.length))
            printfn "%A" (sprintf "%A" bar)

            let baz = helper (HighCard handParsed) ' ' (bar |> Seq.toList)
            printfn "%A" baz
            baz

        let compareHands (ht1 : HandType) (ht2 : HandType) : int =
            let rec helper (h1 : char seq) (h2 : char seq) : int =
                (0, h1,h2) |||> Seq.fold2 (fun acc c1 c2 -> if acc <> 0 then acc else (compare (parseChar c1) (parseChar c2)))

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
            let foo = Regex.Match(line, "^(.*) (\d+)$")

            (foo.Groups[1].Value |> parseHand, foo.Groups[2].Value |> int) // (hand, bid)
            )
        |> Array.sortWith (fun (h1,_) (h2,_) -> compareHands h1 h2)
        |> Array.mapi (fun i (_,b) -> b * (i+1))
        |> Array.sum |> string
