module public IntegerLiteral.Tests

open System
open FsUnit
open Xunit
open Superpower
open Superpower.Model
open One.Parser.IntegerLiteral

let numberCharsWithoutZero =
    seq { for value in ['1'..'9'] do yield [|value :> Object|] }

let upToThreeDigitsWithoutLeadingZero =
    seq {
        yield [| "1" :> Object |]
        yield [| "12" :> Object |]
        yield [| "123" :> Object |]
    }

let exactlyThreeDigits =
    seq {
        yield [| "000" :> Object |]
        yield [| "001" :> Object |]
        yield [| "010" :> Object |]
        yield [| "100" :> Object |]
    }

let validFollowUpTokens =
    seq {
        yield [| "_000" :> Object |]
        yield [| "_001" :> Object |]
        yield [| "_010" :> Object |]
        yield [| "_100" :> Object |]
    }

let invalidFollowUpTokens =
    seq {
        yield [| "000" :> Object |]
        yield [| "001_" :> Object |]
        yield [| "_010_" :> Object |]
        yield [| "_a01" :> Object |]
        yield [| "_0011" :> Object |]
    }

let validNotZeroIntegerLiterals =
    seq {
        yield [| "1" :> Object |]
        yield [| "10" :> Object |]
        yield [| "100" :> Object |]
        yield [| "1_000" :> Object |]
        yield [| "100_000" :> Object |]
        yield [| "1_000_000" :> Object |]
    }

let invalidNotZeroIntegerLiterals =
    seq {
        yield [| "0" :> Object |]
        yield [| "1000" :> Object |]
        yield [| "_000" :> Object |]
        yield [| "100_" :> Object |]
        yield [| "01_000_000" :> Object |]
    }

let validIntegerLiterals =
    seq {
        yield [| "0" :> Object |]
        yield [| "1" :> Object |]
        yield [| "10" :> Object |]
        yield [| "100" :> Object |]
        yield [| "1_000" :> Object |]
        yield [| "100_000" :> Object |]
        yield [| "1_000_000" :> Object |]
        yield [| "-1" :> Object |]
        yield [| "-10" :> Object |]
        yield [| "-100" :> Object |]
        yield [| "-1_000" :> Object |]
        yield [| "-100_000" :> Object |]
        yield [| "-1_000_000" :> Object |]
    }

let allCharactersExceptZero =
    seq {
        for character in ['1' .. 'z'] do yield [| character.ToString() :> Object |]
    }

[<Theory>]
[<MemberData("numberCharsWithoutZero")>]
let ``notZeroDigitParser accepts digits w/o zero`` (item : char) =
    notZeroDigitParser.IsMatch(new TextSpan(item.ToString())) |> should be True

[<Fact>]
let ``notZeroDigitParser rejects zero`` () =
    notZeroDigitParser.IsMatch(new TextSpan('0'.ToString())) |> should be False

[<Fact>]
let ``twoDigitParserWithoutLeadingZeroParser accepts two digits w/o leading zero`` () =
    twoDigitParserWithoutLeadingZeroParser.IsMatch(new TextSpan("10")) |> should be True

[<Fact>]
let ``twoDigitParserWithoutLeadingZeroParser rejects two digits with leading zero`` () =
    twoDigitParserWithoutLeadingZeroParser.IsMatch(new TextSpan("01")) |> should be False

[<Fact>]
let ``threeDigitParserWithoutLeadingZeroParser accepts three digits w/o leading zero`` () =
    threeDigitParserWithoutLeadingZeroParser.IsMatch(new TextSpan("100")) |> should be True

[<Fact>]
let ``threeDigitParserWithoutLeadingZeroParser produces expected results`` () =
    threeDigitParserWithoutLeadingZeroParser.Parse("100") |> should equal "100"

[<Fact>]
let ``threeDigitParserWithoutLeadingZeroParser rejects three digits with leading zero`` () =
    threeDigitParserWithoutLeadingZeroParser.IsMatch(new TextSpan("011")) |> should be False

[<Theory>]
[<MemberData("upToThreeDigitsWithoutLeadingZero")>]
let ``leftMostThreeDigitGroupParser accepts up to three digits w/o leading zero`` (item : string) =
    leftMostThreeDigitGroupParser.IsMatch(new TextSpan(item)) |> should be True

[<Theory>]
[<MemberData("upToThreeDigitsWithoutLeadingZero")>]
let ``leftMostThreeDigitGroupParser produces expected results`` (item : string) =
    leftMostThreeDigitGroupParser.Parse(item) |> should equal item

[<Fact>]
let ``leftMostThreeDigitGroupParser rejects tokens with more than 3 digits`` () =
    leftMostThreeDigitGroupParser.IsMatch(new TextSpan("1234")) |> should be False

[<Fact>]
let ``leftMostThreeDigitGroupParser rejects tokens with leading zero`` () =
    leftMostThreeDigitGroupParser.IsMatch(new TextSpan("012")) |> should be False

[<Fact>]
let ``leftMostThreeDigitGroupParser rejects empty tokens`` () =
    (fun () -> leftMostThreeDigitGroupParser.IsMatch(new TextSpan("")) |> ignore) |> should throw typeof<ArgumentException>

[<Theory>]
[<MemberData("exactlyThreeDigits")>]
let ``threeDigitGroupParser accepts any three digits`` (item : string) =
    threeDigitGroupParser.IsMatch(new TextSpan(item)) |> should be True

[<Theory>]
[<MemberData("exactlyThreeDigits")>]
let ``threeDigitGroupParser produces expected results`` (item : string) =
    threeDigitGroupParser.Parse(item) |> should equal item

[<Theory>]
[<MemberData("validFollowUpTokens")>]
let ``threeDigitFollowUpParser accepts vald follow up tokens`` (item : string) =
    threeDigitFollowUpParser.IsMatch(new TextSpan(item)) |> should be True

[<Theory>]
[<MemberData("invalidFollowUpTokens")>]
let ``threeDigitFollowUpParser rejects vald follow up tokens`` (item : string) =
    threeDigitFollowUpParser.IsMatch(new TextSpan(item)) |> should be False

[<Theory>]
[<MemberData("validNotZeroIntegerLiterals")>]
let ``notZeroIntegerParser accepts all vald integer literals`` (item : string) =
    notZeroIntegerParser.IsMatch(new TextSpan(item)) |> should be True

[<Theory>]
[<MemberData("validNotZeroIntegerLiterals")>]
let ``notZeroIntegerParser produces expected result`` (item : string) =
    notZeroIntegerParser.Parse(item) |> should equal (item.Replace("_", ""))

[<Theory>]
[<MemberData("invalidNotZeroIntegerLiterals")>]
let ``notZeroIntegerParser rejects all invald integer literals`` (item : string) =
    notZeroIntegerParser.IsMatch(new TextSpan(item)) |> should be False

[<Fact>]
let ``zeroIntegerParser accepts zero literal`` () =
    zeroIntegerParser.IsMatch(new TextSpan("0")) |> should be True

[<Fact>]
let ``zeroIntegerParser produces expected result`` () =
    zeroIntegerParser.Parse("0") |> should equal "0"

[<Theory>]
[<MemberData("allCharactersExceptZero")>]
let ``zeroIntegerParser rejects all literals except zero`` (item : string) =
    zeroIntegerParser.IsMatch(new TextSpan(item)) |> should be False

[<Theory>]
[<MemberData("validIntegerLiterals")>]
let ``integerParser accepts all valid literals`` (item : string) =
    integerLiteralParser.IsMatch(new TextSpan(item)) |> should be True

[<Theory>]
[<MemberData("validIntegerLiterals")>]
let ``integerParser produces expected results`` (item : string) =
    integerLiteralParser.Parse (item) |> should equal (item.Replace("_", ""))

[<Fact>]
let ``integerParser rejects minus zero`` () =
    zeroIntegerParser.IsMatch(new TextSpan("-0")) |> should be False