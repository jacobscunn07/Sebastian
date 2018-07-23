using Fixie;

namespace Sebastian.Tests
{
    public class TestingConvention : Discovery
    {
        public TestingConvention()
        {
            Classes
            .Where(x => x.Name.EndsWith("Should"));

            Methods
                .Where(x => x.IsVoid());
        }
    }
}
