namespace AdventOfCode

open System.Text.RegularExpressions

type Day4() =
    static member private checkCard (idx : int) (lines : string[]) : int list  =
        try
            let parsed = Regex.Matches(lines[idx], "Card +\d+: +((\d+ +)+)\| +((\d+ *)+)")

            let strWin,strHave = parsed[0].Groups[1], parsed[0].Groups[3]

            let numsHave = Regex.Matches(strHave.Value, "\d+") |> Seq.map (fun s -> int(s.Value))

            let matches = Regex.Matches(strWin.Value, "\d+") |> Seq.map (fun s -> int(s.Value))
                        |> Seq.filter (fun num -> Seq.contains num numsHave) |> Seq.length

            [idx+1..idx+matches]
        with _ -> []
    
    static member Star1 (input : string[]) : string =
        let rec handleLines (totalCards : int) (idx : int) (lines : string[]) : int =
            if idx = lines.Length then totalCards
            else
                let winningNums = Day4.checkCard idx lines |> List.length in
                handleLines (totalCards + (if winningNums = 0 then 0 else 1<<<(winningNums-1))) (idx + 1) (lines)

        input |> handleLines 0 0 |> string

    static member Star2 (input : string[]) : string =
        let rec handleLines (processedCards : int) (copiedCards : int list) (idx : int) (lines : string[]) : int =
            if idx = lines.Length then processedCards
            else
                let cardCopies,filteredCopies =
                    ((0,[]), copiedCards)
                    ||> List.fold (fun (matches, filtered) card ->
                        if card = idx then (matches + 1, filtered) else (matches, card :: filtered))
                let cardsWon = (Day4.checkCard idx lines) |> List.replicate (cardCopies + 1) |> List.concat // optimize this? funtional hash table?
                handleLines (processedCards + 1 + cardCopies) (cardsWon @ filteredCopies) (idx + 1) (lines)

        input |> handleLines 0 [] 0 |> string
