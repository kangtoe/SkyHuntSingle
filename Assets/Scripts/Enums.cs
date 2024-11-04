public enum Edge
{
    Up = 0,
    Down,
    Right,
    Left
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