namespace BrewUp.Shared.FSharp.Validation

open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open FluentValidation

type ValidationHandler() =
    let mutable isValid = true
    let mutable errors = Dictionary<string, string array>()
    
    member this.IsValid = isValid
    member this.Errors = errors
    
    member this.ValidateAsync<'T when 'T : not struct>(validator: IValidator<'T>, validateMe: 'T) : Task =
        async {
            let! validationResult = validator.ValidateAsync(validateMe) |> Async.AwaitTask
            
            if validationResult.IsValid then
                errors <- Dictionary<string, string array>()
                return ()
            else
                let grouped = 
                    validationResult.Errors
                    |> Seq.groupBy (fun e -> e.PropertyName)
                    |> Seq.map (fun (key, values) -> 
                        KeyValuePair(key, values |> Seq.map (fun e -> e.ErrorMessage) |> Array.ofSeq))
                    
                errors <- Dictionary<string, string array>(grouped)
                isValid <- false
        } |> Async.StartAsTask :> Task
    
    member this.ValidateQueryString(queryString: string seq) =
        let errorDict = Dictionary<string, string array>()
        isValid <- true
        
        // Add query string validation logic here if needed
        errors <- errorDict