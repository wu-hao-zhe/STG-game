using Godot;
using System;

public partial class Area2d2 : Area2D
{
	[Export] public Area2D HitArea;
	[Export] public PackedScene UIPrefab;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		HitArea.AreaEntered += (otherArea) =>	{
			
			var pos=new Vector2(0,0);
			var e=UIPrefab.Instantiate() as Node2D;
			GetTree().Root.AddChild(e);
			e.GlobalPosition=pos;
			GetTree().Paused=true;
		};
	}
}
