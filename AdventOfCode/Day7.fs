namespace AdventOfCode

open System.Text.RegularExpressions

type Hand = char seq
type HandType = HighCard of Hand
                | FiveKind of Hand | FourKind of Hand | ThreeKind of Hand
                | FullHouse of Hand | TwoPair of Hand | OnePair of Hand

type Day7() =
    static member private parseChar (star : int) (c : char) : int =
        match c with
        | 'A' -> 14 | 'K' -> 13 | 'Q' -> 12 | 'J' when star=1 -> 11 | 'T' -> 10 | 'J' when star=2 -> 1
        |'2' -> 2 | '3' -> 3 | '4' -> 4 | '5' -> 5 | '6' -> 6 | '7' -> 7 | '8' -> 8 | '9' -> 9

    static member private compareHands (star : int) (ht1 : HandType) (ht2 : HandType) : int =
        let rec helper (h1 : char seq) (h2 : char seq) : int =
            (0, h1, h2) |||> Seq.fold2 (fun acc c1 c2 -> if acc <> 0 then acc else (compare (Day7.parseChar star c1) (Day7.parseChar star c2)))

        match ht1 with
        | FiveKind h1 -> match ht2 with FiveKind h2 -> helper h1 h2 | _ -> 1
        | FourKind h1 -> match ht2 with FiveKind _ -> -1 | FourKind h2 -> helper h1 h2 | _ -> 1
        | FullHouse h1 -> match ht2 with FiveKind _ | FourKind _ -> -1 | FullHouse h2 -> helper h1 h2 | _ -> 1
        | ThreeKind h1 -> match ht2 with FiveKind _ | FourKind _ | FullHouse _ -> -1 | ThreeKind h2 -> helper h1 h2 | _ -> 1
        | TwoPair h1 -> match ht2 with OnePair _ | HighCard _ -> 1 | TwoPair h2 -> helper h1 h2 | _ -> -1
        | OnePair h1 -> match ht2 with HighCard _ -> 1 | OnePair h2 -> helper h1 h2 | _ -> -1
        | HighCard h1 -> match ht2 with HighCard h2 -> helper h1 h2 | _ -> -1

    static member private parseHand (withJokers : bool) (hand : string) =
        let handParsed = hand |> Seq.toList
        let (handNoJokers, numJokers) = 
            if not withJokers then (handParsed, 0)
            else hand |> Seq.fold (fun (cs,j) c -> if c = 'J' then (cs,j+1) else (c :: cs, j)) ([], 0)

        handNoJokers |> List.mapi (fun i c -> (c, handNoJokers |> List.filter (fun e -> e = c) |> List.length))
        |> List.sortWith (fun (_,c1) (_,c2) -> compare c2 c1) |> List.distinctBy (fun (h,_) -> h)
        |> function
        | (_,c1) :: (_,c2) :: r ->
            if c1+numJokers >= 5 then FiveKind handParsed
            elif c1+numJokers = 4 then FourKind handParsed
            elif c1+numJokers = 3 then
                let jokersLeft = numJokers - (3 - c1)
                if c2+jokersLeft = 2 || c2+jokersLeft = 3 then FullHouse handParsed
                else ThreeKind handParsed
            elif c1+numJokers = 2 then
                let jokersLeft = numJokers - (2 - c1)
                if c2+jokersLeft = 2 then TwoPair handParsed
                else OnePair handParsed
            else HighCard handParsed
        | (_,c) :: r ->
            if c+numJokers >= 5 then FiveKind handParsed
            elif c+numJokers = 4 then FourKind handParsed
            elif c+numJokers = 3 then ThreeKind handParsed
            elif c+numJokers = 2 then OnePair handParsed
            else HighCard handParsed
        | _ ->
            if numJokers = 5 then FiveKind handParsed
            elif numJokers = 4 then FourKind handParsed
            elif numJokers = 3 then ThreeKind handParsed
            elif numJokers = 2 then OnePair handParsed
            else HighCard handParsed

    static member Star1 (input : string[]) : string =
        input |> Array.map (fun line ->
            let parse = Regex.Match(line, "^(.*) (\d+)$")
            (parse.Groups[1].Value |> Day7.parseHand false, parse.Groups[2].Value |> int))
        |> Array.sortWith (fun (h1,_) (h2,_) -> Day7.compareHands 1 h1 h2)
        |> Array.mapi (fun i (h,b) -> b*(i+1))
        |> Array.sum |> string

    static member Star2 (input : string[]) : string =
        input |> Array.map (fun line ->
            let parse = Regex.Match(line, "^(.*) (\d+)$")
            (parse.Groups[1].Value |> Day7.parseHand true, parse.Groups[2].Value |> int))
        |> Array.sortWith (fun (h1,_) (h2,_) -> Day7.compareHands 2 h1 h2)
        |> Array.mapi (fun i (h,b) -> b*(i+1))
        |> Array.sum |> string
