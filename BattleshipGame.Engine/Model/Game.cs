using System;
using System.Collections.Generic;
using Com.Lepecki.BattleshipGame.Engine.Data;
using Com.Lepecki.BattleshipGame.Engine.Gears;

namespace Com.Lepecki.BattleshipGame.Engine.Model
{
    public class Game : IObservable<GameEvent>
    {
        private readonly List<IObserver<GameEvent>> _observers = new List<IObserver<GameEvent>>();
        private readonly Occupant[,] _emptyView;

        private GridBuilder? _gridBuilder;
        private Grid? _grid = null;

        public Game(ICoordinateCalculator coordinateCalculator, GridBuilderOptions gridBuilderOptions)
        {
            _gridBuilder = new GridBuilder(coordinateCalculator, gridBuilderOptions);
            _emptyView = new Occupant[gridBuilderOptions.Size, gridBuilderOptions.Size];
        }

        public Guid Id { get; } = Guid.NewGuid();

        public bool Finished { get; private set; } = false;

        public bool TryPut(GameEvent gameEvent, out string message)
        {
            if (gameEvent.GameId != Id)
            {
                message = "Wrong game";
                return false;
            }

            if (Finished)
            {
                message = $"The game has already ended";
                return false;
            }

            bool success = false;
            message = "Unknown error";

            switch (gameEvent)
            {
                case PlaceBattleshipEvent pbe:
                    success = PlaceShip(Occupant.Battleship, pbe.Stern, pbe.Orientation, out message);
                    break;

                case PlaceDestroyerEvent pde:
                    success = PlaceShip(Occupant.Destroyer, pde.Stern, pde.Orientation, out message);
                    break;

                case FireEvent fe:
                    success = Fire(fe.Target, out message);
                    break;
            }

            if (success)
            {
                NotifySubscribers(gameEvent);
            }

            return success;
        }

        public Occupant[,] GetOpponentView()
        {
            if (_grid == null)
            {
                return _emptyView;
            }

            return _grid!.GetOpponentView();
        }

        public IDisposable Subscribe(IObserver<GameEvent> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }

            return new Unsubscriber(_observers, observer);
        }

        private bool PlaceShip(Occupant shipClass, Coordinate stern, Orientation orientation, out string message)
        {
            if (_gridBuilder == null)
            {
                message = $"The grid has already been built";
                return false;
            }

            try
            {
                switch (shipClass)
                {
                    case Occupant.Battleship:
                        _gridBuilder?.PlaceBattleship(stern, orientation);
                        break;

                    case Occupant.Destroyer:
                        _gridBuilder?.PlaceDestroyer(stern, orientation);
                        break;

                    default:
                        message = "Unknown ship class";
                        return false;
                }

                if (_gridBuilder!.CanBuild)
                {
                    _grid = _gridBuilder.Build();
                    _gridBuilder = null;
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

        private bool Fire(Coordinate coordinate, out string message)
        {
            if (_grid == null)
            {
                message = "The game hasn't yet started";
                return false;
            }

            try
            {
                _grid?.Hit(coordinate);

                if (_grid?.CurrentHits == _grid?.MaxHits)
                {
                    Finished = true;
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

        private void NotifySubscribers(GameEvent gameEvent)
        {
            foreach (IObserver<GameEvent> observer in _observers)
            {
                observer.OnNext(gameEvent);
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
