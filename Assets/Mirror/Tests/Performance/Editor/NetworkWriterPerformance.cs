using Mirror;
using NUnit.Framework;
using Unity.PerformanceTesting;

namespace Tests
{
    [Category("Performance")]
    public class NetworkWriterPerformance
    {
        // A Test behaves as an ordinary method
        [Test]
#if UNITY_2019_2_OR_NEWER
        [Performance]
#else
        [PerformanceTest]
#endif
        [Version("2")]
        public void WritePackedInt32()
        {
            Measure.Method(WriteInt32)
                .WarmupCount(10)
                .MeasurementCount(100)
                .Run();
        }

        static void WriteInt32()
        {
            for (int j = 0; j < 1000; j++)
            {
                using (PooledNetworkWriter writer = NetworkWriterPool.GetWriter())
                {
                    for (int i = 0; i < 10; i++)
                    {
                        writer.WritePackedInt32(i * 1000);
                    }
                }
            }
        }
    }
}
