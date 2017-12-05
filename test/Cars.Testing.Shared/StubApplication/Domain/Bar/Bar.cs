using System;
using System.Collections.Generic;
using Cars.Attributes;
using Cars.EventSource;
using Cars.Testing.Shared.StubApplication.Domain.Bar.Projections;

namespace Cars.Testing.Shared.StubApplication.Domain.Bar
{
    [ProjectionProvider(typeof(BarOnlyIdProjectionProvider))]
    [ProjectionProvider(typeof(BarWithoutMessagesProjectionProvider))]
    [ProjectionProvider(typeof(BarProjectionProvider))]
    [ProjectionProvider(typeof(BarProjectionProvider))]
    public class Bar : Mutator
    {
        private List<string> _messages = new List<string>();

        public string LastText { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public IReadOnlyList<string> Messages
        {
            get => _messages.AsReadOnly();

	        private set => _messages = new List<string>(value);
        }
        
        public Bar()
        {
        }

        private Bar(Guid id)
        {
            Emit(new BarCreated(id));
        }

        public static Bar Create(Guid id)
        {
            return new Bar(id);
        }

        public void Speak(string text)
        {
            Emit(new SpokeSomething(text));
        }

        protected override void RegisterEvents()
        {
            SubscribeTo<BarCreated>(e =>
            {
                AggregateId = e.AggregateId;
                UpdatedAt = DateTime.Now;
            });

            SubscribeTo<SpokeSomething>(e =>
            {
                LastText = e.Text;
                UpdatedAt = DateTime.Now;

                _messages.Add(e.Text);
            });
        }
    }
}