using System;
using System.Collections.Generic;
using Com.Lepecki.BattleshipGame.Engine.Data;
using Com.Lepecki.BattleshipGame.Engine.Gears;

namespace Com.Lepecki.BattleshipGame.Engine.Model
{
    public class Game : IObservable<GameEvent>
    {
        private readonly List<IObserver<GameEvent>> _observers = new List<IObserver<GameEvent>>();
        private readonly Guid _firstPlayerId;
        private readonly Guid _secondPlayerId;
        private readonly GridContainer _container;
\
        private Guid? _expectedPlayerId = null;
        private Guid? _winnerPlayerId = null;

        public Game(Guid firstPlayerId, Guid secondPlayerId)
        {
            _firstPlayerId = firstPlayerId;
            _secondPlayerId = secondPlayerId;
            _container = new GridContainer(firstPlayerId, secondPlayerId);
        }

        public Guid Id { get; } = Guid.NewGuid();

        public bool TryPut(GameEvent gameEvent, out string message)
        {
            if (gameEvent.GameId != Id)
            {
                message = "Wrong game";
                return false;
            }

            if (gameEvent.PlayerId != _firstPlayerId && gameEvent.PlayerId != _secondPlayerId)
            {
                message = "You don't play here";
                return false;
            }

            if (_expectedPlayerId != null && gameEvent.PlayerId != _expectedPlayerId)
            {
                message = "It's not your turn";
                return false;
            }

            if (_winnerPlayerId != null)
            {
                message = $"The game already has ended and player {_winnerPlayerId} has won";
                return false;
            }

            switch (gameEvent)
            {
                case PlaceBattleshipEvent pbe:
                    return PlaceShip(pbe.PlayerId, Occupant.Battleship, pbe.Stern, pbe.Orientation, out message);

                case PlaceDestroyerEvent pde:
                    return PlaceShip(pde.PlayerId, Occupant.Destroyer, pde.Stern, pde.Orientation, out message);

                case FireEvent fe:
                    return Fire(fe.PlayerId, fe.Target, out message);
            }

            message = "Unknown error";
            return false;
        }

        public IDisposable Subscribe(IObserver<GameEvent> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }

            return new Unsubscriber(_observers, observer);
        }

        private bool PlaceShip(Guid playerId, Occupant shipClass, Coordinate stern, Orientation orientation, out string message)
        {
            GridBuilder? gridBuilder = _container.GetGridBuilder(playerId);

            if (gridBuilder == null)
            {
                message = $"Player {playerId} grid has already been built";
                return false;
            }

            try
            {
                switch (shipClass)
                {
                    case Occupant.Battleship:
                        _container.GetGridBuilder(playerId)?.PlaceBattleship(stern, orientation);
                        PostPlacement(playerId);
                        message = string.Empty;
                        return true;

                    case Occupant.Destroyer:
                        _container.GetGridBuilder(playerId)?.PlaceDestroyer(stern, orientation);
                        PostPlacement(playerId);
                        message = string.Empty;
                        return true;

                    default:
                        message = "Unknown ship class";
                        return false;
                }

            }
            catch (Exception exception)
            {
                message = exception.Message;
                return false;
            }

            void PostPlacement(Guid playerId)
            {
                if (_container.CanBuild(playerId))
                {
                    _container.BuildGrid(playerId);

                    if (_container.GetGrid(_firstPlayerId) != null && _container.GetGrid(_secondPlayerId) != null)
                    {
                        _expectedPlayerId = _firstPlayerId;
                    }
                }
            }
        }

        private bool Fire(Guid playerId, Coordinate coordinate, out string message)
        {
            if (_expectedPlayerId == null)
            {
                message = "The game hasn't yet started";
                return false;
            }

            Guid otherPlayerId = playerId == _firstPlayerId ? _secondPlayerId : _firstPlayerId;
            Grid? grid = _container.GetGrid(otherPlayerId);

            try
            {
                grid?.Hit(coordinate);

                if (grid?.CurrentHits == grid?.MaxHits)
                {
                    _winnerPlayerId = playerId;
                }

                message = string.Empty;
                return true;
            }
            catch (Exception exception)
            {
                message = exception.Message;
                return false;
            }
        }

        private class Unsubscriber : IDisposable
        {
            private readonly List<IObserver<GameEvent>> _observers;
            private readonly IObserver<GameEvent> _observer;

            public Unsubscriber(List<IObserver<GameEvent>> observers, IObserver<GameEvent> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observers.Contains(_observer))
                {
                    _observers.Remove(_observer);
                }
            }
        }
    }
}
