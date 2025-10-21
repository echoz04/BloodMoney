using Cysharp.Threading.Tasks;

namespace Sources.Runtime.Services.Loaders.GameData
{
    public class GameDataLoader : IGameDataLoader
    {
        private const string GameDataPath = "GameData";
        
        private Game.GameData _data;
        
        public Game.GameData Get() =>
            _data;

        public async UniTask<Game.GameData> LoadAsync()
        {
            return _data ??= (Game.GameData)await UnityEngine.Resources.LoadAsync<Game.GameData>(GameDataPath);
        }
    }
}