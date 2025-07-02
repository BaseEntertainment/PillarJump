using Lofelt.NiceVibrations;

public static class VibrationSystem
{
	public static void PlayPreset(HapticPatterns.PresetType type)
	{
		if (GameSettings.VibrationEnabled == false)
		{
			return;
		}

		HapticPatterns.PlayPreset(type);
	}

	public static void Selection() => PlayPreset(HapticPatterns.PresetType.Selection);

	public static void Success() => PlayPreset(HapticPatterns.PresetType.Success);

	public static void Warning() => PlayPreset(HapticPatterns.PresetType.Warning);

	public static void Failure() => PlayPreset(HapticPatterns.PresetType.Failure);

	public static void LightImpact() => PlayPreset(HapticPatterns.PresetType.LightImpact);

	public static void MediumImpact() => PlayPreset(HapticPatterns.PresetType.MediumImpact);

	public static void HeavyImpact() => PlayPreset(HapticPatterns.PresetType.HeavyImpact);

	public static void RigidImpact() => PlayPreset(HapticPatterns.PresetType.RigidImpact);

	public static void SoftImpact() => PlayPreset(HapticPatterns.PresetType.SoftImpact);
}
