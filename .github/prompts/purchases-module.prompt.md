# Purchases Module

## Linee guida
Utilizza le linee guida contenute in evenstorming-softwaredesign.prompt.md per interpretare l'immagine con l'esplorazione del dominio del modulo Purchases.
Quelle in muflone-core-prompt.md per la documentazione della libreria Muflone (oltre al codice d'esempio che troverai nel template CrastuArrustutu).

Utilizza l'ultima versione della libreria Muflone (https://www.nuget.org/packages/Muflone) (8.5.0).

## Classi base
In questa fase devi implementare la struttura che ospiterà la businness logic del modulo Purchases. Sempre facendo riferimento al template https://github.com/BrewUp/CrastuArrustutu/tree/main, ora dovrai implementare comandi ed eventi, con i rispettivi handler, utilizzando le classi base che trovi nei progetti CrastuArrustutu.Tannura.Domain per quanto riguarda i CommandHandler, e CrastuArrustutu.Tannura.ReadModel per quanto riguarda gli EvenhHandler.
Utilizza queste classi per implementare i tuoi handler:
- CommandHandlerBaseAsync<TCommand>
- DomainEventHandlerBaseAsync<TEvent>

Utilizza costruttori primari per la definizione di comandi e eventi.  
Utilizza costruttori primari ovunque sia possibile.

## Specification Test
Prepara, nel progetto BrewUp.Purchase.Test i test di dominio utilizzando la libreria Muflone.Specifications (https://www.nuget.org/packages/Muflone.SpecificationTests).
Prepara la struttura dei test, che ovviamente falliranno, nella cartella Entities del progetto.
Non scrivere il codice all'interno dei metdodi del test. Limitati a preparare la struttura dei test con i metodi Given, When, Then vuoti.
Segui l'esempio del progetto https://github.com/BrewUp/CrastuArrustutu (che puoi clonare) che trovi nel progetto CrastuArrustutu.Tannura.Tests all'interno della cartella Entities.

## Aggregati
Per l'implementazione degli aggregati utilizza la classe base AggregateRoot che trovi nella libreria Muflone. 
L'aggregato deve avere un costruttore protected senza parametri.
Deve contenere un metodo statico Create per l'inizializzazione dell'aggregato.
Il costruttore con parametri deve essere private e chiamato dal metodo statico Create.
Il costruttore privato è responsabile di invocare il metodo RaiseEvent che crea l'evento di dominio associato all'operazione.
Per le entity puoi utilizzare la classe base Entity che trovi sempre nella libreria Muflone.

## Comandi ed Eventi
Utilizza le classi basi che trovi in Muflone per i comand (Command) e per gli eventi (DomainEvent).
Anche in questo caso utilizza costruttori primari per la definizione di comandi e eventi.
Crea comandi ed eventi nel progetto BrewUp.Purchases.SharedKernel nella cartella Messages, e poi rispettivamente nelle sottocartelle Commands e Events.

## Naming Convention
Non utilizzare i suffissi Command e DomainEvent per le classi dei comandi e degli eventi. Utilizza invece i suffissi CommandHandler e EventHandler per le classi degli handler.

## Infrastruttura
Per l'infrastruttura utilizza il package Muflone.Eventstore.gRPC, che implementa l'interfaccia IRepository per te. Trovi il package qui https://www.nuget.org/packages/Muflone.Eventstore.gRPC e il modo in cui implementarlo nel progetto CrastuArrustutu.Infrastructure. In questo progetto trovi l'estensione per la dependency injection (InfrastructureHelper) che registra il repository di Muflone con i relativi settings (EventStoreSettings.cs).

