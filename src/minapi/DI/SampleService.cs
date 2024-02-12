namespace wsl_and_docker.DI
{
    public interface ISampleService
    {
        void DoSomething();
    }

    public class SampleService : ISampleService
    {
        public void DoSomething()
        {
            Console.WriteLine("SampleService doing something and writes to console");
        }
    }
}
