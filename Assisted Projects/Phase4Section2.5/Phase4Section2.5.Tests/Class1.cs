using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace Phase4Section2._5.Tests
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void BasicAssertions()
        {
            int total = 100, marks1 = 60, marks2 = 75;
            string name = null;

            Assert.That(marks1, Is.Not.EqualTo(marks2));
            Assert.That(marks1, Is.LessThan(marks2));
            Assert.That(marks2, Is.InRange(50, 75));

            Assert.That(name, Is.Null);
        }

        [Test]
        public void Warnings()
        {
            int total = 100, marks1 = 60, marks2 = 75;
            string name = null;

            Warn.If(marks1 > 100);
            Warn.If(name == null);

            Warn.Unless(marks1 + marks2 < 200);

            Assert.Warn("This is a warning message");
        }

        [Test]
        public void ArrangeActAssert()
        {
            var calc = new Calculator();
            var answer = calc.add(5, 19);

            Assert.That(answer, Is.EqualTo(24));
        }

        [Test]
        public void MultipleAssertions()
        {
            int total = 100, marks1 = 60, marks2 = 75;
            string name = null;

            Assert.Multiple(() =>
            {
                Assert.That(marks1, Is.Not.EqualTo(marks2));
                Assert.That(marks1, Is.LessThan(marks2));
                Assert.That(marks2, Is.InRange(50, 75));
            });

        }

        [Test]
        public void Exceptions()
        {
            var calc = new Calculator();

            Assert.Throws<InvalidOperationException>(() => calc.addStrings("aaa", "Bbb"));
        }

        [Test]
        [TestCase(10, 20, ExpectedResult = 30)]
        [TestCase(100, 200, ExpectedResult = 300)]
        [TestCase(1000, 2000, ExpectedResult = 3000)]
        public int DataDriven1(int x, int y)
        {
            var calc = new Calculator();
            return calc.add(x, y);
        }

        public static List<TestCaseData> TestCases
        {
            get
            {
                var testCases = new List<TestCaseData>();

                using (var fs = File.OpenRead("c:\\testdata.txt"))
                using (var sr = new StreamReader(fs))
                {
                    string line = string.Empty;
                    while (line != null)
                    {
                        line = sr.ReadLine();
                        if (line != null)
                        {
                            string[] split = line.Split(new char[] { ',' },
                                StringSplitOptions.None);

                            int a = Convert.ToInt32(split[0]);
                            int b = Convert.ToInt32(split[1]);
                            int answer = Convert.ToInt32(split[2]);

                            var testCase = new TestCaseData(a, b).Returns(answer);
                            testCases.Add(testCase);
                        }
                    }
                }

                return testCases;
            }


        }

        [Test]
        [TestCaseSource("TestCases")]
        public int DataDriven2(int x, int y)
        {
            var calc = new Calculator();
            return calc.add(x, y);
        }

        [Test]
        public void Mocking()
        {
            int x = 9, y = 19;
            Mock<ICalculator> mockCalc = new Mock<ICalculator>();
            ICalculator calc = mockCalc.Object;
            Assert.That(calc, Is.Not.Null);
        }

        [Test]
        public void Stub()
        {
            int x = 9, y = 19;
            Mock<ICalculator> mockCalc = new Mock<ICalculator>();
            mockCalc
                .Setup(c => c.add(It.IsAny<Int32>(), It.IsAny<Int32>()))
                .Returns(x + y);

            ICalculator calc = mockCalc.Object;
            Assert.That(calc.add(x, y), Is.EqualTo(x + y));
        }

        [Test]
        public void Fake()
        {
            int x = 9, y = 19;
            FakeCalculator calc = new FakeCalculator();
            Assert.That(calc.add(x, y), Is.GreaterThan(0));
        }

        [Test]
        public void StaticFake()
        {
            int x = 10, y = 20;
            var wrapper = new SCalcWrapper();
            Assert.That(wrapper.add(x, y), Is.EqualTo(x + y));
        }

        [Test]
        public void DynamicFake()
        {
            int x = 10, y = 20;
            Mock<IDynamicCalc> mockCalc = new Mock<IDynamicCalc>();
            var result = new
            {
                V = Convert.ToInt32(x + y)
            };
            mockCalc.Setup(c => c.add(It.IsAny<object>(), It.IsAny<object>())).Returns(result);
        }
    }
}

