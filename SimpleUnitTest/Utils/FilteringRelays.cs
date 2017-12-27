using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace SimpleUnitTest.Utils
{
    public class FilteringRelays : DefaultEngineParts
    {
        private readonly Func<ISpecimenBuilder, bool> spec;

        public FilteringRelays(Func<ISpecimenBuilder, bool> specification)
        {
            if (specification == null)
            {
                throw new ArgumentNullException("specification");
            }

            spec = specification;
        }

        public override IEnumerator<ISpecimenBuilder> GetEnumerator()
        {
            var enumerator = base.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (spec(enumerator.Current))
                {
                    yield return enumerator.Current;
                }
            }
        }
    }
}
