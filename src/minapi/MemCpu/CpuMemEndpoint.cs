using System.Diagnostics;
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
            var sw = Stopwatch.StartNew();
            var result = n is null ? CalculateValue() : CalculateValue(n.Value);
            sw.Stop();
            var msg = $"Took {sw.ElapsedMilliseconds}ms (in microsecond: {sw.Elapsed.TotalMicroseconds}). Value=[{result:f3}]";
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
