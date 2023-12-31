// https://www.compositional-it.com/news-blog/a-quick-look-at-argu/

open Argu
open System



type CliArguments =
    | Name of string

    interface IArgParserTemplate with
        member this.Usage =
            match this with
            | Name _ -> "The name of the entity to be greeted."



let parseName name =
    if Seq.forall Char.IsLetter name 
    then name
    else failwith "Name can only contain letters."

[<EntryPoint>]
let main args =
    let result = 
        ArgumentParser
            .Create<CliArguments>(
                errorHandler = ProcessExiter(None)
            )
            .ParseCommandLine(args)

    let name = 
        result.TryPostProcessResult(<@ Name @>, parseName)
        |> Option.defaultValue "World"

    printfn $"Hello, {name}!"
    0