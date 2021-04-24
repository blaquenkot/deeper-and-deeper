using System;

public interface ITile 
{
    void tileActivated(BoardController parent);
    event Action onTileDeactivated;
}