//using System.Threading;
//using System.Threading.Tasks;
//using Microsoft.Extensions.Hosting;
//
//namespace DiplomaSurvive.Services
//{
//    /// <summary>
//    /// Event finalizer service
//    /// </summary>
//    /// <seealso cref="Microsoft.Extensions.Hosting.BackgroundService" />
//    public class EventFinalizerService : BackgroundService
//    {
//        /// <summary>
//        /// Gets or sets the tournament service.
//        /// </summary>
//        /// <value>
//        /// The tournament service.
//        /// </value>
//       private IEventService _eventService { get; set; }
//
//        /// <summary>
//        /// Initializes a new instance of the <see cref="EventFinalizerService"/> class.
//        /// </summary>
//        /// <param name="tournamentService">The tournament service.</param>
//        public EventFinalizerService(IEventService eventService)
//        {
//            _eventService = eventService;
//        }
//
//        /// <summary>
//        /// This method is called when the <see cref="T:Microsoft.Extensions.Hosting.IHostedService" /> starts. The implementation should return a task that represents
//        /// the lifetime of the long running operation(s) being performed.
//        /// </summary>
//        /// <param name="stoppingToken">Triggered when <see cref="M:Microsoft.Extensions.Hosting.IHostedService.StopAsync(System.Threading.CancellationToken)" /> is called.</param>
//        /// <returns>
//        /// A <see cref="T:System.Threading.Tasks.Task" /> that represents the long running operations.
//        /// </returns>
//        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//        {
//           await _eventService.FinalizeResults();
//           await Task.CompletedTask;
//        }
//    }
//}