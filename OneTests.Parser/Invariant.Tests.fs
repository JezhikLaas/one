module Invariant.Tests

open FsUnit
open Xunit
open Superpower
open One.Parser.OneTokenizer
open One.Parser.TokenListParsers.Invariant

[<Fact>]
let ``booleanExpression recognizes boolean literal true``() =
    let tokens = tokenizer.TryTokenize ("true")
    let parsed = booleanExpression.TryParse (tokens.Value)
    parsed.HasValue |> should be True
    parsed.Value |> should equal "true"

[<Fact>]
let ``booleanExpression recognizes boolean literal false``() =
    let tokens = tokenizer.TryTokenize ("false")
    let parsed = booleanExpression.TryParse (tokens.Value)
    parsed.HasValue |> should be True
    parsed.Value |> should equal "false"

[<Fact>]
let ``taggedExpressionList recognizes tagged clauses``() =
    let tokens = tokenizer.TryTokenize ("valid_value: true")
    let parsed = taggedExpressionList.TryParse (tokens.Value)
    parsed.HasValue |> should be True
    parsed.Value |> should equal [("valid_value", "true")]

[<Fact>]
let ``taggedExpressionList recognizes semicolon separated tagged clauses``() =
    let tokens = tokenizer.TryTokenize ("valid_value: true; valid_value2: true")
    let parsed = taggedExpressionList.TryParse (tokens.Value)
    parsed.HasValue |> should be True
    parsed.Value |> should equal [("valid_value", "true"); ("valid_value2", "true")]

[<Fact>]
let ``taggedExpressionList recognizes linefeed separated tagged clauses``() =
    let tokens = tokenizer.TryTokenize ("valid_value: true\nvalid_value2: true")
    let parsed = taggedExpressionList.TryParse (tokens.Value)
    parsed.HasValue |> should be True
    parsed.Value |> should equal [("valid_value", "true"); ("valid_value2", "true")]

[<Fact>]
let ``taggedExpressionList rejects space separated tagged clauses``() =
    let tokens = tokenizer.TryTokenize ("valid_value: true valid_value2: true")
    let parsed = taggedExpressionList.TryParse (tokens.Value)
    parsed.HasValue |> should be True
    parsed.Value |> should equal [("valid_value", "true")]

[<Fact>]
let ``invariant recognizes invariant clause with literal expression``() =
    let tokens = tokenizer.TryTokenize ("invariant\nvalid_value: true")
    let parsed = invariant.TryParse (tokens.Value)
    parsed.HasValue |> should be True
    
