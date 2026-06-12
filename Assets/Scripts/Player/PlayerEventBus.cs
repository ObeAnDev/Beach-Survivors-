using System;

public static class PlayerEventBus
{
    public static Action<int> OnLevelChanged;
    public static Action<float> OnExpChanged;
    public static Action<int> OnCoinChange;
    public static Action OnLevelUp;

    public static Action<float> OnHealthChanged;

    public static Action OnPlayerDeath;
}
