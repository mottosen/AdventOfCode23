namespace AdventOfCode

open System
open System.Net.Http
open System.IO

exception FileException of string

type InputLoader() =
    static member GetInputFromFile (path : string) : string[] =
        try
            IO.File.ReadAllLines(path)
        with
        | _ -> raise (FileException $"Error reading file: {path}")