using MoreMountains.NiceVibrations;

public static class VibrationSystem
{
	public static void Selection()
	{
		if (GameSettings.VibrationEnabled == false)
		{
			return;
		}

		MMVibrationManager.Haptic(HapticTypes.Selection);
	}

	public static void Success()
	{
		if (GameSettings.VibrationEnabled == false)
		{
			return;
		}

		MMVibrationManager.Haptic(HapticTypes.Success);
	}

	public static void Warning()
	{
		if (GameSettings.VibrationEnabled == false)
		{
			return;
		}

		MMVibrationManager.Haptic(HapticTypes.Warning);
	}

	public static void Failure()
	{
		if (GameSettings.VibrationEnabled == false)
		{
			return;
		}

		MMVibrationManager.Haptic(HapticTypes.Failure);
	}

	public static void LightImpact()
	{
		if (GameSettings.VibrationEnabled == false)
		{
			return;
		}

		MMVibrationManager.Haptic(HapticTypes.LightImpact);
	}

	public static void MediumImpact()
	{
		if (GameSettings.VibrationEnabled == false)
		{
			return;
		}

		MMVibrationManager.Haptic(HapticTypes.MediumImpact);
	}

	public static void HeavyImpact()
	{
		if (GameSettings.VibrationEnabled == false)
		{
			return;
		}

		MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
	}

	public static void RigidImpact()
	{
		if (GameSettings.VibrationEnabled == false)
		{
			return;
		}

		MMVibrationManager.Haptic(HapticTypes.RigidImpact);
	}

	public static void SoftImpact()
	{
		if (GameSettings.VibrationEnabled == false)
		{
			return;
		}

		MMVibrationManager.Haptic(HapticTypes.SoftImpact);
	}
}
