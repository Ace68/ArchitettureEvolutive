using Muflone.Messages.Commands;

namespace BrewUp.Sales.Domain.CommandHandlers;

public abstract class CommandHandlerBaseAsync<TCommand>() : ICommandHandlerAsync<TCommand> where TCommand : Command
{
    public abstract Task HandleAsync(TCommand command, CancellationToken cancellationToken = new());
    
    #region Dispose

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
        }
    }


    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }


    ~CommandHandlerBaseAsync()
    {
        Dispose(false);
    }

    #endregion
}