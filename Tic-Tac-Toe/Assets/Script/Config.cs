
namespace Assets.Script
{
    public class Config
    {
        private int size;
        public GameSave gameSave { get; }
    
        public int Size
        {
            get
            {
                return size;
            }
        }
        public Config(int size, GameSave gameSave)
        {
            this.size = size;
            this.gameSave = gameSave;
        }
    }
}
