using BrewUp.InMemoryBroker;
using BrewUp.Shared.Domain;
using BrewUp.Warehouse.Entities.Entities;
using BrewUp.Warehouse.Infrastructure;
using BrewUp.Warehouse.Infrastructure.Repository;
using KellermanSoftware.CompareNetObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;

namespace BrewUp.Warehouse.Tests.Entities;

public abstract class WarehouseCommandSpecification<TCommand> where TCommand : Command
	{
		protected Exception ExpectedException { get; set; } = null!;
		protected readonly Func<IServiceProvider, IBrewUpRepository<Product>> RepositoryFactory;
		protected readonly IBrewUpRepository<Product> Repository;
		protected readonly Dictionary<Type, object> CommandHandlers = new();
		protected readonly Dictionary<object, Command> CommandsToExecute = new();

		protected WarehouseCommandSpecification()
		{
			ServiceCollection services = [];
			services.AddInMemoryBroker();
			services.AddLogging();
    
			var builder = new DbContextOptionsBuilder<WarehouseContext>();
			builder.UseInMemoryDatabase("BrewUp");
			var options = builder.Options;

			// Create the factory function
			RepositoryFactory = provider =>
			{
				var context = new WarehouseContext(options);
				var eventBus = provider.GetRequiredService<IEventBus>();
				var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
				return new ProductRepository(context, eventBus, loggerFactory);
			};

			// Register the factory with DI
			services.AddScoped(RepositoryFactory);
    
			// Build the service provider
			ServiceProvider serviceProvider = services.BuildServiceProvider();
    
			// Create an actual instance for direct use in tests
			Repository = RepositoryFactory(serviceProvider);
		}
		
		protected void RegisterHandler<TSpecificCommand>(ICommandHandlerAsync<TSpecificCommand> handler, 
			TSpecificCommand command) 
			where TSpecificCommand : Command
		{
			CommandHandlers[typeof(TSpecificCommand)] = handler;
        
			if (command != null)
				CommandsToExecute[handler] = command;
		}

		private async Task ExecuteInitialCommands()
		{
			foreach (var (handler, command) in CommandsToExecute)
			{
				var handlerType = handler.GetType();
				var handleAsyncMethod = handlerType.GetMethod("HandleAsync");

				if (handleAsyncMethod == null) 
					continue;
				
				var task = (Task)handleAsyncMethod.Invoke(handler, [command, CancellationToken.None])!;
				await task;
			}
		}

		/// <summary>
		///   The list of events that represent the initial status of the aggregate root under test
		/// </summary>
		/// <returns></returns>
		protected abstract IEnumerable<DomainEvent> Given();

		/// <summary>
		///   The command that that triggers the events
		/// </summary>
		/// <returns></returns>
		protected abstract TCommand When();

		/// <summary>
		///   Returns the instance of the command handler
		/// </summary>
		/// <returns></returns>
		protected abstract ICommandHandlerAsync<TCommand> OnHandler();

		/// <summary>
		///   The list of events that should be compared to the ones emitted by the aggregate root
		/// </summary>
		/// <returns></returns>
		protected abstract IEnumerable<DomainEvent> Expect();

		private static void CompareEvents(IEnumerable<DomainEvent> expected, IEnumerable<DomainEvent> published)
		{
			if (published == null)
				published = new List<DomainEvent>();

			var expectedArray = expected as DomainEvent[] ?? expected.ToArray();
			var publishedArray = published as DomainEvent[] ?? published.ToArray();
			Assert.True(expectedArray.Length == publishedArray.Length, "Different number of expected/published events.");

			var config = new ComparisonConfig();
			config.MembersToIgnore.Add("Headers");
			config.MembersToIgnore.Add("MessageId");

			var compareObjects = new CompareLogic(config);
			var eventPairs = expectedArray.Zip(publishedArray, (e, p) => new { Expected = e, Published = p });
			foreach (var eventPair in eventPairs)
			{
				var result = compareObjects.Compare(eventPair.Expected, eventPair.Published);
				Assert.True(result.AreEqual,
					$"Events {eventPair.Expected.GetType()} and {eventPair.Published.GetType()} are different: {result.DifferencesString}");
			}
		}

		[Fact]
		public async Task SetUp()
		{
			var handler = OnHandler();
			try
			{
				await ExecuteInitialCommands();
				await handler.HandleAsync(When());
				var expected = Expect().ToList();
				var published = Repository.GetUncommittedEvents();
				CompareEvents(expected, published);
			}
			catch (Exception exception) //Otherwise should be something expected
			{
				if (ExpectedException == null)
					Assert.Fail($"{exception.GetType()}: {exception.Message}\n{exception.StackTrace}");
				Assert.True(exception.GetType() == ExpectedException.GetType(),
					$"Exception type {exception.GetType()} differs from expected type {ExpectedException.GetType()}");
				Assert.True(exception.Message == ExpectedException.Message,
					$"Exception message \"{exception.Message}\" differs from expected message \"{ExpectedException.GetType()}\"");
			}
		}
	}