namespace One.Parser
    
    type OneNode =
        | ClassHeader of string * string list
        | Invariant of (string * string) list