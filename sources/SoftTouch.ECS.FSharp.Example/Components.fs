module Components


[<Struct>]
type Machin =
    val Value : int
    new (v : int) = {Value = v}