using Com.Lepecki.BattleshipGame.Engine.Exceptions;
using Com.Lepecki.BattleshipGame.Engine.Model;
using Xunit;

namespace Com.Lepecki.BattleshipGame.Engine.Tests.Model
{
    public class EmptyFieldTests
    {
        [Fact]
        public void NotFiredAtOwnerView()
        {
            Field field = new EmptyField();
            Assert.Equal(Occupant.Empty, field.GetOwnerView());
        }

        [Fact]
        public void NotFiredAtOpponentView()
        {
            Field field = new EmptyField();
            Assert.Equal(Occupant.Hidden, field.GetOpponentView());
        }

        [Fact]
        public void FiredAtOwnerView()
        {
            Field field = new EmptyField();
            field.Hit();
            Assert.Equal(Occupant.Empty, field.GetOwnerView());
        }

        [Fact]
        public void FiredAtOpponentView()
        {
            Field field = new EmptyField();
            field.Hit();
            Assert.Equal(Occupant.Missed, field.GetOpponentView());
        }

        [Fact]
        public void FiredAtTwice()
        {
            Field field = new EmptyField();
            field.Hit();
            Assert.Throws<RuleViolationException>(field.Hit);
        }
    }
}
