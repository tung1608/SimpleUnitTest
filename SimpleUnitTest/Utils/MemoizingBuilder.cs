using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture.Kernel;

namespace SimpleUnitTest.Utils
{
    public class MemoizingBuilder : ISpecimenBuilder
    {
        private readonly ISpecimenBuilder _builder;
        private readonly ConcurrentDictionary<object, object> _instances;

        public MemoizingBuilder(ISpecimenBuilder builder)
        {
            _builder = builder;
            _instances = new ConcurrentDictionary<object, object>();
        }

        public object Create(object request, ISpecimenContext context)
        {
            return _instances.GetOrAdd(
                request,
                r => _builder.Create(r, context));
        }
    }
}
