// Copyright Epic Games, Inc. All Rights Reserved.

using UnrealBuildTool;

public class LivingWorld : ModuleRules
{
	public LivingWorld(ReadOnlyTargetRules Target) : base(Target)
	{
		PCHUsage = PCHUsageMode.UseExplicitOrSharedPCHs;

		PublicDependencyModuleNames.AddRange(new string[] {
			"Core",
			"CoreUObject",
			"Engine",
			"InputCore",
			"EnhancedInput",
			"AIModule",
			"StateTreeModule",
			"GameplayStateTreeModule",
			"UMG",
			"Slate"
		});

		PrivateDependencyModuleNames.AddRange(new string[] { });

		PublicIncludePaths.AddRange(new string[] {
			"LivingWorld",
			"LivingWorld/Variant_Platforming",
			"LivingWorld/Variant_Platforming/Animation",
			"LivingWorld/Variant_Combat",
			"LivingWorld/Variant_Combat/AI",
			"LivingWorld/Variant_Combat/Animation",
			"LivingWorld/Variant_Combat/Gameplay",
			"LivingWorld/Variant_Combat/Interfaces",
			"LivingWorld/Variant_Combat/UI",
			"LivingWorld/Variant_SideScrolling",
			"LivingWorld/Variant_SideScrolling/AI",
			"LivingWorld/Variant_SideScrolling/Gameplay",
			"LivingWorld/Variant_SideScrolling/Interfaces",
			"LivingWorld/Variant_SideScrolling/UI"
		});

		// Uncomment if you are using Slate UI
		// PrivateDependencyModuleNames.AddRange(new string[] { "Slate", "SlateCore" });

		// Uncomment if you are using online features
		// PrivateDependencyModuleNames.Add("OnlineSubsystem");

		// To include OnlineSubsystemSteam, add it to the plugins section in your uproject file with the Enabled attribute set to true
	}
}
