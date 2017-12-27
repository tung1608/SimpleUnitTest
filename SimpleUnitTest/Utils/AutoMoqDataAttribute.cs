using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using Ploeh.AutoFixture.NUnit2;

namespace SimpleUnitTest.Utils
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute()
            : base(
            new Fixture(
                new FilteringRelays(sb => !(sb is MethodInvoker)))
                    .Customize(new AutoFreezeMoq()))
        {

        }
    }
}
