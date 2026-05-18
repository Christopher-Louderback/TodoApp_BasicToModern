using Quartz;
using Todo.Services.Jobs;
using Microsoft.Extensions.Configuration;

namespace Todo.API.Extensions
{
    public static class QuartzServiceExtensions
    {
        public static IServiceCollection AddQuartzConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddQuartz(q =>
            {
                // in memory to clustered changes
                q.UsePersistentStore(s =>
                {
                    s.UseProperties = true;
                    s.UseClustering(c =>
                    {
                        c.CheckinMisfireThreshold = TimeSpan.FromSeconds(20);
                        c.CheckinInterval = TimeSpan.FromSeconds(10);
                    });

                    //Worth looking into switch to Newtonsoft later or if issues arise
                    s.UseJsonSerializer();
                    s.UseMySql(sql =>
                    {
                        sql.ConnectionString = configuration.GetConnectionString("DefaultConnection")!;
                        sql.TablePrefix = "QRTZ_";
                    });
                });

                q.UseDefaultThreadPool(tp =>
                {
                    tp.MaxConcurrency = 3;
                });

                var dailyReportJobKey = new JobKey("DailyTaskReportJob", "EmailJobs");
                var dailyReportTrigger = new TriggerKey("DailyReportTrigger", "EmailJobs");
                q.AddJob<DailyTodoItemReportJob>(opts => opts
                    .WithIdentity(dailyReportJobKey)
                    .WithDescription("Send daily task report email at 6:00 PM"));

                q.AddTrigger(opts => opts
                    .ForJob(dailyReportJobKey)
                    .WithIdentity(dailyReportTrigger)
                    .WithCronSchedule("0 0 18 * * ?") // 6:00 PM mỗi ngày
                    .WithDescription("Trigger for daily report at 6:00 PM"));

                var weeklySummaryJobKey = new JobKey("WeeklyTaskSummaryJob", "EmailJobs");
                var weeklySummaryReportTrigger = new TriggerKey("WeeklySummaryTrigger", "EmailJobs");
                q.AddJob<WeeklyTaskSummaryJob>(opts => opts
                    .WithIdentity(weeklySummaryJobKey)
                    .WithDescription("Send weekly task summary every Monday at 9:00 AM"));

                q.AddTrigger(opts => opts
                    .ForJob(weeklySummaryJobKey)
                    .WithIdentity(weeklySummaryReportTrigger)
                    .WithCronSchedule("0 0 9 ? * MON") // 9:00 am thứ 2
                    .WithDescription("Trigger for weekly summary every Monday"));

                var reminderJobKey = new JobKey("TaskReminderJob", "EmailJobs");
                var reminderReportTrigger = new TriggerKey("ReminderTrigger", "EmailJobs");
                q.AddJob<TaskReminderJob>(opts => opts
                    .WithIdentity(reminderJobKey)
                    .WithDescription("Send task reminder every morning at 8:00 AM"));

                q.AddTrigger(opts => opts
                    .ForJob(reminderJobKey)
                    .WithIdentity(reminderReportTrigger)
                    .WithCronSchedule("0 0 8 * * ?") // 8:00 am mỗi ngày
                    .WithDescription("Trigger for morning task reminder"));
            });

            services.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = true;
                options.AwaitApplicationStarted = true;
            });

            return services;
        }
    }
}
