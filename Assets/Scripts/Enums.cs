public enum Edge
{
    Undefined = 0,
    Up,
    Down,
    Right,
    Left,
    Random = 99,
}

public enum GameState
{ 
    Undefinded = 0,
    OnTitle,
    OnCombat,
    OnPaused,
    OnUpgrade,
    GameOver,
    GameClear,
}

public enum SoundType
{
    Bgm,
    Sfx
}

public enum UpgradeType
{
    Ship,
    Shooter,
    Missle,
    Pulse,
    Supercharge
}

public enum UpgradeField
{
    Shield,
    Impact,
    MultiShot,
    Heat,
    Missle,
    Reload,
    Power,
    Charge
}