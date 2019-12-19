using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DiplomaSurvive.Models;
using DiplomaSurvive.Services;
using FluentScheduler;
using Microsoft.Extensions.Logging;

namespace DiplomaSurvive.Api.Registries
{
    /// <summary>
    /// Scheduling registry.
    /// </summary>
    /// <seealso cref="FluentScheduler.Registry" />
    public class SchedulingRegistry : Registry
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<SchedulingRegistry> _logger;


        private readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        /// <summary>
        /// The database context
        /// </summary>
//        private readonly IMongoDbContext _dbContext;

        /// <summary>
        /// The tournament service
        /// </summary>
        private IEventService _eventService;

        /// <summary>
        /// The settings
        /// </summary>
        private EventDto _settings;

        /// <summary>
        /// The tournament configuration
        /// </summary>
        private EventDto _tournamentConfiguration;

        /// <summary>
        /// Schedules the leaderboards.
        /// </summary>
        /// <returns>
        /// The method is void.
        /// </returns>
        private async Task ScheduleCleaningUpLeaderboardsAsync()
        {
            var oldEvnts = await _eventService.GetOldEventsAsync();

            foreach (var e in oldEvnts)
            {
                _logger.LogInformation("Cleaning up was started for such tournament: {evn}",
                    e.ID);

                await _eventService.ClearEventLeaderboards(e.ID);

                _logger.LogInformation("Cleaning up was finished for such tournament: {evn}",
                    e.ID);
            }
        }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <returns>
        /// The method is void.
        /// </returns>
        private async Task GetEventSettingsAsync()
        {
            var newSettings = await _eventService.GetAsync();

            var needChange = _settings == null || string.CompareOrdinal(JsonSerializer.Serialize(newSettings, options),
                                 JsonSerializer.Serialize(_settings, options)) != 0;

            if (needChange)
            {
                _settings = newSettings;

                _logger.LogInformation("New settings were downloaded.");

                JobManager.RemoveAllJobs();

                _logger.LogInformation("All jobs were cancelled.");

                JobManager.AddJob(() => ScheduleCleaningUpLeaderboardsAsync().Wait(),
                    s => s.WithName(nameof(ScheduleCleaningUpLeaderboardsAsync)).ToRunEvery(1).Days().At(4, 0));
                JobManager.AddJob(() => GetEventSettingsAsync().Wait(),
                    s => s.WithName(nameof(GetEventSettingsAsync)).ToRunEvery(30).Seconds());

                JobManager.AddJob(() => _eventService.FinalizeResults(_settings.ID).Wait(),
                    s => s.WithName($"ExecuteFinalizingAsync({_settings.ID})")
                        .ToRunOnceAt(_settings.Finish.ToDateTime()));

                var names = JobManager.AllSchedules.Select(s => s.Name).ToArray();
                var jobNames = string.Join(", ", names);
                _logger.LogInformation("Such jobs were scheduled: {jobNames}.", jobNames);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SchedulingRegistry" /> class.
        /// </summary>
        /// <param name="eventService">The tournament service.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The database context.</param>
        public SchedulingRegistry(IEventService eventService,
            ILogger<SchedulingRegistry> logger /*,IMongoDbContext dbContext*/)
        {
            _eventService = eventService;
            _logger = logger;
            //_dbContext = dbContext;

            JobManager.UseUtcTime();

            JobManager.AddJob(() => ScheduleCleaningUpLeaderboardsAsync().Wait(),
                s => s.WithName(nameof(ScheduleCleaningUpLeaderboardsAsync)).ToRunEvery(1).Days().At(4, 0));
            JobManager.AddJob(() => GetEventSettingsAsync().Wait(),
                s => s.WithName(nameof(GetEventSettingsAsync)).ToRunEvery(30).Seconds());

            var names = JobManager.AllSchedules.Select(s => s.Name).ToArray();
            var jobNames = string.Join(", ", names);

            _logger.LogInformation("Such jobs were scheduled: {jobNames}.", jobNames);

            ScheduleCleaningUpLeaderboardsAsync().Wait();
            GetEventSettingsAsync().Wait();

            JobManager.JobStart += LogStartInfo;
            JobManager.JobEnd += LogEndInfo;
            JobManager.JobException += job => _logger.LogError(job.Exception, " * Failed: {name}", job.Name);
        }

        public void LogStartInfo(JobStartInfo job)
        {
            if (!job.Name.Contains(nameof(GetEventSettingsAsync)))
            {
                _logger.LogInformation(" + Started: {name}", job.Name);
            }
        }

        public void LogEndInfo(JobEndInfo job)
        {
            if (!job.Name.Contains(nameof(GetEventSettingsAsync)))
            {
                _logger.LogInformation(" - Ended: {name}, was executed: {duration}", job.Name, job.Duration);
            }
        }
    }
}