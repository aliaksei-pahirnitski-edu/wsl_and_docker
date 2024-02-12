using System.Collections;
using System.Diagnostics;
using System.IO;
using wsl_and_docker.DI;

namespace wsl_and_docker.MemCpu
{
    public class CpuMemEndpoint
    {
        private readonly ISampleService _sampleService;

        public CpuMemEndpoint(ISampleService sampleService)
        {
            _sampleService = sampleService;
        }

        public string DoCalc(int? n)
        {
            Console.WriteLine($"[{DateTime.Now:hh:mm:ss}] {nameof(DoCalc)} n=[{n}]");

            var sw = Stopwatch.StartNew();
            _sampleService.DoSomething();
            var result = n is null ? CalculateValue() : CalculateValue(n.Value);
            sw.Stop();
            var msg = $"Calc Took {sw.ElapsedMilliseconds}ms (in microsecond: {sw.Elapsed.TotalMicroseconds}). Value=[{result:f3}]";
            return msg;
        }

        public async Task<string> EatMemory(int? n)
        {
            int N = n ?? 10_000_000;
            Console.WriteLine($"[{DateTime.Now:hh:mm:ss}] {nameof(DoCalc)} N=[{N}]");
            var before = Process.GetCurrentProcess().WorkingSet64;

            var sw = Stopwatch.StartNew();
            IList memory = new ArrayList();            

            for (int i = 0; i < 10; i++)
            {
                var data = new List<string>(capacity:N);
                memory.Add(data);
                await Task.Delay(100);
            }

            var after = Process.GetCurrentProcess().WorkingSet64;
            sw.Stop();
            var msg = $"Memory Took {sw.ElapsedMilliseconds}ms. WorkingSet64=[{before} -> {after}]";
            return msg;
        }

        private static double CalculateValue(int n = 10_000_000)
        {
            double result = 1;
            for (int i = 0; i < n; i++)
            {
                CalcKind kind = (CalcKind)((int)Math.Ceiling(Math.Abs(result)) % 3);
                result += kind switch
                {
                    CalcKind.A => CalcSin(i + result),
                    CalcKind.B => CalcLn(i + result),
                    CalcKind.C => CalcCos(i + result),
                    _ => throw new NotImplementedException(),
                };
            }
            return result;
        }

        private enum CalcKind
        {
            A = 0,
            B = 1,
            C = 2
        }

        private static double CalcSin(double d) => Math.Sin(d);
        private static double CalcLn(double d) => Math.Log(1 + d*d);
        private static double CalcCos(double d) => Math.Cos(Math.PI * d * (1-d));
    }
}
