namespace Zombat.Game
{
    class Game
    {
        private readonly Map _map;
        private readonly Player _player;

        public Game(Map map)
        {
            _map = map;
            _player = new Player(_map.Spawn);
        }
    }
}
