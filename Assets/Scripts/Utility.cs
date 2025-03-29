[System.Serializable]
enum PlayerState
{
    Idle,
    Parry,
    Dodge,
    Block,
}

public enum AttackType
{
    Normal,
    Heavy,
    Parryable
}

public enum GameState
{
    preparation,
    playing,
    endgame
}
