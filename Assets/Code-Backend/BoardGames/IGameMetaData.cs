
public interface IGameMetaData
{
    byte Id { get; set; }
    int GetMaxPlayers();
    string GetScene();
    void InstantiateScene();
    string GetGameName();
    IBoardGame GetBoardGame();

}
