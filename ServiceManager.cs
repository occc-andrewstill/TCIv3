using System;
using System.Collections.Generic;
using System.Timers;
using System.Configuration;
using NLog;
using System.Linq;
using TCIv3.BLL;
namespace TCIv3
{
    public class ServiceManager
    {
        private readonly Timer _timer;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ServiceManager()
        {
            logger.Info("Service manager start");

            ConfigurationManager.RefreshSection("appSettings");
            ConfigurationManager.RefreshSection("connectionStrings");

            //Converts TimerInterval from app.config into minutes. Timer elapses every (TimerInterval) minutes.
            Double timerInterval = (Convert.ToDouble(ConfigurationManager.AppSettings["TimerInterval"]) * 1000 * 60);

            _timer = new Timer(timerInterval) { AutoReset = true };
            _timer.Elapsed += TimerElapsed;
            logger.Info("Service manager end");
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            TaskManager tm = new TaskManager();
            tm.Run();
        }

        public void Start()
        {
            logger.Debug("Start service");
            _timer.Start();
        }

        public void Stop()
        {
            logger.Debug("Stop service");
            _timer.Stop();
        }
    }
}
