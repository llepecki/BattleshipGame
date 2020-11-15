using System;
using Com.Lepecki.BattleshipGame.Engine.Gears;

namespace Com.Lepecki.BattleshipGame.Engine.Model
{
    public class GridContainer
    {
        private readonly Guid _firstPlayerId;
        private readonly Guid _secondPlayerId;

        private GridBuilder? _firstPlayerGridBuilder;
        private GridBuilder? _secondPlayerGridBuilder;
        private Grid? _firstPlayerGrid = null;
        private Grid? _secondPlayerGrid = null;

        public GridContainer(Guid firstPlayerId, Guid secondPlayerId)
        {
            _firstPlayerId = firstPlayerId;
            _secondPlayerId = secondPlayerId;

            ICoordinateCalculator calculator = new CoordinateCalculator();
            GridBuilderOptions options = new GridBuilderOptions();

            _firstPlayerGridBuilder = new GridBuilder(calculator, options);
            _secondPlayerGridBuilder = new GridBuilder(calculator, options);
        }

        public bool CanBuild(Guid playerId)
        {
            ThrowWhenUnknownPlayerId(playerId);
            
            if (playerId == _firstPlayerId)
            {
                if (_firstPlayerGridBuilder == null)
                {
                    return false;
                }

                return _firstPlayerGridBuilder.CanBuild;
            }
            else
            {
                if (_secondPlayerGridBuilder == null)
                {
                    return false;
                }

                return _secondPlayerGridBuilder.CanBuild;
            }
        }

        public bool GridReady(Guid playerId)
        {
            ThrowWhenUnknownPlayerId(playerId);
            return playerId == _firstPlayerId ? _firstPlayerGrid != null : _secondPlayerGrid != null;
        }

        public GridBuilder? GetGridBuilder(Guid playerId)
        {
            ThrowWhenUnknownPlayerId(playerId);
            return playerId == _firstPlayerId ? _firstPlayerGridBuilder : _secondPlayerGridBuilder;
        }

        public Grid? GetGrid(Guid playerId)
        {
            ThrowWhenUnknownPlayerId(playerId);
            return playerId == _firstPlayerId ? _firstPlayerGrid : _secondPlayerGrid;
        }

        public void BuildGrid(Guid playerId)
        {
            ThrowWhenUnknownPlayerId(playerId);

            if (playerId == _firstPlayerId)
            {
                if (_firstPlayerGridBuilder == null)
                {
                    throw new InvalidOperationException($"Player {playerId} grid has already been built");
                }

                if (!_firstPlayerGridBuilder.CanBuild)
                {
                    throw new InvalidOperationException($"Player {playerId} can't be bult");
                }

                _firstPlayerGrid = _firstPlayerGridBuilder.Build();
                _firstPlayerGridBuilder = null;
            }
            else
            {
                if (_secondPlayerGridBuilder == null)
                {
                    throw new InvalidOperationException($"Player {playerId} grid has already been built");
                }

                if (!_secondPlayerGridBuilder.CanBuild)
                {
                    throw new InvalidOperationException($"Player {playerId} can't be bult");
                }

                _secondPlayerGrid = _secondPlayerGridBuilder.Build();
                _secondPlayerGridBuilder = null;
            }
        }

        private void ThrowWhenUnknownPlayerId(Guid playerId)
        {
            if (playerId != _firstPlayerId && playerId != _secondPlayerId)
            {
                throw new ArgumentOutOfRangeException(nameof(playerId));
            }
        }
    }
}
