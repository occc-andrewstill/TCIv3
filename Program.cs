using Topshelf;
using System.Configuration;
using TCIv3.BLL;

namespace TCIv3
{
    class Program
    {
        static void Main(string[] args)
        {
            if (ConfigurationManager.AppSettings["Environment"] == "Local")
            {
                TaskManager tm = new TaskManager();
                tm.Run();
            }
            else
            {
                HostFactory.Run(x =>
                {
                    x.Service<ServiceManager>(s =>
                    {
                        s.ConstructUsing(FileProcess => new ServiceManager());
                        s.WhenStarted(FileProcess => FileProcess.Start());
                        s.WhenStopped(FileProcess => FileProcess.Stop());
                    });

                    x.StartAutomatically();

                    x.RunAsLocalSystem();

                    x.EnableServiceRecovery(recoverOption =>
                    {
                        recoverOption.RestartService(1);
                        recoverOption.RestartService(5);
                        recoverOption.TakeNoAction();
                    });

                    x.SetServiceName("OCCC_CitationImport_v3");
                    x.SetDisplayName("OCCC Traffic Citation Import v3");
                    x.SetDescription("This service imports citations from various agencies into Odyssey.");
                });
            }
        }
    }
}
