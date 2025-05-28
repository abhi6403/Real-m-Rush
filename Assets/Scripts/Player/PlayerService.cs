namespace RealmRush.Player
{
    public class PlayerService
    {
        private PlayerController _playerController;

        public PlayerService(PlayerView playerView) => _playerController = new PlayerController(playerView);
    }
}