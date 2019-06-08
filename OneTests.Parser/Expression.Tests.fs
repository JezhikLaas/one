module Expression.Tests

open FsUnit
open Xunit
open Superpower
open One.Parser
open One.Parser.OneTokenizer
open One.Parser.TokenListParsers.Expression

[<Fact>]
let ``nonObjectCall recognizes static call``() =
    let tokens = tokenizer.TryTokenize("{String}.Empty")
    let parsed = nonObjectCall.TryParse(tokens.Value)
    parsed.Value |> should equal (StaticCall ("String", "Empty"))
