namespace BrewUp.Shared.FSharp.Exceptions

open System

type BrewUpException(message: string, innerException: Exception option) =
    inherit Exception(message, defaultArg innerException null)
    
    new(message: string) = BrewUpException(message, None)

type EntityNotFoundException(message: string, innerException: Exception option) =
    inherit BrewUpException(message, innerException)
    
    new(message: string) = EntityNotFoundException(message, None)