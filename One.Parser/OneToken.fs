namespace One.Parser

    type OneToken =
        StringLiteral
        | DecimalLiteral
        | DoubleLiteral
        | FloatLiteral
        | BooleanLiteral
        | LowerCaseIdentifier
        | UpperCaseIdentifier
        | UpperCaseWithUnderscoreIdentifier