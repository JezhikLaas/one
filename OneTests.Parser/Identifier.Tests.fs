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

let validUpperCaseIdentifierWithUnderscoreIdentifiers =
    seq {
        yield [|"Lower_Case_Identifier_Parser" :> Object|]
        yield [|"Boolean_Literal_Parser" :> Object|]
        yield [|"A_123" :> Object|]
    }

let invalidUpperCaseIdentifierWithUnderscoreIdentifiers =
    seq {
        yield [|"lowerC_aseI_dentifierP_arser" :> Object|]
        yield [|"1Boolean_Literal_Parser" :> Object|]
        yield [|"BooleanLiteralParser" :> Object|]
        yield [|"AA_123" :> Object|]
    }

let validLowerCaseIdentifierWithUnderscoreIdentifiers =
    seq {
        yield [|"lower_case_identifier_parser" :> Object|]
        yield [|"boolean_literal_parser" :> Object|]
        yield [|"a_123" :> Object|]
    }

let invalidLowerCaseIdentifierWithUnderscoreIdentifiers =
    seq {
        yield [|"Lower_CaseI_dentifierP_arser" :> Object|]
        yield [|"1boolean_literal_parser" :> Object|]
        yield [|"BooleanLiteralParser" :> Object|]
        yield [|"A_123" :> Object|]
    }
    
[<Theory>]
[<MemberData("validLowerCaseIdentifiers")>]
let ``lowerCaseIdentifierParser accepts identifiers starting with a lowercase letter``(item : string) =
    lowerCaseIdentifierParser.IsMatch(new TextSpan(item)) |> should be True

[<Theory>]
[<MemberData("validLowerCaseIdentifiers")>]
let ``lowerCaseIdentifierParser yields input as result``(item : string) =
    lowerCaseIdentifierParser.Parse(item) |> should equal item

[<Theory>]
[<MemberData("invalidLowerCaseIdentifiers")>]
let ``lowerCaseIdentifierParser rejects invalid identifiers``(item : string) =
    lowerCaseIdentifierParser.IsMatch(new TextSpan(item)) |> should be False

[<Theory>]
[<MemberData("validUpperCaseIdentifiers")>]
let ``upperCaseIdentifierParser accepts identifiers starting with a lowercase letter``(item : string) =
    upperCaseIdentifierParser.IsMatch(new TextSpan(item)) |> should be True

[<Theory>]
[<MemberData("validUpperCaseIdentifiers")>]
let ``upperCaseIdentifierParser yields input as result``(item : string) =
    upperCaseIdentifierParser.Parse(item) |> should equal item

[<Theory>]
[<MemberData("invalidUpperCaseIdentifiers")>]
let ``upperCaseIdentifierParser rejects invalid identifiers``(item : string) =
    upperCaseIdentifierParser.IsMatch(new TextSpan(item)) |> should be False

[<Theory>]
[<MemberData("validUpperCaseIdentifierWithUnderscoreIdentifiers")>]
let ``upperCaseIdentifierWithUnderscoreParser accepts identifiers starting with a uppercase letter``(item : string) =
    upperCaseWithUnderscoreIdentifierParser.IsMatch(new TextSpan(item)) |> should be True

[<Theory>]
[<MemberData("validUpperCaseIdentifierWithUnderscoreIdentifiers")>]
let ``upperCaseIdentifierWithUnderscoreParser yields input as result``(item : string) =
    upperCaseWithUnderscoreIdentifierParser.Parse(item) |> should equal item

[<Theory>]
[<MemberData("invalidUpperCaseIdentifierWithUnderscoreIdentifiers")>]
let ``upperCaseIdentifierWithUnderscoreParser rejects invalid identifiers``(item : string) =
    upperCaseWithUnderscoreIdentifierParser.IsMatch(new TextSpan(item)) |> should be False

[<Theory>]
[<MemberData("validLowerCaseIdentifierWithUnderscoreIdentifiers")>]
let ``lowerCaseIdentifierWithUnderscoreParser accepts identifiers starting with a lowercase letter``(item : string) =
    lowerCaseWithUnderscoreIdentifierParser.IsMatch(new TextSpan(item)) |> should be True

[<Theory>]
[<MemberData("validLowerCaseIdentifierWithUnderscoreIdentifiers")>]
let ``lowerCaseIdentifierWithUnderscoreParser yields input as result``(item : string) =
    lowerCaseWithUnderscoreIdentifierParser.Parse(item) |> should equal item

[<Theory>]
[<MemberData("invalidLowerCaseIdentifierWithUnderscoreIdentifiers")>]
let ``lowerCaseIdentifierWithUnderscoreParser rejects invalid identifiers``(item : string) =
    lowerCaseWithUnderscoreIdentifierParser.IsMatch(new TextSpan(item)) |> should be False

[<Theory>]
[<MemberData("validLowerCaseIdentifierWithUnderscoreIdentifiers")>]
let ``identifierParser accepts identifiers starting with a lowercase letter containing underscores``(item : string) =
    identifierParser.IsMatch(new TextSpan(item)) |> should be True
    
[<Theory>]
[<MemberData("validLowerCaseIdentifiers")>]
let ``identifierParser accepts identifiers starting with a lowercase letter``(item : string) =
    identifierParser.IsMatch(new TextSpan(item)) |> should be True
    
[<Fact>]
let ``identifierParser accepts aaB``() =
    identifierParser.IsMatch(new TextSpan("aaB")) |> should be True
    
[<Theory>]
[<MemberData("validUpperCaseIdentifiers")>]
let ``identifierParser accepts identifiers starting with a uppercase letter``(item : string) =
    identifierParser.IsMatch(new TextSpan(item)) |> should be True

[<Theory>]
[<MemberData("validUpperCaseIdentifierWithUnderscoreIdentifiers")>]
let ``identifierParser accepts identifiers starting with a uppercase letter containing underscores``(item : string) =
    identifierParser.IsMatch(new TextSpan(item)) |> should be True
