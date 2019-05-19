module Identifier.Tests

open System
open FsUnit
open Superpower
open Superpower.Model
open Xunit
open One.Parser.Identifier

let validLowerCaseIdentifiers =
    seq {
        yield [|"lowerCaseIdentifierParser" :> Object|]
        yield [|"booleanLiteralParser" :> Object|]
        yield [|"a123" :> Object|]
    }

let invalidLowerCaseIdentifiers =
    seq {
        yield [|"LowerCaseIdentifierParser" :> Object|]
        yield [|"1booleanLiteralParser" :> Object|]
        yield [|"a_123" :> Object|]
    }

let validUpperCaseIdentifiers =
    seq {
        yield [|"LowerCaseIdentifierParser" :> Object|]
        yield [|"BooleanLiteralParser" :> Object|]
        yield [|"A123" :> Object|]
    }

let invalidUpperCaseIdentifiers =
    seq {
        yield [|"lowerCaseIdentifierParser" :> Object|]
        yield [|"1BooleanLiteralParser" :> Object|]
        yield [|"A_123" :> Object|]
    }

[<Theory>]
[<MemberData("validLowerCaseIdentifiers")>]
let ``lowerCaseIdentifierParser accepts identifiers starting with a lowercase letter``(item : string) =
    lowerCaseIdentifierParser.IsMatch(new TextSpan(item)) |> should be True

[<Theory>]
[<MemberData("invalidLowerCaseIdentifiers")>]
let ``lowerCaseIdentifierParser rejects invalid identifiers``(item : string) =
    lowerCaseIdentifierParser.IsMatch(new TextSpan(item)) |> should be False

[<Theory>]
[<MemberData("validUpperCaseIdentifiers")>]
let ``upperCaseIdentifierParser accepts identifiers starting with a lowercase letter``(item : string) =
    upperCaseIdentifierParser.IsMatch(new TextSpan(item)) |> should be True

[<Theory>]
[<MemberData("invalidUpperCaseIdentifiers")>]
let ``upperCaseIdentifierParser rejects invalid identifiers``(item : string) =
    upperCaseIdentifierParser.IsMatch(new TextSpan(item)) |> should be False
