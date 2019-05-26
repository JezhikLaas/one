module One.Parser.BooleanLiteral

    open Superpower
    open Superpower.Model
    open Superpower.Parsers
    
    let falseParser =
        Span.EqualTo("false").Value("false")    
    
    let falseRecognizer =
        Span.EqualTo("false").Value(Unit.Value)
    
    let trueParser =
        Span.EqualTo("true").Value("true")
    
    let trueRecognizer =
        Span.EqualTo("true").Value(Unit.Value)
    
    let booleanLiteralParser =
        falseParser.Try().Or(trueParser)    

    let booleanLiteralRecognizer =
        falseRecognizer.Try().Or(trueRecognizer)