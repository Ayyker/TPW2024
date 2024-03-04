using Microsoft.VisualStudio.TestPlatform.TestHost;
using TPW2024;

namespace TWP_Testy {
    public class Tests {
        [SetUp]
        public void Setup() {
        }

        [Test]
        public void Test1() {
            Pies fafik = new Pies();
            Assert.True(fafik.CheckPies());
        }
    }
}