using System;

public interface ITile
{
    // Activates the tile, returns whether or not this tile remains
    // "active". If false, the effect is immediate.
    bool tileActivated(BoardController parent);
    event Action onTileDeactivated;
}
