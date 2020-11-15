using Com.Lepecki.BattleshipGame.Engine.Exceptions;
using Com.Lepecki.BattleshipGame.Engine.Model;
using Xunit;

namespace Com.Lepecki.BattleshipGame.Engine.Tests.Model
{
    public abstract class FieldTests<T> where T : Field
    {
        protected abstract Field CreateField();
        protected abstract Occupant ExpectedNotFiredAtOwnerView { get; }
        protected abstract Occupant ExpectedNotFiredAtOpponentView { get; }
        protected abstract Occupant ExpectedFiredAtOwnerView { get; }
        protected abstract Occupant ExpectedFiredAtOpponentView { get; }

        [Fact]
        public void NotFiredAtOwnerView()
        {
            Field field = CreateField();
            Assert.Equal(ExpectedNotFiredAtOwnerView, field.GetOwnerView());
        }

        [Fact]
        public void NotFiredAtOpponentView()
        {
            Field field = CreateField();
            Assert.Equal(ExpectedNotFiredAtOpponentView, field.GetOpponentView());
        }

        [Fact]
        public void FiredAtOwnerView()
        {
            Field field = CreateField();
            field.Fire();
            Assert.Equal(ExpectedFiredAtOwnerView, field.GetOwnerView());
        }

        [Fact]
        public void FiredAtOpponentView()
        {
            Field field = CreateField();
            field.Fire();
            Assert.Equal(ExpectedFiredAtOpponentView, field.GetOpponentView());
        }

        [Fact]
        public void FiredAtTwice()
        {
            Field field = CreateField();
            field.Fire();
            Assert.Throws<RuleViolationException>(field.Fire);
        }
    }
}
