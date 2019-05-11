module IntegerLiteral.Tests

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
let ``threeDigitParserWithoutLeadingZeroParser accepts two digits w/o leading zero`` () =
    threeDigitParserWithoutLeadingZeroParser.IsMatch(new TextSpan("100")) |> should be True

[<Fact>]
let ``threeDigitParserWithoutLeadingZeroParser rejects two digits with leading zero`` () =
    threeDigitParserWithoutLeadingZeroParser.IsMatch(new TextSpan("011")) |> should be False

[<Theory>]
[<MemberData("upToThreeDigitsWithoutLeadingZero")>]
let ``leftMostThreeDigitGroupParser accepts up to three digits w/o leading zero`` (item : string) =
    leftMostThreeDigitGroupParser.IsMatch(new TextSpan(item)) |> should be True

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
let ``threeDigitGroupParser`` accepts any three digits (item : string) =
    threeDigitGroupParser.IsMatch(new TextSpan(item)) |> should be True
