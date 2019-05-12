module FloatingPointLiteral.Tests

open System
open FsUnit
open Xunit
open Superpower
open One.Parser.FloatingPointLiteral
open Superpower.Model

let validFractionalParts =
    seq {
        yield [|"10"|]
        yield [|"123456789"|]
        yield [|"000000001"|]
        yield [|"000010000"|]
        yield [|"0"|]
    }

let validDecimalLiterals =
    seq {
        yield [| "0.0" :> Object |]
        yield [| "1.1" :> Object |]
        yield [| "10.10" :> Object |]
        yield [| "100.100" :> Object |]
        yield [| "1_000.12345" :> Object |]
        yield [| "100_000.1123" :> Object |]
        yield [| "1_000_000.0002" :> Object |]
        yield [| "-1.123" :> Object |]
        yield [| "-10.11" :> Object |]
        yield [| "-100.123" :> Object |]
        yield [| "-1_000.1" :> Object |]
        yield [| "-100_000.321" :> Object |]
        yield [| "-1_000_000.321" :> Object |]
    }

let validFloatLiterals =
    seq {
        for value in validDecimalLiterals do yield [|(value.[0] :?> string + "f") :> Object|]
    }

let validDoubleLiterals =
    seq {
        for value in validDecimalLiterals do yield [|(value.[0] :?> string + "d") :> Object|]
    }

[<Theory>]
[<MemberData("validFractionalParts")>]
let ``fractionalPartParser accepts any digits not only zeros`` (item : string) =
    fractionalPartParser.IsMatch(new TextSpan(item)) |> should be True

[<Theory>]
[<MemberData("validDecimalLiterals")>]
let ``decimalParser produces expected results`` (item : string) =
    decimalLiteralParser.Parse (item) |> should equal (item.Replace("_", ""))

[<Theory>]
[<MemberData("validFloatLiterals")>]
let ``floatParser produces expected results`` (item : string) =
    floatLiteralParser.Parse (item) |> should equal (item.Replace("_", ""))

[<Fact>]
let ``floatTagIsCaseSensitive`` () =
    floatLiteralParser.IsMatch(new TextSpan("1.0F")) |> should be False

[<Fact>]
let ``doubleTagIsCaseSensitive`` () =
    doubleLiteralParser.IsMatch(new TextSpan("1.0D")) |> should be False

[<Theory>]
[<MemberData("validDoubleLiterals")>]
let ``doubleParser produces expected results`` (item : string) =
    doubleLiteralParser.Parse (item) |> should equal (item.Replace("_", ""))
