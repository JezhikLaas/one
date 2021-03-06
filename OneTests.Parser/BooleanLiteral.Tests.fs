module BooleanLiteral.Tests

open System
open FsUnit
open Xunit
open Superpower
open Superpower.Model
open One.Parser.BooleanLiteral

let booleanLiterals =
    seq {
        yield [|"true" :> Object|]
        yield [|"false" :> Object|]
    }

let notBooleanLiterals =
    seq {
        yield [|"10" :> Object|]
        yield [|"xxx" :> Object|]
    }

[<Fact>]

let ``booleanParser accepts true`` () =
    booleanLiteralParser.IsMatch(new TextSpan("true")) |> should be True

[<Fact>]
let ``booleanParser accepts false`` () =
    booleanLiteralParser.IsMatch(new TextSpan("false")) |> should be True

let ``booleanLiteralRecognizer accepts true`` () =
    booleanLiteralRecognizer.IsMatch(new TextSpan("true")) |> should be True

[<Fact>]
let ``booleanLiteralRecognizer accepts false`` () =
    booleanLiteralRecognizer.IsMatch(new TextSpan("false")) |> should be True

[<Theory>]
[<MemberData("booleanLiterals")>]
let ``booleanParser yields expected results`` (item : string) =
    booleanLiteralParser.Parse(item) |> should equal item

[<Theory>]
[<MemberData("notBooleanLiterals")>]
let ``booleanLiteralRecognizer rejects invalid tokens`` (item : string) =
    booleanLiteralRecognizer.IsMatch(new TextSpan(item)) |> should be False
