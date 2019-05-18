module StringLiteral.Tests

open System
open FsUnit
open Xunit
open Superpower
open One.Parser.StringLiteral
open Superpower.Model

let escapeSequenceLiterals =
    seq {
        yield [|@"\""" :> Object|]
        yield [|@"\\" :> Object|]
        yield [|@"\b" :> Object|]
        yield [|@"\t" :> Object|]
        yield [|@"\u1234" :> Object|]
    }

let invalidEscapeSequenceLiterals =
    seq {
        yield [|@"\ü" :> Object|]
        yield [|@"\ä" :> Object|]
        yield [|@"\u12" :> Object|]
        yield [|@"\q" :> Object|]
        yield [|@"\u123" :> Object|]
    }

let stringLiterals = 
    seq {
        yield [|"\"Hello world\"" :> Object|]
        yield [|"\"Hello \\\"world\\\"\"" :> Object|]
        yield [|"\"Hello \n world\"" :> Object|]
        yield [|"\"\n\n\n\"" :> Object|]
        yield [|"\"\"" :> Object|]
    }

let invalidStringLiterals = 
    seq {
        yield [|"\"Hello world" :> Object|]
        yield [|"\"Hello \ü world\"" :> Object|]
        yield [|"\"\u12\"" :> Object|]
    }

[<Theory>]
[<MemberData("escapeSequenceLiterals")>]
let ``esacapeSequenceParser yields expected results`` (item : string) =
    esacapeSequenceParser.Parse(item) |> should equal item

[<Theory>]
[<MemberData("invalidEscapeSequenceLiterals")>]
let ``esacapeSequenceParser rejects invalid escape sequences`` (item : string) =
    esacapeSequenceParser.IsMatch(new TextSpan(item)) |> should be False

[<Theory>]
[<MemberData("invalidStringLiterals")>]
let ``stringLiteralParser rejects invalid strings`` (item : string) =
    stringLiteralParser.IsMatch(new TextSpan(item)) |> should be False

[<Theory>]
[<MemberData("stringLiterals")>]
let ``stringLiteralParser yields expected results`` (item : string) =
    stringLiteralParser.Parse(item) |> should equal item
