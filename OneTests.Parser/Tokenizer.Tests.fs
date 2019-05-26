module Tokenizer.Tests

open FsUnit
open Xunit
open One.Parser.OneTokenizer

[<Fact>]
let ``tokenizer produces tokens for classHeader``() =
    let tokens = tokenizer.TryTokenize ("class Test : ITest")
    tokens.HasValue |> should be True
