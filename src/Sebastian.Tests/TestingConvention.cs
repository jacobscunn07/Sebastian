using Fixie;

namespace Sebastian.Tests
{
    public class TestingConvention : Discovery, Execution
    {
        public TestingConvention()
        {
            Classes
            .Where(x => x.Name.EndsWith("Should"));

            Methods
                .Where(x => x.IsVoid() && x.Name != "SetUp" && !x.IsStatic);
        }

        static void SetUp(object instance)
        {
            instance.GetType().GetMethod("SetUp")?.Execute(instance);
        }

        public void Execute(TestClass testClass)
        {
            testClass.RunCases(@case =>
            {
                var instance = testClass.Construct();

                SetUp(instance);

                @case.Execute(instance);
            });
        }
    }
}
