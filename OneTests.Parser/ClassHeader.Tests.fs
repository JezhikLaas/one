module ClassHeader.Tests

open FsUnit
open Xunit
open Superpower
open One.Parser.OneTokenizer
open One.Parser.TokenListParsers

[<Fact>]
let ``ancestorList recognizes comma separated list``() =
    let tokens = tokenizer.TryTokenize ("IAtest, IBtest")
    let parsed = ancestorList.TryParse (tokens.Value)
    parsed.HasValue |> should be True

[<Fact>]
let ``ancestorList recognizes single ancestor``() =
    let tokens = tokenizer.TryTokenize ("IAtest")
    let parsed = ancestorList.TryParse (tokens.Value)
    parsed.HasValue |> should be True

[<Fact>]
let ``classHeader recognizes class with one ancestor``() =
    let tokens = tokenizer.TryTokenize ("class Test : ITest")
    let parsed = classHeader.TryParse (tokens.Value)
    parsed.HasValue |> should be True

[<Fact>]
let ``classHeader rejects class without ancestor``() =
    let tokens = tokenizer.TryTokenize ("class Test")
    let parsed = classHeader.TryParse (tokens.Value)
    parsed.HasValue |> should be False
