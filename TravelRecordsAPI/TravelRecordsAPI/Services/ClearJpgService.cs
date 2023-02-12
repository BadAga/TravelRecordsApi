namespace TravelRecordsAPI.Services
{
    public class ClearJpgService:BackgroundService
    {

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;

            foreach (string sFile in System.IO.Directory.GetFiles(workingDirectory, "*.jpg"))
            {
                try
                {
                    System.IO.File.Delete(sFile);
                }
                catch (IOException ioExp)
                {

                }
            }
            return Task.FromResult(0);
        } 
    }
}
