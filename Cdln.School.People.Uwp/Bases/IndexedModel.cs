using System;

namespace Cdln.School.People.Uwp
{
    public abstract class IndexedModel : Model
    {
        public Guid Index { get; set; }

        public IndexedModel(Guid id, Guid index)
            : base(id) { Index = index; }
    }
}
